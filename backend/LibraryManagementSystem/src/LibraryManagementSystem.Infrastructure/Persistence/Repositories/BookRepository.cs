using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class BookRepository(
    LibraryDbContext context
) : IBookRepository
{
    public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Copies)
            .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted, cancellationToken);
    }

    public async Task<Book?> GetByIsbn13Async(string isbn13, CancellationToken cancellationToken = default)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Isbn13 == isbn13 && !b.IsDeleted, cancellationToken);
    }

    public async Task AddAsync(Book book, CancellationToken cancellationToken = default)
    {
        await context.Books.AddAsync(book, cancellationToken);
    }

    public async Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Copies)
            .ThenInclude(c => c.Rentals)
            .ThenInclude(r => r.User)
            .Where(b => !b.IsDeleted)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<(Book Book, int TotalCopies, int AvailableCopies)>> SearchBooksAsync(
            string? title,
            string? authorName,
            string? genreName,
            string? isbn13,
            CancellationToken cancellationToken = default)
    {
        var query = context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Copies)
            .ThenInclude(c => c.Rentals)
            .ThenInclude(r => r.User)
            .Where(b => !b.IsDeleted);

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b => EF.Functions.Like(b.Title, $"%{title}%"));
        }

        if (!string.IsNullOrWhiteSpace(authorName))
        {
            query = query.Where(b =>
                EF.Functions.Like(b.Author.FirstName!, $"%{authorName}%") ||
                EF.Functions.Like(b.Author.LastName, $"%{authorName}%"));
        }

        if (!string.IsNullOrWhiteSpace(genreName))
        {
            query = query.Where(b => EF.Functions.Like(b.Genre.Name, $"%{genreName}%"));
        }

        if (!string.IsNullOrWhiteSpace(isbn13))
        {
            query = query.Where(b => EF.Functions.Like(b.Isbn13, $"%{isbn13}%"));
        }

        var books = await query.ToListAsync(cancellationToken);

        var result = books.Select(b => (
            Book: b,
            TotalCopies: b.Copies.Count,
            AvailableCopies: b.Copies.Count(c => !c.Rentals.Any(r => r.ReturnDate == null))
        )).ToList();

        return result;
    }
}

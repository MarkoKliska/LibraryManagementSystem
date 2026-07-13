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

    public async Task<(IEnumerable<Book> Books, int TotalCount)> SearchBooksAsync(
            string? title,
            string? authorName,
            string? genreName,
            string? isbn13,
            bool availableOnly,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
    {
        var query = context.Books
            .AsNoTracking()
            .AsSplitQuery()
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Copies.Where(c => !c.IsDeleted))
                .ThenInclude(c => c.Rentals.Where(r => r.ReturnDate == null))
            .Where(b => !b.IsDeleted)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(title))
        {
            var titleTrimmed = title.Trim();
            query = query.Where(b => EF.Functions.ILike(b.Title, $"%{titleTrimmed}%"));
        }

        if (!string.IsNullOrWhiteSpace(authorName))
        {
            var authorTrimmed = authorName.Trim();
            query = query.Where(b =>
                EF.Functions.ILike(b.Author.FirstName ?? "", $"%{authorTrimmed}%") ||
                EF.Functions.ILike(b.Author.LastName, $"%{authorTrimmed}%"));
        }

        if (!string.IsNullOrWhiteSpace(genreName))
        {
            var genreTrimmed = genreName.Trim();
            query = query.Where(b => EF.Functions.ILike(b.Genre.Name, $"%{genreTrimmed}%"));
        }

        if (!string.IsNullOrWhiteSpace(isbn13))
        {
            var isbnTrimmed = isbn13.Trim();
            query = query.Where(b => EF.Functions.Like(b.Isbn13, $"%{isbnTrimmed}%"));
        }

        if (availableOnly)
        {
            query = query.Where(b => b.Copies.Any(c =>
                !c.IsDeleted && !c.Rentals.Any(r => r.ReturnDate == null)));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var books = await query
            .OrderBy(b => b.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (books, totalCount);
    }
    public async Task<(IEnumerable<Book> Books, int TotalCount)> GetAllPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = context.Books
            .Include(b => b.Author)
            .Include(b => b.Genre)
            .Include(b => b.Copies.Where(c => !c.IsDeleted))
                .ThenInclude(c => c.Rentals.Where(r => r.ReturnDate == null))
                    .ThenInclude(r => r.User)
            .Where(b => !b.IsDeleted)
            .OrderBy(b => b.Title);

        var totalCount = await query.CountAsync(cancellationToken);

        var books = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (books, totalCount);
    }
}

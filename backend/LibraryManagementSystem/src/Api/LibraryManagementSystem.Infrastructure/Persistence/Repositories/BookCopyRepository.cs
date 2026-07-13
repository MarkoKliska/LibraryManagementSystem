using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class BookCopyRepository(
    LibraryDbContext context
) : IBookCopyRepository
{
    public async Task<BookCopy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.BookCopies
            .Include(c => c.Book)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, cancellationToken);
    }

    public async Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        return await context.BookCopies
            .Include(c => c.Book)
            .Where(c => c.BookId == bookId && !c.IsDeleted && !c.Rentals.Any(r => r.ReturnDate == null))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(BookCopy bookCopy, CancellationToken cancellationToken = default)
    {
        await context.BookCopies.AddAsync(bookCopy, cancellationToken);
    }
}

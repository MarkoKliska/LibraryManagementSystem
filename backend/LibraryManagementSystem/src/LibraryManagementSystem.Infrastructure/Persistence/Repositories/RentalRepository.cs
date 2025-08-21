using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using LibraryManagementSystem.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Infrastructure.Persistence.Repositories;

public class RentalRepository(
    LibraryDbContext context
) : IRentalRepository
{
    public async Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Rentals
            .Include(r => r.User)
            .Include(r => r.BookCopy)
            .ThenInclude(c => c.Book)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Rental>> GetActiveRentalsByUserAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await context.Rentals
            .Include(r => r.User)
            .Include(r => r.BookCopy)
            .ThenInclude(c => c.Book)
            .Where(r => r.UserId == userId && r.ReturnDate == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<Rental?> GetActiveRentalByBookCopyAsync(Guid bookCopyId, CancellationToken cancellationToken = default)
    {
        return await context.Rentals
            .Include(r => r.User)
            .Include(r => r.BookCopy)
            .ThenInclude(c => c.Book)
            .FirstOrDefaultAsync(r => r.BookCopyId == bookCopyId && r.ReturnDate == null, cancellationToken);
    }

    public async Task AddAsync(Rental rental, CancellationToken cancellationToken = default)
    {
        await context.Rentals.AddAsync(rental, cancellationToken);
    }
}

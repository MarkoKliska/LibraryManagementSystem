using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories;

public interface IRentalRepository
{
    Task<Rental?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Rental>> GetActiveRentalsByUserAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Rental?> GetActiveRentalByBookCopyAsync(Guid bookCopyId, CancellationToken cancellationToken = default);
    Task AddAsync(Rental rental, CancellationToken cancellationToken = default);
}
using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories;

public interface IBookCopyRepository
{
    Task<BookCopy?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<BookCopy>> GetAvailableCopiesAsync(Guid bookId, CancellationToken cancellationToken = default);
    Task AddAsync(BookCopy bookCopy, CancellationToken cancellationToken = default);
}

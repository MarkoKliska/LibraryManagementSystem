using LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.Domain.Repositories;

public interface IBookRepository
{
    Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Book?> GetByIsbn13Async(string isbn13, CancellationToken cancellationToken = default);
    Task AddAsync(Book book, CancellationToken cancellationToken = default);
    Task<IEnumerable<Book>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<(Book Book, int TotalCopies, int AvailableCopies)>> SearchBooksAsync(
        string? title,
        string? authorName,
        string? genreName,
        string? isbn13,
        CancellationToken cancellationToken = default);
}
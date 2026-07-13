using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.SearchBook;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.SearchBook;

public sealed class SearchBooksQueryHandler(
    IBookRepository bookRepository
) : IRequestHandler<SearchBooksQuery, Result<IEnumerable<SearchBooksResponseDto>>>
{
    public async Task<Result<IEnumerable<SearchBooksResponseDto>>> Handle(
        SearchBooksQuery query,
        CancellationToken ct)
    {
        var books = await bookRepository.SearchBooksAsync(
            query.Request.Title,
            query.Request.AuthorName,
            query.Request.GenreName,
            query.Request.Isbn13,
            ct);

        var result = books.Select(b => new SearchBooksResponseDto
        {
            Id = b.Book.Id,
            Title = b.Book.Title,
            AuthorName = b.Book.Author.FirstName != null
                ? $"{b.Book.Author.LastName}, {b.Book.Author.FirstName}"
                : b.Book.Author.LastName,
            GenreName = b.Book.Genre.Name,
            Isbn13 = b.Book.Isbn13,
            TotalCopies = b.TotalCopies,
            AvailableCopies = b.AvailableCopies
        }).ToList();

        return Result<IEnumerable<SearchBooksResponseDto>>.Success(result);
    }
}
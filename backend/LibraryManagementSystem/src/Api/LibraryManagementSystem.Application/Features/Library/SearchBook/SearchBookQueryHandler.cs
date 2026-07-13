using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.SearchBook;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.SearchBook;

public sealed class SearchBooksQueryHandler(
    IBookRepository bookRepository
) : IRequestHandler<SearchBooksQuery, Result<PagedResult<SearchBooksResponseDto>>>
{
    public async Task<Result<PagedResult<SearchBooksResponseDto>>> Handle(
        SearchBooksQuery query,
        CancellationToken ct)
    {
        var req = query.Request;
        var page = Math.Max(1, req.Page);
        var pageSize = Math.Clamp(req.PageSize, 1, 100);

        var (books, totalCount) = await bookRepository.SearchBooksAsync(
            req.Title, req.AuthorName, req.GenreName, req.Isbn13,
            req.AvailableOnly, page, pageSize, ct);

        var items = books.Select(b => new SearchBooksResponseDto
        {
            Id = b.Id,
            Title = b.Title,
            AuthorName = b.Author.FirstName != null
                ? $"{b.Author.LastName}, {b.Author.FirstName}"
                : b.Author.LastName,
            GenreName = b.Genre.Name,
            Isbn13 = b.Isbn13,
            TotalCopies = b.Copies.Count,
            AvailableCopies = b.Copies.Count(c => !c.Rentals.Any())
        }).ToList();

        return Result<PagedResult<SearchBooksResponseDto>>.Success(new PagedResult<SearchBooksResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}
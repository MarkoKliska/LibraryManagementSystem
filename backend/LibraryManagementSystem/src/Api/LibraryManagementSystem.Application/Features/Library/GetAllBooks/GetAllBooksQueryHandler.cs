using LibraryManagementSystem.Application.DTOs.Library.GetBookList;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksQuery;

public sealed class GetAllBooksQueryHandler(
    IBookRepository bookRepository,
    IBookCopyRepository bookCopyRepository
) : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<BookListResponseDto>>>
{
    public async Task<Result<IEnumerable<BookListResponseDto>>> Handle(GetAllBooksQuery query, CancellationToken ct)
    {
        var books = await bookRepository.GetAllAsync(ct);
        var result = new List<BookListResponseDto>();

        foreach (var book in books.Where(b => !b.IsDeleted))
        {
            var copies = await bookCopyRepository.GetAvailableCopiesAsync(book.Id, ct);

            result.Add(new BookListResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.FirstName != null ? $"{book.Author.LastName}, {book.Author.FirstName}" : book.Author.LastName,
                GenreName = book.Genre.Name,
                Isbn13 = book.Isbn13,
                TotalCopies = book.Copies.Count,
                AvailableCopies = copies.Count()
            });
        }

        return Result<IEnumerable<BookListResponseDto>>.Success(result);
    }
}
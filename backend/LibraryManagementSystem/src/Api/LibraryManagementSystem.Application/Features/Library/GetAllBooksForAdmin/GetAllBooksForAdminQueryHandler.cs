using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.GetAllBooksForAdmin;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksForAdmin;

public sealed class GetAllBooksForAdminQueryHandler(
    IBookRepository bookRepository
) : IRequestHandler<GetAllBooksForAdminQuery, Result<PagedResult<BookListForAdminResponseDto>>>
{
    public async Task<Result<PagedResult<BookListForAdminResponseDto>>> Handle(GetAllBooksForAdminQuery query, CancellationToken ct)
    {
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);

        var (books, totalCount) = await bookRepository.GetAllPagedAsync(page, pageSize, ct);

        var items = books.Select(book =>
        {
            var activeRental = book.Copies.SelectMany(c => c.Rentals).FirstOrDefault();

            return new BookListForAdminResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.FirstName != null ? $"{book.Author.LastName}, {book.Author.FirstName}" : book.Author.LastName,
                GenreName = book.Genre.Name,
                Isbn13 = book.Isbn13,
                TotalCopies = book.Copies.Count,
                AvailableCopies = book.Copies.Count(c => !c.Rentals.Any()),
                RentedByUserId = activeRental?.UserId,
                RentedByUserEmail = activeRental?.User.Email
            };
        }).ToList();

        return Result<PagedResult<BookListForAdminResponseDto>>.Success(new PagedResult<BookListForAdminResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}
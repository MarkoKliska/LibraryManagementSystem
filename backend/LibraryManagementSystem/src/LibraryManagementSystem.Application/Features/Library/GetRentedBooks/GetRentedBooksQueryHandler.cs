using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.GetRentedBooks;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetRentedBooks;

public sealed class GetRentedBooksQueryHandler(
    IRentalRepository rentalRepository
) : IRequestHandler<GetRentedBooksQuery, Result<IEnumerable<RentedBookResponseDto>>>
{
    public async Task<Result<IEnumerable<RentedBookResponseDto>>> Handle(GetRentedBooksQuery query, CancellationToken ct)
    {
        var rentals = await rentalRepository.GetActiveRentalsByUserAsync(query.Request.UserId, ct);

        if (!rentals.Any())
            return Result<IEnumerable<RentedBookResponseDto>>.Success(Enumerable.Empty<RentedBookResponseDto>());

        var result = rentals.Select(r => new RentedBookResponseDto
        {
            RentalId = r.Id,
            BookId = r.BookCopy.BookId,
            Title = r.BookCopy.Book.Title,
            AuthorName = r.BookCopy.Book.Author.FirstName != null
                ? $"{r.BookCopy.Book.Author.LastName}, {r.BookCopy.Book.Author.FirstName}"
                : r.BookCopy.Book.Author.LastName,
            GenreName = r.BookCopy.Book.Genre.Name,
            Isbn13 = r.BookCopy.Book.Isbn13,
            RentalDate = r.RentalDate,
            DueDate = r.DueDate,
            BookCopyId = r.BookCopyId
        });

        return Result<IEnumerable<RentedBookResponseDto>>.Success(result);
    }
}

using LibraryManagementSystem.Application.DTOs.Library.GetAllBooksForAdmin;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksForAdmin;

public sealed class GetAllBooksForAdminQueryHandler(
    IBookRepository bookRepository,
    IBookCopyRepository bookCopyRepository
) : IRequestHandler<GetAllBooksForAdminQuery, Result<IEnumerable<BookListForAdminResponseDto>>>
{
    public async Task<Result<IEnumerable<BookListForAdminResponseDto>>> Handle(GetAllBooksForAdminQuery query, CancellationToken ct)
    {
        var books = await bookRepository.GetAllAsync(ct);
        var result = new List<BookListForAdminResponseDto>();

        foreach (var book in books.Where(b => !b.IsDeleted))
        {
            var copies = await bookCopyRepository.GetAvailableCopiesAsync(book.Id, ct);
            var activeRental = book.Copies
                .SelectMany(c => c.Rentals)
                .FirstOrDefault(r => r.ReturnDate == null);

            result.Add(new BookListForAdminResponseDto
            {
                Id = book.Id,
                Title = book.Title,
                AuthorName = book.Author.FirstName != null ? $"{book.Author.LastName}, {book.Author.FirstName}" : book.Author.LastName,
                GenreName = book.Genre.Name,
                Isbn13 = book.Isbn13,
                TotalCopies = book.Copies.Count,
                AvailableCopies = copies.Count(),
                RentedByUserId = activeRental?.UserId,
                RentedByUserEmail = activeRental?.User.Email
            });
        }

        return Result<IEnumerable<BookListForAdminResponseDto>>.Success(result);
    }
}
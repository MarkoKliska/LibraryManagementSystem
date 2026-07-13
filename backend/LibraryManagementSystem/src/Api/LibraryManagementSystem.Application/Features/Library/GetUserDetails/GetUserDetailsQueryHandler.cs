using LibraryManagementSystem.Application.DTOs.Library.GetUserDetails;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetUserDetails;

public sealed class GetUserDetailsQueryHandler(
    IUserRepository userRepository,
    IRentalRepository rentalRepository
) : IRequestHandler<GetUserDetailsQuery, Result<GetUserDetailsResponseDto>>
{
    public async Task<Result<GetUserDetailsResponseDto>> Handle(GetUserDetailsQuery query, CancellationToken ct)
    {
        var user = await userRepository.GetByIdAsync(query.UserId, ct);
        if (user == null || user.IsDeleted)
            return Result<GetUserDetailsResponseDto>.Failure("User not found");

        var rentals = await rentalRepository.GetActiveRentalsByUserAsync(query.UserId, ct);

        var response = new GetUserDetailsResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Rentals = rentals.Select(r => new RentalDetailsDto
            {
                RentalId = r.Id,
                BookId = r.BookCopy.Book.Id,
                BookTitle = r.BookCopy.Book.Title,
                RentalDate = r.RentalDate,
                DueDate = r.DueDate,
                ReturnDate = r.ReturnDate
            }).ToList()
        };

        return Result<GetUserDetailsResponseDto>.Success(response);
    }
}

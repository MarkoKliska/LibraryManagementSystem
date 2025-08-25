using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.RentBook;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.RentBook;

public sealed class RentBookCommandHandler(
    IBookCopyRepository bookCopyRepository,
    IRentalRepository rentalRepository,
    IUnitOfWork unitOfWork 
) : IRequestHandler<RentBookCommand, Result<RentBookResponseDto>>
{
    public async Task<Result<RentBookResponseDto>> Handle(RentBookCommand command, CancellationToken ct)
    {
        var availableCopies = await bookCopyRepository.GetAvailableCopiesAsync(command.Request.BookId, ct);

        if (!availableCopies.Any())
            return Result<RentBookResponseDto>.Failure("No available copies for this book.");

        //var activeRentals = await rentalRepository.GetActiveRentalsByUserAsync(command.Request.UserId, ct);
        //var hasActiveRental = activeRentals.Any(r => r.BookCopy.BookId == command.Request.BookId);

        //if (hasActiveRental)
        //    return Result<RentBookResponseDto>.Failure("You already have an active rental for this book. Please return it before renting again.");

        var bookCopy = availableCopies.First(); 

        var rental = new Rental(command.Request.UserId, bookCopy.Id);

        await rentalRepository.AddAsync(rental, ct);
        await unitOfWork.SaveChangesAsync(ct); 

        return Result<RentBookResponseDto>.Success(new RentBookResponseDto
        {
            RentalId = rental.Id,
            DueDate = rental.DueDate
        });
    }
}

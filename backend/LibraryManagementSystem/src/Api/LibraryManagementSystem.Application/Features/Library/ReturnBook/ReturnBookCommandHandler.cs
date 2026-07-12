using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.ReturnBook;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.ReturnBook;

public sealed class ReturnBookCommandHandler(
    IRentalRepository rentalRepository,
    IUnitOfWork unitOfWork,
    IDomainEventDispatcher domainEventDispatcher
) : IRequestHandler<ReturnBookCommand, Result<ReturnBookResponseDto>>
{
    public async Task<Result<ReturnBookResponseDto>> Handle(ReturnBookCommand command, CancellationToken ct)
    {
        var rental = await rentalRepository.GetByIdAsync(command.Request.RentalId, ct);

        if (rental == null)
            return Result<ReturnBookResponseDto>.Failure("Rental not found.");

        if (rental.UserId != command.Request.UserId)
            return Result<ReturnBookResponseDto>.Failure("Unauthorized to return this book.");

        if (rental.ReturnDate != null)
            return Result<ReturnBookResponseDto>.Failure("Book is already returned.");

        rental.SetReturned();
        await unitOfWork.SaveChangesAsync(ct);
        await domainEventDispatcher.DispatchEventsAsync(ct);

        return Result<ReturnBookResponseDto>.Success(new ReturnBookResponseDto());
    }
}
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Utils;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.DeleteAccount;

public sealed class DeleteAccountCommandHandler(
    IUserRepository users,
    IRentalRepository rentals,
    IUnitOfWork uow
) : IRequestHandler<DeleteAccountCommand, Result>
{
    public async Task<Result> Handle(DeleteAccountCommand command, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(command.UserId, ct);

        if (user is null)
            return Result.Failure("User not found.");

        if (!PasswordHasher.VerifyPassword(command.Request.Password, user.PasswordHash))
            return Result.Failure("Password is incorrect.");

        var activeRentals = await rentals.GetActiveRentalsByUserAsync(command.UserId, ct);
        if (activeRentals.Any())
            return Result.Failure("Cannot delete account while you have active rentals. Please return your books first.");

        user.SetDeleted();

        await uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Application.Utils;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.ChangePassword;

public sealed class ChangePasswordCommandHandler(
    IUserRepository users,
    IUnitOfWork uow
) : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(ChangePasswordCommand command, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(command.UserId, ct);

        if (user is null)
            return Result.Failure("User not found.");

        if (!PasswordHasher.VerifyPassword(command.Request.OldPassword, user.PasswordHash))
            return Result.Failure("Old password is incorrect.");

        if (command.Request.NewPassword.Length < 6)
            return Result.Failure("Password needs to be at least 6 characters long.");

        if (command.Request.OldPassword == command.Request.NewPassword)
            return Result.Failure("New password and old password can not be the same.");

        var newHash = PasswordHasher.HashPassword(command.Request.NewPassword);
        user.SetPasswordHash(newHash);

        await uow.SaveChangesAsync(ct);

        return Result.Success();
    }
}

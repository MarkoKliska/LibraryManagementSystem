using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.SaveUserChanges;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.SaveChanges;

public sealed class SaveUserChangesCommandHandler(
    IUserRepository users,
    IUnitOfWork unitOfWork
) : IRequestHandler<SaveUserChangesCommand, Result<SaveUserChangesResponseDto>>
{
    public async Task<Result<SaveUserChangesResponseDto>> Handle(
        SaveUserChangesCommand command,
        CancellationToken ct)
    {
        var user = await users.GetByIdAsync(command.UserId, ct);
        if (user is null)
            return Result<SaveUserChangesResponseDto>.Failure("User not found");

        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.FirstName) ||
            string.IsNullOrWhiteSpace(request.LastName) ||
            string.IsNullOrWhiteSpace(request.Email))
        {
            return Result<SaveUserChangesResponseDto>.Failure("Invalid input");
        }

        bool hasChanges = false;

        if (!string.Equals(user.FirstName, request.FirstName, StringComparison.Ordinal))
        {
            user.SetFirstName(request.FirstName.Trim());
            hasChanges = true;
        }

        if (!string.Equals(user.LastName, request.LastName, StringComparison.Ordinal))
        {
            user.SetLastName(request.LastName.Trim());
            hasChanges = true;
        }

        var newEmail = request.Email.Trim();

        if (!string.Equals(user.Email, newEmail, StringComparison.Ordinal))
        {
            var existingWithEmail = await users.GetByEmailAsync(newEmail, ct);
            if (existingWithEmail is not null && existingWithEmail.Id != user.Id)
                return Result<SaveUserChangesResponseDto>.Failure("Email is already in use");

            user.SetEmail(newEmail);
            hasChanges = true;
        }

        if (!hasChanges)
            return Result<SaveUserChangesResponseDto>.Failure("No changes detected");

        await unitOfWork.SaveChangesAsync(ct);

        var response = new SaveUserChangesResponseDto
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        return Result<SaveUserChangesResponseDto>.Success(response);
    }
}
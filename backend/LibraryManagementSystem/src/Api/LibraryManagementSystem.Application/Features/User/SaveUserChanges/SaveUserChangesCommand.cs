using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.SaveUserChanges;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.SaveChanges;

public sealed record SaveUserChangesCommand(
    Guid UserId,
    SaveUserChangesRequestDto Request
) : IRequest<Result<SaveUserChangesResponseDto>>;

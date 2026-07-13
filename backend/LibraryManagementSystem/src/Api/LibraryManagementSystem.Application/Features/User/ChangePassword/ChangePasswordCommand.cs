using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.ChangePassword;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.ChangePassword;

public sealed record ChangePasswordCommand(Guid UserId, ChangePasswordRequestDto Request)
    : IRequest<Result>;

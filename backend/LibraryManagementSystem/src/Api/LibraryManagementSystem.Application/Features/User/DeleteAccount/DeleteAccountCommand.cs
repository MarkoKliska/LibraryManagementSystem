using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.DeleteAccount;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.DeleteAccount;

public sealed record DeleteAccountCommand(Guid UserId, DeleteAccountRequestDto Request)
    : IRequest<Result>;

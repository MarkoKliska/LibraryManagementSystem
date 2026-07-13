using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.CreateUser;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.CreateUser;

public sealed record CreateUserCommand(CreateUserRequestDto Request)
    : IRequest<Result<CreateUserResponseDto>>;

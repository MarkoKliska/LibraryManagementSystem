using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.Login;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.Login;

public sealed record LoginCommand(LoginRequestDto Request)
    : IRequest<Result<LoginResponseDto>>;

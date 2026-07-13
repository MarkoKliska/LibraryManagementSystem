using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetUser;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetUser;

public sealed record GetUserQuery(GetUserRequestDto Request)
    : IRequest<Result<GetUserResponseDto>>;

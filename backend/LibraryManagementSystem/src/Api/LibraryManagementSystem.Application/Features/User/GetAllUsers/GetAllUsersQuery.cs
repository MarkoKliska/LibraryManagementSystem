using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetAllUsers;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery(int Page = 1, int PageSize = 20)
    : IRequest<Result<PagedResult<GetAllUsersResponseDto>>>;

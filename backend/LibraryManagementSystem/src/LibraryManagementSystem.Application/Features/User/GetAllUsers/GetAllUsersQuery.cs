using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetAllUsers;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetAllUsers;

public sealed record GetAllUsersQuery 
    : IRequest<Result<IEnumerable<GetAllUsersResponseDto>>>;

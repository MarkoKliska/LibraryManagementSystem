using LibraryManagementSystem.Application.DTOs.Admin.GetUserDetails;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.GetUserDetails;

public sealed record GetUserDetailsQuery(Guid UserId) 
    : IRequest<Result<GetUserDetailsResponseDto>>;

using LibraryManagementSystem.Application.DTOs.Library.GetUserDetails;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetUserDetails;

public sealed record GetUserDetailsQuery(Guid UserId) 
    : IRequest<Result<GetUserDetailsResponseDto>>;

using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetUser;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetUser;

public sealed class GetUserQueryHandler(
    IUserRepository users
) : IRequestHandler<GetUserQuery, Result<GetUserResponseDto>>
{
    public async Task<Result<GetUserResponseDto>> Handle(GetUserQuery query, CancellationToken ct)
    {
        var user = await users.GetByIdAsync(query.Request.UserId, ct);

        if (user is null)
            return Result<GetUserResponseDto>.Failure("User not found");

        var dto = new GetUserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };

        return Result<GetUserResponseDto>.Success(dto);
    }
}

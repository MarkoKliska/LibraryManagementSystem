using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetAllUsers;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetAllUsers;

public sealed class GetAllUsersQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<GetAllUsersResponseDto>>>
{
    public async Task<Result<IEnumerable<GetAllUsersResponseDto>>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        var users = await userRepository.GetAllAsync(ct);
        var result = users
            .Where(u => !u.IsDeleted)
            .Select(u => new GetAllUsersResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .ToList();

        return Result<IEnumerable<GetAllUsersResponseDto>>.Success(result);
    }
}
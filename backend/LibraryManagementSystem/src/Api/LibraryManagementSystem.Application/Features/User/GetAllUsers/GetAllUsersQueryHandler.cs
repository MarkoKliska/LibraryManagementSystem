using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.User.GetAllUsers;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.User.GetAllUsers;

public sealed class GetAllUsersQueryHandler(
    IUserRepository userRepository
) : IRequestHandler<GetAllUsersQuery, Result<PagedResult<GetAllUsersResponseDto>>>
{
    public async Task<Result<PagedResult<GetAllUsersResponseDto>>> Handle(GetAllUsersQuery query, CancellationToken ct)
    {
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);

        var (users, totalCount) = await userRepository.GetAllPagedAsync(page, pageSize, ct);

        var items = users
            .Select(u => new GetAllUsersResponseDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email
            })
            .ToList();

        return Result<PagedResult<GetAllUsersResponseDto>>.Success(new PagedResult<GetAllUsersResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}
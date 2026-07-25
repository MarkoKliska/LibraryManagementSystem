using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAuthorsPaged;

public sealed class GetAuthorsPagedQueryHandler(
    IAuthorRepository authorRepository
) : IRequestHandler<GetAuthorsPagedQuery, Result<PagedResult<AddAuthorResponseDto>>>
{
    public async Task<Result<PagedResult<AddAuthorResponseDto>>> Handle(GetAuthorsPagedQuery query, CancellationToken ct)
    {
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);

        var (authors, totalCount) = await authorRepository.GetAllPagedAsync(page, pageSize, query.Search, ct);

        var items = authors.Select(a => new AddAuthorResponseDto
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName
        }).ToList();

        return Result<PagedResult<AddAuthorResponseDto>>.Success(new PagedResult<AddAuthorResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}
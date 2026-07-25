using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetGenresPaged;

public sealed class GetGenresPagedQueryHandler(
    IGenreRepository genreRepository
) : IRequestHandler<GetGenresPagedQuery, Result<PagedResult<AddGenreResponseDto>>>
{
    public async Task<Result<PagedResult<AddGenreResponseDto>>> Handle(GetGenresPagedQuery query, CancellationToken ct)
    {
        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 100);

        var (genres, totalCount) = await genreRepository.GetAllPagedAsync(page, pageSize, query.Search, ct);

        var items = genres.Select(g => new AddGenreResponseDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();

        return Result<PagedResult<AddGenreResponseDto>>.Success(new PagedResult<AddGenreResponseDto>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        });
    }
}
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetGenresPaged;

public sealed record GetGenresPagedQuery(int Page = 1, int PageSize = 20, string? Search = null)
    : IRequest<Result<PagedResult<AddGenreResponseDto>>>;
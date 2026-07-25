using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAuthorsPaged;

public sealed record GetAuthorsPagedQuery(int Page = 1, int PageSize = 20, string? Search = null)
    : IRequest<Result<PagedResult<AddAuthorResponseDto>>>;
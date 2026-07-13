using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.GetAllBooksForAdmin;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksForAdmin;

public sealed record GetAllBooksForAdminQuery(int Page = 1, int PageSize = 20)
    : IRequest<Result<PagedResult<BookListForAdminResponseDto>>>;
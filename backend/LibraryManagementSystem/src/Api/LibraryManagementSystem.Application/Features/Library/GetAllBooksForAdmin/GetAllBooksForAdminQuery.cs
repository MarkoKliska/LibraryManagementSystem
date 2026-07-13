using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.GetAllBooksForAdmin;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksForAdmin;

public sealed record GetAllBooksForAdminQuery
    : IRequest<Result<IEnumerable<BookListForAdminResponseDto>>>;
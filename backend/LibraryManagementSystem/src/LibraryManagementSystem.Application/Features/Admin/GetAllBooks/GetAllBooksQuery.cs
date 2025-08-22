using LibraryManagementSystem.Application.DTOs.Admin.GetBookList;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.GetAllBooksQuery;

public sealed record GetAllBooksQuery 
    : IRequest<Result<IEnumerable<BookListResponseDto>>>;
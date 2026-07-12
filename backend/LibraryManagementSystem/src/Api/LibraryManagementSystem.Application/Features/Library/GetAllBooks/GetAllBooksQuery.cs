using LibraryManagementSystem.Application.DTOs.Library.GetBookList;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllBooksQuery;

public sealed record GetAllBooksQuery 
    : IRequest<Result<IEnumerable<BookListResponseDto>>>;
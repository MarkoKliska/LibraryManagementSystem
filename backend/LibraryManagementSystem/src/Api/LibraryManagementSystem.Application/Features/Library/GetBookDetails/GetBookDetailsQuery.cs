using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetBookDetails;

public sealed record GetBookDetailsQuery(Guid BookId) 
    : IRequest<Result<AddBookResponseDto>>;

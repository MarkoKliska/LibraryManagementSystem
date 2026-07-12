using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.SearchBook;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.SearchBook;

public sealed record SearchBooksQuery(SearchBooksRequestDto Request)
    : IRequest<Result<IEnumerable<SearchBooksResponseDto>>>;

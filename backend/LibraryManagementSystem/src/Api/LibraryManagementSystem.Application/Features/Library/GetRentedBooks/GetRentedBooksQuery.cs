using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.GetRentedBooks;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetRentedBooks;

public sealed record GetRentedBooksQuery(GetRentedBooksRequestDto Request)
    : IRequest<Result<IEnumerable<GetRentedBookResponseDto>>>;
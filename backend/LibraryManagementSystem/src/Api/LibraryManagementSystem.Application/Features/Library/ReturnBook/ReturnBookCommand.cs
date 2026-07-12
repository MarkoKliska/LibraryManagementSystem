using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.ReturnBook;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.ReturnBook;

public sealed record ReturnBookCommand(ReturnBookRequestDto Request)
    : IRequest<Result<ReturnBookResponseDto>>;

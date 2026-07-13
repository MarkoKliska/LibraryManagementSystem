using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Library.UpdateBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.UpdateBook;

public sealed record UpdateBookCommand(UpdateBookRequestDto Request) 
    : IRequest<Result<AddBookResponseDto>>;
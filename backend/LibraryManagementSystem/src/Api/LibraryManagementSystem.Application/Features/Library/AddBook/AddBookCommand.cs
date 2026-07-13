using LibraryManagementSystem.Application.DTOs.Library.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.AddBook;

public sealed record AddBookCommand(AddBookRequestDto Request) 
    : IRequest<Result<AddBookResponseDto>>;

using LibraryManagementSystem.Application.DTOs.Admin.AddBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.AddBook;

public sealed record AddBookCommand(AddBookRequestDto Request) 
    : IRequest<Result<AddBookResponseDto>>;

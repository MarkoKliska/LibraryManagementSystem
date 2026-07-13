using LibraryManagementSystem.Application.DTOs.Library.DeleteBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteBook;

public sealed record DeleteBookCommand(DeleteBookRequestDto Request) 
    : IRequest<Result<string>>;
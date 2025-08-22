using LibraryManagementSystem.Application.DTOs.Admin.DeleteBook;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.DeleteBook;

public sealed record DeleteBookCommand(DeleteBookRequestDto Request) 
    : IRequest<Result<string>>;
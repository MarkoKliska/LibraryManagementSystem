using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.DeleteAuthor;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteAuthor;

public sealed record DeleteAuthorCommand(DeleteAuthorRequestDto Request) 
    : IRequest<Result<string>>;

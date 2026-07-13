using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.AddAuthor;

public sealed record AddAuthorCommand(
    AddAuthorRequestDto Request
) : IRequest<Result<AddAuthorResponseDto>>;

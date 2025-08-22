using LibraryManagementSystem.Application.DTOs.Admin.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.AddAuthor;

public sealed record AddAuthorCommand(
    AddAuthorRequestDto Request
) : IRequest<Result<AddAuthorResponseDto>>;

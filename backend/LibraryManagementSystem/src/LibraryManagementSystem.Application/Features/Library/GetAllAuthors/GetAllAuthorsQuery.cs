using LibraryManagementSystem.Application.DTOs.Library.AddAuthor;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllAuthors;

public sealed record GetAllAuthorsQuery 
    : IRequest<Result<IEnumerable<AddAuthorResponseDto>>>;

using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.DTOs.Library.DeleteGenre;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteGenre;

public sealed record DeleteGenreCommand(DeleteGenreRequestDto Request) 
    : IRequest<Result<string>>;

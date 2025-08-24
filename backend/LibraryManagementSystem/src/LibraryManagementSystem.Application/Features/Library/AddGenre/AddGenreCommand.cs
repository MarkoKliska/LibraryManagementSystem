using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.AddGenre;

public sealed record AddGenreCommand(AddGenreRequestDto Request) 
    : IRequest<Result<AddGenreResponseDto>>;

using LibraryManagementSystem.Application.DTOs.Admin.AddGenre;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Admin.AddGenre;

public sealed record AddGenreCommand(AddGenreRequestDto Request) 
    : IRequest<Result<AddGenreResponseDto>>;

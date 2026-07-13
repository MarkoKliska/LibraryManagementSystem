using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Application.DTOs.Common;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllGenres;

public sealed record GetAllGenresQuery 
    : IRequest<Result<IEnumerable<AddGenreResponseDto>>>;

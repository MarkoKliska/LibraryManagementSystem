using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.GetAllGenres;

public sealed class GetAllGenresQueryHandler(
    IGenreRepository genreRepository
) : IRequestHandler<GetAllGenresQuery, Result<IEnumerable<AddGenreResponseDto>>>
{
    public async Task<Result<IEnumerable<AddGenreResponseDto>>> Handle(GetAllGenresQuery query, CancellationToken ct)
    {
        var genres = await genreRepository.GetAllAsync(ct);

        var response = genres.Select(g => new AddGenreResponseDto
        {
            Id = g.Id,
            Name = g.Name
        }).ToList();

        return Result<IEnumerable<AddGenreResponseDto>>.Success(response);
    }
}

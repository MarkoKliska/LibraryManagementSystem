using LibraryManagementSystem.Application.DTOs.Library.AddGenre;
using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.AddGenre;

public sealed class AddGenreCommandHandler(
    IGenreRepository genreRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<AddGenreCommand, Result<AddGenreResponseDto>>
{
    public async Task<Result<AddGenreResponseDto>> Handle(AddGenreCommand command, CancellationToken ct)
    {
        var request = command.Request;

        if (string.IsNullOrWhiteSpace(request.Name))
            return Result<AddGenreResponseDto>.Failure("Genre name is required");

        var name = request.Name.Trim();

        if (await genreRepository.ExistsByNameAsync(name, ct))
            return Result<AddGenreResponseDto>.Failure($"Genre '{name}' already exists");


        var genre = new Genre(request.Name.Trim());
        await genreRepository.AddAsync(genre, ct);
        await unitOfWork.SaveChangesAsync(ct);

        var response = new AddGenreResponseDto
        {
            Id = genre.Id,
            Name = genre.Name
        };

        return Result<AddGenreResponseDto>.Success(response);
    }
}

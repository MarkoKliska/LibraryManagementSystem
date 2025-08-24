using LibraryManagementSystem.Application.DTOs.Common;
using LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.Domain.Repositories;
using MediatR;

namespace LibraryManagementSystem.Application.Features.Library.DeleteGenre;

public sealed class DeleteGenreCommandHandler(
    IGenreRepository genreRepository,
    IBookRepository bookRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<DeleteGenreCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteGenreCommand command, CancellationToken ct)
    {
        var genre = await genreRepository.GetByIdAsync(command.Request.GenreId, ct);
        if (genre == null || genre.IsDeleted)
            return Result<string>.Failure("Genre not found");

        var hasBooks = await bookRepository.GetAllAsync(ct)
            .ContinueWith(t => t.Result.Any(b => b.GenreId == command.Request.GenreId && !b.IsDeleted), ct);
        if (hasBooks)
            return Result<string>.Failure("Cannot delete genre because it has associated books");

        genre.SetDeleted();
        await unitOfWork.SaveChangesAsync(ct);

        return Result<string>.Success("Genre deleted successfully");
    }
}

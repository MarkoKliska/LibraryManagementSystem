namespace LibraryManagementSystem.Application.DTOs.Library.DeleteGenre;

public record DeleteGenreRequestDto
{
    public Guid GenreId { get; init; }
}

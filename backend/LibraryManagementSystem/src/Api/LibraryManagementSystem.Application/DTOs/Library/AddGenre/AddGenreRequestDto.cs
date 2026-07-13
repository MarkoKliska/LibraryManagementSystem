namespace LibraryManagementSystem.Application.DTOs.Library.AddGenre;

public record AddGenreRequestDto
{
    public string Name { get; init; } = default!;
}

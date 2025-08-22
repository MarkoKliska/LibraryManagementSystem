namespace LibraryManagementSystem.Application.DTOs.Admin.AddGenre;

public record AddGenreRequestDto
{
    public string Name { get; init; } = default!;
}

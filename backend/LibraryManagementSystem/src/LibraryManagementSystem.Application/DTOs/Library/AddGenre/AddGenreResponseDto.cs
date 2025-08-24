namespace LibraryManagementSystem.Application.DTOs.Library.AddGenre;

public record AddGenreResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
}

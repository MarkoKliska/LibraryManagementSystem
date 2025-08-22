namespace LibraryManagementSystem.Application.DTOs.Admin.AddGenre;

public record AddGenreResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = default!;
}

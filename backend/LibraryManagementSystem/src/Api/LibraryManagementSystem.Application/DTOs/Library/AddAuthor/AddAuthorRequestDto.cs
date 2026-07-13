namespace LibraryManagementSystem.Application.DTOs.Library.AddAuthor;

public record AddAuthorRequestDto
{
    public string? FirstName { get; init; }
    public string LastName { get; init; } = default!;
}

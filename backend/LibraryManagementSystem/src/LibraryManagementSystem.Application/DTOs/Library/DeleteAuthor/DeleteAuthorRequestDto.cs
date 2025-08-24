namespace LibraryManagementSystem.Application.DTOs.Library.DeleteAuthor;

public record DeleteAuthorRequestDto
{
    public Guid AuthorId { get; init; }
}

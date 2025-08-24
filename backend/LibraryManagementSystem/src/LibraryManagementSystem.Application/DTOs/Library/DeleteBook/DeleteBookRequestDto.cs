namespace LibraryManagementSystem.Application.DTOs.Library.DeleteBook;

public record DeleteBookRequestDto
{
    public Guid BookId { get; init; }
}
namespace LibraryManagementSystem.Application.DTOs.Admin.DeleteBook;

public record DeleteBookRequestDto
{
    public Guid BookId { get; init; }
}
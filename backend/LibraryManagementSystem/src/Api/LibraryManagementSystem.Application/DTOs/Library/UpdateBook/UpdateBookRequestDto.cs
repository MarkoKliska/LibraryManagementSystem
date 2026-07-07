namespace LibraryManagementSystem.Application.DTOs.Library.UpdateBook;

public record UpdateBookRequestDto
{
    public Guid BookId { get; init; }
    public string Title { get; init; } = default!;
    public Guid AuthorId { get; init; }
    public Guid GenreId { get; init; }
    public string Isbn13 { get; init; } = default!;
    public int NumberOfCopies { get; init; }
}

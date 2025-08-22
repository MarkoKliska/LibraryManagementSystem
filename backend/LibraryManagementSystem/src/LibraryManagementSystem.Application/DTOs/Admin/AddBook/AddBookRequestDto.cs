namespace LibraryManagementSystem.Application.DTOs.Admin.AddBook;

public record AddBookRequestDto
{
    public string Title { get; init; } = default!;
    public Guid AuthorId { get; init; }
    public Guid GenreId { get; init; }
    public string Isbn13 { get; init; } = default!;
    public int NumberOfCopies { get; init; }
}

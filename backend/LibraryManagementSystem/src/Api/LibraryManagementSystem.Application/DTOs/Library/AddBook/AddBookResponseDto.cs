namespace LibraryManagementSystem.Application.DTOs.Library.AddBook;

public record AddBookResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public Guid AuthorId { get; init; }
    public string AuthorName { get; init; } = default!;
    public Guid GenreId { get; init; }
    public string GenreName { get; init; } = default!;
    public string Isbn13 { get; init; } = default!;
    public int NumberOfCopies { get; init; }
}

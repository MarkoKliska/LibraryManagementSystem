namespace LibraryManagementSystem.Application.DTOs.Library.SearchBook;

public record SearchBooksResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string AuthorName { get; init; } = default!;
    public string GenreName { get; init; } = default!;
    public string Isbn13 { get; init; } = default!;
    public int TotalCopies { get; init; }
    public int AvailableCopies { get; init; }
}
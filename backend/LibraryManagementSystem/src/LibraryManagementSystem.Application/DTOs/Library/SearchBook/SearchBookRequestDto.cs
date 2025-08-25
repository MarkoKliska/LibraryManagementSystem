namespace LibraryManagementSystem.Application.DTOs.Library.SearchBook;

public record SearchBooksRequestDto
{
    public string? Title { get; init; }
    public string? AuthorName { get; init; }
    public string? GenreName { get; init; }
    public string? Isbn13 { get; init; }
}
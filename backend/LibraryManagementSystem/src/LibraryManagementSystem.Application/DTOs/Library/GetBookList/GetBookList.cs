namespace LibraryManagementSystem.Application.DTOs.Library.GetBookList;

public record BookListResponseDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string AuthorName { get; init; } = default!;
    public string GenreName { get; init; } = default!;
    public string Isbn13 { get; init; } = default!;
    public int TotalCopies { get; init; }
    public int AvailableCopies { get; init; }
    public Guid? RentedByUserId { get; init; }
    public string? RentedByUserEmail { get; init; }
}

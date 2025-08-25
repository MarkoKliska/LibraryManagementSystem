namespace LibraryManagementSystem.Application.DTOs.Library.GetRentedBooks;

public record GetRentedBookResponseDto
{
    public Guid RentalId { get; init; }
    public Guid BookId { get; init; }
    public string Title { get; init; } = default!;
    public string AuthorName { get; init; } = default!;
    public string GenreName { get; init; } = default!;
    public string Isbn13 { get; init; } = default!;
    public DateTime RentalDate { get; init; }
    public DateTime DueDate { get; init; }
    public Guid BookCopyId { get; init; }
}
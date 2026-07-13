namespace LibraryManagementSystem.Application.DTOs.Library.GetUserDetails;

public record RentalDetailsDto
{
    public Guid RentalId { get; init; }
    public Guid BookId { get; init; }
    public string BookTitle { get; init; } = default!;
    public DateTime RentalDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime? ReturnDate { get; init; }
}
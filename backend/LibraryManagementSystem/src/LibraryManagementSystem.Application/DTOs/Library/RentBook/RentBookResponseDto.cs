namespace LibraryManagementSystem.Application.DTOs.Library.RentBook;

public record RentBookResponseDto
{
    public Guid RentalId { get; init; }
    public DateTime DueDate { get; init; }
}

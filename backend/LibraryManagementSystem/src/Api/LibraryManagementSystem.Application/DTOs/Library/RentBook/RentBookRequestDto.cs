namespace LibraryManagementSystem.Application.DTOs.Library.RentBook;

public record RentBookRequestDto
{
    public Guid BookId { get; init; }
    public Guid UserId { get; init; }
}

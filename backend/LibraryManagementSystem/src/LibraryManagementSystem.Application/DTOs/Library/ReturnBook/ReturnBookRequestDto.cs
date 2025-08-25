namespace LibraryManagementSystem.Application.DTOs.Library.ReturnBook;

public record ReturnBookRequestDto
{
    public Guid RentalId { get; init; }
    public Guid UserId { get; init; }
}
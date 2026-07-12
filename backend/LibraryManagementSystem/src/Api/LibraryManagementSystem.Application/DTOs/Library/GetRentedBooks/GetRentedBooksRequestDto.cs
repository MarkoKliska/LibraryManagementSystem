namespace LibraryManagementSystem.Application.DTOs.Library.GetRentedBooks;

public record GetRentedBooksRequestDto
{
    public Guid UserId { get; init; }
}
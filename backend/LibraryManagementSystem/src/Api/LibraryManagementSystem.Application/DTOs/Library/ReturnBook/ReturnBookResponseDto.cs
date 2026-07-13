namespace LibraryManagementSystem.Application.DTOs.Library.ReturnBook;

public record ReturnBookResponseDto
{
    public string Message { get; init; } = "Book returned successfully.";
}

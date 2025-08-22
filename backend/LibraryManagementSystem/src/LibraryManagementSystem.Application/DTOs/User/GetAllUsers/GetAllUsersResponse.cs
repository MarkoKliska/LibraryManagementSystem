namespace LibraryManagementSystem.Application.DTOs.User.GetAllUsers;

public record GetAllUsersResponseDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
}
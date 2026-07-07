namespace LibraryManagementSystem.Application.DTOs.User.CreateUser;

public class CreateUserRequestDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Role { get; set; } = "User";
}

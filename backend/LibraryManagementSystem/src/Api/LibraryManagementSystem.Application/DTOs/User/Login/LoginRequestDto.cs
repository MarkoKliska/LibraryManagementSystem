namespace LibraryManagementSystem.Application.DTOs.User.Login;

public class LoginRequestDto
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}

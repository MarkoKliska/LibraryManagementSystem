namespace LibraryManagementSystem.Application.DTOs.User.CreateUser;
//moze da bude record
public class CreateUserResponseDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Role { get; set; } = default!;
    public string Token { get; set; } = default!;
}

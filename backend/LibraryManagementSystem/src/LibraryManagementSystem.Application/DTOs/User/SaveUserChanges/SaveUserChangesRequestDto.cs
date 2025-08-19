namespace LibraryManagementSystem.Application.DTOs.User.SaveUserChanges;

public class SaveUserChangesRequestDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
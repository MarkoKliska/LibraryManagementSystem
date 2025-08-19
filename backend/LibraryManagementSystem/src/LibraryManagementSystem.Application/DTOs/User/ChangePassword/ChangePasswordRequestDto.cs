namespace LibraryManagementSystem.Application.DTOs.User.ChangePassword;

public class ChangePasswordRequestDto
{
    public string OldPassword { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
}

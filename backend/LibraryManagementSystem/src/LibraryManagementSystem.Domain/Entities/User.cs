namespace LibraryManagementSystem.Domain.Entities;
public enum UserRole
{
    User = 0,
    Admin = 1
}
public class User
{
    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public string PasswordHash { get; private set; } = default!;

    public bool IsDeleted { get; private set; } = false;
    public UserRole Role { get; private set; } = UserRole.User;
    public User() { }
    public User(string firstName, string lastName, string email, string passwordHash, UserRole role = UserRole.User)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        IsDeleted = false;
    }

    public void SetRole(UserRole role)
    {
        Role = role;
    }
}

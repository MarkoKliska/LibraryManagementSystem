namespace LibraryManagementSystem.Domain.Entities;

public class Author
{
    public Guid Id { get; private set; }

    public string? FirstName { get; private set; }
    public string LastName { get; private set; } = default!;
    public bool IsDeleted { get; private set; } = false;
    public ICollection<Book> Books { get; private set; } = new List<Book>();
    public Author() { }
    public Author(string? firstName, string lastName)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        IsDeleted = false;
    }

    public void SetFirstName(string? firstName)
    {
        FirstName = firstName;
    }

    public void SetLastName(string lastName)
    {
        LastName = lastName;
    }

    public void SetDeleted()
    {
        IsDeleted = true;
    }
}

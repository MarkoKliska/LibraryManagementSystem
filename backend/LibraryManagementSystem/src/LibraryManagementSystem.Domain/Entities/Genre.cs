namespace LibraryManagementSystem.Domain.Entities;

public class Genre
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public bool IsDeleted { get; private set; } = false;

    public ICollection<Book> Books { get; private set; } = new List<Book>();

    public Genre() { }

    public Genre(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsDeleted = false;
    }

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetDeleted()
    {
        IsDeleted = true;
    }
}
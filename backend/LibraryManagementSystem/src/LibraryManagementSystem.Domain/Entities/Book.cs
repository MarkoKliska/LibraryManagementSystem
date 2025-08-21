namespace LibraryManagementSystem.Domain.Entities;

public class Book
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = default!;

    public Guid AuthorId { get; private set; }

    public Guid GenreId { get; private set; }

    public string Isbn13 { get; private set; } = default!;

    public bool IsDeleted { get; private set; } = false;

    public Author Author { get; private set; } = default!;

    public Genre Genre { get; private set; } = default!;

    public ICollection<BookCopy> Copies { get; private set; } = new List<BookCopy>();

    public Book() { }

    public Book(string title, Guid authorId, Guid genreId, string isbn13)
    {
        Id = Guid.NewGuid();
        Title = title;
        AuthorId = authorId;
        GenreId = genreId;
        Isbn13 = isbn13;
        IsDeleted = false;
    }

    public void SetTitle(string title)
    {
        Title = title;
    }

    public void SetAuthorId(Guid authorId)
    {
        AuthorId = authorId;
    }

    public void SetGenreId(Guid genreId)
    {
        GenreId = genreId;
    }

    public void SetIsbn13(string isbn13)
    {
        Isbn13 = isbn13;
    }

    public void SetDeleted()
    {
        IsDeleted = true;
    }
}

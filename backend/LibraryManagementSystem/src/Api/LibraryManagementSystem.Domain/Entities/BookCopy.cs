namespace LibraryManagementSystem.Domain.Entities;

public class BookCopy
{
    public Guid Id { get; private set; }

    public Guid BookId { get; private set; }

    public bool IsDeleted { get; private set; } = false;

    public Book Book { get; private set; } = default!;

    public ICollection<Rental> Rentals { get; private set; } = new List<Rental>();

    public BookCopy() { }

    public BookCopy(Guid bookId)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        IsDeleted = false;
    }

    public void SetBookId(Guid bookId)
    {
        BookId = bookId;
    }

    public void SetDeleted()
    {
        IsDeleted = true;
    }
}

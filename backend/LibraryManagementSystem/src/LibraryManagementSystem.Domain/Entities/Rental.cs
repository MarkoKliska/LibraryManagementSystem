namespace LibraryManagementSystem.Domain.Entities;

public class Rental
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public Guid BookCopyId { get; private set; }

    public DateTime RentalDate { get; private set; }

    public DateTime DueDate { get; private set; }

    public DateTime? ReturnDate { get; private set; }

    public User User { get; private set; } = default!;

    public BookCopy BookCopy { get; private set; } = default!;

    public Rental() { }

    public Rental(Guid userId, Guid bookCopyId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        BookCopyId = bookCopyId;
        RentalDate = DateTime.UtcNow;
        DueDate = RentalDate.AddDays(30);
        ReturnDate = null;
    }

    public void SetReturned()
    {
        if (ReturnDate != null)
            throw new InvalidOperationException("Book is already returned.");
        ReturnDate = DateTime.UtcNow;
    }
}

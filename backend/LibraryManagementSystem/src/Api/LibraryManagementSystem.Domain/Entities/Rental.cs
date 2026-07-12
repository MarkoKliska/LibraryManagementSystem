using LibraryManagementSystem.Domain.Common.BaseEntity;
using LibraryManagementSystem.Domain.Events;

namespace LibraryManagementSystem.Domain.Entities;

public class Rental : BaseEntity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid BookCopyId { get; private set; }
    public DateTime RentalDate { get; private set; }
    public DateTime DueDate { get; private set; }
    public DateTime? ReturnDate { get; private set; }
    public bool ReminderSent { get; private set; } = false;
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

        AddDomainEvent(new BookRentedDomainEvent(Id, UserId, BookCopyId, DueDate));
    }

    public void SetReturned()
    {
        if (ReturnDate != null)
            throw new InvalidOperationException("Book is already returned.");
        ReturnDate = DateTime.UtcNow;

        AddDomainEvent(new BookReturnedDomainEvent(Id, UserId, BookCopyId));
    }

    public void MarkReminderSent()
    {
        if (ReminderSent)
            throw new InvalidOperationException("Reminder has already been sent for this rental.");

        ReminderSent = true;
        AddDomainEvent(new RentalDueSoonDomainEvent(Id, UserId, BookCopyId, DueDate));
    }
}
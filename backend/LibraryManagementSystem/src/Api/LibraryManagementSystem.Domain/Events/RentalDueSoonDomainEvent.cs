using LibraryManagementSystem.Domain.Common.Interfaces;

namespace LibraryManagementSystem.Domain.Events;

public sealed record RentalDueSoonDomainEvent(
    Guid RentalId,
    Guid UserId,
    Guid BookCopyId,
    DateTime DueDate) : IDomainEvent;
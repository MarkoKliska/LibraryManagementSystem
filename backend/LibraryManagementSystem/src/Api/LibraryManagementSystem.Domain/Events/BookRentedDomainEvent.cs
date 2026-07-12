using LibraryManagementSystem.Domain.Common.Interfaces;

namespace LibraryManagementSystem.Domain.Events;

public sealed record BookRentedDomainEvent(
    Guid RentalId,
    Guid UserId,
    Guid BookCopyId,
    DateTime DueDate) : IDomainEvent;
using LibraryManagementSystem.Domain.Common.Interfaces;

namespace LibraryManagementSystem.Domain.Events;

public sealed record BookReturnedDomainEvent(
    Guid RentalId,
    Guid UserId,
    Guid BookCopyId) : IDomainEvent;
using LibraryManagementSystem.Domain.Common.Interfaces;

namespace LibraryManagementSystem.Domain.Events;

public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Email,
    string FirstName) : IDomainEvent;
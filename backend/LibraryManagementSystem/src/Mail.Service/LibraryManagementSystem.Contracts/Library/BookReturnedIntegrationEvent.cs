namespace LibraryManagementSystem.Contracts.Library;

public record BookReturnedIntegrationEvent(
    Guid UserId,
    string Email,
    string BookTitle) : IntegrationEvent;

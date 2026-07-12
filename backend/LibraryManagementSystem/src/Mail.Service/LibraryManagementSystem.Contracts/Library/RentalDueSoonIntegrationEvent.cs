namespace LibraryManagementSystem.Contracts.Library;

public record RentalDueSoonIntegrationEvent(
    Guid UserId,
    string Email,
    string BookTitle,
    DateTime DueDate) : IntegrationEvent;

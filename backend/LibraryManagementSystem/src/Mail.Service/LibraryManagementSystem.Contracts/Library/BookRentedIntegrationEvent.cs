namespace LibraryManagementSystem.Contracts.Library;

public record BookRentedIntegrationEvent(
    Guid UserId, 
    string Email,
    string BookTitle,
    DateTime DueDate
    ) : IntegrationEvent;

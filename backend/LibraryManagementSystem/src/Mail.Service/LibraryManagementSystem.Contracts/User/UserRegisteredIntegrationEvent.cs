namespace LibraryManagementSystem.Contracts.User;

public record UserRegisteredIntegrationEvent(
    Guid UserId,
    string Email, 
    string FirstName) : IntegrationEvent;

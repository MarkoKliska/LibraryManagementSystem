using LibraryManagementSystem.Contracts.User;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class UserRegisteredEmailTemplate : IEmailTemplate<UserRegisteredIntegrationEvent>
{
    public (string Subject, string Body) Render(UserRegisteredIntegrationEvent integrationEvent)
    {
        var subject = "Welcome to the Library Management System";
        var body = $"""
            <p>Hi {integrationEvent.FirstName},</p>
            <p>Your account has been created successfully. Welcome aboard!</p>
            """;
        return (subject, body);
    }
}
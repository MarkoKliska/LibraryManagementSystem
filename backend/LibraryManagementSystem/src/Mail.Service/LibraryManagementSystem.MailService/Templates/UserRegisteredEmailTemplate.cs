using System.Net;
using LibraryManagementSystem.Contracts.User;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class UserRegisteredEmailTemplate : IEmailTemplate<UserRegisteredIntegrationEvent>
{
    public (string Subject, string Body) Render(UserRegisteredIntegrationEvent integrationEvent)
    {
        var firstName = WebUtility.HtmlEncode(integrationEvent.FirstName);
        var subject = "Welcome to the Library Management System";
        var body = $"""
            <p>Hi {firstName},</p>
            <p>Your account has been created successfully. Welcome aboard!</p>
            """;
        return (subject, body);
    }
}
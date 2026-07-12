using System.Globalization;
using System.Net;
using LibraryManagementSystem.Contracts.Library;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class RentalDueSoonEmailTemplate : IEmailTemplate<RentalDueSoonIntegrationEvent>
{
    public (string Subject, string Body) Render(RentalDueSoonIntegrationEvent integrationEvent)
    {
        var bookTitle = WebUtility.HtmlEncode(integrationEvent.BookTitle);
        var dueDate = integrationEvent.DueDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        var subject = $"Reminder: \"{integrationEvent.BookTitle}\" is due soon";
        var body = $"""
            <p>Just a reminder that <strong>{bookTitle}</strong> is due on <strong>{dueDate}</strong>.</p>
            <p>Please return it on time to avoid any late fees.</p>
            """;
        return (subject, body);
    }
}
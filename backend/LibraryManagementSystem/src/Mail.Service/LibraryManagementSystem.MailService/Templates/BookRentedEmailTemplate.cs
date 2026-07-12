using System.Globalization;
using System.Net;
using LibraryManagementSystem.Contracts.Library;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class BookRentedEmailTemplate : IEmailTemplate<BookRentedIntegrationEvent>
{
    public (string Subject, string Body) Render(BookRentedIntegrationEvent integrationEvent)
    {
        var bookTitle = WebUtility.HtmlEncode(integrationEvent.BookTitle);
        var dueDate = integrationEvent.DueDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        var subject = $"You rented \"{integrationEvent.BookTitle}\"";
        var body = $"""
            <p>You have successfully rented <strong>{bookTitle}</strong>.</p>
            <p>Please return it by <strong>{dueDate}</strong>.</p>
            """;
        return (subject, body);
    }
}
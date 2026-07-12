using System.Net;
using LibraryManagementSystem.Contracts.Library;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class BookReturnedEmailTemplate : IEmailTemplate<BookReturnedIntegrationEvent>
{
    public (string Subject, string Body) Render(BookReturnedIntegrationEvent integrationEvent)
    {
        var bookTitle = WebUtility.HtmlEncode(integrationEvent.BookTitle);
        var subject = $"You returned \"{integrationEvent.BookTitle}\"";
        var body = $"<p>Thanks for returning <strong>{bookTitle}</strong>.</p>";
        return (subject, body);
    }
}
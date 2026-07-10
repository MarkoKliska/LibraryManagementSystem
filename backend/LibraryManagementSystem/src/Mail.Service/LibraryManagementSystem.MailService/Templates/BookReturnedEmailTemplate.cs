using LibraryManagementSystem.Contracts.Library;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class BookReturnedEmailTemplate : IEmailTemplate<BookReturnedIntegrationEvent>
{
    public (string Subject, string Body) Render(BookReturnedIntegrationEvent integrationEvent)
    {
        var subject = $"You returned \"{integrationEvent.BookTitle}\"";
        var body = $"<p>Thanks for returning <strong>{integrationEvent.BookTitle}</strong>.</p>";
        return (subject, body);
    }
}
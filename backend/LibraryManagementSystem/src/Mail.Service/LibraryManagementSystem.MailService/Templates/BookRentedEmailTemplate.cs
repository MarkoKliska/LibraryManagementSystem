using LibraryManagementSystem.Contracts.Library;

namespace LibraryManagementSystem.MailService.Templates;

public sealed class BookRentedEmailTemplate : IEmailTemplate<BookRentedIntegrationEvent>
{
    public (string Subject, string Body) Render(BookRentedIntegrationEvent integrationEvent)
    {
        var subject = $"You rented \"{integrationEvent.BookTitle}\"";
        var body = $"""
            <p>You have successfully rented <strong>{integrationEvent.BookTitle}</strong>.</p>
            <p>Please return it by <strong>{integrationEvent.DueDate:MMMM d, yyyy}</strong>.</p>
            """;
        return (subject, body);
    }
}

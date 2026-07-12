using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.MailService.Emailing;
using LibraryManagementSystem.MailService.Templates;
using MassTransit;

namespace LibraryManagementSystem.MailService.Consumers;

public sealed class BookReturnedIntegrationEventConsumer(
    IEmailTemplate<BookReturnedIntegrationEvent> template,
    IEmailSender emailSender
) : IConsumer<BookReturnedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookReturnedIntegrationEvent> context)
    {
        var (subject, body) = template.Render(context.Message);
        await emailSender.SendAsync(
            new EmailMessage(context.Message.Email, subject, body),
            context.CancellationToken);
    }
}
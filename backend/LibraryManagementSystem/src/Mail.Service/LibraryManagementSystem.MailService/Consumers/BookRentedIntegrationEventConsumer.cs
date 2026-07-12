using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.MailService.Emailing;
using LibraryManagementSystem.MailService.Templates;
using MassTransit;

namespace LibraryManagementSystem.MailService.Consumers;

public sealed class BookRentedIntegrationEventConsumer(
    IEmailTemplate<BookRentedIntegrationEvent> template,
    IEmailSender emailSender
) : IConsumer<BookRentedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<BookRentedIntegrationEvent> context)
    {
        var (subject, body) = template.Render(context.Message);
        await emailSender.SendAsync(
            new EmailMessage(context.Message.Email, subject, body),
            context.CancellationToken);
    }
}
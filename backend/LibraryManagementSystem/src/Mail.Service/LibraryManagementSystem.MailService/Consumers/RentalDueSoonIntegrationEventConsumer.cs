using LibraryManagementSystem.Contracts.Library;
using LibraryManagementSystem.MailService.Emailing;
using LibraryManagementSystem.MailService.Templates;
using MassTransit;

namespace LibraryManagementSystem.MailService.Consumers;

public sealed class RentalDueSoonIntegrationEventConsumer(
    IEmailTemplate<RentalDueSoonIntegrationEvent> template,
    IEmailSender emailSender
) : IConsumer<RentalDueSoonIntegrationEvent>
{
    public async Task Consume(ConsumeContext<RentalDueSoonIntegrationEvent> context)
    {
        var (subject, body) = template.Render(context.Message);
        await emailSender.SendAsync(
            new EmailMessage(context.Message.Email, subject, body),
            context.CancellationToken);
    }
}
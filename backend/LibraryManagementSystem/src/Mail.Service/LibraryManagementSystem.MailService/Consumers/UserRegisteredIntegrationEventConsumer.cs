using LibraryManagementSystem.Contracts.User;
using LibraryManagementSystem.MailService.Emailing;
using LibraryManagementSystem.MailService.Templates;
using MassTransit;

namespace LibraryManagementSystem.MailService.Consumers;

public sealed class UserRegisteredIntegrationEventConsumer(
    IEmailTemplate<UserRegisteredIntegrationEvent> template,
    IEmailSender emailSender
) : IConsumer<UserRegisteredIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        var (subject, body) = template.Render(context.Message);
        await emailSender.SendAsync(
            new EmailMessage(context.Message.Email, subject, body),
            context.CancellationToken);
    }
}
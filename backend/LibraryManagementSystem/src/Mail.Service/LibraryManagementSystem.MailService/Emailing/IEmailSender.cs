namespace LibraryManagementSystem.MailService.Emailing;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
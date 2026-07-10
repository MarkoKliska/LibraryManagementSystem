namespace LibraryManagementSystem.MailService.Templates;

public interface IEmailTemplate<TEvent>
{
    (string Subject, string Body) Render(TEvent integrationEvent);
}
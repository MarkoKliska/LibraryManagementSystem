namespace LibraryManagementSystem.MailService.Emailing;

public sealed record EmailMessage(string To, string Subject, string HtmlBody);
using System.Net;
using System.Net.Mail;

public interface IEmailService {
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}

public class EmailService : IEmailService {
    public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
        var fromEmail = "mirzeyev005orxan@gmail.com";
        var fromPassword = "ucmp ornn nkun ydzw"; 

        var message = new MailMessage();
        message.From = new MailAddress(fromEmail);
        message.Subject = subject;
        message.To.Add(new MailAddress(email));
        message.Body = htmlMessage;
        message.IsBodyHtml = true;

        using var smtpClient = new SmtpClient("smtp.gmail.com") {
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, fromPassword),
            EnableSsl = true,
        };
        await smtpClient.SendMailAsync(message);
    }
}
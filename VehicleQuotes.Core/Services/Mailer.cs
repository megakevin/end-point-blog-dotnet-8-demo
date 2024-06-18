using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using VehicleQuotes.Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace VehicleQuotes.Core.Services;

public class MailData
{
    public required string To { get; set; }
    public required string ToName { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}

public interface IMailer
{
    Task<bool> SendMailAsync(MailData mailData);
}

public class Mailer : IMailer
{
    private readonly MailSettings _mailSettings;
    private readonly string[] _mailCcAddresses;
    private readonly string[] _mailBccAddresses;

    public Mailer(IOptions<MailSettings> mailSettingsOptions, IConfiguration config)
    {
        _mailSettings = mailSettingsOptions.Value;
        _mailCcAddresses = SplitAddresses(config["MailCcAddresses"]);
        _mailBccAddresses = SplitAddresses(config["MailBccAddresses"]);
    }

    private static string[] SplitAddresses(string? adresses) =>
        adresses?.Split(";", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? [];

    public async Task<bool> SendMailAsync(MailData mailData)
    {
        try
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(mailData.ToName, mailData.To));

            emailMessage.Cc.AddRange(_mailCcAddresses.Select(a => new MailboxAddress(a, a)));
            emailMessage.Bcc.AddRange(_mailBccAddresses.Select(a => new MailboxAddress(a, a)));

            emailMessage.Subject = mailData.Subject;

            var emailBodyBuilder = new BodyBuilder
            {
                TextBody = mailData.Body,
                HtmlBody = mailData.Body
            };

            emailMessage.Body = emailBodyBuilder.ToMessageBody();

            using var mailClient = new SmtpClient();

            await mailClient.ConnectAsync(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await mailClient.AuthenticateAsync(_mailSettings.UserName, _mailSettings.Password);
            await mailClient.SendAsync(emailMessage);
            await mailClient.DisconnectAsync(true);

            return true;
        }
        catch
        {
            // TODO: log the email delivery failure
            return false;
        }
    }
}

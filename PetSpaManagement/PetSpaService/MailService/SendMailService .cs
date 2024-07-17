using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PetSpaBussinessObject;
using PetSpaService.MailService;

public class SendMailService : ISendMailService
{
    private readonly MailSettings _mailSettings;
    public SendMailService(IOptions<MailSettings> mailSettingsOptions)
    {
        _mailSettings = mailSettingsOptions.Value;
    }
    public bool SendMail(MailData mailData)
    {
        using (MimeMessage emailMessage = new MimeMessage())
        {
            MailboxAddress emailFrom = new MailboxAddress(_mailSettings.SenderName, _mailSettings.SenderEmail);
            emailMessage.From.Add(emailFrom);
            MailboxAddress emailTo = new MailboxAddress(mailData.ReceiverName, mailData.ReceiverEmail);
            emailMessage.To.Add(emailTo);
            emailMessage.Subject = mailData.Title;
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.TextBody = mailData.Body;
            emailMessage.Body = emailBodyBuilder.ToMessageBody();
            using (SmtpClient mailClient = new SmtpClient())
            {
                mailClient.Connect(_mailSettings.Server, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                mailClient.Authenticate(_mailSettings.SenderEmail, _mailSettings.Password);
                mailClient.Send(emailMessage);
                mailClient.Disconnect(true);
            }
        }
        return true;
    }
}

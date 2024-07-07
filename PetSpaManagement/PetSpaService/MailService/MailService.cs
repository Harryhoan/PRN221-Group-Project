using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using MailKit.Security;
using YourNamespaceWhereEmailSettingsIsDefined;



namespace PetSpaService.MailService
{
	public class MailService
	{
		private readonly EmailSettings _emailSettings;

		public MailService(IOptions<EmailSettings> emailSettings)
		{
			_emailSettings = emailSettings.Value;
		}


		public async Task SendEmailAsync(string email, string subject, string htmlMessage, string textMessage)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
			message.To.Add(new MailboxAddress("", email));
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder();
			bodyBuilder.HtmlBody = htmlMessage;
			bodyBuilder.TextBody = textMessage;

			message.Body = bodyBuilder.ToMessageBody();

			using (var client = new SmtpClient())
			{
				await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls);
				await client.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);
				await client.SendAsync(message);
				await client.DisconnectAsync(true);
			}
		}
	}
}

using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Todo.Users.Core.Options;
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;

        public EmailSender(IOptions<EmailOptions> emailOptions) => this.emailOptions = emailOptions.Value;

        public async Task SendActivationUserMessageAsync(string to, Guid userId)
        {
            MimeEntity text = new TextPart("http://localhost:5001/api/1.0/users/activate/" + userId)
            { Text = "http://localhost:5001/api/1.0/users/activate/" + userId };
            MimeMessage message = new MimeMessage(emailOptions.From, to, "Activate accaunt", text);

            using var client = new SmtpClient();

            await client.ConnectAsync(emailOptions.Host, emailOptions.Port);

            await client.AuthenticateAsync(emailOptions.From, emailOptions.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }

        public Task SendMessageAsync(string to, string subject, string text = "", string html = "")
        {
            throw new NotImplementedException();
        }
    }
}

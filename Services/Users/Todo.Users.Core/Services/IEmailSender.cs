using System;
using System.Threading.Tasks;

namespace Todo.Users.Core.Services
{
    public interface IEmailSender
    {
        Task SendMessageAsync(string to, string subject, string text = "", string html = "");

        Task SendActivationUserMessageAsync(string to, Guid userId);
    }
}

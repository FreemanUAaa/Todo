using MediatR;

namespace Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommand : IRequest
    {
        public string Email { get; set; }
    }
}

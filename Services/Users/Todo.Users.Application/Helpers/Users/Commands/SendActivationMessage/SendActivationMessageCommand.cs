using MediatR;

namespace Todo.Users.Application.Helpers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommand : IRequest
    {
        public string Email { get; set; }
    }
}

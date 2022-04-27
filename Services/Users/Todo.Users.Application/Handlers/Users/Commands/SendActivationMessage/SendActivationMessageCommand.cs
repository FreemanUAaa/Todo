using MediatR;
using System;

namespace Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

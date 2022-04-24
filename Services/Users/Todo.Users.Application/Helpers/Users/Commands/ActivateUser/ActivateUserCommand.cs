using MediatR;
using System;

namespace Todo.Users.Application.Helpers.Users.Commands.ActivateUser
{
    public class ActivateUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

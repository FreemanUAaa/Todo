using MediatR;
using System;

namespace Todo.Users.Application.Helpers.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

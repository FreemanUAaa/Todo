using MediatR;
using System;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.DeleteUserCover
{
    public class DeleteUserCoverCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using System;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.UpdateUserCover
{
    public class UpdateUserCoverCommand : IRequest
    {
        public Guid UserId { get; set; }

        public IFormFile File { get; set; }
    }
}

using MediatR;
using System;

namespace Todo.Users.Application.Handlers.UserCovers.Queries.GetCoverBytes
{
    public class GetCoverBytesQuery : IRequest<byte[]>
    {
        public Guid UserId { get; set; }
    }
}

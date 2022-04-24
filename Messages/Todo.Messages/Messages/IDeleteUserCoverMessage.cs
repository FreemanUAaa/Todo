using System;

namespace Todo.Messages.Messages
{
    public interface IDeleteUserCoverMessage
    {
        public Guid UserId { get; set; }
    }
}

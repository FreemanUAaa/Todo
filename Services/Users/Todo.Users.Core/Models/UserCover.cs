using System;

namespace Todo.Users.Core.Models
{
    public class UserCover
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string FileName { get; set; }
    }
}

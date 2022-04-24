using System;
using System.Threading.Tasks;

namespace Todo.Users.Producers.Interfaces.Producers
{
    public interface IUserCoverProducer
    {
        Task DeleteUserCoverAsync(Guid userId);
    }
}

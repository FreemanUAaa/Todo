using System.Threading.Tasks;

namespace Todo.Users.Producers.Interfaces.Producers
{
    public interface ICacheProducer
    {
        Task DeleteCacheAsync(string key);
    }
}

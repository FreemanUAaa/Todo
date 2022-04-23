using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Models;

namespace Todo.Users.Core.Database
{
    public interface IDatabaseContext
    {
        DbSet<User> Users { get; set; }

        DbSet<UserCover> UserCovers { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

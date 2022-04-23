using Microsoft.EntityFrameworkCore;
using Todo.Users.Core.Database;
using Todo.Users.Core.Models;

namespace Todo.Users.Database
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<UserCover> UserCovers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}

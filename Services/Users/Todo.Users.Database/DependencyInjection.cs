using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Todo.Users.Core.Database;

namespace Todo.Users.Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));

            services.AddTransient<IDatabaseContext, DatabaseContext>();

            return services;
        }
    }
}

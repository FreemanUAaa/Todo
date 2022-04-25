using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Todo.Users.Producers.Interfaces.Producers;
using Todo.Users.Producers.Producers;

namespace Todo.Users.Producers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProducers(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq();
            });

            services.AddTransient<IUserCoverProducer, UserCoverProducer>();
            services.AddTransient<ICacheProducer, CacheProducer>();

            return services;
        }
    }
}

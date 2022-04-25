using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Todo.Users.Application.Behaviors;
using Todo.Users.Application.Services;
using Todo.Users.Core.Options;
using Todo.Users.Core.Services;

namespace Todo.Users.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            string redisConnection = configuration.GetConnectionString("Redis");

            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisConnection;
                opt.InstanceName = "Cache";
            });

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IFileManager, FileManager>();

            AuthOptions authOptions = configuration.GetSection("Auth").Get<AuthOptions>();

            services.Configure<AuthOptions>(configuration.GetSection("Auth"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.RequireHttpsMetadata = false;
                           options.TokenValidationParameters = new TokenValidationParameters
                           {
                               ValidateIssuer = true,
                               ValidateAudience = true,
                               ValidateLifetime = true,
                               ValidateIssuerSigningKey = true,
                               ValidIssuer = authOptions.Issuer,
                               ValidAudience = authOptions.Audience,
                               IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                           };
                       });

            return services;
        }
    }
}

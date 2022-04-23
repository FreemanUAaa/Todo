using Microsoft.AspNetCore.Builder;
using Todo.Users.Application.Middlewares;

namespace Todo.Users.Application
{
    public static class Middleware
    {
        public static IApplicationBuilder AddApplicationMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}

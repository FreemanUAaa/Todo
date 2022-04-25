using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Cache;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Models;
using Todo.Users.Producers.Interfaces.Producers;

namespace Todo.Users.Application.Handlers.Users.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand>
    {
        private readonly ILogger<ActivateUserCommandHandler> logger;

        private readonly ICacheProducer cacheProducer;

        private readonly IDatabaseContext database;

        public ActivateUserCommandHandler(IDatabaseContext database, ICacheProducer cacheProducer, ILogger<ActivateUserCommandHandler> logger) =>
            (this.database, this.cacheProducer, this.logger) = (database, cacheProducer, logger);

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            user.IsActivated = true;

            await cacheProducer.DeleteCacheAsync(CacheContracts.GetUserDetailsKey(user.Id));

            database.Users.Update(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User activated successfully");

            return Unit.Value;
        }
    }
}

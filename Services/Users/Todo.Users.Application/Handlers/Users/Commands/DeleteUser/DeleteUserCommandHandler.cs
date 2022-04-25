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

namespace Todo.Users.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly ILogger<DeleteUserCommandHandler> logger;

        private readonly IUserCoverProducer userCoverProducer;

        private readonly ICacheProducer cacheProducer;

        private readonly IDatabaseContext database;

        public DeleteUserCommandHandler(IDatabaseContext database, IUserCoverProducer userCoverProducer, ICacheProducer cacheProducer, ILogger<DeleteUserCommandHandler> logger) =>
            (this.database, this.userCoverProducer, this.cacheProducer, this.logger) = (database, userCoverProducer, cacheProducer, logger);

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            await userCoverProducer.DeleteUserCoverAsync(user.Id);
            await cacheProducer.DeleteCacheAsync(CacheContracts.GetUserDetailsKey(user.Id));

            database.Users.Remove(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User removed successfully");

            return Unit.Value;
        }
    }
}

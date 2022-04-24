using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Models;

namespace Todo.Users.Application.Helpers.Users.Commands.ActivateUser
{
    public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand>
    {
        private readonly ILogger<ActivateUserCommandHandler> logger;

        private readonly IDatabaseContext database;

        public ActivateUserCommandHandler(IDatabaseContext database, ILogger<ActivateUserCommandHandler> logger) =>
            (this.database, this.logger) = (database, logger);

        public async Task<Unit> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FindAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            user.IsActivated = true;

            database.Users.Update(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User activated successfully");

            return Unit.Value;
        }
    }
}

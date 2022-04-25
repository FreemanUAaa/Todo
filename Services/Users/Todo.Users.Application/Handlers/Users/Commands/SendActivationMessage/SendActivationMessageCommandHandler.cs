using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Models;
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage
{
    public class SendActivationMessageCommandHandler : IRequestHandler<SendActivationMessageCommand>
    {
        private readonly ILogger<SendActivationMessageCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IEmailSender email;

        public SendActivationMessageCommandHandler(IDatabaseContext database, IEmailSender email, ILogger<SendActivationMessageCommandHandler> logger) =>
            (this.database, this.email, this.logger) = (database, email, logger);

        public async Task<Unit> Handle(SendActivationMessageCommand request, CancellationToken cancellationToken)
        {
            User user = await database.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            await email.SendActivationUserMessageAsync(user.Email, user.Id);

            logger.LogInformation("Message sent successfully");

            return Unit.Value;
        }
    }
}

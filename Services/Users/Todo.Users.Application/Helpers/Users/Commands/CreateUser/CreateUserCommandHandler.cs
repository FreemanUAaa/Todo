using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Helpers.Hasher;
using Todo.Users.Core.Helpers.Salts;
using Todo.Users.Core.Models;
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Helpers.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly ILogger<CreateUserCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IEmailSender email;

        public CreateUserCommandHandler(IDatabaseContext database, IEmailSender email, ILogger<CreateUserCommandHandler> logger) =>
            (this.database, this.email, this.logger) = (database, email, logger);

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (database.Users.Any(x => x.Email == request.Email))
            {
                logger.LogInformation("Email is already used");
                throw new Exception(ExceptionStrings.EmailIsAlreadyUsed);
            }

            byte[] salt = SaltGenerator.Generate();
            string hash = PasswordHasher.Hash(request.Password, salt);

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserName = request.Email,
                Email = request.Email,
                IsActivated = false,
                PasswordHash = hash,
                PasswordSalt = salt,
            };

            await email.SendActivationUserMessageAsync(user.Email, user.Id);

            database.Users.Add(user);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("User successfully created");

            return user.Id;
        }
    }
}

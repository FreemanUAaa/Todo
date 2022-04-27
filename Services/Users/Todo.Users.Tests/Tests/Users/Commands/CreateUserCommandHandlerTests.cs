using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Commands.CreateUser;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Commands
{
    public class CreateUserCommandHandlerTests : BaseCommandTests<CreateUserCommandHandler>
    {
        [Fact]
        public async void CreateUserCommandHandlerSuccess()
        {
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Database, EmailSender, Logger);
            CreateUserCommand command = new CreateUserCommand()
            {
                Email = "test-email",
                Name = "test-name",
                Password = "test-password",
            };



            Guid userId = await handler.Handle(command, CancellationToken.None);


            Assert.NotNull(await Database.Users.FindAsync(userId));
        }

        [Fact]
        public async void CreateUserCommandHandlerFailOnWrongEmail()
        {
            User user = await CreateAndSaveUserAsync();
            CreateUserCommandHandler handler = new CreateUserCommandHandler(Database, EmailSender, Logger);
            CreateUserCommand command = new CreateUserCommand() { Email = user.Email, };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<User> CreateAndSaveUserAsync()
        {
            User user = new User()
            {
                Email = "test-email",
                Name = "test-name",
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Commands.ActivateUser;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Commands
{
    public class ActivateUserCommandHandlerTests : BaseCommandTests<ActivateUserCommandHandler>
    {
        [Fact]
        public async void ActivateUserCommandHandlerSuccess()
        {
            User user = await CreateAndSaveUserAsync();
            ActivateUserCommandHandler handler = new ActivateUserCommandHandler(Database, CacheProducer, Logger);
            ActivateUserCommand command = new ActivateUserCommand() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);
        }

        [Fact]
        public async void ActivateUserCommandHandlerFailOnWrongUserId()
        {
            ActivateUserCommandHandler handler = new ActivateUserCommandHandler(Database, CacheProducer, Logger);
            ActivateUserCommand command = new ActivateUserCommand();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<User> CreateAndSaveUserAsync()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "test-email",
                Name = "test-name",
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}

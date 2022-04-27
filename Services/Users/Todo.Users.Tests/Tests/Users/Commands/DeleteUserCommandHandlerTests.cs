using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Commands.DeleteUser;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Commands
{
    public class DeleteUserCommandHandlerTests : BaseCommandTests<DeleteUserCommandHandler>
    {
        [Fact]
        public async void DeleteUserCommandHandlerSuccess()
        {
            User user = await CreateAndSaveUserAsync();
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(Database, UserCoverProducer, CacheProducer, Logger);
            DeleteUserCommand command = new DeleteUserCommand() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.Users.FindAsync(user.Id));
        }

        [Fact]
        public async void DeleteUserCommandFailOnWrongId()
        {
            DeleteUserCommandHandler handler = new DeleteUserCommandHandler(Database, UserCoverProducer, CacheProducer, Logger);
            DeleteUserCommand command = new DeleteUserCommand();


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

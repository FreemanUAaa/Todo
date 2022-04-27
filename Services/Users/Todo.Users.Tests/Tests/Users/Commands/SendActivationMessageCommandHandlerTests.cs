using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Commands.SendActivationMessage;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Commands
{
    public class SendActivationMessageCommandHandlerTests : BaseCommandTests<SendActivationMessageCommandHandler>
    {
        [Fact]
        public async void SendActivationMessageCommandHandlerSuccess()
        {
            User user = await CreateAndSaveUserAsync();
            SendActivationMessageCommandHandler handler = new SendActivationMessageCommandHandler(Database, EmailSender, Logger);
            SendActivationMessageCommand command = new SendActivationMessageCommand() { UserId = user.Id };


            await handler.Handle(command, CancellationToken.None);
        }

        [Fact]
        public async void SendActivationMessageCommandHandlerFailOnWrongUserId()
        {
            SendActivationMessageCommandHandler handler = new SendActivationMessageCommandHandler(Database, EmailSender, Logger);
            SendActivationMessageCommand command = new SendActivationMessageCommand();


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

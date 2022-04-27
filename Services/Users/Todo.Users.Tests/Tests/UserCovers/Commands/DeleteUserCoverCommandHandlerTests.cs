using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.UserCovers.Commands.DeleteUserCover;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Assets;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.UserCovers.Commands
{
    public class DeleteUserCoverCommandHandlerTests : BaseCommandTests<DeleteUserCoverCommandHandler>
    {
        [Fact]
        public async void DeleteUserCoverCommandHandlerSuccess()
        {
            UserCover cover = await CreateAndSaveUserCoverAsync();
            DeleteUserCoverCommandHandler handler = new DeleteUserCoverCommandHandler(Database, FileManager, Paths, Logger);
            DeleteUserCoverCommand command = new DeleteUserCoverCommand() { UserId = cover.UserId };


            await handler.Handle(command, CancellationToken.None);


            Assert.Null(await Database.UserCovers.FirstOrDefaultAsync(x => x.UserId == cover.UserId));

            string filePath = Path.Combine(Paths.Value.UserCoverPath, cover.FileName);

            Assert.False(File.Exists(filePath));
        }

        [Fact]
        public async void DeleteUserCoverCommandHandlerFailOnWrongId()
        {
            DeleteUserCoverCommandHandler handler = new DeleteUserCoverCommandHandler(Database, FileManager, Paths, Logger);
            DeleteUserCoverCommand command = new DeleteUserCoverCommand();

            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(command, CancellationToken.None));
        }

        private async Task<UserCover> CreateAndSaveUserCoverAsync()
        {
            string fileName = "test-file.jpg";

            string filePath = Path.Combine(Paths.Value.UserCoverPath, fileName);

            await File.WriteAllBytesAsync(filePath, Photo.Bytes);

            UserCover cover = new UserCover()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                FileName = fileName,
            };

            Database.UserCovers.Add(cover);
            await Database.SaveChangesAsync();

            return cover;
        }
    }
}

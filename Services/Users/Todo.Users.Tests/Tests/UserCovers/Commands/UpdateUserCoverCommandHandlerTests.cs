using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading;
using Todo.Users.Application.Handlers.UserCovers.Commands.UpdateUserCover;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Assets;
using Todo.Users.Tests.Helpers;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.UserCovers.Commands
{
    public class UpdateUserCoverCommandHandlerTests : BaseCommandTests<UpdateUserCoverCommandHandler>
    {
        [Fact]
        public async void UpdateUserCoverCommandHandlerSuccess()
        {
            IFormFile file = FormFileFactory.Create(Photo.Bytes, "test.jpg");

            Guid userId = Guid.NewGuid();

            UpdateUserCoverCommandHandler handler = new UpdateUserCoverCommandHandler(Database, FileManager, Paths, Logger);
            UpdateUserCoverCommand command = new UpdateUserCoverCommand() { UserId = userId, File = file, };


            await handler.Handle(command, CancellationToken.None);


            UserCover cover = await Database.UserCovers.FirstOrDefaultAsync(x => x.UserId == userId);

            Assert.NotNull(cover);
            
            string newFilePath = Path.Combine(Paths.Value.UserCoverPath, cover.FileName);

            Assert.True(File.Exists(newFilePath));

            byte[] bytes = await File.ReadAllBytesAsync(newFilePath);

            Assert.Equal(bytes, Photo.Bytes);
        }

        [Fact]
        public async void UpdateUserCoverCommandHandlerFailOnWrongExtension()
        {
            IFormFile file = FormFileFactory.Create(Photo.Bytes, "test.wrong");

            Guid userId = Guid.NewGuid();

            UpdateUserCoverCommandHandler handler = new UpdateUserCoverCommandHandler(Database, FileManager, Paths, Logger);
            UpdateUserCoverCommand command = new UpdateUserCoverCommand() { UserId = userId, File = file, };


            await Assert.ThrowsAsync<Exception>(async () =>
                 await handler.Handle(command, CancellationToken.None));
        }
    }
}

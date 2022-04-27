using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.UserCovers.Queries.GetCoverBytes;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Assets;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.UserCovers.Queries
{
    public class GetCoverBytesQueryHandlerTests : BaseQueryTests<GetCoverBytesQueryHandler>
    {
        [Fact]
        public async void GetCoverBytesQueryHandlerSuccess()
        {
            UserCover cover = await CreateAndSaveUserCoverAsync();
            GetCoverBytesQueryHandler handler = new GetCoverBytesQueryHandler(Database, Paths, Logger);
            GetCoverBytesQuery query = new GetCoverBytesQuery() { UserId = cover.UserId };


            byte[] coverBytes = await handler.Handle(query, CancellationToken.None);


            Assert.Equal(coverBytes, Photo.Bytes);
        }

        [Fact]
        public async void GetCoverBytesQueryHandlerFailOnWrongId()
        {
            GetCoverBytesQueryHandler handler = new GetCoverBytesQueryHandler(Database, Paths, Logger);
            GetCoverBytesQuery query = new GetCoverBytesQuery();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
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

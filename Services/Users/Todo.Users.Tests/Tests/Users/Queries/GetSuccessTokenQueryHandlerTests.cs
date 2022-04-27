using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Queries.GetSuccessToken;
using Todo.Users.Core.Helpers.Hasher;
using Todo.Users.Core.Helpers.Salts;
using Todo.Users.Core.Models;
using Todo.Users.Core.Options;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Queries
{
    public class GetSuccessTokenQueryHandlerTests : BaseQueryTests<GetSuccessTokenQueryHandler>
    {
        private readonly IOptions<AuthOptions> auth;

        public GetSuccessTokenQueryHandlerTests() =>
            auth = Options.Create(new AuthOptions("issue", "audionce", "mysupersecret_secretkey!123", 120));

        [Fact]
        public async void GetSuccessTokenQueryHandlerSuccess()
        {
            User user = await CreateAndSaveUserAsync();
            GetSuccessTokenQueryHandler handler = new GetSuccessTokenQueryHandler(Database, auth, Logger);
            GetSuccessTokenQuery query = new GetSuccessTokenQuery()
            {
                Email = user.Email,
                Password = "test-password",
            };


            string token = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(token);
        }

        [Fact]
        public async void GetSuccessTokenQueryHandlerFailOnWrongAuthData()
        {
            GetSuccessTokenQueryHandler handler = new GetSuccessTokenQueryHandler(Database, auth, Logger);
            GetSuccessTokenQuery query = new GetSuccessTokenQuery()
            {
                Email = "wrong-email",
                Password = "wrong-password",
            };


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
        }

        private async Task<User> CreateAndSaveUserAsync()
        {
            byte[] salt = SaltGenerator.Generate();
            string hash = PasswordHasher.Hash("test-password", salt);

            User user = new User()
            {
                Id = Guid.NewGuid(),
                Email = "test-email",
                Name = "test-name",
                PasswordHash = hash,
                PasswordSalt = salt,
            };

            Database.Users.Add(user);
            await Database.SaveChangesAsync();

            return user;
        }
    }
}

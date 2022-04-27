using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Application.Handlers.Users.Queries.GetUserDetails;
using Todo.Users.Core.Models;
using Todo.Users.Tests.Tests.Base;
using Xunit;

namespace Todo.Users.Tests.Tests.Users.Queries
{
    public class GetUserDetailsQueryHandlerTests : BaseQueryTests<GetUserDetailsQueryHandler>
    {
        [Fact]
        public async void GetUserDetailsQueryHandlerSuccess()
        {
            User user = await CreateAndSaveUserAsync();
            GetUserDetailsQueryHandler handler = new GetUserDetailsQueryHandler(Database, Mapper, Logger);
            GetUserDetailsQuery query = new GetUserDetailsQuery() { UserId = user.Id };


            GetUserDetailsVm vm = await handler.Handle(query, CancellationToken.None);


            Assert.NotNull(vm);
        }

        [Fact]
        public async void GetUserDetailsQueryHandlerFailOnWrongId()
        {
            GetUserDetailsQueryHandler handler = new GetUserDetailsQueryHandler(Database, Mapper, Logger);
            GetUserDetailsQuery query = new GetUserDetailsQuery();


            await Assert.ThrowsAsync<Exception>(async () =>
                await handler.Handle(query, CancellationToken.None));
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

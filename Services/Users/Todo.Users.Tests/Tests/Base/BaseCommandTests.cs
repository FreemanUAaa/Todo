using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Todo.Users.Application.Services;
using Todo.Users.Core.Database;
using Todo.Users.Core.Options;
using Todo.Users.Core.Services;
using Todo.Users.Producers.Interfaces.Producers;
using Todo.Users.Tests.Database;

namespace Todo.Users.Tests.Tests.Base
{
    public abstract class BaseCommandTests<TLogger> where TLogger : class
    {
        public readonly IDatabaseContext Database;

        public readonly IFileManager FileManager;

        public readonly ILogger<TLogger> Logger;

        public readonly ICacheProducer CacheProducer;

        public readonly IUserCoverProducer UserCoverProducer;

        public readonly IOptions<FilePathsOptions> Paths;

        public readonly IEmailSender EmailSender;

        public BaseCommandTests()
        {
            Database = DatabaseContextFactory.Create();

            FileManager = new FileManager();

            Logger = new Mock<ILogger<TLogger>>().Object;

            EmailSender = new Mock<IEmailSender>().Object;

            CacheProducer = new Mock<ICacheProducer>().Object;

            UserCoverProducer = new Mock<IUserCoverProducer>().Object;

            Paths = Options.Create(new FilePathsOptions(@"D:\sharp\Todo\Services\Users\Todo.Users.Tests\SavedPhotos\"));
        }
    }
}

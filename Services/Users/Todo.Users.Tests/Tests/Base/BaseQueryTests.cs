using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Todo.Users.Application.Mapper;
using Todo.Users.Core.Database;
using Todo.Users.Core.Interfaces.Mapper;
using Todo.Users.Core.Options;
using Todo.Users.Database;
using Todo.Users.Tests.Database;

namespace Todo.Users.Tests.Tests.Base
{
    public abstract class BaseQueryTests<TLogger> where TLogger : class
    {
        public readonly IOptions<FilePathsOptions> Paths;

        public readonly IDatabaseContext Database;

        public readonly ILogger<TLogger> Logger;

        public readonly IMapper Mapper;

        public BaseQueryTests()
        {
            Database = DatabaseContextFactory.Create();

            Logger = new Mock<ILogger<TLogger>>().Object;

            Paths = Options.Create(new FilePathsOptions(@"D:\sharp\Todo\Services\Users\Todo.Users.Tests\SavedPhotos\"));

            MapperConfiguration configurationProvider = new MapperConfiguration(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(typeof(DatabaseContext).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
                config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
            });

            Mapper = configurationProvider.CreateMapper();
        }
    }
}

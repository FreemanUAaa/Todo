using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Todo.Users.Core.Database;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Models;
using Todo.Users.Core.Options;

namespace Todo.Users.Application.Handlers.UserCovers.Queries.GetCoverBytes
{
    public class GetCoverBytesQueryHandler : IRequestHandler<GetCoverBytesQuery, byte[]>
    {
        private readonly ILogger<GetCoverBytesQueryHandler> logger;

        private readonly IDatabaseContext database;

        private readonly FilePathsOptions paths;

        public GetCoverBytesQueryHandler(IDatabaseContext database, IOptions<FilePathsOptions> paths, ILogger<GetCoverBytesQueryHandler> logger) =>
            (this.database, this.paths, this.logger) = (database, paths.Value, logger);

        public async Task<byte[]> Handle(GetCoverBytesQuery request, CancellationToken cancellationToken)
        {
            UserCover cover = await database.UserCovers.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(paths.UserCoverPath, cover.FileName);

            byte[] bytes = await File.ReadAllBytesAsync(filePath);

            logger.LogInformation("The cover was obtained successfully");

            return bytes;
        }
    }
}

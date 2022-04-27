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
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.DeleteUserCover
{
    public class DeleteUserCoverCommandHandler : IRequestHandler<DeleteUserCoverCommand>
    {
        private readonly ILogger<DeleteUserCoverCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly IFileManager fileManager;

        private readonly FilePathsOptions paths;

        public DeleteUserCoverCommandHandler(IDatabaseContext database, IFileManager fileManager, IOptions<FilePathsOptions> paths, ILogger<DeleteUserCoverCommandHandler> logger) =>
            (this.database, this.fileManager, this.paths, this.logger) = (database, fileManager, paths.Value, logger);

        public async Task<Unit> Handle(DeleteUserCoverCommand request, CancellationToken cancellationToken)
        {
            UserCover cover = await database.UserCovers.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (cover == null)
            {
                throw new Exception(ExceptionStrings.NotFound);
            }

            string filePath = Path.Combine(paths.UserCoverPath, cover.FileName);

            try
            {
                await fileManager.DeleteFileAsync(filePath);
            }
            catch
            {
                throw new Exception(ExceptionStrings.ErrorOccurred);
            }

            database.UserCovers.Remove(cover);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The cover was removed successfully");

            return Unit.Value;
        }
    }
}

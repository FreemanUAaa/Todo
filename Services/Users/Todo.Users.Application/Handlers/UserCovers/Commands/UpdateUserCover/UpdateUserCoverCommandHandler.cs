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
using Todo.Users.Core.Helpers.Files;
using Todo.Users.Core.Models;
using Todo.Users.Core.Options;
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Handlers.UserCovers.Commands.UpdateUserCover
{
    public class UpdateUserCoverCommandHandler : IRequestHandler<UpdateUserCoverCommand>
    {
        private readonly ILogger<UpdateUserCoverCommandHandler> logger;

        private readonly IDatabaseContext database;

        private readonly FilePathsOptions paths;

        private readonly IFileManager fileManager;

        public UpdateUserCoverCommandHandler(IDatabaseContext database, IFileManager fileManager, IOptions<FilePathsOptions> paths, ILogger<UpdateUserCoverCommandHandler> logger) =>
            (this.database, this.fileManager, this.paths, this.logger) = (database, fileManager, paths.Value, logger);

        public async Task<Unit> Handle(UpdateUserCoverCommand request, CancellationToken cancellationToken)
        {
            UserCover cover = await database.UserCovers.FirstOrDefaultAsync(x => x.UserId == request.UserId);

            if (cover == null)
            {
                cover = new UserCover()
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                };

                database.UserCovers.Add(cover);
            }

            string extension = Path.GetExtension(request.File.FileName);
            bool isValidExtension = FileExtensionsChecker.IsValidUserCoverExtension(extension);

            if (!isValidExtension)
            {
                throw new Exception(ExceptionStrings.ExtensionNotSupported);
            }

            if (!string.IsNullOrEmpty(cover.FileName))
            {
                string coverPath = Path.Combine(paths.UserCoverPath, cover.FileName);

                try
                {
                    await fileManager.DeleteFileAsync(coverPath);
                }
                catch
                {
                    throw new Exception(ExceptionStrings.FailedUpdateCover);
                }
            }

            string newFileName = FileNameGenerator.GenerateUniqueFileName(paths.UserCoverPath, extension, 10);

            try
            {
                string newFilePath = Path.Combine(paths.UserCoverPath, newFileName);
                await fileManager.SaveFileAsync(newFilePath, request.File);
            }
            catch
            {
                throw new Exception(ExceptionStrings.FailedUpdateCover);
            }

            cover.FileName = newFileName;

            database.UserCovers.Update(cover);
            await database.SaveChangesAsync(cancellationToken);

            logger.LogInformation("The cover has been updated successfully");

            return Unit.Value;
        }
    }
}

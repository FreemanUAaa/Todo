using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using Todo.Users.Core.Exceptions;
using Todo.Users.Core.Services;

namespace Todo.Users.Application.Services
{
    public class FileManager : IFileManager
    {
        public async Task DeleteFileAsync(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException();
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            await Task.Run(() => File.Delete(path));
        }

        public async Task SaveFileAsync(string path, IFormFile file)
        {
            if (path == null || file == null)
            {
                throw new ArgumentNullException();
            }

            if (File.Exists(path))
            {
                throw new Exception(ExceptionStrings.FileAlreadyExists);
            }

            using FileStream stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
        }
    }
}

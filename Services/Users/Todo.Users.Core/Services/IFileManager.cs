using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Todo.Users.Core.Services
{
    public interface IFileManager
    {
        Task SaveFileAsync(string path, IFormFile file);

        Task DeleteFileAsync(string path);
    }
}

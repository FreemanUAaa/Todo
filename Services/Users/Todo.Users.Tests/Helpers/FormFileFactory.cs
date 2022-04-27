using Microsoft.AspNetCore.Http;
using System.IO;

namespace Todo.Users.Tests.Helpers
{
    public static class FormFileFactory
    {
        public static IFormFile Create(byte[] bytes, string fileName)
        {
            MemoryStream stream = new MemoryStream(bytes);

            return new FormFile(stream, 0, bytes.Length, fileName, fileName);
        }
    }
}

using System.IO;

namespace Todo.Users.Core.Helpers.Files
{
    public static class FileNameGenerator
    {
        public static string GenerateUniqueFileName(string path, string extension, int attempts)
        {
            for (int i = 0; i <= attempts; i++)
            {
                string newFileName = GetRandomFileName(extension);

                string newFilePath = Path.Combine(path, newFileName);

                if (!File.Exists(newFilePath))
                {
                    return newFileName;
                }
            }

            return null;
        }

        private static string GetRandomFileName(string extension)
        {
            if (extension.Contains("."))
            {
                return Path.GetRandomFileName() + extension;
            }

            return Path.GetRandomFileName() + "." + extension;
        }
    }
}

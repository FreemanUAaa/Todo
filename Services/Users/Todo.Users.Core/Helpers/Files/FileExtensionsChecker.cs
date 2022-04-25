using System.Collections.Generic;

namespace Todo.Users.Core.Helpers.Files
{
    public class FileExtensionsChecker
    {
        public static readonly List<string> UserPhotoAllowedExtension = new List<string>() {
            ".png", ".jpg", ".jpeg", ".jfif", ".pjpeg", ".pjp"
        };

        public static bool IsValidUserCoverExtension(string extension)
        {
            if (!extension.Contains("."))
            {
                extension = "." + extension;
            }

            return UserPhotoAllowedExtension.Contains(extension);
        }
    }
}

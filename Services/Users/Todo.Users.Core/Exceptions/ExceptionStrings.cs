namespace Todo.Users.Core.Exceptions
{
    public static class ExceptionStrings
    {
        public static string FileAlreadyExists => "Еhe file in the specified path is already exists";

        public static string WrongEmailOrPassword => "Invalid email or password";

        public static string ExtensionNotSupported => "Extension not supported";

        public static string EmailIsAlreadyUsed => "Email is already used";

        public static string FailedUpdateCover => "Failed to update cover";

        public static string NotFound => "Page not found";
    }
}

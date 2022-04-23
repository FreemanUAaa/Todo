namespace Todo.Users.Core.Options
{
    public class FilePathsOptions
    {
        public readonly string UserCoverPath;

        public FilePathsOptions(string userCoverPath) => UserCoverPath = userCoverPath;
    }
}

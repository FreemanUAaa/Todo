using System.IO;

namespace Todo.Users.Tests.Assets
{
    public static class Photo
    {
        public static byte[] Bytes { get; set; }

        static Photo()
        {
            Bytes = File.ReadAllBytes(@"D:\sharp\Todo\Services\Users\Todo.Users.Tests\Assets\photo.jpg");
        }
    }
}

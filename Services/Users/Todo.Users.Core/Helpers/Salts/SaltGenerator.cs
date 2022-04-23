using System.Security.Cryptography;

namespace Todo.Users.Core.Helpers.Salts
{
    public class SaltGenerator
    {
        public static int SaltSize => 128 / 8;

        public static byte[] Generate()
        {
            byte[] salt = new byte[SaltSize];

            using RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

            rngCsp.GetNonZeroBytes(salt);

            return salt;
        }
    }
}

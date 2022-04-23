using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Todo.Users.Core.Helpers.Hasher
{
    public static class PasswordHasher
    {
        public static int IterationCount => 10;

        public static int NumBytesRequested => 256 / 8;

        public static string Hash(string password, byte[] salt)
        {
            if (salt == null)
            {
                throw new ArgumentNullException("The salt can't be null");
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password, salt: salt, prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: IterationCount, numBytesRequested: NumBytesRequested));

            return hashed;
        }
    }
}

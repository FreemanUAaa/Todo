using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Todo.Users.Core.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int Lifetime { get; set; }

        public AuthOptions(string issuer, string audience, string key, int lifetime) =>
            (Issuer, Audience, Key, Lifetime) = (issuer, audience, key, lifetime);

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}

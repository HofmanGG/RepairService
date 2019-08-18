using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_BLL.Infrastructure
{
    public static class AuthJwtTokenOptions
    {
        public const string Issuer = "issuer";

        public const string Audience = "helloSocNewt";

        private const string Key = "reallyhidenreallysecurityreallyimpossiblekey";

        public static SecurityKey GetSecurityKey() => 
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
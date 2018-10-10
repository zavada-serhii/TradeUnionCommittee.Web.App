using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TradeUnionCommittee.Api
{
    public class AuthOptions
    {
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string SecretKey { get; set; }
        public static int LifeTime { get; set; }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}

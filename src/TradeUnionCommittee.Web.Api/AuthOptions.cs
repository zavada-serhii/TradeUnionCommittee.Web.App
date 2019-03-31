using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TradeUnionCommittee.Web.Api
{
    public class AuthOptions
    {
        public static string Issuer { get; set; }
        public static string Audience { get; set; }
        public static string Key { get; set; }
        public static int LifeTime { get; set; }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TradeUnionCommittee.Web.Api.Model
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int LifeTime { get; set; }
        public int KeyLength { get; set; }
        public string HashRefreshToken { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        }
    }
}

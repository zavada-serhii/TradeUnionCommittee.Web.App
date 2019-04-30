using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Model;

namespace TradeUnionCommittee.Web.Api.Configurations
{
    public interface IJwtBearerConfiguration
    {
        Task<ActualResult<TokenModel>> SignInByPassword(TokenViewModel model);
        Task<ActualResult<TokenModel>> SignInByRefreshToken(RefreshTokenViewModel model);
    }

    public class JwtBearerConfiguration : IJwtBearerConfiguration
    {
        private readonly IAccountService _accountService;
        private readonly IOptions<AuthOptions> _authOptions;

        public JwtBearerConfiguration(IAccountService accountService, IOptions<AuthOptions> authOptions)
        {
            _accountService = accountService;
            _authOptions = authOptions;
        }

        public Task<ActualResult<TokenModel>> SignInByPassword(TokenViewModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActualResult<TokenModel>> SignInByRefreshToken(RefreshTokenViewModel model)
        {
            throw new System.NotImplementedException();
        }

        //------------------------------------------------------------------------------------------

        private string GetHash(string input)
        {
            using (HashAlgorithm hashAlgorithm = new SHA512CryptoServiceProvider())
            {
                var byteValue = Encoding.UTF8.GetBytes(input + _authOptions.Value.HashRefreshToken);
                var byteHash = hashAlgorithm.ComputeHash(byteValue);
                return Convert.ToBase64String(byteHash);
            }
        }

        private string GenerateRandomCryptographicKey(int keyLength)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[keyLength];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}
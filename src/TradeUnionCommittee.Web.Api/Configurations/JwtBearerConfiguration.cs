using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
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

        public async Task<ActualResult<TokenModel>> SignInByPassword(TokenViewModel model)
        {
            model.ClientType = model.ClientType.ToUpper();

            var identity = await GetIdentity(model.ClientType, model.Email, model.Password);
            if (identity == null)
            {
                return new ActualResult<TokenModel>("User not found");
            }

            var refreshToken = await CreateRefreshToken(model.ClientType, identity.Name);
            if (refreshToken == null)
            {
                return new ActualResult<TokenModel>("Error while create refresh token");
            }

            return new ActualResult<TokenModel> { Result = GetToken(identity, refreshToken) };
        }

        public async Task<ActualResult<TokenModel>> SignInByRefreshToken(RefreshTokenViewModel model)
        {
            model.ClientType = model.ClientType.ToUpper();

            var protectedTicket = await _accountService.GetProtectedTicket(GetHash(model.RefreshToken));
            if (protectedTicket == null)
            {
                return new ActualResult<TokenModel>("Invalid refresh token");
            }

            if (protectedTicket.ClientType != model.ClientType)
            {
                return new ActualResult<TokenModel>("Invalid client id");
            }

            var identity = await GetIdentity(protectedTicket.ClientType, protectedTicket.Email);
            if (identity == null)
            {
                return new ActualResult<TokenModel>("User not found");
            }

            var refreshToken = await CreateRefreshToken(protectedTicket.ClientType, protectedTicket.Email);
            if (refreshToken == null)
            {
                return new ActualResult<TokenModel>("Error while create refresh token");
            }

            return new ActualResult<TokenModel> { Result = GetToken(identity, refreshToken) };
        }

        //------------------------------------------------------------------------------------------

        private async Task<ClaimsIdentity> GetIdentity(string clientType, string email, string password = null)
        {
            AccountDTO user;

            if (password == null)
            {
                user = await _accountService.SignIn(email, null, SignInType.RefreshToken);
            }
            else
            {
                user = await _accountService.SignIn(email, password, SignInType.Credentials);
            }

            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                    new Claim("client_type", clientType)
                };

                return new ClaimsIdentity(claims,
                    "Password",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
            }
            return null;
        }

        private TokenModel GetToken(ClaimsIdentity identity, string refreshToken)
        {
            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromMinutes(_authOptions.Value.LifeTime));

            var jwt = new JwtSecurityToken(
                _authOptions.Value.Issuer,
                _authOptions.Value.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(_authOptions.Value.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                RefreshToken = refreshToken,
                AccessTokenExpires = expires,
                Email = identity.Name,
                Role = identity.Claims.Single(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value
            };
        }

        private async Task<string> CreateRefreshToken(string clientType, string identityName)
        {
            var clientRefreshTokenLifeTime = await _accountService.GetClientRefreshTokenLifeTime(clientType);
            if (clientRefreshTokenLifeTime != null)
            {
                var refreshTokenId = GetHash(GenerateRandomCryptographicKey(_authOptions.Value.KeyLength));
                var now = DateTime.UtcNow;
                var token = new RefreshTokenDTO
                {
                    Id = GetHash(refreshTokenId),
                    ClientType = clientType,
                    Subject = identityName,
                    IssuedUtc = now,
                    ExpiresUtc = now.Add(TimeSpan.FromMinutes(clientRefreshTokenLifeTime.Value))
                };
                await _accountService.CreateRefreshToken(token);
                return refreshTokenId;
            }
            return null;
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
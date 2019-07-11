using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Configurations;
using TradeUnionCommittee.Web.Api.Extensions;

namespace TradeUnionCommittee.Web.Api.Controllers.Account
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IJwtBearerConfiguration _jwtBearer;

        public AccountController(IJwtBearerConfiguration jwtBearer)
        {
            _jwtBearer = jwtBearer;
        }

        /// <summary>
        /// Client Type available values:
        /// 'Web-Application',
        /// 'Desktop-Application' or
        /// 'Mobile-Application'
        /// </summary>
        [HttpPost]
        [Route("Token")]
        [MapToApiVersion("1.0")]
        public async Task Token([FromBody] TokenViewModel viewModel)
        {
            var account = await _jwtBearer.SignInByPassword(viewModel);
            if (account.IsValid)
            {
                await HttpContext.WriteToken(account.Result);
                return;
            }
            await HttpContext.BadRequest(account.ErrorsList);
        }

        /// <summary>
        /// Client Type available values:
        /// 'Web-Application',
        /// 'Desktop-Application' or
        /// 'Mobile-Application'
        /// </summary>
        [HttpPost]
        [Route("RefreshToken")]
        [MapToApiVersion("1.0")]
        public async Task RefreshToken([FromBody] RefreshTokenViewModel viewModel)
        {
            var account = await _jwtBearer.SignInByRefreshToken(viewModel);
            if (account.IsValid)
            {
                await HttpContext.WriteToken(account.Result);
                return;
            }
            await HttpContext.BadRequest(account.ErrorsList);
        }
    }
}
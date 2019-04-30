using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Configurations;
using TradeUnionCommittee.Web.Api.Extensions;

namespace TradeUnionCommittee.Web.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtBearerConfiguration _jwtBearer;

        public AccountController(IJwtBearerConfiguration jwtBearer)
        {
            _jwtBearer = jwtBearer;
        }

        /// <summary>
        /// Examples ClientId:
        /// 0 - Web Application, 
        /// 1 - Desktop Application,
        /// 2 - Mobile Application
        /// </summary>
        [HttpPost]
        [Route("Token")]
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
        /// Examples ClientId:
        /// 0 - Web Application, 
        /// 1 - Desktop Application,
        /// 2 - Mobile Application
        /// </summary>
        [HttpPost]
        [Route("RefreshToken")]
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
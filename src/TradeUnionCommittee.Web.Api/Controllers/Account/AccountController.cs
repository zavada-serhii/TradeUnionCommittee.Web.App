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
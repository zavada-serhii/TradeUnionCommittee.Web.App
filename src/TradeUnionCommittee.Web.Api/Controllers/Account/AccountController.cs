using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Configurations;
using TradeUnionCommittee.Web.Api.Model;

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
        [Route("Token", Name = "Token")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Token([FromBody] TokenViewModel viewModel)
        {
            var account = await _jwtBearer.SignInByPassword(viewModel);
            if (account.IsValid)
            {
                return WriteToken(account.Result);
            }
            return BadRequest(account.ErrorsList);
        }

        /// <summary>
        /// Client Type available values:
        /// 'Web-Application',
        /// 'Desktop-Application' or
        /// 'Mobile-Application'
        /// </summary>
        [HttpPost]
        [Route("RefreshToken", Name = "RefreshToken")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenViewModel viewModel)
        {
            var account = await _jwtBearer.SignInByRefreshToken(viewModel);
            if (account.IsValid)
            {
                return WriteToken(account.Result);
            }
            return BadRequest(account.ErrorsList);
        }

        //-----------------------------------------------------------------------------------------------

        private OkObjectResult WriteToken(TokenModel model)
        {
            return Ok(new
            {
                access_token = model.AccessToken,
                refresh_token = model.RefreshToken,
                expires_in = model.AccessTokenExpires.Ticks,
                token_type = model.TokenType,
                user_name = model.Email,
                user_role = model.Role
            });
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Configurations;

namespace TradeUnionCommittee.Web.Api.Controllers.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtBearerConfiguration _jwtBearer;
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService, IJwtBearerConfiguration jwtBearer)
        {
            _jwtBearer = jwtBearer;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("Token")]
        public async Task Token([FromBody] BaseLoginViewModel viewModel)
        {
            var identity = await _jwtBearer.GetToken(viewModel.Email, viewModel.Password);
            if (identity.IsValid)
            {
                Response.ContentType = "application/json";
                await Response.WriteAsync(identity.Result);
                return;
            }
            Response.StatusCode = 400;
            await Response.WriteAsync(identity.ErrorsList.FirstOrDefault());
        }
    }
}
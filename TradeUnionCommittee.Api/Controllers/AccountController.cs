using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TradeUnionCommittee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly List<Person> _people = new List<Person>
        {
            new Person {Login="stewie.griffin@test.com", Password="P@ssw0rd_admin", Role = "Admin" }
        };

        //private readonly IAccountService _accountService;

        //public AccountController(IAccountService accountService)
        //{
        //    _accountService = accountService;
        //}

        [HttpPost("token")]
        public async Task Token()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                Response.StatusCode = 400;
                await Response.WriteAsync("Invalid username or password.");
                return;
            }

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifeTime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            Response.ContentType = "application/json";
            await Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            //var result = await _accountService.Login(username, password);

            //if (result.IsValid)
            {
                //var allUser = await _accountService.GetAllUsersAsync();
                // user = allUser.Result.FirstOrDefault(x => x.Email == username);

                Person person = _people.FirstOrDefault(x => x.Login == username && x.Password == password);

                //if (user != null)
                {
                    //var role = ConvertToEnglishLang(user.Role);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, username),
                        //new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role)
                    };
                    return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                }

                return null;
            }
            return null;
        }

        private string ConvertToEnglishLang(string param)
        {
            switch (param)
            {
                case "Адміністратор":
                    return "Admin";
                case "Бухгалтер":
                    return "Accountant";
                case "Заступник":
                    return "Deputy";
                default:
                    return string.Empty;
            }
        }
    }

    public class Person
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

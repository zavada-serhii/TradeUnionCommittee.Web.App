using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TradeUnionCommittee.Web.Api.Model;

namespace TradeUnionCommittee.Web.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static async Task BadRequest(this HttpContext context, string error)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(error);
        }

        public static async Task BadRequest(this HttpContext context, IEnumerable<string> errors)
        {
            var result = new StringBuilder();
            foreach (var error in errors)
            {
                result.Append(error).Append('\n');
            }
            await context.BadRequest(result.ToString());
        }

        public static async Task WriteToken(this HttpContext context, TokenModel model)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                access_token = model.AccessToken,
                refresh_token = model.RefreshToken,
                expires_in = model.AccessTokenExpires,
                token_type = model.TokenType,
                user_name = model.Email,
                user_role = model.Role
            }));
        }
    }
}
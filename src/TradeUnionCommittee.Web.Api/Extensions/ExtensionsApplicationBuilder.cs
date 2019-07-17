using Microsoft.AspNetCore.Builder;
using TradeUnionCommittee.Web.Api.Middleware;

namespace TradeUnionCommittee.Web.Api.Extensions
{
    public static class ExtensionsApplicationBuilder
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}

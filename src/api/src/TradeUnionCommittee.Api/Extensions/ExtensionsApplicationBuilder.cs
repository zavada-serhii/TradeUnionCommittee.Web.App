using Microsoft.AspNetCore.Builder;
using TradeUnionCommittee.Api.Middleware;

namespace TradeUnionCommittee.Api.Extensions
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

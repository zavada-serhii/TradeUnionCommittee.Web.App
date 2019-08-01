using Microsoft.Extensions.DependencyInjection;

namespace TradeUnionCommittee.DataAnalysis.Service
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDataAnalysisService(this IServiceCollection services, string url)
        {
            services.AddSingleton(x => new AnalysisClient(url));
            services.AddTransient<ITestService, TestService>();
            return services;
        }
    }
}
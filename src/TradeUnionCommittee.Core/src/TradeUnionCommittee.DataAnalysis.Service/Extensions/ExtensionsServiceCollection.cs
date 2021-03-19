using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DataAnalysis.Service.Contracts;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.Services;

namespace TradeUnionCommittee.DataAnalysis.Service.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDataAnalysisService(this IServiceCollection services, DataAnalysisConnection connection)
        {
            services.AddSingleton(x => new DataAnalysisClient(connection));

            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IForecastingService, ForecastingService>();
            services.AddTransient<IDeterminingService, DeterminingService>();
            services.AddTransient<IOptimizationService, OptimizationService>();
            services.AddTransient<ICheckingService, CheckingService>();

            return services;
        }
    }
}
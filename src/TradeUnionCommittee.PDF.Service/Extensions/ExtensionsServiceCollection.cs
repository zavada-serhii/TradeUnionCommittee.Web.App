using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.PDF.Service.Interfaces;
using TradeUnionCommittee.PDF.Service.Services;

namespace TradeUnionCommittee.PDF.Service.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddPdfService(this IServiceCollection services)
        {
            services.AddTransient<IReportGeneratorService, ReportGeneratorService>();
            return services;
        }
    }
}
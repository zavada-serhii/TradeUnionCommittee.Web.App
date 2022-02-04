using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using System.Text;
using TradeUnionCommittee.DataAnalysis.Service.Contracts;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.Services;

namespace TradeUnionCommittee.DataAnalysis.Service.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDataAnalysisService(this IServiceCollection services, DataAnalysisConnection connection)
        {
            services.AddHttpClient("DataAnalysis", c =>
            {
                c.BaseAddress = new Uri(connection.Url);

                if (connection.UseBasicAuthentication)
                {
                    var credential = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{connection.UserName}:{connection.Password}"));
                    c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credential);
                }
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var httpClientHandler = new HttpClientHandler();

                if (connection.IgnoreCertificateValidation)
                {
                    httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    httpClientHandler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true;
                }

                return httpClientHandler;
            });

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
using Microsoft.Extensions.DependencyInjection;
using Minio;
using TradeUnionCommittee.CloudStorage.Service.Contracts;
using TradeUnionCommittee.CloudStorage.Service.Model;
using TradeUnionCommittee.CloudStorage.Service.Services;
using TradeUnionCommittee.DAL.CloudStorage.Extensions;

namespace TradeUnionCommittee.CloudStorage.Service.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddCloudStorageService(this IServiceCollection services, CloudStorageConnection credentials, string dbConnection)
        {
            services.AddCloudStorageContext(dbConnection);

            var client = new MinioClient()
                .WithEndpoint(credentials.Url)
                .WithCredentials(credentials.AccessKey, credentials.SecretKey);

            if (credentials.UseSsl)
                client.WithSSL();

            if (credentials.IgnoreCertificateValidation)
                client.WithHttpClient(new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (r, c, ch, policy) => true
                }));

            services.AddSingleton(x => client.Build());
            services.AddTransient<IReportPdfBucketService, ReportPdfBucketService>();
            return services;
        }
    }
}
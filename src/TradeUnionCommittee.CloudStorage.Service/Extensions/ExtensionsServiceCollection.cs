using Microsoft.Extensions.DependencyInjection;
using Minio;
using TradeUnionCommittee.CloudStorage.Service.Interfaces;
using TradeUnionCommittee.CloudStorage.Service.Model;
using TradeUnionCommittee.CloudStorage.Service.Services;
using TradeUnionCommittee.DAL.CloudStorage.Extensions;

namespace TradeUnionCommittee.CloudStorage.Service.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddCloudStorageService(this IServiceCollection services, CloudStorageCredentials credentials)
        {
            services.AddCloudStorageContext(credentials.DbConnectionString);
            if (credentials.UseStorageSsl)
            {
                services.AddSingleton(x => new MinioClient(credentials.Url, credentials.AccessKey, credentials.SecretKey).WithSSL());
            }
            else
            {
                services.AddSingleton(x => new MinioClient(credentials.Url, credentials.AccessKey, credentials.SecretKey));
            }
            services.AddScoped<IReportPdfBucketService, ReportPdfBucketService>();
            return services;
        }
    }
}
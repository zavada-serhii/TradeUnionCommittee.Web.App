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
            if (credentials.UseSsl)
            {
                services.AddSingleton(x => new MinioClient(credentials.Url, credentials.AccessKey, credentials.SecretKey).WithSSL());
            }
            else
            {
                services.AddSingleton(x => new MinioClient(credentials.Url, credentials.AccessKey, credentials.SecretKey));
            }
            services.AddTransient<IReportPdfBucketService, ReportPdfBucketService>();
            return services;
        }
    }
}
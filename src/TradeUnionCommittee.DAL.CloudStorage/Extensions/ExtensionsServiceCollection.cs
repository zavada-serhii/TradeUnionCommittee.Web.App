using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.CloudStorage.EF;

namespace TradeUnionCommittee.DAL.CloudStorage.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddCloudStorageContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeCloudStorageContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
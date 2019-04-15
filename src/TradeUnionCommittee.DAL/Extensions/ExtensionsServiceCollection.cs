using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Repository;

namespace TradeUnionCommittee.DAL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<ITrigramSearchRepository, TrigramSearchRepository>();
            return services;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.Audit.EF;
using TradeUnionCommittee.DAL.Audit.Repository;

namespace TradeUnionCommittee.DAL.Audit.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddAuditDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeAuditContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<ISystemAuditRepository, SystemAuditRepository>();
            return services;
        }
    }
}
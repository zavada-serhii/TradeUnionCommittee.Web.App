using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Native;

namespace TradeUnionCommittee.DAL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TradeUnionCommitteeContext>();
            services.AddTransient<ISearchNative, SearchNative>();
            services.AddTransient<ISystemAuditNative, SystemAuditNative>();
            return services;
        }
    }
}
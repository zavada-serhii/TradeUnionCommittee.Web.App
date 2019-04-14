using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.Identity.EF;
using TradeUnionCommittee.DAL.Identity.Entities;

namespace TradeUnionCommittee.DAL.Identity.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeIdentityContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TradeUnionCommitteeIdentityContext>();
            return services;
        }
    }
}
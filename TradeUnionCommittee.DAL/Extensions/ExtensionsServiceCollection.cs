using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.DAL.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TradeUnionCommitteeContext>(options => options.UseNpgsql(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<TradeUnionCommitteeContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
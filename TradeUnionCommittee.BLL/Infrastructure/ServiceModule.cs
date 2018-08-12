using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.DAL.Repositories;

namespace TradeUnionCommittee.BLL.Infrastructure
{
    public sealed class ServiceModule
    {
        public ServiceModule(string connectionString, IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>(o => new UnitOfWork(connectionString));
            services.AddSingleton(cm => AutoMapperModule.ConfigureAutoMapper());
        }
    }

    internal class AutoMapperModule
    {
        /// <summary>
        ///     Configures the automatic mapper.
        /// </summary>
        /// <returns>IMapper.</returns>
        public static IMapper ConfigureAutoMapper()
        {
            return new MapperConfiguration(map =>
            {
               

            }).CreateMapper();
        }
    }
}
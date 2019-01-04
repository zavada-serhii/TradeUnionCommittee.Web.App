using TradeUnionCommittee.BLL.Interfaces.Dashboard;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;

        public DashboardService(IUnitOfWork database, IAutoMapperUtilities mapperService)
        {
            _database = database;
            _mapperService = mapperService;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
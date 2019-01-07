using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.DTO;
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

        public PieResult PieData_Test()
        {
            const int count = 12;
            return new PieResult
            {
                Data = RandomNumber(1, 20, count),
                Labels = RandomString(5, count)
            };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private IEnumerable<double> RandomNumber(double minimum, double maximum, int count)
        {
            var random = new Random();
            var result = new List<double>();

            for (var i = 0; i < count; i++)
            {
                result.Add(Math.Round(random.NextDouble() * (maximum - minimum) + minimum, 2));
            }

            return result;
        }

        private IEnumerable<string> RandomString(int length, int count)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = new List<string>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new string(Enumerable.Repeat(chars, length)
                    .Select(s => s[random.Next(s.Length)]).ToArray()));
            }
            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
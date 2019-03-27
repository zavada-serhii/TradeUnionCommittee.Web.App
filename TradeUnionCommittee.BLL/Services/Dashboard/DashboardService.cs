using System;
using System.Collections.Generic;
using System.Linq;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;
using TradeUnionCommittee.DAL.EF;

namespace TradeUnionCommittee.BLL.Services.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;

        public DashboardService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService)
        {
            _context = context;
            _mapperService = mapperService;
        }

        public PieResult PieData_Test()
        {
            const int count = 12;
            return new PieResult
            {
                Data = RandomNumber(1, 20, count),
                Labels = RandomString(count)
            };
        }

        public BarResult BarData_Test()
        {
            const int count = 20;
            return new BarResult
            {
                Data = RandomNumber(1, 20000, count),
                Labels = RandomString(count)
            };
        }

        public AreaResult AreaData_Test()
        {
            const int count = 40;
            return new AreaResult
            {
                Data = RandomNumber(1, 40000, count),
                Labels = RandomString(count)
            };
        }

        public RadarResult RadarData_Test()
        {
            const int count = 10;

            var radar = new List<DataSet>();

            for (var i = 0; i < 3; i++)
            {
                radar.Add(new DataSet
                {
                    Label = RandomString(1).FirstOrDefault(),
                    Data = RandomNumber(1, 20, count)
                });
            }

            return new RadarResult
            {
                Labels = RandomString(count),
                Data = radar
            };
        }

        public LineResult LineData_Test()
        {
            const int count = 10;
            var line = new List<DataSet>();

            for (var i = 0; i < 3; i++)
            {
                line.Add(new DataSet
                {
                    Label = RandomString(1).FirstOrDefault(),
                    Data = RandomNumber(1, 20, count)
                });
            }

            return new LineResult
            {
                Labels = RandomString(count),
                Data = line
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

        private IEnumerable<string> RandomString(int count)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var result = new List<string>();

            for (var i = 0; i < count; i++)
            {
                result.Add(new string(Enumerable.Repeat(chars, 5)
                    .Select(s => s[random.Next(s.Length)]).ToArray()));
            }
            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
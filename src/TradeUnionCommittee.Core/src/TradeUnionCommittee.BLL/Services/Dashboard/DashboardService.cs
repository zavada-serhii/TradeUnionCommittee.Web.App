using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Dashboard
{
    internal class DashboardService : IDashboardService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly ITestService _testService;

        public DashboardService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, ITestService testService)
        {
            _context = context;
            _mapperService = mapperService;
            _testService = testService;
        }

        public PieResult PieData_Test()
        {
            //var tmp1 = _testService.HealthCheck();
            //var tmp2 = _testService.TestPostJson();
            //var tmp3 = _testService.TestPostCsv();

            //var tmp4 = _testService.RunAllTasks();

            const int count = 12;
            return new PieResult
            {
                Data = RandomDoubleNumbers(1, 20, count),
                Labels = RandomStrings(count)
            };
        }

        public BarResult BarData_Test()
        {
            const int count = 20;
            return new BarResult
            {
                Data = RandomDoubleNumbers(1, 20000, count),
                Labels = RandomStrings(count)
            };
        }

        public AreaResult AreaData_Test()
        {
            const int count = 40;
            return new AreaResult
            {
                Data = RandomDoubleNumbers(1, 40000, count),
                Labels = RandomStrings(count)
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
                    Label = RandomStrings(1).FirstOrDefault(),
                    Data = RandomDoubleNumbers(1, 20, count)
                });
            }

            return new RadarResult
            {
                Labels = RandomStrings(count),
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
                    Label = RandomStrings(1).FirstOrDefault(),
                    Data = RandomDoubleNumbers(1, 20, count)
                });
            }

            return new LineResult
            {
                Labels = RandomStrings(count),
                Data = line
            };
        }

        public IEnumerable<BubbleResult> BubbleData_Test()
        {
            var result = new List<BubbleResult>();

            for (var i = 0; i < 5; i++)
            {
                result.Add(new BubbleResult
                {
                    Label = RandomString(),
                    BackgroundColor = HexConverter(RandomColor()),
                    BorderColor = HexConverter(RandomColor()),
                    Data = RandomBubble()
                });
            }

            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private IEnumerable<double> RandomDoubleNumbers(double minimum, double maximum, int count)
        {
            var random = new Random();
            var result = new List<double>();

            for (var i = 0; i < count; i++)
            {
                result.Add(Math.Round(random.NextDouble() * (maximum - minimum) + minimum, 2));
            }

            return result;
        }

        private double RandomNumber(double minimum, double maximum)
        {
            return Math.Round(new Random().NextDouble() * (maximum - minimum) + minimum, 2);
        }

        private IEnumerable<string> RandomStrings(int count)
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

        private string RandomString()
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 5).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private Color RandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private IEnumerable<Bubble> RandomBubble()
        {
            var bubbles = new List<Bubble>();
            var randomNumber = new Random().Next(0, 150);
            for (var j = 0; j < randomNumber; j++)
            {
                bubbles.Add(new Bubble
                {
                    X = RandomNumber(0.0, 300.0),
                    Y = RandomNumber(0.0, 300.0),
                    R = 4
                });
            }
            return bubbles;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private string HexConverter(Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
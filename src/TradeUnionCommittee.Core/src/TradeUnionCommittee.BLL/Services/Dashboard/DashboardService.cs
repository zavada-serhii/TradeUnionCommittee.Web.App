using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;
using TradeUnionCommittee.DataAnalysis.Service.Services;

namespace TradeUnionCommittee.BLL.Services.Dashboard
{
    internal class DashboardService : IDashboardService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;
        private readonly IForecastingService _forecastingService;

        public DashboardService(TradeUnionCommitteeContext context, IMapper mapper, IForecastingService forecastingService)
        {
            _context = context;
            _mapper = mapper;
            _forecastingService = forecastingService;
        }

        public async Task<ChartResult<IEnumerable<IEnumerable<double>>>> CorrelationAnalysis()
        {
            try
            {
                var resultData = new List<Task11Model>();
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new 
                    {
                        x.BirthDate,
                        TravelCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Travel),
                        WellnessCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Wellness),
                        TourCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Tour)
                    })
                    .ToListAsync();

                foreach (var data in dbData)
                {
                    resultData.Add(new Task11Model
                    {
                        Age = data.BirthDate.CalculateAge(),
                        TravelCount = data.TravelCount,
                        WellnessCount = data.WellnessCount,
                        TourCount = data.TourCount,
                    });
                }

                var apiData = _forecastingService.CorrelationAnalysis(resultData).ToList();

                var result = new List<List<double>>();
                for (var i = 0; i < apiData.Count; i++)
                {
                    for (var j = 0; j < apiData.ElementAt(i).Count(); j++)
                    {
                        result.Add(new List<double> { i, j, apiData.ElementAt(i).ElementAt(j) });
                    }
                }

                return new ChartResult<IEnumerable<IEnumerable<double>>> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<ChartResult<BasicColumn>> CheckingSignificanceCoefficients()
        {
            try
            {
                var resultData = new List<Task11Model>();
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new
                    {
                        x.BirthDate,
                        TravelCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Travel),
                        WellnessCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Wellness),
                        TourCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Tour)
                    })
                    .ToListAsync();

                foreach (var data in dbData)
                {
                    resultData.Add(new Task11Model
                    {
                        Age = data.BirthDate.CalculateAge(),
                        TravelCount = data.TravelCount,
                        WellnessCount = data.WellnessCount,
                        TourCount = data.TourCount,
                    });
                }

                var apiData = _forecastingService.CheckingSignificanceCoefficients(resultData).ToList();

                var result = new BasicColumn
                {
                    Categories = apiData.Select(x => $"{x.FirstCriterion} - {x.SecondCriterion}"),
                    Series = new List<SeriesBasicColumn>
                    {
                        new SeriesBasicColumn
                        {
                            Name = "TCriteria",
                            Data = apiData.Select(x => x.TCriteria)
                        },

                        new SeriesBasicColumn
                        {
                            Name = "TStatistics",
                            Data = apiData.Select(x => x.TStatistics)
                        }
                    }
                };

                return new ChartResult<BasicColumn> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<ChartResult<IEnumerable<BubbleResult>>> ClusterAnalysis(TypeEvents type)
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new 
                    {
                        x.BirthDate,
                        EventCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == (TypeEvent)type),
                    })
                    .ToListAsync();

                var data = dbData.Select(x => new Task14Model
                {
                    X = x.BirthDate.CalculateAge(),
                    Y = x.EventCount
                });

                var countClusters = 6;
                var apiData = _forecastingService.ClusterAnalysis(data, countClusters);
                var clusterColors = GetRandomBubbleColors(countClusters * 2).ToList();

                var result = new List<BubbleResult>();
                for (var i = 0; i < countClusters; i++)
                {
                    var x = apiData.X.ElementAt(i).ToList();
                    var y = apiData.Y.ElementAt(i);

                    result.Add(new BubbleResult
                    {
                        Label = $"{x.Min()}-{x.Max()}",
                        BackgroundColor = clusterColors.ElementAt(i),
                        BorderColor = clusterColors.ElementAt(i + 2),
                        Data = x.Zip(y, (a, b) => new Bubble { X = a, Y = b, R = 4 })
                    });
                }

                return new ChartResult<IEnumerable<BubbleResult>> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public async Task<ChartResult<BarResult>> GetEmployeeAgeGroup()
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Select(x => x.BirthDate)
                    .ToListAsync();

                var ages = dbData
                    .Select(x => x.CalculateAge())
                    .ToList();

                var result = new BarResult
                {
                    Data = new List<int>
                    {
                        ages.Count(x => x < 18),
                        ages.Count(x => x >= 18 && x <= 21),
                        ages.Count(x => x >= 22 && x <= 25),
                        ages.Count(x => x >= 26 && x <= 30),
                        ages.Count(x => x >= 31 && x <= 35),
                        ages.Count(x => x >= 36 && x <= 40),
                        ages.Count(x => x >= 41 && x <= 45),
                        ages.Count(x => x >= 46 && x <= 50),
                        ages.Count(x => x >= 51 && x <= 55),
                        ages.Count(x => x >= 56 && x <= 60),
                        ages.Count(x => x >= 61 && x <= 65),
                        ages.Count(x => x >= 66 && x <= 70),
                        ages.Count(x => x >= 71 && x <= 75),
                        ages.Count(x => x >= 76 && x <= 80),
                        ages.Count(x => x >= 81 && x <= 85),
                        ages.Count(x => x >= 86 && x <= 89),
                        ages.Count(x => x >= 90),
                    },
                    Labels = new List<string>
                    {
                        "Less than 18",
                        "18-21",
                        "22-25",
                        "26-30",
                        "31-35",
                        "36-40",
                        "41-45",
                        "46-50",
                        "51-55",
                        "56-60",
                        "61-65",
                        "66-70",
                        "71-75",
                        "76-80",
                        "81-85",
                        "86-89",
                        "More than 90"
                    }
                };

                return new ChartResult<BarResult> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Test Services

        public PieResult PieData_Test()
        {
            const int count = 12;
            return new PieResult
            {
                Data = RandomDoubleNumbers(1, 20, count),
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

        private IEnumerable<string> GetRandomBubbleColors(int count)
        {
            var random = new Random();
            var result = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

            while (count > result.Count)
            {
                var color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
                result.Add(HexConverter(color));
            }

            return result;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private string HexConverter(Color c) => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");

        //------------------------------------------------------------------------------------------------------------------------------------------

        #endregion
    }
}
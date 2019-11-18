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
        private readonly IDeterminingService _determiningService;

        public DashboardService(TradeUnionCommitteeContext context, IMapper mapper, 
            IForecastingService forecastingService, 
            IDeterminingService determiningService)
        {
            _context = context;
            _mapper = mapper;
            _forecastingService = forecastingService;
            _determiningService = determiningService;
        }

        #region Task 1

        /// <summary>
        /// Task 1.1
        /// </summary>
        /// <returns></returns>
        public async Task<ChartResult<IEnumerable<IEnumerable<double>>>> CorrelationAnalysisBetweenTeacherAgeAndTypeOfEvent()
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new Task11Model
                    {
                        Age = x.BirthDate.CalculateAge(),
                        TravelCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Travel),
                        WellnessCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Wellness),
                        TourCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Tour)
                    })
                    .ToListAsync();

                var apiData = _forecastingService.CorrelationAnalysis(dbData).ToList();

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

        /// <summary>
        /// Task 1.2
        /// </summary>
        /// <returns></returns>
        public async Task<ChartResult<BarChart<Series>>> CheckingSignificanceAgeTeacherAndTypeOfEvent()
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new Task11Model
                    {
                        Age = x.BirthDate.CalculateAge(),
                        TravelCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Travel),
                        WellnessCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Wellness),
                        TourCount = x.EventEmployees.Count(c => c.IdEventNavigation.Type == TypeEvent.Tour)
                    })
                    .ToListAsync();

                var apiData = _forecastingService.CheckingSignificanceCoefficients(dbData).ToList();

                var result = new BarChart<Series>
                {
                    Labels = apiData.Select(x => $"{x.FirstCriterion} - {x.SecondCriterion}"),
                    Data = new List<Series>
                    {
                        new Series
                        {
                            Name = "TCriteria",
                            Data = apiData.Select(x => x.TCriteria)
                        },

                        new Series
                        {
                            Name = "TStatistics",
                            Data = apiData.Select(x => x.TStatistics)
                        }
                    }
                };

                return new ChartResult<BarChart<Series>> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        /// <summary>
        /// Task 1.3
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ChartResult<IEnumerable<BubbleChart<Bubble>>>> ClusterAnalysisAgeTeacherAndTypeOfEvent(TypeEvents type)
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Include(x => x.EventEmployees)
                    .ThenInclude(x => x.IdEventNavigation)
                    .Select(x => new Task14Model
                    {
                        X = x.BirthDate.CalculateAge(),
                        Y = x.EventEmployees.Count(c => c.IdEventNavigation.Type == (TypeEvent)type),
                    })
                    .ToListAsync();

                const int countClusters = 6;
                var apiData = _forecastingService.ClusterAnalysis(dbData, countClusters);
                var clusterColors = GetRandomBubbleColors(countClusters * 2).ToList();

                var result = new List<BubbleChart<Bubble>>();
                for (var i = 0; i < countClusters; i++)
                {
                    var x = apiData.X.ElementAt(i).ToList();
                    var y = apiData.Y.ElementAt(i);

                    result.Add(new BubbleChart<Bubble>
                    {
                        Label = $"{x.Min()}-{x.Max()}",
                        BackgroundColor = clusterColors.ElementAt(i),
                        BorderColor = clusterColors.ElementAt(i + 2),
                        Data = x.Zip(y, (a, b) => new Bubble { X = a, Y = b, R = 4 })
                    });
                }

                return new ChartResult<IEnumerable<BubbleChart<Bubble>>> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        /// <summary>
        /// Task 1.4
        /// </summary>
        /// <returns></returns>
        public async Task<ChartResult<BarChart<int>>> GetEmployeeAgeGroup()
        {
            try
            {
                var dbData = await _context
                    .Employee
                    .Select(x => x.BirthDate.CalculateAge())
                    .ToListAsync();

                var result = new BarChart<int>
                {
                    Data = new List<int>
                    {
                        dbData.Count(x => x < 18),
                        dbData.Count(x => x >= 18 && x <= 21),
                        dbData.Count(x => x >= 22 && x <= 25),
                        dbData.Count(x => x >= 26 && x <= 30),
                        dbData.Count(x => x >= 31 && x <= 35),
                        dbData.Count(x => x >= 36 && x <= 40),
                        dbData.Count(x => x >= 41 && x <= 45),
                        dbData.Count(x => x >= 46 && x <= 50),
                        dbData.Count(x => x >= 51 && x <= 55),
                        dbData.Count(x => x >= 56 && x <= 60),
                        dbData.Count(x => x >= 61 && x <= 65),
                        dbData.Count(x => x >= 66 && x <= 70),
                        dbData.Count(x => x >= 71 && x <= 75),
                        dbData.Count(x => x >= 76 && x <= 80),
                        dbData.Count(x => x >= 81 && x <= 85),
                        dbData.Count(x => x >= 86 && x <= 89),
                        dbData.Count(x => x >= 90),
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

                return new ChartResult<BarChart<int>> { Chart = result };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        #endregion

        #region Task 2

        /// <summary>
        /// Task 2.1
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ChartResult<BarChart<double>>> MultiCorrelationBetweenTypeOfEventAndDependents(TypeEvents type)
        {
            var dbData = await _context
                .Employee
                .Include(x => x.EventEmployees)
                .ThenInclude(x => x.IdEventNavigation)
                .Include(x => x.Children)
                .Include(x => x.GrandChildren)
                .Select(x => new Task21Model
                {
                    Y = x.EventEmployees.Count(c => c.IdEventNavigation.Type == (TypeEvent)type),
                    X1 = x.Children.Count,
                    X2 = x.GrandChildren.Count
                })
                .ToListAsync();

            var apiData = _determiningService.MultiCorrelationCoefficient(dbData);

            return new ChartResult<BarChart<double>>
            {
                Chart = new BarChart<double>
                {
                    Data = new List<double> { apiData },
                    Labels = new List<string> { "Multi correlation coefficient" }
                }
            };
        }

        /// <summary>
        /// Task 2.2
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<ChartResult<IEnumerable<BubbleChart<Bubble>>>> ClusterAnalysisSignHavingChildrenAndTypeOfEvent(TypeEvents type)
        {
            var dbData = await _context
                .Employee
                .Include(x => x.EventEmployees)
                .ThenInclude(x => x.IdEventNavigation)
                .Include(x => x.Children)
                .Select(x => new Task14Model
                {
                    X = x.EventEmployees.Count(c => c.IdEventNavigation.Type == (TypeEvent)type),
                    Y = x.Children.Count
                })
                .ToListAsync();

            const int countClusters = 3;
            var apiData = _forecastingService.ClusterAnalysis(dbData, countClusters);
            var clusterColors = GetRandomBubbleColors(countClusters * 2).ToList();

            var result = new List<BubbleChart<Bubble>>();
            for (var i = 0; i < countClusters; i++)
            {
                var x = apiData.X.ElementAt(i).ToList();
                var y = apiData.Y.ElementAt(i);

                result.Add(new BubbleChart<Bubble>
                {
                    Label = $"{x.Min()}-{x.Max()}",
                    BackgroundColor = clusterColors.ElementAt(i),
                    BorderColor = clusterColors.ElementAt(i + 2),
                    Data = x.Zip(y, (a, b) => new Bubble { X = a, Y = b, R = 4 })
                });
            }

            return new ChartResult<IEnumerable<BubbleChart<Bubble>>> { Chart = result };
        }

        /// <summary>
        /// Task 2.3
        /// </summary>
        /// <returns></returns>
        public async Task<ChartResult<PieChart<int>>> GetPercentageRatioHavingDependents()
        {
            var dbData = await _context
                .Employee
                .Include(x => x.Children)
                .Include(x => x.GrandChildren)
                .Select(x => new
                {
                    WithChildren = x.Children.Any() && !x.GrandChildren.Any() ? 1 : 0,
                    WithGrandChildren = !x.Children.Any() && x.GrandChildren.Any() ? 1 : 0,
                    WithChildrenAndGrandChildren = x.Children.Any() && x.GrandChildren.Any() ? 1 : 0,
                    WithoutChildrenAndGrandChildren = !x.Children.Any() && !x.GrandChildren.Any() ? 1 : 0
                })
                .GroupBy(x => 1)
                .Select(x => new List<int>
                {
                    x.Sum(e => e.WithChildren),
                    x.Sum(e => e.WithGrandChildren),
                    x.Sum(e => e.WithChildrenAndGrandChildren),
                    x.Sum(e => e.WithoutChildrenAndGrandChildren)
                })
                .ToListAsync();

            return new ChartResult<PieChart<int>>
            {
                Chart = new PieChart<int>
                {
                    Data = dbData.First(),
                    Labels = new List<string>
                    {
                        "Employees only with children",
                        "Employees only with grandchildren",
                        "Employees only with children and grandchildren",
                        "Employees without children and grandchildren"
                    }
                }
            };
        }

        /// <summary>
        /// Task 2.4 - 2.6
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task RegressionModelInfluenceDependentsAndTypeOfEvent(TypeEvents type)
        {
            var firstTypeEvent = (TypeEvent)type;

            var typeEventValues = Enum.GetValues(typeof(TypeEvent))
                .Cast<TypeEvent>()
                .Where(x => x != firstTypeEvent);

            var dbData = await _context
                .Employee
                .Include(x => x.EventEmployees)
                .ThenInclude(x => x.IdEventNavigation)
                .Include(x => x.AwardEmployees)
                .Include(x => x.MaterialAidEmployees)
                .Include(x => x.Children)
                .Include(x => x.GrandChildren)
                .Select(x => new Task24Model
                {
                    Y = x.EventEmployees.Count(c => c.IdEventNavigation.Type == firstTypeEvent),
                    X1 = x.EventEmployees.Count(c => c.IdEventNavigation.Type == typeEventValues.ElementAt(0)),
                    X2 = x.EventEmployees.Count(c => c.IdEventNavigation.Type == typeEventValues.ElementAt(1)),
                    X3 = x.AwardEmployees.Count,
                    X4 = x.MaterialAidEmployees.Count,
                    X5 = x.Children.Count,
                    X6 = x.GrandChildren.Count
                })
                .ToListAsync();

            var apiData = _determiningService.MultiFactorModel(dbData);
        }

        /// <summary>
        /// Task 2.7
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task ReducedAnalysisDataDependentsAndTypeOfEvent(TypeEvents type)
        {
            var firstTypeEvent = (TypeEvent)type;

            var typeEventValues = Enum.GetValues(typeof(TypeEvent))
                .Cast<TypeEvent>()
                .Where(x => x != firstTypeEvent);

            var dbData = await _context
                .Employee
                .Include(x => x.EventEmployees)
                .ThenInclude(x => x.IdEventNavigation)
                .Include(x => x.Children)
                .Include(x => x.GrandChildren)
                .Select(x => new Task27Model
                {
                    X1 = x.EventEmployees.Count(c => c.IdEventNavigation.Type == firstTypeEvent),
                    X2 = x.EventEmployees.Count(c => c.IdEventNavigation.Type == typeEventValues.ElementAt(0)),
                    X3 = x.EventEmployees.Count(c => c.IdEventNavigation.Type == typeEventValues.ElementAt(1)),
                    X4 = x.Children.Count,
                    X5 = x.GrandChildren.Count
                })
                .ToListAsync();

            var apiData = _determiningService.PrincipalComponentAnalysis(dbData, 2).ToList();
        }

        #endregion

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Test Services

        public PieChart<double> PieData_Test()
        {
            const int count = 12;
            return new PieChart<double>
            {
                Data = RandomDoubleNumbers(1, 20, count),
                Labels = RandomStrings(count)
            };
        }

        public BarChart<int> BarData_Test()
        {
            const int count = 20;
            return new BarChart<int>
            {
                Data = RandomIntNumbers(1, 20000, count),
                Labels = RandomStrings(count)
            };
        }

        public AreaChart<double> AreaData_Test()
        {
            const int count = 40;
            return new AreaChart<double>
            {
                Data = RandomDoubleNumbers(1, 40000, count),
                Labels = RandomStrings(count)
            };
        }

        public RadarChart<double> RadarData_Test()
        {
            const int count = 10;

            var radar = new List<DataSet<double>>();

            for (var i = 0; i < 3; i++)
            {
                radar.Add(new DataSet<double>
                {
                    Label = RandomStrings(1).FirstOrDefault(),
                    Data = RandomDoubleNumbers(1, 20, count)
                });
            }

            return new RadarChart<double>
            {
                Labels = RandomStrings(count),
                Data = radar
            };
        }

        public LineChart<double> LineData_Test()
        {
            const int count = 10;
            var line = new List<DataSet<double>>();

            for (var i = 0; i < 3; i++)
            {
                line.Add(new DataSet<double>
                {
                    Label = RandomStrings(1).FirstOrDefault(),
                    Data = RandomDoubleNumbers(1, 20, count)
                });
            }

            return new LineChart<double>
            {
                Labels = RandomStrings(count),
                Data = line
            };
        }

        public IEnumerable<BubbleChart<Bubble>> BubbleData_Test()
        {
            var result = new List<BubbleChart<Bubble>>();

            for (var i = 0; i < 5; i++)
            {
                result.Add(new BubbleChart<Bubble>
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

        private IEnumerable<int> RandomIntNumbers(int minimum, int maximum, int count)
        {
            var random = new Random();
            var result = new List<int>();

            for (var i = 0; i < count; i++)
            {
                result.Add(random.Next(minimum, maximum));
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

        private string HexConverter(Color c) => "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");

        //------------------------------------------------------------------------------------------------------------------------------------------

        #endregion
    }
}
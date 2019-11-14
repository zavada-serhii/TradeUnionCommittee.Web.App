using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Net;
using TradeUnionCommittee.DataAnalysis.Service.Exceptions;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 1
    /// </summary>
    public class ForecastingService : IForecastingService
    {
        private readonly DataAnalysisClient _client;

        public ForecastingService(DataAnalysisClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Task 1.1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<double>> CorrelationAnalysis(IEnumerable<Task11Model> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CorrelationAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<IEnumerable<double>>>(response.Content);
            }

            throw new ServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<Task13Model> CheckingSignificanceCoefficients(IEnumerable<Task11Model> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CheckingSignificanceCoefficients", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<Task13Model>>(response.Content);
            }

            throw new ServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.3
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countClusters"></param>
        /// <returns></returns>
        public ClusterModel ClusterAnalysis(IEnumerable<Task14Model> data, int countClusters)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/ClusterAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(data),
                CountCluster = countClusters
            });
            request.AddBody(json);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<ClusterModel>(response.Content);
            }

            throw new ServiceUnavailableException();
        }
    }

    public class Task11Model
    {
        public int Age { get; set; }
        public int TravelCount { get; set; }
        public int WellnessCount { get; set; }
        public int TourCount { get; set; }
    }

    public class Task13Model
    {
        public string FirstCriterion { get; set; }
        public string SecondCriterion { get; set; }
        public double TCriteria { get; set; }
        public double TStatistics { get; set; }
        public bool Differs { get; set; }
    }

    //------------------------------------------------------

    public class Task14Model
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ClusterModel
    {
        public IEnumerable<IEnumerable<double>> X { get; set; }
        public IEnumerable<IEnumerable<double>> Y { get; set; }
        public IEnumerable<IEnumerable<double>> Centers { get; set; }
    }
}
using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Net;
using TradeUnionCommittee.DataAnalysis.Service.Contracts;
using TradeUnionCommittee.DataAnalysis.Service.Exceptions;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.ViewModels;

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
        public IEnumerable<IEnumerable<double>> CorrelationAnalysis(IEnumerable<ForecastingCorrelationModel> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CorrelationAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddJsonBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<IEnumerable<double>>>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IEnumerable<ForecastingSignificanceViewModel> CheckingSignificanceCoefficients(IEnumerable<ForecastingCorrelationModel> data)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/CheckingSignificanceCoefficients", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddJsonBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<ForecastingSignificanceViewModel>>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 1.3
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countClusters"></param>
        /// <returns></returns>
        public ForecastingClusterViewModel ClusterAnalysis(IEnumerable<ForecastingClusterModel> data, int countClusters)
        {
            var request = new RestRequest("api/Forecasting/ActualingTrips/ClusterAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(data),
                CountCluster = countClusters
            });
            request.AddJsonBody(json);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<ForecastingClusterViewModel>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }
    }
}
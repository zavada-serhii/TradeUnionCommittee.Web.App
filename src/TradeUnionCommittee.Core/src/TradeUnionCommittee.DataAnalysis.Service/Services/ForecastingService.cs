using System.Text.Json;
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
        private readonly HttpClient _dataAnalysisClient;

        public ForecastingService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }

        /// <summary>
        /// Task 1.1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IEnumerable<double>>> CorrelationAnalysis(IEnumerable<ForecastingCorrelationModel> data)
        {
            var body = JsonSerializer.Serialize(ServiceStack.Text.CsvSerializer.SerializeToString(data));

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "api/Forecasting/ActualingTrips/CorrelationAnalysis"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<IEnumerable<IEnumerable<double>>>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        /// <summary>
        /// Task 1.2
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ForecastingSignificanceViewModel>> CheckingSignificanceCoefficients(IEnumerable<ForecastingCorrelationModel> data)
        {
            var body = JsonSerializer.Serialize(ServiceStack.Text.CsvSerializer.SerializeToString(data));

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "api/Forecasting/ActualingTrips/CheckingSignificanceCoefficients"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<IEnumerable<ForecastingSignificanceViewModel>>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        /// <summary>
        /// Task 1.3
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countClusters"></param>
        /// <returns></returns>
        public async Task<ForecastingClusterViewModel> ClusterAnalysis(IEnumerable<ForecastingClusterModel> data, int countClusters)
        {
            var body = JsonSerializer.Serialize(new 
            {
                Csv = ServiceStack.Text.CsvSerializer.SerializeToString(data),
                CountCluster = countClusters
            });

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "api/Forecasting/ActualingTrips/ClusterAnalysis"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ForecastingClusterViewModel>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }
    }
}
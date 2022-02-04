using System.Text.Json;
using TradeUnionCommittee.DataAnalysis.Service.Contracts;
using TradeUnionCommittee.DataAnalysis.Service.Exceptions;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.ViewModels;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 2 and 3
    /// </summary>
    public class DeterminingService : IDeterminingService
    {
        private readonly HttpClient _dataAnalysisClient;

        public DeterminingService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }

        /// <summary>
        /// Task 2.1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<double> MultiCorrelationCoefficient(IEnumerable<DeterminingMultiCorrelationModel> data)
        {
            var body = JsonSerializer.Serialize(ServiceStack.Text.CsvSerializer.SerializeToString(data));

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "/api/Determining/ProbablePastime/MultiCorrelationCoefficient"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<double>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        /// <summary>
        /// Task 2.4 - 2.6
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<DeterminingMultiFactorViewModel> MultiFactorModel(IEnumerable<DeterminingMultiFactorModel> data)
        {
            var body = JsonSerializer.Serialize(ServiceStack.Text.CsvSerializer.SerializeToString(data));

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "/api/Determining/ProbablePastime/MultiFactorModel"),
                Content = new StringContent(body)
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                return ServiceStack.Text.JsonSerializer.DeserializeFromString<DeterminingMultiFactorViewModel>(responseString);
            }
            else
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new AnalysisServiceUnavailableException(responseString);
            }
        }

        /// <summary>
        /// Task 2.7 - NEED FIX PYTHON API METHOD
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countComponents"></param>
        /// <returns></returns>
        public async Task<IEnumerable<IEnumerable<double>>> PrincipalComponentAnalysis(IEnumerable<DeterminingPrincipalComponentModel> data, int countComponents)
        {
            var body = JsonSerializer.Serialize(new
            {
                Csv = ServiceStack.Text.CsvSerializer.SerializeToString(data),
                CountComponents = countComponents
            });

            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "/api/Determining/ProbablePastime/PrincipalComponentAnalysis"),
                Content = new StringContent(body, System.Text.Encoding.UTF8)
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
    }
}
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
    /// Task 2 and 3
    /// </summary>
    public class DeterminingService : IDeterminingService
    {
        private readonly DataAnalysisClient _client;

        public DeterminingService(DataAnalysisClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Task 2.1
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public double MultiCorrelationCoefficient(IEnumerable<DeterminingMultiCorrelationModel> data)
        {
            var request = new RestRequest("/api/Determining/ProbablePastime/MultiCorrelationCoefficient", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<double>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 2.4 - 2.6
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DeterminingMultiFactorViewModel MultiFactorModel(IEnumerable<DeterminingMultiFactorModel> data)
        {
            var request = new RestRequest("/api/Determining/ProbablePastime/MultiFactorModel", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<DeterminingMultiFactorViewModel>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }

        /// <summary>
        /// Task 2.7
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countComponents"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<double>> PrincipalComponentAnalysis(IEnumerable<DeterminingPrincipalComponentModel> data, int countComponents)
        {
            var request = new RestRequest("/api/Determining/ProbablePastime/PrincipalComponentAnalysis", Method.POST) { RequestFormat = DataFormat.Json };
            var json = JsonSerializer.SerializeToString(new
            {
                Csv = CsvSerializer.SerializeToString(data),
                CountComponents = countComponents
            });
            request.AddBody(json);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<IEnumerable<IEnumerable<double>>>(response.Content);
            }

            throw new AnalysisServiceUnavailableException();
        }
    }
}
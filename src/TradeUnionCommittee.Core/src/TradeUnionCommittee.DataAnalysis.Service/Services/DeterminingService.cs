using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Net;
using TradeUnionCommittee.DataAnalysis.Service.Exceptions;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;

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
        public double MultiCorrelationCoefficient(IEnumerable<Task21Model> data)
        {
            var request = new RestRequest("/api/Determining/ProbablePastime/MultiCorrelationCoefficient", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<double>(response.Content);
            }

            throw new ServiceUnavailableException();
        }

        /// <summary>
        /// Task 2.4 - 2.6
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MultiFactorModel MultiFactorModel(IEnumerable<Task24Model> data)
        {
            var request = new RestRequest("/api/Determining/ProbablePastime/MultiFactorModel", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(data);
            request.AddBody(csv);

            var response = _client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonSerializer.DeserializeFromString<MultiFactorModel>(response.Content);
            }

            throw new ServiceUnavailableException();
        }

        /// <summary>
        /// Task 2.7
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countComponents"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<double>> PrincipalComponentAnalysis(IEnumerable<Task27Model> data, int countComponents)
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

            throw new ServiceUnavailableException();
        }
    }

    public class Task21Model
    {
        public int Y { get; set; }
        public int X1 { get; set; }
        public int X2 { get; set; }
    }

    //------------------------------------------------------

    public class Task24Model : Task21Model
    {
        public int X3 { get; set; }
        public int X4 { get; set; }
        public int X5 { get; set; }
        public int X6 { get; set; }
    }

    public class MultiFactorModel
    {
        public IEnumerable<double> RegressionModel { get; set; }
        public IEnumerable<double> Standardization { get; set; }
        public IEnumerable<double> SignificanceTest { get; set; }
        public IEnumerable<double> ConfidenceInterval { get; set; }
    }

    //------------------------------------------------------

    public class Task27Model
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int X3 { get; set; }
        public int X4 { get; set; }
        public int X5 { get; set; }
    }
}
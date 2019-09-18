using RestSharp;
using ServiceStack.Text;
using System.Collections.Generic;
using System.Net;
using TradeUnionCommittee.DataAnalysis.Service.Interfaces;
using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    public class TestService : ITestService
    {
        private readonly DataAnalysisClient _client;

        public TestService(DataAnalysisClient client)
        {
            _client = client;
        }

        public bool HealthCheck()
        {
            var request = new RestRequest("api/home/healtcheck", Method.GET);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IEnumerable<TestModel> TestPostJson()
        {
            var request = new RestRequest("api/home/postjson", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddBody(GetTestData);

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK 
                ? JsonSerializer.DeserializeFromString<List<TestModel>>(response.Content)
                : null;
        }

        public string TestPostCsv()
        {
            var request = new RestRequest("api/home/postcsv", Method.POST) { RequestFormat = DataFormat.Json };
            var csv = CsvSerializer.SerializeToString(GetTestData);
            request.AddBody(csv);

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK
                ? response.Content
                : null;
        }

        private IEnumerable<TestModel> GetTestData => new List<TestModel>
        {
            new TestModel
            {
                Id = 1,
                FullName = "Gabriel Logan",
                Email = "gabriel.logan@test.com"
            },
            new TestModel
            {
                Id = 2,
                FullName = "Annabelle Stafford",
                Email = "annabelle.stafford@test.com"
            },
            new TestModel
            {
                Id = 35,
                FullName = "Joseph Rogers",
                Email = "joseph.rogers@test.com"
            },
            new TestModel
            {
                Id = 48,
                FullName = "Everett Wood",
                Email = "everett.wood@test.com"
            },
            new TestModel
            {
                Id = 57,
                FullName = "Vivien Jordan",
                Email = "everett.wood@test.com"
            }
        };
    }
}
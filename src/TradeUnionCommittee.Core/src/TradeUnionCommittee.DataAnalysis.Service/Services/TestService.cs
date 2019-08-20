using Newtonsoft.Json;
using RestSharp;
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
            var request = new RestRequest("api/test/healtcheck", Method.GET);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }

        public IEnumerable<TestModel> TestPost()
        {
            var request = new RestRequest("api/test/post", Method.POST) {RequestFormat = DataFormat.Json};
            request.AddBody(new List<TestModel>
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
                }
            });

            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK 
                ? JsonConvert.DeserializeObject<List<TestModel>>(response.Content) 
                : null;
        }
    }
}
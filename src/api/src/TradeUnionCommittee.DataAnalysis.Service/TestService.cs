using System.Net;
using RestSharp;

namespace TradeUnionCommittee.DataAnalysis.Service
{
    public interface ITestService
    {
        bool HealthCheck();
    }

    public class TestService : ITestService
    {
        private readonly AnalysisClient _client;

        public TestService(AnalysisClient client)
        {
            _client = client;
        }

        public bool HealthCheck()
        {
            var request = new RestRequest("api/analytical/healtcheck", Method.GET);
            var response = _client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}

using TradeUnionCommittee.DataAnalysis.Service.Contracts;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    public class HomeService : IHomeService
    {
        private readonly HttpClient _dataAnalysisClient;

        public HomeService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }

        public async Task<bool> HealthCheck()
        {
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_dataAnalysisClient.BaseAddress, "/health/live"),
            };

            HttpResponseMessage response = await _dataAnalysisClient.SendAsync(requestMessage, CancellationToken.None);

            return response.IsSuccessStatusCode && (await response.Content.ReadAsStringAsync()).ToUpper() == "HEALTHY";
        }
    }
}
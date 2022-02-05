using TradeUnionCommittee.DataAnalysis.Service.Contracts;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 5
    /// </summary>
    public class CheckingService : ICheckingService
    {
        private readonly HttpClient _dataAnalysisClient;

        public CheckingService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }
    }
}
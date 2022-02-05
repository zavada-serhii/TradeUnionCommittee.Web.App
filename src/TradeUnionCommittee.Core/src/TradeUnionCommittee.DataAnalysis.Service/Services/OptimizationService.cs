using TradeUnionCommittee.DataAnalysis.Service.Contracts;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 4
    /// </summary>
    public class OptimizationService : IOptimizationService
    {
        private readonly HttpClient _dataAnalysisClient;

        public OptimizationService(IHttpClientFactory clientFactory)
        {
            _dataAnalysisClient = clientFactory.CreateClient("DataAnalysis");
        }
    }
}
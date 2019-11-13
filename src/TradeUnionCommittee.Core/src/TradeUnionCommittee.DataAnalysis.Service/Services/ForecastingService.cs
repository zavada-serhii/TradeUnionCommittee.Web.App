using TradeUnionCommittee.DataAnalysis.Service.Interfaces;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 1
    /// </summary>
    public class ForecastingService : IForecastingService
    {
        private readonly DataAnalysisClient _client;

        public ForecastingService(DataAnalysisClient client)
        {
            _client = client;
        }
    }
}
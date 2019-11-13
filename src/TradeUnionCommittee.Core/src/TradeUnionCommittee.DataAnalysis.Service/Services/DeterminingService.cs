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
    }
}
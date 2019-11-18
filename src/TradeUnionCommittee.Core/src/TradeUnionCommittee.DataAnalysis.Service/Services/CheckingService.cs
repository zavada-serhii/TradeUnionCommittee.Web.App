using TradeUnionCommittee.DataAnalysis.Service.Interfaces;

namespace TradeUnionCommittee.DataAnalysis.Service.Services
{
    /// <summary>
    /// Task 5
    /// </summary>
    public class CheckingService : ICheckingService
    {
        private readonly DataAnalysisClient _client;

        public CheckingService(DataAnalysisClient client)
        {
            _client = client;
        }
    }
}
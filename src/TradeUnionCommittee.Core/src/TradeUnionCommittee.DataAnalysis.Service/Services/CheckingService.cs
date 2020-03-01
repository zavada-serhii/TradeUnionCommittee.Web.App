using TradeUnionCommittee.DataAnalysis.Service.Contracts;

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
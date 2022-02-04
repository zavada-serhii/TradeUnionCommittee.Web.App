using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Contracts
{
    public interface ITestService
    {
        Task<IEnumerable<TestResponseModel>> PostJson();
        Task<string> PostCsv();
    }
}
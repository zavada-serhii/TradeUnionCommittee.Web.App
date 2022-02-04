namespace TradeUnionCommittee.DataAnalysis.Service.Contracts
{
    public interface IHomeService
    {
        Task<bool> HealthCheck();
    }
}
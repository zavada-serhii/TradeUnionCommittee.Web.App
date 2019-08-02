using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Interfaces
{
    public interface ITestService
    {
        bool HealthCheck();
        IEnumerable<TestModel> TestPost();
    }
}
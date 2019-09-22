using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Models;

namespace TradeUnionCommittee.DataAnalysis.Service.Interfaces
{
    public interface ITestService
    {
        bool HealthCheck();
        IEnumerable<TestModel> TestPostJson();
        string TestPostCsv();

        Dictionary<string, bool> TestTask_1();
        Dictionary<string, bool> TestTask_2();
        Dictionary<string, bool> TestTasks_3_4_5();
    }
}
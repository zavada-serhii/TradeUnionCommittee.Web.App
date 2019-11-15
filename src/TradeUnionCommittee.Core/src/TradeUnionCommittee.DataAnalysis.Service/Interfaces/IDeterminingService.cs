using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Services;

namespace TradeUnionCommittee.DataAnalysis.Service.Interfaces
{
    /// <summary>
    /// Task 2 and 3
    /// </summary>
    public interface IDeterminingService
    {
        double MultiCorrelationCoefficient(IEnumerable<Task21Model> data);
        MultiFactorModel MultiFactorModel(IEnumerable<Task22Model> data);
        IEnumerable<IEnumerable<double>> PrincipalComponentAnalysis(IEnumerable<Task25Model> data, int countComponents);
    }
}
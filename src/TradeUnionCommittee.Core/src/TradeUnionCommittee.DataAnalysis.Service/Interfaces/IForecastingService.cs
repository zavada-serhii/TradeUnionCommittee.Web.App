using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Services;

namespace TradeUnionCommittee.DataAnalysis.Service.Interfaces
{
    /// <summary>
    /// Task 1
    /// </summary>
    public interface IForecastingService
    {
        IEnumerable<IEnumerable<double>> CorrelationAnalysis(IEnumerable<Task11Model> data);
        IEnumerable<Task13Model> CheckingSignificanceCoefficients(IEnumerable<Task11Model> data);
    }
}
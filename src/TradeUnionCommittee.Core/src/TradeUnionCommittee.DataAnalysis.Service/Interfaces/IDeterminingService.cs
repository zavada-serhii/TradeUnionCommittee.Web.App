using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.ViewModels;

namespace TradeUnionCommittee.DataAnalysis.Service.Interfaces
{
    /// <summary>
    /// Task 2 and 3
    /// </summary>
    public interface IDeterminingService
    {
        double MultiCorrelationCoefficient(IEnumerable<DeterminingMultiCorrelationModel> data);
        DeterminingMultiFactorViewModel MultiFactorModel(IEnumerable<DeterminingMultiFactorModel> data);
        IEnumerable<IEnumerable<double>> PrincipalComponentAnalysis(IEnumerable<DeterminingPrincipalComponentModel> data, int countComponents);
    }
}
using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.ViewModels;

namespace TradeUnionCommittee.DataAnalysis.Service.Contracts
{
    /// <summary>
    /// Task 2 and 3
    /// </summary>
    public interface IDeterminingService
    {
        Task<double> MultiCorrelationCoefficient(IEnumerable<DeterminingMultiCorrelationModel> data);
        Task<DeterminingMultiFactorViewModel> MultiFactorModel(IEnumerable<DeterminingMultiFactorModel> data);
        Task<IEnumerable<IEnumerable<double>>> PrincipalComponentAnalysis(IEnumerable<DeterminingPrincipalComponentModel> data, int countComponents);
    }
}
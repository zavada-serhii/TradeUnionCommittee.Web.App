using System.Collections.Generic;
using TradeUnionCommittee.DataAnalysis.Service.Models;
using TradeUnionCommittee.DataAnalysis.Service.ViewModels;

namespace TradeUnionCommittee.DataAnalysis.Service.Contracts
{
    /// <summary>
    /// Task 1
    /// </summary>
    public interface IForecastingService
    {
        Task<IEnumerable<IEnumerable<double>>> CorrelationAnalysis(IEnumerable<ForecastingCorrelationModel> data);
        Task<IEnumerable<ForecastingSignificanceViewModel>> CheckingSignificanceCoefficients(IEnumerable<ForecastingCorrelationModel> data);
        Task<ForecastingClusterViewModel> ClusterAnalysis(IEnumerable<ForecastingClusterModel> data, int countClusters);
    }
}
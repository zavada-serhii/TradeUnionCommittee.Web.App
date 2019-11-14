using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        Task<IEnumerable<IEnumerable<double>>> CorrelationAnalysis();
        Task<BasicColumn> CheckingSignificanceCoefficients();
        Task<IEnumerable<BubbleResult>> ClusterAnalysis(TypeEvents type);
        Task<BarResult> GetEmployeeAgeGroup();

        PieResult PieData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        Task<ChartResult<IEnumerable<IEnumerable<double>>>> CorrelationAnalysis();
        Task<ChartResult<BasicColumn>> CheckingSignificanceCoefficients();
        Task<ChartResult<IEnumerable<BubbleResult>>> ClusterAnalysis(TypeEvents type);
        Task<ChartResult<BarResult>> GetEmployeeAgeGroup();

        PieResult PieData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        Task<IEnumerable<IEnumerable<double>>> CorrelationAnalysis();
        Task<BasicColumn> CheckingSignificanceCoefficients();

        PieResult PieData_Test();
        BarResult BarData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
        IEnumerable<BubbleResult> BubbleData_Test();
    }
}
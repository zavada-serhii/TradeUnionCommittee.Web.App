using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        Task<ChartResult<IEnumerable<IEnumerable<double>>>> CorrelationAnalysisBetweenTeacherAgeAndTypeOfEvent();
        Task<ChartResult<BasicColumnChart>> CheckingSignificanceAgeTeacherAndTypeOfEvent();
        Task<ChartResult<IEnumerable<BubbleChart>>> ClusterAnalysisAgeTeacherAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<BarChart<int>>> GetEmployeeAgeGroup();

        Task<ChartResult<BarChart<double>>> MultiCorrelationBetweenTypeOfEventAndDependents(TypeEvents type);
        Task<ChartResult<IEnumerable<BubbleChart>>> ClusterAnalysisSignHavingChildrenAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<PieChart<int>>> GetPercentageRatioHavingDependents();

        PieChart<double> PieData_Test();
        BarChart<int> BarData_Test();
        AreaChart<double> AreaData_Test();
        RadarChart<double> RadarData_Test();
        LineChart<double> LineData_Test();
        IEnumerable<BubbleChart> BubbleData_Test();
    }
}
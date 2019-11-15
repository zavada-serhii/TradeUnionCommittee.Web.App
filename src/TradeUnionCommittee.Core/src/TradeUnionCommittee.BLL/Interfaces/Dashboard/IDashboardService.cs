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
        Task<ChartResult<BasicColumn>> CheckingSignificanceAgeTeacherAndTypeOfEvent();
        Task<ChartResult<IEnumerable<BubbleResult>>> ClusterAnalysisAgeTeacherAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<BarResultInt>> GetEmployeeAgeGroup();

        Task<ChartResult<BarResultDouble>> MultiCorrelationBetweenTypeOfEventAndDependents(TypeEvents type);
        Task<double> RegressionModelInfluenceDependentsAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<IEnumerable<IEnumerable<double>>>> ReducedAnalysisDataDependentsAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<IEnumerable<BubbleResult>>> ClusterAnalysisSignHavingChildrenAndTypeOfEvent(TypeEvents type);
        Task<ChartResult<PieResultInt>> GetPercentageRatioHavingDependents();

        PieResultDouble PieData_Test();
        BarResultInt BarData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
        IEnumerable<BubbleResult> BubbleData_Test();
    }
}
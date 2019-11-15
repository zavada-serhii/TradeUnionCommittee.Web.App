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
        Task<ChartResult<BarResult>> GetEmployeeAgeGroup();

        Task MultiCorrelationBetweenTypeOfEventAndDependents(TypeEvents type);
        Task RegressionModelInfluenceDependentsAndTypeOfEvent(TypeEvents type);
        Task ReducedAnalysisDataDependentsAndTypeOfEvent(TypeEvents type);
        Task ClusterAnalysisSignHavingChildrenAndTypeOfEvent(TypeEvents type);
        Task GetPercentageRatioHavingDependents();

        PieResult PieData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
    }
}
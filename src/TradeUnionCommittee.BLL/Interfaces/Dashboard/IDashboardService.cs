using System;
using System.Collections.Generic;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        PieResult PieData_Test();
        BarResult BarData_Test();
        AreaResult AreaData_Test();
        RadarResult RadarData_Test();
        LineResult LineData_Test();
        IEnumerable<BubbleResult> BubbleData_Test();
    }
}
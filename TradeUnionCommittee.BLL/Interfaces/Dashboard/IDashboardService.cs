using System;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Dashboard
{
    public interface IDashboardService : IDisposable
    {
        PieResult PieData_Test();
    }
}
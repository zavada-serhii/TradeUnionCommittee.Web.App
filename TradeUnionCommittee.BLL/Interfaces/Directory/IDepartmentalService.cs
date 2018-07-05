using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IDepartmentalService : IService<DepartmentalDTO>
    {
        Task<ActualResult<Dictionary<long, string>>> GetAllShortcut();
    }
}
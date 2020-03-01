using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Contracts.Directory
{
    public interface IDepartmentalService : IService<DepartmentalDTO>, IDirectory<DepartmentalDTO>
    {
        Task<ActualResult<Dictionary<string, string>>> GetAllShortcut();
    }
}
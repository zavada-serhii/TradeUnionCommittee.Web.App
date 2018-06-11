using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IPositionService : IService<DirectoryDTO>
    {
        Task<ActualResult<bool>> CheckName(string name);
    }
}
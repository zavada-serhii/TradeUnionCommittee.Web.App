using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Common.ActualResults;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IPositionService : IService<DirectoryDTO>//, IDirectoryService
    {
        Task<ActualResult<DirectoryDTO>> GetAsync(string hashId);
        Task<ActualResult> DeleteAsync(string hashId);
        Task<bool> CheckNameAsync(string name);
    }
}
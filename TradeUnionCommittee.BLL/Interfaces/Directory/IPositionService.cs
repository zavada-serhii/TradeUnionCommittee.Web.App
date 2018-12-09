using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IPositionService : IService<DirectoryDTO>, IDirectory<DirectoryDTO>, ICheckName
    {
        
    }
}
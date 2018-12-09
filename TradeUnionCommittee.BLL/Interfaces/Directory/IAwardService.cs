using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IAwardService : IService<DirectoryDTO>, IDirectory<DirectoryDTO>, ICheckName
    {
    }
}
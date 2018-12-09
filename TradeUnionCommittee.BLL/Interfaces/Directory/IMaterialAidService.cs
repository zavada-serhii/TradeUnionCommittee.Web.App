using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IMaterialAidService : IService<DirectoryDTO>, IDirectory<DirectoryDTO>, ICheckName
    {
    }
}
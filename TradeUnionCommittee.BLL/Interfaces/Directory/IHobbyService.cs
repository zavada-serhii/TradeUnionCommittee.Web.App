using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface IHobbyService : IService<DirectoryDTO>, IDirectory<DirectoryDTO>, ICheckName
    {
    }
}
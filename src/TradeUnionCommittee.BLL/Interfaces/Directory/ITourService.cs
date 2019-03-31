using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Directory
{
    public interface ITourService : IService<TourDTO>, IDirectory<TourDTO>, ICheckName
    {
    }
}

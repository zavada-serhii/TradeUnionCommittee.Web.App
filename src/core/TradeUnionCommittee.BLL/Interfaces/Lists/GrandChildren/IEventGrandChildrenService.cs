using TradeUnionCommittee.BLL.DTO.GrandChildren;

namespace TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren
{
    public interface ITravelGrandChildrenService : IList<TravelGrandChildrenDTO>, IService<TravelGrandChildrenDTO>
    {
    }

    public interface ITourGrandChildrenService : IList<TourGrandChildrenDTO>, IService<TourGrandChildrenDTO>
    {
    }
}
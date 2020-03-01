using TradeUnionCommittee.BLL.DTO.GrandChildren;

namespace TradeUnionCommittee.BLL.Contracts.Lists.GrandChildren
{
    public interface ITravelGrandChildrenService : IList<TravelGrandChildrenDTO>, IService<TravelGrandChildrenDTO>
    {
    }

    public interface ITourGrandChildrenService : IList<TourGrandChildrenDTO>, IService<TourGrandChildrenDTO>
    {
    }
}
using TradeUnionCommittee.BLL.DTO.Children;

namespace TradeUnionCommittee.BLL.Contracts.Lists.Children
{
    public interface ITravelChildrenService : IList<TravelChildrenDTO>, IService<TravelChildrenDTO>
    {
    }

    public interface IWellnessChildrenService : IList<WellnessChildrenDTO>, IService<WellnessChildrenDTO>
    {
    }

    public interface ITourChildrenService : IList<TourChildrenDTO>, IService<TourChildrenDTO>
    {
    }
}
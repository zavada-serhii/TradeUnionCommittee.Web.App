using TradeUnionCommittee.BLL.DTO.Children;

namespace TradeUnionCommittee.BLL.Interfaces.Lists.Children
{
    public interface ITravelChildrenService : IList<TravelChildrenDTO>, IService<TravelChildrenDTO>, IHashIdEmployee
    {
    }

    public interface IWellnessChildrenService : IList<WellnessChildrenDTO>, IService<WellnessChildrenDTO>, IHashIdEmployee
    {
    }

    public interface ITourChildrenService : IList<TourChildrenDTO>, IService<TourChildrenDTO>, IHashIdEmployee
    {
    }
}
using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface ITravelFamilyService : IList<TravelFamilyDTO>, IService<TravelFamilyDTO>, IHashIdEmployee { }
    public interface IWellnessFamilyService : IList<WellnessFamilyDTO>, IService<WellnessFamilyDTO>, IHashIdEmployee { }
    public interface ITourFamilyService : IList<TourFamilyDTO>, IService<TourFamilyDTO>, IHashIdEmployee { }
}
using TradeUnionCommittee.BLL.DTO.Family;

namespace TradeUnionCommittee.BLL.Interfaces.Lists.Family
{
    public interface ITravelFamilyService : IList<TravelFamilyDTO>, IService<TravelFamilyDTO>, IHashIdEmployee { }
    public interface IWellnessFamilyService : IList<WellnessFamilyDTO>, IService<WellnessFamilyDTO>, IHashIdEmployee { }
    public interface ITourFamilyService : IList<TourFamilyDTO>, IService<TourFamilyDTO>, IHashIdEmployee { }
}
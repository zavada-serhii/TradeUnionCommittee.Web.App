using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface ITravelFamilyService : IList<TravelFamilyDTO>, IService<TravelFamilyDTO> { }
    public interface IWellnessFamilyService : IList<WellnessFamilyDTO>, IService<WellnessFamilyDTO> { }
    public interface ITourFamilyService : IList<TourFamilyDTO>, IService<TourFamilyDTO> { }
}
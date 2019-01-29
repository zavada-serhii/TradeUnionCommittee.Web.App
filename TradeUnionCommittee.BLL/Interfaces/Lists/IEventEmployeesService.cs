using TradeUnionCommittee.BLL.DTO;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface ITravelEmployeesService : IList<TravelEmployeesDTO>, IService<TravelEmployeesDTO> { }
    public interface IWellnessEmployeesService : IList<WellnessEmployeesDTO>, IService<WellnessEmployeesDTO> { }
    public interface ITourEmployeesService : IList<TourEmployeesDTO>, IService<TourEmployeesDTO> { }
}
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;

namespace TradeUnionCommittee.BLL.Interfaces.Lists
{
    public interface ITravelEmployeesService : IList<TravelEmployeesDTO>, IService<TravelEmployeesDTO> { }
    public interface IWellnessEmployeesService : IList<WellnessEmployeesDTO>, IService<WellnessEmployeesDTO> { }
    public interface ITourEmployeesService : IList<TourEmployeesDTO>, IService<TourEmployeesDTO> { }
}
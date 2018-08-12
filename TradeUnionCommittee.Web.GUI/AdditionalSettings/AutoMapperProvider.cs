using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.AdditionalSettings
{
    public class AutoMapperProvider
    {
        /// <summary>
        ///     Configures the automatic mapper.
        /// </summary>
        /// <returns>IMapper.</returns>
        public static IMapper ConfigureAutoMapper()
        {
            return new MapperConfiguration(map =>
            {
                map.CreateMap<AccountDTO, CreateAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdatePersonalDataAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdatePasswordAccountViewModel>().ReverseMap();

                map.CreateMap<AddEmployeeViewModel, AddEmployeeDTO>()
                .ForMember("IdSubdivision",opt => opt.MapFrom(c => c.SubordinateSubdivision ?? c.MainSubdivision))
                .ForMember("CityPhone", opt => opt.MapFrom(c => c.CityPhoneAdditional ?? c.CityPhone));

                map.CreateMap<DirectoryDTO, PositionViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, SocialActivityViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, PrivilegesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, AwardViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, MaterialAidViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, HobbyViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, TravelViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, WellnessViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, TourViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, ActivitiesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, CulturalViewModel>().ReverseMap();
                map.CreateMap<DepartmentalDTO, DepartmentalViewModel>().ReverseMap();
                map.CreateMap<DormitoryDTO, DormitoryViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, SubdivisionViewModel>().ForMember("Name", c => c.MapFrom(x => x.DeptName)).ReverseMap();


                map.CreateMap<MainInfoEmployeeDTO, MainInfoEmployeeViewModel>().ReverseMap();

            }).CreateMapper();
        }
    }
}
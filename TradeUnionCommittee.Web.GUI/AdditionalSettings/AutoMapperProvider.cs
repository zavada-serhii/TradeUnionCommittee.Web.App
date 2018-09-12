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
                //-- Controller Mapper ---------------------------------------------------------------------------------------------------------

                map.CreateMap<AccountDTO, CreateAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateEmailAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateRoleAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdatePasswordAccountViewModel>().ReverseMap();

                map.CreateMap<CreateEmployeeViewModel, CreateEmployeeDTO>()
                .ForMember(d => d.HashIdSubdivision, opt => opt.MapFrom(c => c.HashIdSubordinateSubdivision ?? c.HashIdMainSubdivision))
                .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhoneAdditional ?? c.CityPhone));

                map.CreateMap<DirectoryDTO, PositionViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, SocialActivityViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, PrivilegesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, AwardViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, MaterialAidViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, HobbyViewModel>().ReverseMap();
                map.CreateMap<TravelDTO, TravelViewModel>().ReverseMap();
                map.CreateMap<WellnessDTO, WellnessViewModel>().ReverseMap();
                map.CreateMap<TourDTO, TourViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, ActivitiesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, CulturalViewModel>().ReverseMap();
                map.CreateMap<DepartmentalDTO, DepartmentalViewModel>().ReverseMap();
                map.CreateMap<DormitoryDTO, DormitoryViewModel>().ReverseMap();

                map.CreateMap<SubdivisionDTO, CreateMainSubdivisionViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, UpdateNameSubdivisionViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, UpdateAbbreviationSubdivisionViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, CreateSubordinateSubdivisionViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, RestructuringViewModel>().ReverseMap();

                map.CreateMap<EducationDTO, EducationViewModel>().ReverseMap();
                map.CreateMap<QualificationDTO, QualificationViewModel>().ReverseMap();

                map.CreateMap<GeneralInfoEmployeeDTO, UpdateEmployeeViewModel>().ReverseMap();

            }).CreateMapper();
        }
    }
}
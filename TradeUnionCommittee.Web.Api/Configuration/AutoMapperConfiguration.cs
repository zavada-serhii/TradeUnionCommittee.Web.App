using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Web.Api.Configuration
{
    public class AutoMapperConfiguration
    {
        /// <summary>
        ///     Configures the automatic mapper.
        /// </summary>
        /// <returns>IMapper.</returns>
        public static IMapper ConfigureAutoMapper()
        {
            string nullVarible = null;

            return new MapperConfiguration(map =>
            {
                //-- Controller Mapper ---------------------------------------------------------------------------------------------------------

                map.CreateMap<AccountDTO, CreateAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateEmailAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateRoleAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdatePasswordAccountViewModel>().ReverseMap();

                map.CreateMap<CreateEmployeeViewModel, CreateEmployeeDTO>()
                    .ForMember(d => d.HashIdSubdivision, opt => opt.MapFrom(c => c.HashIdSubordinateSubdivision ?? c.HashIdMainSubdivision))
                    .ForMember(d => d.TypeAccommodation, opt => opt.MapFrom(x => ConverterAccommodation(x.TypeAccommodation)));

                map.CreateMap<GeneralInfoEmployeeDTO, UpdateEmployeeViewModel>()
                    .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhone.Replace("-", string.Empty)))
                    .ReverseMap();

                // -- Start Mapping for Directory 

                map.CreateMap<CreatePositionViewModel, DirectoryDTO>();
                map.CreateMap<CreateSocialActivityViewModel, DirectoryDTO>();
                map.CreateMap<CreatePrivilegesViewModel, DirectoryDTO>();
                map.CreateMap<CreateAwardViewModel, DirectoryDTO>();
                map.CreateMap<CreateMaterialAidViewModel, DirectoryDTO>();
                map.CreateMap<CreateHobbyViewModel, DirectoryDTO>();
                map.CreateMap<CreateTravelViewModel, TravelDTO>();
                map.CreateMap<CreateWellnessViewModel, WellnessDTO>();
                map.CreateMap<CreateTourViewModel, TourDTO>();
                map.CreateMap<CreateActivitiesViewModel, DirectoryDTO>();
                map.CreateMap<CreateCulturalViewModel, DirectoryDTO>();
                map.CreateMap<DirectoryDTO, UpdatePositionViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateSocialActivityViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdatePrivilegesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateAwardViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateMaterialAidViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateHobbyViewModel>().ReverseMap();
                map.CreateMap<TravelDTO, UpdateTravelViewModel>().ReverseMap();
                map.CreateMap<WellnessDTO, UpdateWellnessViewModel>().ReverseMap();
                map.CreateMap<TourDTO, UpdateTourViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateActivitiesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, UpdateCulturalViewModel>().ReverseMap();

                map.CreateMap<CreateDepartmentalViewModel, DepartmentalDTO>();
                map.CreateMap<CreateDormitoryViewModel, DormitoryDTO>();
                map.CreateMap<DepartmentalDTO, UpdateDepartmentalViewModel>().ReverseMap();
                map.CreateMap<DormitoryDTO, UpdateDormitoryViewModel>().ReverseMap();

                map.CreateMap<CreateMainSubdivisionViewModel, SubdivisionDTO>();
                map.CreateMap<CreateSubordinateSubdivisionViewModel, SubdivisionDTO>()
                    .ForMember(d => d.HashIdMain, opt => opt.MapFrom(x => nullVarible))
                    .ForMember(d => d.HashIdSubordinate, opt => opt.MapFrom(x => x.HashIdMain));
                map.CreateMap<RestructuringViewModel, SubdivisionDTO>();
                map.CreateMap<SubdivisionDTO, UpdateNameSubdivisionViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, UpdateAbbreviationSubdivisionViewModel>().ReverseMap();

                // -- End Mapping for Directory 

            }).CreateMapper();
        }

        private static AccommodationType ConverterAccommodation(string accommodationType)
        {
            switch (accommodationType)
            {
                case "privateHouse":
                    return AccommodationType.PrivateHouse;

                case "fromUniversity":
                    return AccommodationType.FromUniversity;

                case "dormitory":
                    return AccommodationType.Dormitory;

                case "departmental":
                    return AccommodationType.Departmental;

                default:
                    return 0;
            }
        }
    }
}
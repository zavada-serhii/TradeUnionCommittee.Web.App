using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.ViewModels.ViewModels.Employee;

namespace TradeUnionCommittee.Api.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            //-- Controller Mapper ---------------------------------------------------------------------------------------------------------

            CreateMap<CreateEmployeeViewModel, CreateEmployeeDTO>()
                .ForMember(d => d.HashIdSubdivision, opt => opt.MapFrom(c => c.HashIdSubordinateSubdivision ?? c.HashIdMainSubdivision))
                .ForMember(d => d.TypeAccommodation, opt => opt.MapFrom(x => AutoMapperHelper.ConverterAccommodation(x.TypeAccommodation)));

            CreateMap<GeneralInfoEmployeeDTO, UpdateEmployeeViewModel>()
                .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhone.Replace("-", string.Empty)))
                .ReverseMap();

            // -- Start Mapping for Directory 

            CreateMap<CreatePositionViewModel, DirectoryDTO>();
            CreateMap<CreateSocialActivityViewModel, DirectoryDTO>();
            CreateMap<CreatePrivilegesViewModel, DirectoryDTO>();
            CreateMap<CreateAwardViewModel, DirectoryDTO>();
            CreateMap<CreateMaterialAidViewModel, DirectoryDTO>();
            CreateMap<CreateHobbyViewModel, DirectoryDTO>();
            CreateMap<CreateTravelViewModel, TravelDTO>();
            CreateMap<CreateWellnessViewModel, WellnessDTO>();
            CreateMap<CreateTourViewModel, TourDTO>();
            CreateMap<CreateActivitiesViewModel, DirectoryDTO>();
            CreateMap<CreateCulturalViewModel, DirectoryDTO>();
            CreateMap<DirectoryDTO, UpdatePositionViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateSocialActivityViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdatePrivilegesViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateAwardViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateMaterialAidViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateHobbyViewModel>().ReverseMap();
            CreateMap<TravelDTO, UpdateTravelViewModel>().ReverseMap();
            CreateMap<WellnessDTO, UpdateWellnessViewModel>().ReverseMap();
            CreateMap<TourDTO, UpdateTourViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateActivitiesViewModel>().ReverseMap();
            CreateMap<DirectoryDTO, UpdateCulturalViewModel>().ReverseMap();

            CreateMap<CreateDepartmentalViewModel, DepartmentalDTO>();
            CreateMap<CreateDormitoryViewModel, DormitoryDTO>();
            CreateMap<DepartmentalDTO, UpdateDepartmentalViewModel>().ReverseMap();
            CreateMap<DormitoryDTO, UpdateDormitoryViewModel>().ReverseMap();

            CreateMap<CreateMainSubdivisionViewModel, CreateSubdivisionDTO>();
            CreateMap<CreateSubordinateSubdivisionViewModel, CreateSubordinateSubdivisionDTO>();
            CreateMap<SubdivisionDTO, UpdateNameSubdivisionViewModel>();
            CreateMap<SubdivisionDTO, UpdateAbbreviationSubdivisionViewModel>();
            CreateMap<UpdateSubdivisionNameDTO, UpdateNameSubdivisionViewModel>().ReverseMap();
            CreateMap<UpdateSubdivisionAbbreviationDTO, UpdateAbbreviationSubdivisionViewModel>().ReverseMap();
            CreateMap<RestructuringViewModel, RestructuringSubdivisionDTO>().ForMember(d => d.RowVersion, opt => opt.MapFrom(x => x.RowVersionSubordinateSubdivision));

            // -- End Mapping for Directory 
        }
    }

    internal sealed class AutoMapperHelper
    {
        public static AccommodationType ConverterAccommodation(string accommodationType)
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
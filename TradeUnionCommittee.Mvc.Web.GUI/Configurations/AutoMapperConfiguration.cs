using System;
using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Configurations
{
    public class AutoMapperConfiguration
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

                map.CreateMap<CreateMainSubdivisionViewModel, CreateSubdivisionDTO>();
                map.CreateMap<CreateSubordinateSubdivisionViewModel, CreateSubordinateSubdivisionDTO>();
                map.CreateMap<UpdateSubdivisionNameDTO, UpdateNameSubdivisionViewModel>().ReverseMap();
                map.CreateMap<UpdateSubdivisionAbbreviationDTO, UpdateAbbreviationSubdivisionViewModel>().ReverseMap();
                map.CreateMap<RestructuringViewModel, RestructuringSubdivisionDTO>()
                    .ForMember(d => d.HashIdSubordinate, opt => opt.MapFrom(x => GetHashId(x.HashIdSubordinate)))
                    .ForMember(d => d.RowVersion, opt => opt.MapFrom(x => GetRowVersion(x.HashIdSubordinate)));

                // -- End Mapping for Directory 


                // -- Start mapping for Report

                map.CreateMap<PdfReportViewModel, ReportPdfDTO>();

                // -- End Mapping for Report

                map.CreateMap<CreatePrivateHouseEmployeesViewModel, PrivateHouseEmployeesDTO>();
                map.CreateMap<UpdatePrivateHouseEmployeesViewModel, PrivateHouseEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateUniversityHouseEmployeesViewModel, PrivateHouseEmployeesDTO>();
                map.CreateMap<UpdateUniversityHouseEmployeesViewModel, PrivateHouseEmployeesDTO>().ReverseMap();

                map.CreateMap<CreatePublicHouseEmployeesViewModel, PublicHouseEmployeesDTO>();
                map.CreateMap<UpdatePublicHouseEmployeesViewModel, PublicHouseEmployeesDTO>().ReverseMap();

                map.CreateMap<UpdatePositionEmployeesViewModel, PositionEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateSocialActivityEmployeesViewModel, SocialActivityEmployeesDTO>();
                map.CreateMap<UpdateSocialActivityEmployeesViewModel, SocialActivityEmployeesDTO>().ReverseMap();

                map.CreateMap<CreatePrivilegeEmployeesViewModel, PrivilegeEmployeesDTO>();
                map.CreateMap<UpdatePrivilegeEmployeesViewModel, PrivilegeEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateHobbyEmployeesViewModel, HobbyEmployeesDTO>();
                map.CreateMap<UpdateHobbyEmployeesViewModel, HobbyEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateFamilyViewModel, FamilyDTO>();
                map.CreateMap<UpdateFamilyViewModel, FamilyDTO>().ReverseMap();

                map.CreateMap<CreateChildrenViewModel, ChildrenDTO>();
                map.CreateMap<UpdateChildrenViewModel, ChildrenDTO>().ReverseMap();

                map.CreateMap<CreateGrandChildrenViewModel, GrandChildrenDTO>();
                map.CreateMap<UpdateGrandChildrenViewModel, GrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateHobbyChildrensViewModel, HobbyChildrensDTO>();
                map.CreateMap<UpdateHobbyChildrensViewModel, HobbyChildrensDTO>().ReverseMap();

                map.CreateMap<CreateHobbyGrandChildrensViewModel, HobbyGrandChildrensDTO>();
                map.CreateMap<UpdateHobbyGrandChildrensViewModel, HobbyGrandChildrensDTO>().ReverseMap();

            }).CreateMapper();
        }

        private static string GetHashId(string hash)
        {
            return hash.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private static uint GetRowVersion(string hash)
        {
            uint.TryParse(hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1], out uint result);
            return result;
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
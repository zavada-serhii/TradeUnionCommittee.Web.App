using AutoMapper;
using System;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.ViewModels.ViewModels.Children;
using TradeUnionCommittee.ViewModels.ViewModels.Employee;
using TradeUnionCommittee.ViewModels.ViewModels.Family;
using TradeUnionCommittee.ViewModels.ViewModels.GrandChildren;

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
                #region Mapping for Account

                map.CreateMap<CreateAccountViewModel, CreateAccountDTO>();
                map.CreateMap<UpdatePersonalDataAccountViewModel, AccountDTO>().ReverseMap();
                map.CreateMap<UpdateEmailAccountViewModel, AccountDTO>().ReverseMap();
                map.CreateMap<UpdatePasswordAccountViewModel, UpdateAccountPasswordDTO>();
                map.CreateMap<UpdateRoleAccountViewModel, AccountDTO>().ReverseMap();

                #endregion

                #region Mapping for Directory 

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

                #endregion

                #region Mapping for Report

                map.CreateMap<PdfReportViewModel, ReportPdfDTO>();

                #endregion

                #region Mapping for Employee 

                map.CreateMap<CreateEmployeeViewModel, CreateEmployeeDTO>()
                    .ForMember(d => d.HashIdSubdivision, opt => opt.MapFrom(c => c.HashIdSubordinateSubdivision ?? c.HashIdMainSubdivision))
                    .ForMember(d => d.TypeAccommodation, opt => opt.MapFrom(x => ConverterAccommodation(x.TypeAccommodation)));

                map.CreateMap<GeneralInfoEmployeeDTO, UpdateEmployeeViewModel>()
                    .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhone.Replace("-", string.Empty)))
                    .ReverseMap();

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

                map.CreateMap<CreateAwardEmployeesViewModel, AwardEmployeesDTO>();
                map.CreateMap<UpdateAwardEmployeesViewModel, AwardEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateMaterialAidEmployeesViewModel, MaterialAidEmployeesDTO>();
                map.CreateMap<UpdateMaterialAidEmployeesViewModel, MaterialAidEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateEventEmployeesViewModel, TravelEmployeesDTO>();
                map.CreateMap<UpdateEventEmployeesViewModel, TravelEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateEventEmployeesViewModel, WellnessEmployeesDTO>();
                map.CreateMap<UpdateEventEmployeesViewModel, WellnessEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateEventEmployeesViewModel, TourEmployeesDTO>();
                map.CreateMap<UpdateEventEmployeesViewModel, TourEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateActivityEmployeesViewModel, ActivityEmployeesDTO>();
                map.CreateMap<UpdateActivityEmployeesViewModel, ActivityEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateCulturalEmployeesViewModel, CulturalEmployeesDTO>();
                map.CreateMap<UpdateCulturalEmployeesViewModel, CulturalEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateGiftEmployeesViewModel, GiftEmployeesDTO>();
                map.CreateMap<UpdateGiftEmployeesViewModel, GiftEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateFluorographyEmployeesViewModel, FluorographyEmployeesDTO>();
                map.CreateMap<UpdateFluorographyEmployeesViewModel, FluorographyEmployeesDTO>().ReverseMap();

                map.CreateMap<CreateApartmentAccountingEmployeesViewModel, ApartmentAccountingEmployeesDTO>();
                map.CreateMap<UpdateApartmentAccountingEmployeesViewModel, ApartmentAccountingEmployeesDTO>().ReverseMap();

                #endregion

                #region Mapping for Family

                map.CreateMap<CreateFamilyViewModel, FamilyDTO>();
                map.CreateMap<UpdateFamilyViewModel, FamilyDTO>().ReverseMap();

                map.CreateMap<CreateEventFamilyViewModel, TravelFamilyDTO>();
                map.CreateMap<UpdateEventFamilyViewModel, TravelFamilyDTO>().ReverseMap();

                map.CreateMap<CreateEventFamilyViewModel, WellnessFamilyDTO>();
                map.CreateMap<UpdateEventFamilyViewModel, WellnessFamilyDTO>().ReverseMap();

                map.CreateMap<CreateEventFamilyViewModel, TourFamilyDTO>();
                map.CreateMap<UpdateEventFamilyViewModel, TourFamilyDTO>().ReverseMap();

                map.CreateMap<CreateActivityFamilyViewModel, ActivityFamilyDTO>();
                map.CreateMap<UpdateActivityFamilyViewModel, ActivityFamilyDTO>().ReverseMap();

                map.CreateMap<CreateCulturalFamilyViewModel, CulturalFamilyDTO>();
                map.CreateMap<UpdateCulturalFamilyViewModel, CulturalFamilyDTO>().ReverseMap();

                #endregion

                #region Mapping for Children

                map.CreateMap<CreateChildrenViewModel, ChildrenDTO>();
                map.CreateMap<UpdateChildrenViewModel, ChildrenDTO>().ReverseMap();

                map.CreateMap<CreateHobbyChildrenViewModel, HobbyChildrenDTO>();
                map.CreateMap<UpdateHobbyChildrenViewModel, HobbyChildrenDTO>().ReverseMap();

                map.CreateMap<CreateEventChildrenViewModel, TravelChildrenDTO>();
                map.CreateMap<UpdateEventChildrenViewModel, TravelChildrenDTO>().ReverseMap();

                map.CreateMap<CreateEventChildrenViewModel, WellnessChildrenDTO>();
                map.CreateMap<UpdateEventChildrenViewModel, WellnessChildrenDTO>().ReverseMap();

                map.CreateMap<CreateEventChildrenViewModel, TourChildrenDTO>();
                map.CreateMap<UpdateEventChildrenViewModel, TourChildrenDTO>().ReverseMap();

                map.CreateMap<CreateActivityChildrenViewModel, ActivityChildrenDTO>();
                map.CreateMap<UpdateActivityChildrenViewModel, ActivityChildrenDTO>().ReverseMap();

                map.CreateMap<CreateCulturalChildrenViewModel, CulturalChildrenDTO>();
                map.CreateMap<UpdateCulturalChildrenViewModel, CulturalChildrenDTO>().ReverseMap();

                map.CreateMap<CreateGiftChildrenViewModel, GiftChildrenDTO>();
                map.CreateMap<UpdateGiftChildrenViewModel, GiftChildrenDTO>().ReverseMap();

                #endregion

                #region Mapping for GrandChildren

                map.CreateMap<CreateGrandChildrenViewModel, GrandChildrenDTO>();
                map.CreateMap<UpdateGrandChildrenViewModel, GrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateHobbyGrandChildrenViewModel, HobbyGrandChildrenDTO>();
                map.CreateMap<UpdateHobbyGrandChildrenViewModel, HobbyGrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateEventGrandChildrenViewModel, TravelGrandChildrenDTO>();
                map.CreateMap<UpdateEventGrandChildrenViewModel, TravelGrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateEventGrandChildrenViewModel, TourGrandChildrenDTO>();
                map.CreateMap<UpdateEventGrandChildrenViewModel, TourGrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateActivityGrandChildrenViewModel, ActivityGrandChildrenDTO>();
                map.CreateMap<UpdateActivityGrandChildrenViewModel, ActivityGrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateCulturalGrandChildrenViewModel, CulturalGrandChildrenDTO>();
                map.CreateMap<UpdateCulturalGrandChildrenViewModel, CulturalGrandChildrenDTO>().ReverseMap();

                map.CreateMap<CreateGiftGrandChildrenViewModel, GiftGrandChildrenDTO>();
                map.CreateMap<UpdateGiftGrandChildrenViewModel, GiftGrandChildrenDTO>().ReverseMap();

                #endregion

            }).CreateMapper();
        }

        private static string GetHashId(string hash)
        {
            return hash.Split(new [] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        private static uint GetRowVersion(string hash)
        {
            uint.TryParse(hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1], out var result);
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
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

namespace TradeUnionCommittee.Razor.Web.GUI.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Mapping for Account

            CreateMap<CreateAccountViewModel, CreateAccountDTO>();
            CreateMap<UpdatePersonalDataAccountViewModel, AccountDTO>().ReverseMap();
            CreateMap<UpdateEmailAccountViewModel, AccountDTO>().ReverseMap();
            CreateMap<UpdatePasswordAccountViewModel, UpdateAccountPasswordDTO>();
            CreateMap<UpdateRoleAccountViewModel, AccountDTO>().ReverseMap();

            #endregion

            #region Mapping for Directory 

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
            CreateMap<RestructuringViewModel, RestructuringSubdivisionDTO>()
                .ForMember(d => d.HashIdSubordinate, opt => opt.MapFrom(x => AutoMapperHelper.GetHashId(x.HashIdSubordinate)))
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(x => AutoMapperHelper.GetRowVersion(x.HashIdSubordinate)));

            #endregion

            #region Mapping for Report

            CreateMap<PdfReportViewModel, ReportPdfDTO>();

            #endregion

            #region Mapping for Employee 

            CreateMap<CreateEmployeeViewModel, CreateEmployeeDTO>()
                .ForMember(d => d.HashIdSubdivision, opt => opt.MapFrom(c => c.HashIdSubordinateSubdivision ?? c.HashIdMainSubdivision))
                .ForMember(d => d.TypeAccommodation, opt => opt.MapFrom(x => AutoMapperHelper.ConverterAccommodation(x.TypeAccommodation)));

            CreateMap<GeneralInfoEmployeeDTO, UpdateEmployeeViewModel>()
                .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhone.Replace("-", string.Empty)))
                .ReverseMap();

            CreateMap<CreatePrivateHouseEmployeesViewModel, PrivateHouseEmployeesDTO>();
            CreateMap<UpdatePrivateHouseEmployeesViewModel, PrivateHouseEmployeesDTO>().ReverseMap();

            CreateMap<CreateUniversityHouseEmployeesViewModel, PrivateHouseEmployeesDTO>();
            CreateMap<UpdateUniversityHouseEmployeesViewModel, PrivateHouseEmployeesDTO>().ReverseMap();

            CreateMap<CreatePublicHouseEmployeesViewModel, PublicHouseEmployeesDTO>();
            CreateMap<UpdatePublicHouseEmployeesViewModel, PublicHouseEmployeesDTO>().ReverseMap();

            CreateMap<UpdatePositionEmployeesViewModel, PositionEmployeesDTO>().ReverseMap();

            CreateMap<CreateSocialActivityEmployeesViewModel, SocialActivityEmployeesDTO>();
            CreateMap<UpdateSocialActivityEmployeesViewModel, SocialActivityEmployeesDTO>().ReverseMap();

            CreateMap<CreatePrivilegeEmployeesViewModel, PrivilegeEmployeesDTO>();
            CreateMap<UpdatePrivilegeEmployeesViewModel, PrivilegeEmployeesDTO>().ReverseMap();

            CreateMap<CreateHobbyEmployeesViewModel, HobbyEmployeesDTO>();
            CreateMap<UpdateHobbyEmployeesViewModel, HobbyEmployeesDTO>().ReverseMap();

            CreateMap<CreateAwardEmployeesViewModel, AwardEmployeesDTO>();
            CreateMap<UpdateAwardEmployeesViewModel, AwardEmployeesDTO>().ReverseMap();

            CreateMap<CreateMaterialAidEmployeesViewModel, MaterialAidEmployeesDTO>();
            CreateMap<UpdateMaterialAidEmployeesViewModel, MaterialAidEmployeesDTO>().ReverseMap();

            CreateMap<CreateEventEmployeesViewModel, TravelEmployeesDTO>();
            CreateMap<UpdateEventEmployeesViewModel, TravelEmployeesDTO>().ReverseMap();

            CreateMap<CreateEventEmployeesViewModel, WellnessEmployeesDTO>();
            CreateMap<UpdateEventEmployeesViewModel, WellnessEmployeesDTO>().ReverseMap();

            CreateMap<CreateEventEmployeesViewModel, TourEmployeesDTO>();
            CreateMap<UpdateEventEmployeesViewModel, TourEmployeesDTO>().ReverseMap();

            CreateMap<CreateActivityEmployeesViewModel, ActivityEmployeesDTO>();
            CreateMap<UpdateActivityEmployeesViewModel, ActivityEmployeesDTO>().ReverseMap();

            CreateMap<CreateCulturalEmployeesViewModel, CulturalEmployeesDTO>();
            CreateMap<UpdateCulturalEmployeesViewModel, CulturalEmployeesDTO>().ReverseMap();

            CreateMap<CreateGiftEmployeesViewModel, GiftEmployeesDTO>();
            CreateMap<UpdateGiftEmployeesViewModel, GiftEmployeesDTO>().ReverseMap();

            CreateMap<CreateFluorographyEmployeesViewModel, FluorographyEmployeesDTO>();
            CreateMap<UpdateFluorographyEmployeesViewModel, FluorographyEmployeesDTO>().ReverseMap();

            CreateMap<CreateApartmentAccountingEmployeesViewModel, ApartmentAccountingEmployeesDTO>();
            CreateMap<UpdateApartmentAccountingEmployeesViewModel, ApartmentAccountingEmployeesDTO>().ReverseMap();

            #endregion

            #region Mapping for Family

            CreateMap<CreateFamilyViewModel, FamilyDTO>();
            CreateMap<UpdateFamilyViewModel, FamilyDTO>().ReverseMap();

            CreateMap<CreateEventFamilyViewModel, TravelFamilyDTO>();
            CreateMap<UpdateEventFamilyViewModel, TravelFamilyDTO>().ReverseMap();

            CreateMap<CreateEventFamilyViewModel, WellnessFamilyDTO>();
            CreateMap<UpdateEventFamilyViewModel, WellnessFamilyDTO>().ReverseMap();

            CreateMap<CreateEventFamilyViewModel, TourFamilyDTO>();
            CreateMap<UpdateEventFamilyViewModel, TourFamilyDTO>().ReverseMap();

            CreateMap<CreateActivityFamilyViewModel, ActivityFamilyDTO>();
            CreateMap<UpdateActivityFamilyViewModel, ActivityFamilyDTO>().ReverseMap();

            CreateMap<CreateCulturalFamilyViewModel, CulturalFamilyDTO>();
            CreateMap<UpdateCulturalFamilyViewModel, CulturalFamilyDTO>().ReverseMap();

            #endregion

            #region Mapping for Children

            CreateMap<CreateChildrenViewModel, ChildrenDTO>();
            CreateMap<UpdateChildrenViewModel, ChildrenDTO>().ReverseMap();

            CreateMap<CreateHobbyChildrenViewModel, HobbyChildrenDTO>();
            CreateMap<UpdateHobbyChildrenViewModel, HobbyChildrenDTO>().ReverseMap();

            CreateMap<CreateEventChildrenViewModel, TravelChildrenDTO>();
            CreateMap<UpdateEventChildrenViewModel, TravelChildrenDTO>().ReverseMap();

            CreateMap<CreateEventChildrenViewModel, WellnessChildrenDTO>();
            CreateMap<UpdateEventChildrenViewModel, WellnessChildrenDTO>().ReverseMap();

            CreateMap<CreateEventChildrenViewModel, TourChildrenDTO>();
            CreateMap<UpdateEventChildrenViewModel, TourChildrenDTO>().ReverseMap();

            CreateMap<CreateActivityChildrenViewModel, ActivityChildrenDTO>();
            CreateMap<UpdateActivityChildrenViewModel, ActivityChildrenDTO>().ReverseMap();

            CreateMap<CreateCulturalChildrenViewModel, CulturalChildrenDTO>();
            CreateMap<UpdateCulturalChildrenViewModel, CulturalChildrenDTO>().ReverseMap();

            CreateMap<CreateGiftChildrenViewModel, GiftChildrenDTO>();
            CreateMap<UpdateGiftChildrenViewModel, GiftChildrenDTO>().ReverseMap();

            #endregion

            #region Mapping for GrandChildren

            CreateMap<CreateGrandChildrenViewModel, GrandChildrenDTO>();
            CreateMap<UpdateGrandChildrenViewModel, GrandChildrenDTO>().ReverseMap();

            CreateMap<CreateHobbyGrandChildrenViewModel, HobbyGrandChildrenDTO>();
            CreateMap<UpdateHobbyGrandChildrenViewModel, HobbyGrandChildrenDTO>().ReverseMap();

            CreateMap<CreateEventGrandChildrenViewModel, TravelGrandChildrenDTO>();
            CreateMap<UpdateEventGrandChildrenViewModel, TravelGrandChildrenDTO>().ReverseMap();

            CreateMap<CreateEventGrandChildrenViewModel, TourGrandChildrenDTO>();
            CreateMap<UpdateEventGrandChildrenViewModel, TourGrandChildrenDTO>().ReverseMap();

            CreateMap<CreateActivityGrandChildrenViewModel, ActivityGrandChildrenDTO>();
            CreateMap<UpdateActivityGrandChildrenViewModel, ActivityGrandChildrenDTO>().ReverseMap();

            CreateMap<CreateCulturalGrandChildrenViewModel, CulturalGrandChildrenDTO>();
            CreateMap<UpdateCulturalGrandChildrenViewModel, CulturalGrandChildrenDTO>().ReverseMap();

            CreateMap<CreateGiftGrandChildrenViewModel, GiftGrandChildrenDTO>();
            CreateMap<UpdateGiftGrandChildrenViewModel, GiftGrandChildrenDTO>().ReverseMap();

            #endregion
        }
    }

    internal sealed class AutoMapperHelper
    {
        public static string GetHashId(string hash)
        {
            return hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        public static uint GetRowVersion(string hash)
        {
            uint.TryParse(hash.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)[1], out var result);
            return result;
        }

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
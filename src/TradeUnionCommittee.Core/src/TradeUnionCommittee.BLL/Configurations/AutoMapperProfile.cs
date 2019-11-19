using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.CloudStorage.Service.Model;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Identity.Entities;
using TradeUnionCommittee.DAL.Repository;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.BLL.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Mapping for User, Role and Journal

            CreateMap<User, AccountDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => x.Id))
                .ReverseMap();

            CreateMap<IdentityRole, RolesDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => x.Id))
                .ForMember(d => d.Name, c => c.MapFrom(x => TranslatorHelper.ConvertToUkrainianLang(x.Name)))
                .ReverseMap();

            CreateMap<RefreshTokenDTO, RefreshToken>()
                .ForMember(d => d.ClientId, c => c.MapFrom(x => x.ClientType));

            #endregion

            #region Mapping for Directory

            CreateMap<Position, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<SocialActivity, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<Privileges, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<Award, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<MaterialAid, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<Hobby, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<Event, TravelDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)))
                .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Travel));

            CreateMap<Event, WellnessDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)))
                .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Wellness));

            CreateMap<Event, TourDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)))
                .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Tour));

            CreateMap<Activities, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            CreateMap<Cultural, DirectoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)));

            //----------------------------------------------------------------------------------------------------------------------------------

            CreateMap<Subdivisions, SubdivisionDTO>()
                .ForMember(d => d.HashIdMain, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

            CreateMap<CreateSubdivisionDTO, Subdivisions>()
                .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation));

            CreateMap<CreateSubordinateSubdivisionDTO, Subdivisions>()
                .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdMain)));

            CreateMap<UpdateSubdivisionNameDTO, Subdivisions>()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdMain)))
                .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

            CreateMap<UpdateSubdivisionAbbreviationDTO, Subdivisions>()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdMain)))
                .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

            CreateMap<RestructuringSubdivisionDTO, Subdivisions>()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdSubordinate)))
                .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdMain)));

            //----------------------------------------------------------------------------------------------------------------------------------

            CreateMap<AddressPublicHouse, DormitoryDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)))
                .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Dormitory));

            CreateMap<AddressPublicHouse, DepartmentalDTO>()
                .ForMember(d => d.HashId, c => c.MapFrom(x => HashHelper.EncryptLong(x.Id)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashId)))
                .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Departmental));

            #endregion

            #region Mapping for Create Employee

            CreateMap<Employee, CreateEmployeeDTO>().ReverseMap()
                .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()));

            CreateMap<PositionEmployees, CreateEmployeeDTO>()
                .ReverseMap()
                .ForMember(d => d.IdSubdivision, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdSubdivision)))
                .ForMember(d => d.IdPosition, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdPosition)))
                .ForMember(d => d.StartDate, c => c.MapFrom(x => x.StartDatePosition))
                .ForMember(d => d.CheckPosition, c => c.MapFrom(x => true));

            CreateMap<PrivateHouseEmployees, CreateEmployeeDTO>()
                .ReverseMap()
                .ForMember(x => x.City, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.CityPrivateHouse : x.CityHouseUniversity))
                .ForMember(x => x.Street, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.StreetPrivateHouse : x.StreetHouseUniversity))
                .ForMember(x => x.NumberHouse, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.NumberHousePrivateHouse : x.NumberHouseUniversity))
                .ForMember(x => x.NumberApartment, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.NumberApartmentPrivateHouse : x.NumberApartmentHouseUniversity))
                .ForMember(x => x.DateReceiving, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.FromUniversity ? x.DateReceivingHouseFromUniversity : null));

            CreateMap<PublicHouseEmployees, CreateEmployeeDTO>()
                .ReverseMap()
                .ForMember(x => x.IdAddressPublicHouse, c => c.MapFrom(x => AutoMapperHelper.DecryptIdAddressPublicHouse(x.TypeAccommodation, x.HashIdDormitory, x.HashIdDepartmental)))
                .ForMember(x => x.NumberRoom, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.Dormitory ? x.NumberRoomDormitory : x.NumberRoomDepartmental));

            CreateMap<SocialActivityEmployees, CreateEmployeeDTO>()
                .ReverseMap()
                .ForMember(x => x.IdSocialActivity, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdSocialActivity)))
                .ForMember(x => x.Note, c => c.MapFrom(x => x.NoteSocialActivity))
                .ForMember(d => d.CheckSocialActivity, c => c.MapFrom(x => true));

            CreateMap<PrivilegeEmployees, CreateEmployeeDTO>()
                .ReverseMap()
                .ForMember(x => x.IdPrivileges, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdPrivileges)))
                .ForMember(x => x.Note, c => c.MapFrom(x => x.NotePrivileges))
                .ForMember(d => d.CheckPrivileges, c => c.MapFrom(x => true));

            //------------------------------------------------------------------------------

            CreateMap<Employee, GeneralInfoEmployeeDTO>()
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.CountYear, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()))
                .ForMember(x => x.Sex, opt => opt.MapFrom(c => TranslatorHelper.ConvertToUkraineGender(c.Sex)))
                .ReverseMap()
                .ForMember(d => d.Id, c => c.MapFrom(x => HashHelper.DecryptLong(x.HashIdEmployee)))
                .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()))
                .ForMember(d => d.DateAdded, c => c.MapFrom(x => DateTime.Now))
                .ForMember(d => d.EndYearWork, c => c.MapFrom(x => x.EndYearWork == 0 ? null : x.EndYearWork))
                .ForMember(d => d.EndDateTradeUnion, c => c.MapFrom(x => x.EndDateTradeUnion == null || x.EndDateTradeUnion == DateTime.MinValue ? null : x.EndYearWork));

            #endregion

            #region Mapping for Private and Public House Employees

            CreateMap<PrivateHouseEmployees, PrivateHouseEmployeesDTO>()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)));

            CreateMap<PublicHouseEmployeesDTO, PublicHouseEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdAddressPublicHouse, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdAddressPublicHouse)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdAddressPublicHouse, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdAddressPublicHouse)))
                .ForMember(x => x.FullAddress, opt => opt.MapFrom(c => c.IdAddressPublicHouseNavigation.NumberDormitory ?? $"{c.IdAddressPublicHouseNavigation.City}, {c.IdAddressPublicHouseNavigation.Street}, {c.IdAddressPublicHouseNavigation.NumberHouse}"));

            #endregion

            #region Mapping for Employee

            CreateMap<PositionEmployees, PositionEmployeesDTO>()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdSubdivision, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdSubdivision)))
                .ForMember(x => x.HashIdPosition, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdPosition)))
                .ReverseMap()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdSubdivision, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdSubdivision)))
                .ForMember(x => x.IdPosition, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdPosition)));

            CreateMap<SocialActivityEmployeesDTO, SocialActivityEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdSocialActivity, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdSocialActivity)))
                .ForMember(x => x.IdSocialActivityNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdSocialActivity, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdSocialActivity)))
                .ForMember(x => x.NameSocialActivity, opt => opt.MapFrom(c => c.IdSocialActivityNavigation.Name));

            CreateMap<PrivilegeEmployeesDTO, PrivilegeEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdPrivileges, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdPrivileges)))
                .ForMember(x => x.IdPrivilegesNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdPrivileges, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdPrivileges)))
                .ForMember(x => x.NamePrivileges, opt => opt.MapFrom(c => c.IdPrivilegesNavigation.Name));

            CreateMap<HobbyEmployeesDTO, HobbyEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdHobby)))
                .ForMember(x => x.IdHobbyNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdHobby)))
                .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

            CreateMap<AwardEmployeesDTO, AwardEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdAward, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdAward)))
                .ForMember(x => x.IdAwardNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdAward, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdAward)))
                .ForMember(x => x.NameAward, opt => opt.MapFrom(c => c.IdAwardNavigation.Name));

            CreateMap<MaterialAidEmployeesDTO, MaterialAidEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdMaterialAid, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdMaterialAid)))
                .ForMember(x => x.IdMaterialAidNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdMaterialAid, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdMaterialAid)))
                .ForMember(x => x.NameMaterialAid, opt => opt.MapFrom(c => c.IdMaterialAidNavigation.Name));

            CreateMap<TravelEmployeesDTO, EventEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<WellnessEmployeesDTO, EventEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<TourEmployeesDTO, EventEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<ActivityEmployeesDTO, ActivityEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdActivities)))
                .ForMember(x => x.IdActivitiesNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdActivities)))
                .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

            CreateMap<CulturalEmployeesDTO, CulturalEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdCultural)))
                .ForMember(x => x.IdCulturalNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdCultural)))
                .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

            CreateMap<GiftEmployeesDTO, GiftEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)));

            CreateMap<FluorographyEmployeesDTO, FluorographyEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)));

            CreateMap<ApartmentAccountingEmployeesDTO, ApartmentAccountingEmployees>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)));

            #endregion

            #region Mapping for Family

            CreateMap<FamilyDTO, Family>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAgeForNull()));

            CreateMap<TravelFamilyDTO, EventFamily>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdFamily)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdFamily)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<WellnessFamilyDTO, EventFamily>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdFamily)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdFamily)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<TourFamilyDTO, EventFamily>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdFamily)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdFamily)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<ActivityFamilyDTO, ActivityFamily>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdFamily)))
                .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdActivities)))
                .ForMember(x => x.IdActivitiesNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdFamily)))
                .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdActivities)))
                .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

            CreateMap<CulturalFamilyDTO, CulturalFamily>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdFamily)))
                .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdCultural)))
                .ForMember(x => x.IdCulturalNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdFamily)))
                .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdCultural)))
                .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

            #endregion

            #region Mapping for Children

            CreateMap<ChildrenDTO, Children>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()));

            CreateMap<HobbyChildrenDTO, HobbyChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdHobby)))
                .ForMember(x => x.IdHobbyNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdHobby)))
                .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

            CreateMap<TravelChildrenDTO, EventChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<WellnessChildrenDTO, EventChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<TourChildrenDTO, EventChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<ActivityChildrenDTO, ActivityChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdActivities)))
                .ForMember(x => x.IdActivitiesNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdActivities)))
                .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

            CreateMap<CulturalChildrenDTO, CulturalChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdCultural)))
                .ForMember(x => x.IdCulturalNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)))
                .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdCultural)))
                .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

            CreateMap<GiftChildrenDTO, GiftChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdChildren)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdChildren)));

            #endregion

            #region Mapping for Grand Children

            CreateMap<GrandChildrenDTO, GrandChildren>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEmployee)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEmployee)))
                .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()));

            CreateMap<HobbyGrandChildrenDTO, HobbyGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdHobby)))
                .ForMember(x => x.IdHobbyNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)))
                .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdHobby)))
                .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

            CreateMap<TravelGrandChildrenDTO, EventGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<TourGrandChildrenDTO, EventGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdEvent)))
                .ForMember(x => x.IdEventNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)))
                .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdEvent)))
                .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

            CreateMap<ActivityGrandChildrenDTO, ActivityGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdActivities)))
                .ForMember(x => x.IdActivitiesNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)))
                .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdActivities)))
                .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

            CreateMap<CulturalGrandChildrenDTO, CulturalGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdCultural)))
                .ForMember(x => x.IdCulturalNavigation, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)))
                .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdCultural)))
                .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

            CreateMap<GiftGrandChildrenDTO, GiftGrandChildrens>()
                .ForMember(x => x.Id, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashId)))
                .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => HashHelper.DecryptLong(c.HashIdGrandChildren)))
                .ReverseMap()
                .ForMember(x => x.HashId, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)))
                .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.IdGrandChildren)));

            #endregion

            #region Mapping for PDF service

            CreateMap<MaterialAidEmployees, MaterialIncentivesEmployeeEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdMaterialAidNavigation.Name))
                .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateIssue));

            CreateMap<AwardEmployees, MaterialIncentivesEmployeeEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdAwardNavigation.Name))
                .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateIssue));

            CreateMap<CulturalEmployees, CulturalEmployeeEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name))
                .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateVisit));

            CreateMap<EventEmployees, EventEmployeeEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdEventNavigation.Name))
                .ForMember(x => x.TypeEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Type));

            CreateMap<GiftEmployees, GiftEmployeeEntity>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.NameEvent))
                .ForMember(x => x.Amount, opt => opt.MapFrom(c => c.Price))
                .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateGift));

            CreateMap<ReportPdfDTO, ReportPdfBucketModel>()
                .ForMember(x => x.DateFrom, opt => opt.MapFrom(c => c.StartDate))
                .ForMember(x => x.DateTo, opt => opt.MapFrom(c => c.EndDate));

            CreateMap<(string FileName, byte[] Data), FileModel>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(c => c.FileName))
                .ForMember(x => x.Data, opt => opt.MapFrom(c => c.Data));

            CreateMap<(string FileName, byte[] Data), FileDTO>()
                .ForMember(x => x.FileName, opt => opt.MapFrom(c => c.FileName))
                .ForMember(x => x.Data, opt => opt.MapFrom(c => c.Data));

            CreateMap<ReportPdfDTO, ReportModel>();

            #endregion

            #region Mapping for Search by Full Name

            CreateMap<ResultFullNameSearch, ResultSearchDTO>()
                .ForMember(x => x.HashIdUser, opt => opt.MapFrom(c => HashHelper.EncryptLong(c.Id)));

            #endregion
        }
    }

    internal sealed class AutoMapperHelper
    {
        public static long DecryptIdAddressPublicHouse(AccommodationType type, string hashIdDormitory, string hashIdDepartmental)
        {
            switch (type)
            {
                case AccommodationType.Dormitory:
                    return HashHelper.DecryptLong(hashIdDormitory);
                case AccommodationType.Departmental:
                    return HashHelper.DecryptLong(hashIdDepartmental);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
using AutoMapper;
using System;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.DTO.Family;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Native;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.BLL.Configurations
{
    public interface IAutoMapperConfiguration
    {
        IMapper Mapper { get; }
    }

    //------------------------------------------------------------------------------

    internal sealed class AutoMapperConfiguration : IAutoMapperConfiguration
    {
        private readonly IHashIdConfiguration _hashIdUtilities;
        private readonly object _nullValue = null;

        public AutoMapperConfiguration(IHashIdConfiguration hashIdUtilities)
        {
            _hashIdUtilities = hashIdUtilities;
        }

        public IMapper Mapper
        {
            get
            {
                return new MapperConfiguration(map =>
                {

                    #region Mapping for User, Role and Journal

                    map.CreateMap<User, AccountDTO>()
                       .ForMember(d => d.HashIdUser, c => c.MapFrom(x => x.Id))
                       .ForMember(d => d.Role, c => c.MapFrom(x => ConvertToUkrainianLang(x.UserRole)))
                       .ReverseMap();

                    map.CreateMap<Role, RolesDTO>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => ConvertToUkrainianLang(x.Name)))
                        .ReverseMap();

                    //map.CreateMap<Journal, JournalDTO>().ReverseMap();

                    #endregion

                    #region Mapping for Directory

                    map.CreateMap<Position, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<SocialActivity, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<Privileges, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<Award, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<MaterialAid, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<Hobby, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<Event, TravelDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Travel));

                    map.CreateMap<Event, WellnessDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Wellness));

                    map.CreateMap<Event, TourDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Tour));

                    map.CreateMap<Activities, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    map.CreateMap<Cultural, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)));

                    //----------------------------------------------------------------------------------------------------------------------------------

                    map.CreateMap<Subdivisions, SubdivisionDTO>()
                        .ForMember(d => d.HashIdMain, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

                    map.CreateMap<CreateSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation));

                    map.CreateMap<CreateSubordinateSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain)));

                    map.CreateMap<UpdateSubdivisionNameDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain)))
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

                    map.CreateMap<UpdateSubdivisionAbbreviationDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain)))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

                    map.CreateMap<RestructuringSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSubordinate)))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain)));

                    //----------------------------------------------------------------------------------------------------------------------------------

                    map.CreateMap<AddressPublicHouse, DormitoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Dormitory));

                    map.CreateMap<AddressPublicHouse, DepartmentalDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Departmental));

                    #endregion

                    #region Mapping for Create Employee

                    map.CreateMap<Employee, CreateEmployeeDTO>().ReverseMap()
                        .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()));

                    map.CreateMap<PositionEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(d => d.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(d => d.IdSubdivision, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSubdivision)))
                        .ForMember(d => d.IdPosition, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdPosition)))
                        .ForMember(d => d.StartDate, c=> c.MapFrom(x => x.StartDatePosition))
                        .ForMember(d => d.CheckPosition, c => c.MapFrom(x => true));

                    map.CreateMap<PrivateHouseEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.City, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.CityPrivateHouse : x.CityHouseUniversity))
                        .ForMember(x => x.Street, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.StreetPrivateHouse : x.StreetHouseUniversity))
                        .ForMember(x => x.NumberHouse, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.NumberHousePrivateHouse : x.NumberHouseUniversity))
                        .ForMember(x => x.NumberApartment, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.PrivateHouse ? x.NumberApartmentPrivateHouse : x.NumberApartmentHouseUniversity))
                        .ForMember(x => x.DateReceiving, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.FromUniversity ? x.DateReceivingHouseFromUniversity : null));

                    map.CreateMap<PublicHouseEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdAddressPublicHouse, c => c.MapFrom(x => DecryptIdAddressPublicHouse(x.TypeAccommodation, x.HashIdDormitory, x.HashIdDepartmental)))
                        .ForMember(x => x.NumberRoom, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.Dormitory ? x.NumberRoomDormitory : x.NumberRoomDepartmental));

                    map.CreateMap<SocialActivityEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdSocialActivity, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSocialActivity)))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NoteSocialActivity))
                        .ForMember(d => d.CheckSocialActivity, c => c.MapFrom(x => true));

                    map.CreateMap<PrivilegeEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdPrivileges, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdPrivileges)))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NotePrivileges))
                        .ForMember(d => d.CheckPrivileges, c => c.MapFrom(x => true));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Employee, GeneralInfoEmployeeDTO>()
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.CountYear, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()))
                        .ForMember(x => x.Sex, opt => opt.MapFrom(c => ConvertToUkraineGender(c.Sex)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdEmployee)))
                        .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()))
                        .ForMember(d => d.DateAdded, c => c.MapFrom(x => DateTime.Now))
                        .ForMember(d => d.EndYearWork, c => c.MapFrom(x => x.EndYearWork == 0 ? null : x.EndYearWork))
                        .ForMember(d => d.EndDateTradeUnion, c => c.MapFrom(x => x.EndDateTradeUnion == null || x.EndDateTradeUnion == DateTime.MinValue ? null : x.EndYearWork));

                    #endregion

                    #region Mapping for Private and Public House Employees

                    map.CreateMap<PrivateHouseEmployees, PrivateHouseEmployeesDTO>()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ReverseMap()
                        .ForMember(x => x.Id , opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee , opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)));

                    map.CreateMap<PublicHouseEmployeesDTO, PublicHouseEmployees>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                       .ForMember(x => x.IdAddressPublicHouse, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdAddressPublicHouse)))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                       .ForMember(x => x.HashIdAddressPublicHouse, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdAddressPublicHouse)))
                       .ForMember(x => x.FullAddress, opt => opt.MapFrom(c => c.IdAddressPublicHouseNavigation.NumberDormitory ?? $"{c.IdAddressPublicHouseNavigation.City}, {c.IdAddressPublicHouseNavigation.Street}, {c.IdAddressPublicHouseNavigation.NumberHouse}"));

                    #endregion

                    #region Mapping for Employee

                    map.CreateMap<PositionEmployees, PositionEmployeesDTO>()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                       .ForMember(x => x.HashIdSubdivision, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdSubdivision)))
                       .ForMember(x => x.HashIdPosition, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdPosition)))
                       .ReverseMap()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                       .ForMember(x => x.IdSubdivision, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdSubdivision)))
                       .ForMember(x => x.IdPosition, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdPosition)));

                    map.CreateMap<SocialActivityEmployeesDTO, SocialActivityEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdSocialActivity, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdSocialActivity)))
                        .ForMember(x => x.IdSocialActivityNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdSocialActivity, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdSocialActivity)))
                        .ForMember(x => x.NameSocialActivity, opt => opt.MapFrom(c => c.IdSocialActivityNavigation.Name));

                    map.CreateMap<PrivilegeEmployeesDTO, PrivilegeEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdPrivileges, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdPrivileges)))
                        .ForMember(x => x.IdPrivilegesNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdPrivileges, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdPrivileges)))
                        .ForMember(x => x.NamePrivileges, opt => opt.MapFrom(c => c.IdPrivilegesNavigation.Name));

                    map.CreateMap<HobbyEmployeesDTO, HobbyEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdHobby)))
                        .ForMember(x => x.IdHobbyNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdHobby)))
                        .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

                    map.CreateMap<AwardEmployeesDTO, AwardEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdAward, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdAward)))
                        .ForMember(x => x.IdAwardNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdAward, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdAward)))
                        .ForMember(x => x.NameAward, opt => opt.MapFrom(c => c.IdAwardNavigation.Name));

                    map.CreateMap<MaterialAidEmployeesDTO, MaterialAidEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdMaterialAid, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdMaterialAid)))
                        .ForMember(x => x.IdMaterialAidNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdMaterialAid, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdMaterialAid)))
                        .ForMember(x => x.NameMaterialAid, opt => opt.MapFrom(c => c.IdMaterialAidNavigation.Name));

                    map.CreateMap<TravelEmployeesDTO, EventEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<WellnessEmployeesDTO, EventEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<TourEmployeesDTO, EventEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<ActivityEmployeesDTO, ActivityEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdActivities)))
                        .ForMember(x => x.IdActivitiesNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdActivities)))
                        .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

                    map.CreateMap<CulturalEmployeesDTO, CulturalEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdCultural)))
                        .ForMember(x => x.IdCulturalNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                        .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdCultural)))
                        .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

                    map.CreateMap<GiftEmployeesDTO, GiftEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)));

                    map.CreateMap<FluorographyEmployeesDTO, FluorographyEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)));

                    map.CreateMap<ApartmentAccountingEmployeesDTO, ApartmentAccountingEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)));

                    #endregion

                    #region Mapping for Family

                    map.CreateMap<FamilyDTO, Family>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                       .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                       .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAgeForNull()));

                    map.CreateMap<TravelFamilyDTO, EventFamily>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdFamily)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdFamily)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<WellnessFamilyDTO, EventFamily>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdFamily)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdFamily)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<TourFamilyDTO, EventFamily>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdFamily)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdFamily)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<ActivityFamilyDTO, ActivityFamily>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdFamily)))
                        .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdActivities)))
                        .ForMember(x => x.IdActivitiesNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdFamily)))
                        .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdActivities)))
                        .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

                    map.CreateMap<CulturalFamilyDTO, CulturalFamily>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdFamily, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdFamily)))
                        .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdCultural)))
                        .ForMember(x => x.IdCulturalNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdFamily, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdFamily)))
                        .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdCultural)))
                        .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

                    #endregion

                    #region Mapping for Children

                    map.CreateMap<ChildrenDTO, Children>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                       .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                       .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()));

                    map.CreateMap<HobbyChildrenDTO, HobbyChildrens>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                       .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdHobby)))
                       .ForMember(x => x.IdHobbyNavigation, opt => opt.MapFrom(c => _nullValue))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                       .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdHobby)))
                       .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

                    map.CreateMap<TravelChildrenDTO, EventChildrens>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                       .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                       .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                       .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                       .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<WellnessChildrenDTO, EventChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<TourChildrenDTO, EventChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<ActivityChildrenDTO, ActivityChildrens>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                       .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdActivities)))
                       .ForMember(x => x.IdActivitiesNavigation, opt => opt.MapFrom(c => _nullValue))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                       .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdActivities)))
                       .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

                    map.CreateMap<CulturalChildrenDTO, CulturalChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                        .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdCultural)))
                        .ForMember(x => x.IdCulturalNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)))
                        .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdCultural)))
                        .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

                    map.CreateMap<GiftChildrenDTO, GiftChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdChildren)))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdChildren)));

                    #endregion

                    #region Mapping for Grand Children

                    map.CreateMap<GrandChildrenDTO, GrandChildren>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee)))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee)))
                       .ForMember(x => x.FullName, opt => opt.MapFrom(c => $"{c.FirstName} {c.SecondName} {c.Patronymic}"))
                       .ForMember(x => x.Age, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()));

                    map.CreateMap<HobbyGrandChildrenDTO, HobbyGrandChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                        .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdHobby)))
                        .ForMember(x => x.IdHobbyNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)))
                        .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdHobby)))
                        .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

                    map.CreateMap<TravelGrandChildrenDTO, EventGrandChildrens>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                       .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                       .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)))
                       .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                       .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<TourGrandChildrenDTO, EventGrandChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                        .ForMember(x => x.IdEvent, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEvent)))
                        .ForMember(x => x.IdEventNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)))
                        .ForMember(x => x.HashIdEvent, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEvent)))
                        .ForMember(x => x.NameEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Name));

                    map.CreateMap<ActivityGrandChildrenDTO, ActivityGrandChildrens>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                       .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                       .ForMember(x => x.IdActivities, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdActivities)))
                       .ForMember(x => x.IdActivitiesNavigation, opt => opt.MapFrom(c => _nullValue))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                       .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)))
                       .ForMember(x => x.HashIdActivities, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdActivities)))
                       .ForMember(x => x.NameActivities, opt => opt.MapFrom(c => c.IdActivitiesNavigation.Name));

                    map.CreateMap<CulturalGrandChildrenDTO, CulturalGrandChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                        .ForMember(x => x.IdCultural, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdCultural)))
                        .ForMember(x => x.IdCulturalNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)))
                        .ForMember(x => x.HashIdCultural, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdCultural)))
                        .ForMember(x => x.NameCultural, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name));

                    map.CreateMap<GiftGrandChildrenDTO, GiftGrandChildrens>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId)))
                        .ForMember(x => x.IdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdGrandChildren)))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)))
                        .ForMember(x => x.HashIdGrandChildren, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdGrandChildren)));

                    #endregion

                    #region Mapping for PDF service

                    map.CreateMap<MaterialAidEmployees, MaterialIncentivesEmployeeEntity>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdMaterialAidNavigation.Name))
                        .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateIssue));

                    map.CreateMap<AwardEmployees, MaterialIncentivesEmployeeEntity>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdAwardNavigation.Name))
                        .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateIssue));

                    map.CreateMap<CulturalEmployees, CulturalEmployeeEntity>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdCulturalNavigation.Name))
                        .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateVisit));

                    map.CreateMap<EventEmployees, EventEmployeeEntity>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(c => c.IdEventNavigation.Name))
                        .ForMember(x => x.TypeEvent, opt => opt.MapFrom(c => c.IdEventNavigation.Type));

                    map.CreateMap<GiftEmployees, GiftEmployeeEntity>()
                        .ForMember(x => x.Name, opt => opt.MapFrom(c => c.NameEvent))
                        .ForMember(x => x.Amount, opt => opt.MapFrom(c => c.Price))
                        .ForMember(x => x.Date, opt => opt.MapFrom(c => c.DateGift));

                    #endregion

                    #region Mapping for Search by Full Name

                    map.CreateMap<ResultFullNameSearch, ResultSearchDTO>()
                       .ForMember(x => x.HashIdUser, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id)));

                    #endregion

                }).CreateMapper();
            }
        }

        private string ConvertToUkrainianLang(string param)
        {
            switch (param)
            {
                case "Admin":
                    return "Адміністратор";
                case "Accountant":
                    return "Бухгалтер";
                case "Deputy":
                    return "Заступник";
                default:
                    return string.Empty;
            }
        }

        private string ConvertToUkraineGender(string sex)
        {
            switch (sex)
            {
                case "Male":
                    return new string("Чоловіча");
                case "Female":
                    return new string("Жіноча");
                default:
                    return sex;
            }
        }

        private long DecryptIdAddressPublicHouse(AccommodationType type, string hashIdDormitory, string hashIdDepartmental)
        {
            switch (type)
            {
                case AccommodationType.Dormitory:
                    return _hashIdUtilities.DecryptLong(hashIdDormitory);
                case AccommodationType.Departmental:
                    return _hashIdUtilities.DecryptLong(hashIdDepartmental);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
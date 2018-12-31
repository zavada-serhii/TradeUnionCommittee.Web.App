using AutoMapper;
using System;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Repositories.Search;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.BLL.Utilities
{
    public interface IAutoMapperUtilities
    {
        IMapper Mapper { get; }
    }

    //------------------------------------------------------------------------------

    internal sealed class AutoMapperUtilities : IAutoMapperUtilities
    {
        private readonly IHashIdUtilities _hashIdUtilities;
        private readonly object _nullValue = null;

        public AutoMapperUtilities(IHashIdUtilities hashIdUtilities)
        {
            _hashIdUtilities = hashIdUtilities;
        }

        public IMapper Mapper
        {
            get
            {
                return new MapperConfiguration(map =>
                {
                    //------------------------------------------------------------------------------

                    map.CreateMap<User, AccountDTO>()
                        .ForMember(d => d.HashIdUser, c => c.MapFrom(x => x.Id))
                        .ForMember(d => d.Role, c => c.MapFrom(x => ConvertToUkrainianLang(x.UserRole)))
                        .ReverseMap();

                    map.CreateMap<Role, RolesDTO>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => ConvertToUkrainianLang(x.Name)))
                        .ReverseMap();

                    map.CreateMap<Journal, JournalDTO>().ReverseMap();

                    // -- Mapping for directory

                    map.CreateMap<Position, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Position)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Position)));

                    map.CreateMap<SocialActivity, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.SocialActivity)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.SocialActivity)));

                    map.CreateMap<Privileges, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Privileges)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Privileges)));

                    map.CreateMap<Award, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Award)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Award)));

                    map.CreateMap<MaterialAid, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.MaterialAid)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.MaterialAid)));

                    map.CreateMap<Hobby, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Hobby)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Hobby)));

                    map.CreateMap<Event, TravelDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Travel)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Travel)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Travel));

                    map.CreateMap<Event, WellnessDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Wellness)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Wellness)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Wellness));

                    map.CreateMap<Event, TourDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Tour)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Tour)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeEvent.Tour));

                    map.CreateMap<Activities, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Activities)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Activities)));

                    map.CreateMap<Cultural, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Cultural)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Cultural)));

                    //----------------------------------------------

                    map.CreateMap<Subdivisions, SubdivisionDTO>()
                        .ForMember(d => d.HashIdMain, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id,Enums.Services.Subdivision)))
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion));

                    map.CreateMap<CreateSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation));

                    map.CreateMap<CreateSubordinateSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain, Enums.Services.Subdivision)));

                    map.CreateMap<UpdateSubdivisionNameDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain, Enums.Services.Subdivision)))
                        .ForMember(d => d.Name, c => c.MapFrom(x => x.Name))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion))
                        .ForMember(d => d.SubdivisionUpdate, c => c.MapFrom(x => Subdivision.UpdateName));

                    map.CreateMap<UpdateSubdivisionAbbreviationDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain, Enums.Services.Subdivision)))
                        .ForMember(d => d.Abbreviation, c => c.MapFrom(x => x.Abbreviation))
                        .ForMember(d => d.RowVersion, c => c.MapFrom(x => x.RowVersion))
                        .ForMember(d => d.SubdivisionUpdate, c => c.MapFrom(x => Subdivision.UpdateAbbreviation));

                    map.CreateMap<RestructuringSubdivisionDTO, Subdivisions>()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSubordinate, Enums.Services.Subdivision)))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdMain, Enums.Services.Subdivision)))
                        .ForMember(d => d.SubdivisionUpdate, c => c.MapFrom(x => Subdivision.RestructuringUnits));

                    //----------------------------------------------

                    map.CreateMap<AddressPublicHouse, DormitoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.AddressPublicHouse)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.AddressPublicHouse)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Dormitory));

                    map.CreateMap<AddressPublicHouse, DepartmentalDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.AddressPublicHouse)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.AddressPublicHouse)))
                        .ForMember(d => d.Type, c => c.MapFrom(x => TypeHouse.Departmental));

                    //------------------------------------------------------------------------------

                    // -- Mapping for create employee

                    map.CreateMap<Employee, CreateEmployeeDTO>().ReverseMap()
                    .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()));

                    map.CreateMap<PositionEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(d => d.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(d => d.IdSubdivision, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSubdivision, Enums.Services.Subdivision)))
                        .ForMember(d => d.IdPosition, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdPosition, Enums.Services.Position)))
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
                        .ForMember(x => x.IdSocialActivity, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSocialActivity, Enums.Services.SocialActivity)))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NoteSocialActivity))
                        .ForMember(d => d.CheckSocialActivity, c => c.MapFrom(x => true));

                    map.CreateMap<PrivilegeEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdPrivileges, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdPrivileges, Enums.Services.Privileges)))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NotePrivileges))
                        .ForMember(d => d.CheckPrivileges, c => c.MapFrom(x => true));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Employee, GeneralInfoEmployeeDTO>()
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.Employee)))
                        .ForMember(x => x.CountYear, opt => opt.MapFrom(c => c.BirthDate.CalculateAge()))
                        .ForMember(x => x.Sex, opt => opt.MapFrom(c => ConvertToUkraineGender(c.Sex)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdEmployee, Enums.Services.Employee)))
                        .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()))
                        .ForMember(d => d.DateAdded, c => c.MapFrom(x => DateTime.Now))
                        .ForMember(d => d.EndYearWork, c => c.MapFrom(x => x.EndYearWork == 0 ? null : x.EndYearWork))
                        .ForMember(d => d.EndDateTradeUnion, c => c.MapFrom(x => x.EndDateTradeUnion == null || x.EndDateTradeUnion == DateTime.MinValue ? null : x.EndYearWork));

                    //------------------------------------------------------------------------------

                    map.CreateMap<PrivateHouseEmployees, PrivateHouseEmployeesDTO>()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.PrivateHouseEmployees)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                        .ReverseMap()
                        .ForMember(x => x.Id , opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.PrivateHouseEmployees)))
                        .ForMember(x => x.IdEmployee , opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)));

                    map.CreateMap<PublicHouseEmployeesDTO, PublicHouseEmployees>()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.PublicHouseEmployees)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)))
                       .ForMember(x => x.IdAddressPublicHouse, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdAddressPublicHouse, Enums.Services.AddressPublicHouse)))
                       .ReverseMap()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.PublicHouseEmployees)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                       .ForMember(x => x.HashIdAddressPublicHouse, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdAddressPublicHouse, Enums.Services.AddressPublicHouse)))
                       .ForMember(x => x.FullAddress, opt => opt.MapFrom(c => c.IdAddressPublicHouseNavigation.NumberDormitory ?? $"{c.IdAddressPublicHouseNavigation.City}, {c.IdAddressPublicHouseNavigation.Street}, {c.IdAddressPublicHouseNavigation.NumberHouse}"));
                    
                    map.CreateMap<PositionEmployees, PositionEmployeesDTO>()
                       .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.PositionEmployees)))
                       .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                       .ForMember(x => x.HashIdSubdivision, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdSubdivision, Enums.Services.Subdivision)))
                       .ForMember(x => x.HashIdPosition, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdPosition, Enums.Services.Position)))
                       .ReverseMap()
                       .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.PositionEmployees)))
                       .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)))
                       .ForMember(x => x.IdSubdivision, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdSubdivision, Enums.Services.Subdivision)))
                       .ForMember(x => x.IdPosition, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdPosition, Enums.Services.Position)));

                    map.CreateMap<SocialActivityEmployeesDTO, SocialActivityEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.SocialActivityEmployees)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.IdSocialActivity, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdSocialActivity, Enums.Services.SocialActivity)))
                        .ForMember(x => x.IdSocialActivityNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.SocialActivityEmployees)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.HashIdSocialActivity, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdSocialActivity, Enums.Services.SocialActivity)))
                        .ForMember(x => x.NameSocialActivity, opt => opt.MapFrom(c => c.IdSocialActivityNavigation.Name));

                    map.CreateMap<PrivilegeEmployeesDTO, PrivilegeEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.PrivilegeEmployees)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.IdPrivileges, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdPrivileges, Enums.Services.Privileges)))
                        .ForMember(x => x.IdPrivilegesNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.PrivilegeEmployees)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.HashIdPrivileges, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdPrivileges, Enums.Services.Privileges)))
                        .ForMember(x => x.NamePrivileges, opt => opt.MapFrom(c => c.IdPrivilegesNavigation.Name));

                    map.CreateMap<HobbyEmployeesDTO, HobbyEmployees>()
                        .ForMember(x => x.Id, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashId, Enums.Services.HobbyEmployees)))
                        .ForMember(x => x.IdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.IdHobby, opt => opt.MapFrom(c => _hashIdUtilities.DecryptLong(c.HashIdHobby, Enums.Services.Hobby)))
                        .ForMember(x => x.IdHobbyNavigation, opt => opt.MapFrom(c => _nullValue))
                        .ReverseMap()
                        .ForMember(x => x.HashId, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.HobbyEmployees)))
                        .ForMember(x => x.HashIdEmployee, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdEmployee, Enums.Services.Employee)))
                        .ForMember(x => x.HashIdHobby, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.IdHobby, Enums.Services.Hobby)))
                        .ForMember(x => x.NameHobby, opt => opt.MapFrom(c => c.IdHobbyNavigation.Name));

                    // -- Mapping for PDF service start

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

                    // -- Mapping for PDF service end



                    // -- Mapping for Search by Full Name start

                    map.CreateMap<ResultFullNameSearch, ResultSearchDTO>()
                        .ForMember(x => x.HashIdUser, opt => opt.MapFrom(c => _hashIdUtilities.EncryptLong(c.Id, Enums.Services.Employee)));

                    // -- Mapping for Search by Full Name end

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
                    return _hashIdUtilities.DecryptLong(hashIdDormitory, Enums.Services.AddressPublicHouse);
                case AccommodationType.Departmental:
                    return _hashIdUtilities.DecryptLong(hashIdDepartmental, Enums.Services.AddressPublicHouse);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
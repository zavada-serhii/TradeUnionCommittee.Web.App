using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.DAL.Entities;

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
                        .ForMember(d => d.Type, c => c.UseValue(TypeEvent.Travel));

                    map.CreateMap<Event, WellnessDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Wellness)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Wellness)))
                        .ForMember(d => d.Type, c => c.UseValue(TypeEvent.Wellness));

                    map.CreateMap<Event, TourDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Tour)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Tour)))
                        .ForMember(d => d.Type, c => c.UseValue(TypeEvent.Tour));

                    map.CreateMap<Activities, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Activities)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Activities)));

                    map.CreateMap<Cultural, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Cultural)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Cultural)));

                    map.CreateMap<Subdivisions, SubdivisionDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Subdivision)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Subdivision)))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashIdSubordinate, Enums.Services.Subdivision)));

                    map.CreateMap<AddressPublicHouse, DormitoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Dormitory)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Dormitory)))
                        .ForMember(d => d.Type, c => c.UseValue(TypeHouse.Dormitory));

                    map.CreateMap<AddressPublicHouse, DepartmentalDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _hashIdUtilities.EncryptLong(x.Id, Enums.Services.Departmental)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _hashIdUtilities.DecryptLong(x.HashId, Enums.Services.Departmental)))
                        .ForMember(d => d.Type, c => c.UseValue(TypeHouse.Departmental));

                    map.CreateMap<Scientific, QualificationDTO>()
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => x.Id));


                    //------------------------------------------------------------------------------

                    // -- Mapping for create employee

                    map.CreateMap<Employee, CreateEmployeeDTO>().ReverseMap()
                    .ForMember(d => d.CityPhone, c => c.MapFrom(x => x.CityPhone.AddMaskForCityPhone()));

                    map.CreateMap<Education, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(d => d.IdEmployee, c => c.MapFrom(x => x.IdEmployee));

                    map.CreateMap<PositionEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(d => d.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(d => d.IdSubdivision, c => c.MapFrom(x => x.IdSubdivision))
                        .ForMember(d => d.IdPosition, c => c.MapFrom(x => x.IdPosition))
                        .ForMember(d => d.StartDate, c=> c.MapFrom(x => x.StartDatePosition))
                        .ForMember(d => d.CheckPosition, c => c.UseValue(true));

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
                        .ForMember(x => x.IdAddressPublicHouse, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.Dormitory ? x.IdDormitory : x.IdDepartmental))
                        .ForMember(x => x.NumberRoom, c => c.MapFrom(x => x.TypeAccommodation == AccommodationType.Dormitory ? x.NumberRoomDormitory : x.NumberRoomDepartmental));

                    map.CreateMap<Scientific, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.ScientificDegree, c => c.MapFrom(x => x.ScientifickDegree))
                        .ForMember(x => x.ScientificTitle, c => c.MapFrom(x => x.ScientifickTitle));

                    map.CreateMap<SocialActivityEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdSocialActivity, c => c.MapFrom(x => x.IdSocialActivity))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NoteSocialActivity))
                        .ForMember(d => d.CheckSocialActivity, c => c.UseValue(true));

                    map.CreateMap<PrivilegeEmployees, CreateEmployeeDTO>()
                        .ReverseMap()
                        .ForMember(x => x.IdEmployee, c => c.MapFrom(x => x.IdEmployee))
                        .ForMember(x => x.IdPrivileges, c => c.MapFrom(x => x.IdPrivileges))
                        .ForMember(x => x.Note, c => c.MapFrom(x => x.NotePrivileges))
                        .ForMember(d => d.CheckPrivileges, c => c.UseValue(true));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Education, EducationDTO>().ReverseMap();

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
    }
}
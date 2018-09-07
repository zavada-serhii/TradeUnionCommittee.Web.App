using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Infrastructure
{
    public interface IAutoMapperService
    {
        IMapper Mapper { get; }
    }

    //------------------------------------------------------------------------------

    internal sealed class AutoMapperService : IAutoMapperService
    {
        private readonly ICryptoUtilities _cryptoUtilities;

        public AutoMapperService(ICryptoUtilities cryptoUtilities)
        {
            _cryptoUtilities = cryptoUtilities;
        }

        public IMapper Mapper
        {
            get
            {
                return new MapperConfiguration(map =>
                {
                    map.CreateMap<Users, AccountDTO>()
                        .ForMember(d => d.HashIdUser, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Account)))
                        .ForMember(d => d.Role, c => c.MapFrom(x => ConvertToUkrainianLang(x.IdRoleNavigation.Name)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashIdUser, EnumCryptoUtilities.Account)))
                        .ForMember(d => d.IdRole, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashIdRole, EnumCryptoUtilities.Role)));

                    map.CreateMap<Roles, RolesDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Role)))
                        .ForMember(d => d.Name, c => c.MapFrom(x => ConvertToUkrainianLang(x.Name)))
                        .ReverseMap();

                    //------------------------------------------------------------------------------

                    map.CreateMap<Position, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Position)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Position)));

                    map.CreateMap<SocialActivity, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.SocialActivity)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.SocialActivity)));

                    map.CreateMap<Privileges, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Privileges)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Privileges)));

                    map.CreateMap<Award, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Award)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Award)));

                    map.CreateMap<MaterialAid, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.MaterialAid)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.MaterialAid)));

                    map.CreateMap<Hobby, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Hobby)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Hobby)));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Event, TravelDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Travel)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Travel)))
                        .ForMember(d => d.TypeId, c => c.UseValue(1));

                    map.CreateMap<Event, WellnessDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Wellness)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Wellness)))
                        .ForMember(d => d.TypeId, c => c.UseValue(2));

                    map.CreateMap<Event, TourDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Tour)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Tour)))
                        .ForMember(d => d.TypeId, c => c.UseValue(3));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Activities, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Activities)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Activities)));

                    map.CreateMap<Cultural, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Cultural)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Cultural)));

                    //------------------------------------------------------------------------------

                    map.CreateMap<Subdivisions, SubdivisionDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Subdivision)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Subdivision)))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashIdSubordinate, EnumCryptoUtilities.Subdivision)));

                    //------------------------------------------------------------------------------

                    map.CreateMap<AddressPublicHouse, DormitoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Dormitory)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Dormitory)))
                        .ForMember(d => d.Type, c => c.UseValue(1));

                    map.CreateMap<AddressPublicHouse, DepartmentalDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Departmental)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Departmental)))
                        .ForMember(d => d.Type, c => c.UseValue(2));

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
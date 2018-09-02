using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Infrastructure
{
    public interface IAutoMapperModule
    {
        IMapper Mapper { get; }
    }

    //------------------------------------------------------------------------------

    internal sealed class AutoMapperModule : IAutoMapperModule
    {
        private readonly ICryptoUtilities _cryptoUtilities;

        public AutoMapperModule(ICryptoUtilities cryptoUtilities)
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

                    map.CreateMap<Position, DirectoryDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Position)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Position)));

                    map.CreateMap<Subdivisions, SubdivisionDTO>()
                        .ForMember(d => d.HashId, c => c.MapFrom(x => _cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Subdivision)))
                        .ReverseMap()
                        .ForMember(d => d.Id, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Subdivision)))
                        .ForMember(d => d.IdSubordinate, c => c.MapFrom(x => _cryptoUtilities.DecryptLong(x.HashIdSubordinate, EnumCryptoUtilities.Subdivision)));

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
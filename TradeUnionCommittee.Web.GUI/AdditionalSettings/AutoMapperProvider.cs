using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.Encryption;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.AdditionalSettings
{
    public class AutoMapperProvider
    {
        /// <summary>
        ///     Configures the automatic mapper.
        /// </summary>
        /// <returns>IMapper.</returns>
        public static IMapper ConfigureAutoMapper()
        {
            ICryptoUtilities cryptoUtilities = new CryptoUtilities();
            return new MapperConfiguration(map =>
            {
                //-- Controller Mapper ---------------------------------------------------------------------------------------------------------

                map.CreateMap<AccountDTO, CreateAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateEmailAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdateRoleAccountViewModel>().ReverseMap();
                map.CreateMap<AccountDTO, UpdatePasswordAccountViewModel>().ReverseMap();

                map.CreateMap<AddEmployeeViewModel, AddEmployeeDTO>()
                .ForMember(d => d.IdSubdivision, opt => opt.MapFrom(c => c.SubordinateSubdivision ?? c.MainSubdivision))
                .ForMember(d => d.CityPhone, opt => opt.MapFrom(c => c.CityPhoneAdditional ?? c.CityPhone));

                map.CreateMap<DirectoryDTO, PositionViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, SocialActivityViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, PrivilegesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, AwardViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, MaterialAidViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, HobbyViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, TravelViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, WellnessViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, TourViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, ActivitiesViewModel>().ReverseMap();
                map.CreateMap<DirectoryDTO, CulturalViewModel>().ReverseMap();
                map.CreateMap<DepartmentalDTO, DepartmentalViewModel>().ReverseMap();
                map.CreateMap<DormitoryDTO, DormitoryViewModel>().ReverseMap();
                map.CreateMap<SubdivisionDTO, SubdivisionViewModel>().ForMember(d => d.Name, c => c.MapFrom(x => x.DeptName)).ReverseMap();
                map.CreateMap<SubdivisionDTO, UpdateSubdivisionViewModel>().ForMember(d=> d.Name, c => c.MapFrom(x => x.DeptName)).ReverseMap();
                map.CreateMap<SubdivisionDTO, UpdateAbbreviationSubdivisionViewModel>().ReverseMap();

                map.CreateMap<EducationDTO, UpdateEducationViewModel>().ForMember(d => d.YearReceiving, c => c.MapFrom(x => x.DateReceiving)).ReverseMap();
                map.CreateMap<QualificationDTO, QualificationViewModel>().ReverseMap();

                map.CreateMap<MainInfoEmployeeDTO, MainInfoEmployeeViewModel>().ReverseMap();

                //-- Service Mapper ------------------------------------------------------------------------------------------------------------

                map.CreateMap<Users, AccountDTO>()
                    .ForMember(d => d.HashIdUser, c => c.MapFrom(x => cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Account)))
                    .ForMember(d => d.Role, c => c.MapFrom(x => ConvertToUkrainianLang(x.IdRoleNavigation.Name)))
                    .ReverseMap()
                    .ForMember(d => d.Id, c => c.MapFrom(x => cryptoUtilities.DecryptLong(x.HashIdUser, EnumCryptoUtilities.Account)))
                    .ForMember(d => d.IdRole, c => c.MapFrom(x => cryptoUtilities.DecryptLong(x.HashIdRole, EnumCryptoUtilities.Role)));

                map.CreateMap<Roles, RolesDTO>()
                    .ForMember(d => d.HashId, c => c.MapFrom(x => cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Role)))
                    .ForMember(d => d.Name, c => c.MapFrom(x => ConvertToUkrainianLang(x.Name)))
                    .ReverseMap();

                map.CreateMap<Position, DirectoryDTO>()
                    .ForMember(d => d.HashId, c => c.MapFrom(x => cryptoUtilities.EncryptLong(x.Id, EnumCryptoUtilities.Position)))
                    .ReverseMap()
                    .ForMember(d => d.Id, c => c.MapFrom(x => cryptoUtilities.DecryptLong(x.HashId, EnumCryptoUtilities.Position)));

            }).CreateMapper();
        }

        private static string ConvertToUkrainianLang(string param)
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
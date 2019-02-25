using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;

        public AccountService(IUnitOfWork database, IAutoMapperConfiguration mapperService)
        {
            _database = database;
            _mapperService = mapperService;
        }

        public async Task<ActualResult> Login(string email, string password, bool rememberMe, Enums.TypeAuthorization type)
        {
            var result = await _database.AccountRepository.Login(email, password, rememberMe, (AuthorizationType)type);
            return result.IsValid ? new ActualResult() : new ActualResult<string>(Errors.InvalidLoginOrPassword);
        }

        public async Task<ActualResult> LogOff()
        {
            var result = await _database.AccountRepository.LogOff();
            return result.IsValid ? new ActualResult() : new ActualResult(Errors.DataBaseError);
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllUsersAsync()
        {
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<AccountDTO>>>(await _database.AccountRepository.GetAllUsersAsync());
        }

        public async Task<ActualResult<AccountDTO>> GetUserAsync(string hashId)
        {
            return _mapperService.Mapper.Map<ActualResult<AccountDTO>>( await _database.AccountRepository.GetUserAsync(hashId));
        }

        public async Task<ActualResult<string>> GetRoleByEmailAsync(string email)
        {
            return _mapperService.Mapper.Map<ActualResult<string>>(await _database.AccountRepository.GetRoleByEmailAsync(email));
        }

        public async Task<ActualResult> CreateUserAsync(AccountDTO dto)
        {
            if (!await CheckEmailAsync(dto.Email))
            {
                var user = new User
                {
                    Email = dto.Email,
                    UserName = dto.Email,
                    Password = dto.Password,
                    UserRole = ConvertToEnglishLang(dto.Role)
                };
                return await _database.AccountRepository.CreateUserAsync(user);
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateUserEmailAsync(AccountDTO dto)
        {
            if (!await CheckEmailAsync(dto.Email))
            {
                return await _database.AccountRepository.UpdateUserEmailAsync(_mapperService.Mapper.Map<User>(dto));
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateUserPasswordAsync(AccountDTO dto)
        {
            return await _database.AccountRepository.UpdateUserPasswordAsync(_mapperService.Mapper.Map<User>(dto));
        }

        public async Task<ActualResult> UpdateUserRoleAsync(AccountDTO dto)
        {
            return await _database.AccountRepository.UpdateUserRoleAsync(dto.HashIdUser, ConvertToEnglishLang(dto.Role));
        }

        public async Task<ActualResult> DeleteUserAsync(string hashId)
        {
            return await _database.AccountRepository.DeleteUserAsync(hashId);
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var result = await _database.AccountRepository.CheckEmailAsync(email);
            return result.IsValid;
        }

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles()
        {
           return _mapperService.Mapper.Map<ActualResult<IEnumerable<RolesDTO>>>( await _database.AccountRepository.GetAllRolesAsync());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private string ConvertToEnglishLang(string param)
        {
            switch (param)
            {
                case "Адміністратор":
                    return "Admin";
                case "Бухгалтер":
                    return "Accountant";
                case "Заступник":
                    return "Deputy";
                default:
                    return string.Empty;
            }
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
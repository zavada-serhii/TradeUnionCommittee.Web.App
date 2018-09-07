using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;
        private readonly ICheckerService _checkerService;

        public AccountService(IUnitOfWork database, IAutoMapperService mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<string>> Login(string email, string password)
        {
            var role = await _database.UsersRepository.Login(email, password);
            return role.IsValid ? new ActualResult<string> { Result = role.Result } : new ActualResult<string>(Errors.InvalidLoginOrPassword);
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllUsersAsync() => 
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<AccountDTO>>>(_database.UsersRepository.GetAllUsers()));


        public async Task<ActualResult<AccountDTO>> GetUserAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, BL.Services.Account);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<AccountDTO>>(_database.UsersRepository.GetUser(check.Result))
                : new ActualResult<AccountDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateUserAsync(AccountDTO dto)
        {
            var checkRole = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdRole, BL.Services.Role, false);
            if (!await CheckEmailAsync(dto.Email))
            {
                if (checkRole.IsValid)
                {
                    _database.UsersRepository.CreateUser(_mapperService.Mapper.Map<Users>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(checkRole.ErrorsList);
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateUserEmailAsync(AccountDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdUser, BL.Services.Account);
            if (check.IsValid)
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    _database.UsersRepository.UpdateUserEmail(_mapperService.Mapper.Map<Users>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateUserPasswordAsync(AccountDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdUser, BL.Services.Account);
            if (check.IsValid)
            {
                _database.UsersRepository.UpdateUserPassword(_mapperService.Mapper.Map<Users>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateUserRoleAsync(AccountDTO dto)
        {
            var checkUser = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdUser, BL.Services.Account);
            var checkRole = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdRole, BL.Services.Role, false);
            if (checkUser.IsValid && checkRole.IsValid)
            {
                _database.UsersRepository.UpdateUserRole(_mapperService.Mapper.Map<Users>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(new List<string> { checkUser.ErrorsList.FirstOrDefault(), checkRole.ErrorsList.FirstOrDefault() });
        }

        public async Task<ActualResult> DeleteUserAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, BL.Services.Account, false);
            if (check.IsValid)
            {
                _database.UsersRepository.DeleteUser(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckEmailAsync(string email) =>
            await Task.Run(() => _database.UsersRepository.FindUsers(p => p.Email == email).Result.Any());

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles() => 
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<RolesDTO>>>(_database.UsersRepository.GetAllRoles()));

        public async Task<ActualResult<RolesDTO>> GetRoleId(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, BL.Services.Role, false);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<RolesDTO>>(_database.UsersRepository.GetRole(check.Result))
                : new ActualResult<RolesDTO>(check.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
       
        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
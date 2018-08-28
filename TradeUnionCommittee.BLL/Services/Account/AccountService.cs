using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        private readonly ICryptoUtilities _cryptoUtilities;

        public AccountService(IUnitOfWork database, ICryptoUtilities cryptoUtilities, IMapper mapper)
        {
            _database = database;
            _cryptoUtilities = cryptoUtilities;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllUsersAsync() => 
            await Task.Run(() => _mapper.Map<ActualResult<IEnumerable<AccountDTO>>>(_database.UsersRepository.GetAllUsers()));


        public async Task<ActualResult<AccountDTO>> GetUserAsync(string hashId)
        {
            var check = await CheckUserDecryptAndTupleInDb(hashId);
            return check.IsValid
                ? _mapper.Map<ActualResult<AccountDTO>>(_database.UsersRepository.GetUser(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Account)))
                : new ActualResult<AccountDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateUserAsync(AccountDTO dto)
        {
            var checkRole = await CheckRoleDecrypt(dto.HashIdRole);
            if (!await CheckEmailAsync(dto.Email))
            {
                if (checkRole.IsValid)
                {
                    _database.UsersRepository.CreateUser(_mapper.Map<Users>(dto));
                    return _mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(checkRole.ErrorsList);
            }
            return new ActualResult("0004");
        }

        public async Task<ActualResult> UpdateUserEmailAsync(AccountDTO dto)
        {
            var check = await CheckUserDecryptAndTupleInDb(dto.HashIdUser);
            if (check.IsValid)
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    _database.UsersRepository.UpdateUserEmail(_mapper.Map<Users>(dto));
                    return _mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult("0004");
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateUserPasswordAsync(AccountDTO dto)
        {
            var check = await CheckUserDecryptAndTupleInDb(dto.HashIdUser);
            if (check.IsValid)
            {
                _database.UsersRepository.UpdateUserPassword(_mapper.Map<Users>(dto));
                return _mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateUserRoleAsync(AccountDTO dto)
        {
            var checkUser = await CheckUserDecryptAndTupleInDb(dto.HashIdUser);
            var checkRole = await CheckRoleDecrypt(dto.HashIdRole);
            if (checkUser.IsValid && checkRole.IsValid)
            {
                _database.UsersRepository.UpdateUserRole(_mapper.Map<Users>(dto));
                return _mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(new List<string> { checkUser.ErrorsList.FirstOrDefault(), checkRole.ErrorsList.FirstOrDefault() });
        }

        public async Task<ActualResult> DeleteUserAsync(string hashId)
        {
            var check = await CheckUserDecryptAndTupleInDb(hashId, false);
            if (check.IsValid)
            {
                _database.UsersRepository.DeleteUser(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Account));
                return _mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckEmailAsync(string email) =>
            await Task.Run(() => _database.UsersRepository.FindUsers(p => p.Email == email).Result.Any());

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles() => 
            await Task.Run(() => _mapper.Map<ActualResult<IEnumerable<RolesDTO>>>(_database.UsersRepository.GetAllRoles()));

        public async Task<ActualResult<RolesDTO>> GetRoleId(string hashId)
        {
            var check = await CheckRoleDecrypt(hashId);
            return check.IsValid
                ? _mapper.Map<ActualResult<RolesDTO>>(_database.UsersRepository.GetRole(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Role)))
                : new ActualResult<RolesDTO>(check.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
       
        public void Dispose()
        {
            _database.Dispose();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task<ActualResult> CheckUserDecryptAndTupleInDb(string hashId, bool checkTuple = true) => await Task.Run(() =>
        {
            if (_cryptoUtilities.CheckDecrypt(hashId, EnumCryptoUtilities.Account))
            {
                if (checkTuple)
                {
                    var id = _cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Account);
                    if (_database.UsersRepository.FindUsers(x => x.Id == id).Result.Any())
                    {
                        return new ActualResult();
                    }
                    return new ActualResult("0001");
                }
                return new ActualResult();
            }
            return new ActualResult("0003");
        });

        private async Task<ActualResult> CheckRoleDecrypt(string hashId) => 
            await Task.Run(() => _cryptoUtilities.CheckDecrypt(hashId, EnumCryptoUtilities.Role) ? new ActualResult() : new ActualResult("0003"));
    }
}
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
        private readonly ICryptoUtilities _cryptoUtilities;

        public AccountService(IUnitOfWork database, ICryptoUtilities cryptoUtilities)
        {
            _database = database;
            _cryptoUtilities = cryptoUtilities;
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var actualResults = new ActualResult<IEnumerable<AccountDTO>>();

                var roles =  _database.RolesRepository.GetAll();
                var users = _database.UsersRepository.GetAll();

                actualResults.Result = (from r in roles.Result
                    join u in users.Result
                    on r.Id equals u.IdRole
                    select new AccountDTO
                    {
                        HashIdUser = _cryptoUtilities.EncryptLong(u.Id,EnumCryptoUtilities.Account),
                        Email = u.Email,
                        Role = ConvertRoleToUkrainianLang(r.Name)
                    }).ToList();

                return actualResults;
            });
        }

        public Task<ActualResult<AccountDTO>> GetAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActualResult<AccountDTO>> GetAsync(string hashId)
        {
            return await Task.Run(() =>
            {
                var id = _cryptoUtilities.DecryptLong(hashId,EnumCryptoUtilities.Account);

                var user = _database.UsersRepository.Get(id);
                if (user.IsValid == false && user.ErrorsList.Count > 0 || user.Result == null)
                {
                    return new ActualResult<AccountDTO> { IsValid = false, ErrorsList = user.ErrorsList };
                }
                return new ActualResult<AccountDTO>
                {
                    Result = new AccountDTO
                    {
                        HashIdUser = _cryptoUtilities.EncryptLong(user.Result.Id,EnumCryptoUtilities.Account),
                        Email = user.Result.Email,
                        IdRole = user.Result.IdRole
                    }
                };
            });
        }

        public async Task<ActualResult> CreateAsync(AccountDTO item)
        {
            var users = _database.UsersRepository.Create(new Users
            {
                Email = item.Email,
                Password = HashingPassword.HashPassword(item.Password),
                IdRole = item.IdRole
            });

            if (users.IsValid && users.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = users.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            users.IsValid = dbState.IsValid;
            return users;
        }

        public async Task<ActualResult> UpdateAsync(AccountDTO item)
        {
            var result = new ActualResult();

            var id = _cryptoUtilities.DecryptLong(item.HashIdUser,EnumCryptoUtilities.Account);

            if (item.IdRole != 0)
            {
                var user = await GetAsync(item.HashIdUser);
                result = _database.UsersRepository.Update(new Users
                {
                    Id = id,
                    IdRole = item.IdRole,
                    Email = user.Result.Email,
                    Password = user.Result.Password
                });
            }

            if (item.Email != null)
            {
                var user = await GetAsync(item.HashIdUser);
                result = _database.UsersRepository.Update(new Users
                {
                    Id = id,
                    Email = item.Email,
                    Password = user.Result.Password,
                    IdRole = user.Result.IdRole
                });
            }

            if (item.Password != null)
            {
                var user = await GetAsync(item.HashIdUser);
                result = _database.UsersRepository.Update(new Users
                {
                    Id = id,
                    Password = HashingPassword.HashPassword(item.Password),
                    Email = user.Result.Email,
                    IdRole = user.Result.IdRole
                });
            }

            if (result.IsValid == false && result.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = result.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            result.IsValid = dbState.IsValid;
            return result;
        }

        public Task<ActualResult> DeleteAsync(long id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var id = _cryptoUtilities.DecryptLong(hashId,EnumCryptoUtilities.Account);
            var user = _database.UsersRepository.Delete(id);
            if (user.IsValid == false && user.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = user.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            user.IsValid = dbState.IsValid;
            return user;
        }

        public async Task<ActualResult> CheckEmail(string email)
        {
            return await Task.Run(() =>
            {
                var res = _database.UsersRepository.Find(p => p.Email == email);
                return res.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles()
        {
            return await Task.Run(() =>
            {
                var dtos = new List<RolesDTO>();
                var collection = _database.RolesRepository.GetAll().Result;
                foreach (var v in collection)
                {
                    switch (v.Name)
                    {
                        case "Admin":
                            dtos.Add(new RolesDTO { Id = v.Id, Name = "Адміністратор" });
                            break;
                        case "Accountant":
                            dtos.Add(new RolesDTO { Id = v.Id, Name = "Бухгалтер" });
                            break;
                        case "Deputy":
                            dtos.Add(new RolesDTO { Id = v.Id, Name = "Заступник" });
                            break;
                    }
                }
                return new ActualResult<IEnumerable<RolesDTO>> {Result = dtos};
            });
        }

        public async Task<ActualResult<RolesDTO>> GetRoleId(long id)
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Roles, RolesDTO>()).CreateMapper();
                return mapper.Map<ActualResult<Roles>, ActualResult<RolesDTO>>(_database.RolesRepository.Get(id));
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private string ConvertRoleToUkrainianLang(string s)
        {
            switch (s)
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

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
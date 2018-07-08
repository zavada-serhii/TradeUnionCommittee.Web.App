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

        public AccountService(IUnitOfWork database)
        {
            _database = database;
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
                        IdUser = u.Id,
                        Email = u.Email,
                        Role = r.Name
                    }).ToList();

                return actualResults;
            });
        }

        public async Task<ActualResult<AccountDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var user = _database.UsersRepository.Get(id);
                if (user.IsValid == false && user.ErrorsList.Count > 0 || user.Result == null)
                {
                    return new ActualResult<AccountDTO> { IsValid = false, ErrorsList = user.ErrorsList };
                }
                return new ActualResult<AccountDTO>
                {
                    Result = new AccountDTO
                    {
                        IdUser = user.Result.Id,
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
            ActualResult result;

            if (item.Password == null)
            {
                result = _database.UsersRepository.Update(new Users
                {
                    Id = item.IdUser,
                    Email = item.Email,
                    IdRole = item.IdRole
                });
            }
            else
            {
                result = _database.UsersRepository.Update(new Users
                {
                    Id = item.IdUser,
                    Password = HashingPassword.HashPassword(item.Password)
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

        public async Task<ActualResult> DeleteAsync(long id)
        {
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
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Roles, RolesDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Roles>>, ActualResult<IEnumerable<RolesDTO>>>(_database.RolesRepository.GetAll());
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

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
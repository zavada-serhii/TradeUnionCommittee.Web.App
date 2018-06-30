using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _database;

        public AccountService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<AccountsDTO>>> GetAll()
        {
            return await Task.Run(() =>
            {
                var actualResults = new ActualResult<IEnumerable<AccountsDTO>>();

                var roles =  _database.RolesRepository.GetAll();
                var users = _database.UsersRepository.GetAll();

                actualResults.Result = (from r in roles.Result
                    join u in users.Result
                    on r.Id equals u.IdRole
                    select new AccountsDTO
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Role = r.Name
                    }).ToList();

                return actualResults;
            });
        }

        public async Task<ActualResult<AccountsDTO>> Get(long id)
        {
            return await Task.Run(() =>
            {
                var user = _database.UsersRepository.Get(id);
                if (user.IsValid == false && user.ErrorsList.Count > 0 || user.Result == null)
                {
                    return new ActualResult<AccountsDTO> { IsValid = false, ErrorsList = user.ErrorsList };
                }
                return new ActualResult<AccountsDTO>
                {
                    Result = new AccountsDTO
                    {
                        Id = user.Result.Id,
                        Email = user.Result.Email,
                        IdRole = user.Result.IdRole
                    }
                };
            });
        }

        public async Task<ActualResult> Create(AccountsDTO item)
        {
            return await Task.Run(async() =>
            {
                var users = _database.UsersRepository.Create(new Users
                {
                    Email = item.Email,
                    Password = item.Password,
                    IdRole = item.IdRole
                });

                if (users.IsValid && users.ErrorsList.Count > 0)
                {
                    return new ActualResult {IsValid = false, ErrorsList = users.ErrorsList};
                }
                await _database.SaveAsync();
                return users;
            });
        }

        public async Task<ActualResult> Update(AccountsDTO item)
        {
            return await Task.Run(async () =>
            {
                var user = _database.UsersRepository.Update(new Users
                {
                    Id = item.Id,
                    Email = item.Email,
                    IdRole = item.IdRole,
                    Password = "UpdatePersonalInfo"
                });
                if (user.IsValid == false && user.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = user.ErrorsList };
                }
                await _database.SaveAsync();
                return user;
            });
        }

        public async Task<ActualResult> Delete(long id)
        {
            return await Task.Run(async () =>
            {
                var user = _database.UsersRepository.Delete(id);
                if (user.IsValid == false && user.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = user.ErrorsList };
                }
                await _database.SaveAsync();
                return user;
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

        public async Task<ActualResult<AccountsDTO>> Login(string login, string password)
        {
            return await Task.Run(() =>
            {
                var actualResults = new ActualResult<AccountsDTO>();

                var roles = _database.RolesRepository.GetAll();
                var users = _database.UsersRepository.Find(x => x.Email == login && x.Password == password);

                var res = from r in roles.Result
                    join u in users.Result
                    on r.Id equals u.IdRole
                    select  new
                    {
                        Email = u.Email,
                        Role = r.Name
                    };

                foreach (var re in res)
                {
                    actualResults.Result.Email = re.Email;
                    actualResults.Result.Role = re.Role;
                }

                return actualResults;
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
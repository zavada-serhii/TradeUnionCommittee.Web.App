using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class PrivilegesService : IPrivilegesService
    {
        private readonly IUnitOfWork _database;

        public PrivilegesService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Privileges, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Privileges>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.PrivilegesRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var privileges = _database.PrivilegesRepository.Get(id);
                if (privileges.IsValid == false && privileges.ErrorsList.Count > 0 || privileges.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = privileges.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = privileges.Result.Id, Name = privileges.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var privileges = _database.PrivilegesRepository.Create(new Privileges { Name = item.Name });
            if (privileges.IsValid == false && privileges.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = privileges.ErrorsList };
            }
            await _database.SaveAsync();
            return privileges;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var privileges = _database.PrivilegesRepository.Update(new Privileges { Id = item.Id, Name = item.Name });
            if (privileges.IsValid == false && privileges.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = privileges.ErrorsList };
            }
            await _database.SaveAsync();
            return privileges;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var privileges = _database.PrivilegesRepository.Delete(id);
            if (privileges.IsValid == false && privileges.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = privileges.ErrorsList };
            }
            await _database.SaveAsync();
            return privileges;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var privileges = _database.PrivilegesRepository.Find(p => p.Name == name);
                return privileges.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
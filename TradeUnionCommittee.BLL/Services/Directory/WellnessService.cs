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
    public class WellnessService : IWellnessService
    {
        private readonly IUnitOfWork _database;

        public WellnessService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Event, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Event>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.EventRepository.Find(x => x.TypeId == 2));
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var wellness = _database.EventRepository.Get(id);
                if (wellness.IsValid == false && wellness.ErrorsList.Count > 0 || wellness.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = wellness.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = wellness.Result.Id, Name = wellness.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var wellness = _database.EventRepository.Create(new Event { Name = item.Name, TypeId = 2 });
            if (wellness.IsValid == false && wellness.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = wellness.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            wellness.IsValid = dbState.IsValid;
            return wellness;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var wellness = _database.EventRepository.Update(new Event { Id = item.Id, Name = item.Name, TypeId = 2 });
            if (wellness.IsValid == false && wellness.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = wellness.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            wellness.IsValid = dbState.IsValid;
            return wellness;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var wellness = _database.EventRepository.Delete(id);
            if (wellness.IsValid == false && wellness.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = wellness.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            wellness.IsValid = dbState.IsValid;
            return wellness;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var wellness = _database.EventRepository.Find(p => p.Name == name && p.TypeId == 2);
                return wellness.Result.Any() ?
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
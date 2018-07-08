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
    public class TravelService : ITravelService
    {
        private readonly IUnitOfWork _database;

        public TravelService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Event, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Event>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.EventRepository.Find(x => x.TypeId == 1));
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var travel = _database.EventRepository.Get(id);
                if (travel.IsValid == false && travel.ErrorsList.Count > 0 || travel.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = travel.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = travel.Result.Id, Name = travel.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var travel = _database.EventRepository.Create(new Event { Name = item.Name, TypeId = 1});
            if (travel.IsValid == false && travel.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = travel.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            travel.IsValid = dbState.IsValid;
            return travel;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var travel = _database.EventRepository.Update(new Event { Id = item.Id, Name = item.Name, TypeId = 1});
            if (travel.IsValid == false && travel.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = travel.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            travel.IsValid = dbState.IsValid;
            return travel;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var travel = _database.EventRepository.Delete(id);
            if (travel.IsValid == false && travel.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = travel.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            travel.IsValid = dbState.IsValid;
            return travel;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var travel = _database.EventRepository.Find(p => p.Name == name && p.TypeId == 1);
                return travel.Result.Any() ?
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
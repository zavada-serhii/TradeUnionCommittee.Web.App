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
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _database;

        public TourService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Event, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Event>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.EventRepository.Find(x => x.TypeId == 3));
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var tour = _database.EventRepository.Get(id);
                if (tour.IsValid == false && tour.ErrorsList.Count > 0 || tour.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = tour.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = tour.Result.Id, Name = tour.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var tour = _database.EventRepository.Create(new Event { Name = item.Name, TypeId = 3 });
            if (tour.IsValid == false && tour.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = tour.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            tour.IsValid = dbState.IsValid;
            return tour;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var tour = _database.EventRepository.Update(new Event { Id = item.Id, Name = item.Name, TypeId = 3 });
            if (tour.IsValid == false && tour.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = tour.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            tour.IsValid = dbState.IsValid;
            return tour;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var tour = _database.EventRepository.Delete(id);
            if (tour.IsValid == false && tour.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = tour.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            tour.IsValid = dbState.IsValid;
            return tour;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var tour = _database.EventRepository.Find(p => p.Name == name && p.TypeId == 3);
                return tour.Result.Any() ?
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
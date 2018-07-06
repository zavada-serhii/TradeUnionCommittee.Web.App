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
    public class ActivitiesService : IActivitiesService
    {
        private readonly IUnitOfWork _database;

        public ActivitiesService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Activities, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Activities>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.ActivitiesRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var activities = _database.ActivitiesRepository.Get(id);
                if (activities.IsValid == false && activities.ErrorsList.Count > 0 || activities.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = activities.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = activities.Result.Id, Name = activities.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var activities = _database.ActivitiesRepository.Create(new Activities { Name = item.Name });
            if (activities.IsValid == false && activities.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = activities.ErrorsList };
            }
            await _database.SaveAsync();
            return activities;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var activities = _database.ActivitiesRepository.Update(new Activities { Id = item.Id, Name = item.Name });
            if (activities.IsValid == false && activities.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = activities.ErrorsList };
            }
            await _database.SaveAsync();
            return activities;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var activities = _database.ActivitiesRepository.Delete(id);
            if (activities.IsValid == false && activities.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = activities.ErrorsList };
            }
            await _database.SaveAsync();
            return activities;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var activities = _database.ActivitiesRepository.Find(p => p.Name == name);
                return activities.Result.Any() ?
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
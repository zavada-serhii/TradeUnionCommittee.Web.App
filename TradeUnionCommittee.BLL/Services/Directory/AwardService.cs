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
    public class AwardService : IAwardService
    {
        private readonly IUnitOfWork _database;

        public AwardService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Award, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Award>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.AwardRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var award = _database.AwardRepository.Get(id);
                if (award.IsValid == false && award.ErrorsList.Count > 0 || award.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = award.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = award.Result.Id, Name = award.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var award = _database.AwardRepository.Create(new Award { Name = item.Name });
            if (award.IsValid == false && award.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = award.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            award.IsValid = dbState.IsValid;
            return award;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var award = _database.AwardRepository.Update(new Award { Id = item.Id, Name = item.Name });
            if (award.IsValid == false && award.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = award.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            award.IsValid = dbState.IsValid;
            return award;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var award = _database.AwardRepository.Delete(id);
            if (award.IsValid == false && award.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = award.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            award.IsValid = dbState.IsValid;
            return award;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var award = _database.AwardRepository.Find(p => p.Name == name);
                return award.Result.Any() ?
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
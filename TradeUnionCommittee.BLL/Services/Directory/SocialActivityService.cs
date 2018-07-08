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
    public class SocialActivityService : ISocialActivityService
    {
        private readonly IUnitOfWork _database;

        public SocialActivityService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<SocialActivity, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<SocialActivity>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.SocialActivityRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var socialActivity = _database.SocialActivityRepository.Get(id);
                if (socialActivity.IsValid == false && socialActivity.ErrorsList.Count > 0 || socialActivity.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = socialActivity.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = socialActivity.Result.Id, Name = socialActivity.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var socialActivity = _database.SocialActivityRepository.Create(new SocialActivity { Name = item.Name });
            if (socialActivity.IsValid == false && socialActivity.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = socialActivity.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            socialActivity.IsValid = dbState.IsValid;
            return socialActivity;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var socialActivity = _database.SocialActivityRepository.Update(new SocialActivity { Id = item.Id, Name = item.Name });
            if (socialActivity.IsValid == false && socialActivity.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = socialActivity.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            socialActivity.IsValid = dbState.IsValid;
            return socialActivity;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var socialActivity = _database.SocialActivityRepository.Delete(id);
            if (socialActivity.IsValid == false && socialActivity.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = socialActivity.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            socialActivity.IsValid = dbState.IsValid;
            return socialActivity;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var socialActivity = _database.SocialActivityRepository.Find(p => p.Name == name);
                return socialActivity.Result.Any() ?
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
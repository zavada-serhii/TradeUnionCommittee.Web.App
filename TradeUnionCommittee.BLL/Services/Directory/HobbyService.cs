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
    public class HobbyService : IHobbyService
    {
        private readonly IUnitOfWork _database;

        public HobbyService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Hobby, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Hobby>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.HobbyRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var hobby = _database.HobbyRepository.Get(id);
                if (hobby.IsValid == false && hobby.ErrorsList.Count > 0 || hobby.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = hobby.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = hobby.Result.Id, Name = hobby.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var hobby = _database.HobbyRepository.Create(new Hobby { Name = item.Name });
            if (hobby.IsValid == false && hobby.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = hobby.ErrorsList };
            }
            await _database.SaveAsync();
            return hobby;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var hobby = _database.HobbyRepository.Update(new Hobby { Id = item.Id, Name = item.Name });
            if (hobby.IsValid == false && hobby.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = hobby.ErrorsList };
            }
            await _database.SaveAsync();
            return hobby;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var hobby = _database.HobbyRepository.Delete(id);
            if (hobby.IsValid == false && hobby.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = hobby.ErrorsList };
            }
            await _database.SaveAsync();
            return hobby;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var hobby = _database.HobbyRepository.Find(p => p.Name == name);
                return hobby.Result.Any() ?
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
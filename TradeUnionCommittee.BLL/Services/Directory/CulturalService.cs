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
    public class CulturalService : ICulturalService
    {
        private readonly IUnitOfWork _database;

        public CulturalService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Cultural, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Cultural>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.CulturalRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var cultural = _database.CulturalRepository.Get(id);
                if (cultural.IsValid == false && cultural.ErrorsList.Count > 0 || cultural.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = cultural.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = cultural.Result.Id, Name = cultural.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var cultural = _database.CulturalRepository.Create(new Cultural { Name = item.Name });
            if (cultural.IsValid == false && cultural.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = cultural.ErrorsList };
            }
            await _database.SaveAsync();
            return cultural;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var cultural = _database.CulturalRepository.Update(new Cultural { Id = item.Id, Name = item.Name });
            if (cultural.IsValid == false && cultural.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = cultural.ErrorsList };
            }
            await _database.SaveAsync();
            return cultural;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var cultural = _database.CulturalRepository.Delete(id);
            if (cultural.IsValid == false && cultural.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = cultural.ErrorsList };
            }
            await _database.SaveAsync();
            return cultural;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var cultural = _database.CulturalRepository.Find(p => p.Name == name);
                return cultural.Result.Any() ?
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
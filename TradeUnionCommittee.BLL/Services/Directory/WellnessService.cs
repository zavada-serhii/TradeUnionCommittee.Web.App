using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class WellnessService : IWellnessService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public WellnessService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<WellnessDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<WellnessDTO>>>(await _database.EventRepository.Find(x => x.Type == TypeEvent.Wellness));

        public async Task<ActualResult<WellnessDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Wellness);
            return _mapperService.Mapper.Map<ActualResult<WellnessDTO>>(await _database.EventRepository.GetById(id));
        }

        public async Task<ActualResult> CreateAsync(WellnessDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.EventRepository.Create(_mapperService.Mapper.Map<Event>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(WellnessDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.EventRepository.Update(_mapperService.Mapper.Map<Event>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.EventRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Wellness));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.EventRepository.Find(p => p.Name == name && p.Type == TypeEvent.Wellness);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
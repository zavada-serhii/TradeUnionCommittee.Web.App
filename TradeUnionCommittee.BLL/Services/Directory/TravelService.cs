using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class TravelService : ITravelService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public TravelService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TravelDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<TravelDTO>>>(await _database.EventRepository.Find(x => x.Type == TypeEvent.Travel));

        public async Task<ActualResult<TravelDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Travel);
            return _mapperService.Mapper.Map<ActualResult<TravelDTO>>(await _database.EventRepository.Get(id));
        }

        public async Task<ActualResult> CreateAsync(TravelDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.EventRepository.Create(_mapperService.Mapper.Map<Event>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(TravelDTO dto)
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
            await _database.EventRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Travel));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.EventRepository.Find(p => p.Name == name && p.Type == TypeEvent.Travel);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
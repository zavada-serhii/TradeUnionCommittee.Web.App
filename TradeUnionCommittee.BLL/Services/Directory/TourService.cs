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
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public TourService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<TourDTO>>> GetAllAsync()
        {
            var tour = await _database.EventRepository.FindWithOrderBy(x => x.Type == TypeEvent.Tour, c => c.Name);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<TourDTO>>>(tour);
        }

        public async Task<ActualResult<TourDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Tour);
            return _mapperService.Mapper.Map<ActualResult<TourDTO>>(await _database.EventRepository.GetById(id));
        }

        public async Task<ActualResult> CreateAsync(TourDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.EventRepository.Create(_mapperService.Mapper.Map<Event>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(TourDTO dto)
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
            await _database.EventRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Tour));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.EventRepository.Any(p => p.Name == name && p.Type == TypeEvent.Tour);
            return result.Result;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
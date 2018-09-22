using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
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
        private readonly ICheckerService _checkerService;

        public TravelService(IUnitOfWork database, IAutoMapperUtilities mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<IEnumerable<TravelDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<TravelDTO>>>(await _database.EventRepository.Find(x => x.TypeId == 1));

        public async Task<ActualResult<TravelDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptWithId(hashId, Enums.Services.Travel);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<TravelDTO>>(await _database.EventRepository.Get(check.Result))
                : new ActualResult<TravelDTO>(check.ErrorsList);
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
            var check = await _checkerService.CheckDecryptWithId(dto.HashId, Enums.Services.Travel);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    await _database.EventRepository.Update(_mapperService.Mapper.Map<Event>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptWithId(hashId, Enums.Services.Travel);
            if (check.IsValid)
            {
                await _database.EventRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.EventRepository.Find(p => p.Name == name && p.TypeId == 1);
            return result.Result.Any();
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
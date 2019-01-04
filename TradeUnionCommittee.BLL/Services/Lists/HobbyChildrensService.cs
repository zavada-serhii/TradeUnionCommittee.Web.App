using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists
{
    public class HobbyChildrensService : IHobbyChildrensService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public HobbyChildrensService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyChildrensDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.HobbyChildrensRepository.GetWithIncludeToList(x => x.IdChildren == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<HobbyChildrensDTO>>>(result);
        }

        public async Task<ActualResult<HobbyChildrensDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyChildrens);
            var result = await _database.HobbyChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<HobbyChildrensDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(HobbyChildrensDTO item)
        {
            await _database.HobbyChildrensRepository.Create(_mapperService.Mapper.Map<HobbyChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(HobbyChildrensDTO item)
        {
            await _database.HobbyChildrensRepository.Update(_mapperService.Mapper.Map<HobbyChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.HobbyChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyChildrens));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
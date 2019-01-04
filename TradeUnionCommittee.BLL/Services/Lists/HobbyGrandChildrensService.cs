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
    public class HobbyGrandChildrensService : IHobbyGrandChildrensService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public HobbyGrandChildrensService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyGrandChildrensDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.HobbyGrandChildrensRepository.GetWithIncludeToList(x => x.IdGrandChildren == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<HobbyGrandChildrensDTO>>>(result);
        }

        public async Task<ActualResult<HobbyGrandChildrensDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyGrandChildrens);
            var result = await _database.HobbyGrandChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<HobbyGrandChildrensDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(HobbyGrandChildrensDTO item)
        {
            await _database.HobbyGrandChildrensRepository.Create(_mapperService.Mapper.Map<HobbyGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(HobbyGrandChildrensDTO item)
        {
            await _database.HobbyGrandChildrensRepository.Update(_mapperService.Mapper.Map<HobbyGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.HobbyGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyGrandChildrens));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
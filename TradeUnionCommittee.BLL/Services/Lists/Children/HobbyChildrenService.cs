using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Children;
using TradeUnionCommittee.BLL.Interfaces.Lists.Children;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.Children
{
    public class HobbyChildrenService : IHobbyChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public HobbyChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.HobbyChildrensRepository.GetWithIncludeToList(x => x.IdChildren == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<HobbyChildrenDTO>>>(result);
        }

        public async Task<ActualResult<HobbyChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyChildren);
            var result = await _database.HobbyChildrensRepository.GetWithInclude(x => x.Id == id, c => c.IdHobbyNavigation);
            return _mapperService.Mapper.Map<ActualResult<HobbyChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(HobbyChildrenDTO item)
        {
            await _database.HobbyChildrensRepository.Create(_mapperService.Mapper.Map<HobbyChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(HobbyChildrenDTO item)
        {
            await _database.HobbyChildrensRepository.Update(_mapperService.Mapper.Map<HobbyChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.HobbyChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.HobbyChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
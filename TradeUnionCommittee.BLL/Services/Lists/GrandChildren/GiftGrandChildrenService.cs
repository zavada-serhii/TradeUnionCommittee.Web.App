using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Lists.GrandChildren
{
    public class GiftGrandChildrenService : IGiftGrandChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public GiftGrandChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GiftGrandChildrenDTO>>> GetAllAsync(string hashIdGrandChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdGrandChildren, Enums.Services.GrandChildren);
            var result = await _database.GiftGrandChildrensRepository.Find(x => x.IdGrandChildren == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<GiftGrandChildrenDTO>>>(result);
        }

        public async Task<ActualResult<GiftGrandChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftGrandChildren);
            var result = await _database.GiftGrandChildrensRepository.GetById(id);
            return _mapperService.Mapper.Map<ActualResult<GiftGrandChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(GiftGrandChildrenDTO item)
        {
            await _database.GiftGrandChildrensRepository.Create(_mapperService.Mapper.Map<GiftGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(GiftGrandChildrenDTO item)
        {
            await _database.GiftGrandChildrensRepository.Update(_mapperService.Mapper.Map<GiftGrandChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.GiftGrandChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftGrandChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
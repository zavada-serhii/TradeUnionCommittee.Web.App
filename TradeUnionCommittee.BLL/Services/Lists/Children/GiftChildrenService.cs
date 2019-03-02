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
    public class GiftChildrenService : IGiftChildrenService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public GiftChildrenService(IUnitOfWork database, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<GiftChildrenDTO>>> GetAllAsync(string hashIdChildren)
        {
            var id = _hashIdUtilities.DecryptLong(hashIdChildren, Enums.Services.Children);
            var result = await _database.GiftChildrensRepository.Find(x => x.IdChildren == id);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<GiftChildrenDTO>>>(result);
        }

        public async Task<ActualResult<GiftChildrenDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftChildren);
            var result = await _database.GiftChildrensRepository.GetById(id);
            return _mapperService.Mapper.Map<ActualResult<GiftChildrenDTO>>(result);
        }

        public async Task<ActualResult> CreateAsync(GiftChildrenDTO item)
        {
            await _database.GiftChildrensRepository.Create(_mapperService.Mapper.Map<GiftChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateAsync(GiftChildrenDTO item)
        {
            await _database.GiftChildrensRepository.Update(_mapperService.Mapper.Map<GiftChildrens>(item));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.GiftChildrensRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.GiftChildren));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
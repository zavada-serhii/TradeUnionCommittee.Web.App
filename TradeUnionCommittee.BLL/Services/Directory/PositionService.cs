using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.Encryption;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _database;
        private readonly ICryptoUtilities _cryptoUtilities;
        private readonly IAutoMapperModule _mapperModule;

        public PositionService(IUnitOfWork database, IAutoMapperModule mapperModule, ICryptoUtilities cryptoUtilities)
        {
            _database = database;
            _mapperModule = mapperModule;
            _cryptoUtilities = cryptoUtilities;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() => 
            await Task.Run(() => _mapperModule.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(_database.PositionRepository.GetAll()));

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            var check = await CheckDecryptAndTupleInDb(hashId);
            return check.IsValid 
                ? _mapperModule.Mapper.Map<ActualResult<DirectoryDTO>>(_database.PositionRepository.Get(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Position))) 
                : new ActualResult<DirectoryDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                _database.PositionRepository.Create(_mapperModule.Mapper.Map<Position>(dto));
                return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult("0004");
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            var check = await CheckDecryptAndTupleInDb(dto.HashId);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    _database.PositionRepository.Update(_mapperModule.Mapper.Map<Position>(dto));
                    return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult("0004");
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await CheckDecryptAndTupleInDb(hashId, false);
            if (check.IsValid)
            {
                _database.PositionRepository.Delete(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Position));
                return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckNameAsync(string name) => 
            await Task.Run(() => _database.PositionRepository.Find(p => p.Name == name).Result.Any());

        private async Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, bool checkTuple = true) => await Task.Run(() =>
        {
            if (_cryptoUtilities.CheckDecrypt(hashId, EnumCryptoUtilities.Position))
            {
                if (checkTuple)
                {
                    var id = _cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Position);
                    if (_database.PositionRepository.Find(x => x.Id == id).Result.Any())
                    {
                        return new ActualResult();
                    }
                    return new ActualResult("0001");
                }
                return new ActualResult();
            }
            return new ActualResult("0003");
        });

        public void Dispose()
        {
            _database.Dispose();
        }

        //-------------------------------------------------------------------------------------------------------------------

        public Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
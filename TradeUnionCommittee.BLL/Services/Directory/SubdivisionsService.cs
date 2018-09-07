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
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly IUnitOfWork _database;
        private readonly ICryptoUtilities _cryptoUtilities;
        private readonly IAutoMapperService _mapperModule;

        public SubdivisionsService(IUnitOfWork database, IAutoMapperService mapperModule, ICryptoUtilities cryptoUtilities)
        {
            _database = database;
            _mapperModule = mapperModule;
            _cryptoUtilities = cryptoUtilities;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperModule.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == null)));

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId)
        {
            var check = await CheckDecryptAndTupleInDb(hashId);
            return check.IsValid
                ? _mapperModule.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == _cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Subdivision)))
                : new ActualResult<IEnumerable<SubdivisionDTO>>(check.ErrorsList);
        }

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId)
        {
            var check = await CheckDecryptAndTupleInDb(hashId);
            return check.IsValid
                ? _mapperModule.Mapper.Map<ActualResult<SubdivisionDTO>>(_database.SubdivisionsRepository.Get(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Subdivision)))
                : new ActualResult<SubdivisionDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateMainSubdivisionAsync(SubdivisionDTO dto)
        {
            if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Name))
            {
                _database.SubdivisionsRepository.Create(_mapperModule.Mapper.Map<Subdivisions>(dto));
                return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> CreateSubordinateSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await CheckDecryptAndTupleInDb(dto.HashIdSubordinate, false);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Name))
                {
                    _database.SubdivisionsRepository.Create(_mapperModule.Mapper.Map<Subdivisions>(dto));
                    return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateNameSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await CheckDecryptAndTupleInDb(dto.HashId);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    var subdivision = _database.SubdivisionsRepository
                        .Find(x => x.Id == _cryptoUtilities.DecryptLong(dto.HashId, EnumCryptoUtilities.Subdivision))
                        .Result.FirstOrDefault();

                    if (subdivision != null)
                    {
                        subdivision.Name = dto.Name;
                        _database.SubdivisionsRepository.Update(subdivision);
                        return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateAbbreviationSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await CheckDecryptAndTupleInDb(dto.HashId);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    var subdivision = _database.SubdivisionsRepository
                        .Find(x => x.Id == _cryptoUtilities.DecryptLong(dto.HashId, EnumCryptoUtilities.Subdivision))
                        .Result.FirstOrDefault();

                    if (subdivision != null)
                    {
                        subdivision.Abbreviation = dto.Abbreviation;
                        _database.SubdivisionsRepository.Update(subdivision);
                        return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await CheckDecryptAndTupleInDb(hashId, false);
            if (check.IsValid)
            {
                _database.SubdivisionsRepository.Delete(_cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Subdivision));
                return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> RestructuringUnits(SubdivisionDTO dto)
        {
            var checkMainSubdivisions = await CheckDecryptAndTupleInDb(dto.HashId);
            var checkSubordinateSubdivisions = await CheckDecryptAndTupleInDb(dto.HashIdSubordinate);
            if (checkMainSubdivisions.IsValid && checkSubordinateSubdivisions.IsValid)
            {
                var subdivision = _database.SubdivisionsRepository
                    .Find(x => x.Id == _cryptoUtilities.DecryptLong(dto.HashIdSubordinate, EnumCryptoUtilities.Subdivision)).Result
                    .FirstOrDefault();

                if (subdivision != null)
                {
                    subdivision.IdSubordinate = _cryptoUtilities.DecryptLong(dto.HashId, EnumCryptoUtilities.Subdivision);
                    _database.SubdivisionsRepository.Update(subdivision);
                    return _mapperModule.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            var listErrors = checkMainSubdivisions.ErrorsList.ToList();
            listErrors.AddRange(checkSubordinateSubdivisions.ErrorsList);
            return new ActualResult(listErrors);
        }

        public void Dispose()
        {
            _database.Dispose();
        }

        //-------------------------------------------------------------------------------------------------------------------

        private async Task<ActualResult> CheckDecryptAndTupleInDb(string hashId, bool checkTuple = true) => await Task.Run(() =>
        {
            if (_cryptoUtilities.CheckDecrypt(hashId, EnumCryptoUtilities.Subdivision))
            {
                if (checkTuple)
                {
                    var id = _cryptoUtilities.DecryptLong(hashId, EnumCryptoUtilities.Subdivision);
                    if (_database.SubdivisionsRepository.Find(x => x.Id == id).Result.Any())
                    {
                        return new ActualResult();
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult();
            }
            return new ActualResult(Errors.InvalidId);
        });

        //-------------------------------------------------------------------------------------------------------------------
        
        public async Task<bool> CheckNameAsync(string name) =>
            await Task.Run(() => _database.SubdivisionsRepository.Find(p => p.Name == name).Result.Any());

        public async Task<bool> CheckAbbreviationAsync(string name) =>
            await Task.Run(() => _database.SubdivisionsRepository.Find(p => p.Abbreviation == name).Result.Any());
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.BL;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Infrastructure;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperService _mapperService;
        private readonly ICheckerService _checkerService;

        public SubdivisionsService(IUnitOfWork database, IAutoMapperService mapperService, ICheckerService checkerService)
        {
            _database = database;
            _mapperService = mapperService;
            _checkerService = checkerService;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync() =>
            await Task.Run(() => _mapperService.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == null)));

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Subdivision);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == check.Result))
                : new ActualResult<IEnumerable<SubdivisionDTO>>(check.ErrorsList);
        }

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Subdivision);
            return check.IsValid
                ? _mapperService.Mapper.Map<ActualResult<SubdivisionDTO>>(_database.SubdivisionsRepository.Get(check.Result))
                : new ActualResult<SubdivisionDTO>(check.ErrorsList);
        }

        public async Task<ActualResult> CreateMainSubdivisionAsync(SubdivisionDTO dto)
        {
            if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Name))
            {
                _database.SubdivisionsRepository.Create(_mapperService.Mapper.Map<Subdivisions>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> CreateSubordinateSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDb(dto.HashIdSubordinate, Enums.Services.Subdivision,false);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Name))
                {
                    _database.SubdivisionsRepository.Create(_mapperService.Mapper.Map<Subdivisions>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateNameSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Subdivision);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    var subdivision = _database.SubdivisionsRepository
                        .Find(x => x.Id == check.Result)
                        .Result.FirstOrDefault();

                    if (subdivision != null)
                    {
                        subdivision.Name = dto.Name;
                        _database.SubdivisionsRepository.Update(subdivision);
                        return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> UpdateAbbreviationSubdivisionAsync(SubdivisionDTO dto)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Subdivision);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    var subdivision = _database.SubdivisionsRepository
                        .Find(x => x.Id == check.Result)
                        .Result.FirstOrDefault();

                    if (subdivision != null)
                    {
                        subdivision.Abbreviation = dto.Abbreviation;
                        _database.SubdivisionsRepository.Update(subdivision);
                        return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                    }
                    return new ActualResult(Errors.TupleDeleted);
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _checkerService.CheckDecryptAndTupleInDbWithId(hashId, Enums.Services.Subdivision, false);
            if (check.IsValid)
            {
                _database.SubdivisionsRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> RestructuringUnits(SubdivisionDTO dto)
        {
            var checkMainSubdivisions = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashId, Enums.Services.Subdivision);
            var checkSubordinateSubdivisions = await _checkerService.CheckDecryptAndTupleInDbWithId(dto.HashIdSubordinate, Enums.Services.Subdivision);

            if (checkMainSubdivisions.IsValid && checkSubordinateSubdivisions.IsValid)
            {
                var subdivision = _database.SubdivisionsRepository
                    .Find(x => x.Id == checkSubordinateSubdivisions.Result).Result
                    .FirstOrDefault();

                if (subdivision != null)
                {
                    subdivision.IdSubordinate = checkMainSubdivisions.Result;
                    _database.SubdivisionsRepository.Update(subdivision);
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
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
        
        public async Task<bool> CheckNameAsync(string name) =>
            await Task.Run(() => _database.SubdivisionsRepository.Find(p => p.Name == name).Result.Any());

        public async Task<bool> CheckAbbreviationAsync(string name) =>
            await Task.Run(() => _database.SubdivisionsRepository.Find(p => p.Abbreviation == name).Result.Any());
    }
}
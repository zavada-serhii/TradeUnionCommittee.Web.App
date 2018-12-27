using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public SubdivisionsService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync() => 
            _mapperService.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(await _database.SubdivisionsRepository.Find(x => x.IdSubordinate == null));

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision);
            return _mapperService.Mapper.Map<ActualResult<SubdivisionDTO>>(await _database.SubdivisionsRepository.GetById(id));
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId)
        {
            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision);
            return _mapperService.Mapper.Map<ActualResult<IEnumerable<SubdivisionDTO>>>(await _database.SubdivisionsRepository.Find(x => x.IdSubordinate == id));
        }

        public async Task<Dictionary<string,string>> GetSubordinateSubdivisionsForMvc(string hashId)
        {
            var subordinateSubdivisions = await GetSubordinateSubdivisions(hashId);
            return subordinateSubdivisions.Result.ToDictionary(subdivision => $"{subdivision.HashIdMain},{subdivision.RowVersion}", subdivision => subdivision.Name);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<TreeSubdivisionsDTO>> GetTreeSubdivisions()
        {
            var subdivisions = await _database.SubdivisionsRepository.GetWithInclude(x => x.IdSubordinate == null, c => c.InverseIdSubordinateNavigation);
            return subdivisions.Result.Select(subdivision => new TreeSubdivisionsDTO
            {
                GroupName = subdivision.Name,
                Subdivisions = FormationTree(subdivision)
            }).ToList();
        }

        private IEnumerable<SubdivisionDTO> FormationTree(Subdivisions subdivisions)
        {
            var list = new List<SubdivisionDTO>
            {
                new SubdivisionDTO
                {
                    HashIdMain = _hashIdUtilities.EncryptLong(subdivisions.Id, Enums.Services.Subdivision),
                    Name = subdivisions.Name
                }
            };
            list.AddRange(subdivisions.InverseIdSubordinateNavigation.Select(subdivision => new SubdivisionDTO
            {
                HashIdMain = _hashIdUtilities.EncryptLong(subdivision.Id, Enums.Services.Subdivision),
                Name = subdivision.Name
            }));
            return list;
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> CreateMainSubdivisionAsync(CreateSubdivisionDTO dto)
        {
            if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Abbreviation))
            {
                await _database.SubdivisionsRepository.Create(_mapperService.Mapper.Map<Subdivisions>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> CreateSubordinateSubdivisionAsync(CreateSubordinateSubdivisionDTO dto)
        {
            if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Abbreviation))
            {
                await _database.SubdivisionsRepository.Create(_mapperService.Mapper.Map<Subdivisions>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateNameSubdivisionAsync(UpdateSubdivisionNameDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.SubdivisionsRepository.Update(_mapperService.Mapper.Map<Subdivisions>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAbbreviationSubdivisionAsync(UpdateSubdivisionAbbreviationDTO dto)
        {
            if (!await CheckNameAsync(dto.Abbreviation))
            {
                await _database.SubdivisionsRepository.Update(_mapperService.Mapper.Map<Subdivisions>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> RestructuringUnits(RestructuringSubdivisionDTO dto)
        {
            await _database.SubdivisionsRepository.Update(_mapperService.Mapper.Map<Subdivisions>(dto));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.SubdivisionsRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.SubdivisionsRepository.Find(p => p.Name == name);
            return result.Result.Any();
        }

        public async Task<bool> CheckAbbreviationAsync(string name)
        {
            var result = await _database.SubdivisionsRepository.Find(p => p.Abbreviation == name);
            return result.Result.Any();
        }

        //-------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public SubdivisionsService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync()
        {
            try
            {
                var subdivision = await _context.Subdivisions.Where(x => x.IdSubordinate == null).OrderBy(x => x.Name).ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<SubdivisionDTO>>(subdivision);
                return new ActualResult<IEnumerable<SubdivisionDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<SubdivisionDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision);
                var subdivision = await _context.Subdivisions.FindAsync(id);
                var result = _mapperService.Mapper.Map<SubdivisionDTO>(subdivision);
                return new ActualResult<SubdivisionDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<SubdivisionDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision);
                var subdivision = await _context.Subdivisions.Where(x => x.IdSubordinate == id).OrderBy(x => x.Name).ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<SubdivisionDTO>>(subdivision);
                return new ActualResult<IEnumerable<SubdivisionDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<SubdivisionDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<Dictionary<string,string>> GetSubordinateSubdivisionsForMvc(string hashId)
        {
            var subordinateSubdivisions = await GetSubordinateSubdivisions(hashId);
            return subordinateSubdivisions.Result.ToDictionary(subdivision => $"{subdivision.HashIdMain},{subdivision.RowVersion}", subdivision => subdivision.Name);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<IEnumerable<TreeSubdivisionsDTO>> GetTreeSubdivisions()
        {
            var subdivisions = await _context.Subdivisions
                                             .Where(x => x.IdSubordinate == null)
                                             .Include(x => x.InverseIdSubordinateNavigation)
                                             .OrderBy(x => x.Name)
                                             .ToListAsync();

            return subdivisions.Select(subdivision => new TreeSubdivisionsDTO
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
            try
            {
                if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Abbreviation))
                {
                    await _context.Subdivisions.AddAsync(_mapperService.Mapper.Map<Subdivisions>(dto));
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateSubordinateSubdivisionAsync(CreateSubordinateSubdivisionDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name) && !await CheckAbbreviationAsync(dto.Abbreviation))
                {
                    await _context.Subdivisions.AddAsync(_mapperService.Mapper.Map<Subdivisions>(dto));
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateNameSubdivisionAsync(UpdateSubdivisionNameDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    var mapping = _mapperService.Mapper.Map<Subdivisions>(dto);
                    _context.Entry(mapping).State = EntityState.Modified;
                    _context.Entry(mapping).Property(x => x.IdSubordinate).IsModified = false;
                    _context.Entry(mapping).Property(x => x.Abbreviation).IsModified = false;
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAbbreviationSubdivisionAsync(UpdateSubdivisionAbbreviationDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Abbreviation))
                {
                    var mapping = _mapperService.Mapper.Map<Subdivisions>(dto);
                    _context.Entry(mapping).State = EntityState.Modified;
                    _context.Entry(mapping).Property(x => x.IdSubordinate).IsModified = false;
                    _context.Entry(mapping).Property(x => x.Name).IsModified = false;
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return new ActualResult(Errors.DuplicateData);
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> RestructuringUnits(RestructuringSubdivisionDTO dto)
        {
            try
            {
                var mapping = _mapperService.Mapper.Map<Subdivisions>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.Abbreviation).IsModified = false;
                _context.Entry(mapping).Property(x => x.Name).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (DbUpdateConcurrencyException)
            {
                return new ActualResult(Errors.TupleDeletedOrUpdated);
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Subdivision);
                var result = await _context.Subdivisions.FindAsync(id);
                if (result != null)
                {
                    _context.Subdivisions.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<bool> CheckNameAsync(string name)
        {
            return await _context.Subdivisions.AnyAsync(p => p.Name == name);
        }

        public async Task<bool> CheckAbbreviationAsync(string name)
        {
            return await _context.Subdivisions.AnyAsync(p => p.Abbreviation == name);
        }

        //-------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
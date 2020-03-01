using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Directory;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class SubdivisionsService : ISubdivisionsService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public SubdivisionsService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync()
        {
            try
            {
                var subdivision = await _context.Subdivisions.Where(x => x.IdSubordinate == null).OrderBy(x => x.Name).ToListAsync();
                var result = _mapper.Map<IEnumerable<SubdivisionDTO>>(subdivision);
                return new ActualResult<IEnumerable<SubdivisionDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<SubdivisionDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var subdivision = await _context.Subdivisions.FindAsync(id);
                if (subdivision == null)
                {
                    return new ActualResult<SubdivisionDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<SubdivisionDTO>(subdivision);
                return new ActualResult<SubdivisionDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<SubdivisionDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var subdivision = await _context.Subdivisions.Where(x => x.IdSubordinate == id).OrderBy(x => x.Name).ToListAsync();
                var result = _mapper.Map<IEnumerable<SubdivisionDTO>>(subdivision);
                return new ActualResult<IEnumerable<SubdivisionDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<SubdivisionDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
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
                    HashIdMain = HashHelper.EncryptLong(subdivisions.Id),
                    Name = subdivisions.Name
                }
            };
            list.AddRange(subdivisions.InverseIdSubordinateNavigation.Select(subdivision => new SubdivisionDTO
            {
                HashIdMain = HashHelper.EncryptLong(subdivision.Id),
                Name = subdivision.Name
            }));
            return list;
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<string>> CreateMainSubdivisionAsync(CreateSubdivisionDTO dto)
        {
            try
            {
                var subdivision = _mapper.Map<Subdivisions>(dto);
                await _context.Subdivisions.AddAsync(subdivision);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(subdivision.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateSubordinateSubdivisionAsync(CreateSubordinateSubdivisionDTO dto)
        {
            try
            {
                var subdivision = _mapper.Map<Subdivisions>(dto);
                await _context.Subdivisions.AddAsync(subdivision);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(subdivision.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> UpdateNameSubdivisionAsync(UpdateSubdivisionNameDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<Subdivisions>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.IdSubordinate).IsModified = false;
                _context.Entry(mapping).Property(x => x.Abbreviation).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAbbreviationSubdivisionAsync(UpdateSubdivisionAbbreviationDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<Subdivisions>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.IdSubordinate).IsModified = false;
                _context.Entry(mapping).Property(x => x.Name).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> RestructuringUnits(RestructuringSubdivisionDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<Subdivisions>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.Abbreviation).IsModified = false;
                _context.Entry(mapping).Property(x => x.Name).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var result = await _context.Subdivisions.FindAsync(id);
                if (result != null)
                {
                    _context.Subdivisions.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //-------------------------------------------------------------------------------------------------------------------

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
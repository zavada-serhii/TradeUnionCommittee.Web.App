using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class DepartmentalService : IDepartmentalService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public DepartmentalService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync()
        {
            try
            {
                var departmental = await _context.AddressPublicHouse
                                                 .Where(x => x.Type == TypeHouse.Departmental)
                                                 .OrderBy(x => x.Street)
                                                 .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<DepartmentalDTO>>(departmental);
                return new ActualResult<IEnumerable<DepartmentalDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<DepartmentalDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }


        public async Task<ActualResult<Dictionary<string, string>>> GetAllShortcut()
        {
            try
            {
                var departmental = await _context.AddressPublicHouse
                                                 .Where(x => x.Type == TypeHouse.Departmental)
                                                 .OrderBy(x => x.Street)
                                                 .ToDictionaryAsync(result => _hashIdUtilities.EncryptLong(result.Id),
                                                                    result => $"{result.City}, {result.Street}, {result.NumberHouse}");
                return new ActualResult<Dictionary<string, string>> { Result = departmental };
            }
            catch (Exception exception)
            {
                return new ActualResult<Dictionary<string, string>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var departmental = await _context.AddressPublicHouse.FindAsync(id);
                if (departmental == null)
                {
                    return new ActualResult<DepartmentalDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<DepartmentalDTO>(departmental);
                return new ActualResult<DepartmentalDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<DepartmentalDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> CreateAsync(DepartmentalDTO dto)
        {
            try
            {
                await _context.AddressPublicHouse.AddAsync(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO dto)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<AddressPublicHouse>(dto)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.AddressPublicHouse.FindAsync(id);
                if (result != null)
                {
                    _context.AddressPublicHouse.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
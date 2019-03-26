﻿using Microsoft.EntityFrameworkCore;
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
    public class CulturalService : ICulturalService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public CulturalService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            try
            {
                var cultural = await _context.Cultural.OrderBy(x => x.Name).ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<DirectoryDTO>>(cultural);
                return new ActualResult<IEnumerable<DirectoryDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<DirectoryDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Cultural);
                var cultural = await _context.Cultural.FindAsync(id);
                var result = _mapperService.Mapper.Map<DirectoryDTO>(cultural);
                return new ActualResult<DirectoryDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<DirectoryDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    await _context.Cultural.AddAsync(_mapperService.Mapper.Map<Cultural>(dto));
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

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            try
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    _context.Entry(_mapperService.Mapper.Map<Cultural>(dto)).State = EntityState.Modified;
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

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Cultural);
                var result = await _context.Cultural.FindAsync(id);
                if (result != null)
                {
                    _context.Cultural.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            return await _context.Cultural.AnyAsync(p => p.Name == name);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
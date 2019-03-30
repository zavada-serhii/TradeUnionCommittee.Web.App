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
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class DormitoryService : IDormitoryService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public DormitoryService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAllAsync()
        {
            try
            {
                var dormitory = await _context.AddressPublicHouse
                                              .Where(x => x.Type == TypeHouse.Dormitory)
                                              .OrderBy(x => x.NumberDormitory)
                                              .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<DormitoryDTO>>(dormitory);
                return new ActualResult<IEnumerable<DormitoryDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<DormitoryDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var dormitory = await _context.AddressPublicHouse.FindAsync(id);
                var result = _mapperService.Mapper.Map<DormitoryDTO>(dormitory);
                return new ActualResult<DormitoryDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<DormitoryDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(DormitoryDTO dto)
        {
            try
            {
                if (!await CheckDuplicateDataAsync(dto))
                {
                    await _context.AddressPublicHouse.AddAsync(_mapperService.Mapper.Map<AddressPublicHouse>(dto));
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

        public async Task<ActualResult> UpdateAsync(DormitoryDTO dto)
        {
            try
            {
                if (!await CheckDuplicateDataAsync(dto))
                {
                    _context.Entry(_mapperService.Mapper.Map<AddressPublicHouse>(dto)).State = EntityState.Modified;
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
                var id = _hashIdUtilities.DecryptLong(hashId);
                var result = await _context.AddressPublicHouse.FindAsync(id);
                if (result != null)
                {
                    _context.AddressPublicHouse.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        private async Task<bool> CheckDuplicateDataAsync(DormitoryDTO dto)
        {
            return await _context.AddressPublicHouse
                                        .AnyAsync(p => p.City == dto.City &&
                                                   p.Street == dto.Street &&
                                                   p.NumberHouse == dto.NumberHouse &&
                                                   p.NumberDormitory == dto.NumberDormitory &&
                                                   p.Type == TypeHouse.Dormitory);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
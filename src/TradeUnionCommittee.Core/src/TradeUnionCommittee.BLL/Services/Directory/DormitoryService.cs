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
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    internal class DormitoryService : IDormitoryService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public DormitoryService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAllAsync()
        {
            try
            {
                var dormitory = await _context.AddressPublicHouse
                                              .Where(x => x.Type == TypeHouse.Dormitory)
                                              .OrderBy(x => x.NumberDormitory)
                                              .ToListAsync();
                var result = _mapper.Map<IEnumerable<DormitoryDTO>>(dormitory);
                return new ActualResult<IEnumerable<DormitoryDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<DormitoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<DormitoryDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var dormitory = await _context.AddressPublicHouse.FindAsync(id);
                if (dormitory == null)
                {
                    return new ActualResult<DormitoryDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<DormitoryDTO>(dormitory);
                return new ActualResult<DormitoryDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<DormitoryDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(DormitoryDTO dto)
        {
            try
            {
                var dormitory = _mapper.Map<AddressPublicHouse>(dto);
                await _context.AddressPublicHouse.AddAsync(dormitory);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(dormitory.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(DormitoryDTO dto)
        {
            try
            {
                _context.Entry(_mapper.Map<AddressPublicHouse>(dto)).State = EntityState.Modified;
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
                var id = HashHelper.DecryptLong(hashId);
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
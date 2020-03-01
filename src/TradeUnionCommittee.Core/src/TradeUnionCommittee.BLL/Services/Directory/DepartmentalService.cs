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
    internal class DepartmentalService : IDepartmentalService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public DepartmentalService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync()
        {
            try
            {
                var departmental = await _context.AddressPublicHouse
                                                 .Where(x => x.Type == TypeHouse.Departmental)
                                                 .OrderBy(x => x.Street)
                                                 .ToListAsync();
                var result = _mapper.Map<IEnumerable<DepartmentalDTO>>(departmental);
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
                                                 .ToDictionaryAsync(result => HashHelper.EncryptLong(result.Id),
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
                var id = HashHelper.DecryptLong(hashId);
                var departmental = await _context.AddressPublicHouse.FindAsync(id);
                if (departmental == null)
                {
                    return new ActualResult<DepartmentalDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<DepartmentalDTO>(departmental);
                return new ActualResult<DepartmentalDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<DepartmentalDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(DepartmentalDTO dto)
        {
            try
            {
                var departmental = _mapper.Map<AddressPublicHouse>(dto);
                await _context.AddressPublicHouse.AddAsync(departmental);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(departmental.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO dto)
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
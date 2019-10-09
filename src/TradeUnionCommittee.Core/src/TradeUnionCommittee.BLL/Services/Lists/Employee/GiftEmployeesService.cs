using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class GiftEmployeesService : IGiftEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public GiftEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<GiftEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashId.DecryptLong(hashIdEmployee);
                var gift = await _context.GiftEmployees
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateGift)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<GiftEmployeesDTO>>(gift);
                return new ActualResult<IEnumerable<GiftEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<GiftEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<GiftEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashId.DecryptLong(hashId);
                var gift = await _context.GiftEmployees.FindAsync(id);
                if (gift == null)
                {
                    return new ActualResult<GiftEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<GiftEmployeesDTO>(gift);
                return new ActualResult<GiftEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<GiftEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(GiftEmployeesDTO item)
        {
            try
            {
                var giftEmployees = _mapper.Map<GiftEmployees>(item);
                await _context.GiftEmployees.AddAsync(giftEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashId.EncryptLong(giftEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(GiftEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<GiftEmployees>(item)).State = EntityState.Modified;
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
                var id = HashId.DecryptLong(hashId);
                var result = await _context.GiftEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.GiftEmployees.Remove(result);
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

        //--------------- Business Logic ---------------
    }
}
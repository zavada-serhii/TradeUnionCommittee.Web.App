using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class AwardEmployeesService : IAwardEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public AwardEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<AwardEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var award = await _context.AwardEmployees
                    .Include(x => x.IdAwardNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateIssue)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<AwardEmployeesDTO>>(award);
                return new ActualResult<IEnumerable<AwardEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<AwardEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<AwardEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var award = await _context.AwardEmployees
                    .Include(x => x.IdAwardNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (award == null)
                {
                    return new ActualResult<AwardEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<AwardEmployeesDTO>(award);
                return new ActualResult<AwardEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<AwardEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(AwardEmployeesDTO item)
        {
            try
            {
                var awardEmployees = _mapper.Map<AwardEmployees>(item);
                await _context.AwardEmployees.AddAsync(awardEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(awardEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(AwardEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<AwardEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.AwardEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.AwardEmployees.Remove(result);
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
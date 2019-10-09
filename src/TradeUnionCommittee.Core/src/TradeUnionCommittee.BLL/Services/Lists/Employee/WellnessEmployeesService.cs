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
using TradeUnionCommittee.DAL.Enums;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class WellnessEmployeesService : IWellnessEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public WellnessEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<WellnessEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var wellness = await _context.EventEmployees
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdEmployee == id && x.IdEventNavigation.Type == TypeEvent.Wellness)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<WellnessEmployeesDTO>>(wellness);
                return new ActualResult<IEnumerable<WellnessEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<WellnessEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<WellnessEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var wellness = await _context.EventEmployees
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (wellness == null)
                {
                    return new ActualResult<WellnessEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<WellnessEmployeesDTO>(wellness);
                return new ActualResult<WellnessEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<WellnessEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(WellnessEmployeesDTO item)
        {
            try
            {
                var wellnessEmployees = _mapper.Map<EventEmployees>(item);
                await _context.EventEmployees.AddAsync(wellnessEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(wellnessEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(WellnessEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<EventEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.EventEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.EventEmployees.Remove(result);
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
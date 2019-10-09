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
    internal class TourEmployeesService : ITourEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public TourEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<TourEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var tour = await _context.EventEmployees
                    .Include(x => x.IdEventNavigation)
                    .Where(x => x.IdEmployee == id && x.IdEventNavigation.Type == TypeEvent.Tour)
                    .OrderByDescending(x => x.StartDate)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<TourEmployeesDTO>>(tour);
                return new ActualResult<IEnumerable<TourEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<TourEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<TourEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var tour = await _context.EventEmployees
                    .Include(x => x.IdEventNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (tour == null)
                {
                    return new ActualResult<TourEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<TourEmployeesDTO>(tour);
                return new ActualResult<TourEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<TourEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(TourEmployeesDTO item)
        {
            try
            {
                var tourEmployees = _mapper.Map<EventEmployees>(item);
                await _context.EventEmployees.AddAsync(tourEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(tourEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(TourEmployeesDTO item)
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
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.Lists.Employee;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    internal class CulturalEmployeesService : ICulturalEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public CulturalEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<CulturalEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var cultural = await _context.CulturalEmployees
                    .Include(x => x.IdCulturalNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateVisit)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<CulturalEmployeesDTO>>(cultural);
                return new ActualResult<IEnumerable<CulturalEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CulturalEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<CulturalEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var cultural = await _context.CulturalEmployees
                    .Include(x => x.IdCulturalNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (cultural == null)
                {
                    return new ActualResult<CulturalEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<CulturalEmployeesDTO>(cultural);
                return new ActualResult<CulturalEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<CulturalEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(CulturalEmployeesDTO item)
        {
            try
            {
                var culturalEmployees = _mapper.Map<CulturalEmployees>(item);
                await _context.CulturalEmployees.AddAsync(culturalEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(culturalEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(CulturalEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<CulturalEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.CulturalEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.CulturalEmployees.Remove(result);
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
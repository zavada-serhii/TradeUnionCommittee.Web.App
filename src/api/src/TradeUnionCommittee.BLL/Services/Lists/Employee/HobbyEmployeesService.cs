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
    internal class HobbyEmployeesService : IHobbyEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public HobbyEmployeesService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<HobbyEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var hobby = await _context.HobbyEmployees
                    .Include(x => x.IdHobbyNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderBy(x => x.IdHobbyNavigation.Name)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<HobbyEmployeesDTO>>(hobby);
                return new ActualResult<IEnumerable<HobbyEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<HobbyEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<HobbyEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var hobby = await _context.HobbyEmployees
                    .Include(x => x.IdHobbyNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (hobby == null)
                {
                    return new ActualResult<HobbyEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapperService.Mapper.Map<HobbyEmployeesDTO>(hobby);
                return new ActualResult<HobbyEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<HobbyEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(HobbyEmployeesDTO item)
        {
            try
            {
                var hobbyEmployees = _mapperService.Mapper.Map<HobbyEmployees>(item);
                await _context.HobbyEmployees.AddAsync(hobbyEmployees);
                await _context.SaveChangesAsync();
                var hashId = _hashIdUtilities.EncryptLong(hobbyEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(HobbyEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<HobbyEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.HobbyEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.HobbyEmployees.Remove(result);
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
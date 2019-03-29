using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.Services.Lists.Employee
{
    public class MaterialAidEmployeesService : IMaterialAidEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly IHashIdConfiguration _hashIdUtilities;

        public MaterialAidEmployeesService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, IHashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<MaterialAidEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashIdEmployee);
                var materialAid = await _context.MaterialAidEmployees
                    .Include(x => x.IdMaterialAidNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateIssue)
                    .ToListAsync();
                var result = _mapperService.Mapper.Map<IEnumerable<MaterialAidEmployeesDTO>>(materialAid);
                return new ActualResult<IEnumerable<MaterialAidEmployeesDTO>> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<IEnumerable<MaterialAidEmployeesDTO>>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult<MaterialAidEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = _hashIdUtilities.DecryptLong(hashId);
                var materialAid = await _context.MaterialAidEmployees
                    .Include(x => x.IdMaterialAidNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                var result = _mapperService.Mapper.Map<MaterialAidEmployeesDTO>(materialAid);
                return new ActualResult<MaterialAidEmployeesDTO> { Result = result };
            }
            catch (Exception)
            {
                return new ActualResult<MaterialAidEmployeesDTO>(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> CreateAsync(MaterialAidEmployeesDTO item)
        {
            try
            {
                await _context.MaterialAidEmployees.AddAsync(_mapperService.Mapper.Map<MaterialAidEmployees>(item));
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public async Task<ActualResult> UpdateAsync(MaterialAidEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapperService.Mapper.Map<MaterialAidEmployees>(item)).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
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
                var result = await _context.MaterialAidEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.MaterialAidEmployees.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception)
            {
                return new ActualResult(Errors.DataBaseError);
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        //--------------- Business Logic ---------------
    }
}
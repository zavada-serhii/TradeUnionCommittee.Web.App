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
    internal class MaterialAidEmployeesService : IMaterialAidEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public MaterialAidEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<MaterialAidEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var materialAid = await _context.MaterialAidEmployees
                    .Include(x => x.IdMaterialAidNavigation)
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateIssue)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<MaterialAidEmployeesDTO>>(materialAid);
                return new ActualResult<IEnumerable<MaterialAidEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<MaterialAidEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<MaterialAidEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var materialAid = await _context.MaterialAidEmployees
                    .Include(x => x.IdMaterialAidNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (materialAid == null)
                {
                    return new ActualResult<MaterialAidEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<MaterialAidEmployeesDTO>(materialAid);
                return new ActualResult<MaterialAidEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<MaterialAidEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(MaterialAidEmployeesDTO item)
        {
            try
            {
                var materialAidEmployees = _mapper.Map<MaterialAidEmployees>(item);
                await _context.MaterialAidEmployees.AddAsync(materialAidEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(materialAidEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(MaterialAidEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<MaterialAidEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.MaterialAidEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.MaterialAidEmployees.Remove(result);
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
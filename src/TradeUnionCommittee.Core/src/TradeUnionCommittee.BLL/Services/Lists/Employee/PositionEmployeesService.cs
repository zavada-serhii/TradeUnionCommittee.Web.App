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
    internal class PositionEmployeesService : IPositionEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public PositionEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<PositionEmployeesDTO>> GetAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var position = await _context.PositionEmployees.FirstOrDefaultAsync(x => x.IdEmployee == id);
                if (position == null)
                {
                    return new ActualResult<PositionEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<PositionEmployeesDTO>(position);
                return new ActualResult<PositionEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PositionEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(PositionEmployeesDTO dto)
        {
            try
            {
                var validation = await CheckDate(dto);
                if (validation.IsValid)
                {
                    _context.Entry(_mapper.Map<PositionEmployees>(dto)).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new ActualResult();
                }
                return validation;
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

        private async Task<ActualResult> CheckDate(PositionEmployeesDTO dto)
        {
            try
            {
                var id = HashHelper.DecryptLong(dto.HashIdEmployee);
                var employee = await _context.Employee.FindAsync(id);
                if (employee != null)
                {
                    var listErrors = new List<string>();
                    if (dto.StartDate.Year < employee.StartYearWork)
                    {
                        listErrors.Add($"Рік дати початку менший року початку роботи в ОНУ - {employee.StartYearWork}!");
                    }
                    return listErrors.Any() ? new ActualResult(listErrors) : new ActualResult();
                }
                return new ActualResult(Errors.TupleDeleted);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }
    }
}
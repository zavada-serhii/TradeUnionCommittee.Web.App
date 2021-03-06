﻿using AutoMapper;
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
    internal class ApartmentAccountingEmployeesService : IApartmentAccountingEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public ApartmentAccountingEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>> GetAllAsync(string hashIdEmployee)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var apartmentAccounting = await _context.ApartmentAccountingEmployees
                    .Where(x => x.IdEmployee == id)
                    .OrderByDescending(x => x.DateAdoption)
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<ApartmentAccountingEmployeesDTO>>(apartmentAccounting);
                return new ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ApartmentAccountingEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ApartmentAccountingEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var apartmentAccounting = await _context.ApartmentAccountingEmployees.FindAsync(id);
                if (apartmentAccounting == null)
                {
                    return new ActualResult<ApartmentAccountingEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ApartmentAccountingEmployeesDTO>(apartmentAccounting);
                return new ActualResult<ApartmentAccountingEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ApartmentAccountingEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(ApartmentAccountingEmployeesDTO item)
        {
            try
            {
                var mapping = _mapper.Map<ApartmentAccountingEmployees>(item);
                await _context.ApartmentAccountingEmployees.AddAsync(mapping);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(mapping.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(ApartmentAccountingEmployeesDTO item)
        {
            try
            {
                var mapping = _mapper.Map<ApartmentAccountingEmployees>(item);
                _context.Entry(mapping).State = EntityState.Modified;
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
                var result = await _context.ApartmentAccountingEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.ApartmentAccountingEmployees.Remove(result);
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
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
    internal class PublicHouseEmployeesService : IPublicHouseEmployeesService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IMapper _mapper;

        public PublicHouseEmployeesService(TradeUnionCommitteeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<PublicHouseEmployeesDTO>>> GetAllAsync(string hashIdEmployee, PublicHouse type)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashIdEmployee);
                var publicHouse = await _context.PublicHouseEmployees
                    .Include(x => x.IdAddressPublicHouseNavigation)
                    .Where(x => x.IdEmployee == id && x.IdAddressPublicHouseNavigation.Type == Converter(type))
                    .ToListAsync();
                var result = _mapper.Map<IEnumerable<PublicHouseEmployeesDTO>>(publicHouse);
                return new ActualResult<IEnumerable<PublicHouseEmployeesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<PublicHouseEmployeesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<PublicHouseEmployeesDTO>> GetAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var publicHouse = await _context.PublicHouseEmployees
                    .Include(x => x.IdAddressPublicHouseNavigation)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (publicHouse == null)
                {
                    return new ActualResult<PublicHouseEmployeesDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<PublicHouseEmployeesDTO>(publicHouse);
                return new ActualResult<PublicHouseEmployeesDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PublicHouseEmployeesDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateAsync(PublicHouseEmployeesDTO item)
        {
            try
            {
                var publicHouseEmployees = _mapper.Map<PublicHouseEmployees>(item);
                await _context.PublicHouseEmployees.AddAsync(publicHouseEmployees);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(publicHouseEmployees.Id);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(PublicHouseEmployeesDTO item)
        {
            try
            {
                _context.Entry(_mapper.Map<PublicHouseEmployees>(item)).State = EntityState.Modified;
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
                var result = await _context.PublicHouseEmployees.FindAsync(id);
                if (result != null)
                {
                    _context.PublicHouseEmployees.Remove(result);
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

        private TypeHouse Converter(PublicHouse type)
        {
            switch (type)
            {
                case PublicHouse.Dormitory:
                    return TypeHouse.Dormitory;
                case PublicHouse.Departmental:
                    return TypeHouse.Departmental;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
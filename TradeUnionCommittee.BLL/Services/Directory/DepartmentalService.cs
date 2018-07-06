using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DepartmentalService : IDepartmentalService
    {
        private readonly IUnitOfWork _database;

        public DepartmentalService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DepartmentalDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddressPublicHouse, DepartmentalDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<AddressPublicHouse>>, ActualResult<IEnumerable<DepartmentalDTO>>>(
                    _database.AddressPublicHouseRepository.Find(x => x.Type == 2));
            });
        }

        public async Task<ActualResult<Dictionary<long, string>>> GetAllShortcut()
        {
            return await Task.Run(() =>
            {
                var departmental = _database.AddressPublicHouseRepository.Find(x => x.Type == 2);
                var dictionary = departmental.Result.ToDictionary(result => result.Id, result => result.City + " " + result.Street + " " + result.NumberHouse);
                return new ActualResult<Dictionary<long, string>> {Result = dictionary};
            });
        }

        public async Task<ActualResult<DepartmentalDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var departmental = _database.AddressPublicHouseRepository.Get(id);
                if (departmental.IsValid == false && departmental.ErrorsList.Count > 0 || departmental.Result == null)
                {
                    return new ActualResult<DepartmentalDTO> { IsValid = false, ErrorsList = departmental.ErrorsList };
                }
                return new ActualResult<DepartmentalDTO>
                {
                    Result = new DepartmentalDTO
                    {
                        Id = departmental.Result.Id,
                        City = departmental.Result.City,
                        Street = departmental.Result.Street,
                        NumberHouse = departmental.Result.NumberHouse
                    }
                };
            });
        }

        public async Task<ActualResult> CreateAsync(DepartmentalDTO item)
        {
            var departmental = _database.AddressPublicHouseRepository.Create(new AddressPublicHouse
            {
                City = item.City,
                Street = item.Street,
                NumberHouse = item.NumberHouse,
                Type = 2
            });
            if (departmental.IsValid == false && departmental.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = departmental.ErrorsList };
            }
            await _database.SaveAsync();
            return departmental;
        }

        public async Task<ActualResult> UpdateAsync(DepartmentalDTO item)
        {
            var departmental = _database.AddressPublicHouseRepository.Update(new AddressPublicHouse
            {
                Id = item.Id,
                City = item.City,
                Street = item.Street,
                NumberHouse = item.NumberHouse,
                Type = 2
            });
            if (departmental.IsValid == false && departmental.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = departmental.ErrorsList };
            }
            await _database.SaveAsync();
            return departmental;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var departmental = _database.AddressPublicHouseRepository.Delete(id);
            if (departmental.IsValid == false && departmental.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = departmental.ErrorsList };
            }
            await _database.SaveAsync();
            return departmental;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
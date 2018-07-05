using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class DormitoryService : IDormitoryService
    {
        private readonly IUnitOfWork _database;

        public DormitoryService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DormitoryDTO>>> GetAll()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddressPublicHouse, DormitoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<AddressPublicHouse>>, ActualResult<IEnumerable<DormitoryDTO>>>(
                    _database.AddressPublicHouseRepository.Find(x => x.Type == 1));
            });
        }

        public async Task<ActualResult<DormitoryDTO>> Get(long id)
        {
            return await Task.Run(() =>
            {
                var dormitory = _database.AddressPublicHouseRepository.Get(id);
                if (dormitory.IsValid == false && dormitory.ErrorsList.Count > 0 || dormitory.Result == null)
                {
                    return new ActualResult<DormitoryDTO> { IsValid = false, ErrorsList = dormitory.ErrorsList };
                }
                return new ActualResult<DormitoryDTO>
                {
                    Result = new DormitoryDTO
                    {
                        Id = dormitory.Result.Id,
                        City = dormitory.Result.City,
                        Street = dormitory.Result.Street,
                        NumberHouse = dormitory.Result.NumberHouse,
                        NumberDormitory = dormitory.Result.NumberDormitory
                    }
                };
            });
        }

        public async Task<ActualResult> Create(DormitoryDTO item)
        {
            return await Task.Run(async () =>
            {
                var dormitory = _database.AddressPublicHouseRepository.Create(new AddressPublicHouse
                {
                    City = item.City,
                    Street = item.Street,
                    NumberHouse = item.NumberHouse,
                    NumberDormitory = item.NumberDormitory,
                    Type = 1
                });
                if (dormitory.IsValid == false && dormitory.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = dormitory.ErrorsList };
                }
                await _database.SaveAsync();
                return dormitory;
            });
        }

        public async Task<ActualResult> Update(DormitoryDTO item)
        {
            return await Task.Run(async () =>
            {
                var dormitory = _database.AddressPublicHouseRepository.Update(new AddressPublicHouse
                {
                    Id = item.Id,
                    City = item.City,
                    Street = item.Street,
                    NumberHouse = item.NumberHouse,
                    NumberDormitory = item.NumberDormitory,
                    Type = 1
                });
                if (dormitory.IsValid == false && dormitory.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = dormitory.ErrorsList };
                }
                await _database.SaveAsync();
                return dormitory;
            });
        }

        public async Task<ActualResult> Delete(long id)
        {
            return await Task.Run(async () =>
            {
                var dormitory = _database.AddressPublicHouseRepository.Delete(id);
                if (dormitory.IsValid == false && dormitory.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = dormitory.ErrorsList };
                }
                await _database.SaveAsync();
                return dormitory;
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
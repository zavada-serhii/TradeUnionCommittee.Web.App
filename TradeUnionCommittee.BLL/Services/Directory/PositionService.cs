using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _database;

        public PositionService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAll()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Position, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Position>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.PositionRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> Get(long? id)
        {
            return await Task.Run(() =>
            {
                if (id == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "Id can not be null!") } };
                }
                var position = _database.PositionRepository.Get(id.Value);
                if (position.IsValid == false && position.ErrorsList.Count > 0)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = position.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = position.Result.Id, Name = position.Result.Name } };
            });
        }

        public async Task<ActualResult> Create(DirectoryDTO item)
        {
            return await Task.Run(() =>
            {
                if (item == null)
                {
                    return new ActualResult { IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "DirectoryDTO can not be null!") } };
                }
                var position = _database.PositionRepository.Create(new Position { Name = item.Name });
                if (position.IsValid == false && position.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
                }
                return position;
            });
        }

        public async Task<ActualResult> Edit(DirectoryDTO item)
        {
            return await Task.Run(() =>
            {
                if (item == null)
                {
                    return new ActualResult { IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "DirectoryDTO can not be null!") } };
                }
                var position = _database.PositionRepository.Edit(new Position { Id = item.Id, Name = item.Name });
                if (position.IsValid == false && position.ErrorsList.Count > 0)
                {
                    return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
                }
                return position;
            });
        }

        public async Task<ActualResult> Remove(long? id)
        {
            return await Task.Run(() =>
            {
                if (id == null)
                {
                    return new ActualResult { IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "Id can not be null!") } };
                }
                var position = _database.PositionRepository.Remove(id.Value);
                if (position.IsValid == false && position.ErrorsList.Count > 0)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = position.ErrorsList };
                }
                return position;
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
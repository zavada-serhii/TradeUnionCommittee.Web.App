using AutoMapper;
using System;
using System.Collections.Generic;
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

        public ActualResult<IEnumerable<DirectoryDTO>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Position, DirectoryDTO>()).CreateMapper();
            return mapper.Map<ActualResult<IEnumerable<Position>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.PositionRepository.GetAll());
        }

        public ActualResult<DirectoryDTO> Get(long? id)
        {
            if (id == null)
            {
                return new ActualResult<DirectoryDTO> {IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "Id can not be null!") } };
            }
            var position = _database.PositionRepository.Get(id.Value);
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = position.ErrorsList };
            }
            return new ActualResult<DirectoryDTO> {Result = new DirectoryDTO {Id = position.Result.Id, Name = position.Result.Name}};
        }

        public ActualResult Create(DirectoryDTO item)
        {
            if (item == null)
            {
                return new ActualResult {IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "DirectoryDTO can not be null!") } };
            }
            var position = _database.PositionRepository.Create(new Position {Name = item.Name});
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
            }
            return position;
        }

        public ActualResult Edit(DirectoryDTO item)
        {
            if (item == null)
            {
                return new ActualResult { IsValid = false, ErrorsList = new List<Error> { new Error(DateTime.Now, "DirectoryDTO can not be null!") } };
            }
            var position = _database.PositionRepository.Edit(new Position {Id = item.Id, Name = item.Name });
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
            }
            return position;
        }

        public ActualResult Remove(long? id)
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
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
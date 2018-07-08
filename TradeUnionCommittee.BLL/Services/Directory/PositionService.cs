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
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _database;

        public PositionService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Position, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Position>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.PositionRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var position = _database.PositionRepository.Get(id);
                if (position.IsValid == false && position.ErrorsList.Count > 0 || position.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = position.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = position.Result.Id, Name = position.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var position = _database.PositionRepository.Create(new Position { Name = item.Name });
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            position.IsValid = dbState.IsValid;
            return position;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var position = _database.PositionRepository.Update(new Position { Id = item.Id, Name = item.Name });
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            position.IsValid = dbState.IsValid;
            return position;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var position = _database.PositionRepository.Delete(id);
            if (position.IsValid == false && position.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = position.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            position.IsValid = dbState.IsValid;
            return position;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var position = _database.PositionRepository.Find(p => p.Name == name);
                return position.Result.Any() ?
                    new ActualResult { IsValid = false} :
                    new ActualResult { IsValid = true};
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
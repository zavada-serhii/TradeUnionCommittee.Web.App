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
    public class MaterialAidService : IMaterialAidService
    {
        private readonly IUnitOfWork _database;

        public MaterialAidService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MaterialAid, DirectoryDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<MaterialAid>>, ActualResult<IEnumerable<DirectoryDTO>>>(_database.MaterialAidRepository.GetAll());
            });
        }

        public async Task<ActualResult<DirectoryDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var materialAid = _database.MaterialAidRepository.Get(id);
                if (materialAid.IsValid == false && materialAid.ErrorsList.Count > 0 || materialAid.Result == null)
                {
                    return new ActualResult<DirectoryDTO> { IsValid = false, ErrorsList = materialAid.ErrorsList };
                }
                return new ActualResult<DirectoryDTO> { Result = new DirectoryDTO { Id = materialAid.Result.Id, Name = materialAid.Result.Name } };
            });
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO item)
        {
            var materialAid = _database.MaterialAidRepository.Create(new MaterialAid { Name = item.Name });
            if (materialAid.IsValid == false && materialAid.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = materialAid.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            materialAid.IsValid = dbState.IsValid;
            return materialAid;
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO item)
        {
            var materialAid = _database.MaterialAidRepository.Update(new MaterialAid { Id = item.Id, Name = item.Name });
            if (materialAid.IsValid == false && materialAid.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = materialAid.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            materialAid.IsValid = dbState.IsValid;
            return materialAid;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var materialAid = _database.MaterialAidRepository.Delete(id);
            if (materialAid.IsValid == false && materialAid.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = materialAid.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            materialAid.IsValid = dbState.IsValid;
            return materialAid;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var materialAid = _database.MaterialAidRepository.Find(p => p.Name == name);
                return materialAid.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
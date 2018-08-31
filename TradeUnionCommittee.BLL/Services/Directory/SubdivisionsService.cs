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
    public class SubdivisionsService : ISubdivisionsService
    {
        private readonly IUnitOfWork _database;

        public SubdivisionsService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subdivisions, SubdivisionDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Subdivisions>>, ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == null));
            });
        }

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetSubordinateSubdivisions(long id)
        {
            return await Task.Run(() =>
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Subdivisions, SubdivisionDTO>()).CreateMapper();
                return mapper.Map<ActualResult<IEnumerable<Subdivisions>>, ActualResult<IEnumerable<SubdivisionDTO>>>(_database.SubdivisionsRepository.Find(x => x.IdSubordinate == id));
            });
        }

        public async Task<ActualResult<SubdivisionDTO>> GetAsync(long id)
        {
            return await Task.Run(() =>
            {
                var subdivision = _database.SubdivisionsRepository.Get(id);
                if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0 || subdivision.Result == null)
                {
                    return new ActualResult<SubdivisionDTO>
                    {
                        IsValid = false, ErrorsList = subdivision.ErrorsList
                    };
                }
                return new ActualResult<SubdivisionDTO>
                {
                    Result = new SubdivisionDTO
                    {
                        Id = subdivision.Result.Id,
                        Name = subdivision.Result.Name,
                        Abbreviation = subdivision.Result.Abbreviation
                    }
                };
            });
        }

        public async Task<ActualResult> CreateAsync(SubdivisionDTO item)
        {
            var subdivision = _database.SubdivisionsRepository.Create(new Subdivisions
            {
                Name = item.Name,
                Abbreviation = item.Abbreviation,
                IdSubordinate = item.IdSubordinate
            });
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            subdivision.IsValid = dbState.IsValid;
            return subdivision;
        }

        public async Task<ActualResult> UpdateAsync(SubdivisionDTO dto)
        {
            var sub = _database.SubdivisionsRepository.Get(dto.Id);
            sub.Result.Name = dto.Name;
            var subdivision = _database.SubdivisionsRepository.Update(sub.Result);

            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            subdivision.IsValid = dbState.IsValid;
            return subdivision;
        }

        public async Task<ActualResult> UpdateAbbreviation(SubdivisionDTO dto)
        {
            var sub = _database.SubdivisionsRepository.Get(dto.Id);
            sub.Result.Abbreviation = dto.Abbreviation;
            var subdivision = _database.SubdivisionsRepository.Update(sub.Result);

            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            subdivision.IsValid = dbState.IsValid;
            return subdivision;

        }

        public async Task<ActualResult> RestructuringUnits(SubdivisionDTO dto)
        {
            var sub = _database.SubdivisionsRepository.Get(dto.Id);
            sub.Result.IdSubordinate = dto.IdSubordinate;
            var subdivision = _database.SubdivisionsRepository.Update(sub.Result);

            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            subdivision.IsValid = dbState.IsValid;
            return subdivision;
        }

        public async Task<ActualResult> DeleteAsync(long id)
        {
            var subdivision = _database.SubdivisionsRepository.Delete(id);
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            subdivision.IsValid = dbState.IsValid;
            return subdivision;
        }

        public async Task<ActualResult> CheckNameAsync(string name)
        {
            return await Task.Run(() =>
            {
                var subdivision = _database.SubdivisionsRepository.Find(p => p.Name == name);
                return subdivision.Result.Any() ?
                    new ActualResult { IsValid = false } :
                    new ActualResult { IsValid = true };
            });
        }

        public async Task<ActualResult> CheckAbbreviationAsync(string name)
        {
            return await Task.Run(() =>
            {
                var subdivision = _database.SubdivisionsRepository.Find(p => p.Abbreviation == name);
                return subdivision.Result.Any() ?
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
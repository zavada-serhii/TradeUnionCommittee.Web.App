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

        public async Task<ActualResult<IEnumerable<SubdivisionDTO>>> GetAll()
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

        public async Task<ActualResult<SubdivisionDTO>> Get(long id)
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
                        DeptName = subdivision.Result.DeptName
                    }
                };
            });
        }

        public async Task<ActualResult> Create(SubdivisionDTO item)
        {
            var subdivision = _database.SubdivisionsRepository.Create(new Subdivisions { DeptName = item.DeptName, IdSubordinate = item.IdSubordinate });
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            await _database.SaveAsync();
            return subdivision;
        }

        public async Task<ActualResult> Update(SubdivisionDTO item)
        {
            var subdivision = _database.SubdivisionsRepository.Update(new Subdivisions
            {
                Id = item.Id,
                DeptName = item.DeptName
            });
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            await _database.SaveAsync();
            return subdivision;
        }

        public async Task<ActualResult> RestructuringUnits(SubdivisionDTO dto)
        {
            var subdivision = _database.SubdivisionsRepository.Update(new Subdivisions
            {
                Id = dto.Id,
                IdSubordinate = dto.IdSubordinate
            });
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            await _database.SaveAsync();
            return subdivision;
        }

        public async Task<ActualResult> Delete(long id)
        {
            var subdivision = _database.SubdivisionsRepository.Delete(id);
            if (subdivision.IsValid == false && subdivision.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = subdivision.ErrorsList };
            }
            await _database.SaveAsync();
            return subdivision;
        }

        public async Task<ActualResult> CheckName(string name)
        {
            return await Task.Run(() =>
            {
                var subdivision = _database.SubdivisionsRepository.Find(p => p.DeptName == name);
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
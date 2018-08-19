using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class ScientificService : IScientificService
    {
        private readonly IUnitOfWork _database;

        public ScientificService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.ScientificRepository.GetAll().Result.Select(x => x.ScientificDegree).Distinct().ToList()
                };
            });
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            return await Task.Run(() =>
            {
                return new ActualResult<IEnumerable<string>>
                {
                    Result = _database.ScientificRepository.GetAll().Result.Select(x => x.ScientificTitle).Distinct().ToList()
                };
            });
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<ActualResult<ScientificDTO>> GetScientificEmployeeAsync(long idEmployee)
        {
            return await Task.Run(() =>
            {
                var scientific = _database.ScientificRepository.GetWithInclude(x => x.IdEmployee == idEmployee).Result.FirstOrDefault();
                var result = new ActualResult<ScientificDTO>();
                if (scientific != null)
                {
                    result.Result = new ScientificDTO
                    {
                        IdEmployee = scientific.IdEmployee,
                        ScientificDegree = scientific.ScientificDegree,
                        ScientificTitle = scientific.ScientificTitle,
                    };
                    return result;
                }
                result.IsValid = false;
                return result;
            });
        }

        public Task<ActualResult> UpdateScientificEmployeeAsync(ScientificDTO dto)
        {
            throw new System.NotImplementedException();
        }
    }
}
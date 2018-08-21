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
    public class QualificationService : IQualificationService
    {
        private readonly IUnitOfWork _database;

        public QualificationService(IUnitOfWork database)
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
   
        public async Task<ActualResult<QualificationDTO>> GetQualificationEmployeeAsync(long idEmployee)
        {
            return await Task.Run(() =>
            {
                var scientific = _database.ScientificRepository.GetWithInclude(x => x.IdEmployee == idEmployee).Result.FirstOrDefault();
                var result = new ActualResult<QualificationDTO>();
                if (scientific != null)
                {
                    result.Result = new QualificationDTO
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

        public async Task<ActualResult> CreateQualificationEmployeeAsync(QualificationDTO dto)
        {
            var qualification = _database.ScientificRepository.Create(new Scientific
            {
                IdEmployee = dto.IdEmployee,
                ScientificDegree = dto.ScientificDegree,
                ScientificTitle = dto.ScientificTitle
            });
            if (qualification.IsValid == false && qualification.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = qualification.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            qualification.IsValid = dbState.IsValid;
            return qualification;
        }

        public async Task<ActualResult> UpdateQualificationEmployeeAsync(QualificationDTO dto)
        {
            var oldData = _database.ScientificRepository.GetWithInclude(x => x.IdEmployee == dto.IdEmployee).Result.FirstOrDefault();

            oldData.ScientificDegree = dto.ScientificDegree;
            oldData.ScientificTitle = dto.ScientificTitle;

            var qualification = _database.ScientificRepository.Update(oldData);

            if (qualification.IsValid == false && qualification.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = qualification.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            qualification.IsValid = dbState.IsValid;
            return qualification;
        }

        public async Task<ActualResult> DeleteQualificationEmployeeAsync(long idEmployee)
        {
            var id = _database.ScientificRepository.GetWithInclude(x => x.IdEmployee == idEmployee).Result.FirstOrDefault().Id;

            var qualification = _database.ScientificRepository.Delete(id);
            if (qualification.IsValid == false && qualification.ErrorsList.Count > 0)
            {
                return new ActualResult { IsValid = false, ErrorsList = qualification.ErrorsList };
            }
            var dbState = await _database.SaveAsync();
            qualification.IsValid = dbState.IsValid;
            return qualification;
        }
    }
}
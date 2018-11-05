using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class QualificationService : IQualificationService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapper;
        private readonly IHashIdUtilities _hashIdUtilities;

        public QualificationService(IUnitOfWork database, IAutoMapperUtilities mapper, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
            _mapper = mapper;
            _hashIdUtilities = hashIdUtilities;
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificDegreeAsync()
        {
            var result = await _database.ScientificRepository.GetAll();
            return new ActualResult<IEnumerable<string>> {Result = result.Result.Select(x => x.ScientificDegree).Distinct().ToList()};
        }

        public async Task<ActualResult<IEnumerable<string>>> GetAllScientificTitleAsync()
        {
            var result = await _database.ScientificRepository.GetAll();
            return new ActualResult<IEnumerable<string>> { Result = result.Result.Select(x => x.ScientificTitle).Distinct().ToList() };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
   
        public async Task<ActualResult<QualificationDTO>> GetQualificationEmployeeAsync(string hashId)
        {
            var checkDecrypt = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Qualification);
            if (checkDecrypt.IsValid)
            {
                var scientific = await _database.ScientificRepository.Get(checkDecrypt.Result);
                if (scientific.Result != null)
                {
                    return _mapper.Mapper.Map<ActualResult<QualificationDTO>>(scientific);
                }
                return new ActualResult<QualificationDTO>(Errors.TupleDeleted);
            }
            return new ActualResult<QualificationDTO>(checkDecrypt.ErrorsList);
        }

        public async Task<ActualResult> CreateQualificationEmployeeAsync(QualificationDTO dto)
        {
            await _database.ScientificRepository.Create(_mapper.Mapper.Map<Scientific>(dto));
            return _mapper.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<ActualResult> UpdateQualificationEmployeeAsync(QualificationDTO dto)
        {
            var checkDecrypt = await _hashIdUtilities.CheckDecryptWithId(dto.HashId, Enums.Services.Qualification);
            if (checkDecrypt.IsValid)
            {
                dto.Id = checkDecrypt.Result;
                await _database.ScientificRepository.Update(_mapper.Mapper.Map<Scientific>(dto));
                return _mapper.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(checkDecrypt.ErrorsList);
        }

        public async Task<ActualResult> DeleteQualificationEmployeeAsync(string hashId)
        {
            var checkDecrypt = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Qualification);
            if (checkDecrypt.IsValid)
            {
                await _database.ScientificRepository.Delete(checkDecrypt.Result);
                return _mapper.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult<QualificationDTO>(checkDecrypt.ErrorsList);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
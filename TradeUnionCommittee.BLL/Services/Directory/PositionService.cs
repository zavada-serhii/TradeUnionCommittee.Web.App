using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Directory
{
    public class PositionService : IPositionService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;
        private readonly IPdfService _pdfService;

        public PositionService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities, IPdfService pdfService)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
            _pdfService = pdfService;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(await _database.PositionRepository.GetAll());

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            //var reportPdfDto = new ReportPdfDTO
            //{
            //    HashUserId = 1,
            //    PathToSave = @"E:\PDF\Report\",
            //    StartDate = DateTime.Now.Date.AddYears(-15),
            //    EndDate = DateTime.Now.Date.AddYears(5)
            //};

            //for (var i = 0; i < 8; i++)
            //{
            //    reportPdfDto.Type = (ReportType)i;
            //    await _pdfService.CreateReport(reportPdfDto);
            //}

            var id = _hashIdUtilities.DecryptLong(hashId, Enums.Services.Position);
            return _mapperService.Mapper.Map<ActualResult<DirectoryDTO>>(await _database.PositionRepository.Get(id));
        }

        public async Task<ActualResult> CreateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.PositionRepository.Create(_mapperService.Mapper.Map<Position>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> UpdateAsync(DirectoryDTO dto)
        {
            if (!await CheckNameAsync(dto.Name))
            {
                await _database.PositionRepository.Update(_mapperService.Mapper.Map<Position>(dto));
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(Errors.DuplicateData);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            await _database.PositionRepository.Delete(_hashIdUtilities.DecryptLong(hashId, Enums.Services.Position));
            return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.PositionRepository.Find(p => p.Name == name);
            return result.Result.Any();
        }


        public void Dispose()
        {
            _database.Dispose();
            _pdfService.Dispose();
        }
    }
}
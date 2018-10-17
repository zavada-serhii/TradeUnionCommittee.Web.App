using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.Services.Search;
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
        private readonly IReportService _reportService;

        public PositionService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities, IReportService reportService)
        {
            _database = database;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
            _reportService = reportService;
        }

        public async Task<ActualResult<IEnumerable<DirectoryDTO>>> GetAllAsync() =>
            _mapperService.Mapper.Map<ActualResult<IEnumerable<DirectoryDTO>>>(await _database.PositionRepository.GetAll());

        public async Task<ActualResult<DirectoryDTO>> GetAsync(string hashId)
        {
            ReportDTO dto = new ReportDTO
            {
                HashId = 1,
                PathToSave = @"E:\",
                StartDate = DateTime.Now.Date.AddYears(-15),
                EndDate = DateTime.Now.Date.AddYears(5)
            };

            //await _reportService.CreateReport(dto, ReportType.MaterialAid);
            //await _reportService.CreateReport(dto, ReportType.Award);
            //await _reportService.CreateReport(dto, ReportType.Travel);
            //await _reportService.CreateReport(dto, ReportType.Wellness);
            //await _reportService.CreateReport(dto, ReportType.Tour);
            //await _reportService.CreateReport(dto, ReportType.Cultural);
            //await _reportService.CreateReport(dto, ReportType.Gift);


            ITestReportService testReportService = new TestReportService(_database);
            await testReportService.CreateReport(dto, ReportType.MaterialAid);



            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Position);
            return check.IsValid 
                ? _mapperService.Mapper.Map<ActualResult<DirectoryDTO>>(await _database.PositionRepository.Get(check.Result)) 
                : new ActualResult<DirectoryDTO>(check.ErrorsList);
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
            var check = await _hashIdUtilities.CheckDecryptWithId(dto.HashId, Enums.Services.Position);
            if (check.IsValid)
            {
                if (!await CheckNameAsync(dto.Name))
                {
                    await _database.PositionRepository.Update(_mapperService.Mapper.Map<Position>(dto));
                    return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
                }
                return new ActualResult(Errors.DuplicateData);
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            var check = await _hashIdUtilities.CheckDecryptWithId(hashId, Enums.Services.Position);
            if (check.IsValid)
            {
                await _database.PositionRepository.Delete(check.Result);
                return _mapperService.Mapper.Map<ActualResult>(await _database.SaveAsync());
            }
            return new ActualResult(check.ErrorsList);
        }

        public async Task<bool> CheckNameAsync(string name)
        {
            var result = await _database.PositionRepository.Find(p => p.Name == name);
            return result.Result.Any();
        }


        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
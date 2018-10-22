using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Services.Search;
using TradeUnionCommittee.BLL.TestPDF.DTO;
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
            //C:\Users\zavad\Desktop\Pdf\Report\
            //E:\Temp\
            var reportPdfDto = new ReportPdfDTO
            {
                HashUserId = 1,
                PathToSave = @"C:\Users\zavad\Desktop\Pdf\Report\",
                StartDate = DateTime.Now.Date.AddYears(-15),
                EndDate = DateTime.Now.Date.AddYears(5)
            };

            for (var i = 0; i < 8; i++)
            {
                reportPdfDto.Type = (ReportType)i;
                await _pdfService.CreateReport(reportPdfDto);
            }

            //---------------------------------------------------------------------------------

            //C:\Users\zavad\Desktop\Pdf\Search\
            //E:\Temp\
            //var searchPdfDto = new SearchPdfDTO
            //{
            //    HashEventId = 1,
            //    PathToSave = @"C:\Users\zavad\Desktop\Pdf\Search\",
            //    StartDate = DateTime.Now.Date.AddYears(-15),
            //    EndDate = DateTime.Now.Date.AddYears(5)
            //};

            //for (var i = 0; i < 5; i++)
            //{
            //    searchPdfDto.Type = (SearchType)i;
            //    await _pdfService.CreateSearch(searchPdfDto);
            //}

            //---------------------------------------------------------------------------------

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
            _pdfService.Dispose();
        }
    }
}
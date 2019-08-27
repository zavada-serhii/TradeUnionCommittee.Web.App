using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.CloudStorage.Service.Interfaces;
using TradeUnionCommittee.CloudStorage.Service.Model;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Interfaces;
using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.BLL.Services.PDF
{
    internal class PdfService : IPdfService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly AutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;
        private readonly IReportGeneratorService _reportGeneratorService;
        private readonly IReportPdfBucketService _pdfBucketService;

        public PdfService(TradeUnionCommitteeContext context, AutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities, IReportPdfBucketService pdfBucketService, IReportGeneratorService reportGeneratorService)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
            _pdfBucketService = pdfBucketService;
            _reportGeneratorService = reportGeneratorService;
        }

        //------------------------------------------------------------------------------------------

        public async Task<ActualResult<FileDTO>> CreateReport(ReportPdfDTO dto)
        {
            try
            {
                var fileName = Guid.NewGuid().ToString();
                var model = await FillModelReport(dto, fileName);
                var pdf = _reportGeneratorService.Generate(model);

                await _pdfBucketService.PutPdfObject(new ReportPdfBucketModel
                {
                    HashIdEmployee = dto.HashIdEmployee,
                    FileName = fileName,
                    EmailUser = dto.EmailUser,
                    IpUser = dto.IpUser,
                    TypeReport = (int)model.Type,
                    DateFrom = model.StartDate,
                    DateTo = model.EndDate,
                    Data = pdf
                });

                return new ActualResult<FileDTO> { Result = new FileDTO { FileName = fileName, Data = pdf} };
            }
            catch (Exception exception)
            {
                return new ActualResult<FileDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        //------------------------------------------------------------------------------------------

        private async Task<ReportModel> FillModelReport(ReportPdfDTO dto, string fileName)
        {
            var model = new ReportModel
            {
                HashIdEmployee = dto.HashIdEmployee,
                FileName = fileName,
                Type = (TradeUnionCommittee.PDF.Service.Enums.TypeReport)dto.Type,
                FullNameEmployee = await GetFullNameEmployee(dto),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            switch (dto.Type)
            {
                case TypeReport.All:
                    model.MaterialAidEmployees = await GetMaterialAid(dto);
                    model.AwardEmployees = await GetAward(dto);
                    model.EventEmployees = await AddAllEvent(dto);
                    model.CulturalEmployees = await GetCultural(dto);
                    model.GiftEmployees = await GetGift(dto);
                    break;
                case TypeReport.MaterialAid:
                    model.MaterialAidEmployees = await GetMaterialAid(dto);
                    break;
                case TypeReport.Award:
                    model.AwardEmployees = await GetAward(dto);
                    break;
                case TypeReport.Travel:
                    model.EventEmployees = await GetEvent(dto, TypeEvent.Travel);
                    break;
                case TypeReport.Wellness:
                    model.EventEmployees = await GetEvent(dto, TypeEvent.Wellness);
                    break;
                case TypeReport.Tour:
                    model.EventEmployees = await GetEvent(dto, TypeEvent.Tour);
                    break;
                case TypeReport.Cultural:
                    model.CulturalEmployees = await GetCultural(dto);
                    break;
                case TypeReport.Gift:
                    model.GiftEmployees = await GetGift(dto);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return model;
        }

        //------------------------------------------------------------------------------------------

        private async Task<string> GetFullNameEmployee(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var employee = await _context.Employee.FindAsync(id);
            return $"{employee.FirstName} {employee.SecondName} {employee.Patronymic}";
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetMaterialAid(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var result = await _context.MaterialAidEmployees
                .Include(x => x.IdMaterialAidNavigation)
                .Where(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateIssue)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetAward(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var result = await _context.AwardEmployees
                .Include(x => x.IdAwardNavigation)
                .Where(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateIssue)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<CulturalEmployeeEntity>> GetCultural(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var result = await _context.CulturalEmployees
                .Include(x => x.IdCulturalNavigation)
                .Where(x => x.DateVisit.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateVisit)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<CulturalEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<EventEmployeeEntity>> GetEvent(ReportPdfDTO dto, TypeEvent typeEvent)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var result = await _context.EventEmployees
                .Include(x => x.IdEventNavigation)
                .Where(x => x.StartDate.Between(dto.StartDate, dto.EndDate) &&
                            x.EndDate.Between(dto.StartDate, dto.EndDate) &&
                            x.IdEventNavigation.Type == typeEvent &&
                            x.IdEmployee == id)
                .OrderBy(x => x.StartDate)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<EventEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<GiftEmployeeEntity>> GetGift(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashIdEmployee);
            var result = await _context.GiftEmployees
                .Where(x => x.DateGift.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateGift)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<GiftEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<EventEmployeeEntity>> AddAllEvent(ReportPdfDTO dto)
        {
            var result = new List<EventEmployeeEntity>();
            result.AddRange(await GetEvent(dto, TypeEvent.Travel));
            result.AddRange(await GetEvent(dto, TypeEvent.Wellness));
            result.AddRange(await GetEvent(dto, TypeEvent.Tour));
            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
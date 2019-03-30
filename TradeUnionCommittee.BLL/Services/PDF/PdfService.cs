using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Configurations;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.EF;
using TradeUnionCommittee.DAL.Enums;
using TradeUnionCommittee.PDF.Service;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.BLL.Services.PDF
{
    internal class PdfService : IPdfService
    {
        private readonly TradeUnionCommitteeContext _context;
        private readonly IAutoMapperConfiguration _mapperService;
        private readonly HashIdConfiguration _hashIdUtilities;

        public PdfService(TradeUnionCommitteeContext context, IAutoMapperConfiguration mapperService, HashIdConfiguration hashIdUtilities)
        {
            _context = context;
            _mapperService = mapperService;
            _hashIdUtilities = hashIdUtilities;
        }

        //------------------------------------------------------------------------------------------

        public async Task<ActualResult<byte[]>> CreateReport(ReportPdfDTO dto)
        {
            var pathToFile = new ReportGenerator().Generate(await FillModelReport(dto));
            if (!string.IsNullOrEmpty(pathToFile))
            {
                byte[] data;
                using (var fstream = File.OpenRead(pathToFile))
                {
                    data = new byte[fstream.Length];
                    await fstream.ReadAsync(data, 0, data.Length);
                }
                return new ActualResult<byte[]> {Result = data};
            }
            return new ActualResult<byte[]>(Errors.FileNotFound);
        }

        //------------------------------------------------------------------------------------------

        private async Task<ReportModel> FillModelReport(ReportPdfDTO dto)
        {
            var model = new ReportModel
            {
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
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
            var employee = await _context.Employee.FindAsync(id);
            return $"{employee.FirstName} {employee.SecondName} {employee.Patronymic}";
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetMaterialAid(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
            var result = await _context.MaterialAidEmployees
                .Include(x => x.IdMaterialAidNavigation)
                .Where(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateIssue)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetAward(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
            var result = await _context.AwardEmployees
                .Include(x => x.IdAwardNavigation)
                .Where(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateIssue)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<CulturalEmployeeEntity>> GetCultural(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
            var result = await _context.CulturalEmployees
                .Include(x => x.IdCulturalNavigation)
                .Where(x => x.DateVisit.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id)
                .OrderBy(x => x.DateVisit)
                .ToListAsync();
            return _mapperService.Mapper.Map<IEnumerable<CulturalEmployeeEntity>>(result);
        }

        private async Task<IEnumerable<EventEmployeeEntity>> GetEvent(ReportPdfDTO dto, TypeEvent typeEvent)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
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
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId);
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
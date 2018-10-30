using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.PDF.DTO;
using TradeUnionCommittee.BLL.PDF.Models;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.PDF
{
    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _database;
        private ReportPdfDTO _reportPdfDto;

        public PdfService(IUnitOfWork database)
        {
            _database = database;
        }

        //------------------------------------------------------------------------------------------

        public async Task CreateReport(ReportPdfDTO dto)
        {
            _reportPdfDto = dto;
            var model = await FillModelReport();
            new ReportGenerator().Generate(model);
        }

        //------------------------------------------------------------------------------------------

        private async Task<ReportModel> FillModelReport()
        {
            var model = new ReportModel
            {
                Type = _reportPdfDto.Type,
                PathToSave = _reportPdfDto.PathToSave,
                FullNameEmployee = await GetFullNameEmployee(),
                StartDate = _reportPdfDto.StartDate,
                EndDate = _reportPdfDto.EndDate
            };

            switch (_reportPdfDto.Type)
            {
                case ReportType.All:
                    model.MaterialAidEmployees = await GetMaterialAid();
                    model.AwardEmployees = await GetAward();
                    model.EventEmployees = await AddAllEvent();
                    model.CulturalEmployees = await GetCultural();
                    model.GiftEmployees = await GetGift();
                    break;
                case ReportType.MaterialAid:
                    model.MaterialAidEmployees = await GetMaterialAid();
                    break;
                case ReportType.Award:
                    model.AwardEmployees = await GetAward();
                    break;
                case ReportType.Travel:
                    model.EventEmployees = await GetEvent(TypeEvent.Travel);
                    break;
                case ReportType.Wellness:
                    model.EventEmployees = await GetEvent(TypeEvent.Wellness);
                    break;
                case ReportType.Tour:
                    model.EventEmployees = await GetEvent(TypeEvent.Tour);
                    break;
                case ReportType.Cultural:
                    model.CulturalEmployees = await GetCultural();
                    break;
                case ReportType.Gift:
                    model.GiftEmployees = await GetGift();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return model;
        }

        //------------------------------------------------------------------------------------------

        private async Task<string> GetFullNameEmployee()
        {
            var employee = await _database.EmployeeRepository.Get(_reportPdfDto.HashUserId);
            var result = employee.Result;
            return $"{result.FirstName} {result.SecondName} {result.Patronymic}";
        }

        private async Task<IEnumerable<MaterialAidEmployees>> GetMaterialAid()
        {
            var result = await _database
                .MaterialAidEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                     x.IdEmployee == _reportPdfDto.HashUserId,
                                p => p.IdMaterialAidNavigation);
            return result.Result.OrderBy(x => x.DateIssue);
        }

        private async Task<IEnumerable<AwardEmployees>> GetAward()
        {
            var result = await _database
                .AwardEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                     x.IdEmployee == _reportPdfDto.HashUserId,
                                p => p.IdAwardNavigation);
            return result.Result.OrderBy(x => x.DateIssue);
        }

        private async Task<IEnumerable<CulturalEmployees>> GetCultural()
        {
            var resultReport = await _database
                .CulturalEmployeesRepository
                .GetWithInclude(x => x.DateVisit.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                     x.IdEmployee == _reportPdfDto.HashUserId,
                                p => p.IdCulturalNavigation);
            return resultReport.Result.OrderBy(x => x.DateVisit);
        }

        private async Task<IEnumerable<EventEmployees>> GetEvent(TypeEvent typeEvent)
        {
            var resultReport = await _database
                .EventEmployeesRepository
                .GetWithInclude(x => x.StartDate.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                     x.EndDate.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                     x.IdEventNavigation.Type == typeEvent &&
                                     x.IdEmployee == _reportPdfDto.HashUserId,
                                p => p.IdEventNavigation);
            return resultReport.Result.OrderBy(x => x.StartDate);
        }

        private async Task<IEnumerable<GiftEmployees>> GetGift()
        {
            var resultReport = await _database
                .GiftEmployeesRepository
                .GetWithInclude(x => x.DateGift.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) && x.IdEmployee == _reportPdfDto.HashUserId);
            return resultReport.Result.OrderBy(x => x.DateGift);
        }

        private async Task<IEnumerable<EventEmployees>> AddAllEvent()
        {
            var result = new List<EventEmployees>();
            result.AddRange(await GetEvent(TypeEvent.Travel));
            result.AddRange(await GetEvent(TypeEvent.Wellness));
            result.AddRange(await GetEvent(TypeEvent.Tour));
            return result;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}
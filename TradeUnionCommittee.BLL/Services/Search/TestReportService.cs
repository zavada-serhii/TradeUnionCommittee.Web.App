using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.TestPDF;
using TradeUnionCommittee.BLL.TestPDF.Models;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public interface ITestReportService
    {
        Task CreateReport(ReportDTO dto, ReportType type);
        void Dispose();
    }

    public class TestReportService : ITestReportService
    {
        private readonly IUnitOfWork _database;
        private ReportDTO _reportDto;
        private ReportType _reportType;

        public TestReportService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task CreateReport(ReportDTO dto, ReportType type)
        {
            _reportDto = dto;
            _reportType = type;

            new GeneratePdf().Generate(await FillModel());
        }

        private async Task<TestReportModel> FillModel()
        {
            var model = new TestReportModel
            {
                Type = _reportType,
                PathToSave = _reportDto.PathToSave,
                FullNameEmployee = await GetFullNameEmployee(),
                StartDate = _reportDto.StartDate,
                EndDate = _reportDto.EndDate
            };

            switch (_reportType)
            {
                case ReportType.All:
                    model.MaterialAidEmployees = await GetMaterialAid();
                    model.AwardEmployees = await GetAward();
                    model.EventEmployees = await GetEvent(TypeEvent.Travel);
                    model.EventEmployees = await GetEvent(TypeEvent.Wellness);
                    model.EventEmployees = await GetEvent(TypeEvent.Tour);
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
            var employee = await _database.EmployeeRepository.Get(_reportDto.HashId);
            var result = employee.Result;
            return $"{result.FirstName} {result.SecondName} {result.Patronymic}";
        }

        private async Task<IEnumerable<MaterialAidEmployees>> GetMaterialAid()
        {
            var result = await _database
                .MaterialAidEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(_reportDto.StartDate, _reportDto.EndDate) &&
                                     x.IdEmployee == _reportDto.HashId,
                                p => p.IdMaterialAidNavigation);

            return result.Result.OrderBy(x => x.DateIssue);
        }

        private async Task<IEnumerable<AwardEmployees>> GetAward()
        {
            var result = await _database
                .AwardEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(_reportDto.StartDate, _reportDto.EndDate) &&
                                     x.IdEmployee == _reportDto.HashId,
                                p => p.IdAwardNavigation);

            return result.Result.OrderBy(x => x.DateIssue);
        }

        private async Task<IEnumerable<CulturalEmployees>> GetCultural()
        {
            var result = await _database
                .CulturalEmployeesRepository
                .GetWithInclude(x => x.DateVisit.Between(_reportDto.StartDate, _reportDto.EndDate) &&
                                     x.IdEmployee == _reportDto.HashId,
                                p => p.IdCulturalNavigation);

            return result.Result.OrderBy(x => x.DateVisit);
        }

        private async Task<IEnumerable<EventEmployees>> GetEvent(TypeEvent typeEvent)
        {
            var result = await _database
                .EventEmployeesRepository
                .GetWithInclude(x => x.StartDate.Between(_reportDto.StartDate, _reportDto.EndDate) &&
                                     x.EndDate.Between(_reportDto.StartDate, _reportDto.EndDate) &&
                                     x.IdEventNavigation.Type == typeEvent &&
                                     x.IdEmployee == _reportDto.HashId,
                                p => p.IdEventNavigation);

            return result.Result.OrderBy(x => x.StartDate);
        }

        private async Task<IEnumerable<GiftEmployees>> GetGift()
        {
            var result = await _database
                .GiftEmployeesRepository
                .GetWithInclude(x => x.DateGift.Between(_reportDto.StartDate, _reportDto.EndDate) && x.IdEmployee == _reportDto.HashId);

            return result.Result.OrderBy(x => x.DateGift);
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}

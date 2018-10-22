using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.TestPDF;
using TradeUnionCommittee.BLL.TestPDF.DTO;
using TradeUnionCommittee.BLL.TestPDF.Models;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public interface IPdfService
    {
        Task CreateReport(ReportPdfDTO dto);
        Task CreateSearch(SearchPdfDTO dto);
        void Dispose();
    }

    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _database;
        private ReportPdfDTO _reportPdfDto;
        private SearchPdfDTO _searchPdfDto;

        public PdfService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task CreateReport(ReportPdfDTO dto)
        {
            _reportPdfDto = dto;
            new PdfGenerator().GenerateReport(await FillModelReport());
        }

        public async Task CreateSearch(SearchPdfDTO dto)
        {
            _searchPdfDto = dto;
            new PdfGenerator().GenerateSearch(await FillModelSearch());
            //Travel
            //Wellness
            //Tour
            //Cultural
            //Gift - by name event 
        }



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
                    model.CulturalEmployees = await GetCultural(PdfType.Report);
                    model.GiftEmployees = await GetGift(PdfType.Report);
                    break;
                case ReportType.MaterialAid:
                    model.MaterialAidEmployees = await GetMaterialAid();
                    break;
                case ReportType.Award:
                    model.AwardEmployees = await GetAward();
                    break;
                case ReportType.Travel:
                    model.EventEmployees = await GetEvent(TypeEvent.Travel, PdfType.Report);
                    break;
                case ReportType.Wellness:
                    model.EventEmployees = await GetEvent(TypeEvent.Wellness, PdfType.Report);
                    break;
                case ReportType.Tour:
                    model.EventEmployees = await GetEvent(TypeEvent.Tour, PdfType.Report);
                    break;
                case ReportType.Cultural:
                    model.CulturalEmployees = await GetCultural(PdfType.Report);
                    break;
                case ReportType.Gift:
                    model.GiftEmployees = await GetGift(PdfType.Report);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return model;
        }

        private async Task<SearchModel> FillModelSearch()
        {
            var model = new SearchModel
            {
                Type = _searchPdfDto.Type,
                PathToSave = _searchPdfDto.PathToSave,
                StartDate = _searchPdfDto.StartDate,
                EndDate = _searchPdfDto.EndDate
            };

            switch (_searchPdfDto.Type)
            {
                case SearchType.Travel:
                    model.EventEmployees = await GetEvent(TypeEvent.Travel, PdfType.Search);
                    break;
                case SearchType.Wellness:
                    model.EventEmployees = await GetEvent(TypeEvent.Wellness, PdfType.Search);
                    break;
                case SearchType.Tour:
                    model.EventEmployees = await GetEvent(TypeEvent.Tour, PdfType.Search);
                    break;
                case SearchType.Cultural:
                    model.CulturalEmployees = await GetCultural(PdfType.Search);
                    break;
                case SearchType.Gift:
                    model.GiftEmployees = await GetGift(PdfType.Search);
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

        private async Task<IEnumerable<CulturalEmployees>> GetCultural(PdfType type)
        {
            switch (type)
            {
                case PdfType.Report:
                    var resultReport = await _database
                        .CulturalEmployeesRepository
                        .GetWithInclude(x => x.DateVisit.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                             x.IdEmployee == _reportPdfDto.HashUserId,
                                        p => p.IdCulturalNavigation);

                    return resultReport.Result.OrderBy(x => x.DateVisit);
                case PdfType.Search:
                    var resultSearch = await _database
                        .CulturalEmployeesRepository
                        .GetWithInclude(x => x.DateVisit.Between(_searchPdfDto.StartDate, _searchPdfDto.EndDate) &&
                                             x.IdCultural == _searchPdfDto.HashEventId,
                                        p => p.IdCulturalNavigation);

                    return resultSearch.Result.OrderBy(x => x.DateVisit);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private async Task<IEnumerable<EventEmployees>> GetEvent(TypeEvent typeEvent, PdfType type)
        {
            switch (type)
            {
                case PdfType.Report:
                    var resultReport = await _database
                        .EventEmployeesRepository
                        .GetWithInclude(x => x.StartDate.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                             x.EndDate.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) &&
                                             x.IdEventNavigation.Type == typeEvent &&
                                             x.IdEmployee == _reportPdfDto.HashUserId,
                                        p => p.IdEventNavigation);

                    return resultReport.Result.OrderBy(x => x.StartDate);
                case PdfType.Search:

                    var resultSearch = await _database
                        .EventEmployeesRepository
                        .GetWithInclude(x => x.StartDate.Between(_searchPdfDto.StartDate, _searchPdfDto.EndDate) &&
                                             x.EndDate.Between(_searchPdfDto.StartDate, _searchPdfDto.EndDate) &&
                                             x.IdEventNavigation.Type == typeEvent &&
                                             x.IdEvent == _searchPdfDto.HashEventId,
                                        p => p.IdEventNavigation);

                    return resultSearch.Result.OrderBy(x => x.StartDate);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private async Task<IEnumerable<GiftEmployees>> GetGift(PdfType type)
        {
            switch (type)
            {
                case PdfType.Report:
                    var resultReport = await _database
                        .GiftEmployeesRepository
                        .GetWithInclude(x => x.DateGift.Between(_reportPdfDto.StartDate, _reportPdfDto.EndDate) && x.IdEmployee == _reportPdfDto.HashUserId);

                    return resultReport.Result.OrderBy(x => x.DateGift);
                case PdfType.Search:
                    var resultSearch = await _database
                        .GiftEmployeesRepository
                        .GetWithInclude(x => x.DateGift.Between(_searchPdfDto.StartDate, _searchPdfDto.EndDate) && 
                                             x.NameEvent == Convert.ToString(_searchPdfDto.HashEventId));

                    return resultSearch.Result.OrderBy(x => x.DateGift);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private async Task<IEnumerable<EventEmployees>> AddAllEvent()
        {
            var result = new List<EventEmployees>();
            result.AddRange(await GetEvent(TypeEvent.Travel, PdfType.Report));
            result.AddRange(await GetEvent(TypeEvent.Wellness, PdfType.Report));
            result.AddRange(await GetEvent(TypeEvent.Tour, PdfType.Report));
            return result;
        }

        public void Dispose()
        {
            _database.Dispose();
        }
    }
}

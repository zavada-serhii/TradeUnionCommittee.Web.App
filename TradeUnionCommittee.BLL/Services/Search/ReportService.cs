using System;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.PDF.Models;
using TradeUnionCommittee.BLL.PDF.ReportTemplates;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;

namespace TradeUnionCommittee.BLL.Services.Search
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _database;

        public ReportService(IUnitOfWork database)
        {
            _database = database;
        }

        public async Task CreateReport(PdfDTO dto, ReportType type)
        {
            switch (type)
            {
                case ReportType.All:
                    break;
                case ReportType.MaterialAid:
                    await CreateMaterialAidReport(dto);
                    break;
                case ReportType.Award:
                    await CreateAwardReport(dto);
                    break;
                case ReportType.Travel:
                    await CreateEventReport(dto, TypeEvent.Travel);
                    break;
                case ReportType.Wellness:
                    await CreateEventReport(dto, TypeEvent.Wellness);
                    break;
                case ReportType.Tour:
                    await CreateEventReport(dto, TypeEvent.Tour);
                    break;
                case ReportType.Cultural:
                    await CreateCulturalReport(dto);
                    break;
                case ReportType.Gift:
                    await CreateGiftReport(dto);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }   
        }

        private async Task CreateMaterialAidReport(PdfDTO dto)
        {
            var result = await _database
                         .MaterialAidEmployeesRepository
                         .GetWithInclude(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) && 
                                              x.IdEmployeeNavigation.Id == dto.HashUserId,
                                         p => p.IdEmployeeNavigation,
                                         p => p.IdMaterialAidNavigation);

            IReportTemplate template = new MaterialAidTemplate();
            template.CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MaterialAidEmployees = result.Result.OrderBy(x => x.DateIssue)
            });
        }

        private async Task CreateAwardReport(PdfDTO dto)
        {
            var result = await _database
                        .AwardEmployeesRepository
                        .GetWithInclude(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) &&
                                             x.IdEmployeeNavigation.Id == dto.HashUserId,
                                        p => p.IdEmployeeNavigation,
                                        p => p.IdAwardNavigation);

            IReportTemplate template = new AwardTemplate();
            template.CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                AwardEmployees = result.Result.OrderBy(x => x.DateIssue)
            });
        }

        private async Task CreateCulturalReport(PdfDTO dto)
        {
            var result = await _database
                .CulturalEmployeesRepository
                .GetWithInclude(x => x.DateVisit.Between(dto.StartDate,dto.EndDate) &&
                                     x.IdEmployeeNavigation.Id == dto.HashUserId,
                                p => p.IdEmployeeNavigation,
                                p => p.IdCulturalNavigation);

            IReportTemplate template = new CulturalTemplate();
            template.CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                CulturalEmployees = result.Result.OrderBy(x => x.DateVisit)
            });
        }

        private async Task CreateEventReport(PdfDTO dto, TypeEvent typeEvent)
        {
            var result = await _database
                .EventEmployeesRepository
                .GetWithInclude(x => (x.StartDate.Between(dto.StartDate, dto.EndDate) &&
                                     x.EndDate.Between(dto.StartDate, dto.EndDate)) &&
                                     x.IdEventNavigation.Type == typeEvent &&
                                     x.IdEmployeeNavigation.Id == dto.HashUserId,
                                p => p.IdEmployeeNavigation,
                                p => p.IdEventNavigation);

            IReportTemplate template = new EventTemplate();
            template.CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                EventEmployees = result.Result.OrderBy(x => x.StartDate)
            });
        }

        private async Task CreateGiftReport(PdfDTO dto)
        {
            var result = await _database
                .GiftEmployeesRepository
                .GetWithInclude(x => x.DateGift.Between(dto.StartDate, dto.EndDate) &&
                                     x.IdEmployeeNavigation.Id == dto.HashUserId,
                                p => p.IdEmployeeNavigation);

            IReportTemplate template = new GiftTemplate();
            template.CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                GiftEmployees = result.Result.OrderBy(x => x.DateGift)
            });
        }

        public void Dispose()
        {
           _database.Dispose();
        }
    }
}
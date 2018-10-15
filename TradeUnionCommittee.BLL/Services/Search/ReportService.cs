using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.BLL.PDF;
using TradeUnionCommittee.BLL.PDF.ReportTemplates;
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

        public async Task CreateReport(ReportDTO dto, ReportType type)
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
                    break;
                case ReportType.Wellness:
                    break;
                case ReportType.Tour:
                    break;
                case ReportType.Cultural:
                    break;
                case ReportType.Gift:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }   
        }

        private async Task CreateMaterialAidReport(ReportDTO dto)
        {
            var result = await _database
                         .MaterialAidEmployeesRepository
                         .GetWithInclude(x => x.DateIssue >= dto.StartDate && x.DateIssue <= dto.EndDate && 
                                              x.IdEmployeeNavigation.Id == dto.HashId,
                                         p => p.IdEmployeeNavigation,
                                         p => p.IdMaterialAidNavigation);

            new MaterialAidTemplate().CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                MaterialAidEmployees = result.Result
            });
        }

        private async Task CreateAwardReport(ReportDTO dto)
        {
            var result = await _database
                        .AwardEmployeesRepository
                        .GetWithInclude(x => x.DateIssue >= dto.StartDate && x.DateIssue <= dto.EndDate &&
                                             x.IdEmployeeNavigation.Id == dto.HashId,
                                        p => p.IdEmployeeNavigation,
                                        p => p.IdAwardNavigation);

            new AwardTemplate().CreateTemplateReport(new ReportModel
            {
                PathToSave = dto.PathToSave,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                AwardEmployees = result.Result
            });
        }

        public void Dispose()
        {
           _database.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Extensions;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.BLL.Utilities;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Common.Enums;
using TradeUnionCommittee.DAL.Entities;
using TradeUnionCommittee.DAL.Interfaces;
using TradeUnionCommittee.PDF.Service;
using TradeUnionCommittee.PDF.Service.Entities;
using TradeUnionCommittee.PDF.Service.Models;

namespace TradeUnionCommittee.BLL.Services.PDF
{
    public class PdfService : IPdfService
    {
        private readonly IUnitOfWork _database;
        private readonly IAutoMapperUtilities _mapperService;
        private readonly IHashIdUtilities _hashIdUtilities;

        public PdfService(IUnitOfWork database, IAutoMapperUtilities mapperService, IHashIdUtilities hashIdUtilities)
        {
            _database = database;
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
            var employee = await _database.EmployeeRepository.Get(_hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee));
            var result = employee.Result;
            return $"{result.FirstName} {result.SecondName} {result.Patronymic}";
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetMaterialAid(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee);
            var result = await _database
                .MaterialAidEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) &&
                                     x.IdEmployee == id,
                                p => p.IdMaterialAidNavigation);
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result.Result.OrderBy(x => x.DateIssue));
        }

        private async Task<IEnumerable<MaterialIncentivesEmployeeEntity>> GetAward(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee);
            var result = await _database
                .AwardEmployeesRepository
                .GetWithInclude(x => x.DateIssue.Between(dto.StartDate, dto.EndDate) &&
                                     x.IdEmployee == id,
                                p => p.IdAwardNavigation);
            return _mapperService.Mapper.Map<IEnumerable<MaterialIncentivesEmployeeEntity>>(result.Result.OrderBy(x => x.DateIssue));
        }

        private async Task<IEnumerable<CulturalEmployeeEntity>> GetCultural(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee);
            var result = await _database
                .CulturalEmployeesRepository
                .GetWithInclude(x => x.DateVisit.Between(dto.StartDate, dto.EndDate) &&
                                     x.IdEmployee == id,
                                p => p.IdCulturalNavigation);
            return _mapperService.Mapper.Map<IEnumerable<CulturalEmployeeEntity>>(result.Result.OrderBy(x => x.DateVisit));
        }

        private async Task<IEnumerable<EventEmployeeEntity>> GetEvent(ReportPdfDTO dto, TypeEvent typeEvent)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee);
            var result = await _database
                .EventEmployeesRepository
                .GetWithInclude(x => x.StartDate.Between(dto.StartDate, dto.EndDate) &&
                                     x.EndDate.Between(dto.StartDate, dto.EndDate) &&
                                     x.IdEventNavigation.Type == typeEvent &&
                                     x.IdEmployee == id,
                                p => p.IdEventNavigation);
            return _mapperService.Mapper.Map<IEnumerable<EventEmployeeEntity>>(result.Result.OrderBy(x => x.StartDate));
        }

        private async Task<IEnumerable<GiftEmployeeEntity>> GetGift(ReportPdfDTO dto)
        {
            var id = _hashIdUtilities.DecryptLong(dto.HashEmployeeId, Enums.Services.Employee);
            var result = await _database
                .GiftEmployeesRepository
                .GetWithInclude(x => x.DateGift.Between(dto.StartDate, dto.EndDate) && x.IdEmployee == id);
            return _mapperService.Mapper.Map<IEnumerable<GiftEmployeeEntity>>(result.Result.OrderBy(x => x.DateGift));
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
            _database.Dispose();
        }
    }
}
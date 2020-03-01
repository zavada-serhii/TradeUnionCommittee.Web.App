using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.Contracts.SystemAudit;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Helpers;
using TradeUnionCommittee.DAL.Audit.Entities;
using TradeUnionCommittee.DAL.Audit.Enums;
using TradeUnionCommittee.DAL.Audit.Repository;

namespace TradeUnionCommittee.BLL.Services.SystemAudit
{
    public class SystemAuditService : ISystemAuditService
    {
        private readonly ISystemAuditRepository _auditRepository;
        public SystemAuditService(ISystemAuditRepository auditRepository)
        {
            _auditRepository = auditRepository;
        }

        public async Task AuditAsync(string email, string ipUser, Enums.Operations operation, Enums.Tables table)
        {
            try
            {
                await _auditRepository.AuditAsync(new Journal { EmailUser = email, IpUser = ipUser, Operation = (Operations)operation, Table = (Tables)table });
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task AuditAsync(string email, string ipUser, Enums.Operations operation, Enums.Tables[] tables)
        {
            try
            {
                foreach (var table in tables)
                {
                    await AuditAsync(email, ipUser, operation, table);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task<ActualResult<IEnumerable<JournalDTO>>> FilterAsync(string email, DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _auditRepository.FilterAsync(email, startDate, endDate);
                return new ActualResult<IEnumerable<JournalDTO>>
                {
                    Result = result.Select(journal => new JournalDTO
                    {
                        EmailUser = journal.EmailUser,
                        DateTime = journal.DateTime,
                        Operation = dictionatyOperations[journal.Operation],
                        Tables = dictionatyTables[journal.Table],
                    }).ToList()
                };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<JournalDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _auditRepository.Dispose();
        }

        private static Dictionary<Operations, string> dictionatyOperations = new Dictionary<Operations, string>
        {
            { Operations.Select, "Перегляд" },
            { Operations.Insert, "Додавання" },
            { Operations.Update, "Оновлення" },
            { Operations.Delete, "Видалення" }
        };

        private static Dictionary<Tables, string> dictionatyTables = new Dictionary<Tables, string>
        {
            { Tables.Employee, "Співробітників" },
            { Tables.Children, "Діти" },
            { Tables.GrandChildren, "Онуки" },
            { Tables.Family, "Члени сім'ї" },
            { Tables.Award, "Премії" },
            { Tables.MaterialAid, "Матеріальні допомоги" },
            { Tables.Hobby, "Хобі" },
            { Tables.Event, "Заходи" },
            { Tables.Cultural, "Культурні заклади" },
            { Tables.Activities, "Заходи" },
            { Tables.Privileges, "Пільги" },
            { Tables.SocialActivity, "Громадські посади" },
            { Tables.Position, "Посади" },
            { Tables.Subdivisions, "Підрозділи" },
            { Tables.AddressPublicHouse, "Адреси відомчих та гуртожитків" },
            { Tables.AwardEmployees, "Премії співробітників" },
            { Tables.MaterialAidEmployees, "Матеріальні допомоги співробітників" },
            { Tables.HobbyEmployees, "Хобі співробітників" },
            { Tables.FluorographyEmployees, "Флюрографії співробітників" },
            { Tables.EventEmployees, "Заходи співробітників" },
            { Tables.CulturalEmployees, "Культурні заклади співробітників" },
            { Tables.ActivityEmployees, "Заходи співробітників" },
            { Tables.GiftEmployees, "Подарунки співробітників" },
            { Tables.PrivilegeEmployees, "Пільги співробітників" },
            { Tables.SocialActivityEmployees, "Громадські посади співробітників" },
            { Tables.PositionEmployees, "Посади співробітників" },
            { Tables.PublicHouseEmployees, "Відомчі та гуртожитки співробітників" },
            { Tables.PrivateHouseEmployees, "Приватне житло співробітників" },
            { Tables.ApartmentAccountingEmployees, "Квартирний облік співробітників" },
            { Tables.EventChildrens, "Заходи дітей" },
            { Tables.CulturalChildrens, "Культурні заклади дітей" },
            { Tables.HobbyChildrens, "Хобі дітей" },
            { Tables.ActivityChildrens, "Заходи дітей" },
            { Tables.GiftChildrens, "Подарунки дітей" },
            { Tables.EventGrandChildrens, "Заходи онуків" },
            { Tables.CulturalGrandChildrens, "Культурні заклади онуків" },
            { Tables.HobbyGrandChildrens, "Хобі онуків" },
            { Tables.ActivityGrandChildrens, "Заходи онуків" },
            { Tables.GiftGrandChildrens, "Подарунки онуків" },
            { Tables.EventFamily, "Заходи членів сім'ї" },
            { Tables.CulturalFamily, "Культурні заклади членів сім'ї" },
            { Tables.ActivityFamily, "Заходи членів сім'ї" }
        };
    }
}
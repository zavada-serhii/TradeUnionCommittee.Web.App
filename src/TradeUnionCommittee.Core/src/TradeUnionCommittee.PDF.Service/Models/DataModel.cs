using System.Collections.Generic;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Models
{
    public class DataModel : BaseModel
    {
        public IEnumerable<MaterialIncentivesEmployeeEntity> MaterialAidEmployees { get; set; } = new List<MaterialIncentivesEmployeeEntity>();
        public IEnumerable<MaterialIncentivesEmployeeEntity> AwardEmployees { get; set; } = new List<MaterialIncentivesEmployeeEntity>();
        public IEnumerable<CulturalEmployeeEntity> CulturalEmployees { get; set; } = new List<CulturalEmployeeEntity>();
        public IEnumerable<EventEmployeeEntity> EventEmployees { get; set; } = new List<EventEmployeeEntity>();
        public IEnumerable<GiftEmployeeEntity> GiftEmployees { get; set; } = new List<GiftEmployeeEntity>();
    }
}

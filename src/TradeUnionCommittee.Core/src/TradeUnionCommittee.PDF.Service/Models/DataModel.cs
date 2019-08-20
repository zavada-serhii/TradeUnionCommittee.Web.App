using System.Collections.Generic;
using TradeUnionCommittee.PDF.Service.Entities;

namespace TradeUnionCommittee.PDF.Service.Models
{
    public class DataModel : BaseModel
    {
        public IEnumerable<MaterialIncentivesEmployeeEntity> MaterialAidEmployees { get; set; }
        public IEnumerable<MaterialIncentivesEmployeeEntity> AwardEmployees { get; set; }
        public IEnumerable<CulturalEmployeeEntity> CulturalEmployees { get; set; }
        public IEnumerable<EventEmployeeEntity> EventEmployees { get; set; }
        public IEnumerable<GiftEmployeeEntity> GiftEmployees { get; set; }
    }
}

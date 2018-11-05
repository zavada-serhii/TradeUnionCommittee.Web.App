using System.Collections.Generic;
using TradeUnionCommittee.DAL.Entities;

namespace TradeUnionCommittee.BLL.PDF.Models
{
    public class DataModel : BaseModel
    {
        public IEnumerable<MaterialAidEmployees> MaterialAidEmployees { get; set; }
        public IEnumerable<AwardEmployees> AwardEmployees { get; set; }
        public IEnumerable<CulturalEmployees> CulturalEmployees { get; set; }
        public IEnumerable<EventEmployees> EventEmployees { get; set; }
        public IEnumerable<GiftEmployees> GiftEmployees { get; set; }
    }
}
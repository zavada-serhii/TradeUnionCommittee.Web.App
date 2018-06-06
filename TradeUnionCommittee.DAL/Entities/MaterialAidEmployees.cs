using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class MaterialAidEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdMaterialAid { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssue { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public MaterialAid IdMaterialAidNavigation { get; set; }
    }
}

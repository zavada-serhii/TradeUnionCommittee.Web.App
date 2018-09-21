using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class MaterialAidEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public long IdMaterialAid { get; set; }
        [ConcurrencyCheck]
        public decimal Amount { get; set; }
        [ConcurrencyCheck]
        public DateTime DateIssue { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public MaterialAid IdMaterialAidNavigation { get; set; }
    }
}

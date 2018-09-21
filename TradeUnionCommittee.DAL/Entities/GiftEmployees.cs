using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GiftEmployees
    {
        public long Id { get; set; }
        [ConcurrencyCheck]
        public long IdEmployee { get; set; }
        [ConcurrencyCheck]
        public string NameEvent { get; set; }
        [ConcurrencyCheck]
        public string NameGift { get; set; }
        [ConcurrencyCheck]
        public decimal Price { get; set; }
        [ConcurrencyCheck]
        public decimal Discount { get; set; }
        [ConcurrencyCheck]
        public DateTime DateGift { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}

using System;

namespace TradeUnionCommittee.DAL.Entities
{
    public class GiftEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public string NameEvent { get; set; }
        public string NameGift { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public DateTime DateGift { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
    }
}

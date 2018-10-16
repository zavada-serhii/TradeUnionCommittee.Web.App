using System;

namespace TradeUnionCommittee.BLL.PDF.Models
{
    public class BaseModel
    {
        public string PathToSave { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

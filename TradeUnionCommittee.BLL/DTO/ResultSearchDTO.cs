using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class ResultSearchDTO
    {
        public long IdUser { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string MainSubdivision { get; set; }
        public string SubordinateSubdivision { get; set; }
    }
}

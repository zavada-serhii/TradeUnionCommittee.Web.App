using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class ResultSearchDTO
    {
        public string HashIdUser { get; set; }
        public string FullName { get; set; }
        public string SurnameAndInitials { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobilePhone { get; set; }
        public string CityPhone { get; set; }
        public string MainSubdivision { get; set; }
        public string MainSubdivisionAbbreviation { get; set; }
        public string SubordinateSubdivision { get; set; }
        public string SubordinateSubdivisionAbbreviation { get; set; }
    }
}
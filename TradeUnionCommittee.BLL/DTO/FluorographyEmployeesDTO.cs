using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class FluorographyEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string PlacePassing { get; set; }
        public string Result { get; set; }
        public DateTime DatePassage { get; set; }
        public uint RowVersion { get; set; }
    }
}
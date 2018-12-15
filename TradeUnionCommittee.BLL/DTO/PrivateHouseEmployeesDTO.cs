using System;

namespace TradeUnionCommittee.BLL.DTO
{
    public class PrivateHouseEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumberHouse { get; set; }
        public string NumberApartment { get; set; }
        public uint RowVersion { get; set; }
    }

    public class UniversityHouseEmployeesDTO : PrivateHouseEmployeesDTO
    {
        public DateTime? DateReceiving { get; set; }
    }
}
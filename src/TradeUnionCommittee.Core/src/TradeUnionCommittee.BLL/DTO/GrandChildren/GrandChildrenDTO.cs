using System;

namespace TradeUnionCommittee.BLL.DTO.GrandChildren
{
    public class GrandChildrenDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string FullName { get; internal set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; internal set; }
        public uint RowVersion { get; set; }
    }
}
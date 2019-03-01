using System;

namespace TradeUnionCommittee.BLL.DTO.Children
{
    public class ChildrenDTO
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
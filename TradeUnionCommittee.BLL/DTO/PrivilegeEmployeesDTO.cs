namespace TradeUnionCommittee.BLL.DTO
{
    public class PrivilegeEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdPrivileges { get; set; }
        public string NamePrivileges { get; set; }
        public string Note { get; set; }
        public bool CheckPrivileges { get; set; }
        public uint RowVersion { get; set; }
    }
}
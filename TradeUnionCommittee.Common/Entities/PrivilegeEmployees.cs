namespace TradeUnionCommittee.Common.Entities
{
    public class PrivilegeEmployees
    {
        public long Id { get; set; }
        public long IdEmployee { get; set; }
        public long IdPrivileges { get; set; }
        public string Note { get; set; }
        public bool CheckPrivileges { get; set; }

        public Employee IdEmployeeNavigation { get; set; }
        public Privileges IdPrivilegesNavigation { get; set; }
    }
}

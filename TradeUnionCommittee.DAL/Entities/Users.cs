namespace TradeUnionCommittee.DAL.Entities
{
    public class Users
    {
        public long Id { get; set; }
        public long IdRole { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Roles IdRoleNavigation { get; set; }
    }
}

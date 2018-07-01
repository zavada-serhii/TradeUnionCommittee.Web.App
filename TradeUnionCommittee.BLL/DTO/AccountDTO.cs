namespace TradeUnionCommittee.BLL.DTO
{
    public class AccountDTO
    {
        public long IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long IdRole { get; set; }
        public string Role { get; set; }

        public int KeyUpdate { get; set; }
    }
}
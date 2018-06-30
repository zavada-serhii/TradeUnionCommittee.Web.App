namespace TradeUnionCommittee.BLL.DTO
{
    public class AccountsDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long IdRole { get; set; }
        public string Role { get; set; }
    }
}
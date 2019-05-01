namespace TradeUnionCommittee.BLL.DTO
{
    public class AccountDTO
    {
        public string HashId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Role { get; set; }
    }

    public class CreateAccountDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }

    public class UpdateAccountPasswordDTO
    {
        public string HashId { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
    }
}
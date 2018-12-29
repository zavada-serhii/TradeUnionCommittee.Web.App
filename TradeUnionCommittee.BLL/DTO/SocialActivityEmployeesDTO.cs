namespace TradeUnionCommittee.BLL.DTO
{
    public class SocialActivityEmployeesDTO
    {
        public string HashId { get; set; }
        public string HashIdEmployee { get; set; }
        public string HashIdSocialActivity { get; set; }
        public string NameSocialActivity { get; set; }
        public string Note { get; set; }
        public bool CheckSocialActivity { get; set; }
        public uint RowVersion { get; set; }
    }
}
namespace TradeUnionCommittee.Api.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; } = "Bearer";
    }
}
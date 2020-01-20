﻿namespace TradeUnionCommittee.Api.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long AccessTokenExpires { get; set; }
        public string TokenType { get; } = "Bearer";
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace TradeUnionCommittee.DataAnalysis.Service.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class TestResponseModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("fullName")]
        public string FullName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
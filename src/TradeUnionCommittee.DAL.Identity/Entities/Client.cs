using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.DAL.Identity.Entities
{
    public class Client
    {
        [Key]
        public string Id { get; set; }
        [Required]
        public int RefreshTokenLifeTime { get; set; }
    }
}
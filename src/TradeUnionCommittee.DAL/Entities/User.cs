using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }
        [NotMapped]
        public string UserRole { get; set; }
    }
}
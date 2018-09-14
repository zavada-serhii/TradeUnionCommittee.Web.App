using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TradeUnionCommittee.DAL.Entities
{
    public class User : IdentityUser
    {
        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string OldPassword { get; set; }
        [NotMapped]
        public IList<string> UserRoles { get; set; } = new List<string>();
    }
}
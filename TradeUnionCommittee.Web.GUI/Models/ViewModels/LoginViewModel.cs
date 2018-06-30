using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models.ViewModels
{
    public class LoginViewModel
    {        
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
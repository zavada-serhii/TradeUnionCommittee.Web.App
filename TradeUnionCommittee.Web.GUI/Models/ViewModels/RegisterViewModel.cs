using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models.ViewModels
{
    public class RegisterViewModel
    {
        public long? Id { get; set; }

        public string Email { get; set; }

        public long IdRole { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
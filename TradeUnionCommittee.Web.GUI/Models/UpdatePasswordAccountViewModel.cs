using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class UpdatePasswordAccountViewModel
    {
        public string HashIdUser { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
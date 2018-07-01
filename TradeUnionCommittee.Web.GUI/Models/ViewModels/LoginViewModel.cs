using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models.ViewModels
{
    public class LoginViewModel
    {      
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        public string Password { get; set; }
    }
}
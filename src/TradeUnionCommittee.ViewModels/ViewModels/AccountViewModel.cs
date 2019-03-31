using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class BaseLoginViewModel
    {
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        public string Password { get; set; }
    }

    public class LoginViewModel : BaseLoginViewModel
    {
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Цей email вже використовується!")]
        public string Email { get; set; }

        public string Role { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class UpdateEmailAccountViewModel
    {
        public string HashIdUser { get; set; }

        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Цей email вже використовується!")]
        public string Email { get; set; }
    }

    public class UpdatePasswordAccountViewModel
    {
        public string HashIdUser { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Старий пароль не може бути порожнім")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Новий пароль не може бути порожнім")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class UpdateRoleAccountViewModel
    {
        public string HashIdUser { get; set; }

        public string Role { get; set; }
    }
}
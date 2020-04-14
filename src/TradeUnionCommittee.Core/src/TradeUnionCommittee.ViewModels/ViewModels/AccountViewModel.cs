using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.Attributes;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class BaseLoginViewModel
    {
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [DefaultValue("stewie.griffin@test.com")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        [DefaultValue("P@ssw0rd_admin")]
        public string Password { get; set; }
    }

    public class LoginViewModel : BaseLoginViewModel
    {
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class TokenViewModel : BaseLoginViewModel
    {
        [Required]
        [StringRange(AllowableValues = new[] { "WEB-APPLICATION", "DESKTOP-APPLICATION", "MOBILE-APPLICATION" }, 
            ErrorMessage = "Client Type must be either 'Web-Application', 'Desktop-Application' or 'Mobile-Application'.")]
        [DefaultValue("WEB-APPLICATION")]
        public string ClientType { get; set; }
    }

    public class RefreshTokenViewModel
    {
        [Required]
        [StringRange(AllowableValues = new[] { "WEB-APPLICATION", "DESKTOP-APPLICATION", "MOBILE-APPLICATION" },
            ErrorMessage = "ClientType must be either 'Web-Application', 'Desktop-Application' or 'Mobile-Application'.")]
        public string ClientType { get; set; }
        [Required(ErrorMessage = "Refresh Token не може бути порожнім")]
        [DefaultValue("WEB-APPLICATION")]
        public string RefreshToken { get; set; }
    }

    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Цей email вже використовується!")]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль не може бути порожнім")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Прізвище не може бути порожнім")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Ім'я не може бути порожнім")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }

    public class UpdatePersonalDataAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Прізвище не може бути порожнім")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Ім'я не може бути порожнім")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }

    public class UpdateEmailAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "Цей email вже використовується!")]
        public string Email { get; set; }
    }

    public class UpdatePasswordAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
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
        [Required]
        public string HashId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
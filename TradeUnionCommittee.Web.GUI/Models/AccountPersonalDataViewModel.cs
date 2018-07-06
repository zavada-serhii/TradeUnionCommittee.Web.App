using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class AccountPersonalDataViewModel
    {
        public long? IdUser { get; set; }

        [Required(ErrorMessage = "Email не може бути порожнім")]
        [EmailAddress(ErrorMessage = "Некоректний Email")]
        public string Email { get; set; }

        public long IdRole { get; set; }
    }
}
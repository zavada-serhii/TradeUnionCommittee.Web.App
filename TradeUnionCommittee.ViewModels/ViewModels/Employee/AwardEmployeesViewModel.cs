using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreateAwardEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Назва матеріального заохочення не може бути порожньою!")]
        public string HashIdAward { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Дата видачі не може бути порожньою!")]
        public DateTime DateIssue { get; set; }
    }

    public class UpdateAwardEmployeesViewModel : CreateAwardEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
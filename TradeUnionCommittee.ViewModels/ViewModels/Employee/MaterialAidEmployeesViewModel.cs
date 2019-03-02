using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Employee
{
    public class CreateMaterialAidEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Назва матеріальної допомоги не може бути порожньою!")]
        public string HashIdMaterialAid { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Дата видачі не може бути порожньою!")]
        public DateTime DateIssue { get; set; }
    }

    public class UpdateMaterialAidEmployeesViewModel : CreateMaterialAidEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
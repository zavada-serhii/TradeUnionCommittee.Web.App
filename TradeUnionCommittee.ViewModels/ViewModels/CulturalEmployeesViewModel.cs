using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateCulturalEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Назва закладу не може бути порожньою!")]
        public string HashIdCultural { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Знижка не може бути порожньою!")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Дата візиту не може бути порожньою!")]
        public DateTime DateVisit { get; set; }
    }

    public class UpdateCulturalEmployeesViewModel : CreateCulturalEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
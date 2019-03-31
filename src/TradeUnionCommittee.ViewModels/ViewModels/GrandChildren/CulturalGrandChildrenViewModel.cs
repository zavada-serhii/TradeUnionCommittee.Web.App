using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.GrandChildren
{
    public class CreateCulturalGrandChildrenViewModel
    {
        [Required]
        public string HashIdGrandChildren { get; set; }
        [Required(ErrorMessage = "Назва закладу не може бути порожньою!")]
        public string HashIdCultural { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Знижка не може бути порожньою!")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Дата візиту не може бути порожньою!")]
        public DateTime DateVisit { get; set; }
    }

    public class UpdateCulturalGrandChildrenViewModel : CreateCulturalGrandChildrenViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
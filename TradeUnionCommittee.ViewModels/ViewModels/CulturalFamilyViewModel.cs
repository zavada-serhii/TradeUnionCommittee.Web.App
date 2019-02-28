using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateCulturalFamilyViewModel
    {
        [Required]
        public string HashIdFamily { get; set; }
        [Required(ErrorMessage = "Назва закладу не може бути порожньою!")]
        public string HashIdCultural { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Знижка не може бути порожньою!")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Дата візиту не може бути порожньою!")]
        public DateTime DateVisit { get; set; }
    }

    public class UpdateCulturalFamilyViewModel : CreateCulturalFamilyViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
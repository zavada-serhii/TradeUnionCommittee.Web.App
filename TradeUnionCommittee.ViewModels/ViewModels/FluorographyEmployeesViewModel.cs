using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class CreateFluorographyEmployeesViewModel
    {
        [Required]
        public string HashIdEmployee { get; set; }
        [Required(ErrorMessage = "Місце проходження не може бути порожнім!")]
        public string PlacePassing { get; set; }
        [Required(ErrorMessage = "Результат не може бути порожнім!")]
        public string Result { get; set; }
        [Required(ErrorMessage = "Дата проходження не може бути порожньою!")]
        public DateTime DatePassage { get; set; }
    }

    public class UpdateFluorographyEmployeesViewModel : CreateFluorographyEmployeesViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
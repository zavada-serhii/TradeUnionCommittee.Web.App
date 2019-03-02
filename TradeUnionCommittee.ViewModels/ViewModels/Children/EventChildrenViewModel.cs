using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Children
{
    public class CreateEventChildrenViewModel
    {
        [Required]
        public string HashIdChildren { get; set; }
        [Required(ErrorMessage = "Назва заходу не може бути порожньою!")]
        public string HashIdEvent { get; set; }
        [Required(ErrorMessage = "Розмір не може бути порожнім!")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Знижка не може бути порожньою!")]
        public decimal Discount { get; set; }
        [Required(ErrorMessage = "Дата початку не може бути порожньою!")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Дата закінчення не може бути порожньою!")]
        public DateTime EndDate { get; set; }
    }

    public class UpdateEventChildrenViewModel : CreateEventChildrenViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
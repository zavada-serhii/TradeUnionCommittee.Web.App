using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels.Family
{
    public class CreateActivityFamilyViewModel
    {
        [Required]
        public string HashIdFamily { get; set; }
        [Required(ErrorMessage = "Назва заходу не може бути порожньою!")]
        public string HashIdActivities { get; set; }
        [Required(ErrorMessage = "Дата проведення не може бути порожньою!")]
        public DateTime DateEvent { get; set; }
    }

    public class UpdateActivityFamilyViewModel : CreateActivityFamilyViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.ViewModels.Family;

namespace TradeUnionCommittee.ViewModels.ViewModels.GrandChildren
{
    public class CreateGrandChildrenViewModel : CreateBaseFamilyViewModel
    {
        [Required(ErrorMessage = "Дата народження не може бути порожньою!")]
        public DateTime BirthDate { get; set; }
    }

    public class UpdateGrandChildrenViewModel : UpdateBaseFamilyViewModel
    {
        [Required(ErrorMessage = "Дата народження не може бути порожньою!")]
        public DateTime BirthDate { get; set; }
    }
}
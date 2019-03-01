using System;
using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.ViewModels.Family;

namespace TradeUnionCommittee.ViewModels.ViewModels.Children
{
    public class CreateChildrenViewModel : CreateBaseFamilyViewModel
    {
        [Required(ErrorMessage = "Дата народження не може бути порожньою!")]
        public DateTime BirthDate { get; set; }
    }

    public class UpdateChildrenViewModel : UpdateBaseFamilyViewModel
    {
        [Required(ErrorMessage = "Дата народження не може бути порожньою!")]
        public DateTime BirthDate { get; set; }
    }
}

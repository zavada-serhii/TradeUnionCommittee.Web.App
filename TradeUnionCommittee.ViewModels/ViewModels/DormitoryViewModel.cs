using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class DormitoryViewModel : DepartmentalViewModel
    {
        [Required(ErrorMessage = "Номер гуртожитку не може бути порожнім!")]
        public string NumberDormitory { get; set; }
    }
}
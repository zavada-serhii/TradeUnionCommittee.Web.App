using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public abstract class BasePublicHouseViewModel
    {
        [Required(ErrorMessage = "Місто не може бути порожнім!")]
        public virtual string City { get; set; }
        [Required(ErrorMessage = "Вулиця не може бути порожньою!")]
        public virtual string Street { get; set; }
        [Required(ErrorMessage = "Номер дому не може бути порожнім!")]
        public virtual string NumberHouse { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateDepartmentalViewModel : BasePublicHouseViewModel
    {
        public override string City { get; set; }
        public override string Street { get; set; }
        public override string NumberHouse { get; set; }
    }

    public class UpdateDepartmentalViewModel : BasePublicHouseViewModel
    {
        [Required]
        public string HashId { get; set; }
        public override string City { get; set; }
        public override string Street { get; set; }
        public override string NumberHouse { get; set; }
        [Required]
        public uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateDormitoryViewModel : BasePublicHouseViewModel
    {
        public override string City { get; set; }
        public override string Street { get; set; }
        public override string NumberHouse { get; set; }
        [Required(ErrorMessage = "Номер гуртожитку не може бути порожнім!")]
        public string NumberDormitory { get; set; }
    }

    public class UpdateDormitoryViewModel : UpdateDepartmentalViewModel
    {
        [Required(ErrorMessage = "Номер гуртожитку не може бути порожнім!")]
        public string NumberDormitory { get; set; }
    }
}

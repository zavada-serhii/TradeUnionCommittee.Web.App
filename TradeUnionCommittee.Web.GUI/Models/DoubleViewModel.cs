using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public abstract class BaseDirectoryViewModel
    {
        public virtual string HashId { get; set; }

        public virtual string Name { get; set; }

        public virtual uint RowVersion { get; set; }
    }

    public class PositionViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class SocialActivityViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "SocialActivity", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class PrivilegesViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Privileges", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class AwardViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Award", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class MaterialAidViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "MaterialAid", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class HobbyViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Hobby", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class TravelViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Travel", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class WellnessViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Wellness", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class TourViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Tour", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class ActivitiesViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Activities", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }

    public class CulturalViewModel : BaseDirectoryViewModel
    {
        public override string HashId { get; set; }

        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        [Remote("CheckName", "Cultural", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }

        public override uint RowVersion { get; set; }
    }
}
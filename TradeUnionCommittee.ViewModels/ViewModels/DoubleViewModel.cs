using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public abstract class BaseDirectoryViewModel
    {
        [Required(ErrorMessage = "Назва не може бути порожньою!")]
        public virtual string Name { get; set; }
    }

    public abstract class DirectoryViewModel : BaseDirectoryViewModel
    {
        [Required]
        public virtual string HashId { get; set; }
        [Required]
        public virtual uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreatePositionViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdatePositionViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Position", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateSocialActivityViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "SocialActivity", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateSocialActivityViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "SocialActivity", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreatePrivilegesViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Privileges", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdatePrivilegesViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Privileges", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateAwardViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Award", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateAwardViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Award", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateMaterialAidViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "MaterialAid", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateMaterialAidViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "MaterialAid", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateHobbyViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Hobby", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateHobbyViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Hobby", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateTravelViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Travel", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateTravelViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Travel", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateWellnessViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Wellness", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateWellnessViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Wellness", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateTourViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Tour", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateTourViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Tour", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateActivitiesViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Activities", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateActivitiesViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Activities", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateCulturalViewModel : BaseDirectoryViewModel
    {
        [Remote("CheckName", "Cultural", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
    }

    public class UpdateCulturalViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        [Remote("CheckName", "Cultural", ErrorMessage = "Ця назва вже використовується!")]
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }
}
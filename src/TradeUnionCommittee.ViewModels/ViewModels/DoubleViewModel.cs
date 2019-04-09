using System.ComponentModel.DataAnnotations;

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
        public override string Name { get; set; }
    }

    public class UpdatePositionViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateSocialActivityViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateSocialActivityViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreatePrivilegesViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdatePrivilegesViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateAwardViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateAwardViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateMaterialAidViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateMaterialAidViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateHobbyViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateHobbyViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateTravelViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateTravelViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateWellnessViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateWellnessViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateTourViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateTourViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateActivitiesViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateActivitiesViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------

    public class CreateCulturalViewModel : BaseDirectoryViewModel
    {
        public override string Name { get; set; }
    }

    public class UpdateCulturalViewModel : DirectoryViewModel
    {
        public override string HashId { get; set; }
        public override string Name { get; set; }
        public override uint RowVersion { get; set; }
    }
}
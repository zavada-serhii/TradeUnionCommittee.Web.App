using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.Web.GUI.Models
{
    public class RestructuringViewModel
    {
        [Required]
        public long IdMainSubdivision { get; set; }
        [Required]
        public long IdSubordinateSubdivision { get; set; }
    }
}
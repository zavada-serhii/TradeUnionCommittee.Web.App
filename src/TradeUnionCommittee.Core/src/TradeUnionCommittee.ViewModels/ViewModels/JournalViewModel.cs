using System;
using System.ComponentModel.DataAnnotations;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class JournalViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
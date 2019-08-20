using System;
using System.ComponentModel.DataAnnotations;
using TradeUnionCommittee.ViewModels.Enums;

namespace TradeUnionCommittee.ViewModels.ViewModels
{
    public class PdfReportViewModel
    {
        [Required(ErrorMessage = "Дата з не може бути пустою")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Дата по не може бути пустою")]
        public DateTime EndDate { get; set; }
        [Required]
        public string HashEmployeeId { get; set; }
        [Required]
        [EnumDataType(typeof(TypeReport))]
        public TypeReport Type { get; set; }
    }
}
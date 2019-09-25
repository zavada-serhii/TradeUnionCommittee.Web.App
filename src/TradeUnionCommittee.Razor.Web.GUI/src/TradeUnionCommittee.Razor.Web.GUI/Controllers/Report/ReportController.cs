using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.Razor.Web.GUI.Extensions;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Razor.Web.GUI.Controllers.Report
{
    public class ReportController : Controller
    {
        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ReportController(IPdfService pdfService, IMapper mapper, IHttpContextAccessor accessor)
        {
            _pdfService = pdfService;
            _mapper = mapper;
            _accessor = accessor;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Index(string id)
        {
            return View(new PdfReportViewModel {HashIdEmployee = id});
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(PdfReportViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<ReportPdfDTO>(viewModel);
                dto.EmailUser = User.GetEmail();
                dto.IpUser = _accessor.GetIp();
                var result = await _pdfService.CreateReport(dto);
                if (result.IsValid)
                {
                    return File(result.Result.Data, "application/pdf", $"{result.Result.FileName}.pdf");
                }
                return BadRequest();
            }
            return View("Index", viewModel);
        }
    }
}
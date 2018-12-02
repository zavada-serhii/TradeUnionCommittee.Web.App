using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Report
{
    public class ReportController : Controller
    {
        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;

        public ReportController(IHostingEnvironment appEnvironment, IPdfService pdfService, IMapper mapper)
        {
            _pdfService = pdfService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Index(string id)
        {
            return View(new PdfReportViewModel {HashEmployeeId = id});
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(PdfReportViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _pdfService.CreateReport(_mapper.Map<ReportPdfDTO>(viewModel));
                if (result.IsValid)
                {
                    return File(result.Result, "application/pdf", $"{Guid.NewGuid()}.pdf");
                }
                return BadRequest();
            }
            return View("Index", viewModel);
        }
    }
}
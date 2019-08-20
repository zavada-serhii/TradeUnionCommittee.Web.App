using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.PDF;
using TradeUnionCommittee.Mvc.Web.GUI.Extensions;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Report
{
    public class ReportController : Controller
    {
        private readonly IPdfService _pdfService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public ReportController(IHostingEnvironment appEnvironment, IPdfService pdfService, IMapper mapper, IHttpContextAccessor accessor)
        {
            _pdfService = pdfService;
            _mapper = mapper;
            _accessor = accessor;
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
                var dto = _mapper.Map<ReportPdfDTO>(viewModel);
                dto.EmailUser = User.GetEmail();
                dto.IpUser = _accessor.GetIp();
                var result = await _pdfService.CreateReport(dto);
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
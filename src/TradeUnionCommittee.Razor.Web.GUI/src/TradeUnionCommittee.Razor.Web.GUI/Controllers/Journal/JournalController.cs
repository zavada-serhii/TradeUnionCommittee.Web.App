using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Contracts.SystemAudit;
using TradeUnionCommittee.Razor.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.Razor.Web.GUI.Extensions;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Razor.Web.GUI.Controllers.Journal
{
    public class JournalController : Controller
    {
        private readonly IDirectories _directories;
        private readonly ISystemAuditService _systemAuditService;

        public JournalController(IDirectories directories, ISystemAuditService systemAuditService)
        {
            _directories = directories;
            _systemAuditService = systemAuditService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Emails = await _directories.GetEmails();
            return View();
        }

        [HttpPost, ActionName("Filter")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Filter(JournalViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _systemAuditService.FilterAsync(vm.Email, vm.StartDate, vm.EndDate);
                if (result.IsValid)
                {
                    return Json(result.Result);
                }
                return BadRequest(result.ErrorsList);
            }
            return BadRequest(ModelState.GetErrors());
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Journal
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

        public async Task<IActionResult> Index()
        {
            ViewBag.Emails = await _directories.GetEmails();
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Filter([Required] string email, [Required] DateTime startDate, [Required] DateTime endDate)
        {
            var result = await _systemAuditService.FilterAsync(email, startDate, endDate);
            if (result.IsValid)
            {
                ViewBag.Emails = await _directories.GetEmails();
                return View("Index", result.Result);
            }
            return BadRequest();
        }
    }
}
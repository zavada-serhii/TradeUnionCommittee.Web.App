using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Directory()
        {
            return View();
        }

        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TradeUnionCommittee.GUI.Models;

namespace TradeUnionCommittee.GUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Directory()
        {
            return View();
        }
    }
}

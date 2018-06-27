using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.GUI.Models.ViewModels;

namespace TradeUnionCommittee.GUI.Controllers
{
    public class AddEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(new AddEmployeeViewModel());
        }
    }
}
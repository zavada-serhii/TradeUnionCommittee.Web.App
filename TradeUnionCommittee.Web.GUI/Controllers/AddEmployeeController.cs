using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;

namespace TradeUnionCommittee.Web.GUI.Controllers
{
    public class AddEmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View(new AddEmployeeViewModel());
        }
    }
}
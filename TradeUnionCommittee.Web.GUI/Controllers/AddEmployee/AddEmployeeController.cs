using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;

namespace TradeUnionCommittee.Web.GUI.Controllers.AddEmployee
{
    public class AddEmployeeController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public IActionResult Index()
        {
            return View(new AddEmployeeViewModel());
        }
    }
}
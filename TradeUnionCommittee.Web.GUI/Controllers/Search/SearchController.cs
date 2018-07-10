using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TradeUnionCommittee.Web.GUI.Controllers.Search
{
    public class SearchController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult ResultSearch()
        {
            return View();
        }
    }
}
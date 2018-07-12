using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;

namespace TradeUnionCommittee.Web.GUI.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly ISearchService _services;
        private readonly IOops _oops;

        public SearchController(ISearchService services, IOops oops)
        {
            _services = services;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> ResultSearch()
        {
            var result = await _services.ListAddedEmployeesTemp();
            return View(result);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Search;

namespace TradeUnionCommittee.Web.GUI.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Index()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SearchFullName([Bind("fullname")] string fullname)
        {
            var result = await _searchService.SearchFullName(fullname);
            return View("ResultSearch",result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _searchService.Dispose();
            base.Dispose(disposing);
        }
    }
}
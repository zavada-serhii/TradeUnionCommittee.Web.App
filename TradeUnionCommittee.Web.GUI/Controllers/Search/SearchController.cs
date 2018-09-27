using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;

namespace TradeUnionCommittee.Web.GUI.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly ISearchService _searchService;
        private readonly IDropDownList _dropDownList;

        public SearchController(ISearchService searchService, IDropDownList dropDownList)
        {
            _searchService = searchService;
            _dropDownList = dropDownList;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            await FillingDropDownLists();
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

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SearchGender([Bind("gender,subdivisiongender")] string gender, string subdivisiongender)
        {
            var result = await _searchService.SearchGender(gender, subdivisiongender);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SearchPosition([Bind("position,subdivisionposition")] string position, string subdivisionposition)
        {
            var result = await _searchService.SearchPosition(position, subdivisionposition);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SearchPrivilege([Bind("privilege,subdivisionprivilege")] string privilege, string subdivisionprivilege)
        {
            var result = await _searchService.SearchPrivilege(privilege, subdivisionprivilege);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownLists()
        {
            ViewBag.MainSubdivision = await _dropDownList.GetMainSubdivision();
            ViewBag.Position = await _dropDownList.GetPosition();
            ViewBag.Privilegies = await _dropDownList.GetPrivilegies();
            ViewBag.Hobby = await _dropDownList.GetHobby();
            ViewBag.Dormitory = await _dropDownList.GetDormitory();
            ViewBag.Departmental = await _dropDownList.GetDepartmental();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _searchService.Dispose();
            base.Dispose(disposing);
        }
    }
}
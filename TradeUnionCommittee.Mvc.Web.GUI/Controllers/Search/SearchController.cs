using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Search;
using TradeUnionCommittee.Mvc.Web.GUI.Configuration.DropDownLists;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Search
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchFullName([Bind("fullname")] string fullname)
        {
            var result = await _searchService.SearchFullName(fullname);
            return View("ResultSearch",result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchGender([Bind("gender,subdivisiongender")] string gender, string subdivisiongender)
        {
            var result = await _searchService.SearchGender(gender, subdivisiongender);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPosition([Bind("position,subdivisionposition")] string position, string subdivisionposition)
        {
            var result = await _searchService.SearchPosition(position, subdivisionposition);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchPrivilege([Bind("privilege,subdivisionprivilege")] string privilege, string subdivisionprivilege)
        {
            var result = await _searchService.SearchPrivilege(privilege, subdivisionprivilege);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchAccommodation([Bind("typeAccommodation,idDormitory,idDepartmental")] string typeAccommodation, string idDormitory, string idDepartmental)
        {
            var result = await _searchService.SearchAccommodation(TemporaryConverterAccommodation(typeAccommodation), idDormitory, idDepartmental);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchBirthDate([Bind("typeBirthDate,startDateBirth,endDateBirth")] int typeBirthDate, DateTime startDateBirth, DateTime endDateBirth)
        {
            var result = await _searchService.SearchBirthDate((CoverageType)typeBirthDate, startDateBirth, endDateBirth);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchHobby([Bind("typeHobby,idHobby")] int typeHobby, string idHobby)
        {
            var result = await _searchService.SearchHobby((CoverageType)typeHobby, idHobby);
            return View("ResultSearch", result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchMobilePhone([Bind("mobilePhone")] string mobilePhone)
        {
            var result = await _searchService.SearchEmployee(EmployeeType.MobilePhone, mobilePhone);

            if (result.IsValid)
            {
                return RedirectToAction("Index", "Employee", new { id = result.Result });
            }
            return NotFound();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchCityPhone([Bind("cityPhone")] string cityPhone)
        {
            var result = await _searchService.SearchEmployee(EmployeeType.CityPhone, cityPhone);

            if (result.IsValid)
            {
                return RedirectToAction("Index", "Employee", new { id = result.Result });
            }
            return NotFound();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchIdentificationСode([Bind("identificationCode")] string identificationCode)
        {
            var result = await _searchService.SearchEmployee(EmployeeType.IdentificationСode, identificationCode);

            if (result.IsValid)
            {
                return RedirectToAction("Index", "Employee", new { id = result.Result });
            }
            return NotFound();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------
        
        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SearchMechnikovCard([Bind("mechnikovCard")] string mechnikovCard)
        {
            var result = await _searchService.SearchEmployee(EmployeeType.MechnikovCard, mechnikovCard);

            if (result.IsValid)
            {
                return RedirectToAction("Index", "Employee", new { id = result.Result });
            }
            return NotFound();
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

            ViewBag.Accommodation = new List<ArrayList>
            {
                new ArrayList { "dormitory", 0, "Гуртожиток" },
                new ArrayList { "departmental", 1, "Вiдомче житло" },
                new ArrayList { "from-university", 2, "Житло надане унiверситетом" }
            };

            ViewBag.TypeBirthDate = new List<ArrayList>
            {
                new ArrayList { "employeeBirthDate", 0, "Член профспілки" },
                new ArrayList { "childrenBirthDate", 1, "Дiти" },
                new ArrayList { "grandChildrenBirthDate", 2, "Онуки" }
            };

            ViewBag.TypeHobby = new List<ArrayList>
            {
                new ArrayList { "employeeHobby", 0, "Член профспілки" },
                new ArrayList { "childrenHobby", 1, "Дiти" },
                new ArrayList { "grandChildrenHobby", 2, "Онуки" }
            };
        }

        private AccommodationType TemporaryConverterAccommodation(string value)
        {
            switch (value)
            {
                case "dormitory":
                    return AccommodationType.Dormitory;
                case "departmental":
                    return AccommodationType.Departmental;
                case "from-university":
                    return AccommodationType.FromUniversity;
                default:
                    return 0;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _searchService.Dispose();
            base.Dispose(disposing);
        }
    }
}
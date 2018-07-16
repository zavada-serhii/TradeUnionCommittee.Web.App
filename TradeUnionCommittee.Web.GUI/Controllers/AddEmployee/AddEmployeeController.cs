using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.AddEmployee
{
    public class AddEmployeeController : Controller
    {
        private readonly IEmployeeService _services;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;

        public AddEmployeeController(IEmployeeService services, IDropDownList dropDownList, IOops oops)
        {
            _services = services;
            _dropDownList = dropDownList;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> Index()
        {
            await FillingDropDownLists();
            return View(new AddEmployeeViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmployee(AddEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AddEmployeeViewModel, AddEmployeeDTO>().
                ForMember("IdSubdivision", opt => opt.MapFrom(c => vm.SubordinateSubdivision ?? vm.MainSubdivision)).
                ForMember("CityPhone", opt => opt.MapFrom(c => vm.CityPhoneAdditional ?? vm.CityPhone))).CreateMapper();

                var result = await _services.AddEmployeeAsync(mapper.Map<AddEmployeeViewModel, AddEmployeeDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("AddEmployee", "Index", result.ErrorsList);
            }
            await FillingDropDownLists();
            return View("Index", vm);
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetSubordinateSubdivision(int id)
        {
            var subordinateSubdivision = await _dropDownList.GetSubordinateSubdivisions(id);
            return Json(subordinateSubdivision);
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> CheckIdentificationСode(string identificationСode)
        {
            var result = await _services.CheckIdentificationСode(identificationСode);
            return Json(result.IsValid);
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> CheckMechnikovCard(string mechnikovCard)
        {
            var result = await _services.CheckMechnikovCard(mechnikovCard);
            return Json(result.IsValid);
        }

        private async Task FillingDropDownLists()
        {
            ViewBag.LevelEducation = await _dropDownList.GetLevelEducation();
            ViewBag.Study = await _dropDownList.GetStudy();
            ViewBag.MainSubdivision = await _dropDownList.GetMainSubdivision();
            ViewBag.Position = await _dropDownList.GetPosition();
            ViewBag.Dormitory = await _dropDownList.GetDormitory();
            ViewBag.Departmental = await _dropDownList.GetDepartmental();
            ViewBag.ScientificTitle = await _dropDownList.GetScientificTitle();
            ViewBag.AcademicDegree = await _dropDownList.GetAcademicDegree();
            ViewBag.SocialActivity = await _dropDownList.GetSocialActivity();
            ViewBag.Privilegies = await _dropDownList.GetPrivilegies();
        }

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
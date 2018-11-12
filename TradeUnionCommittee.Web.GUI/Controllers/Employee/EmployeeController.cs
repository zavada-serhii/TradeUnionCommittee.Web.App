using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _employeeService = employeeService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index(string id)
        {
            var result = await _employeeService.GetMainInfoEmployeeAsync(id);
            return View(result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> Create()
        {
            await FillingDropDownLists();
            return View(new CreateEmployeeViewModel());
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeService.AddEmployeeAsync(_mapper.Map<CreateEmployeeDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Create")
                    : _oops.OutPutError("Employee", "Create", result.ErrorsList);
            }
            await FillingDropDownLists();
            return View("Create", vm);
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetSubordinateSubdivision(string id)
        {
            return Json(await _dropDownList.GetSubordinateSubdivisions(id));
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> CheckIdentificationСode(string identificationСode)
        {
            return Json(!await _employeeService.CheckIdentificationСode(identificationСode));
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> CheckMechnikovCard(string mechnikovCard)
        {
            return Json(!await _employeeService.CheckMechnikovCard(mechnikovCard));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return NotFound();
            var result = await _employeeService.GetMainInfoEmployeeAsync(id);
            await FillingStudyDropDownLists();
            await FillingScientificDropDownLists();
            return result.IsValid
                ? View(_mapper.Map<UpdateEmployeeViewModel>(result.Result))
                : _oops.OutPutError("Employee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(UpdateEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdEmployee == null) return NotFound();
                var result = await _employeeService.UpdateMainInfoEmployeeAsync(_mapper.Map<GeneralInfoEmployeeDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index", "Employee", new { id = vm.HashIdEmployee })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            await FillingStudyDropDownLists();
            await FillingScientificDropDownLists();
            vm.Sex = ConvertToUkraineGender(vm.Sex);
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var result = await _employeeService.GetMainInfoEmployeeAsync(id);
            return result.IsValid
                ? View(result.Result)
                : _oops.OutPutError("Employee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null) return NotFound();
            var result = await _employeeService.DeleteAsync(id);
            return result.IsValid
                ? RedirectToAction("Directory", "Home")
                : _oops.OutPutError("Home", "Directory", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownLists()
        {
            await FillingStudyDropDownLists();
            ViewBag.MainSubdivision = await _dropDownList.GetMainSubdivision();
            ViewBag.Position = await _dropDownList.GetPosition();
            ViewBag.Dormitory = await _dropDownList.GetDormitory();
            ViewBag.Departmental = await _dropDownList.GetDepartmental();
            await FillingScientificDropDownLists();
            ViewBag.SocialActivity = await _dropDownList.GetSocialActivity();
            ViewBag.Privilegies = await _dropDownList.GetPrivilegies();
        }

        private async Task FillingStudyDropDownLists()
        {
            ViewBag.LevelEducation = await _dropDownList.GetLevelEducation();
            ViewBag.Study = await _dropDownList.GetStudy();
        }

        private async Task FillingScientificDropDownLists()
        {
            ViewBag.AcademicDegree = await _dropDownList.GetAcademicDegree();
            ViewBag.ScientificTitle = await _dropDownList.GetScientificTitle();
        }

        private string ConvertToUkraineGender(string sex)
        {
            switch (sex)
            {
                case "Male":
                    return new string("Чоловіча");
                case "Female":
                    return new string("Жіноча");
                default:
                    return sex;
            }
        }

        protected override void Dispose(bool disposing)
        {
            _employeeService.Dispose();
            base.Dispose(disposing);
        }
    }
}
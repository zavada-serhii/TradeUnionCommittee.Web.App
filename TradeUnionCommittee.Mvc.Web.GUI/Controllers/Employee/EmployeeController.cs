using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Employee
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;

        public EmployeeController(IEmployeeService employeeService, IDropDownList dropDownList, IOops oops, IMapper mapper, ISystemAuditService systemAuditService)
        {
            _employeeService = employeeService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
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
                var model = _mapper.Map<CreateEmployeeDTO>(vm);
                var result = await _employeeService.AddEmployeeAsync(model);
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, new[] { Tables.Employee, Tables.PositionEmployees });

                    if (model.TypeAccommodation == AccommodationType.Dormitory || model.TypeAccommodation == AccommodationType.Departmental)
                    {
                        await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.PublicHouseEmployees);
                    }
                    else
                    {
                        await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.PrivateHouseEmployees);
                    }

                    if (model.SocialActivity)
                    {
                        await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.SocialActivityEmployees);
                    }

                    if (model.Privileges)
                    {
                        await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.PrivilegeEmployees);
                    }

                    return RedirectToAction("Create");
                }
                return _oops.OutPutError("Employee", "Create", result.ErrorsList);
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
            return result.IsValid ? View(_mapper.Map<UpdateEmployeeViewModel>(result.Result)) : _oops.OutPutError("Employee", "Index", result.ErrorsList);
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
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Update, Tables.Employee);
                    return RedirectToAction("Index", "Employee", new {id = vm.HashIdEmployee});
                }
                return _oops.OutPutError("Employee", "Index", result.ErrorsList);
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
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Employee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null) return NotFound();
            var result = await _employeeService.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Delete, Tables.Employee);
                return RedirectToAction("Directory", "Home");
            }
            return _oops.OutPutError("Home", "Index", result.ErrorsList);
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
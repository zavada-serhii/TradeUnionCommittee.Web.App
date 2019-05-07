using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO.Employee;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists.Employee;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.Mvc.Web.GUI.Extensions;
using TradeUnionCommittee.ViewModels.ViewModels.Employee;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists.Employee
{
    public class PrivilegeEmployeesController : Controller
    {
        private readonly IPrivilegeEmployeesService _services;
        private readonly IDirectories _directories;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public PrivilegeEmployeesController(IPrivilegeEmployeesService services, IDirectories directories, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
        {
            _services = services;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
            _directories = directories;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index([Required] string id)
        {
            var result = await _services.GetAsync(id);
            ViewData["HashIdEmployee"] = id;
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Create([Required] string id)
        {
            ViewBag.Privilege = await _directories.GetPrivilegies();
            return View(new CreatePrivilegeEmployeesViewModel { HashIdEmployee = id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePrivilegeEmployeesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<PrivilegeEmployeesDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.PrivilegeEmployees);
                    return RedirectToAction("Index", new { id = vm.HashIdEmployee });
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            ViewBag.Privilege = await _directories.GetPrivilegies();
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                ViewBag.Privilege = await _directories.GetPrivilegies(result.Result.HashIdPrivileges);
                return View(_mapper.Map<UpdatePrivilegeEmployeesViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePrivilegeEmployeesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateAsync(_mapper.Map<PrivilegeEmployeesDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.PrivilegeEmployees);
                    return RedirectToAction("Index", new { id = vm.HashIdEmployee });
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            ViewBag.Privilege = await _directories.GetPrivilegies(vm.HashIdPrivileges);
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Required] string hashId, [Required] string hashIdEmployee)
        {
            var result = await _services.DeleteAsync(hashId);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.PrivilegeEmployees);
                return RedirectToAction("Index", new { id = hashIdEmployee });
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory
{
    public class SubdivisionController : Controller
    {
        private readonly ISubdivisionsService _services;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public SubdivisionController(ISubdivisionsService services, IDropDownList dropDownList, IOops oops, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
        {
            _services = services;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAllAsync();
            return View(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetSubordinateSubdivisions(id);
            var nameMainSubdivision = await _services.GetAsync(id);
            ViewData["IdMainSubdivision"] = id;
            ViewData["NameMainSubdivision"] = nameMainSubdivision.Result.Name;
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CreateMainSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateMainSubdivisionAsync(_mapper.Map<CreateSubdivisionDTO>(vm));

                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Insert, Tables.Subdivisions);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult CreateSubordinate(string id)
        {
            return View(new CreateSubordinateSubdivisionViewModel { HashIdMain = id });
        }

        [HttpPost, ActionName("CreateSubordinate")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubordinateConfirmed(CreateSubordinateSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateSubordinateSubdivisionAsync(_mapper.Map<CreateSubordinateSubdivisionDTO>(vm));

                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Insert, Tables.Subdivisions);
                    return RedirectToAction("Details", new { id = vm.HashIdMain });
                }
                return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateName(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id);
            return result.IsValid ? View(_mapper.Map<UpdateNameSubdivisionViewModel>(result.Result)) : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("UpdateName")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNameConfirmed(UpdateNameSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdMain == null) return NotFound();
                var result = await _services.UpdateNameSubdivisionAsync(_mapper.Map<UpdateSubdivisionNameDTO>(vm));

                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateAbbreviation(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id);
            return result.IsValid ? View(_mapper.Map<UpdateAbbreviationSubdivisionViewModel>(result.Result)) : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("UpdateAbbreviation")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbbreviationConfirmed(UpdateAbbreviationSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdMain == null) return NotFound();
                var result = await _services.UpdateAbbreviationSubdivisionAsync(_mapper.Map<UpdateSubdivisionAbbreviationDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Restructuring(string id)
        {
            if (id == null) return NotFound();
            var subordinateSubdivision = await _services.GetSubordinateSubdivisionsForMvc(id);
            ViewBag.MainSubdivision = await _dropDownList.GetMainSubdivision();
            ViewBag.SubordinateSubdivision = new SelectList(subordinateSubdivision, "Key", "Value");
            return View();
        }

        [HttpPost, ActionName("Restructuring")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestructuringConfirmed(RestructuringViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.RestructuringUnits(_mapper.Map<RestructuringSubdivisionDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id);
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.DeleteAsync(id);

            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Delete, Tables.Subdivisions);
                return RedirectToAction("Index");
            }
            return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckName(string name)
        {
            return Json(!await _services.CheckNameAsync(name));
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckAbbreviation(string abbreviation)
        {
            return Json(!await _services.CheckAbbreviationAsync(abbreviation));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
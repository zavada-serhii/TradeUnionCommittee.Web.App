using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory
{
    public class ActivitiesController : Controller
    {
        private readonly IActivitiesService _services;
        private readonly IOops _oops;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;

        public ActivitiesController(IActivitiesService services, IOops oops, IMapper mapper, ISystemAuditService systemAuditService)
        {
            _services = services;
            _oops = oops;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAllAsync();
            return View(result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateActivitiesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<DirectoryDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.Activities);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Activities", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id);
            return result.IsValid ? View(_mapper.Map<UpdateActivitiesViewModel>(result.Result)) : _oops.OutPutError("Activities", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(UpdateActivitiesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashId == null) return NotFound();
                var result = await _services.UpdateAsync(_mapper.Map<DirectoryDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Update, Tables.Activities);
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Activities", "Index", result.ErrorsList);
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
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Activities", "Index", result.ErrorsList);
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
                await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Delete, Tables.Activities);
                return RedirectToAction("Index");
            }
            return _oops.OutPutError("Activities", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckName(string name)
        {
            return Json(!await _services.CheckNameAsync(name));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
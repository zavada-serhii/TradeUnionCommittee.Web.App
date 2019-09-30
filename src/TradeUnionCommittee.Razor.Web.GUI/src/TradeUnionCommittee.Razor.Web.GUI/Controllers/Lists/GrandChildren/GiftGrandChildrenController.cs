using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO.GrandChildren;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists.GrandChildren;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Razor.Web.GUI.Extensions;
using TradeUnionCommittee.ViewModels.ViewModels.GrandChildren;

namespace TradeUnionCommittee.Razor.Web.GUI.Controllers.Lists.GrandChildren
{
    public class GiftGrandChildrenController : Controller
    {
        private readonly IGiftGrandChildrenService _services;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public GiftGrandChildrenController(IGiftGrandChildrenService services, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
        {
            _services = services;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index([Required] string subid)
        {
            var result = await _services.GetAllAsync(subid);
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
        public IActionResult Create([Required] string subid)
        {
            return View(new CreateGiftGrandChildrenViewModel { HashIdGrandChildren = subid });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGiftGrandChildrenViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<GiftGrandChildrenDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.GiftGrandChildrens);
                    return RedirectToAction("Index", new { id = ControllerContext.RouteData.Values["id"], subid = vm.HashIdGrandChildren });
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update([Required] string subid)
        {
            var result = await _services.GetAsync(subid);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateGiftGrandChildrenViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateGiftGrandChildrenViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateAsync(_mapper.Map<GiftGrandChildrenDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.GiftGrandChildrens);
                    return RedirectToAction("Index", new { id = ControllerContext.RouteData.Values["id"], subid = vm.HashIdGrandChildren });
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.GiftGrandChildrens);
            }
            return Ok(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
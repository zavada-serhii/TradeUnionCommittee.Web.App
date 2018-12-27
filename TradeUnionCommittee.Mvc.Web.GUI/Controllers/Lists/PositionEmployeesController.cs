using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    public class PositionEmployeesController : Controller
    {
        private readonly IPositionEmployeesService _services;
        private readonly IDirectories _directories;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public PositionEmployeesController(IPositionEmployeesService services, IDirectories directories, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
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
            if (result.IsValid)
            {
                await FillingDropDownLists(result.Result.HashIdSubdivision, result.Result.HashIdPosition);
                return View(_mapper.Map<UpdatePositionEmployeesViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdatePositionEmployeesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateAsync(_mapper.Map<PositionEmployeesDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.PositionEmployees);
                    return RedirectToAction("Index", new { id = vm.HashIdEmployee });
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            await FillingDropDownLists(vm.HashIdSubdivision, vm.HashIdPosition);
            return View("Index", vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownLists(string idSubdivisions, string idPosition)
        {
            ViewBag.Subdivisions = await _directories.GetTreeSubdivisions(idSubdivisions);
            ViewBag.Position = await _directories.GetPosition(idPosition);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
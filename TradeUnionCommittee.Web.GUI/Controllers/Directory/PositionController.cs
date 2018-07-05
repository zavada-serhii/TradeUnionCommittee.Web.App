using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Directory
{
    public class PositionController : Controller
    {
        private readonly IPositionService _services;
        private readonly IOops _oops;

        public PositionController(IPositionService services, IOops oops)
        {
            _services = services;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll();
            return View(result);
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
        public async Task<IActionResult> Create([Bind("Name")] DirectoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.Create(new DirectoryDTO { Name = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Position", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.Get(id.Value);
            if (result.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DirectoryDTO, DirectoryViewModel>()).CreateMapper();
                return View(mapper.Map<DirectoryDTO, DirectoryViewModel>(result.Result));
            }
            return _oops.OutPutError("Position", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed([Bind("Id,Name")] DirectoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.Update(new DirectoryDTO { Id = vm.Id.Value, Name = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Position", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.Get(id.Value);
            return result.IsValid 
                ? View(result.Result) 
                : _oops.OutPutError("Position", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.Delete(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Position", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckName(string name)
        {
            var result = await _services.CheckName(name);
            return Json(result.IsValid);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Directory
{
    public class TravelController : Controller
    {
        private readonly ITravelService _services;
        private readonly IOops _oops;

        public TravelController(ITravelService services, IOops oops)
        {
            _services = services;
            _oops = oops;
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
        public async Task<IActionResult> Create([Bind("Name")] TravelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(new DirectoryDTO { Name = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Travel", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id.Value);
            if (result.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DirectoryDTO, TravelViewModel>()).CreateMapper();
                return View(mapper.Map<DirectoryDTO, TravelViewModel>(result.Result));
            }
            return _oops.OutPutError("Travel", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed([Bind("Id,Name")] TravelViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.UpdateAsync(new DirectoryDTO { Id = vm.Id.Value, Name = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Travel", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id.Value);
            return result.IsValid
                ? View(result.Result)
                : _oops.OutPutError("Travel", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.DeleteAsync(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Travel", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckName(string name)
        {
            var result = await _services.CheckNameAsync(name);
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
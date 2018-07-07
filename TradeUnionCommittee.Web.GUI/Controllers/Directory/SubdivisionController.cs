using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Directory
{
    public class SubdivisionController : Controller
    {
        private readonly ISubdivisionsService _services;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;

        public SubdivisionController(ISubdivisionsService services, IDropDownList dropDownList, IOops oops)
        {
            _services = services;
            _dropDownList = dropDownList;
            _oops = oops;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed([Bind("Name")] SubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(new SubdivisionDTO { DeptName = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
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
                return View(new SubdivisionViewModel { Id = result.Result.Id, Name = result.Result.DeptName });
            }
            return _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed([Bind("Id,Name")] SubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.UpdateAsync(new SubdivisionDTO { Id = vm.Id.Value, DeptName = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
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
                ? View(new SubdivisionViewModel {Id = result.Result.Id, Name = result.Result.DeptName})
                : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
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
                : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetSubordinateSubdivisions(id.Value);
            var nameMainSubdivision = await _services.GetAsync(id.Value);
            ViewData["IdMainSubdivision"] = id;
            ViewData["NameMainSubdivision"] = nameMainSubdivision.Result.DeptName;
            return result.IsValid
                ? View(result.Result) 
                : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult CreateSubordinate(long? id)
        {
            return View(new SubdivisionViewModel { Id = id});
        }

        [HttpPost, ActionName("CreateSubordinate")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSubordinateConfirmed([Bind("Id,Name")] SubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(new SubdivisionDTO { IdSubordinate = vm.Id, DeptName = vm.Name });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Restructuring(long? id)
        {
            if (id == null) return NotFound();
            var subordinateSubdivision = await _services.GetSubordinateSubdivisions(id.Value);
            ViewBag.MainSubdivision = await _dropDownList.GetMainSubdivision();
            ViewBag.SubordinateSubdivision = new SelectList(subordinateSubdivision.Result, "Id", "DeptName");
            return View();
        }

        [HttpPost, ActionName("Restructuring")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestructuringConfirmed(RestructuringViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.RestructuringUnits(new SubdivisionDTO
                {
                    Id = vm.IdSubordinateSubdivision,
                    IdSubordinate = vm.IdMainSubdivision
                });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
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
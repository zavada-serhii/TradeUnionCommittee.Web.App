using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
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
        private readonly IMapper _mapper;

        public SubdivisionController(ISubdivisionsService services, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _services = services;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
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
        public async Task<IActionResult> CreateConfirmed(SubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<SubdivisionDTO>(vm));
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
            return result.IsValid 
                ? View(_mapper.Map<UpdateSubdivisionViewModel>(result.Result)) 
                : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(UpdateSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.UpdateAsync(_mapper.Map<SubdivisionDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateAbbreviation(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id.Value);
            return result.IsValid
                ? View(_mapper.Map<UpdateAbbreviationSubdivisionViewModel>(result.Result))
                : _oops.OutPutError("Subdivision", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("UpdateAbbreviation")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAbbreviationConfirmed(UpdateAbbreviationSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.UpdateAbbreviation(_mapper.Map<SubdivisionDTO>(vm));
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
                ? View(_mapper.Map<SubdivisionViewModel>(result.Result))
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
            ViewData["NameMainSubdivision"] = nameMainSubdivision.Result.Name;
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
        public async Task<IActionResult> CreateSubordinateConfirmed(SubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(new SubdivisionDTO { IdSubordinate = vm.Id, Name = vm.Name,Abbreviation = vm.Abbreviation});
                return result.IsValid
                    ? RedirectToAction("Details", new { id = vm.Id})
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
            ViewBag.SubordinateSubdivision = new SelectList(subordinateSubdivision.Result, "Id", "Name");
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

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> CheckAbbreviation(string abbreviation)
        {
            var result = await _services.CheckAbbreviationAsync(abbreviation);
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
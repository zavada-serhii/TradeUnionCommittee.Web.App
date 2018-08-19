using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.InfoEmployee
{
    public class MainInfoEmployeeController : Controller
    {
        private readonly IEmployeeService _services;
        private readonly IEducationService _educationService;
        private readonly IScientificService _scientificService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public MainInfoEmployeeController(IEmployeeService services, IEducationService educationService, IScientificService scientificService, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _services = services;
            _educationService = educationService;
            _scientificService = scientificService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Index(long id)
        {
            var result = await _services.GetMainInfoEmployeeAsync(id);
            return View(result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetMainInfoEmployeeAsync(id.Value);
            return result.IsValid 
                ? View(_mapper.Map<MainInfoEmployeeViewModel>(result.Result)) 
                : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(MainInfoEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdEmployee == null) return NotFound();
                var result = await _services.UpdateMainInfoEmployeeAsync(_mapper.Map<MainInfoEmployeeDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            //if (id == null) return NotFound();
            //var result = await _services.GetAsync(id.Value);
            //return result.IsValid
            //    ? View(result.Result)
            //    : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);

            return await Task.Run(() => View("Update"));
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            //if (id == null) return NotFound();
            //var result = await _services.DeleteAsync(id.Value);
            //return result.IsValid
            //    ? RedirectToAction("Index")
            //    : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);

            return await Task.Run(() => View("Update"));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateEducation(long? id)
        {
            if (id == null) return NotFound();
            var result = await _educationService.GetEducationEmployeeAsync(id.Value);
            if (result.IsValid)
            {
                await FillingDropDownListsEducation();
                return View(_mapper.Map<UpdateEducationViewModel>(result.Result));
            }
            return _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("UpdateEducation")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEducationConfirmed(UpdateEducationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdEmployee == null) return NotFound();
                var result = await _educationService.UpdateEducationEmployeeAsync(_mapper.Map<EducationDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> UpdateQualification(long? id)
        {
            if (id == null) return NotFound();
            var result = await _scientificService.GetScientificEmployeeAsync(id.Value);
            if (result.IsValid)
            {
                await FillingDropDownListsQualification();
                return View(_mapper.Map<UpdateEducationViewModel>(result.Result));
            }
            return _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownListsEducation()
        {
            ViewBag.LevelEducation = await _dropDownList.GetLevelEducation();
            ViewBag.Study = await _dropDownList.GetStudy();
        }

        private async Task FillingDropDownListsQualification()
        {
            ViewBag.AcademicDegree = await _dropDownList.GetAcademicDegree();
            ViewBag.ScientificTitle = await _dropDownList.GetScientificTitle();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
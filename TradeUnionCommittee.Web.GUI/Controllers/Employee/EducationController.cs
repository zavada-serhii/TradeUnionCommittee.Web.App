using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Employee
{
    public class EducationController : Controller
    {
        private readonly IEducationService _educationService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public EducationController(IEducationService educationService, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _educationService = educationService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _educationService.GetEducationEmployeeAsync(id.Value);
            if (result.IsValid)
            {
                await FillingDropDownListsEducation();
                return View(_mapper.Map<EducationViewModel>(result.Result));
            }
            return _oops.OutPutError("Employee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(EducationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdEmployee == null) return NotFound();
                var result = await _educationService.UpdateEducationEmployeeAsync(_mapper.Map<EducationDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index", "Employee", new { id = vm.IdEmployee })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownListsEducation()
        {
            ViewBag.LevelEducation = await _dropDownList.GetLevelEducation();
            ViewBag.Study = await _dropDownList.GetStudy();
        }
    }
}
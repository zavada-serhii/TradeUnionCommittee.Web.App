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
    public class QualificationController : Controller
    {
        private readonly IQualificationService _qualificationService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public QualificationController(IQualificationService qualificationService, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _qualificationService = qualificationService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Create(long id)
        {
            await FillingDropDownListsQualification();
            return View(new QualificationViewModel { IdEmployee = id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QualificationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _qualificationService.CreateQualificationEmployeeAsync(_mapper.Map<QualificationDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index", "Employee", new { id = vm.IdEmployee })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _qualificationService.GetQualificationEmployeeAsync(id.Value);
            if (result.IsValid)
            {
                await FillingDropDownListsQualification();
                return View(_mapper.Map<QualificationViewModel>(result.Result));
            }
            return _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQualification(QualificationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdEmployee == null) return NotFound();
                var result = await _qualificationService.UpdateQualificationEmployeeAsync(_mapper.Map<QualificationDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index", "Employee", new { id = vm.IdEmployee })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var result = await _qualificationService.GetQualificationEmployeeAsync(id.Value);
            return result.IsValid
                ? View(result.Result)
                : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _qualificationService.DeleteQualificationEmployeeAsync(id.Value);
            return result.IsValid
                ? RedirectToAction("Index", "Employee", new { id })
                : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownListsQualification()
        {
            ViewBag.AcademicDegree = await _dropDownList.GetAcademicDegree();
            ViewBag.ScientificTitle = await _dropDownList.GetScientificTitle();
        }
    }
}
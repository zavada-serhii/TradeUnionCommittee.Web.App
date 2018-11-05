using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Web.GUI.Controllers.Oops;
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
        public async Task<IActionResult> Create(string id)
        {
            await FillingDropDownListsQualification();
            return View(new QualificationViewModel { HashEmployeeId = id });
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
                    ? RedirectToAction("Index", "Employee", new { id = vm.HashEmployeeId })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> Update(string id)
        {
            if (id == null) return NotFound();
            var result = await _qualificationService.GetQualificationEmployeeAsync(id);
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
                if (vm.HashId == null) return NotFound();
                var result = await _qualificationService.UpdateQualificationEmployeeAsync(_mapper.Map<QualificationDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index", "Employee", new { id = vm.HashEmployeeId })
                    : _oops.OutPutError("Employee", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var result = await _qualificationService.GetQualificationEmployeeAsync(id);
            return result.IsValid
                ? View(result.Result)
                : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string hashId, string hashEmployeeId)
        {
            if (hashId == null || hashEmployeeId == null) return NotFound();
            var result = await _qualificationService.DeleteQualificationEmployeeAsync(hashId);
            return result.IsValid
                ? RedirectToAction("Index", "Employee", new { id = hashEmployeeId })
                : _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        private async Task FillingDropDownListsQualification()
        {
            ViewBag.AcademicDegree = await _dropDownList.GetAcademicDegree();
            ViewBag.ScientificTitle = await _dropDownList.GetScientificTitle();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _qualificationService.Dispose();
            base.Dispose(disposing);
        }
    }
}
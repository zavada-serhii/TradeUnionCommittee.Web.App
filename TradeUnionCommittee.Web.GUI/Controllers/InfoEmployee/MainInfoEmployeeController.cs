using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.InfoEmployee
{
    public class MainInfoEmployeeController : Controller
    {
        private readonly IEmployeeService _services;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public MainInfoEmployeeController(IEmployeeService services, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _services = services;
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

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
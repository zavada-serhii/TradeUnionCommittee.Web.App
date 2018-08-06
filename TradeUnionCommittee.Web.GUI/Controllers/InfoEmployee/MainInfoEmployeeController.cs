using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Employee;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;

namespace TradeUnionCommittee.Web.GUI.Controllers.InfoEmployee
{
    public class MainInfoEmployeeController : Controller
    {
        private readonly IEmployeeService _services;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;

        public MainInfoEmployeeController(IEmployeeService services, IDropDownList dropDownList, IOops oops)
        {
            _services = services;
            _dropDownList = dropDownList;
            _oops = oops;
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
            //if (id == null) return NotFound();
            //var result = await _services.GetAsync(id.Value);
            //if (result.IsValid)
            //{
            //    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DirectoryDTO, ActivitiesViewModel>()).CreateMapper();
            //    return View(mapper.Map<DirectoryDTO, ActivitiesViewModel>(result.Result));
            //}
            //return _oops.OutPutError("Activities", "Index", result.ErrorsList);

            return await Task.Run(() => View());

        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed()
        {
            //if (ModelState.IsValid)
            //{
            //    if (vm.Id == null) return NotFound();
            //    var result = await _services.UpdateAsync(new DirectoryDTO { Id = vm.Id.Value, Name = vm.Name });
            //    return result.IsValid
            //        ? RedirectToAction("Index")
            //        : _oops.OutPutError("Activities", "Index", result.ErrorsList);
            //}
            //return View(vm);

            return await Task.Run(() => View());
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
            //    : _oops.OutPutError("Activities", "Index", result.ErrorsList);

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
            //    : _oops.OutPutError("Activities", "Index", result.ErrorsList);

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
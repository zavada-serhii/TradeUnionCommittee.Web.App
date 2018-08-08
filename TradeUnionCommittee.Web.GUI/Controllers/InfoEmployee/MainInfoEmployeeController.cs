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
            if (id == null) return NotFound();
            var result = await _services.GetMainInfoEmployeeAsync(id.Value);
            if (result.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<MainInfoEmployeeDTO, MainInfoEmployeeViewModel>()).CreateMapper();
                return View(mapper.Map<MainInfoEmployeeDTO, MainInfoEmployeeViewModel>(result.Result));
            }
            return _oops.OutPutError("MainInfoEmployee", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(MainInfoEmployeeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdEmployee == null) return NotFound();
                var result = await _services.UpdateMainInfoEmployeeAsync(new MainInfoEmployeeDTO
                {
                    IdEmployee = vm.IdEmployee.Value,
                    FirstName = vm.FirstName,
                    SecondName = vm.SecondName,
                    Patronymic = vm.Patronymic,
                    Sex = vm.Sex,
                    BirthDate = vm.BirthDate,
                    IdentificationСode = vm.IdentificationСode,
                    MechnikovCard = vm.MechnikovCard,
                    MobilePhone = vm.MobilePhone,
                    CityPhone = vm.CityPhone,
                    Note = vm.Note,
                    BasicProfession = vm.BasicProfession,
                    StartYearWork = vm.StartYearWork,
                    EndYearWork = vm.EndYearWork,
                    StartDateTradeUnion = vm.StartDateTradeUnion,
                    EndDateTradeUnion = vm.EndDateTradeUnion
                });
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
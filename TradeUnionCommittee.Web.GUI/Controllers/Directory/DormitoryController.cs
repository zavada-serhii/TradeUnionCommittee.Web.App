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
    public class DormitoryController : Controller
    {
        private readonly IDormitoryService _services;
        private readonly IOops _oops;

        public DormitoryController(IDormitoryService services, IOops oops)
        {
            _services = services;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAllAsync();
            return View(result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DormitoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(new DormitoryDTO
                {
                    City = vm.City,
                    Street = vm.Street,
                    NumberHouse = vm.NumberHouse,
                    NumberDormitory = vm.NumberDormitory
                });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Dormitory", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _services.GetAsync(id.Value);
            if (result.IsValid)
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DormitoryDTO, DormitoryViewModel>()).CreateMapper();
                return View(mapper.Map<DormitoryDTO, DormitoryViewModel>(result.Result));
            }
            return _oops.OutPutError("Dormitory", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin,Accountant")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConfirmed(DormitoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id == null) return NotFound();
                var result = await _services.UpdateAsync(new DormitoryDTO
                {
                    Id = vm.Id.Value,
                    City = vm.City,
                    Street = vm.Street,
                    NumberHouse = vm.NumberHouse,
                    NumberDormitory = vm.NumberDormitory
                });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Dormitory", "Index", result.ErrorsList);
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
                : _oops.OutPutError("Dormitory", "Index", result.ErrorsList);
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
                : _oops.OutPutError("Dormitory", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
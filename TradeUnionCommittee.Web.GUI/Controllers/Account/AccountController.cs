using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.DropDownLists;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.Models;

namespace TradeUnionCommittee.Web.GUI.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;

        public AccountController(IAccountService accountService, IDropDownList dropDownList, IOops oops)
        {
            _accountService = accountService;
            _dropDownList = dropDownList;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAllAsync();
            return View(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Role = await _dropDownList.GetRoles();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(CreateAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.CreateAsync(new AccountDTO { Email = vm.Email, Password = vm.Password, IdRole = vm.IdRole });
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                return _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetAsync(id.Value);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetRoles();

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountDTO, UpdatePersonalDataAccountViewModel>()).CreateMapper();
                return View(mapper.Map<AccountDTO, UpdatePersonalDataAccountViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateConfirmed(UpdatePersonalDataAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.UpdateAsync(new AccountDTO
                {
                    IdUser = vm.IdUser.Value,
                    Email = vm.Email,
                    IdRole = vm.IdRole
                });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePassword(long? id)
        {
            if (id == null) return NotFound();
            var model = new UpdatePasswordAccountViewModel { IdUser = id};
            return View(model);
        }

        [HttpPost, ActionName("UpdatePassword")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasswordConfirmed(UpdatePasswordAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.UpdateAsync(new AccountDTO
                {
                    IdUser = vm.IdUser.Value,
                    Password = vm.Password
                });
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetAsync(id.Value);
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.DeleteAsync(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            var result = await _accountService.CheckEmail(email);
            return Json(result.IsValid);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}
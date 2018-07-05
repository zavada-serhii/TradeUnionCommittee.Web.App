using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Web.GUI.AdditionalSettings.Oops;
using TradeUnionCommittee.Web.GUI.DropDownLists;
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
            var result = await _accountService.GetAll();
            return View(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Role = await _dropDownList.GetDormitory();
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateConfirmed(AccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Create(new AccountDTO { Email = vm.Email, Password = vm.Password, IdRole = vm.IdRole });
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
            var result = await _accountService.Get(id.Value);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetDormitory();

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountDTO, AccountViewModel>()).CreateMapper();
                return View(mapper.Map<AccountDTO, AccountViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateConfirmed(AccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.Update(new AccountDTO
                {
                    KeyUpdate = 1,
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
            var model = new AccountViewModel {IdUser = id};
            return View(model);
        }

        [HttpPost, ActionName("UpdatePassword")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasswordConfirmed(AccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.Update(new AccountDTO
                {
                    KeyUpdate = 2,
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
            var result = await _accountService.Get(id.Value);
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.Delete(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}
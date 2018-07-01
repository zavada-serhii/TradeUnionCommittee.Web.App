using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;
using TradeUnionCommittee.Web.GUI.Oops;

namespace TradeUnionCommittee.Web.GUI.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IOops _oops;

        public AccountController(IAccountService accountService, IOops oops)
        {
            _accountService = accountService;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAll();
            return result.IsValid ? View(result) : _oops.OutPutError("Home", "Directory", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var roles =  await _accountService.GetRoles();
            ViewBag.Role = new SelectList(roles.Result, "Id", "Name");
            return View();
        }

        [HttpPost, ActionName("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateConfirmed(AccountViewModel vm)
        {
            var result = await _accountService.Create(new AccountDTO { Email = vm.Email, Password = vm.Password, IdRole = vm.IdRole });
            if (result.IsValid)
            {
                return RedirectToAction("Index");
            }
            return _oops.OutPutError("Account", "Index", new List<Error>());
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
                var roles = await _accountService.GetRoles();
                ViewBag.Role = new SelectList(roles.Result, "Id", "Name");

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountDTO, AccountViewModel>()).CreateMapper();
                return View(mapper.Map<AccountDTO, AccountViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateConfirmed(AccountViewModel vm)
        {
            if (vm.IdUser == null) return NotFound();
            var result = await _accountService.Update(new AccountDTO {IdUser = vm.IdUser.Value, Email = vm.Email, IdRole = vm.IdRole});
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
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
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.Delete(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [Authorize(Roles = "Admin")]
        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}
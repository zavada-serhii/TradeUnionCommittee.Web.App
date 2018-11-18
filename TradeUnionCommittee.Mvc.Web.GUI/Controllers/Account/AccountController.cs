using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Mvc.Web.GUI.Configuration.DropDownLists;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Oops;
using TradeUnionCommittee.Mvc.Web.GUI.Models;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDropDownList _dropDownList;
        private readonly IOops _oops;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IDropDownList dropDownList, IOops oops, IMapper mapper)
        {
            _accountService = accountService;
            _dropDownList = dropDownList;
            _oops = oops;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model.Email, model.Password, AuthorizationType.Cookie);
                if (result.IsValid)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Directory", "Home");
                }
                ViewBag.IncorectLoginPassword = result.ErrorsList.FirstOrDefault();
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SignOut()
        {
            var result = await _accountService.LogOff();
            if (result.IsValid)
            {
                return RedirectToAction("Login", "Account");
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAllUsersAsync();
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
                var result = await _accountService.CreateUserAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail(string id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetUserAsync(id);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetRoles();
                return View(_mapper.Map<UpdateEmailAccountViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("UpdateEmail")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmailConfirmed(UpdateEmailAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdUser == null) return NotFound();
                var result = await _accountService.UpdateUserEmailAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePassword(string id)
        {
            if (id == null) return NotFound();
            var model = new UpdatePasswordAccountViewModel { HashIdUser = id };
            return View(model);
        }

        [HttpPost, ActionName("UpdatePassword")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasswordConfirmed(UpdatePasswordAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdUser == null) return NotFound();
                var result = await _accountService.UpdateUserPasswordAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(string id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetUserAsync(id);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetRoles();
                return View(_mapper.Map<UpdateRoleAccountViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("UpdateRole")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleConfirmed(UpdateRoleAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashIdUser == null) return NotFound();
                var result = await _accountService.UpdateUserRoleAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetUserAsync(id);
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.DeleteUserAsync(id);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail(string email)
        {
            return Json(!await _accountService.CheckEmailAsync(email));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _accountService.Login(model.Email, model.Password);
                if (role.IsValid)
                {
                    await Authenticate(model.Email, role.Result); // аутентификация
                    return RedirectToAction("Directory", "Home");
                }
                ViewBag.IncorectLoginPassword = role.ErrorsList.FirstOrDefault();
                return View();
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
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
        public IActionResult UpdatePassword(string id)
        {
            if (id == null) return NotFound();
            var model = new UpdatePasswordAccountViewModel { HashIdUser = id};
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

        private async Task Authenticate(string email, string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            // создаем объект ClaimsIdentity
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _accountService.Dispose();
            base.Dispose(disposing);
        }
    }
}
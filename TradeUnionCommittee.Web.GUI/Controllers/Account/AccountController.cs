using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Security.Claims;
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
        private readonly ILoginService _loginService;
        private readonly IOops _oops;

        public AccountController(IAccountService accountService, ILoginService loginService, IOops oops)
        {
            _accountService = accountService;
            _loginService = loginService;
            _oops = oops;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
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
                var result = await _loginService.Login(model.Email, model.Password);

                if (result.IsValid)
                {
                    await Authenticate(result.Result.Email, result.Result.Role); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Index()
        {
            var result = await _accountService.GetAll();
            return result.IsValid ? View(result) : _oops.OutPutError("Home", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles =  await _accountService.GetRoles();
            ViewBag.Role = new SelectList(roles.Result, "Id", "Name");
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreateConfirmed(RegisterViewModel vm)
        {
            var result = await _accountService.Create(new AccountsDTO { Email = vm.Email, Password = vm.Password, IdRole = vm.IdRole });
            if (result.IsValid)
            {
                return RedirectToAction("Index");
            }
            return _oops.OutPutError("Account", "Index", new List<Error>());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Update(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.Get(id.Value);
            if (result.IsValid)
            {
                var roles = await _accountService.GetRoles();
                ViewBag.Role = new SelectList(roles.Result, "Id", "Name");

                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AccountsDTO, RegisterViewModel>()).CreateMapper();
                return View(mapper.Map<AccountsDTO, RegisterViewModel>(result.Result));
            }
            return _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdateConfirmed(RegisterViewModel vm)
        {
            if (vm.Id == null) return NotFound();
            var result = await _accountService.Update(new AccountsDTO { Id = vm.Id.Value, Email = vm.Email, IdRole = vm.IdRole});
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }


        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.Get(id.Value);
            return result.IsValid ? View(result.Result) : _oops.OutPutError("Account", "Index", result.ErrorsList);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.Delete(id.Value);
            return result.IsValid
                ? RedirectToAction("Index")
                : _oops.OutPutError("Account", "Index", result.ErrorsList);
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
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
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
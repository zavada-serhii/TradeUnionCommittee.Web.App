using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Interfaces.Login;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;
using TradeUnionCommittee.Web.GUI.Oops;

namespace TradeUnionCommittee.Web.GUI.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController( ILoginService loginService, IOops oops)
        {
            _loginService = loginService;
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
                var role = await _loginService.Login(model.Email, model.Password);
                if (role.IsValid)
                {
                    await Authenticate(model.Email, role.Result); // аутентификация
                    return RedirectToAction("Directory", "Home");
                }
                ViewBag.IncorectLoginPassword = "Неправильний логін або пароль";
                return View();
            }
            return View(model);
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

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _loginService.Dispose();
            base.Dispose(disposing);
        }
    }
}
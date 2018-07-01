using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using TradeUnionCommittee.Web.GUI.Models.ViewModels;

namespace TradeUnionCommittee.Web.GUI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public IActionResult Directory()
        {
            return View();
        }
       
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        [Authorize(Roles = "Admin,Accountant,Deputy")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Account;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IDirectories _dropDownList;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IDirectories dropDownList, IMapper mapper)
        {
            _accountService = accountService;
            _dropDownList = dropDownList;
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
                if (await _accountService.SignIn(model.Email, model.Password, model.RememberMe))
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    return RedirectToAction("Directory", "Home");
                }
                ViewBag.IncorectLoginPassword = "Неправильний логін або пароль!";
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy")]
        public async Task<IActionResult> SignOut()
        {
            await _accountService.SignOut();
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
            var result = await _accountService.GetAllAccountsAsync();
            if (result.IsValid)
            {
                return View(result.Result);
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
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
                var result = await _accountService.CreateAsync(_mapper.Map<CreateAccountDTO>(vm));
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsList"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePersonalData([Required] string id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdatePersonalDataAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }

        [HttpPost, ActionName("UpdatePersonalData")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePersonalDataConfirmed(UpdatePersonalDataAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdatePersonalDataAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail([Required] string id)
        {
            var result = await _accountService.GetAccountAsync(id);
            if (result.IsValid)
            {
                return View(_mapper.Map<UpdateEmailAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }


        [HttpPost, ActionName("UpdateEmail")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmailConfirmed(UpdateEmailAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateEmailAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdatePassword([Required] string id)
        {
            return View(new UpdatePasswordAccountViewModel { HashId = id });
        }

        [HttpPost, ActionName("UpdatePassword")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePasswordConfirmed(UpdatePasswordAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdatePasswordAsync(_mapper.Map<UpdateAccountPasswordDTO>(vm));
                if(result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole([Required] string id)
        {
            var result = await _accountService.GetAccountRoleAsync(id);
            if (result.IsValid)
            {
                ViewBag.Role = await _dropDownList.GetRoles();
                return View(_mapper.Map<UpdateRoleAccountViewModel>(result.Result));
            }
            TempData["ErrorsList"] = result.ErrorsList;
            return View();
        }


        [HttpPost, ActionName("UpdateRole")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRoleConfirmed(UpdateRoleAccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateRoleAsync(_mapper.Map<AccountDTO>(vm));
                if (result.IsValid)
                {
                    return RedirectToAction("Index");
                }
                TempData["ErrorsListConfirmed"] = result.ErrorsList;
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            return Ok(await _accountService.DeleteAsync(id));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckEmail([Required] string email)
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
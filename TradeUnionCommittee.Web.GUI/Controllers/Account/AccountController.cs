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
                var result = await _accountService.CreateAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmail(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetAsync(id.Value);
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
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.UpdateAsync(_mapper.Map<AccountDTO>(vm));
                return result.IsValid
                    ? RedirectToAction("Index")
                    : _oops.OutPutError("Account", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(long? id)
        {
            if (id == null) return NotFound();
            var result = await _accountService.GetAsync(id.Value);
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
                if (vm.IdUser == null) return NotFound();
                var result = await _accountService.UpdateAsync(_mapper.Map<AccountDTO>(vm));
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
                var result = await _accountService.UpdateAsync(_mapper.Map<AccountDTO>(vm));
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
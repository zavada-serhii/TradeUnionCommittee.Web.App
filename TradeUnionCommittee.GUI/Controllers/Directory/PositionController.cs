using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.GUI.Models.ViewModels;

namespace TradeUnionCommittee.GUI.Controllers.Directory
{
    public class PositionController : Controller
    {
        private readonly IPositionService _services;

        public PositionController(IPositionService services)
        {
            _services = services;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Index()
        {
            var result = await _services.GetAll();
            return View(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] DirectoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.Create(new DirectoryDTO {Name = vm.Name});
                return result.IsValid ? RedirectToAction("Index") : new ErrorController().OutPutError("Position", "Index", result.ErrorsList);
            }
            return View(vm);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            var result = await _services.Get(id);
            if (!result.IsValid) return NotFound();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<DirectoryDTO, DirectoryViewModel>()).CreateMapper();
            return View(mapper.Map<DirectoryDTO, DirectoryViewModel>(result.Result));
        }

        [HttpPost]
        public async Task<IActionResult> Update([Bind("Id,Name")] DirectoryViewModel vm)
        {
            if (ModelState.IsValid && vm.Id != null)
            {
                var result = await _services.Update(new DirectoryDTO {Id = vm.Id.Value, Name = vm.Name});
                return result.IsValid ? RedirectToAction("Index") : new ErrorController().OutPutError("Position", "Index", result.ErrorsList);
            }
            return BadRequest();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _services.Get(id.Value);

            if (!result.IsValid) return NotFound();

            return View(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await _services.Delete(id);
            return result.IsValid ? RedirectToAction("Index") : new ErrorController().OutPutError("Position", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> CheckName(string name)
        {
            var result = await _services.CheckName(name);
            return Json(result.Result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
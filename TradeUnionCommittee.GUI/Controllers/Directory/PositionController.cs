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

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _services.Get(id);

            if (!result.IsValid) return NotFound();

            return View(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] DirectoryViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Id != null)
                {
                    var result = await _services.Update(new DirectoryDTO {Id = vm.Id.Value, Name = vm.Name});
                    return result.IsValid ? RedirectToAction("Index") : new ErrorController().OutPutError("Position", "Index", result.ErrorsList);
                }
                return BadRequest();
            }

            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _services.Get(id);

            if (!result.IsValid) return NotFound();

            return View(result.Result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var result = await _services.Delete(id);
            return result.IsValid ? RedirectToAction("Index") : new ErrorController().OutPutError("Position", "Index", result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
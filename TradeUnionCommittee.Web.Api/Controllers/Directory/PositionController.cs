using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Web.Api.Controllers.Directory
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMapper _mapper;

        public PositionController(IPositionService services, ISystemAuditService systemAuditService, IMapper mapper)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet("{id}", Name = "Get")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Get(string id)
        {
            if (id != null)
            {
                return Ok(await _services.GetAsync(id));
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromBody] PositionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<DirectoryDTO>(vm));
               
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.Position);
                    return Ok();
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState.ValidationState);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update([FromBody] PositionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.HashId == null) return NotFound();
                var result = await _services.UpdateAsync(_mapper.Map<DirectoryDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Update, Tables.Position);
                    return Ok();
                }
                return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Delete, Tables.Position);
                return Ok();
            }
            return BadRequest(result);
        }

        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CheckName(string name)
        {
            return Ok(!await _services.CheckNameAsync(name));
        }
    }
}
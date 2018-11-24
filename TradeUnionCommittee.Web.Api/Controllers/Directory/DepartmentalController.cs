using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Web.Api.Controllers.Directory
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentalController : ControllerBase
    {
        private readonly IDepartmentalService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMapper _mapper;

        public DepartmentalController(IDepartmentalService services, ISystemAuditService systemAuditService, IMapper mapper)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet]
        [Route("Get/{id}")]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Get([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentalViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateAsync(_mapper.Map<DepartmentalDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Insert, Tables.AddressPublicHouse);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("Update")]
        [Authorize(Roles = "Admin,Accountant", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentalViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateAsync(_mapper.Map<DepartmentalDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Update, Tables.AddressPublicHouse);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.Identity.Name, Operations.Delete, Tables.AddressPublicHouse);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
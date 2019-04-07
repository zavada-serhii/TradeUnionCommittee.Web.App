using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Common.ActualResults;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Web.Api.Controllers.Directory
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionController : ControllerBase
    {
        private readonly ISubdivisionsService _services;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public SubdivisionController(ISubdivisionsService services, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
        {
            _services = services;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Route("GetAllMainSubdivision")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetAllMainSubdivision()
        {
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet]
        [Route("Get/{id}")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Get([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("GetSubordinateSubdivisions/{id}")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetSubordinateSubdivisions([Required] string id)
        {
            var result = await _services.GetSubordinateSubdivisions(id);
            if (result.IsValid && result.Result.Any())
            {
                return Ok(result);
            }
            return BadRequest(new ActualResult("Not found subordinate subdivisions!"));
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("CreateMainSubdivision")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateMainSubdivision([FromBody] CreateMainSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateMainSubdivisionAsync(_mapper.Map<CreateSubdivisionDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Insert, Tables.Subdivisions);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateSubordinateSubdivision")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> CreateSubordinateSubdivision([FromBody] CreateSubordinateSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.CreateSubordinateSubdivisionAsync(_mapper.Map<CreateSubordinateSubdivisionDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Insert, Tables.Subdivisions);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPut]
        [Route("UpdateName")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateNameSubdivisionAsync(_mapper.Map<UpdateSubdivisionNameDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("UpdateAbbreviation")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> UpdateAbbreviation([FromBody] UpdateAbbreviationSubdivisionViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.UpdateAbbreviationSubdivisionAsync(_mapper.Map<UpdateSubdivisionAbbreviationDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("RestructuringUnits")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> RestructuringUnits([FromBody] RestructuringViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _services.RestructuringUnits(_mapper.Map<RestructuringSubdivisionDTO>(vm));
                if (result.IsValid)
                {
                    await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Update, Tables.Subdivisions);
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.Identity.Name, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Operations.Delete, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
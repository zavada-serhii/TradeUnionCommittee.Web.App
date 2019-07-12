using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.ActualResults;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.ViewModels.ViewModels;
using TradeUnionCommittee.Web.Api.Attributes;
using TradeUnionCommittee.Web.Api.Extensions;

namespace TradeUnionCommittee.Web.Api.Controllers.Directory
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllMainSubdivision()
        {
            return Ok(await _services.GetAllAsync());
        }

        [HttpGet]
        [Route("Get/{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateMainSubdivision([FromBody] CreateMainSubdivisionViewModel vm)
        {
            var result = await _services.CreateMainSubdivisionAsync(_mapper.Map<CreateSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost]
        [Route("CreateSubordinateSubdivision")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateSubordinateSubdivision([FromBody] CreateSubordinateSubdivisionViewModel vm)
        {
            var result = await _services.CreateSubordinateSubdivisionAsync(_mapper.Map<CreateSubordinateSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPut]
        [Route("UpdateName")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameSubdivisionViewModel vm)
        {
            var result = await _services.UpdateNameSubdivisionAsync(_mapper.Map<UpdateSubdivisionNameDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("UpdateAbbreviation")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAbbreviation([FromBody] UpdateAbbreviationSubdivisionViewModel vm)
        {
            var result = await _services.UpdateAbbreviationSubdivisionAsync(_mapper.Map<UpdateSubdivisionAbbreviationDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("RestructuringUnits")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RestructuringUnits([FromBody] RestructuringViewModel vm)
        {
            var result = await _services.RestructuringUnits(_mapper.Map<RestructuringSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.Subdivisions);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TradeUnionCommittee.Api.Attributes;
using TradeUnionCommittee.Api.Extensions;
using TradeUnionCommittee.BLL.DTO;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Directory;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.ViewModels.ViewModels;

namespace TradeUnionCommittee.Api.Controllers.Directory
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
        private readonly ILogger<SubdivisionController> _logger;

        public SubdivisionController(ISubdivisionsService services, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor, ILogger<SubdivisionController> logger)
        {
            _services = services;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
            _logger = logger;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Route("GetAllMainSubdivision")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SubdivisionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAllMainSubdivision()
        {
            var result = await _services.GetAllAsync();
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpGet]
        [Route("Get/{id}", Name = "GetSubdivision")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(SubdivisionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get([Required] string id)
        {
            var result = await _services.GetAsync(id);
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return NotFound(result.ErrorsList);
        }

        [HttpGet]
        [Route("GetSubordinateSubdivisions/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<SubdivisionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetSubordinateSubdivisions([Required] string id)
        {
            var result = await _services.GetSubordinateSubdivisions(id);
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return NotFound(result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost]
        [Route("CreateMainSubdivision")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CreateMainSubdivisionViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateMainSubdivision([FromBody] CreateMainSubdivisionViewModel vm)
        {
            var result = await _services.CreateMainSubdivisionAsync(_mapper.Map<CreateSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Subdivisions);
                return CreatedAtRoute("GetSubdivision", new { id = result.Result }, vm);
            }
            return UnprocessableEntity(result.ErrorsList);
        }

        [HttpPost]
        [Route("CreateSubordinateSubdivision")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CreateSubordinateSubdivisionViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status422UnprocessableEntity)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateSubordinateSubdivision([FromBody] CreateSubordinateSubdivisionViewModel vm)
        {
            var result = await _services.CreateSubordinateSubdivisionAsync(_mapper.Map<CreateSubordinateSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Subdivisions);
                return CreatedAtRoute("GetSubdivision", new { id = result.Result }, vm);
            }
            return UnprocessableEntity(result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpPut]
        [Route("UpdateName")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateName([FromBody] UpdateNameSubdivisionViewModel vm)
        {
            var result = await _services.UpdateNameSubdivisionAsync(_mapper.Map<UpdateSubdivisionNameDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return NoContent();
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpPut]
        [Route("UpdateAbbreviation")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateAbbreviation([FromBody] UpdateAbbreviationSubdivisionViewModel vm)
        {
            var result = await _services.UpdateAbbreviationSubdivisionAsync(_mapper.Map<UpdateSubdivisionAbbreviationDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return NoContent();
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpPut]
        [Route("RestructuringUnits")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RestructuringUnits([FromBody] RestructuringViewModel vm)
        {
            var result = await _services.RestructuringUnits(_mapper.Map<RestructuringSubdivisionDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Subdivisions);
                return NoContent();
            }
            return BadRequest(result.ErrorsList);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.Subdivisions);
                return NoContent();
            }
            return NotFound(result.ErrorsList);
        }
    }
}
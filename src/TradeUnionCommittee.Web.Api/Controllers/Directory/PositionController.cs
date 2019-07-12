using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly ILogger<PositionController> _logger;

        public PositionController(IPositionService services, ISystemAuditService systemAuditService, IMapper mapper, IHttpContextAccessor accessor, ILogger<PositionController> logger)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _mapper = mapper;
            _accessor = accessor;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAll")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<string>), 400)]
        [ProducesResponseType(typeof(IEnumerable<DirectoryDTO>), 200)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
        {
            //_logger.LogInformation($"Test Position API Controller: {JsonConvert.SerializeObject(await _services.GetAllAsync())}");
            var result = await _services.GetAllAsync();
            if (result.IsValid)
            {
                return Ok(result.Result);
            }
            return BadRequest(result.ErrorsList);
        }

        [HttpGet]
        [Route("Get/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(DirectoryDTO), 200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 404)]
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

        [HttpPost]
        [Route("Create")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 422)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreatePositionViewModel vm)
        {
            var result = await _services.CreateAsync(_mapper.Map<DirectoryDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Position);
                return Ok();
                //return Created();
            }
            return UnprocessableEntity(result.ErrorsList);
        }

        [HttpPut]
        [Route("Update")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 422)]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdatePositionViewModel vm)
        {
            var result = await _services.UpdateAsync(_mapper.Map<DirectoryDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Position);
                return Ok();
            }
            return UnprocessableEntity(result.ErrorsList);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<string>), 404)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.Position);
                return Ok();
            }
            return NotFound(result.ErrorsList);
        }
    }
}
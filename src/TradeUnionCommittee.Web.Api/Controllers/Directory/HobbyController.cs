using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class HobbyController : ControllerBase
    {
        private readonly IHobbyService _services;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public HobbyController(IHobbyService services, ISystemAuditService systemAuditService, IMapper mapper, IHttpContextAccessor accessor)
        {
            _services = services;
            _systemAuditService = systemAuditService;
            _mapper = mapper;
            _accessor = accessor;
        }

        [HttpGet]
        [Route("GetAll")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetAll()
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

        [HttpPost]
        [Route("Create")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] CreateHobbyViewModel vm)
        {
            var result = await _services.CreateAsync(_mapper.Map<DirectoryDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Insert, Tables.Hobby);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("Update")]
        [ModelValidation]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin,Accountant,Deputy", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromBody] UpdateHobbyViewModel vm)
        {
            var result = await _services.UpdateAsync(_mapper.Map<DirectoryDTO>(vm));
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Update, Tables.Hobby);
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [MapToApiVersion("1.0")]
        [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var result = await _services.DeleteAsync(id);
            if (result.IsValid)
            {
                await _systemAuditService.AuditAsync(User.GetEmail(), _accessor.GetIp(), Operations.Delete, Tables.Hobby);
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
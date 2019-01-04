using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.BLL.Interfaces.Lists;
using TradeUnionCommittee.BLL.Interfaces.SystemAudit;
using TradeUnionCommittee.Mvc.Web.GUI.Controllers.Directory;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Lists
{
    //HobbyGrandChildrensDTO
    //CreateHobbyGrandChildrensViewModel
    //UpdateHobbyGrandChildrensViewModel

    public class HobbyGrandChildrensController : Controller
    {
        private readonly IHobbyGrandChildrensService _services;
        private readonly IDirectories _directories;
        private readonly IMapper _mapper;
        private readonly ISystemAuditService _systemAuditService;
        private readonly IHttpContextAccessor _accessor;

        public HobbyGrandChildrensController(IHobbyGrandChildrensService services, IDirectories directories, IMapper mapper, ISystemAuditService systemAuditService, IHttpContextAccessor accessor)
        {
            _services = services;
            _mapper = mapper;
            _systemAuditService = systemAuditService;
            _accessor = accessor;
            _directories = directories;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Index()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Create()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Update()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        public IActionResult Delete()
        {
            return View();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
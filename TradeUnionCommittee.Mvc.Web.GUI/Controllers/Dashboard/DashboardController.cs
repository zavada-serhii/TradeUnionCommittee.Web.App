using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;

namespace TradeUnionCommittee.Mvc.Web.GUI.Controllers.Dashboard
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _services;
        private readonly IMapper _mapper;

        public DashboardController(IDashboardService services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
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
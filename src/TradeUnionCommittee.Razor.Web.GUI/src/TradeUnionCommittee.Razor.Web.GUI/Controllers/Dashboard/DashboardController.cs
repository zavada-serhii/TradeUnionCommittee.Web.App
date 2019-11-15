using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TradeUnionCommittee.BLL.Enums;
using TradeUnionCommittee.BLL.Interfaces.Dashboard;

namespace TradeUnionCommittee.Razor.Web.GUI.Controllers.Dashboard
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

        #region Task 1

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CorrelationAnalysisBetweenTeacherAgeAndTypeOfEvent()
        {
            return Json(await _services.CorrelationAnalysisBetweenTeacherAgeAndTypeOfEvent());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CheckingSignificanceAgeTeacherAndTypeOfEvent()
        {
            return Json(await _services.CheckingSignificanceAgeTeacherAndTypeOfEvent());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClusterAnalysisAgeTeacherAndTypeOfEvent([Required] int id)
        {
            return Json(await _services.ClusterAnalysisAgeTeacherAndTypeOfEvent((TypeEvents)id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EmployeeAgeGroup()
        {
            return Json(await _services.GetEmployeeAgeGroup());
        }

        #endregion

        #region Task 2

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MultiCorrelationBetweenTypeOfEventAndDependents([Required] int id)
        {
            return Json(await _services.MultiCorrelationBetweenTypeOfEventAndDependents((TypeEvents)id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ClusterAnalysisSignHavingChildrenAndTypeOfEvent([Required] int id)
        {
            return Json(await _services.ClusterAnalysisSignHavingChildrenAndTypeOfEvent((TypeEvents)id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PercentageRatioHavingDependents([Required] int id)
        {
            return Json(await _services.GetPercentageRatioHavingDependents());
        }

        #endregion

        #region Test

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult PieData()
        {
            return Json(_services.PieData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BarData()
        {
            return Json(_services.BarData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AreaData()
        {
            return Json(_services.AreaData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult RadarData()
        {
            return Json(_services.RadarData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult LineData()
        {
            return Json(_services.LineData_Test());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult BubbleData()
        {
            return Json(_services.BubbleData_Test());
        }

        #endregion

        protected override void Dispose(bool disposing)
        {
            _services.Dispose();
            base.Dispose(disposing);
        }
    }
}
using HMIS.MEAs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonDTOs.ResponseDTO;
using HMIS.MEAs.Domain.Models.DTO.Dashboard;

namespace HMIS.MEAs.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly DashboardService _dash;
        #region Constructor
        public DashboardController(DashboardService dash)
        {

            _dash = dash;

        }


        #endregion

        #region CUD Operations
   
        #endregion

        #region READ Operations


        [HttpGet]
        [Route("GetMEACoverageCompliance")]
        public async Task<IActionResult> GetMEACoverageCompliance()
        {
            var data = await _dash.GetMEACoverageCompliance();
            return Ok(new ResponseSuccess { data = data });
        }


        [HttpGet]
        [Route("Get_HF_LastVisit")]
        public async Task<IActionResult> Get_HF_LastVisit(string hfmiscode)
        {
            var data = await _dash.Get_HF_LastVisit(hfmiscode);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpPost]
        [Route("GetTodaysVisitDetails")]
        public async Task<IActionResult> GetTodaysVisitDetails(int hft)
        {
            var data = await _dash.GetTodaysVisitDetails(hft);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetDashboardCounts")]
        public async Task<IActionResult> GetDashboardCounts()
        {
            var data = await _dash.GetDashboardCounts();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetCurrentMonthVisits")]
        public async Task<IActionResult> GetCurrentMonthVisits()
        {
            var data = await _dash.GetCurrentMonthVisits();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpPost]
        [Route("GetMEAsCoverageAndCompliance")]
        public async Task<IActionResult> GetMEAsCoverageAndCompliance(SearchDashboardDTO search)
        {
            var data = await _dash.GetMEAsCoverageAndCompliance(search);
            return Ok(new ResponseSuccess { data = data });
        }
        #endregion

    }
}

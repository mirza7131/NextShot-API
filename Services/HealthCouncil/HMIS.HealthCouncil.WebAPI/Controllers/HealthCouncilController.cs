using CommonDTOs.ResponseDTO;
using HMIS.HealthCouncil.Domain.Models.Dto.Budget;
using HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil;
using HMIS.HealthCouncil.Domain.Models.DTO.Budget;
using HMIS.HealthCouncil.Domain.Models.DTO.FilterDto;
using HMIS.HealthCouncil.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HealthCouncil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HealthCouncilController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly HealthCouncilService _HealthCouncilService;
        #endregion


        #region Constructor
        public HealthCouncilController(HealthCouncilService HealthCouncilService)
        {
            _HealthCouncilService = HealthCouncilService;
        }
        #endregion



        #region CUD Operations
        [HttpPost]
        [Route("CreateOrEditCommitteeFormulation")]
        public async Task<IActionResult> CreateOrEditCommitteeFormulation(CommitteeFormulationDto input)
        {
            await _HealthCouncilService.CreateOrEditCommitteeFormulation(input);
            return Ok(new ResponseSuccess());
        }


        [HttpPost]
        [Route("CreateOrEditMeetingCall")]
        public async Task<IActionResult> CreateOrEditMeetingCall(MeetingCallDto input)
        {
            await _HealthCouncilService.CreateOrEditMeetingCall(input);
            return Ok(new ResponseSuccess());
        }

        [HttpPost]
        [Route("CreateOrEditMeetingDetails")]
        public async Task<IActionResult> CreateOrEditMeetingDetails(MeetingDetailDto input)
        {
            await _HealthCouncilService.CreateOrEditMeetingDetails(input);
            return Ok(new ResponseSuccess());
        }


        [HttpPost]
        [Route("CreateOrEditVendor")]
        public async Task<IActionResult> CreateOrEditVendor(VendorDto vendorDto)
        {
            await _HealthCouncilService.CreateOrEditVendor(vendorDto);
            return Ok(new ResponseSuccess());
        }



        [HttpPost]
        [Route("CreateOrEditExpenses")]
        public async Task<IActionResult> CreateOrEditExpenses(ExpenseDto expenseDto)
        {
            await _HealthCouncilService.CreateOrEditExpenses(expenseDto);
            return Ok(new ResponseSuccess());
        }
        #endregion



        #region Read Operations

        [HttpGet]
        [Route("GetCommitteeFormulationList")]
        public async Task<IActionResult> GetCommitteeFormulationList([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetCommitteeFormulationList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingCallList")]
        public async Task<IActionResult> GetMeetingCallList([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetMeetingCallList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetMeetingDetailsList")]
        public async Task<IActionResult> GetMeetingDetailsList([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetMeetingDetailsList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingCalls")]
        public async Task<IActionResult> GetMeetingCalls(int HealthFacilityId)
        {
            var obj = await _HealthCouncilService.GetMeetingCalls(HealthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetVendors")]
        public async Task<IActionResult> GetVendors([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetVendors(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingDetailsById")]
        public async Task<IActionResult> GetMeetingDetailsById(Guid MeetingDetailId)
        {
            var obj = await _HealthCouncilService.GetMeetingDetailsById(MeetingDetailId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetHealthFacilityAccountBalance")]
        public async Task<IActionResult> GetHealthFacilityAccountBalance(int healthFacilityId)
        {
            var obj = await _HealthCouncilService.GetHealthFacilityAccountBalance(healthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetAccountHeadsList")]
        public async Task<IActionResult> GetAccountHeadsList()
        {
            var obj = await _HealthCouncilService.GetAccountHeadsList();
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetAccountMeetingsList")]
        public async Task<IActionResult> GetAccountMeetingsList()
        {
            var obj = await _HealthCouncilService.GetAccountMeetingsList();
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingDisscussedCatgories")]
        public async Task<IActionResult> GetMeetingDisscussedCatgories(Guid MeetingId)
        {
            var obj = await _HealthCouncilService.GetMeetingDisscussedCatgories(MeetingId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingExpendetures")]
        public async Task<IActionResult> GetMeetingExpendetures(Guid MeetingDisscussedCategoryId)
        {
            var obj = await _HealthCouncilService.GetMeetingExpendetures(MeetingDisscussedCategoryId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetAllVendorsHealthFacilityWise")]
        public async Task<IActionResult> GetAllVendorsHealthFacilityWise([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetAllVendorsHealthFacilityWise(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetMeetingExpenses")]
        public async Task<IActionResult> GetMeetingExpenses([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _HealthCouncilService.GetMeetingExpenses(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion


    }
}

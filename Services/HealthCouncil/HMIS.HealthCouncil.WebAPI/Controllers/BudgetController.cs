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
    public class Budget : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly BudgetService _BudgetService;
        #endregion

        #region Constructor

        public Budget(
            BudgetService BudgetService
        )
        {
            _BudgetService = BudgetService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditBudgetDto input)
        {
            var obj = await _BudgetService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("BulkCreateOrEdit")]
        public async Task<IActionResult> BulkCreateOrEdit(CreateOrEditBudgetDto input)
        {
            var obj = await _BudgetService.BulkCreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }



        #region DG Office
        [HttpPost]
        [Route("UpdateReleaseBudget")]
        public async Task<IActionResult> UpdateReleaseBudget(ReleasebudgetDto input)
        {
            var obj = await _BudgetService.UpdateReleaseBudget(input);
            return Ok(new ResponseSave { data = obj });
        }



        [HttpPost]
        [Route("UpdateCheuqeStatus")]
        public async Task<IActionResult> UpdateCheuqeStatus(ChequeStatusDto chequeStatusDto)
        {
            await _BudgetService.UpdateCheuqeStatus(chequeStatusDto);
            return Ok(new ResponseSuccess());
        }

        [HttpPost]
        [Route("SaveChequeImage")]
        public async Task<IActionResult> SaveChequeImage(ChequeImageDto chequeImageDto)
        {
            var obj = await _BudgetService.SaveChequeImage(chequeImageDto);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpPost]
        [Route("RemoveImage")]
        public async Task<IActionResult> RemoveImage(ChequeImageDto chequeImageDto)
        {
            var obj = await _BudgetService.RemoveImage(chequeImageDto);
            return Ok(new ResponseSuccess { data = obj });
        }


        #endregion



        #region HealthCouncil

        [HttpPost]
        [Route("SaveBankStatement")]
        public async Task<IActionResult> SaveBankStatement(BankStatementDto bankStatementDto)
        {
            await _BudgetService.SaveBankStatement(bankStatementDto);
            return Ok(new ResponseSuccess());
        }


        [HttpPost]
        [Route("CreateOrEditContigentStaff")]
        public async Task<IActionResult> CreateOrEditContigentStaff(ContigmentStaffDto contigmentStaffDto)
        {
            await _BudgetService.CreateOrEditContigentStaff(contigmentStaffDto);
            return Ok(new ResponseSuccess());
        }


        #endregion

        #endregion


        #region Read Operations

        [HttpGet]
        [Route("GetFacilityTypeCountAndBalance")]
        public async Task<IActionResult> GetFacilityTypeCountAndBalance([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetFacilityTypeCountAndBalance(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetIssuedCheques")]
        public async Task<IActionResult> GetIssuedCheques([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetIssuedCheques(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllocateBudget")]
        public async Task<IActionResult> GetAllocateBudget([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetAllocateBudget(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetReleaseBudget")]
        public async Task<IActionResult> GetReleaseBudget([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetReleaseBudget(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetHealthFacilitiesDetail")]
        public async Task<IActionResult> GetHealthFacilityDetail([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetHealthFacilitiesDetail(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetChequeImage")]
        public async Task<IActionResult> GetChequeImage(Guid BudgetId)
        {
            var obj = await _BudgetService.GetChequeImage(BudgetId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetContigentStaffList")]
        public async Task<IActionResult> GetContigentStaffList([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetContigentStaffList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetChequeIssue")]
        public async Task<IActionResult> GetChequeIssue([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetChequeIssue(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetBankStatements")]
        public async Task<IActionResult> GetBankStatements([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BudgetService.GetBankStatements(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion
    }
}


using CommonDTOs.ResponseDTO;
using CommonDTOs.TBScreeningDTO;
using HMIS.HealthCouncil.Domain.Models.DbModels;
using HMIS.HealthCouncil.Domain.Models.Dto.BankDetails;
using HMIS.HealthCouncil.Domain.Models.DTO.FilterDto;
using HMIS.HealthCouncil.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.HealthCouncil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankDetailsController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly BankDetailService _BankDetailService;
        #endregion

        #region Constructor
        public BankDetailsController(BankDetailService bankDetailService)
        {
            _BankDetailService = bankDetailService;
        }
        #endregion



        #region CUD Operations
        [HttpPost]
        [Route("CreateOrEditBankDetails")]
        public async Task<IActionResult> CreateOrEditBankDetails(BankDetailsDto input)
        {
            await _BankDetailService.CreateOrEditBankDetails(input);
            return Ok(new ResponseSuccess());
        }
        #endregion


        #region Read Operations
        [HttpGet]
        [Route("GetBankListByHealthFacility")]
        public async Task<IActionResult> GetBankListByHealthFacility([FromQuery] UserLevelFilterDto filter)
        {
            var obj = await _BankDetailService.GetBankListByHealthFacility(filter);
            return Ok(new ResponseSuccess { data = obj });
        }
        #endregion
    }
}

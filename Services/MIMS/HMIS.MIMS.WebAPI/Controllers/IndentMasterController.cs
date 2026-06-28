using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;
using HMIS.MIMS.Domain.Models.DTO.PaginationDto;
using HMIS.MIMS.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.MIMS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IndentMasterController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly IndentMasterService<IndentMaster> _IndentMasterService;
        private readonly MIMSService _mimsService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;

        #region Constructor
    
        public IndentMasterController(TokenService tokenService, IndentMasterService<IndentMaster> IndentMasterService, MIMSService mimsService,
            IConfiguration config)
        {
            _tokenService = tokenService;
            _IndentMasterService = IndentMasterService;
            _mimsService = mimsService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
           
        
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditIndentMasterDto input)
        {
            var obj = await _IndentMasterService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _IndentMasterService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpGet]
        [Route("IsOpeningStockSynced")]
        public async Task<IActionResult> IsOpeningStockSynced()
        {
            var obj = await _IndentMasterService.IsOpeningStockSynced();
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("UpdateMainStoreOpeningStock")]
        public async Task<IActionResult> UpdateMainStoreOpeningStock()
        {
            var obj = await _IndentMasterService.UpdateMainStoreOpeningStock();
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateMimsIndentStatus")]
        public async Task<IActionResult> UpdateMimsIndentStatus(UpdateIndentStatusDto input)
        {
            var obj = await _IndentMasterService.UpdateMimsIndentStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        [HttpGet]
        [Route("GetUnSyncMainStoreMedicineIndentFromMims")]
        public async Task<IActionResult> GetUnSyncMainStoreMedicineIndentFromMims()
        {
            var list=  await _IndentMasterService.GetUnSyncMainStoreMedicineIndentFromMims(_mimsBaseUrl, TokenService.GetHfHrId());
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("SyncMainStoreIndentDetailForHMIS")]
        public async Task<IActionResult> SyncMainStoreIndentDetailForHMIS(int IndentId)
        {
            var list = await _IndentMasterService.SyncMainStoreIndentDetailForHMIS(_mimsBaseUrl, TokenService.GetHfHrId() , IndentId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetUnSyncMainStoreIndentDetailForHMIS")]
        public async Task<IActionResult> GetUnSyncMainStoreIndentDetailForHMIS(int IndentId)
        {
            var list = await _IndentMasterService.GetUnSyncMainStoreIndentDetailForHMIS(_mimsBaseUrl, TokenService.GetHfHrId(), IndentId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetFilteredIndentListWithPagination")]
        public async Task<IActionResult> GetFilteredIndentListWithPagination([FromQuery] FilterIndentListDto? filter)
        {
            var response = await _IndentMasterService.GetFilteredIndentListWithPagination(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetMimsIndentDetailById")]
        public async Task<IActionResult> GetMimsIndentDetailById(Guid? IndentMasterId)
        {
            var response = await _IndentMasterService.GetMimsIndentDetailById(IndentMasterId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetMedicineIndentLog")]
        public async Task<IActionResult> GetMedicineIndentLog([FromQuery] FilterIndentMasterDto? filter)
        {

            var list = await _IndentMasterService.GetMedicineIndentLog(filter);
            return Ok(new ResponseSuccess { data = list });

        }

        [HttpGet]
        [Route("GetSyncIndentDetail")]
        public async Task<IActionResult> GetSyncIndentDetail(Guid? IndentMasterId)
        {
            var list = await _IndentMasterService.GetSyncIndentDetail(IndentMasterId);
            return Ok(new ResponseSuccess { data = list });

        }

    }
}

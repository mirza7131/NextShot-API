using CommonDTOs.ResponseDTO;
using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.InventoryDetailDto;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;
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
    public class StockCheckController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<StockCheckController> _logger;
        private readonly TokenService _tokenService;
        private readonly StockCheckService<InventoryMaster> _StockCheckService;

        #endregion

        #region Constructor

        public StockCheckController(ILogger<StockCheckController> logger, TokenService tokenService, StockCheckService<InventoryMaster> StockCheckService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _StockCheckService = StockCheckService;
        }
        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetStockByBrachOrByMedicine")]
        public async Task<IActionResult> GetStockByBrachOrByMedicine([FromQuery] StockCheckFilter filter)
        {
            var response = await _StockCheckService.GetStockByBrachOrByMedicine(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("GetInventoryDetailByBranchId")]
        public async Task<IActionResult> GetInventoryDetailByBranchId([FromBody] StockCheckboxCheckFilter? filter)
        {
            var response = await _StockCheckService.GetInventoryDetailByBranchId(filter);
            if (filter?.IsBatchWise == true)
            {
                return Ok(new ResponseSuccess { data = response as List<ViewInventoryDetailSP_DTO> });
            }
            else
            {
                return Ok(new ResponseSuccess { data = response as List<ViewInventoryMasterSP_DTO> });
            }
        }

        #endregion

    }
}

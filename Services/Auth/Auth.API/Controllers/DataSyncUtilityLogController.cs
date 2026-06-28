using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DataSyncUtilityLog;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class DataSyncUtilityLogController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        
        private readonly DataSyncUtilityLogService<DataSyncUtilityLog> _DataSyncUtilityLogService;

        #endregion

        #region Constructor

        public DataSyncUtilityLogController(ILogger<AuthenticationController> logger, TokenService tokenService, DataSyncUtilityLogService<DataSyncUtilityLog> DataSyncUtilityLogService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _DataSyncUtilityLogService = DataSyncUtilityLogService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditDataSyncUtilityLogDto input)
        {
            var obj = await _DataSyncUtilityLogService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateDataSyncLogInfo")]
        public async Task<IActionResult> CreateDataSyncLogInfo(CreateOrEditDataSyncUtilityLogDto input)
        {
            var obj = await _DataSyncUtilityLogService.CreateDataSyncLogInfo(input);
            return Ok(new ResponseSave { data = obj });
        }

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _DataSyncUtilityLogService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var response = await _DataSyncUtilityLogService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterDataSyncUtilityLogDto dataSyncUtilityLogDto)
        //{
        //    var response = await _DataSyncUtilityLogService.GetAllWithPagination(dataSyncUtilityLogDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _DataSyncUtilityLogService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

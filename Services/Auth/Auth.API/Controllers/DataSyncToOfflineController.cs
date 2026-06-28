using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DataSyncToOfflineDto;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSyncToOfflineController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly DataSyncToOfflineService<DataSyncToOffline> _DataSyncToOffline;


        //#endregion

        //#region Constructor

        public DataSyncToOfflineController(ILogger<AuthenticationController> logger, TokenService tokenService, DataSyncToOfflineService<DataSyncToOffline> DataSyncToOffline)
        {
            _logger = logger;
            _tokenService = tokenService;
            _DataSyncToOffline = DataSyncToOffline;
        }

        //#endregion

        //#region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditDataSyncToOfflineDto input)
        //{
        //    var obj = await _DataSyncToOffline.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _DataSyncToOffline.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        [HttpPost]
        [Route("SyncData")]
        public async Task<IActionResult> SyncData(FilterDataSyncToOfflineDto filter)
        {
            var obj = await _DataSyncToOffline.SyncData(filter);
            return Ok(new ResponseUpdate { data = obj });
        }

        //#endregion

        //#region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var response = await _DataSyncToOffline.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterDataSyncToOfflineDto profileTypeDto)
        //{
        //    var response = await _DataSyncToOffline.GetAllWithPagination(profileTypeDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        [HttpPost]
        [Route("GetDataToSync")]
        public async Task<IActionResult> GetDataToSync(FilterDataSyncToOfflineDto filter)
        {
            var obj = await _DataSyncToOffline.GetDataToSync(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

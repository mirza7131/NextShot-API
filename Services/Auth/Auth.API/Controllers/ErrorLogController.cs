using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ErrorLogDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ErrorLogController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly ErrorLogService<ErrorLog> _ErrorLogService;

        #endregion

        #region Constructor

        public ErrorLogController(ILogger<AuthenticationController> logger, TokenService tokenService, ErrorLogService<ErrorLog> ErrorLogService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _ErrorLogService = ErrorLogService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditErrorLogDto input)
        {
            var obj = await _ErrorLogService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(long Id)
        //{
        //    var obj = await _ErrorLogService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _ErrorLogService.GetAll();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(long Id)
        {
            var obj = await _ErrorLogService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

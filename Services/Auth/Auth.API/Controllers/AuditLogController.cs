using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AuditDto;
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
    public class AuditLogController : ControllerBase
    { 
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly AuditLogService<AuditLog> _AuditLogService;

        #endregion

        #region Constructor

        public AuditLogController(ILogger<AuthenticationController> logger, TokenService tokenService, AuditLogService<AuditLog> AuditLogService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _AuditLogService = AuditLogService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditAuditLogDto input)
        {
            var obj = await _AuditLogService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(long Id)
        //{
        //    var obj = await _AuditLogService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _AuditLogService.GetAll();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(long Id)
        {
            var obj = await _AuditLogService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

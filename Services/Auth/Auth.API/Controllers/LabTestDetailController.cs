using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.LabTestDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LabTestDetailController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly LabTestDetailService<LabTestDetail> _LabTestDetailService;

        #endregion

        #region Constructor

        public LabTestDetailController(ILogger<AuthenticationController> logger, TokenService tokenService, LabTestDetailService<LabTestDetail> LabTestDetailService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _LabTestDetailService = LabTestDetailService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditLabTestDetailDto input)
        {
            var obj = await _LabTestDetailService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _LabTestDetailService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _LabTestDetailService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _LabTestDetailService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

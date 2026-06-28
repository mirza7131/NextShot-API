using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LabTestDto;
using AuthDAL.Models.Dto.UserDto;
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
    public class LabTestController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly LabTestService<LabTest> _LabTestService;

        #endregion

        #region Constructor

        public LabTestController(ILogger<AuthenticationController> logger, TokenService tokenService, LabTestService<LabTest> LabTestService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _LabTestService = LabTestService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditLabTestDto input)
        {
            var obj = await _LabTestService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _LabTestService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _LabTestService.GetAll();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithHealthFacilityId")]
        public async Task<IActionResult> GetAllWithHealthFacilityId(int? HealthFacilityId = null)
        {
            var list = await _LabTestService.GetAllWithHealthFacilityId(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterUserDto filterUserDto)
        {
            var response = await _LabTestService.GetAllWithPagination(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _LabTestService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllByDepartmentId")]
        public async Task<IActionResult> GetAllByDepartmentId(Guid DepartmentProfileId)
        {
            var list = await _LabTestService.GetAll(DepartmentProfileId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllCacheLabTest")]
        public async Task<IActionResult> GetAllCacheLabTest()
        {
            var list = await _LabTestService.GetAllCacheLabTest();
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Helper Methods


        #endregion
    }
}

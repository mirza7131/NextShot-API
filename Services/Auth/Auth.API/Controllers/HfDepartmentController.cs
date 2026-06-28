using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HfDepartmentDto;
using AuthDAL.Models.Dto.PaginationDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HfDepartmentController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HfDepartmentService<HfDepartment> _HfDepartmentService;

        #endregion

        #region Constructor

        public HfDepartmentController(ILogger<AuthenticationController> logger, TokenService tokenService, HfDepartmentService<HfDepartment> HfDepartmentService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HfDepartmentService = HfDepartmentService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHfDepartmentDto input)
        {
            var obj = await _HfDepartmentService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HfDepartmentService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }


        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HfDepartmentService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HfDepartmentService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetHfDepartmentsByHealthFacility")]
        public async Task<IActionResult> GetHfDepartmentsByHealthFacility(int? HealthFacilityId)
        {
            var obj = await _HfDepartmentService.GetHfDepartmentsByHealthFacility(HealthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetDepartmentAndSectionByHealthFacility")]
        public async Task<IActionResult> GetDepartmentAndSectionByHealthFacility(int HealthFacilityId)
        {
            var obj = await _HfDepartmentService.GetDepartmentAndSectionByHealthFacility(HealthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllWithDepartment")]
        public async Task<IActionResult> GetAllWithDepartment([FromQuery]PagerDto filter)
        {
            var response = await _HfDepartmentService.GetAllWithDepartment(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

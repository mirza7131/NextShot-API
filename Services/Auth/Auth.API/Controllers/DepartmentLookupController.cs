using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
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
    [Authorize]
    public class DepartmentLookupController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly DepartmentLookupService<DepartmentLookup> _DepartmentLookupService;


        #endregion

        #region Constructor

        public DepartmentLookupController(ILogger<AuthenticationController> logger, TokenService tokenService, DepartmentLookupService<DepartmentLookup> DepartmentLookupService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _DepartmentLookupService = DepartmentLookupService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditDepartmentLookupDto input)
        {
            var obj = await _DepartmentLookupService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _DepartmentLookupService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _DepartmentLookupService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterDepartmentLookupDto departmentLookupDto)
        {
            var response = await _DepartmentLookupService.GetAllWithPagination(departmentLookupDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _DepartmentLookupService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        //[HttpGet]
        //[Route("SeedHfDepartmentsHfDepartmentSection")]
        //public async Task<IActionResult> SeedHfDepartmentsHfDepartmentSection()
        //{
        //    await _DepartmentLookupService.SeedHfDepartmentsHfDepartmentSection();
        //    return Ok(new ResponseSuccess { });
        //}

        #endregion

        #region Helper Methods


        #endregion
    }
}

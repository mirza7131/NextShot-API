using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HfDepartmentDto;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HfDepartmentSectionController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly HfDepartmentSectionService<HfDepartmentSection> _HfDepartmentSectionService;

        #endregion

        #region Constructor

        public HfDepartmentSectionController(ILogger<AuthenticationController> logger, TokenService tokenService, HfDepartmentSectionService<HfDepartmentSection> HfDepartmentSectionService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _HfDepartmentSectionService = HfDepartmentSectionService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditHfDepartmentSectionDto input)
        {
            var obj = await _HfDepartmentSectionService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("BulkCreateOrEdit")]
        public async Task<IActionResult> BulkCreateOrEdit(List<CreateOrEditHfDepartmentSectionDto> inputs)
        {
            var obj = await _HfDepartmentSectionService.BulkCreateOrEdit(inputs);
            return Ok(new ResponseSave { data = obj });
            
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _HfDepartmentSectionService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpGet]
        [Route("DeleteByHfDepartmentId")]
        public async Task<IActionResult> DeleteByHfDepartmentId(int HfDepartmentId)
        {
            var obj = await _HfDepartmentSectionService.DeleteByHfDepartmentId(HfDepartmentId);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("DumpSectionAgainstHfDepartment")]
        public async Task<IActionResult> DumpSectionAgainstHfDepartment(DumpHfDepartmentSection input)
        {
            var obj = await _HfDepartmentSectionService.DumpSectionAgainstHfDepartment(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _HfDepartmentSectionService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _HfDepartmentSectionService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetHfDepartmentSectionsByHfDepartmentId")]
        public async Task<IActionResult> GetHfDepartmentSectionsByHfDepartmentId(int HfDepartmentId)
        {
            var obj = await _HfDepartmentSectionService.GetHfDepartmentSectionsByHfDepartmentId(HfDepartmentId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetBedsBySectionId")]
        public async Task<IActionResult> GetBedsBySectionId(int? HealthFacilityId, int? DepartmentLookupId, int? SectionLookupId)
        {
            var list = await _HfDepartmentSectionService.GetBedsBySectionId(HealthFacilityId, DepartmentLookupId, SectionLookupId);
            return Ok(new ResponseSuccess { data = list });
        }

        //[HttpGet]
        //[Route("GetAllWithDepartmentSection")]
        //public async Task<IActionResult> GetAllWithDepartmentSection([FromQuery]PagerDto filter)
        //{
        //    var response = await _HfDepartmentSectionService.GetAllWithDepartmentSection(filter);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        #endregion

        #region Helper Methods

        #endregion
    }
}

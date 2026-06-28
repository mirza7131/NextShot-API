using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.SectionLookupDto;
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
    public class SectionLookupController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly SectionLookupService<SectionLookup> _SectionLookupService;

        #endregion

        #region Constructor

        public SectionLookupController(ILogger<AuthenticationController> logger, TokenService tokenService, SectionLookupService<SectionLookup> SectionLookupService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _SectionLookupService = SectionLookupService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditSectionLookupDto input)
        {
            var obj = await _SectionLookupService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _SectionLookupService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("DumpIpdSectionsFromHr")]
        public async Task<IActionResult> DumpIpdSectionsFromHr(DumpSectionFromHrDto input)
        {
            var obj = await _SectionLookupService.DumpIpdSectionsFromHr(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _SectionLookupService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _SectionLookupService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByDepartmentId")]
        public async Task<IActionResult> GetByDepartmentId(int DepartmentId)
        {
            var obj = await _SectionLookupService.GetByDepartmentId(DepartmentId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllSectionWithDepartment")]
        public async Task<IActionResult> GetAllSectionWithDepartment()
        {
            var list = await _SectionLookupService.GetAllSectionWithDepartment();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithDepartment")]
        public async Task<IActionResult> GetAllWithDepartment([FromQuery]FilterSectionLookupDto sectionLookupDto)
        {
            var list = await _SectionLookupService.GetAllWithDepartment(sectionLookupDto);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

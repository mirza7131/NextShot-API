using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.DrugAddict.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsssController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<TestsssController> _logger;
        private readonly TokenService _tokenService;
        private readonly ProfileTypeService<ProfileType> _ProfileTypeService;

        #endregion

        #region Constructor

        public TestsssController(ILogger<TestsssController> logger, TokenService tokenService, ProfileTypeService<ProfileType> ProfileTypeService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _ProfileTypeService = ProfileTypeService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditProfileTypeDto input)
        {
            var obj = await _ProfileTypeService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _ProfileTypeService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _ProfileTypeService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterProfileTypeDto profileTypeDto)
        {
            var response = await _ProfileTypeService.GetAllWithPagination(profileTypeDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _ProfileTypeService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}


using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.TestDto;
using HMIS.DrugAddict.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestssController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly TestsssService<Testsss> _testsssService;

        #endregion

        #region Constructor
        //ILogger<AuthenticationController> logger
        public TestssController( TokenService tokenService, TestsssService<Testsss> testsssService)
        {
           // _logger = logger;
            _tokenService = tokenService;
            _testsssService = testsssService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditTestsssDto input)
        {
            var obj = await _testsssService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _testsssService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _testsssService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response});
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery]FilterTestssDto testssDto)
        {
            var response = await _testsssService.GetAllWithPagination(testssDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _testsssService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

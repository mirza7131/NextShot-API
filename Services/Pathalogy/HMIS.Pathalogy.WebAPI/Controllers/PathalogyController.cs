using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Pathalogy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PathalogyController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PathalogyController> _logger;
        private readonly TokenService _tokenService;
        //private readonly PathalogyService<Patient> _PathalogyService;

        #endregion

        #region Constructor

        public PathalogyController(ILogger<PathalogyController> logger, TokenService tokenService/*, PathalogyService<Pathalogy> PathalogyService*/)
        {
            _logger = logger;
            _tokenService = tokenService;
            //_PathalogyService = PathalogyService;
        }

        #endregion

        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditPathalogyDto input)
        //{
        //    var obj = await _PathalogyService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _PathalogyService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var response = await _PathalogyService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterPathalogyDto PathalogyDto)
        //{
        //    var response = await _PathalogyService.GetAllWithPagination(PathalogyDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetAllWithPathalogyType")]
        //public async Task<IActionResult> GetAllWithPathalogyType([FromQuery] FilterPathalogyDto PathalogyDto)
        //{
        //    var response = await _PathalogyService.GetAllWithPathalogyType(PathalogyDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(Guid Id)
        //{
        //    var obj = await _PathalogyService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        //[HttpGet]
        //[Route("GetPathalogyByPathalogyType")]
        //public async Task<IActionResult> GetPathalogyByPathalogyType(string PathalogyType)
        //{
        //    var obj = await _PathalogyService.GetPathalogyByPathalogyType(PathalogyType);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        #endregion

        #region Helper Methods


        #endregion
    }
}

using CommonDTOs.ResponseDTO;
using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.MimsLookupsDto;
using HMIS.MIMS.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.MIMS.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class MimsLookupsController : ControllerBase
    //{
    //}

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MimsLookupsController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<MimsLookupsController> _logger;
        private readonly TokenService _tokenService;
        private readonly MimsLookupsService<MimsBranch> _MimsLookupsService; 

        #endregion

        #region Constructor

        public MimsLookupsController(ILogger<MimsLookupsController> logger, TokenService tokenService, MimsLookupsService<MimsBranch> MimsLookupsService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _MimsLookupsService = MimsLookupsService;
        }

        #endregion

        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditMimsLookupsDto input)
        //{
        //    var obj = await _MimsLookupsService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _MimsLookupsService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllMimsBranches")]
        public async Task<IActionResult> GetAllMimsBranches()
        {
            var response = await _MimsLookupsService.GetAllMimsBranches();
            return Ok(new ResponseSuccess { data = response });
        }

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterMimsLookupsDto MimsLookupsDto)
        //{
        //    var response = await _MimsLookupsService.GetAllWithPagination(MimsLookupsDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetAllWithMimsLookupsType")]
        //public async Task<IActionResult> GetAllWithMimsLookupsType([FromQuery] FilterMimsLookupsDto MimsLookupsDto)
        //{
        //    var response = await _MimsLookupsService.GetAllWithMimsLookupsType(MimsLookupsDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(Guid Id)
        //{
        //    var obj = await _MimsLookupsService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        //[HttpGet]
        //[Route("GetMimsLookupsByMimsLookupsType")]
        //public async Task<IActionResult> GetMimsLookupsByMimsLookupsType(string MimsLookupsType)
        //{
        //    var obj = await _MimsLookupsService.GetMimsLookupsByMimsLookupsType(MimsLookupsType);
        //    return Ok(new ResponseSuccess { data = obj });
        //}


        //[HttpGet]
        //[Route("GetMimsLookupsByShortName")]
        //public async Task<IActionResult> GetMimsLookupsByShortName(string ShortName)
        //{
        //    var obj = await _MimsLookupsService.GetMimsLookupsByShortName(ShortName);
        //    return Ok(new ResponseSuccess { data = obj });
        //}


        //[HttpGet]
        //[Route("GetDataByMimsLookups")]
        //public async Task<IActionResult> GetDataByMimsLookups(string MimsLookupsType)
        //{
        //    var obj = await _MimsLookupsService.GetDataByMimsLookups(MimsLookupsType);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        #endregion

        #region Helper Methods


        #endregion
    }
}

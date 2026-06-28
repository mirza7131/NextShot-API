using CommonDTOs.ResponseDTO;
using HMIS.DrugAddict.Domain.Models.Dto.FilterDto;
using HMIS.DrugAddict.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.DrugAddict.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OutSourceApisController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly OutSourceApisService _outSourceApisService;
     
        #endregion

        #region Constructor
        public OutSourceApisController(TokenService tokenService, OutSourceApisService outSourceApisService)
        {
            _tokenService = tokenService;
            _outSourceApisService = outSourceApisService;
           
        }
        #endregion

        #region Read Operations
        [HttpGet]
        [Route("GetDrugAddictPatientVisits")]
        public async Task<IActionResult> GetDrugAddictPatientVisits([FromQuery] ExternalApiFilterDto filter)
        {
            var list = await _outSourceApisService.GetDrugAddictPatientVisits(filter);

            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
    }
}

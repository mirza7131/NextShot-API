using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.NursingEventDto;


namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NursingEventsController : Controller
    {

        #region Class Fields & Propertities

        private readonly ILogger<NursingEventsController> _logger;
        private readonly TokenService _tokenService;
        private readonly NursingEventsService<NursingEvent> _NursingEventsService;
        #endregion

        #region Constructor
        public NursingEventsController(ILogger<NursingEventsController> logger, TokenService tokenService, NursingEventsService<NursingEvent> nursingEventsService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _NursingEventsService = nursingEventsService;
        }
        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditNursingEventDto input)
        {
            var obj = await _NursingEventsService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateEventStatus")]
        public async Task<IActionResult> UpdateEventStatus(Guid NursingEventsId)
        {
            var obj = await _NursingEventsService.UpdateEventStatus(NursingEventsId);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _NursingEventsService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }


        #endregion

        #region Read Operations


        [HttpGet]
        [Route("GetNursingEventsListWithDetail")]
        public async Task<IActionResult> GetNursingEventsListWithDetail([FromQuery] FilterNursingEventDto filter)
        {
            var list = await _NursingEventsService.GetNursingEventsListWithDetail(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

    }
}
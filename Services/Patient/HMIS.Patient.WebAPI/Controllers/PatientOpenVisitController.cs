using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientOpenVisitController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientVitalController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientOpenVisitService<PatientOpenVisit> _PatientOpenVisitService;
        #endregion

        #region Constructor

        public PatientOpenVisitController(ILogger<PatientVitalController> logger, TokenService tokenService, PatientOpenVisitService<PatientOpenVisit> PatientOpenVisitService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientOpenVisitService = PatientOpenVisitService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditOpenVisitDto input)
        {
            var obj = await _PatientOpenVisitService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientOpenVisitService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("ReleaseOccupiedPatientVisit")]
        public async Task<IActionResult> ReleaseOccupiedPatientVisit(Guid VisitId)
        {
            var obj = await _PatientOpenVisitService.ReleaseOccupiedPatientVisit(VisitId);
            return Ok(new ResponseUpdate
            {
                data = obj,
                message = CommonMessageConstant.PatientReleased
            });
        }

        //[HttpPost]
        //[Route("DischargePatientVisit")]
        //public async Task<IActionResult> DischargePatientVisit(PatientDischargeDto input)
        //{
        //    var obj = await _PatientOpenVisitService.DischargePatientVisit(input);
        //    return Ok(new ResponseUpdate { data = obj });
        //}

        [HttpPost]
        [Route("UpdateSscStatusAndDocumentDto")]
        public async Task<IActionResult> UpdateSscStatusAndDocumentDto(UpdateSscStatusAndDocumentDto input)
        {
            var obj = await _PatientOpenVisitService.UpdateSscStatusAndDocumentDto(input);
            return Ok(new ResponseUpdate { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientOpenVisitService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetPatientVisitsListWithDetail")]
        public async Task<IActionResult> GetPatientVisitsListWithDetail([FromQuery]FilterPatientVisitDto filterPatientVisitDto)
        {
            var response = await _PatientOpenVisitService.GetPatientVisitsListWithDetail(filterPatientVisitDto);

            return Ok(new ResponseSuccess { data = response });
            //return Ok(new ResponsePaginatedDTO { data = response.List, pageCount = response.TotalPages, totalRecords = response.TotalCount });
        }

        [HttpGet]
        [Route("GetIPDPatientVisitsListWithDetail")]
        public async Task<IActionResult> GetIPDPatientVisitsListWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var response = await _PatientOpenVisitService.GetIPDPatientVisitsListWithDetail(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientOpenVisitService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetTodayVisits")]
        public async Task<IActionResult> GetTodayVisits([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientOpenVisitService.GetTodayVisits(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetOldVisits")]
        public async Task<IActionResult> GetOldVisits([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientOpenVisitService.GetOldVisits(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllQue")]
        public async Task<IActionResult> GetAllQue(string SearchValue)
        {
            var list = await _PatientOpenVisitService.GetAllQue(SearchValue);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetDischargedPatientVisitsListWithDetail")]
        public async Task<IActionResult> GetDischargedPatientVisitsListWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var response = await _PatientOpenVisitService.GetDischargedPatientVisitsListWithDetail(filterPatientVisitDto);

            return Ok(new ResponseSuccess { data = response });
        }
        
        [HttpGet]
        [Route("GetPatientDetailsForReferralSlipByVisitId")]
        public async Task<IActionResult> GetPatientDetailsForReferralSlipByVisitId(Guid VisitId)
        {
            var response = await _PatientOpenVisitService.GetPatientDetailsForReferralSlipByVisitId(VisitId);

            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetDischargedButNotSscClaimedPatientVisitsListWithDetail")]
        public async Task<IActionResult> GetDischargedButNotSscClaimedPatientVisitsListWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var response = await _PatientOpenVisitService.GetDischargedButNotSscClaimedPatientVisitsListWithDetail(filterPatientVisitDto);

            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetSscClaimConfirmPatientVisitsListWithDetail")]
        public async Task<IActionResult> GetSscClaimConfirmPatientVisitsListWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var response = await _PatientOpenVisitService.GetSscClaimConfirmPatientVisitsListWithDetail(filterPatientVisitDto);

            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("UpdatePatientVisitForSscClaimed")]
        public async Task<IActionResult> UpdatePatientVisitForSscClaimed(PatientVisitSscClaimedDto input)
        {
            var obj = await _PatientOpenVisitService.UpdatePatientVisitForSscClaimed(input);
            return Ok(new ResponseSave { data = obj });
        }
 
        [HttpPost]
        [Route("UpdatePatientEligibleForSSC")]
        public async Task<IActionResult> UpdatePatientEligibleForSSC(CreateorEditPatientEligibleForSSCDto input)
        {
            var obj = await _PatientOpenVisitService.UpdatePatientEligibleForSSC(input);

            return Ok(new ResponseSave { message = "Record is Saved Successfully!", data = obj });
        }
        #endregion

        #region Helper Methods


        #endregion
    }
}

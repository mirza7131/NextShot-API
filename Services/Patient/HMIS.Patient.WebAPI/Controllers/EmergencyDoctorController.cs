
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Service;

using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmergencyDoctorController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientDiagnoseController> _logger;
        private readonly TokenService _tokenService;
        private readonly EmergencyDoctorService _emergencyDoctorService;

        #endregion

        #region Constructor

        public EmergencyDoctorController(
            ILogger<PatientDiagnoseController> logger, 
            TokenService tokenService,
            EmergencyDoctorService emergencyDoctorService
        )
        {
            _logger = logger;
            _tokenService = tokenService;
            _emergencyDoctorService = emergencyDoctorService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEditPatientDiagnose")]
        public async Task<IActionResult> CreateOrEditPatientDiagnose(CreateOrEditPatientDiagnoseDto input)
        {
            var obj = await _emergencyDoctorService.CreateOrEditPatientDiagnose(input);
            return Ok(new ResponseSave { data = obj });
        }

        //[HttpPost]
        //[Route("CreateOrEditPatientDiagnoseWithPrescription")]
        //public async Task<IActionResult> CreateOrEditPatientDiagnoseWithPrescription(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        //{
        //    var obj = await _PatientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescription(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("CreateOrEditPhysiotherapyForm")]
        //public async Task<IActionResult> CreateOrEditPhysiotherapyForm(CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto input)
        //{
        //    var obj = await _PatientDiagnoseService.CreateOrEditPhysiotherapyForm(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _PatientDiagnoseService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _PatientDiagnoseService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetByIdWithPrescription")]
        //public async Task<IActionResult> GetByIdWithPrescription(Guid Id)
        //{
        //    var obj = await _PatientDiagnoseService.GetByIdWithPrescription(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        //[HttpGet]
        //[Route("GetAllQue")]
        //public async Task<IActionResult> GetAllQue(int? HealthFacilityId)
        //{
        //    var list = await _PatientDiagnoseService.GetAllQue(HealthFacilityId);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetLabTestbyVisitId")]
        //public async Task<IActionResult> GetLabTestbyVisitId(Guid VisitId)
        //{
        //    var list = await _PatientDiagnoseService.GetLabTestbyVisitId(VisitId);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        //{
        //    var list = await _PatientDiagnoseService.GetAllWithPagination(filterPatientVisitDto);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetAllWithPaginationWithDetail")]
        //public async Task<IActionResult> GetAllWithPaginationWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        //{
        //    var list = await _PatientDiagnoseService.GetAllWithPaginationWithDetail(filterPatientVisitDto);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        #endregion

        #region Helper Methods


        #endregion

        #region IPD

        //[HttpGet]
        //[Route("GetAllIpdQue")]
        //public async Task<IActionResult> GetAllIpdQue(int? HealthFacilityId)
        //{
        //    var list = await _PatientDiagnoseService.GetAllIpdQue(HealthFacilityId);
        //    return Ok(new ResponseSuccess { data = list });
        //}


        //[HttpPost]
        //[Route("CreateOrEditPatientDiagnoseWithPrescriptionForIpd")]
        //public async Task<IActionResult> CreateOrEditPatientDiagnoseWithPrescriptionForIpd(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        //{
        //    var obj = await _PatientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescriptionForIpd(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        #endregion
    }
}

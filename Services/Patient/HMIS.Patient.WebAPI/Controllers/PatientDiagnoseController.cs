
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientLabTestDto;
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
    public class PatientDiagnoseController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientDiagnoseController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;

        #endregion

        #region Constructor

        public PatientDiagnoseController(ILogger<PatientDiagnoseController> logger, TokenService tokenService, PatientDiagnoseService<PatientDiagnose> PatientDiagnoseService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientDiagnoseService = PatientDiagnoseService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDiagnoseDto input)
        {
            var obj = await _PatientDiagnoseService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditPatientDiagnoseWithPrescription")]
        public async Task<IActionResult> CreateOrEditPatientDiagnoseWithPrescription(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            var obj = await _PatientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescription(input);
            return Ok(new ResponseSave { data = obj });
        }



        [HttpPost]
        [Route("CreateDoctorNotes")]
        public async Task<IActionResult> CreateDoctorNotes(DoctorNotesDTO input)
        {
            var obj = await _PatientDiagnoseService.CreateDoctorNotes(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreatePatientLabTest")]
        public async Task<IActionResult> CreatePatientLabTest(List<CreateOrEditPatientLabTestDto> PatientLabTests)
        {
            await _PatientDiagnoseService.CreatePatientLabTest(PatientLabTests);
            return Ok(new ResponseSuccess { });
        }

        [HttpPost]
        [Route("CreateOrEditPhysiotherapyForm")]
        public async Task<IActionResult> CreateOrEditPhysiotherapyForm(CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto input)
        {
            var obj = await _PatientDiagnoseService.CreateOrEditPhysiotherapyForm(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientDiagnoseService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("UpdateProcedureData")]
        public async Task<IActionResult> UpdateProcedureData(List<DentalProcedureListDTO> dentalProcedures)
        {
            await _PatientDiagnoseService.UpdateProcedureData(dentalProcedures);
            return Ok(new ResponseSave { });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientDiagnoseService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetByIdWithPrescription")]
        public async Task<IActionResult> GetByIdWithPrescription(Guid Id)
        {
            var obj = await _PatientDiagnoseService.GetByIdWithPrescription(Id);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetDoctorNotesByVisit")]
        public async Task<IActionResult> GetDoctorNotesByVisit(Guid PatientVisitId)
        {
            var obj = await _PatientDiagnoseService.GetDoctorNotesByVisit(PatientVisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllQue")]
        public async Task<IActionResult> GetAllQue(int? HealthFacilityId)
        {
            var list = await _PatientDiagnoseService.GetAllQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllQueMlc")]
        public async Task<IActionResult> GetAllQueMLC(int? HealthFacilityId)
        {
            var list = await _PatientDiagnoseService.GetAllQueMLC(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetLabTestbyVisitId")]
        public async Task<IActionResult> GetLabTestbyVisitId(Guid VisitId)
        {
            var list = await _PatientDiagnoseService.GetLabTestbyVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetLabTestbyDiagnoseId")]
        public async Task<IActionResult> GetLabTestbyDiagnoseId(Guid DiagnoseId)
        {
            var list = await _PatientDiagnoseService.GetLabTestbyDiagnoseId(DiagnoseId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientDiagnoseService.GetAllWithPagination(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllWithPaginationWithDetail")]
        public async Task<IActionResult> GetAllWithPaginationWithDetail([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientDiagnoseService.GetAllWithPaginationWithDetail(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetFilteredDentalProcedureList")]
        public async Task<IActionResult> GetFilteredDentalProcedureList([FromQuery] FilterPatientDto filter)
        {
            var list = await _PatientDiagnoseService.GetFilteredDentalProcedureList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [AllowAnonymous]
		[HttpGet]
		[Route("GetMeaslesPatientList")]
		public async Task<IActionResult> GetMeaslesPatientList([FromQuery] FilterMeaslesPatientDto filter)
		{
			var list = await _PatientDiagnoseService.GetMeaslesPatientList(filter);
			return Ok(new ResponseSuccess { data = list });
		}


		[HttpGet]
        [Route("GetJsonObj")]
        public async Task<IActionResult> GetJsonObj(Guid? visitId, string formType, Guid? diagnoseId)
        {
            var list = await _PatientDiagnoseService.GetJsonObj(visitId, formType, diagnoseId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion

        #region IPD

        [HttpGet]
        [Route("GetAllIpdQue")]
        public async Task<IActionResult> GetAllIpdQue(int? HealthFacilityId)
        {
            var list = await _PatientDiagnoseService.GetAllIpdQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpPost]
        [Route("CreateOrEditPatientDiagnoseWithPrescriptionForIpd")]
        public async Task<IActionResult> CreateOrEditPatientDiagnoseWithPrescriptionForIpd(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            var obj = await _PatientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescriptionForIpd(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion
    }
}

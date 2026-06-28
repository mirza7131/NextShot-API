using CommonDTOs.ResponseDTO;
using HMIS.FamilyPlanning.Domain.Models.DbModels;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using HMIS.FamilyPlanning.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.FamilyPlanning.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ClientHistoryController : ControllerBase
	{

		#region Class Fields & Propertities
		private ClientHistoryService _clientHistoryService;
		#endregion

		#region Constructor
		public ClientHistoryController(ClientHistoryService clientHistoryService)
		{
			_clientHistoryService = clientHistoryService;
		}
		#endregion



		#region CUD

		[HttpPost]
		[Route("CreateOrEditRegistrationDetail")]

		public async Task<IActionResult> CreateOrEditRegistrationDetail(RegistrationDetailDTO registrationDetail)
		{
			await _clientHistoryService.CreateOrEditRegistrationDetail(registrationDetail);
			return Ok(new ResponseSave { });
		}

		[HttpPost]
		[Route("CreateOrEditPastHistory")]

		public async Task<IActionResult> CreateOrEditPastHistory(PastHistoryDTO pastHistory)
		{
			await _clientHistoryService.CreateOrEditPastHistory(pastHistory);
			return Ok(new ResponseSave { });
		}


		[HttpPost]
		[Route("CreateOrEditMedicalHistory")]

		public async Task<IActionResult> CreateOrEditMedicalHistory(MedicalHistoryDTO medicalHistoryDTO)
		{
			await _clientHistoryService.CreateOrEditMedicalHistory(medicalHistoryDTO);
			return Ok(new ResponseSave { });
		}

		[HttpPost]
		[Route("CreateOrEditSurgicalHistory")]

		public async Task<IActionResult> CreateOrEditSurgicalHistory(SurgicalHistoryDTO surgicalHistoryDTO)
		{
			await _clientHistoryService.CreateOrEditSurgicalHistory(surgicalHistoryDTO);
			return Ok(new ResponseSave { });
		}


		[HttpPost]
		[Route("CreateOrEditExamination")]

		public async Task<IActionResult> CreateOrEditExamination(ExaminationDTO examinationDTO)
		{
			await _clientHistoryService.CreateOrEditExamination(examinationDTO);
			return Ok(new ResponseSave { });
		}
		#endregion



		#region Read Operations
		[HttpGet]
		[Route("GetRegistrationDetailById")]

		public async Task<IActionResult> GetRegistrationDetailById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetRegistrationDetailById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}


		[HttpGet]
		[Route("GetVitalDetailById")]

		public async Task<IActionResult> GetVitalDetailById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetVitalDetailById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}


		[HttpGet]
		[Route("GetPatientPastHistoryById")]

		public async Task<IActionResult> GetPatientPastHistoryById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetPatientPastHistoryById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}


		[HttpGet]
		[Route("GetPatientMedicalHistoryById")]
		public async Task<IActionResult> GetPatientMedicalHistoryById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetPatientMedicalHistoryById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}



		[HttpGet]
		[Route("GetPatientSurgicalHistoryById")]
		public async Task<IActionResult> GetPatientSurgicalHistoryById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetPatientSurgicalHistoryById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}



		[HttpGet]
		[Route("GetPatientExaminationById")]
		public async Task<IActionResult> GetPatientExaminationById(Guid PatientId)
		{
			var data = await _clientHistoryService.GetPatientExaminationById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}




		[HttpGet]
		[Route("IsPatientRegisteredForFPAsync")]
		public async Task<IActionResult> IsPatientRegisteredForFPAsync(Guid PatientId)
		{
			var data = await _clientHistoryService.IsPatientRegisteredForFPAsync(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}

		#endregion
	}
}

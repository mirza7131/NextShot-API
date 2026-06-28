using CommonDTOs.ResponseDTO;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using HMIS.FamilyPlanning.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.FamilyPlanning.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ClientFollowupController : ControllerBase
	{
		#region Class Fields & Propertities
		private ClientFollowupService _clientFollowupService;
		#endregion


		#region Constructor
		public ClientFollowupController(ClientFollowupService clientFollowupService)
		{
			_clientFollowupService = clientFollowupService;
		}
		#endregion



		#region CUD

		[HttpPost]
		[Route("CreateOrEditClientFollowUp")]

		public async Task<IActionResult> CreateOrEditClientFollowUp(ClientFollowupDTO clientFollowupDTO)
		{
			await _clientFollowupService.CreateOrEditClientFollowUp(clientFollowupDTO);
			return Ok(new ResponseSave { });
		}
		#endregion



		#region Read Operations

		[HttpGet]
		[Route("GetMethodInUse")]

		public async Task<IActionResult> GetMethodInUse(Guid PatientId)
		{
			var data = await _clientFollowupService.GetMethodInUse(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}


		[HttpGet]
		[Route("IspatientFollowUp")]
		public async Task<IActionResult> IspatientFollowUp(Guid PatientId, Guid PatientVisitId)
		{
			var data = await _clientFollowupService.IspatientFollowUp(PatientId, PatientVisitId);
			return Ok(new ResponseSuccess { data = data });
		}
		#endregion
	}
}

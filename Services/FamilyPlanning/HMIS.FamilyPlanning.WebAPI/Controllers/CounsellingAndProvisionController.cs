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
	public class CounsellingAndProvisionController : ControllerBase
	{

		#region Class Fields & Propertities
		private CounclingAndProvisionalService _counclingAndProvisionalService;
		#endregion

		#region Constructor
		public CounsellingAndProvisionController(CounclingAndProvisionalService counclingAndProvisionalService)
		{
			_counclingAndProvisionalService = counclingAndProvisionalService;
		}
		#endregion



		#region CUD


		[HttpPost]
		[Route("CreateOrEditCounsellingAndProvision")]

		public async Task<IActionResult> CreateOrEditCounsellingAndProvision(CounsellingAndProvisionDTO counsellingAndProvisionDTO)
		{
			await _counclingAndProvisionalService.CreateOrEditCounsellingAndProvision(counsellingAndProvisionDTO);
			return Ok(new ResponseSave { });
		}




		[HttpGet]
		[Route("GetPatientCounsellingAndProvisionById")]
		public async Task<IActionResult> GetPatientCounsellingAndProvisionById(Guid PatientId)
		{
			var data = await _counclingAndProvisionalService.GetPatientCounsellingAndProvisionById(PatientId);
			return Ok(new ResponseSuccess { data = data });
		}
		#endregion
	}
}

using CommonDTOs.ResponseDTO;
using HMIS.NCD.Domain.Models.DTO;
using HMIS.NCD.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.NCD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NcdClinicController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly INcdClinic _ncdService;
        #endregion

        #region Constructor
        public NcdClinicController(INcdClinic ncdService)
        {
            _ncdService = ncdService;
        }
        #endregion

        #region CU
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientFollowUpNcdClinicDto Input)
        {
            var obj = await _ncdService.CreateOrEdit(Input);
            return Ok(new ResponseSave { data = obj });
        }


        [HttpPost]
        [Route("SavePatientPrescription")]
        public async Task<IActionResult> SavePatientPrescription(PatientPrescriptionDTO Input)
        {
            await _ncdService.SavePatientPrescription(Input);
            return Ok(new ResponseSave { });
        }
        [HttpPost]
        [Route("SaveFollowup")]
        public async Task<IActionResult> SaveFollowup(PatientPrescriptionDTO Input)
        {
            await _ncdService.SaveFollowup(Input);
            return Ok(new ResponseSave { });
        }
        #endregion


        #region GET

        [HttpGet]
        [Route("GetPatientLastIssueBookLetDate")]
        public async Task<IActionResult> GetPatientLastIssueBookLetDate(Guid PatientId)
        {
            var date = await _ncdService.GetPatientLastIssueBookLetDate(PatientId);
            return Ok(new ResponseSuccess { data = date });
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using CommonDTOs.ResponseDTO;
using HMIS.NCD.Domain.Models.DTO;
using HMIS.NCD.Service.Interfaces;
using HMIS.Aggregator.API;

namespace HMIS.NCD.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellWomanController : ControllerBase
    {

        #region Class Fields & Properties
        private readonly IWellWomanClinic _wellWomanService;
        #endregion


        #region Constructor
        public WellWomanController(IWellWomanClinic wellWomanService)
        {
            _wellWomanService = wellWomanService;
        }
        #endregion


        #region CUD
        [HttpPost]
        [Route("SaveBreastCancerPatient")]
        public async Task<IActionResult> SaveBreastCancerPatient(BreastCancerRiskIdentificationDTO Input)
        {
            await _wellWomanService.SaveBreastCancerPatient(Input);
            return Ok(new ResponseSave { });
        }

        [HttpPost]
        [Route("SaveBreastClinicalExamination")]
        public async Task<IActionResult> SaveBreastClinicalExamination(BreastCBCDTO model)
        {
            await _wellWomanService.SaveBreastClinicalExamination(model);
            return Ok(new ResponseSave { });
        }


        [HttpPost]
        [Route("SaveCVCExamination")]
        public async Task<IActionResult> SaveCVCExamination(CVCExamDTO model)
        {
            await _wellWomanService.SaveCVCExamination(model);
            return Ok(new ResponseSave { });
        }

        [HttpPost]
        [Route("SaveGDMPatientwithTrimester")]
        public async Task<IActionResult> SaveGDMPatientwithTrimester(GDMPatientDTO model)
        {
            await _wellWomanService.SaveGDMPatientwithTrimester(model);
            return Ok(new ResponseSave { });
        }



        [HttpPost]
        [Route("SaveUltraSoundAndDiagnosisResults")]
        public async Task<IActionResult> SaveUltraSoundAndDiagnosisResults(UltraSoundAndDiagnosisDTO model)
        {
            await _wellWomanService.SaveUltraSoundAndDiagnosisResults(model);
            return Ok(new ResponseSave { });
        }

        #endregion



        #region GET
        [HttpGet]
        [Route("GetRiskAssessmentForBreastCancerStatus")]
        public async Task<IActionResult> GetRiskAssessmentForBreastCancerStatus(Guid PatientId)
        {
            var dbObj = await _wellWomanService.GetRiskAssessmentForBreastCancerStatus(PatientId);
            return Ok(new ResponseSuccess { data = dbObj });
        }
        
        [HttpGet]
        [Route("CheckGDMData")]
        public async Task<IActionResult> CheckGDMData(Guid PatientId)
        {
            var dbObj = await _wellWomanService.CheckGDMData(PatientId);
            return Ok(new ResponseSuccess { data = dbObj });
        }
        
        [HttpGet]
        [Route("GetCervicalCancerPatientDetailByVisitId")]
        public async Task<IActionResult> GetCervicalCancerPatientDetailByVisitId(Guid PatientVisitId)
        {
            var dbObj = await _wellWomanService.GetCervicalCancerPatientDetailByVisitId(PatientVisitId);
            return Ok(new ResponseSuccess { data = dbObj });
        }

        #endregion

    }
}

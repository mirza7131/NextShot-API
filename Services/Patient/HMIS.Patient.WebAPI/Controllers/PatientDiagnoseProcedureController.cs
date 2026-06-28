using AuthBAL;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseProcedureDto;
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
    public class PatientDiagnoseProcedureController : ControllerBase
    {
        #region Class Fields & Propertities
        private readonly PatientDiagnoseProcedureService _patientDiagnoseProcedureService;
        #endregion

        #region Constructor

        public PatientDiagnoseProcedureController(
          PatientDiagnoseProcedureService patientDiagnoseProcedureService
        )
        {
            _patientDiagnoseProcedureService = patientDiagnoseProcedureService;
        }

        #endregion
        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDiagnoseProcedureDto input)
        {
            var obj = await _patientDiagnoseProcedureService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }


        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _mimsMedicineDataService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        //[HttpPost]
        //[Route("ImportDataFromMims")]
        //public async Task<IActionResult> ImportDataFromMims()
        //{
        //    await _mimsMedicineDataService.ImportDataFromMims();
        //    return Ok(new ResponseSave());
        //}

        //[HttpPost]
        //[Route("DiscontinueMedicineAdvised")]
        //public async Task<IActionResult> DiscontinueMedicineAdvised(CreateOrEditMedicineAdvisedDto input)
        //{
        //    var obj = await _medicineAdvisedService.DiscontinueMedicineAdvised(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("UpdateMedicineAdvisedDays")]
        //public async Task<IActionResult> UpdateMedicineAdvisedDays(CreateOrEditMedicineAdvisedDto input)
        //{
        //    var obj = await _medicineAdvisedService.UpdateMedicineAdvisedDays(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        #endregion
        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _mimsMedicineDataService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetByPatientVisitId")]
        public async Task<IActionResult> GetByPatientVisitId(Guid patientVisitId)
        {
            var list = await _patientDiagnoseProcedureService.GetByPatientVisitId(patientVisitId);

            return Ok(new ResponseSuccess { data = list });
        }



        #endregion
        #region Helper Methods
        #endregion
    }
}

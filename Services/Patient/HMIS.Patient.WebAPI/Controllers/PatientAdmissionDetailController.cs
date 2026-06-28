using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientAdmissionDetailController : Controller
    {
        #region Class Fields & Propertities
        private readonly PatientAdmissionDetailService<PatientAdmissionDetail> _PatientAdmissionDetailService;
        #endregion

        #region Constructor

        public PatientAdmissionDetailController(
            PatientAdmissionDetailService<PatientAdmissionDetail> PatientAdmissionDetailService
         )
        {
            _PatientAdmissionDetailService = PatientAdmissionDetailService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientAdmissionDetailDto input)
        {
            var obj = await _PatientAdmissionDetailService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _PatientAdmissionDetailService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientAdmissionDetailService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientAdmissionDetailService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAdmissionDetailByVisitId")]
        public async Task<IActionResult> GetAdmissionDetailByVisitId(Guid? VisitId)
        {
            var list = await _PatientAdmissionDetailService.GetAdmissionDetailByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

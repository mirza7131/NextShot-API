using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedRequisitionDto;
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
    public class MedicineAdvisedRequisitionController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<MedicineAdvisedRequisitionController> _logger;
        private readonly TokenService _tokenService;
        private readonly MedicineAdvisedRequisitionService<MedicineAdvisedRequisition> _MedicineAdvisedRequisitionService;

        #endregion

        #region Constructor

        public MedicineAdvisedRequisitionController(ILogger<MedicineAdvisedRequisitionController> logger, TokenService tokenService, MedicineAdvisedRequisitionService<MedicineAdvisedRequisition> MedicineAdvisedRequisitionService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _MedicineAdvisedRequisitionService = MedicineAdvisedRequisitionService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            var obj = await _MedicineAdvisedRequisitionService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditWithPrescription")]
        public async Task<IActionResult> CreateOrEditWithPrescription(CreateOrEditMedicineAdvisedRequisitionDto input)
        {
            var obj = await _MedicineAdvisedRequisitionService.CreateOrEditWithPrescription(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpGet]
        [Route("UpdatePatientMedicineStatus")]
        public async Task<IActionResult> UpdatePatientMedicineStatus(Guid PatientPrescriptionId, string Status)
        {
            await _MedicineAdvisedRequisitionService.UpdatePatientMedicineStatus(PatientPrescriptionId, Status);
            return Ok(new ResponseSuccess {});
        }

        [HttpGet]
        [Route("UpdatePatientRequisitionStatus")]
        public async Task<IActionResult> UpdatePatientRequisitionStatus(Guid MedicineAdvisedRequisitionId, int Status)
        {
            await _MedicineAdvisedRequisitionService.UpdatePatientRequisitionStatus(MedicineAdvisedRequisitionId, Status);
            return Ok(new ResponseSuccess { });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _MedicineAdvisedRequisitionService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _MedicineAdvisedRequisitionService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        } 
        
        [HttpGet]
        [Route("GetDespencedMedicine")]
        public async Task<IActionResult> GetDespencedMedicine(Guid? MedicineAdvisedRequisitionId)
        {
            var response = await _MedicineAdvisedRequisitionService.GetDespencedMedicine(MedicineAdvisedRequisitionId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterMedicineAdvisedRequisitionDto filter)
        {
            var response = await _MedicineAdvisedRequisitionService.GetAllWithPagination(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _MedicineAdvisedRequisitionService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

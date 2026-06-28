using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
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
    public class MedicineDispatchController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientDiagnoseController> _logger;
        private readonly TokenService _tokenService;
        private readonly MedicineDispatchService<MedicineDispatch> _MedicineDispatchService;
        private readonly MIMSService _mimsService;
        #endregion

        #region Constructor

        public MedicineDispatchController(
            ILogger<PatientDiagnoseController> logger, 
            TokenService tokenService, 
            MedicineDispatchService<MedicineDispatch> MedicineDispatchService,
            MIMSService mimsService
        )
        {
            _logger = logger;
            _tokenService = tokenService;
            _MedicineDispatchService = MedicineDispatchService;
            _mimsService = mimsService;
        }

        #endregion

        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(List<CreateOrEditMedicineDispatchDto> input)
        //{
        //    var obj = await _PharmacyService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        [HttpPost]
        [Route("CreatePatientDispatch")]
        public async Task<IActionResult> CreatePatientDispatch(CreateOrEditPatientMedicineDispatchDto input)
        {
            var obj = await _MedicineDispatchService.CreatePatientDispatch(input);
            return Ok(new ResponseSave { data = obj });
        }

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(int Id)
        //{
        //    var obj = await _MedicineLookupService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _MedicineLookupService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(int Id)
        //{
        //    var obj = await _MedicineLookupService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        [HttpGet]
        [Route("GetAllPharmacyList")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterPatientVitalDto filter)
        {
            var list = await _MedicineDispatchService.GetAllWithPagination(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllQue")]
        public async Task<IActionResult> GetAllQue(int HealthFacilityId)
        {
            var list = await _MedicineDispatchService.GetAllQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacySlipByVisitId")]
        public async Task<IActionResult> GetPharmacySlipByVisitId(Guid VisitId)
        {
            var list = await _MedicineDispatchService.GetPharmacySlipByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Helper Methods


        #endregion

        #region IPD

        [HttpGet]
        [Route("GetAllIpdQue")]
        public async Task<IActionResult> GetAllIpdQue(int HealthFacilityId)
        {
            var list = await _MedicineDispatchService.GetAllIpdQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion
    }
}

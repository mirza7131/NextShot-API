using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Repositories.UOW;
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
    public class PatientPrescriptionController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientPrescriptionController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientPrescriptionService<PatientPrescription> _PatientPrescriptionService;
        

        #endregion

        #region Constructor

        public PatientPrescriptionController(ILogger<PatientPrescriptionController> logger, TokenService tokenService, PatientPrescriptionService<PatientPrescription> PatientPrescriptionService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientPrescriptionService = PatientPrescriptionService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientPrescriptionDto input)
        {
            var obj = await _PatientPrescriptionService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientPrescriptionService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("CheckIfDispensed")]
        public async Task<IActionResult> CheckIfDispensed(Guid Id)
        {
            var obj = await _PatientPrescriptionService.CheckIfDispensed(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("CheckIfCanDeleteLab")]
        public async Task<IActionResult> CheckIfCanDeleteLab(Guid Id)
        {
            var obj = await _PatientPrescriptionService.CheckIfCanDeleteLab(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _PatientPrescriptionService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientPrescriptionService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDocument;
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
    public class PatientDocumentController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly PatientDocumentService<PatientDocument> _patientDocumentService;
        #endregion

        #region Constructor

        public PatientDocumentController(TokenService tokenService,
            PatientDocumentService<PatientDocument> PatientDocumentService
            )
        {
            _tokenService = tokenService;
            _patientDocumentService = PatientDocumentService;
        }

        #endregion


        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDocumentDto input)
        //{
        //    var obj = await _patientDocumentService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _patientDocumentService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(Guid Id)
        //{
        //    var obj = await _patientDocumentService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        [HttpGet]
        [Route("GetByVisitId")]
        public async Task<IActionResult> GetByVisitId(Guid VisitId)
        {
            var obj = await _patientDocumentService.GetByVisitId(VisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _patientDocumentService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetAllWithPagination")]
        //public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterPatientDocumentDto filterPatientDocumentSterilizationRecordDto)
        //{
        //    var response = await _PatientDocumentService.GetAllWithPagination(filterPatientDocumentSterilizationRecordDto);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        #endregion
    }
}

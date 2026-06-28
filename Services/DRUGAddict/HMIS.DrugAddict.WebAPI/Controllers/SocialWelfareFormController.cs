
using Azure;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.FilterDto;
using HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Models.Dto.TestDto;
using HMIS.DrugAddict.Domain.Models.DTO.SocialWelfareFormDto;
using HMIS.DrugAddict.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SocialWelfareFormController : ControllerBase
    {
        #region Class Fields & Propertities

        //private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly SocialWelfareFormService<SocialWelfareForm> _socialWelfareFormService;

        #endregion

        #region Constructor
        //ILogger<AuthenticationController> logger
        public SocialWelfareFormController(TokenService tokenService, SocialWelfareFormService<SocialWelfareForm> socialWelfareFormService)
        {
            // _logger = logger;
            _tokenService = tokenService;
            _socialWelfareFormService = socialWelfareFormService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditSocialWelfareFormDto input)
        {
            var obj = await _socialWelfareFormService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _socialWelfareFormService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("AssignDoctorToDrugAddictPatient")]
        public async Task<IActionResult> AssignDoctorToDrugAddictPatient([FromBody] SocaialWelfareAssignDoctorDto assignDoctor)
        {
            try
            {
                var obj = await _socialWelfareFormService.AssignDoctorToPatient(assignDoctor);
                return Ok(new ResponseSave { data=obj });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _socialWelfareFormService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterSocialWelfareFormDto socialWelfareDto)
        {
            var response = await _socialWelfareFormService.GetAllWithPagination(socialWelfareDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _socialWelfareFormService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }
        [HttpGet]
        [Route("GetPatientVisitClose")]
        public async Task<IActionResult> CLosedPatientVisit(Guid Id)
        {
            var obj = await _socialWelfareFormService.GetPatientsVisitClosed(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet("GetPatientByPatientVisitId")]
        public async Task<IActionResult> GetPatientsByPatientVisitId([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientVisitList = await _socialWelfareFormService.GetPatientsByPatientVisitId(filterDataObj);
            return Ok(new ResponseSuccess { data = patientVisitList });
        }

        [HttpGet]
        [Route("GetPatientsByTheirDistrict")]
        public async Task<IActionResult> GetPatientsByTheirDistrict([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
                var patientListByDistrictId = await _socialWelfareFormService.GetPatientsByTheirDistrict(filterDataObj);
                return Ok(new ResponseSuccess { data = patientListByDistrictId });
        }
        [HttpGet]
        [Route("GetUnassignPatientsByTheirDistrict")]
        public async Task<IActionResult> GetUnassignPatientsByTheirDistrict([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientListByDistrictId = await _socialWelfareFormService.GetUnassignPatientsByTheirDistrict(filterDataObj);
            return Ok(new ResponseSuccess { data = patientListByDistrictId });
        }

        [HttpGet]
        [Route("GetDrugAddictDoctorsByTheirDistrict")]
        public async Task<IActionResult> GetDrugAddictDoctorsByTheirDistrict(int DistrictID)
        {
                var doctorsListByDistrictId = await _socialWelfareFormService.GetDrugAddictDoctorsByTheirDistrict(DistrictID);
                return Ok(new ResponseSuccess { data = doctorsListByDistrictId });
        }

        [HttpGet]
        [Route("GetAllPatientsByDistrictName")]
        public async Task<IActionResult> GetAllPatientsByDistrictName([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var list = await _socialWelfareFormService.GetAllPatientsByDistrictName(filterDataObj);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("PatientIsDrugAddictOrNot")]
        public async Task<IActionResult> PatientIsDrugAddictOrNot([FromQuery] Guid patientOpenVisitId)
        {
            bool isPatientDrugAddict = await _socialWelfareFormService.PatientIsDrugAddictOrNot(patientOpenVisitId);
            return Ok(new ResponseSuccess { data = isPatientDrugAddict });
        }

        [HttpGet]
        [Route("GetAllPatientsCount")]

        public async Task<IActionResult> GetAllPatientsCount([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsCount(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }

        [HttpGet]
        [Route("GetAllPatientsCountCd")]
        public async Task<IActionResult> GetAllPatientsCountCd([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsCountCd(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }

        [HttpGet]
        [Route("GetAllPatientsCountRehabilitationCd")]
        public async Task<IActionResult> GetAllPatientsCountRehabilitationCd([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsCountRehabilitationCd(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }
        [HttpGet]
        [Route("GetAllPatientRehablitation")]
        public async Task<IActionResult> GetAllPatientRehablitation([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var ptRehabList=await _socialWelfareFormService.GetAllPatientRehablitation(filterDataObj);
            return Ok(new ResponseSuccess { data = ptRehabList });
        }

        [HttpGet]
        [Route("GetAllPatientsCountRelapseCd")]
        public async Task<IActionResult> GetAllPatientsCountRelapseCd([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsCountRelapseCd(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }
        [HttpGet]
        [Route("GetAllPatientsRelapse")]
        public async Task<IActionResult> GetAllPatientsRelapse([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsRelapse(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }
        [HttpGet]
        [Route("GetAllPatientsExpire")]
        public async Task<IActionResult> GetAllPatientsExpire([FromQuery] GetAllPatientCountFilterDto? filterDataObj) 
        {
            var ptExpireList = await _socialWelfareFormService.GetAllPatientsExpire(filterDataObj);
            return Ok(new ResponseSuccess { data = ptExpireList });
        }

        [HttpGet]
        [Route("GetAllPatientsCountExpireCd")]
        public async Task<IActionResult> GetAllPatientsCountExpireCd([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patientCountByDistrict = await _socialWelfareFormService.GetAllPatientsCountExpireCd(filterDataObj);
            return Ok(new ResponseSuccess { data = patientCountByDistrict });
        }

        [HttpGet]
        [Route("GetAllPatientsByDistrictNameCDSec")]
        public async Task<IActionResult> GetAllPatientsByDistrictNameCDSec([FromQuery] GetAllPatientCountFilterDto? filterDataObj)
        {
            var patients = await _socialWelfareFormService.GetAllPatientsByDistrictNameCDSec(filterDataObj);
            return Ok(new ResponseSuccess { data= patients });
        }

        [HttpGet]
        [Route("GetSinglePatientsVisitCD")]
        public async Task<IActionResult> GetSinglePatientsVisitCD([FromQuery] Guid patientVisitId)
        {
            var ptSessionList=await _socialWelfareFormService.GetSinglePatientsVisitCD(patientVisitId);
            return Ok(new ResponseSuccess { data=ptSessionList });  
        }

        [HttpGet]
        [Route("CheckPatientIsInTheDiaganoseORNot")]
        public async Task<IActionResult> CheckPatientIsInTheDiaganoseORNot([FromQuery] Guid patientVisitId)
        {
            var check = await _socialWelfareFormService.CheckPatientIsInTheDiaganoseORNot(patientVisitId);
            return Ok(new ResponseSuccess { data =check });
        }

        //[HttpGet]
        //[Route("GetRecordsByDistrictOrDivision")]
        //public async Task<IActionResult> GetRecordsByDistrictOrDivision(int districtid,int divisionid)
        //{
        //    var res = await _socialWelfareFormService.GetRecordsByDistrictOrDivision(districtid,divisionid);
        //    return Ok(res);
        //}
        #endregion

        #region Helper Methods


        #endregion
    }
}

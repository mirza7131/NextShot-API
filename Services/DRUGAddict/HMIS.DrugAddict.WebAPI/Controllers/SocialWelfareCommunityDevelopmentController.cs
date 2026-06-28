using CommonDTOs.ResponseDTO;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto;
using HMIS.DrugAddict.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMIS.DrugAddict.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SocialWelfareCommunityDevelopmentController : ControllerBase
    {
        #region class Fields and Properties
        private readonly TokenService _tokenService;
        private readonly SocialWelfareCommunityDevelopmentService<SocialWelfareTaskPerformedByCd> _socialWelfareFormService;
        #endregion

        #region Constructor
        public SocialWelfareCommunityDevelopmentController(TokenService tokenService, SocialWelfareCommunityDevelopmentService<SocialWelfareTaskPerformedByCd> socialWelfareFormService)
        {
            // _logger = logger;
            _tokenService = tokenService;
            _socialWelfareFormService = socialWelfareFormService;
        }
        #endregion

        #region CU Operation
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditSocialWelfareTaskPerformedByCDDto entity)
        {
            var obj=await _socialWelfareFormService.CreateOrEdit(entity);
            return Ok(new ResponseSave { data = obj });
        }
        #endregion

        #region Read Operation
        [HttpGet]
        [Route("GetAll")] 
        public async Task<IActionResult> GetAll()
        {
            var list = await _socialWelfareFormService.GetAll();
            return Ok(new ResponseSuccess { data = list});
        }
        [HttpGet]
        [Route("GetSingleFieldOfficerPatient")]
        public async Task<IActionResult> GetSingleFieldOfficerPatient([FromQuery] Guid DoctorId)
        {
            var obj=await _socialWelfareFormService.GetSingleFieldOfficerPatient(DoctorId);
            return Ok(new ResponseSuccess { data = obj});
        }

        [HttpGet]
        [Route("GetSinglePatientCD")]
        public async Task<IActionResult> GetSocialWelfareDoctorPatientList([FromQuery] Guid PatientVisitId)
        {
            var obj = await _socialWelfareFormService.GetSinglePatientCD(PatientVisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetPatientByPatientVisitId")]
        public async Task<IActionResult> GetPatientByPatientVisitId([FromQuery] Guid PatientId)
        {
            var res = await _socialWelfareFormService.GetPatientByPatientVisitId(PatientId);
            return Ok(new ResponseSuccess { data = res });
        }

        [HttpPost]
        [Route("PatientsVisitClosed")]
        public async Task<IActionResult> PatientsVisitClosed([FromQuery] Guid PatientVisitid, string? patientStatus)
        {
            var res = await _socialWelfareFormService.PatientsVisitClosed(PatientVisitid,patientStatus);
            return Ok(new ResponseSuccess { data = res });
        }
        #endregion
    }
}

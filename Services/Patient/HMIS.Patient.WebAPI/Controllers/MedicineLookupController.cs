using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineData;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Service;

using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimsMedicineDataService = HMIS.Patient.Service.MimsMedicineDataService;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicineLookupController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientDiagnoseController> _logger;
        private readonly TokenService _tokenService;
        private readonly MedicineLookupService<MedicineLookup> _MedicineLookupService;
        private readonly MIMSService _mimsService;
        private readonly MimsMedicineDataService _mimsMedicineDataService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly bool _isActiveOffline;
        #endregion

        #region Constructor

        public MedicineLookupController(
            ILogger<PatientDiagnoseController> logger,
            TokenService tokenService,
            MedicineLookupService<MedicineLookup> MedicineLookupService,
            MimsMedicineDataService mimsMedicineDataService,
            MIMSService mimsService,
            IConfiguration config
        )
        {
            _logger = logger;
            _tokenService = tokenService;
            _MedicineLookupService = MedicineLookupService;
            _mimsService = mimsService;
            _mimsMedicineDataService = mimsMedicineDataService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;

            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                           config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditMedicineLookupDto input)
        {
            var obj = await _MedicineLookupService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _MedicineLookupService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _MedicineLookupService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _MedicineLookupService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region MIMS Operations

        [HttpGet]
        [Route("GetAvailableMedicine")]
        public async Task<IActionResult> GetAvailableMedicine()
        {
            //var list = await _mimsService.GetMedicineAvailableQuantityByHealthFacility(TokenService.GetUserHfCode());
            if (_isActiveOffline)
            {
                var list = await _mimsMedicineDataService.GetAll();
                return Ok(new ResponseSuccess { data = list });
            }
            else
            {
                var list = await _mimsService.GetMedicineAvailableQuantityByHealthFacility(_mimsBaseUrl, TokenService.GetHfHrId(), TokenService.GetMimsDepartmentId());
                return Ok(new ResponseSuccess { data = list });
            }
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

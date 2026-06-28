using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.MimsGetMedicineResponse;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineData;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineIndentLog;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MimsMedicineDataController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly MimsMedicineDataService _mimsMedicineDataService;
        private readonly MIMSService _mimsService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public MimsMedicineDataController(
            TokenService tokenService,
            MimsMedicineDataService mimsMedicineDataService,
             MIMSService mimsService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mimsMedicineDataService = mimsMedicineDataService;
            _mimsService = mimsService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditMimsMedicineDataDto input)
        {
            var obj = await _mimsMedicineDataService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }


        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _mimsMedicineDataService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        [HttpPost]
        [Route("ImportDataFromMims")]
        public async Task<IActionResult> ImportDataFromMims()
        {
            await _mimsMedicineDataService.ImportDataFromMims();
            return Ok(new ResponseSave());
        }

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _mimsMedicineDataService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(int Id)
        //{
        //    var obj = await _mimsMedicineDataService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        [HttpGet]
        [Route("GetMedicineIndent")]
        public async Task<IActionResult> GetMedicineIndent([FromQuery] FilterMimsMedicineDataDto filter)
        {

            var list = await _mimsMedicineDataService.GetMedicineIndent(filter);
            return Ok(new ResponseSuccess { data = list });

        }

        [HttpGet]
        [Route("GetUnSyncMedicineIndentFromMims")]
        public async Task<IActionResult> GetUnSyncMedicineIndentFromMims()
        {

            var list = await _mimsService.GetUnSyncMedicineIndentFromMims(_mimsBaseUrl, TokenService.GetHfHrId(), Convert.ToInt32(TokenService.GetMimsDepartmentId()));
            return Ok(new ResponseSuccess { data = list });

        }

        [HttpPost]
        [Route("CreateOrEditMimsMedicineIndentLog")]
        public async Task<IActionResult> CreateOrEditMimsMedicineIndentLog(CreateOrEditMimsMedicineIndentLogDto input)
        {
            var obj = await _mimsMedicineDataService.CreateOrEditMimsMedicineIndentLog(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateIndentStatus")]
        public async Task<IActionResult> UpdateIndentStatus(CreateOrEditMimsMedicineIndentLogDto input)
        {
            var obj = await _mimsMedicineDataService.UpdateIndentStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("SyncronizeMimsWithHmis")]
        public async Task<IActionResult> SyncronizeMimsWithHmis()
        {
            var obj = await _mimsMedicineDataService.SyncronizeMimsWithHmis();
            return Ok(new ResponseSave { data = obj });
        }


        [HttpPost]
        [Route("SyncronizeAnIndentFromMimsInHmis")]
        public async Task<IActionResult> SyncronizeAnIndentFromMimsInHmis(CreateOrEditMimsMedicineIndentLogDto input)
        {
            var obj = await _mimsMedicineDataService.SyncronizeAnIndentFromMimsInHmis(input.MimsIndentId??0);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

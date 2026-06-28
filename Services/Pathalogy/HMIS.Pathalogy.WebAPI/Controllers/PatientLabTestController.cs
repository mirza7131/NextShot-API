using CommonDTOs.ResponseDTO;
using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Pathalogy.Domain.Models.DTO.UpdateLabTestResultBulkDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewAnmonalListDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewRiderLabTestListDto;
using HMIS.Pathalogy.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HMIS.Pathalogy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PatientLabTestController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientLabTestController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientLabTestService<PatientLabTest> _PatientLabTestService;

        #endregion

        #region Constructor

        public PatientLabTestController(ILogger<PatientLabTestController> logger, TokenService tokenService, PatientLabTestService<PatientLabTest> PatientLabTestService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientLabTestService = PatientLabTestService;
        }

        #endregion

        #region CUD Operations

        //[HttpPost]
        //[Route("CreateOrEdit")]
        //public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientLabTestDto input)
        //{
        //    var obj = await _PatientLabTestService.CreateOrEdit(input);
        //    return Ok(new ResponseSave { data = obj });
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(Guid Id)
        //{
        //    var obj = await _PatientLabTestService.Delete(Id);
        //    return Ok(new ResponseDelete { data = obj });
        //}

        [HttpPost]
        [Route("UploadResultImage")]
        public async Task<IActionResult> UploadResultImage(UploadResultImageDto input)
        {
            var obj = await _PatientLabTestService.UploadResultImage(input);
            return Ok(new ResponseSave { data = obj });
        }


        [HttpPost]
        [Route("UpdateRider")]
        public async Task<IActionResult> UpdateRider(AssignRiderDto input)
        {
            var obj = await _PatientLabTestService.UpdateRider(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateLabTestResult")]
        public async Task<IActionResult> UpdateLabTestResult(UpdateLabTestResultDto input)
        {
            var obj = await _PatientLabTestService.UpdateTestResult(input);
            return Ok(new ResponseSave { data = obj });
        }
        // Created This Api for Bulk Upload Results Against Barcode
        [HttpPost]
        [Route("UpdateLabTestResultBulk")]
        public async Task<IActionResult> UpdateLabTestResultBulk(List<TestListDto> input)
        {
            var obj = await _PatientLabTestService.UpdateTestResultBulk(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateSampleCollectionStatus")]
        public async Task<IActionResult> UpdateSampleCollectionStatus(UpdateSampleCollectionStatusDto input)
        {
            var obj = await _PatientLabTestService.UpdateSampleCollectionStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        //public async Task<IActionResult> UpdateSampleCollectionStatus(Guid PatientLabTestId, string? SampleTransportMode, bool IsExternal = false)
        //{
        //    var obj = await _PatientLabTestService.UpdateSampleCollectionStatus(PatientLabTestId, SampleTransportMode, IsExternal);
        //    return Ok(new ResponseSave { data = obj });
        //}

        [HttpPost]
        [Route("UpdateSampleRejectedStatus")]
        public async Task<IActionResult> UpdateSampleRejectedStatus(UpdateLabSampleRejectedReasonDto input)
        {
            var obj = await _PatientLabTestService.UpdateSampleRejectedStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("MarkAsArchived")]
        public async Task<IActionResult> MarkAsArchived(Guid PatientLabTestId)
        {
            var obj = await _PatientLabTestService.MarkAsArchived(PatientLabTestId);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateRiderLabTestStatus")]
        public async Task<IActionResult> UpdateRiderLabTestStatus(UpdateRiderLabTestStatusDto input)
        {
            var obj = await _PatientLabTestService.UpdateRiderLabTestStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateStatusOfReportOfConsignmentLabTest")]
        public async Task<IActionResult> UpdateStatusOfReportOfConsignmentLabTest(UpdateConsignmentTestReportStatus input)
        {
            var obj = await _PatientLabTestService.UpdateStatusOfReportOfConsignmentLabTest(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateAnmonalLabTestPayementStatus")]
        public async Task<IActionResult> UpdateAnmonalLabTestPayementStatus(List<ViewAnmonalTestDetail> anmonalLabTestList)
        {
            var obj = await _PatientLabTestService.UpdateAnmonalLabTestPayementStatus(anmonalLabTestList);
            return Ok(new ResponseSuccess { data = obj });
        }
        [HttpPost]
        [Route("UpdateAnmonalDentalProcedurePayementStatus")]
        public async Task<IActionResult> UpdateAnmonalDentalProcedurePayementStatus(List<ViewAnmonalProcedurePaymentList> anmonalProcedureList)
        {
            var obj = await _PatientLabTestService.UpdateAnmonalDentalProcedurePayementStatus(anmonalProcedureList);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var response = await _PatientLabTestService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = response });
        //}

        #region Anmonal

        [HttpGet]
        [Route("GetAlmonerStat")]
        public async Task<IActionResult> GetAlmonerStat([FromQuery] FilterAnmonarDto filter)
        {
            var response = await _PatientLabTestService.GetAlmonerStat(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetFilteredAnmonalListWithPagination")]
        public async Task<IActionResult> GetFilteredAnmonalListWithPagination([FromQuery] FilterAnmonarDto filter)
        {
            var response = await _PatientLabTestService.GetFilteredAnmonalListWithPagination(filter);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetFilteredAnmonalProcedureListWithPagination")]
        public async Task<IActionResult> GetFilteredAnmonalProcedureListWithPagination([FromQuery] FilterProcedureDto filter)
        {
            var response = await _PatientLabTestService.GetFilteredAnmonalProcedureListWithPagination(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetFilteredViewAnmonalProcedurePaymentList")]
        public async Task<IActionResult> GetFilteredViewAnmonalProcedurePaymentList([FromQuery] FilterProcedureDto filter)
        {
            var response = await _PatientLabTestService.GetFilteredViewAnmonalProcedurePaymentList(filter);
            return Ok(new ResponseSuccess { data = response });
        }



        [HttpGet]
        [Route("GeAnmonalDashboardListWithPagination")]
        public async Task<IActionResult> GeAnmonalDashboardListWithPagination([FromQuery] FilterAnmonarDto filter)
        {
            var response = await _PatientLabTestService.GeAnmonalDashboardListWithPagination(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetFilteredAnmonalLabTestDetailByVisitId")]
        public async Task<IActionResult> GetFilteredAnmonalLabTestDetailByVisitId(Guid PatientVisitId, int ListType)
        {
            var response = await _PatientLabTestService.GetFilteredAnmonalLabTestDetailByVisitId(PatientVisitId, ListType);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetFilteredAnmonalPaidLabTestDetailByVisitId")]
        public async Task<IActionResult> GetFilteredAnmonalPaidLabTestDetailByVisitId(Guid PatientVisitId, int ListType)
        {
            var response = await _PatientLabTestService.GetFilteredAnmonalPaidLabTestDetailByVisitId(PatientVisitId, ListType);
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion Anmonal

        [HttpGet]
        [Route("GetAllTestListByVisitId")]
        public async Task<IActionResult> GetAllTestListByVisitId(Guid PatientVisitId)
        {
            var response = await _PatientLabTestService.GetAllTestListByVisitId(PatientVisitId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetTbPatientLabTestByPatientId")]
        public async Task<IActionResult> GetTbPatientLabTestByPatientId(Guid PatientId)
        { 
            var response = await _PatientLabTestService.GetTbPatientLabTestByPatientId(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllPatientLabTestByFilters")]
        public async Task<IActionResult> GetAllPatientLabTestByFilters([FromQuery] FilterViewPatientLabTestListDto filter)
        {
            var response = await _PatientLabTestService.GetAllPatientLabTestByFilters(filter);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("getAllPatientTestListForBatch")]
        public async Task<IActionResult> getAllPatientTestListForBatch([FromQuery] FilterViewPatientLabTestListDto filter)
        {
            var response = await _PatientLabTestService.getAllPatientTestListForBatch(filter);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetCollectedSamplespatientTestList")]
        public async Task<IActionResult> GetCollectedSamplespatientTestList([FromQuery] FilterViewPatientLabTestListDto filter)
        {
            var response = await _PatientLabTestService.GetCollectedSamplespatientTestList(filter);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetRecentBatchName")]
        public async Task<IActionResult> GetRecentBatchName()
        {
            var response = await _PatientLabTestService.GetRecentBatchName();
            return Ok(new ResponseSuccess { data = response });
        }
        
        
        [HttpGet]
        [Route("GetAllPatientLabTestByVisit")]
        public async Task<IActionResult> GetAllPatientLabTestByVisit([FromQuery] PatientLabTestByVisitDTO patientHistoryLabTestDTO)
        {
            var response = await _PatientLabTestService.GetAllPatientLabTestByVisit(patientHistoryLabTestDTO);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetAllRiderTestList")]
        public async Task<IActionResult> GetAllRiderTestList([FromQuery] RiderPatientLabTestListDto filter)
        {
            var response = await _PatientLabTestService.GetAllRiderTestList(filter);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetByPatientLabTestIdForRegistrationSlip")]
        public async Task<IActionResult> GetByPatientLabTestIdForRegistrationSlip(Guid PatientLabTestId)
        {
            var response = await _PatientLabTestService.GetByPatientLabTestIdForRegistrationSlip(PatientLabTestId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [AllowAnonymous]

        [Route("GetAllPatientLabTestDetailByPatientLabTestId")]
        public async Task<IActionResult> GetAllPatientLabTestDetailByPatientLabTestId(Guid PatientLabTestId)
        {
            var response = await _PatientLabTestService.GetAllPatientLabTestDetailByPatientLabTestId(PatientLabTestId);
            return Ok(new ResponseSuccess { data = response });
        }
        // This API is to get HCP Patient Recommended test
        [HttpGet]
        [Route("GetRecommendedLabTestToPatient")]
        public async Task<IActionResult> GetRecommendedLabTestToPatient(Guid PatientId)
        {
            var response = await _PatientLabTestService.GetRecommendedLabTestToPatient(PatientId);
            return Ok(new ResponseSuccess { data = response });
        }
        // This API is to get HCP Patient Recommended test
        [HttpGet]
        [Route("GetRecommendedLabTestsList")]
        public async Task<IActionResult> GetRecommendedLabTestsList()
        {
            var response = await _PatientLabTestService.GetRecommendedLabTestsList();
            return Ok(new ResponseSuccess { data = response });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}

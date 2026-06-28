using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Services;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.DashboardDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Patient.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class DashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly DashboardService _dashboardService;
        private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;
        private readonly BASService _BASService;
        private readonly string _BASBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public DashboardController(TokenService tokenService, DashboardService DashboardService, PatientDiagnoseService<PatientDiagnose> PatientDiagnoseService, BASService _bASService, IConfiguration config)
        {
            _tokenService = tokenService;
            _dashboardService = DashboardService;
            _PatientDiagnoseService = PatientDiagnoseService;
            
            _BASService = _bASService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _BASBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("BAS").Value ?? string.Empty;
            else
                _BASBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("BAS").Value ?? string.Empty;

        }

        #endregion

        #region Data Sync Utility Dashboard

        [HttpGet]
        [Route("getDataSyncUtilityLog")]
        public async Task<IActionResult> getDataSyncUtilityLog([FromQuery] SyncUtilityDashboardFilter filter)
        {
            var list = await _dashboardService.getDataSyncUtilityLog(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Patient Registration Dashboard New  
        [HttpGet] 
        [Route("getRegistrationDashboardLoginUserAllCounts")] 
        public async Task<IActionResult> getRegistrationDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        { 
            var list = await _dashboardService.getRegistrationDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list }); 
        } 

        [HttpGet]
        [Route("getRegistrationDashboardAllCounts")]
        public async Task<IActionResult> getRegistrationDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getRegistrationDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getRegistrationDashboardAllList")]
        public async Task<IActionResult> getRegistrationDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getRegistrationDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getRegDashboardOPDSectionWiseTokenCount")]
        public async Task<IActionResult> getRegDashboardOPDSectionWiseTokenCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getRegDashboardOPDSectionWiseTokenCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region HR Health Dashboard Counts And Listing  
        [HttpGet]
        [Route("getHRHealthDashboardCounts")]
        public async Task<IActionResult> getHRHealthDashboardCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHRHealthDashboardCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getHRSectionWiseTotalCounts")]
        public async Task<IActionResult> getHRSectionWiseTotalCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHRSectionWiseTotalCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getHRHealthDashboardListing")]
        public async Task<IActionResult> getHRHealthDashboardListing([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHRHealthDashboardListing(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Patient Registration Dashboard

        [HttpGet]
        [Route("GetPatientVisitCountByHealthFacilityByPMIS")]
        public async Task<IActionResult> GetPatientVisitCountByHealthFacilityByPMIS([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByHealthFacilityByPMIS(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByHealthFacility")]
        public async Task<IActionResult> GetPatientVisitCountByHealthFacility([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByHealthFacility(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByUser")]
        public async Task<IActionResult> GetPatientVisitCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByProvince")]
        public async Task<IActionResult> GetPatientVisitCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientRegisteredCountByProvince")]
        public async Task<IActionResult> GetPatientRegisteredCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientRegisteredCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByProvinceByGender")]
        public async Task<IActionResult> GetPatientVisitCountByProvinceByGender([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByProvinceByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountSelfVsOthers")]
        public async Task<IActionResult> GetPatientVisitCountSelfVsOthers([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountSelfVsOthers(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientNewRegisteredVsRevisit")]
        public async Task<IActionResult> GetPatientNewRegisteredVsRevisit([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientNewRegisteredVsRevisit(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByGenderBySelfVsOthers")]
        public async Task<IActionResult> GetPatientVisitCountByGenderBySelfVsOthers([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByGenderBySelfVsOthers(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByAgeRangeByGender")]
        public async Task<IActionResult> GetPatientVisitCountByAgeRangeByGender([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByAgeRangeByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByDeptBySecByMonth")]
        public async Task<IActionResult> GetPatientVisitCountByDeptBySecByMonth([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitCountByDeptBySecByMonth(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTodayPatientVisitCountByDeptBySec")]
        public async Task<IActionResult> GetPatientVisitCountByDeptBySecByDate([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetTodayPatientVisitCountByDeptBySec(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        //New Dashboard Tiles
        [HttpGet]
        [Route("GetRegistrationDashboardCardCount")]
        public async Task<IActionResult> GetRegistrationDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var res = await _dashboardService.GetRegistrationDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = res });
        }

        #endregion

        #region Doctor Dashboard
        [HttpGet]
        [Route("getDoctorDashboardLoginUserAllCounts")]
        public async Task<IActionResult> getDoctorDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        
        [HttpGet]
        [Route("getDoctorDashboardPatientAllCounts")]
        public async Task<IActionResult> getDoctorDashboardPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHFPatientAllCounts")]
        public async Task<IActionResult> getDoctorDashboardHFPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardHFPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardPatientAllList")]
        public async Task<IActionResult> getDoctorDashboardPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHFPatientAllList")]
        public async Task<IActionResult> getDoctorDashboardHFPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardHFPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHfPatientCount")]
        public async Task<IActionResult> getDoctorDashboardHfPatientCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDoctorDashboardHfPatientCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByUser")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitDoctorCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByProvince")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitDoctorCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByGender")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitDoctorCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByDisease")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByDisease([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitDoctorCountByDisease(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetDoctorPrescribedMedicineAccumulateQuantity")]
        public async Task<IActionResult> GetDoctorPrescribedMedicineAccumulateQuantity([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetDoctorPrescribedMedicineAccumulateQuantity(filter);
            return Ok(new ResponseSuccess { data = list });
        }

       

        [HttpGet]
        [Route("GetDoctorRecommendedLabCount")]
        public async Task<IActionResult> GetDoctorRecommendedLabCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetDoctorRecommendedLabCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        // Labs counts Internal and External
        [HttpGet]
        [Route("getInternalAndExternalLabCounts")]
        public async Task<IActionResult> getInternalAndExternalLabCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getInternalAndExternalLabCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetDoctorDashboardCardCount")]
        public async Task<IActionResult> GetDoctorDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetDoctorDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetAllQueList")]
        public async Task<IActionResult> GetAllQueList([FromQuery] FilterPatientVisitDto filter)
        {
            var list = await _dashboardService.GetAllpatientInQue(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        //[HttpGet]
        //[Route("GetAllQueList")]
        //public async Task<IActionResult> GetAllQueList([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        //{
        //    var response = await _PatientOpenVisitService.GetPatientVisitsListWithDetail(filterPatientVisitDto);

        //    return Ok(new ResponseSuccess { data = response });
        //    //return Ok(new ResponsePaginatedDTO { data = response.List, pageCount = response.TotalPages, totalRecords = response.TotalCount });
        //}

        #endregion

        #region Vital Login USer Counts
        [HttpGet]
        [Route("getVitalDashboardLoginUserAllCounts")]
        public async Task<IActionResult> getVitalDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getVitalDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Patient Vital Dashboard New  
        [HttpGet]
        [Route("getVitalDashboardAllCounts")]
        public async Task<IActionResult> getVitalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getVitalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getVitalDashboardAllList")]
        public async Task<IActionResult> getVitalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getVitalDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        #endregion
        #region Get Patient Details
        [HttpGet]
        [Route("getPatientDetail")]
        public async Task<IActionResult> getPatientDetail(Guid PatientVisitId)
        {
            var list = await _dashboardService.getPatientDetail(PatientVisitId);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Vital Dashboard

        [HttpGet]
        [Route("GetVitalDashboardCardCount")]
        public async Task<IActionResult> GetVitalDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetVitalDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByUser")]
        public async Task<IActionResult> GetPatientVisitVitalCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByProvince")]
        public async Task<IActionResult> GetPatientVisitVitalCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByGender")]
        public async Task<IActionResult> GetPatientVisitVitalCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByWeightRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByWeightRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByWeightRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetPatientVisitVitalCountByRespiratoryRateRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByRespiratoryRateRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByRespiratoryRateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByTemperatureRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByTemperatureRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByTemperatureRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByPulseRateRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByPulseRateRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByPulseRateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByBloodPressureRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByBloodPressureRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitVitalCountByBloodPressureRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pharmacy New
        [HttpGet]
        [Route("getPharmacyDashboardLoginUserAllCounts")]
        public async Task<IActionResult> getPharmacyDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPharmacyDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPharmacyDashboardAllCounts")]
        public async Task<IActionResult> getPharmacyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPharmacyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPharmacyDashboardAllList")]
        public async Task<IActionResult> getPharmacyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPharmacyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardMedcineIssuedReport")]
        public async Task<IActionResult> GetPharmacyDashboardMedcineIssuedReport([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPharmacyDashboardMedcineIssuedReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPharmacyDashboardMedcineIssuedPatientDetailList")]
        public async Task<IActionResult> getPharmacyDashboardMedcineIssuedPatientDetailList([FromQuery] MedicineIssuedFilter filter)
        {
            var list = await _dashboardService.getPharmacyDashboardMedcineIssuedPatientDetailList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPharmacyDashboardOflineMedicineStock")]
        public async Task<IActionResult> getPharmacyDashboardOflineMedicineStock([FromQuery] MedicineIssuedFilter filter)
        {
            var list = await _dashboardService.getPharmacyDashboardOflineMedicineStock(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Pharmacy old
        [HttpGet]
        [Route("GetPatientVisitPharmacyCountByUser")]
        public async Task<IActionResult> GetPatientVisitPharmacyCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitPharmacyCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyInternalExternalMedicineAccumulateQuantity")]
        public async Task<IActionResult> GetPharmacyInternalExternalMedicineAccumulateQuantity([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPharmacyInternalExternalMedicineAccumulateQuantity(filter);
            return Ok(new ResponseSuccess { data = list });
        }








        //Patient Wise report 
        [HttpGet]
        [Route("getPharmacyInternalExternalAccumulateQuantityPatientWiseReport")]
        public async Task<IActionResult> getPharmacyInternalExternalAccumulateQuantityPatientWiseReport([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPharmacyInternalExternalAccumulateQuantityPatientWiseReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardCardCount")]
        public async Task<IActionResult> GetPharmacyDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPharmacyDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardInternalExternalStats")]
        public async Task<IActionResult> GetPharmacyDashboardInternalExternalStats([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPharmacyDashboardInternalExternalStats(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pathology Login user Counts

        [HttpGet]
        [Route("getLabDashboardLoginUserTestAllCounts")]
        public async Task<IActionResult> getLabDashboardLoginUserTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardLoginUserTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Pathology NEw

        [HttpGet]
        [Route("getLabDashboardTestAllCounts")]
        public async Task<IActionResult> getLabDashboardTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getLabDashboardTestTimeFromStartTillNowAllCounts")]
        public async Task<IActionResult> getLabDashboardTestTimeFromStartTillNowAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardTestTimeFromStartTillNowAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTestAllList")]
        public async Task<IActionResult> getLabDashboardTestAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardTestAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTestTimeFromStartTillNowAllList")]
        public async Task<IActionResult> getLabDashboardTestTimeFromStartTillNowAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardTestTimeFromStartTillNowAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTop20LabTestRecommended")]
        public async Task<IActionResult> getLabDashboardTop20LabTestRecommended([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getLabDashboardTop20LabTestRecommended(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pathology
        [HttpGet]
        [Route("GetPatientVisitPathologySampleCollectedCountByUser")]
        public async Task<IActionResult> GetPatientVisitPathologySampleCollectedCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitPathologySampleCollectedCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitLabTestReportGeneratedCountByUser")]
        public async Task<IActionResult> GetPatientVisitLabTestReportGeneratedCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitLabTestReportGeneratedCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitPathologyCountByGender")]
        public async Task<IActionResult> GetPatientVisitPathologyCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitPathologyCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPathologyDashboardStats")]
        public async Task<IActionResult> GetPathologyDashboardStats([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPathologyDashboardStats(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPathologyDashboardCardCount")]
        public async Task<IActionResult> getPathologyDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPathologyDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTop20RecommendedLabTestCount")]
        public async Task<IActionResult> GetTop20RecommendedLabTestCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetTop20RecommendedLabTestCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitPathologyCountByLabTest")]
        public async Task<IActionResult> GetPatientVisitPathologyCountByLabTest([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitPathologyCountByLabTest(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitPathologyCountByDepartment")]
        public async Task<IActionResult> GetPatientVisitPathologyCountByDepartment([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientVisitPathologyCountByDepartment(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPathologyCountByStatus")]
        public async Task<IActionResult> GetPathologyCountByStatus([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPathologyCountByStatus(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region SecretaryDashboard

        //[HttpGet]
        //[Route("GetSecretaryDashboardCardCount")]
        //public async Task<IActionResult> GetSecretaryDashboardCardCount([FromQuery] DashboardFilter filter)
        //{
        //    var list = await _dashboardService.GetSecretaryDashboardCardCount(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetHeatlCertificateCountByHealthFacilityCode")]
        public async Task<IActionResult> GetHeatlCertificateCountByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetHeatlCertificateCountByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetMedicoLegalCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetMedicoLegalCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetMedicoLegalCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetHealthCouncilCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetHealthCouncilCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetHealthCouncilCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetTBScreeningCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetTBScreeningCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var res = await _dashboardService.GetTBScreeningCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = res });
        }

        [HttpGet]
        [Route("GetMIMSCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetMIMSCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
           var list = await _dashboardService.GetMIMSCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetLastUpdatedDateOfMIMS")]
        public async Task<IActionResult> GetLastUpdatedDateOfMIMS([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetLastUpdatedDateOfMIMS(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAidsCountForHMISByDateRange")]
        public async Task<IActionResult> GetAidsCountForHMISByDateRange([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetAidsCountForHMISByDateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        #endregion

        #region TbDashboard


        [HttpGet]
        [Route("GetTbRegisteredPatientsData")]
        public async Task<IActionResult> GetTbPatientsRegistered([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetTbPatientsRegistered(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        [HttpGet]
        [Route("GetTbPatientsData")]
        public async Task<IActionResult> GetTbPatientsData([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetTbPatientsData(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetTbIssuedMedicine")]
        public async Task<IActionResult> GetTbIssuedMedicine([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetTbIssuedMedicine(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        [HttpGet]
        [Route("GetTbAdvisedTest")]
        public async Task<IActionResult> GetTbAdvisedTest([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetTbAdvisedTest(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbTestResults")]
        public async Task<IActionResult> GetTbTestResults([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetTbTestResults(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbDashboardCardCount")]
        public async Task<IActionResult> GetTbDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetTbDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region OldTbDashboard

        [HttpGet]
        [Route("GetOldTbDashboardCardCount")]
        public async Task<IActionResult> GetOldTbDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetOldTbDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientListOldTb")]
        public async Task<IActionResult> GetPatientListOldTb([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientListOldTb(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientSampleListOldTb")]
        public async Task<IActionResult> GetPatientSampleListOldTb([FromQuery] TbDashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientSampleListOldTb(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Prescription
        // Patient count by Internal and external medicine
        [HttpGet]
        [Route("getPatientCountWithInterExternalMedicines")]
        public async Task<IActionResult> getPatientCountWithInterExternalMedicines([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPatientCountWithInterExternalMedicines(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        // Patient count by Internal and external medicine
        [HttpGet]
        [Route("getPatientCountWithInterExternalMedicineDoctor")]
        public async Task<IActionResult> getPatientCountWithInterExternalMedicineDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPatientCountWithInterExternalMedicinesByDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientPrescriptionIssued")]
        public async Task<IActionResult> GetPatientPrescriptionIssued([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientPrescriptionIssued(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalPharmacy")]
        public async Task<IActionResult> GetPatientInternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalPharmacy")]
        public async Task<IActionResult> GetPatientExternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientExternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalPharmacy")]
        public async Task<IActionResult> GetPatientInternalExternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalExternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        //my list 
        [HttpGet]
        [Route("GetPatientServedByDoctor")]
        public async Task<IActionResult> GetPatientServedByDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientServedByDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientPrescriptionIssuedDoctor")]
        public async Task<IActionResult> GetPatientPrescriptionIssuedDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientPrescriptionIssuedDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientInternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientExternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientExternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientInternalExternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalExternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetMedicineIssueListPharmacy")]
        public async Task<IActionResult> GetMedicineIssueListPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetMedicineIssueListPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetInQueueListPharmacy")]
        public async Task<IActionResult> GetInQueueListPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetInQueueListPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDocHFInQueuelist")]
        public async Task<IActionResult> getDocHFInQueuelist([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDocHFInQueuelist(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientTotalLabDoctor")]
        public async Task<IActionResult> GetPatientTotalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientTotalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientExternalLabDoctor")]
        public async Task<IActionResult> GetPatientExternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientExternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientInternalLabDoctor")]
        public async Task<IActionResult> GetPatientInternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientInternalExternalLabDoctor")]
        public async Task<IActionResult> GetPatientInternalExternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalExternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        

        #endregion
        #region Lab Listing
        [HttpGet]
        [Route("GetPatientTotalLabs")]
        public async Task<IActionResult> GetPatientTotalLabs([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientTotalLabs(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalLab")]
        public async Task<IActionResult> GetPatientInternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalLab")]
        public async Task<IActionResult> GetPatientExternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientExternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalLab")]
        public async Task<IActionResult> GetPatientInternalExternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPatientInternalExternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetInQueuelistLab")]
        public async Task<IActionResult> GetInQueuelistLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetInQueuelistLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPendingReportListLab")]
        public async Task<IActionResult> GetPendingReportListLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetPendingReportListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetReportGernatedListLab")]
        public async Task<IActionResult> GetReportGernatedListLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetReportGernatedListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetSampleCollectedListLab")]
        public async Task<IActionResult> GetSampleCollectedListLab([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetSampleCollectedListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetInternalLabVisitList")]
        public async Task<IActionResult> GetInternalLabVisitList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetInternalLabVisitList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region SehtSahulatCard Dashboard
        [HttpGet]
        [Route("getSehatSahulatCardDashboardCount")]
        public async Task<IActionResult> getSehatSahulatCardDashboardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getSehatSahulatCardDashboardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getSehatSahulatCardDashboardList")]
        public async Task<IActionResult> getSehatSahulatCardDashboardList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getSehatSahulatCardDashboardList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Drug Addict Patient Dashboard Counts
        [HttpGet]
        [Route("getDrugAddcictsDashboardCounts")]
        public async Task<IActionResult> SPDrugAddcictsDashboardCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsDashboardCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDrugAddcictsDashboardListings")]
        public async Task<IActionResult> getDrugAddcictsDashboardListings([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsDashboardListings(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Drug Addict diseases
        [HttpGet]
        [Route("getDrugAddcictsDashboardDieasesCounts")]
        public async Task<IActionResult> getDrugAddcictsDashboardDieasesCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsDashboardDieasesCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDrugAddcictsDashboardDieasesListings")]
        public async Task<IActionResult> getDrugAddcictsDashboardDieasesListings([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsDashboardDieasesListings(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Drug Addict To Social Welfare
        [HttpGet]
        [Route("getDrugAddcictsToSocialWelfareCounts")]
        public async Task<IActionResult> getDrugAddcictsToSocialWelfareCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsToSocialWelfareCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDrugAddcictsToSocialWelfareListings")]
        public async Task<IActionResult> getDrugAddcictsToSocialWelfareListings([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDrugAddcictsToSocialWelfareListings(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region BAS Dashboard

        [HttpGet]
        [Route("getBasDashboardAllCounts")]
        public async Task<IActionResult> getBasDashboardAllCounts([FromQuery] DashboardBASFilter filter)
        {
            string hfcode =await _dashboardService.GetHfCode(filter);


            var list = await _BASService.getBasDashboardAllCounts(_BASBaseUrl, hfcode);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getBasDashboardAllemployeeList")]
        public async Task<IActionResult> getBasDashboardAllemployeeList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);


            var list = await _BASService.getBasDashboardAllemployeeList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getBasDashboardAllDevicesList")]
        public async Task<IActionResult> getBasDashboardAllDevicesList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);


            var list = await _BASService.getBasDashboardAllDevicesList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getBasDashboardAllSyncDevicesList")]
        public async Task<IActionResult> getBasDashboardAllSyncDevicesList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);


            var list = await _BASService.getBasDashboardAllSyncDevicesList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getBasDashboardAllOnlineOfflineDevicesList")]
        public async Task<IActionResult> getBasDashboardAllOnlineOfflineDevicesList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);


            var list = await _BASService.getBasDashboardAllOnlineOfflineDevicesList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }
        
        [HttpGet]
        [Route("getBasDashboardAllRostersList")]
        public async Task<IActionResult> getBasDashboardAllRostersList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            var list = await _BASService.getBasDashboardAllRostersList(_BASBaseUrl, hfcode, CurrentMonth, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getBasDashboardAllAttendenceReportCounts")]
        public async Task<IActionResult> getBasDashboardAllAttendenceReportCounts([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            AttendenceObject attendence = new AttendenceObject();
            attendence.HfmisCode = hfcode;
            attendence.IsFilter = filter.IsFilter;
            attendence.DateFrom = filter.StartDate;
            attendence.DateTo = filter.EndDate;
            var list = await _BASService.getBasDashboardAllAttendenceReportCounts(_BASBaseUrl, attendence);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getBasDashboardAllNotInRosterCount")]
        public async Task<IActionResult> getBasDashboardAllNotInRosterCount([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            var list = await _BASService.getBasDashboardAllNotInRosterCount(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getBasDashboardAllAttendenceReportList")]
        public async Task<IActionResult> getBasDashboardAllAttendenceReportList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            var list = await _BASService.getBasDashboardAllAttendenceReportList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("getBasDashboardAllAttendenceDateWiseList")]
        public async Task<IActionResult> getBasDashboardAllAttendenceDateWiseList([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            var list = await _BASService.getBasDashboardAllAttendenceDateWiseList(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getBasDashboardAllAttendenceDateWiseListDownload")]
        public async Task<IActionResult> getBasDashboardAllAttendenceDateWiseListDownload([FromQuery] DashboardBASFilter filter)
        {
            string hfcode = await _dashboardService.GetHfCode(filter);
            string CurrentMonth = DateTime.Now.ToString("MMMM");
            var list = await _BASService.getBasDashboardAllAttendenceDateWiseListDownload(_BASBaseUrl, hfcode, filter.listType);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region HCP Dashboard 
        [HttpGet]
        [Route("getHCPDashboardAllCounts")]
        public async Task<IActionResult> getHCPDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHCPDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllCountsUpdated")]
        public async Task<IActionResult> getHCPDashboardAllCountsUpdated([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHCPDashboardAllCountsUpdated(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllList")]
        public async Task<IActionResult> getHCPDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHCPDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllListUpdated")]
        public async Task<IActionResult> getHCPDashboardAllListUpdated([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getHCPDashboardAllListUpdated(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetHCPpreviousDashboardIndicatorsCounts")]
        public async Task<IActionResult> GetHCPpreviousDashboardIndicatorsCounts()
        {
            var list = await _dashboardService.GetHCPpreviousDashboardIndicatorsCounts();
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion


        #region DSR Dashboard 
        [HttpGet]
        [Route("getDSRReportAllCounts")]
        public async Task<IActionResult> getDSRReportAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDSRReportAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        //#region Paraplegic Dashboard 
        //[HttpGet]
        //[Route("getParaplegicDashboardAllCounts")]
        //public async Task<IActionResult> getParaplegicDashboardAllCounts([FromQuery] DashboardFilter filter)
        //{
        //    var list = await _dashboardService.getParaplegicDashboardAllCounts(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //[HttpGet]
        //[Route("getParaplegicDashboardAllList")]
        //public async Task<IActionResult> getParaplegicDashboardAllList([FromQuery] DashboardFilter filter)
        //{
        //    var list = await _dashboardService.getParaplegicDashboardAllList(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //#endregion

        #region Dental Dashboard 
        [HttpGet]
        [Route("getDentalDashboardAllCounts")]
        public async Task<IActionResult> getDentalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDentalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDentalDashboardAllList")]
        public async Task<IActionResult> getDentalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDentalDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDentalDashboardAllListProcedures")]
        public async Task<IActionResult> getDentalDashboardAllListProcedures([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getDentalDashboardAllListProcedures(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Physio Dashboard 
        [HttpGet]
        [Route("getPhysioTherapyDashboardAllCounts")]
        public async Task<IActionResult> getPhysioTherapyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPhysioTherapyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPhysioTherapyDashboardAllList")]
        public async Task<IActionResult> getPhysioTherapyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPhysioTherapyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Speech Therapy Dashboard 
        [HttpGet]
        [Route("getSpeechTherapyDashboardAllCounts")]
        public async Task<IActionResult> getSpeechTherapyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getSpeechTherapyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getSpeechTherapyDashboardAllList")]
        public async Task<IActionResult> getSpeechTherapyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getSpeechTherapyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Nutrition Dashboard 
        [HttpGet]
        [Route("getNutritionDashboardAllCounts")]
        public async Task<IActionResult> getNutritionDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getNutritionDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getNutritionDashboardAllList")]
        public async Task<IActionResult> getNutritionDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getNutritionDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Psychology Dashboard 
        [HttpGet]
        [Route("getPsychologyDashboardAllCounts")]
        public async Task<IActionResult> getPsychologyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPsychologyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPsychologyDashboardAllList")]
        public async Task<IActionResult> getPsychologyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getPsychologyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region


        [HttpGet]
        [Route("GetEyeInfectionDashboardCardCount")]
        public async Task<IActionResult> GetEyeInfectionDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.GetEyeInfectionDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        #endregion
    }
}

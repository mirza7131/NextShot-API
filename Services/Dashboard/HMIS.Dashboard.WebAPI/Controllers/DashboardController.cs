using CommonDTOs.ResponseDTO;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Services;
using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Domain.Models.DTO.DashboardDto;
using HMIS.Dashboard.Domain.Models.DTO.IntegratedDashboard;
using HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Dashboard.Domain.Models.DTO.TbMedicineDeliveryData;
using HMIS.Dashboard.Service;
using HMIS.Dashboard.Services;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Dashboard.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    
    public class DashboardController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly DashboardService _dashboardService;
        private readonly DashboardTransService _dashboardTransService;
        private readonly DashboardSyncService _dashboardSyncService;
        //private readonly PatientDiagnoseService<PatientDiagnose> _PatientDiagnoseService;
        private readonly BASService _BASService;
        private readonly string _BASBaseUrl;
        private readonly bool _isDevelopment;
        #endregion

        #region Constructor

        public DashboardController(TokenService tokenService, DashboardService DashboardService, DashboardTransService DashboardTransService, DashboardSyncService dashboardSyncService, BASService _bASService, IConfiguration config)
        {
            _tokenService = tokenService;
            _dashboardService = DashboardService;
            _dashboardTransService = DashboardTransService;
            _dashboardSyncService = dashboardSyncService;

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
            var list = await _dashboardSyncService.getDataSyncUtilityLog(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Patient Registration Dashboard New  
        [HttpGet] 
        [Route("getRegistrationDashboardLoginUserAllCounts")] 
        public async Task<IActionResult> getRegistrationDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        { 
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getRegistrationDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list }); 
        } 

        [HttpGet]
        [Route("getRegistrationDashboardAllCounts")]
        public async Task<IActionResult> getRegistrationDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getRegistrationDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getRegistrationDashboardAllList")]
        public async Task<IActionResult> getRegistrationDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getRegistrationDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getRegDashboardOPDSectionWiseTokenCount")]
        public async Task<IActionResult> getRegDashboardOPDSectionWiseTokenCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getRegDashboardOPDSectionWiseTokenCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region HR Health Dashboard Counts And Listing  
        [HttpGet]
        [Route("getHRHealthDashboardCounts")]
        public async Task<IActionResult> getHRHealthDashboardCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHRHealthDashboardCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getHRSectionWiseTotalCounts")]
        public async Task<IActionResult> getHRSectionWiseTotalCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHRSectionWiseTotalCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getHRHealthDashboardListing")]
        public async Task<IActionResult> getHRHealthDashboardListing([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHRHealthDashboardListing(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Patient Registration Dashboard

        [HttpGet]
        [Route("GetPatientVisitCountByHealthFacilityByPMIS")]
        public async Task<IActionResult> GetPatientVisitCountByHealthFacilityByPMIS([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByHealthFacilityByPMIS(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByHealthFacility")]
        public async Task<IActionResult> GetPatientVisitCountByHealthFacility([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByHealthFacility(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByUser")]
        public async Task<IActionResult> GetPatientVisitCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByProvince")]
        public async Task<IActionResult> GetPatientVisitCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientRegisteredCountByProvince")]
        public async Task<IActionResult> GetPatientRegisteredCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientRegisteredCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByProvinceByGender")]
        public async Task<IActionResult> GetPatientVisitCountByProvinceByGender([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByProvinceByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountSelfVsOthers")]
        public async Task<IActionResult> GetPatientVisitCountSelfVsOthers([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountSelfVsOthers(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientNewRegisteredVsRevisit")]
        public async Task<IActionResult> GetPatientNewRegisteredVsRevisit([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientNewRegisteredVsRevisit(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByGenderBySelfVsOthers")]
        public async Task<IActionResult> GetPatientVisitCountByGenderBySelfVsOthers([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByGenderBySelfVsOthers(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByAgeRangeByGender")]
        public async Task<IActionResult> GetPatientVisitCountByAgeRangeByGender([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByAgeRangeByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitCountByDeptBySecByMonth")]
        public async Task<IActionResult> GetPatientVisitCountByDeptBySecByMonth([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitCountByDeptBySecByMonth(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTodayPatientVisitCountByDeptBySec")]
        public async Task<IActionResult> GetPatientVisitCountByDeptBySecByDate([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTodayPatientVisitCountByDeptBySec(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        //New Dashboard Tiles
        [HttpGet]
        [Route("GetRegistrationDashboardCardCount")]
        public async Task<IActionResult> GetRegistrationDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var res = await SwitchConnectionBetweenTransAndReplication(filter).GetRegistrationDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = res });
        }

        #endregion

        #region Doctor Dashboard
        [HttpGet]
        [Route("getDoctorDashboardLoginUserAllCounts")]
        public async Task<IActionResult> getDoctorDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        
        [HttpGet]
        [Route("getDoctorDashboardPatientAllCounts")]
        public async Task<IActionResult> getDoctorDashboardPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHFPatientAllCounts")]
        public async Task<IActionResult> getDoctorDashboardHFPatientAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardHFPatientAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardPatientAllList")]
        public async Task<IActionResult> getDoctorDashboardPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHFPatientAllList")]
        public async Task<IActionResult> getDoctorDashboardHFPatientAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardHFPatientAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDoctorDashboardHfPatientCount")]
        public async Task<IActionResult> getDoctorDashboardHfPatientCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDoctorDashboardHfPatientCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByUser")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitDoctorCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByProvince")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitDoctorCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByGender")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitDoctorCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitDoctorCountByDisease")]
        public async Task<IActionResult> GetPatientVisitDoctorCountByDisease([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitDoctorCountByDisease(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetDoctorPrescribedMedicineAccumulateQuantity")]
        public async Task<IActionResult> GetDoctorPrescribedMedicineAccumulateQuantity([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetDoctorPrescribedMedicineAccumulateQuantity(filter);
            return Ok(new ResponseSuccess { data = list });
        }

       

        [HttpGet]
        [Route("GetDoctorRecommendedLabCount")]
        public async Task<IActionResult> GetDoctorRecommendedLabCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetDoctorRecommendedLabCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        // Labs counts Internal and External
        [HttpGet]
        [Route("getInternalAndExternalLabCounts")]
        public async Task<IActionResult> getInternalAndExternalLabCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getInternalAndExternalLabCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetDoctorDashboardCardCount")]
        public async Task<IActionResult> GetDoctorDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetDoctorDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        //[HttpGet]
        //[Route("GetAllQueList")]
        //public async Task<IActionResult> GetAllQueList([FromQuery] FilterPatientVisitDto filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetAllpatientInQue(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}
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
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getVitalDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Patient Vital Dashboard New  
        [HttpGet]
        [Route("getVitalDashboardAllCounts")]
        public async Task<IActionResult> getVitalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getVitalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getVitalDashboardAllList")]
        public async Task<IActionResult> getVitalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getVitalDashboardAllList(filter);
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
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetVitalDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByUser")]
        public async Task<IActionResult> GetPatientVisitVitalCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByProvince")]
        public async Task<IActionResult> GetPatientVisitVitalCountByProvince([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByProvince(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByGender")]
        public async Task<IActionResult> GetPatientVisitVitalCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByWeightRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByWeightRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByWeightRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetPatientVisitVitalCountByRespiratoryRateRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByRespiratoryRateRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByRespiratoryRateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByTemperatureRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByTemperatureRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByTemperatureRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByPulseRateRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByPulseRateRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByPulseRateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitVitalCountByBloodPressureRange")]
        public async Task<IActionResult> GetPatientVisitVitalCountByBloodPressureRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitVitalCountByBloodPressureRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pharmacy New
        [HttpGet]
        [Route("getPharmacyDashboardLoginUserAllCounts")]
        public async Task<IActionResult> getPharmacyDashboardLoginUserAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyDashboardLoginUserAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPharmacyDashboardAllCounts")]
        public async Task<IActionResult> getPharmacyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPharmacyDashboardAllList")]
        public async Task<IActionResult> getPharmacyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardMedcineIssuedReport")]
        public async Task<IActionResult> GetPharmacyDashboardMedcineIssuedReport([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPharmacyDashboardMedcineIssuedReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPharmacyDashboardMedcineIssuedPatientDetailList")]
        public async Task<IActionResult> getPharmacyDashboardMedcineIssuedPatientDetailList([FromQuery] MedicineIssuedFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyDashboardMedcineIssuedPatientDetailList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPharmacyDashboardOflineMedicineStock")]
        public async Task<IActionResult> getPharmacyDashboardOflineMedicineStock([FromQuery] MedicineIssuedFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyDashboardOflineMedicineStock(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Pharmacy old
        [HttpGet]
        [Route("GetPatientVisitPharmacyCountByUser")]
        public async Task<IActionResult> GetPatientVisitPharmacyCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitPharmacyCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyInternalExternalMedicineAccumulateQuantity")]
        public async Task<IActionResult> GetPharmacyInternalExternalMedicineAccumulateQuantity([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPharmacyInternalExternalMedicineAccumulateQuantity(filter);
            return Ok(new ResponseSuccess { data = list });
        }








        //Patient Wise report 
        [HttpGet]
        [Route("getPharmacyInternalExternalAccumulateQuantityPatientWiseReport")]
        public async Task<IActionResult> getPharmacyInternalExternalAccumulateQuantityPatientWiseReport([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPharmacyInternalExternalAccumulateQuantityPatientWiseReport(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardCardCount")]
        public async Task<IActionResult> GetPharmacyDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPharmacyDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPharmacyDashboardInternalExternalStats")]
        public async Task<IActionResult> GetPharmacyDashboardInternalExternalStats([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPharmacyDashboardInternalExternalStats(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pathology Login user Counts

        [HttpGet]
        [Route("getLabDashboardLoginUserTestAllCounts")]
        public async Task<IActionResult> getLabDashboardLoginUserTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardLoginUserTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Pathology NEw

        [HttpGet]
        [Route("getLabDashboardTestAllCounts")]
        public async Task<IActionResult> getLabDashboardTestAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardTestAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getLabDashboardTestTimeFromStartTillNowAllCounts")]
        public async Task<IActionResult> getLabDashboardTestTimeFromStartTillNowAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardTestTimeFromStartTillNowAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTestAllList")]
        public async Task<IActionResult> getLabDashboardTestAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardTestAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTestTimeFromStartTillNowAllList")]
        public async Task<IActionResult> getLabDashboardTestTimeFromStartTillNowAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardTestTimeFromStartTillNowAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getLabDashboardTop20LabTestRecommended")]
        public async Task<IActionResult> getLabDashboardTop20LabTestRecommended([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getLabDashboardTop20LabTestRecommended(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Pathology
        [HttpGet]
        [Route("GetPatientVisitPathologySampleCollectedCountByUser")]
        public async Task<IActionResult> GetPatientVisitPathologySampleCollectedCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitPathologySampleCollectedCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitLabTestReportGeneratedCountByUser")]
        public async Task<IActionResult> GetPatientVisitLabTestReportGeneratedCountByUser([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitLabTestReportGeneratedCountByUser(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitPathologyCountByGender")]
        public async Task<IActionResult> GetPatientVisitPathologyCountByGender([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitPathologyCountByGender(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPathologyDashboardStats")]
        public async Task<IActionResult> GetPathologyDashboardStats([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPathologyDashboardStats(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getPathologyDashboardCardCount")]
        public async Task<IActionResult> getPathologyDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPathologyDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTop20RecommendedLabTestCount")]
        public async Task<IActionResult> GetTop20RecommendedLabTestCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTop20RecommendedLabTestCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientVisitPathologyCountByLabTest")]
        public async Task<IActionResult> GetPatientVisitPathologyCountByLabTest([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitPathologyCountByLabTest(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        //[HttpGet]
        //[Route("GetPatientVisitPathologyCountByDepartment")]
        //public async Task<IActionResult> GetPatientVisitPathologyCountByDepartment([FromQuery] DashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientVisitPathologyCountByDepartment(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetPathologyCountByStatus")]
        public async Task<IActionResult> GetPathologyCountByStatus([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPathologyCountByStatus(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region SecretaryDashboard

        //[HttpGet]
        //[Route("GetSecretaryDashboardCardCount")]
        //public async Task<IActionResult> GetSecretaryDashboardCardCount([FromQuery] DashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetSecretaryDashboardCardCount(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetHeatlCertificateCountByHealthFacilityCode")]
        public async Task<IActionResult> GetHeatlCertificateCountByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetHeatlCertificateCountByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetMedicoLegalCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetMedicoLegalCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetMedicoLegalCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetHealthCouncilCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetHealthCouncilCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetHealthCouncilCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetTBScreeningCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetTBScreeningCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
            var res = await SwitchConnectionBetweenTransAndReplication(filter).GetTBScreeningCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = res });
        }

        [HttpGet]
        [Route("GetMIMSCountForHMISByHealthFacilityCode")]
        public async Task<IActionResult> GetMIMSCountForHMISByHealthFacilityCode([FromQuery] DashboardFilter filter)
        {
           var list = await SwitchConnectionBetweenTransAndReplication(filter).GetMIMSCountForHMISByHealthFacilityCode(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetLastUpdatedDateOfMIMS")]
        public async Task<IActionResult> GetLastUpdatedDateOfMIMS([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetLastUpdatedDateOfMIMS(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAidsCountForHMISByDateRange")]
        public async Task<IActionResult> GetAidsCountForHMISByDateRange([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetAidsCountForHMISByDateRange(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        #endregion

        #region TbDashboard


        [HttpGet]
        [Route("GetTbRegisteredPatientsData")]
        public async Task<IActionResult> GetTbPatientsRegistered([FromQuery] TbDashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbPatientsRegistered(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        [HttpGet]
        [Route("GetTbPatientsData")]
        public async Task<IActionResult> GetTbPatientsData([FromQuery] TbDashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbPatientsData(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetTbIssuedMedicine")]
        public async Task<IActionResult> GetTbIssuedMedicine([FromQuery] TbDashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbIssuedMedicine(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        
        [HttpGet]
        [Route("GetTbMedicineToBeDelivered")]
        public async Task<IActionResult> GetTbMedicineToBeDelivered([FromQuery] TbDashboardMedicineDeliveryFilter filter)
        {
            var list = await _dashboardTransService.GetTbMedicineToBeDelivered(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbMedicineToBeDeliveredById")]
        public async Task<IActionResult> GetTbMedicineToBeDeliveredById(int? PatientId)
        {
            var obj = await _dashboardTransService.GetTbMedicineToBeDeliveredById(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("TbMedicineDeliveryStatusUpdate")]
        public async Task<IActionResult> TbMedicineDeliveryStatusUpdate(MedicineDeliveryStatusUpdateDto input)
        {
            var obj = await _dashboardTransService.TbMedicineDeliveryStatusUpdate(input);
            return Ok(new ResponseSave { data = obj });
            //return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbMedicineDeliveryDashboardCardCount")]
        public async Task<IActionResult> GetTbMedicineDeliveryDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardTransService.GetTbMedicineDeliveryDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        [HttpGet]
        [Route("GetTbAdvisedTest")]
        public async Task<IActionResult> GetTbAdvisedTest([FromQuery] TbDashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbAdvisedTest(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbTestResults")]
        public async Task<IActionResult> GetTbTestResults([FromQuery] TbDashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbTestResults(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetTbDashboardCardCount")]
        public async Task<IActionResult> GetTbDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetTbDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region OldTbDashboard

        //[HttpGet]
        //[Route("GetOldTbDashboardCardCount")]
        //public async Task<IActionResult> GetOldTbDashboardCardCount([FromQuery] DashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetOldTbDashboardCardCount(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetPatientListOldTb")]
        //public async Task<IActionResult> GetPatientListOldTb([FromQuery] TbDashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientListOldTb(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetPatientSampleListOldTb")]
        //public async Task<IActionResult> GetPatientSampleListOldTb([FromQuery] TbDashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientSampleListOldTb(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        #endregion

        #region Prescription
        // Patient count by Internal and external medicine
        [HttpGet]
        [Route("getPatientCountWithInterExternalMedicines")]
        public async Task<IActionResult> getPatientCountWithInterExternalMedicines([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPatientCountWithInterExternalMedicines(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        // Patient count by Internal and external medicine
        [HttpGet]
        [Route("getPatientCountWithInterExternalMedicineDoctor")]
        public async Task<IActionResult> getPatientCountWithInterExternalMedicineDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPatientCountWithInterExternalMedicinesByDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientPrescriptionIssued")]
        public async Task<IActionResult> GetPatientPrescriptionIssued([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientPrescriptionIssued(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalPharmacy")]
        public async Task<IActionResult> GetPatientInternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalPharmacy")]
        public async Task<IActionResult> GetPatientExternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientExternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalPharmacy")]
        public async Task<IActionResult> GetPatientInternalExternalPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalExternalPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        //my list 
        [HttpGet]
        [Route("GetPatientServedByDoctor")]
        public async Task<IActionResult> GetPatientServedByDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientServedByDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientPrescriptionIssuedDoctor")]
        public async Task<IActionResult> GetPatientPrescriptionIssuedDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientPrescriptionIssuedDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientInternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientExternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientExternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalPharmacyDoctor")]
        public async Task<IActionResult> GetPatientInternalExternalPharmacyDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalExternalPharmacyDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetMedicineIssueListPharmacy")]
        public async Task<IActionResult> GetMedicineIssueListPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetMedicineIssueListPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetInQueueListPharmacy")]
        public async Task<IActionResult> GetInQueueListPharmacy([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetInQueueListPharmacy(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDocHFInQueuelist")]
        public async Task<IActionResult> getDocHFInQueuelist([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDocHFInQueuelist(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientTotalLabDoctor")]
        public async Task<IActionResult> GetPatientTotalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientTotalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientExternalLabDoctor")]
        public async Task<IActionResult> GetPatientExternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientExternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientInternalLabDoctor")]
        public async Task<IActionResult> GetPatientInternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetPatientInternalExternalLabDoctor")]
        public async Task<IActionResult> GetPatientInternalExternalLabDoctor([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalExternalLabDoctor(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        

        #endregion
        #region Lab Listing
        [HttpGet]
        [Route("GetPatientTotalLabs")]
        public async Task<IActionResult> GetPatientTotalLabs([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientTotalLabs(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalLab")]
        public async Task<IActionResult> GetPatientInternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientExternalLab")]
        public async Task<IActionResult> GetPatientExternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientExternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPatientInternalExternalLab")]
        public async Task<IActionResult> GetPatientInternalExternalLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPatientInternalExternalLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetInQueuelistLab")]
        public async Task<IActionResult> GetInQueuelistLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetInQueuelistLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetPendingReportListLab")]
        public async Task<IActionResult> GetPendingReportListLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetPendingReportListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetReportGernatedListLab")]
        public async Task<IActionResult> GetReportGernatedListLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetReportGernatedListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetSampleCollectedListLab")]
        public async Task<IActionResult> GetSampleCollectedListLab([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetSampleCollectedListLab(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetInternalLabVisitList")]
        public async Task<IActionResult> GetInternalLabVisitList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetInternalLabVisitList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region SehtSahulatCard Dashboard
        [HttpGet]
        [Route("getSehatSahulatCardDashboardCount")]
        public async Task<IActionResult> getSehatSahulatCardDashboardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getSehatSahulatCardDashboardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getSehatSahulatCardDashboardList")]
        public async Task<IActionResult> getSehatSahulatCardDashboardList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getSehatSahulatCardDashboardList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Drug Addict Patient Dashboard Counts
        [HttpGet]
        [Route("getDrugAddcictsDashboardCounts")]
        public async Task<IActionResult> SPDrugAddcictsDashboardCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsDashboardCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDrugAddcictsDashboardListings")]
        public async Task<IActionResult> getDrugAddcictsDashboardListings([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsDashboardListings(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Drug Addict diseases
        [HttpGet]
        [Route("getDrugAddcictsDashboardDieasesCounts")]
        public async Task<IActionResult> getDrugAddcictsDashboardDieasesCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsDashboardDieasesCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDrugAddcictsDashboardDieasesListings")]
        public async Task<IActionResult> getDrugAddcictsDashboardDieasesListings([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsDashboardDieasesListings(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region Drug Addict To Social Welfare
        [HttpGet]
        [Route("getDrugAddcictsToSocialWelfareCounts")]
        public async Task<IActionResult> getDrugAddcictsToSocialWelfareCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsToSocialWelfareCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDrugAddcictsToSocialWelfareListings")]
        public async Task<IActionResult> getDrugAddcictsToSocialWelfareListings([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDrugAddcictsToSocialWelfareListings(filter);
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
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHCPDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllCountsUpdated")]
        public async Task<IActionResult> getHCPDashboardAllCountsUpdated([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHCPDashboardAllCountsUpdated(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllList")]
        public async Task<IActionResult> getHCPDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHCPDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getHCPDashboardAllListUpdated")]
        public async Task<IActionResult> getHCPDashboardAllListUpdated([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHCPDashboardAllListUpdated(filter);
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
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDSRReportAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        #region TB DSR Dashboard 
        //Get Profile Type Of Tb Patient Source
        [HttpGet]
        [Route("GetProfileByProfileType")]
        public async Task<IActionResult> GetProfileByProfileType(string ProfileType)
        {
            var obj = await _dashboardService.GetProfileByProfileType(ProfileType);
            return Ok(new ResponseSuccess { data = obj });
        }
        [HttpGet]
        [Route("getTBDSRReportAllCounts")]
        public async Task<IActionResult> getTBDSRReportAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await _dashboardService.getTBDSRReportAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
        //#region Paraplegic Dashboard 
        //[HttpGet]
        //[Route("getParaplegicDashboardAllCounts")]
        //public async Task<IActionResult> getParaplegicDashboardAllCounts([FromQuery] DashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).getParaplegicDashboardAllCounts(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //[HttpGet]
        //[Route("getParaplegicDashboardAllList")]
        //public async Task<IActionResult> getParaplegicDashboardAllList([FromQuery] DashboardFilter filter)
        //{
        //    var list = await SwitchConnectionBetweenTransAndReplication(filter).getParaplegicDashboardAllList(filter);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //#endregion

        #region Dental Dashboard 
        [HttpGet]
        [Route("getDentalDashboardAllCounts")]
        public async Task<IActionResult> getDentalDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDentalDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getDentalDashboardAllList")]
        public async Task<IActionResult> getDentalDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDentalDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getDentalDashboardAllListProcedures")]
        public async Task<IActionResult> getDentalDashboardAllListProcedures([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getDentalDashboardAllListProcedures(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion

        #region Physio Dashboard 
        [HttpGet]
        [Route("getPhysioTherapyDashboardAllCounts")]
        public async Task<IActionResult> getPhysioTherapyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPhysioTherapyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPhysioTherapyDashboardAllList")]
        public async Task<IActionResult> getPhysioTherapyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPhysioTherapyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Speech Therapy Dashboard 
        [HttpGet]
        [Route("getSpeechTherapyDashboardAllCounts")]
        public async Task<IActionResult> getSpeechTherapyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getSpeechTherapyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getSpeechTherapyDashboardAllList")]
        public async Task<IActionResult> getSpeechTherapyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getSpeechTherapyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Nutrition Dashboard 
        [HttpGet]
        [Route("getNutritionDashboardAllCounts")]
        public async Task<IActionResult> getNutritionDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getNutritionDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getNutritionDashboardAllList")]
        public async Task<IActionResult> getNutritionDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getNutritionDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region Psychology Dashboard 
        [HttpGet]
        [Route("getPsychologyDashboardAllCounts")]
        public async Task<IActionResult> getPsychologyDashboardAllCounts([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPsychologyDashboardAllCounts(filter);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getPsychologyDashboardAllList")]
        public async Task<IActionResult> getPsychologyDashboardAllList([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getPsychologyDashboardAllList(filter);
            return Ok(new ResponseSuccess { data = list });
        }


        #endregion

        #region HealthFacility wise Disease List
        [HttpGet]
        [Route("getHealthFacilitywiseDiseaseListCount")]
        public async Task<IActionResult> getHealthFacilitywiseDiseaseListCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHealthFacilitywiseDiseaseListCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("getHealthFacilitywiseDiseaseListCountExport")]
        public async Task<IActionResult> getHealthFacilitywiseDiseaseListCountExport([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).getHealthFacilitywiseDiseaseListCountExport(filter);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region


        [HttpGet]
        [Route("GetEyeInfectionDashboardCardCount")]
        public async Task<IActionResult> GetEyeInfectionDashboardCardCount([FromQuery] DashboardFilter filter)
        {
            var list = await SwitchConnectionBetweenTransAndReplication(filter).GetEyeInfectionDashboardCardCount(filter);
            return Ok(new ResponseSuccess { data = list });
        }



        #endregion
        #region Integrated dashboard APIs

        #region OPD ER IPD Section

        #region OPD IPD ER Dashboard Counts  
        [HttpPost]
        [Route("getIDOpdErIpdCounts")]
        public async Task<IActionResult> getIDOpdErIpdCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOpdErIpdCounts(model);
            return Ok(list);
        }
        #endregion
        #region OPD ER IPD HftypeWise Count
        [HttpPost]
        [Route("getIDHfTypeWiseOpdErIpdCounts")]
        public async Task<IActionResult> getIDHfTypeWiseOpdErIpdCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHfTypeWiseOpdErIpdCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion

        #endregion

        #region registration Section

        #region Registrations
        [HttpPost]
        [Route("getIDRegistrationAllCounts")]
        public async Task<IActionResult> getIDRegistrationAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDRegistrationAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region registration list hfwise
        [HttpPost]
        [Route("getIDRegistrationHFWiseList")]
        public async Task<IActionResult> getIDRegistrationHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDRegistrationHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region registration list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDRegistrationHFTypeWiseCount")]
        public async Task<IActionResult> getIDRegistrationHFTypeWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDRegistrationHFTypeWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion

        #region registration District and HfType wise when sending with list type
        [HttpPost]
        [Route("getIDRegistrationDistrictandHFTypeWiseCount")]
        public async Task<IActionResult> getIDRegistrationDistrictandHFTypeWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDRegistrationDistrictandHFTypeWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.DistrictandHFTypeWiseCounts
            });
        }
        #endregion

        #region registration Patient Line List
        [HttpPost]
        [Route("getIDRegistrationPatientLineList")]
        public async Task<IActionResult> getIDRegistrationPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDRegistrationPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion

        #endregion

        #region Vitals Section 
        #region Vitals
        [HttpPost]
        [Route("getIDVitalAllCounts")]
        public async Task<IActionResult> getIDVitalAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDVitalAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Vitals hf wise list
        [HttpPost]
        [Route("getIDVitalHFWiseList")]
        public async Task<IActionResult> getIDVitalHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDVitalHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion
        #region Vital Counts HfType wise when sending with list type
        [HttpPost]
        [Route("getIDVitalHFTypeWiseCount")]
        public async Task<IActionResult> getIDVitalHFTypeWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDVitalHFTypeWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Vitals Patient Line list
        [HttpPost]
        [Route("getIDVitalPatientLineList")]
        public async Task<IActionResult> getIDVitalPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDVitalPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion

        #region Prescription Section 

        #region Prescription
        [HttpPost]
        [Route("getIDPrescriptionAllCounts")]
        public async Task<IActionResult> getIDPrescriptionAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPrescriptionAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region DoctorDashboard HFWise List
        [HttpPost]
        [Route("getIDPrescriptionHFWiseList")]
        public async Task<IActionResult> getIDPrescriptionHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPrescriptionHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion
        #region Doctor Counts HfType wise when sending with list type
        [HttpPost]
        [Route("getIDPrescriptionHFTypeWiseCount")]
        public async Task<IActionResult> getIDPrescriptionHFTypeWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPrescriptionHFTypeWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region DoctorDashboard Patient Line List
        [HttpPost]
        [Route("getIDPrescriptionPatientLineList")]
        public async Task<IActionResult> getIDPrescriptionPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPrescriptionPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion

        #endregion

        #region Pharmacy

        #region Pharmacy
        [HttpPost]
        [Route("getIDPharmacyAllCounts")]
        public async Task<IActionResult> getIDPharmacyAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPharmacyAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                data = list
            });
        }
        #endregion
        #region Pharmacy List hf wise
        [HttpPost]
        [Route("getIDPharmacyHFWiseList")]
        public async Task<IActionResult> getIDPharmacyHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPharmacyHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion
        #region Pharmacy Counts HfType wise when sending with list type
        [HttpPost]
        [Route("getIDPharmacyHFTypeWiseCount")]
        public async Task<IActionResult> getIDPharmacyHFTypeWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPharmacyHFTypeWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Pharmacy Patient Line List
        [HttpPost]
        [Route("getIDPharmacyPatientLineList")]
        public async Task<IActionResult> getIDPharmacyPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPharmacyPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion

        #endregion

        //#region System Stats With Sample Taken (Not HfWise)
        //[HttpPost]
        //[Route("getIDOverAllComplianceReportCounts")]
        //public async Task<IActionResult> getIDOverAllComplianceReportCounts([FromBody] IDIntegratedDashboardDTOs model)
        //{
        //    var list = await _dashboardService.getIDOverAllComplianceReportCounts(model);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //#endregion
        //#region Report Stats With Sample Taken for system overall (Not HfWise)
        //[HttpPost]
        //[Route("getOverAllComplianceReportCounts")]
        //public async Task<IActionResult> getOverAllComplianceReportCounts([FromBody] IDIntegratedDashboardDTOs model)
        //{
        //    var list = await _dashboardService.getOverAllComplianceReportCounts(model);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        //#endregion
        #region Hftype Wise Reproting Non Reporting Healthfacility Counts
        [HttpPost]
        [Route("getIDReportingNonReportingHfCounts")]
        public async Task<IActionResult> getIDReportingNonReportingHfCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDReportingNonReportingHfCounts(model);

            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region System Statistics HfWise (Total Counts HfWise) 
        [HttpPost]
        [Route("getIDHfWiseStatisticsReportCounts")]
        public async Task<IActionResult> getIDHfWiseStatisticsReportCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHfWiseStatisticsReportCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HfWsieStatistics
            });
        }
        #endregion

        #region Statistics Compliance Not Reporting Hf List
        [HttpPost]
        [Route("getIDHfWiseStatisticsNotReporting")]
        public async Task<IActionResult> getIDHfWiseStatisticsNotReporting([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHfWiseStatisticsNotReporting(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HfWsieStatistics
            });
        }
        #endregion

        #region System Statistics HfWise (Total Counts HfWise) 
        [HttpPost]
        [Route("getIDHfWiseCompleteStatisticsReportCounts")]
        public async Task<IActionResult> getIDHfWiseCompleteStatisticsReportCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHfWiseCompleteStatisticsReportCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Pathology
        #region Pathology total Count
        [HttpPost]
        [Route("getIDPathologyMainCount")]
        public async Task<IActionResult> getIDPathologyMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Pathology Test Wise Count
        [HttpPost]
        [Route("getIDPathologyTestWiseCount")]
        public async Task<IActionResult> getIDPathologyTestWiseCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyTestWiseCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.TestWiseCount
            });
        }
        #endregion
        #region Pathology HF Wise Count
        [HttpPost]
        [Route("getIDPathologyHFWiseList")]
        public async Task<IActionResult> getIDPathologyHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseList
            });
        }
        #endregion
        #region Pathology HF Type Wise Count
        [HttpPost]
        [Route("getIDPathologyHFTypeWiseList")]
        public async Task<IActionResult> getIDPathologyHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityTypeWiseList
            });
        }
        #endregion
        #region Pathology HF Type Wise Count
        [HttpPost]
        [Route("getIDPathologyPatientLineList")]
        public async Task<IActionResult> getIDPathologyPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #region PathologyDashboard All Counts
        [HttpPost]
        [Route("getIDPathologyAllCounts")]
        public async Task<IActionResult> getIDPathologyAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region PathologyDashboard HF Wise Overall Count after receiving list type (without Test Id)
        [HttpPost]
        [Route("getIDPathologyHFWiseWithListType")]
        public async Task<IActionResult> getIDPathologyHFWiseWithListType([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyHFWiseWithListType(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion
        #region PathologyDashboard HF Type Wise Count Overall after receiving list type (without Test Id)
        [HttpPost]
        [Route("getIDPathologyHFTypeWiseWithListType")]
        public async Task<IActionResult> getIDPathologyHFTypeWiseWithListType([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyHFTypeWiseWithListType(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region PathologyDashboard HF Type Wise Count Overall Count after receiving list type (without Test Id)
        [HttpPost]
        [Route("getIDPathologyPatientLineListWithListType")]
        public async Task<IActionResult> getIDPathologyPatientLineListWithListType([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPathologyPatientLineListWithListType(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion
        #region TB HR Apis
        #region TB total Count
        [HttpPost]
        [Route("getIDTbMainCount")]
        public async Task<IActionResult> getIDTbMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Tb HF Wise Count
        [HttpPost]
        [Route("getIDTbHFWiseAllCounts")]
        public async Task<IActionResult> getIDTbHFWiseAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbHFWiseAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseList
            });
        }
        #endregion
        #region tb HF Type Wise Count
        [HttpPost]
        [Route("getIDTbHFTypeWiseAllCounts")]
        public async Task<IActionResult> getIDTbHFTypeWiseAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbHFTypeWiseAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityTypeWiseList
            });
        }
        #endregion
        #region tb all counts
        [HttpPost]
        [Route("getIDTbAllCounts")]
        public async Task<IActionResult> getIDTbAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region tb list hfwise
        [HttpPost]
        [Route("getIDTbHFWiseList")]
        public async Task<IActionResult> getIDTbHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Tb list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDTbHFTypeWiseList")]
        public async Task<IActionResult> getIDTbHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region tb Patient Line List
        [HttpPost]
        [Route("getIDTbPatientLineList")]
        public async Task<IActionResult> getIDTbPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDTbPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion
        #region HCP HR Apis
        #region HCP total Count
        [HttpPost]
        [Route("getIDHCPMainCount")]
        public async Task<IActionResult> getIDHCPMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHCPMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region HCP all counts
        [HttpPost]
        [Route("getIDHCPAllCounts")]
        public async Task<IActionResult> getIDHCPAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHCPAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region HCP list hfwise
        [HttpPost]
        [Route("getIDHCPHFWiseList")]
        public async Task<IActionResult> getIDHCPHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHCPHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region HCP list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDHCPHFTypeWiseList")]
        public async Task<IActionResult> getIDHCPHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHCPHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region HCP Patient Line List
        [HttpPost]
        [Route("getIDHCPPatientLineList")]
        public async Task<IActionResult> getIDHCPPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDHCPPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion

        #region Dental HR Apis
        #region Dental total Count
        [HttpPost]
        [Route("getIDDentalMainCount")]
        public async Task<IActionResult> getIDDentalMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDDentalMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Dental all counts
        [HttpPost]
        [Route("getIDDentalAllCounts")]
        public async Task<IActionResult> getIDDentalAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDDentalAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Dental list hfwise
        [HttpPost]
        [Route("getIDDentalHFWiseList")]
        public async Task<IActionResult> getIDDentalHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDDentalHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Dental list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDDentalHFTypeWiseList")]
        public async Task<IActionResult> getIDDentalHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDDentalHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Dental Patient Line List
        [HttpPost]
        [Route("getIDDentalPatientLineList")]
        public async Task<IActionResult> getIDDentalPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDDentalPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion

        #region Physiotherapy HR Apis
        #region Physiotherapy total Count
        [HttpPost]
        [Route("getIDPhysiotherapyMainCount")]
        public async Task<IActionResult> getIDPhysiotherapyMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPhysiotherapyMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Physiotherapy all counts
        [HttpPost]
        [Route("getIDPhysiotherapyAllCounts")]
        public async Task<IActionResult> getIDPhysiotherapyAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPhysiotherapyAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Physiotherapy list hfwise
        [HttpPost]
        [Route("getIDPhysiotherapyHFWiseList")]
        public async Task<IActionResult> getIDPhysiotherapyHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPhysiotherapyHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Physiotherapy list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDPhysiotherapyHFTypeWiseList")]
        public async Task<IActionResult> getIDPhysiotherapyHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPhysiotherapyHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Physiotherapy Patient Line List
        [HttpPost]
        [Route("getIDPhysiotherapyPatientLineList")]
        public async Task<IActionResult> getIDPhysiotherapyPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPhysiotherapyPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion


        #region Psychiatry HR Apis
        #region Psychiatry total Count
        [HttpPost]
        [Route("getIDPsychiatryMainCount")]
        public async Task<IActionResult> getIDPsychiatryMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychiatryMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Psychiatry all counts
        [HttpPost]
        [Route("getIDPsychiatryAllCounts")]
        public async Task<IActionResult> getIDPsychiatryAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychiatryAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Psychiatry list hfwise
        [HttpPost]
        [Route("getIDPsychiatryHFWiseList")]
        public async Task<IActionResult> getIDPsychiatryHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychiatryHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Psychiatry list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDPsychiatryHFTypeWiseList")]
        public async Task<IActionResult> getIDPsychiatryHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychiatryHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Psychiatry Patient Line List
        [HttpPost]
        [Route("getIDPsychiatryPatientLineList")]
        public async Task<IActionResult> getIDPsychiatryPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychiatryPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion


        #region Occupational HR Apis
        #region Occupational total Count
        [HttpPost]
        [Route("getIDOccupationalMainCount")]
        public async Task<IActionResult> getIDOccupationalMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOccupationalMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Occupational all counts
        [HttpPost]
        [Route("getIDOccupationalAllCounts")]
        public async Task<IActionResult> getIDOccupationalAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOccupationalAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Occupational list hfwise
        [HttpPost]
        [Route("getIDOccupationalHFWiseList")]
        public async Task<IActionResult> getIDOccupationalHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOccupationalHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Occupational list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDOccupationalHFTypeWiseList")]
        public async Task<IActionResult> getIDOccupationalHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOccupationalHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Occupational Patient Line List
        [HttpPost]
        [Route("getIDOccupationalPatientLineList")]
        public async Task<IActionResult> getIDOccupationalPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDOccupationalPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion

        #region Nutrition HR Apis
        #region Nutrition total Count
        [HttpPost]
        [Route("getIDNutritionMainCount")]
        public async Task<IActionResult> getIDNutritionMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDNutritionMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Nutrition all counts
        [HttpPost]
        [Route("getIDNutritionAllCounts")]
        public async Task<IActionResult> getIDNutritionAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDNutritionAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Nutrition list hfwise
        [HttpPost]
        [Route("getIDNutritionHFWiseList")]
        public async Task<IActionResult> getIDNutritionHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDNutritionHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Nutrition list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDNutritionHFTypeWiseList")]
        public async Task<IActionResult> getIDNutritionHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDNutritionHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Nutrition Patient Line List
        [HttpPost]
        [Route("getIDNutritionPatientLineList")]
        public async Task<IActionResult> getIDNutritionPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDNutritionPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion

        #region SpeechTherapy HR Apis
        #region SpeechTherapy total Count
        [HttpPost]
        [Route("getIDSpeechTherapyMainCount")]
        public async Task<IActionResult> getIDSpeechTherapyMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDSpeechTherapyMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region SpeechTherapy all counts
        [HttpPost]
        [Route("getIDSpeechTherapyAllCounts")]
        public async Task<IActionResult> getIDSpeechTherapyAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDSpeechTherapyAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region SpeechTherapy list hfwise
        [HttpPost]
        [Route("getIDSpeechTherapyHFWiseList")]
        public async Task<IActionResult> getIDSpeechTherapyHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDSpeechTherapyHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region SpeechTherapy list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDSpeechTherapyHFTypeWiseList")]
        public async Task<IActionResult> getIDSpeechTherapyHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDSpeechTherapyHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region SpeechTherapy Patient Line List
        [HttpPost]
        [Route("getIDSpeechTherapyPatientLineList")]
        public async Task<IActionResult> getIDSpeechTherapyPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDSpeechTherapyPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion


        #region Psychology HR Apis
        #region Psychology total Count
        [HttpPost]
        [Route("getIDPsychologyMainCount")]
        public async Task<IActionResult> getIDPsychologyMainCount([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychologyMainCount(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion

        #region Psychology all counts
        [HttpPost]
        [Route("getIDPsychologyAllCounts")]
        public async Task<IActionResult> getIDPsychologyAllCounts([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychologyAllCounts(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = 0,
                data = list
            });
        }
        #endregion
        #region Psychology list hfwise
        [HttpPost]
        [Route("getIDPsychologyHFWiseList")]
        public async Task<IActionResult> getIDPsychologyHFWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychologyHFWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HealthFacilityWiseCountForList
            });
        }
        #endregion

        #region Psychology list HfType wise when sending with list type
        [HttpPost]
        [Route("getIDPsychologyHFTypeWiseList")]
        public async Task<IActionResult> getIDPsychologyHFTypeWiseList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychologyHFTypeWiseList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.HFTypeWiseCounts
            });
        }
        #endregion
        #region Psychology Patient Line List
        [HttpPost]
        [Route("getIDPsychologyPatientLineList")]
        public async Task<IActionResult> getIDPsychologyPatientLineList([FromBody] IDIntegratedDashboardDTOs model)
        {
            var list = await _dashboardService.getIDPsychologyPatientLineList(model);
            return Ok(new ResponseForIntegratedDashboad
            {
                totalRecords = list.TotalRecords,
                list = list.PatientLineList
            });
        }
        #endregion
        #endregion


        #endregion
        #region Helper Function
        [HttpGet]
        public dynamic SwitchConnectionBetweenTransAndReplication(dynamic model)
        {
            if (model.SwitchConnection)
            {
                return _dashboardTransService;
            }
            else
            {
                return _dashboardService;
             
            }
        }
        #endregion

    }
}

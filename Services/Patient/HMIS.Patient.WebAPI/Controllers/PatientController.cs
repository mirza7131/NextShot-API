//using AuthBAL;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.Patient;
using HMIS.Patient.Domain.Models.DTO.PatientContactDetailsDTO;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SMSSender;
using SMSSender.DTO;
using DbModel = HMIS.Patient.Domain.Models.DbModels;

namespace HMIS.Patient.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<PatientController> _logger;
        private readonly TokenService _tokenService;
        private readonly PatientService<DbModel.Patient> _PatientService;
        private readonly SMS _smsService;

        #endregion

        #region Constructor

        public PatientController(ILogger<PatientController> logger, 
            TokenService tokenService, 
            PatientService<DbModel.Patient> PatientService,
            SMS smsService
        )
        {
            _logger = logger;
            _tokenService = tokenService;
            _PatientService = PatientService;
            _PatientService = PatientService;
            _smsService = smsService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPatientDto input)
        {
            var obj = await _PatientService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditWithVisit")]
        public async Task<IActionResult> CreateOrEditWithVisit(CreateOrEditPatientWithVisitDto input)
        {
            var obj = await _PatientService.CreateOrEditWithVisit(input);

            //if (obj.PatientOpenVisits.Count() > 0)
            //{ 
            //    var visitDetail = await _PatientService.PatientVisitDetailById(obj.PatientOpenVisits.FirstOrDefault().PatientOpenVisitId);

            //    SendSMSDto smsObj = new SendSMSDto()
            //    {
            //        Receiver = input!.MobileNo!.Replace("-", ""),
            //        Body = $"Dear {input.FirstName}, Your visit is genearated in {visitDetail.HealthFacilityName} on {DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")} for {visitDetail.Section} against {visitDetail.Mrno}."
            //    };


            //    _smsService.SendSMS(smsObj);
            //}
            return Ok(new ResponseSave { message = "Record is Saved and Visit is Generated!", data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditWithVisitCentrally")]
        public async Task<IActionResult> CreateOrEditWithVisitCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        {
            var obj = await _PatientService.CreateOrEditWithVisitCentrally(input);
            if(input.RequestMode == (int)RequestModeEnum.UpdatePatient)
                return Ok(new ResponseSave { message = "Record is Saved!", data = obj });
            else if (input.RequestMode == (int)RequestModeEnum.CreateVisit)
                return Ok(new ResponseSave { message = "Visit is Generated!", data = obj });
            else
                return Ok(new ResponseSave { message = "Record is Saved and Visit is Generated!", data = obj });
        }


        [HttpPost]
        [Route("CreatePatientContactDetails")]
        public async Task<IActionResult> CreatePatientContactDetails(List<CreatePatientContactDetailsDTO> patientContactDetailsDTOs)
        {
            await _PatientService.CreatePatientContactDetails(patientContactDetailsDTOs);
            return Ok(new ResponseSuccess { message = "Patient Contact Details Saved Successfully" });
        }



        [HttpPost]
        [Route("CreateUnknownPatient")]
        public async Task<IActionResult> CreateUnknownPatient(CreateOrEditPatientWithVisitDto input)
        {
            var obj = await _PatientService.CreateUnknownPatient(input);

            //if (obj.PatientOpenVisits.Count() > 0)
            //{ 
            //    var visitDetail = await _PatientService.PatientVisitDetailById(obj.PatientOpenVisits.FirstOrDefault().PatientOpenVisitId);

            //    SendSMSDto smsObj = new SendSMSDto()
            //    {
            //        Receiver = input!.MobileNo!.Replace("-", ""),
            //        Body = $"Dear {input.FirstName}, Your visit is genearated in {visitDetail.HealthFacilityName} on {DateTime.Now.ToString("dd-MM-yyyy hh:mm tt")} for {visitDetail.Section} against {visitDetail.Mrno}."
            //    };


            //    _smsService.SendSMS(smsObj);
            //}
            return Ok(new ResponseSave { message = "Record is Saved and Visit is Generated!", data = obj });
        }

        [HttpPost]
        [Route("CreatePmis")]
        public async Task<IActionResult> CreatePmis(CreatePmisDto input)
        {
            var obj = await _PatientService.CreatePmis(input);
            return Ok(new ResponseSave { data = obj });
        } 
        
        
        [HttpPost]
        [Route("CreatePatientHistory")]
        public async Task<IActionResult> CreatePatientHistory(CreatePatientHistoryDTO input)
        {
            var obj = await _PatientService.CreatePatientHistory(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreatePatientLabTestExternally")]
        public async Task<IActionResult> CreatePatientLabTestExternally(List<CreateOrEditPatientExternallyDto> input)
        {
            var obj = await _PatientService.CreatePatientLabTestExternally(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("AddNewAdditionalPatientInfo")]
        public async Task<IActionResult> AddNewAdditionalPatientInfo(CreateOrEditAdditionalPatientDTO input)
        {
            var obj = await _PatientService.AddNewAdditionalPatientInfo(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CheckIfPatientVisitExist")]
        public async Task<IActionResult> CheckIfPatientVisitExist(Guid? PatientId, int? HealthFacilityId)
        {
            var obj = await _PatientService.CheckIfPatientVisitExist(PatientId, HealthFacilityId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _PatientService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }        
        
        
        [HttpPost]
        [Route("UpdatePrintStatusPatientDiagnose")]
        public async Task<IActionResult> UpdatePrintStatusPatientDiagnose(Guid PatientDiagnoseId)
        {
            await _PatientService.UpdatePrintStatusPatientDiagnose(PatientDiagnoseId);
            return Ok(new ResponseSuccess { });
        }
        
        [HttpPost]
        [Route("SaveFeePayment")]
        public async Task<IActionResult> SaveFeePayment(FeePayment feePayment)
        {
            var data = await _PatientService.SaveFeePayment(feePayment);
            return Ok(new ResponseSave { data = data });
        }
        [HttpPost]
        [Route("RefundFee")]
        public async Task<IActionResult> RefundFee(FeePayment feePayment)
        {
            var data = await _PatientService.RefundFee(feePayment);
            return Ok(new ResponseSave { data = data });
        }

        #endregion

        #region Read Operations



        //[HttpGet]
        //[Route("IsPatientAdviseMedicineInLastVisit")]
        //public async Task<IActionResult> IsPatientAdviseMedicineInLastVisit(string Cnic, int DeptId, int SectionId)
        //{
        //    var list = await _PatientService.IsPatientAdviseMedicineInLastVisit(Cnic,DeptId,SectionId);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _PatientService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
        //    return Ok(new ResponseSuccess { data = list });
        //}
        public async Task<IActionResult> GetAll([FromQuery] FilterPatientDto filterPatientDto)
        {
            var response = await _PatientService.GetAll(filterPatientDto);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _PatientService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetPatientAdditionalInfoByPatientId")]
        public async Task<IActionResult> GetPatientAdditionalInfoByPatientId(Guid PatientId)
        {
            var res = await _PatientService.GetPatientAdditionalInfoByPatientId(PatientId);
            return Ok(new ResponseSuccess { data = res });
        }

        [HttpGet]
        [Route("GetPatientAdditionalInfoByPatientIdPme")]
        public async Task<IActionResult> GetPatientAdditionalInfoByPatientIdPme(Guid PatientId)
        {
            var res = await _PatientService.GetPatientAdditionalInfoByPatientIdForPostMortem(PatientId);
            return Ok(new ResponseSuccess { data = res });
        }

        [HttpGet]
        [Route("GetByIdWithDetails")]
        public async Task<IActionResult> GetByIdWithDetails(Guid Id)
        {
            var obj = await _PatientService.GetByIdWithDetails(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllDetailsByVisitId")]
        public async Task<IActionResult> GetAllDetailsByVisitId(Guid PatientVisitId)
        {
            var obj = await _PatientService.GetAllDetailsByVisitId(PatientVisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByKeys")]
        public async Task<IActionResult> GetByKeys(string SearchKey, string SearchValue)
        {
            var obj = await _PatientService.GetByKeys(SearchKey, SearchValue);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByMrNo")]
        public async Task<IActionResult> GetByMrNo(string MrNo)
        {
            var obj = await _PatientService.GetByMrNo(MrNo);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("CheckInProfile")]
        public async Task<IActionResult> CheckInProfile(string SearchKey)
        {
            var obj = await _PatientService.CheckInProfile(SearchKey);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("PatientVisitHistory")]
        public async Task<IActionResult> PatientVisitHistory(Guid PatientId)
        {
            var obj = await _PatientService.PatientVisitHistory(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("PatientVisitDetailById")]
        public async Task<IActionResult> PatientVisitDetailById(Guid VisitId, Guid? DiagnoseId)
        {
            var obj = await _PatientService.PatientVisitDetailById(VisitId, DiagnoseId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetPatientDiagnoseRecordByVisitId")]
        public async Task<IActionResult> GetPatientDiagnoseRecordByVisitId(Guid VisitId, Guid? DiagnoseId)
        {
            var obj = await _PatientService.GetPatientDiagnoseRecordByVisitId(VisitId, DiagnoseId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("PatientVisitDetailsForVitalById")]
        public async Task<IActionResult> PatientVisitDetailsForVitalById(Guid VisitId)
        {
            var obj = await _PatientService.PatientVisitDetailsForVitalById(VisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetPatientVisitForDoctorById")]
        public async Task<IActionResult> GetPatientVisitForDoctorById(Guid VisitId, bool? IsFromPMIS, bool? isPatientHistory)
        {
            var obj = await _PatientService.GetPatientVisitForDoctorById(VisitId, IsFromPMIS, isPatientHistory);
            return Ok(new ResponseSuccess { data = obj });
        }
        [HttpGet]
        [Route("GetPatientVisitForMlcDoctorById")]
        public async Task<IActionResult> GetPatientVisitForMlcDoctorById(Guid VisitId, bool? IsFromPMIS, bool? isPatientHistory,string? FormTypeMlc)
        {
            var obj = await _PatientService.GetPatientVisitForMlcDoctorById(VisitId, IsFromPMIS, isPatientHistory, FormTypeMlc);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("PatientVisitDetailsForPharmacyById")]
        public async Task<IActionResult> PatientVisitDetailsForPharmacyById(Guid VisitId, string VisitType = CommonStringConstant.OPD)
        {
            var obj = await _PatientService.PatientVisitDetailsForPharmacyById(VisitId, VisitType);
            return Ok(new ResponseSuccess { data = obj });
        }



        [HttpGet]
        [Route("CheckRoomNoForSpeciality")]
        public async Task<IActionResult> CheckRoomNoForSpeciality(int healthFacilityId, int departmentId)
        {
            var data = await _PatientService.CheckRoomNoForSpeciality(healthFacilityId, departmentId);
            if(data)
                return Ok(new ResponseSuccess { data = CommonMessageConstant.RegisterRoomNoAndFloorNo });
            return Ok(new ResponseSuccess { data = "" });
        }


        [HttpGet]
        [Route("GetFeePaymments")]
        public async Task<IActionResult> GetFeePaymments([FromQuery] FilterSpecialityFeePaymentDto filter)
        {
            var data = await _PatientService.GetFeePaymments(filter);
            return Ok(new ResponseSuccess { data = data });
        }
        

        [HttpGet]
        [Route("GetAlmonerSpecialityFeeStat")]
        public async Task<IActionResult> GetAlmonerSpecialityFeeStat([FromQuery] FilterPatientDto filter)
        {
            var data = await _PatientService.GetAlmonerSpecialityFeeStat(filter);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetAnmonalSpecilityFeeListWithPagination")]
        public async Task<IActionResult> GetAnmonalSpecilityFeeListWithPagination([FromQuery] FilterDentalListPatientDto filter)
        {
            var data = await _PatientService.GetAnmonalSpecilityFeeListWithPagination(filter);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetAlmonerDentalProcedureStat")]
        public async Task<IActionResult> GetAlmonerDentalProcedureStat([FromQuery] FilterPatientDto filter)
        {
            var data = await _PatientService.GetAlmonerDentalProcedureStat(filter);
            return Ok(new ResponseSuccess { data = data });
        }

        [HttpGet]
        [Route("GetAlmonerDentalProcedureListWithPagination")]
        public async Task<IActionResult> GetAlmonerDentalProcedureListWithPagination([FromQuery] FilterDentalListPatientDto filter)
        {
            var data = await _PatientService.GetAlmonerDentalProcedureListWithPagination(filter);
            return Ok(new ResponseSuccess { data = data });
        }



        #endregion

        #region EmergencyDoctor

        [HttpGet]
        [Route("GetPatientVisitForEmergencyDoctorById")]
        public async Task<IActionResult> GetPatientVisitForEmergencyDoctorById(Guid VisitId, bool? IsFromPMIS, bool? isPatientHistory)
        {
            var obj = await _PatientService.GetPatientVisitForEmergencyDoctorById(VisitId, IsFromPMIS, isPatientHistory);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion

        #region IPD

        [HttpGet]
        [Route("PatientDischargeReceiptForIpdById")]
        public async Task<IActionResult> PatientDischargeReceiptForIpdById(Guid VisitId)
        {
            var obj = await _PatientService.PatientDischargeReceiptForIpdById(VisitId);
            return Ok(new ResponseSuccess { data = obj });
        }

        // Need to be Update when work on IPD see Emergency Refer que
        [HttpGet]
        [Route("GetAllQueForIpd")]
        public async Task<IActionResult> GetAllQueForIpd(int? HealthFacilityId)
        {
            var list = await _PatientService.GetAllQueForIpd(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllReferredQue")]
        public async Task<IActionResult> GetAllReferredQue(int? HealthFacilityId)
        {
            var list = await _PatientService.GetAllReferredQue(HealthFacilityId);
            return Ok(new ResponseSuccess { data = list });
        }

        #endregion

        #region Drug Addict


        [HttpPost]
        [Route("VerifyWithNADRA")]
        public async Task<IActionResult> VerifyWithNADRA(UnknownPatientDto UnknownPatientDto)
        {
            var obj = await _PatientService.VerifyWithNADRA(UnknownPatientDto);
            return Ok(new ResponseSuccess { data = obj });
        }
        
        [HttpGet]
        [Route("GetVerifiedPatientDataFromNADRA")]
        public async Task<IActionResult> GetVerifiedPatientDataFromNADRA(Guid PatientId)
        {
            var obj = await _PatientService.GetVerifiedPatientDataFromNADRA(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }   
        
        
        [HttpGet]
        [Route("GetVerifiedPatientDataFromNADRAForDeadBody")]
        public async Task<IActionResult> GetVerifiedPatientDataFromNADRAForDeadBody(Guid PatientId)
        {
            var obj = await _PatientService.GetVerifiedPatientDataFromNADRAForDeadBody(PatientId);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetUnknownPatientsList")]
        public async Task<IActionResult> GetUnknownPatientsList([FromQuery] FilterPatientVisitDto filter)
        {
            var obj = await _PatientService.GetUnknownPatientsList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }
        [HttpGet]
        [Route("GetMortauryPatientsList")]
        public async Task<IActionResult> GetMortauryPatientsList([FromQuery] FilterPatientVisitDto filter)
        {
            var obj = await _PatientService.GetMortauryPatientsList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }
        #endregion

        #region Tb

        [HttpGet]
        [Route("GetTbPatientsList")]
        public async Task<IActionResult> GetTbPatientsList([FromQuery] FilterPatientVisitDto filter)
        {
            var obj = await _PatientService.GetTbPatientsList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetEyeBlindnessPatientList")]
        public async Task<IActionResult> GetEyeBlindnessPatientList([FromQuery] FilterPatientVisitDto filter)
        {
            var obj = await _PatientService.GetEyeBlindnessPatientList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetExcelExportEyeBlindnessPatientList")]
        public async Task<IActionResult> GetExcelExportEyeBlindnessPatientList([FromQuery] FilterPatientVisitDto filter)
        {
            var obj = await _PatientService.GetExcelExportEyeBlindnessPatientList(filter);
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("GetTbPatientContactListByPatientId")]
        public async Task<IActionResult> GetTbPatientContactListByPatientId([FromQuery] FilterTbPatientContactDto filter)
        {
            var obj = await _PatientService.GetTbPatientContactListByPatientId(filter);
            return Ok(new ResponseSuccess { data = obj });
        }
        
        
        [HttpPost]
        [Route("UpdateSputumById")]
        public async Task<IActionResult> UpdateSputumById(Guid ContactId)
        {
            var obj = await _PatientService.UpdateSputumById(ContactId);
            return Ok(new ResponseSuccess { data = obj });
        }



        #endregion

        #region HCP
        [HttpGet]
        [Route("GetLostOfFollowupPatients")]
        public async Task<IActionResult> GetLostOfFollowupPatients([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientService.GetLostOfFollowupPatients(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("GetSvrPcrPendingPatients")]
        public async Task<IActionResult> GetSvrPcrPendingPatients([FromQuery] FilterPatientVisitDto filterPatientVisitDto)
        {
            var list = await _PatientService.GetSvrPcrPendingPatients(filterPatientVisitDto);
            return Ok(new ResponseSuccess { data = list });
        }
        #endregion
    }
}

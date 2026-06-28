using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppCommonMethods;
using AuthBAL;
using AutoMapper;
using Azure;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.Common;

using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;

using HMIS.Patient.Domain.Models.DTO.CreatePatientImagesDTO;

using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.Patient;
using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseRecordDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using PatientDto = HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Models.DTO.ProfileDto;
using HMIS.Patient.Domain.Repositories.UOW;
using HMIS.Patient.Service.Common;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMSSender;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using FileHandler;
using HMIS.Patient.Domain.Models.DTO.CreatePatinetFingerprintsDTO;
using HMIS.Aggregator.API;
using HMIS.Patient.Domain.Models.DTO.PatientContactDetailsDTO;
using System.Data;
using Microsoft.Data.SqlClient;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;

using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Models.NADRA;
using System.Text.Json;
using Microsoft.Identity.Client;
using AppCommonMethods.AppConstants;
using DTOs.UserDTO;
using MedicineDispatchDto = HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Models.DTO.AdditionalPatient;
using Microsoft.IdentityModel.Tokens;
using HMIS.Patient.Domain.Models.DTO.LostOfFollowupDto;
using HMIS.Patient.Domain.Models.DTO.SvrPcrPendingPatientsDto;
using System.Collections.ObjectModel;
using System.Collections;
using HMIS.Patient.Domain.Models.DTO.AlmonerDental;
using Microsoft.AspNetCore.Components.Forms;
using static System.Collections.Specialized.BitVector32;
using HMIS.Patient.Domain.Models.DTO.CreatePatientSource;
using Microsoft.Extensions.Logging;
//using DPUruNet;

namespace HMIS.Patient.Service
{
    public class PatientService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly PatientWorkFlowLogService<PatientWorkFlowLog> _patientWorkFlowLogService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly UnitOfWork<DbModel.PatientAdditionalInfo> _uowPatientAdditionalInfo;
        private readonly UnitOfWork<User> _uowUser;
        private readonly UnitOfWork<PatientOpenVisit> _uowPatientOpenVisit;
        private readonly UnitOfWork<DbModel.Person> _uowPerson;
        private readonly UnitOfWork<Tehsil> _uowTehsil;
        private readonly UnitOfWork<Province> _uowProvince;
        private readonly UnitOfWork<DbModel.Profile> _uowProfile;
        private readonly UnitOfWork<HealthFacilityStation> _uowHealthFacilityStation;
        private readonly UnitOfWork<PatientLocationPrefix> _uowPatientLocationPrefix;
        //private readonly PatientRepository<TEntity> _PatientRepository;
        private readonly bool _GenerateMrNo_OnEveryReg;
        private readonly bool _GenerateVisit_IfAlreadyExists;
        private readonly bool _IsRoomNo;
        private readonly IConfiguration _configMrNo;
        private readonly SMS _smsService;
        private readonly UploadFiles _fileUploader;
        private readonly bool _usePatientDataBank;

        private readonly PatientVitalService<PatientVital> _patientVitalService;
        private readonly PatientDiagnoseService<PatientDiagnose> _patientDiagnoseService;
        private readonly MedicineDispatchService<MedicineDispatch> _medicineDispatchService;
        private readonly PatientVisitFlowService<PatientVisitFlow> _patientVisitFlowService;
        private readonly PatientAdmissionDetailService<PatientAdmissionDetail> _patientAdmissionDetailService;
        private readonly ProfileTypeService _profileTypeService;
        private readonly RequestForNadraVerfication _requestForNadraVerfication;
        private readonly VerifiedPatientDataFromNADRAService _verifiedPatientDataFromNADRAService;
        private readonly NCDService _nCDService;
        private readonly bool _isPatientSearchViaHubDb;
        private readonly bool _isPatientSearchUsingSP;



        #endregion

        #region Constructor

        public PatientService(TokenService tokenService, PatientWorkFlowLogService<PatientWorkFlowLog> patientWorkFlowLogService, UnitOfWork<DbModel.Patient> uowPatient, UnitOfWork<User> uowUser, UnitOfWork<PatientOpenVisit> uowPatientOpenVisit, UnitOfWork<DbModel.Person> uowPerson, UnitOfWork<Tehsil> uowTehsil, UnitOfWork<DbModel.Profile> uowProfile, UnitOfWork<HealthFacilityStation> uowHealthFacilityStation, UnitOfWork<PatientLocationPrefix> uowPatientLocationPrefix, IMapper mapper, IConfiguration config, IConfiguration configMrNo
            , UnitOfWork<Province> uowProvince, UnitOfWork<PatientAdditionalInfo> uowPatientAdditionalInfo, UploadFiles fileUploader, SMS smsService,
            PatientVitalService<PatientVital> patientVitalService,
            PatientDiagnoseService<PatientDiagnose> patientDiagnoseService,
            MedicineDispatchService<MedicineDispatch> medicineDispatchService,
            PatientVisitFlowService<PatientVisitFlow> patientVisitFlowService,
            PatientAdmissionDetailService<PatientAdmissionDetail> patientAdmissionDetailService,
            ProfileTypeService profileTypeService,
            RequestForNadraVerfication requestForNadraVerfication,
            VerifiedPatientDataFromNADRAService verifiedPatientDataFromNADRAService,
            NCDService nCDService

            )
        {
            _tokenService = tokenService;
            _patientWorkFlowLogService = patientWorkFlowLogService;
            _uowPatient = uowPatient;
            _uowPatientAdditionalInfo = uowPatientAdditionalInfo;
            _uowUser = uowUser;
            _uowPatientOpenVisit = uowPatientOpenVisit;
            _uowPerson = uowPerson;
            _uowTehsil = uowTehsil;
            _uowProfile = uowProfile;
            _uowHealthFacilityStation = uowHealthFacilityStation;
            _uowProvince = uowProvince;
            _mapper = mapper;
            _uowPatientLocationPrefix = uowPatientLocationPrefix;
            _GenerateMrNo_OnEveryReg = bool.Parse(config.GetSection("MrNoConfig").GetSection("GenerateMrNo_OnEveryReg").Value);
            _GenerateVisit_IfAlreadyExists = bool.Parse(config.GetSection("VisitGenerationConfig").GetSection("GenerateVisit_IfAlreadyExists").Value);
            _IsRoomNo = bool.Parse(config.GetSection("RoomNoConfiguration").GetSection("IsRoomNo").Value);
            _configMrNo = configMrNo;
            _smsService = smsService;
            _patientVitalService = patientVitalService;
            _patientDiagnoseService = patientDiagnoseService;
            _medicineDispatchService = medicineDispatchService;
            _patientVisitFlowService = patientVisitFlowService;
            _patientAdmissionDetailService = patientAdmissionDetailService;
            _fileUploader = fileUploader;
            _profileTypeService = profileTypeService;
            _requestForNadraVerfication = requestForNadraVerfication;
            _verifiedPatientDataFromNADRAService = verifiedPatientDataFromNADRAService;
            _nCDService = nCDService;

            _usePatientDataBank = bool.Parse(config.GetSection("UsePatientDataBank").Value);

            _isPatientSearchViaHubDb = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                               config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActivePatientSearchViaDbHub") :
                               (
                               config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActivePatientSearchViaDbHub") :
                                false
                               );
            _isPatientSearchUsingSP = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                               config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("PatientSearchUsingSP") :
                               (
                               config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("PatientSearchUsingSP") :
                                false
                               );

        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDto> CreateOrEdit(CreateOrEditPatientDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDto> Create(CreateOrEditPatientDto input)
        {
            // Get Last Mrno
            var dbObj = await _uowTehsil.Repository.GetALL(x => x.TehsilId == input.TehsilId).Select(y => new
            {
                TehsilCode = y.Code,
                Mrno = y.Patients.OrderByDescending(x => x.Mrno).Select(x => x.Mrno).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            //if self the generate Mrno
            if (input.IsSelf == true)
            {
                input.RelationProfileId = null;

                var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic).Count();

                if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);

                if (string.IsNullOrEmpty(dbObj?.Mrno))
                    input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
                else
                    input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            }
            else
                input.Mrno = null;

            var obj = _mapper.Map<DbModel.Patient>(input);
            FillEntity(obj);
            DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
            await _uowPatient.Save();
            return _mapper.Map<CreateOrEditPatientDto>(responseObj);
        }

        private async Task<CreateOrEditPatientDto> Update(CreateOrEditPatientDto input)
        {
            var dbObj = await _uowPatient.Repository.GetById(input.PatientId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            if (dbObj?.Cnic != input.Cnic)
            {
                var dbCnic = await _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic).Select(x => x.Cnic).FirstOrDefaultAsync();
                if (dbCnic == input.Cnic)
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);
            }

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatient.Repository.Update(obj!);
            await _uowPatient.CommitAsync();

            return _mapper.Map<CreateOrEditPatientDto>(obj);
        }

        private async Task<CreateOrEditPatientDto> CreateDuplicateVisit(CreateOrEditPatientDto input)
        {
            // Get Last Mrno
            var dbObj = await _uowTehsil.Repository.GetALL(x => x.TehsilId == input.TehsilId).Select(y => new
            {
                TehsilCode = y.Code,
                Mrno = y.Patients.OrderByDescending(x => x.Mrno).Select(x => x.Mrno).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            //if self the generate Mrno
            if (input.IsSelf == true)
            {
                input.RelationProfileId = null;

                var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic).Count();

                if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);

                if (string.IsNullOrEmpty(dbObj?.Mrno))
                    input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
                else
                    input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            }
            else
                input.Mrno = null;

            var obj = _mapper.Map<DbModel.Patient>(input);
            FillEntity(obj);
            DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
            await _uowPatient.Save();
            return _mapper.Map<CreateOrEditPatientDto>(responseObj);
        }

        public async Task<bool> CheckRoomNoForSpeciality(int healthFacilityId, int departmentId)
        {
            bool isNullProperty = false;
            if (_IsRoomNo)
            {
                if (!AppCommonMethod.IsNullorZeroInt(healthFacilityId))
                {
                    var user = await _uowUser.Repository.GetById(_tokenService.GetUserId());

                    if (AppCommonMethod.IsNullObject(user))
                        return isNullProperty;

                    var data = await _uowPatient.GetDbContext().ViewSpecialityRoomNos.Where(x => x.HealthFacilityId == healthFacilityId)
                        .WhereIf(!AppCommonMethod.IsNullorZeroInt(user.DepartmentId), x => x.DepartmentLookupId == user.DepartmentId).ToListAsync();
                    if (!AppCommonMethod.IsNullOrEmptyList(data))
                    {
                        foreach (var _data in data)
                        {
                            isNullProperty = AppCommonMethod.IsAnyObjectPropertyNull(_data);
                            if (isNullProperty)
                                return isNullProperty;
                        }
                    }
                }
            }


            return isNullProperty;
        }

        public async Task<dynamic> CreateOrEditWithVisit(CreateOrEditPatientWithVisitDto input)
        {
            // if patient id is null and Mode is Create
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId) && input.RequestMode == (int)RequestModeEnum.CreatePatientWithVisit)
                return await CreateWithVisit(input);
            else
            {
                // if patient id is available and Mode is Update
                if (input.RequestMode == (int)RequestModeEnum.UpdatePatientWithVisit)
                    return await UpdateWithVisit(input);
                // if Patient Id is available and Mode is only Create Visit
                else
                    return await CreateVisit(input);
            }


            //if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId))
            //    return await CreateWithVisit(input);
            //else
            //    return await UpdateWithVisit(input);
        }

        private async Task<dynamic> CreateWithVisit(CreateOrEditPatientWithVisitDto input)
        {

            #region DRTB
            if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
            {
                if (input.DRTBPatientStatus == CommonStringConstant.Refered)
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientIsNotReferedToDRTB);
                }
            }
            #endregion

            // ADD DASHES IN CNIC
            if (!string.IsNullOrEmpty(input.Cnic))
            {
                if (!input.Cnic.Contains("-"))
                {
                    input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
                    input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
                }
            }
            else if (!string.IsNullOrEmpty(input.PassportNo))
            {
                input.Cnic = input.PassportNo;
            }

            var isVisitClose = false;
            var tokenUserId = _tokenService.GetUserId();
            var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            input.FullName = input.FirstName + " " + input.LastName;
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                input.HealthFacilityId = user?.HealthFacilityId;
            if (String.IsNullOrEmpty(input.GuardianName))
                input.GuardianName = input.LastName;


            if ((bool)input?.isEyeBlindness)
            {
                isVisitClose = true;
            }

            if (input.IsSelf == true)
            {
                //input.RelationProfileId = null;
                input.NameOfCnicHolder = input.FullName;
                if (input.PatientRole != CommonStringConstant.DrugAddict && input.Category != CommonStringConstant.Unknown)
                {
                    var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.IsSelf == true).Count();

                    if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);

                    if (!string.IsNullOrEmpty(input.PassportNo))
                    {
                        var CheckPassportExists = _uowPatient.Repository.GetALL(x => x.PassportNo == input.PassportNo && x.IsSelf == true).Count();

                        if (!AppCommonMethod.IsNullorZeroInt(CheckPassportExists))
                            throw new UserFriendlyException(CommonMessageConstant.PatientExistWithPassport);
                    }
                }
            }
            else
            {
                var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.FirstName == input.FirstName && x.RelationProfileId == input.RelationProfileId).Count();

                if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithRelationCnic);


                if (!string.IsNullOrEmpty(input.PassportNo))
                {
                    var CheckPassportExists = _uowPatient.Repository.GetALL(x => x.PassportNo == input.PassportNo && x.IsSelf == true).Count();

                    if (!AppCommonMethod.IsNullorZeroInt(CheckPassportExists))
                        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithPassport);
                }
            }

            // if no Section then set default section 
            if (AppCommonMethod.IsNullorZeroInt(input.SectionLookupId))
            {
                var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
                if (defaultSec != null)
                {
                    input.SectionLookupId = defaultSec.SectionLookupId;
                }
            }


            //check for next station from Health Facility
            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.StationProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.StationProfile!.ShortName,
                    Name = x.StationProfile.Name,
                }).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);

            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                // SMS on Visit Close
                //SendSMSDto smsObj = new SendSMSDto()
                //{
                //    Receiver = input!.MobileNo!.Replace("-", ""),
                //    //Body = $"Dear {input.FirstName}, Thank You for Your Visit '\n' Test."

                //    Body = $"معزز {input.FirstName}   ،\r\nڈسٹرکٹ ہیڈ کوارٹر ہسپتال قصور تشریف آوری کا شکریہ\r\nآپ کو اوپی ڈی میں مندرجہ ذیل ادویات مفت فراہم کی گئی ہیں۔\r\n پیناڈول : صبح ، دوپہر، شام"
                //};

                //_smsService.SendSMS(smsObj);
            }

            // Get Last Mrno
            var dbObj = await _uowTehsil.Repository.GetALL(x => x.TehsilId == input.TehsilId).Select(y => new
            {
                TehsilCode = y.Code,
                Mrno = y.Patients.OrderByDescending(x => x.Mrno).Select(x => x.Mrno).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            // Mrno Generation

            input.Mrno = await GenerateMRNNo(input);

            //if (_GenerateMrNo_OnEveryReg)
            //{
            //    if (string.IsNullOrEmpty(dbObj?.Mrno))
            //        input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
            //    else
            //        input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            //}
            //else
            //{
            //    if (input.IsSelf == true)
            //    {
            //        if (string.IsNullOrEmpty(dbObj?.Mrno))
            //            input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
            //        else
            //            input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            //    }
            //    else
            //        input.Mrno = null;
            //}

            // get Default Visit Type ID

            Guid? VisitType = null;
            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
                VisitType = null;

            // if Doctor Register then Attended By User
            if (input.IsDoctor)
                input.Doctor = tokenUserId;
            //Guid systemSourceId = null;
            //if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            //{
            //    var res = await _authProfileTypeService.GetProfileByProfileType(CommonStringConstant._DrugAddict);

            //    //systemSourceId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant._DrugAddict).Select(x => x.ProfileId).FirstOrDefaultAsync();



            //}

            bool IsPatientUnknown = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict && input.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            bool IsDrugAddict = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict)
            {
                IsDrugAddict = true;
            }
            // Generate New TokenNo
            var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);

            // Except Emergency Confirm visit when Generate or Refer
            //var isAdmitted = (input.VisitFor != CommonStringConstant.ER) ? true : false;
            var isAdmitted = true;

            //db
            var obj = _mapper.Map<DbModel.Patient>(input);
            FillEntity(obj);
            obj.IsPatientUnknown = IsPatientUnknown;


            var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);
            obj.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                Age = obj.Age,
                AgeTypeProfileId = ageProfileId,
                TokenNo = NewTokenNo,
                SlipNo = input.SlipNo,
                VisitFor = input.VisitFor,
                VisitSource = input.VisitSource,
                IsAdmitted = isAdmitted,
                VisitTypeProfileId = VisitType, // Default Visit Type Id Set to Walk In
                HealthFacilityId = input.HealthFacilityId,
                VisitNo = 1,
                DepartementLookupId = input.DepartmentLookupId,
                SectionLookupId = input.SectionLookupId,
                CurrentStationProfileId = nextStation!.StationProfileId,
                BedNo = input.BedNo,
                //SourceSystemId = systemSourceId,
                VisitDate = input.VisitDate == null ? DateTime.Now : input.VisitDate,
                IsFromPmis = input.IsFromPMIS,
                IsFromCallCenter = input.IsFromCallCenter,
                AttendedBy = input.Doctor,
                IsDischarge = isVisitClose,
                IsVitalSkip = input.IsVitalSkip,
                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create,
                SscNumber = input.SscNumber,
                IsEligibleForSsc = input.IsEligibleForSsc,
                ReasonIfNotEligibleForSsc = input.ReasonIfNotEligibleForSsc,
                IsDrugAddict = IsDrugAddict,
                PatinetPrivateHeathFacilityId = input.PatinetPrivateHeathFacilityId
            });

            if (!AppCommonMethod.IsNullOrEmptyGuid(_tokenService.GetUserEventId()))
            {
                var _uowEventVisit = new UnitOfWork<EventVisit>(_uowPatient.GetDbContext());
                EventVisit eventVisit = new EventVisit();

                eventVisit.EventVisitId = Guid.NewGuid();
                eventVisit.EventId = _tokenService.GetUserEventId() ?? Guid.Empty;
                eventVisit.HealthFacilityId = TokenService.GetUserHfId();
                eventVisit.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
                eventVisit.IsActive = true;
                eventVisit.CreatedOn = DateTime.Now;
                eventVisit.CreatedBy = _tokenService.GetUserId();
                eventVisit.ActionTypeId = (int)ActionTypeEnum.Create;

                await _uowEventVisit.Repository.Insert(eventVisit);
                await _uowEventVisit.Save();
            }


            if (input.IsFromIPD == true)
            {
                foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
                {
                    FillEntityAdmissionDetails(patientAdmissionDetail);
                    patientAdmissionDetail.PatientId = obj.PatientId;
                    patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                    if (input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
                    {
                        patientAdmissionDetail.ShiftedFromHealthFacilityId = input.ReferredHealthFacilityId;
                        patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.ReferredByDepartmentLookupId;
                        patientAdmissionDetail.ShiftedFromSectionLookupId = input.ReferredBySectionLookupId;
                    }

                    patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.PatientId);
                    patientAdmissionDetail.IpdSource = input!.IPDSource;
                    patientAdmissionDetail.HealthFacilityId = input!.HealthFacilityId;
                    patientAdmissionDetail.DepartmentLookupId = input!.DepartmentLookupId;
                    patientAdmissionDetail.SectionLookupId = input!.SectionLookupId;

                }
            }

            DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
            await _uowPatient.Save();

            input.PatientId = responseObj.PatientId;



            if ((bool)input?.isEyeBlindness)
            {
                var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
                PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
                patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input);
                FillEntityPatientEyeBlindness(patientEyeBlindness);
                if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
                {
                    patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
                }
                await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
                await _uowPatientEyeBlindness.Save();

            }

            if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            {
                await SaveDrugAddictPatientData(input);
            }
            else if (input.IsFromER == true)
            {
                await SaveDrugAddictPatientData(input);
            }

            #region PatientAdditionalInfo
            //_uowPatientAdditionalInfo.Repository.Insert();
            if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
            {
                if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
                {
                    var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
                    additioanlPatientInfo.PatientId = input.PatientId;
                    additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
                    //additonalInfo.PatientVisitId=

                    additioanlPatientInfo.CreatedOn = DateTime.Now;
                    additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

                    var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
                    await _uowPatientAdditionalInfo.Save();
                }

            }
            #endregion



            if (input.PatientRole == CommonStringConstant.DrugAddict && input.Category == CommonStringConstant.Unknown)
            {
                var UnknownPatientObj = _mapper.Map<UnknownPatientDto>(responseObj);
                UnknownPatientObj.PatientRole = input.PatientRole;

                var response = await VerifyWithNADRA(UnknownPatientObj);
                //if (Int32.Parse(response.code) != 200)
                //{
                //    throw new UserFriendlyException(response.message);
                //}
            }


            //update DataBank
            if (input.IsDataBankRecord)
            {
                DataBankService _dataBankService = new DataBankService();

                if (input.IsSelf == true)
                    await _dataBankService.UpdateHMISFlagWithCnic(input.Cnic);
                else
                    await _dataBankService.UpdateHMISFlagWithId(input.DataBankId);
            }

            //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
            var diagnoseResponse = false;
            if (input.IsFromCallCenter == true)
                diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);


            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = responseObj.PatientId;
            objPatientWorkFlowLog.PatientVisitId = responseObj.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = responseObj.HealthFacilityId;
            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(responseObj.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


            // Create Patient Visit Flow
            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.SectionLookupId).FirstOrDefaultAsync();
            objPatientVisitFlow.CurrentDepartmentId = input.DepartmentLookupId;
            objPatientVisitFlow.CurrentSectionId = input.SectionLookupId;
            objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;
            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


            if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.VisitDate))
            {
                // TB Patient Source
                if (!AppCommonMethod.IsNullObject(input.TbPatientSource))
                {
                    var _uowPatientSourceInfo = new UnitOfWork<PatientSourceInfo>(_uowPatient.GetDbContext());
                    PatientSourceInfo patientSourceInfo = new PatientSourceInfo();
                    FillEntityPatientSourceInfo(patientSourceInfo);
                    patientSourceInfo.PatientId = input.PatientId;
                    patientSourceInfo.PatientVisitId = (obj.PatientOpenVisits.Count() > 0) ? obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId : null;
                    patientSourceInfo.PatientSourceProfileId = input.TbPatientSource!.TbPatientSource;
                    patientSourceInfo.Name = input.TbPatientSource!.Name;
                    patientSourceInfo.ContactNo = input.TbPatientSource!.ContactNo;
                    patientSourceInfo.CNIC = input.TbPatientSource!.CNIC;
                    patientSourceInfo.HealthFacility = input.TbPatientSource!.NameofFacility;
                    patientSourceInfo.LHSName = input.TbPatientSource!.LHSName;
                    patientSourceInfo.LHSContactNo = input.TbPatientSource!.LHSContactNo;
                    patientSourceInfo.LHSCNIC = input.TbPatientSource!.LHSCNIC;

                    await _uowPatientSourceInfo.Repository.Insert(patientSourceInfo);
                    await _uowPatientSourceInfo.Save();
                }

                // Tb Patient SOurce


                var _uowtbpatientdetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                TbPatientDetail tbpatientdetail = new TbPatientDetail();
                FillEntityTbPatientDetails(tbpatientdetail);
                tbpatientdetail.PatientId = obj.PatientId;
                tbpatientdetail.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
                tbpatientdetail.PatientTypeProfileId = input.TbPatientTypeId;
                if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTreatmentLengthId))
                {
                    tbpatientdetail.PatientLengthOfInterruptionProfileId = input.TbPatientTreatmentLengthId;
                }
                if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientLengthOfInterruptionId))
                {
                    tbpatientdetail.PatientLengthOfInterruptionProfileId = input.TbPatientLengthOfInterruptionId;
                }
                tbpatientdetail.NoOfMedicineTaken = input.TbPatientNoOfMedicineTaken;
                tbpatientdetail.NoOfMonthsMedicineIssued = input.NoOfMonthsMedicineIssued;
                tbpatientdetail.PatientTreatmentCycleNo = objPatientVisitFlow.PatientTreatmentCycleNo;

                //if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
                //    tbpatientdetail.PatientStatus = input.DRTBPatientStatus;

                await _uowtbpatientdetails.Repository.Insert(tbpatientdetail);
                await _uowtbpatientdetails.Save();
            }

            var resp = _mapper.Map<CreateOrEditPatientWithVisitDto>(responseObj);
            var loginUser = TokenService.GetUserLoggedInfo();
            if (!string.IsNullOrEmpty(loginUser.DesignationName))
                resp.CreatedByName += " (" + loginUser.DesignationName + ")";
            //resp.CreatedOn = responseObj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return resp;
        }


        private async Task SaveTbPatientDetails(CreateOrEditPatientWithVisitDto input, CreateOrEditPatientVisitFlowDto objPatientVisitFlow)
        {
            var userObj = await GetLoggedInfoUserDTO();

            // TB Patient Source
            if(!AppCommonMethod.IsNullObject(input.TbPatientSource))
            {
                var _uowPatientSourceInfo = new UnitOfWork<PatientSourceInfo>(_uowPatient.GetDbContext());
                PatientSourceInfo patientSourceInfo = new PatientSourceInfo();
                FillEntityPatientSourceInfo(patientSourceInfo);
                patientSourceInfo.PatientId = input.PatientId;
                patientSourceInfo.PatientVisitId = objPatientVisitFlow.PatientVisitId;
                patientSourceInfo.PatientSourceProfileId = input.TbPatientSource!.TbPatientSource;
                patientSourceInfo.Name = input.TbPatientSource!.Name;
                patientSourceInfo.ContactNo = input.TbPatientSource!.ContactNo;
                patientSourceInfo.CNIC = input.TbPatientSource!.CNIC;
                patientSourceInfo.HealthFacility = input.TbPatientSource!.NameofFacility;
                patientSourceInfo.LHSName = input.TbPatientSource!.LHSName;
                patientSourceInfo.LHSContactNo = input.TbPatientSource!.LHSContactNo;
                patientSourceInfo.LHSCNIC = input.TbPatientSource!.LHSCNIC;

                await _uowPatientSourceInfo.Repository.Insert(patientSourceInfo);
                await _uowPatientSourceInfo.Save();
            }
            
            // Tb Patient SOurce


            
            var _uowtbpatientdetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
            TbPatientDetail tbpatientdetail = new TbPatientDetail();
            FillEntityTbPatientDetails(tbpatientdetail);
            tbpatientdetail.PatientId = input.PatientId;
            tbpatientdetail.PatientVisitId = objPatientVisitFlow.PatientVisitId;
            tbpatientdetail.PatientTypeProfileId = input.TbPatientTypeId;
            if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTreatmentLengthId))
            {
                tbpatientdetail.PatientLengthOfInterruptionProfileId = input.TbPatientTreatmentLengthId;
            }
            if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientLengthOfInterruptionId))
            {
                tbpatientdetail.PatientLengthOfInterruptionProfileId = input.TbPatientLengthOfInterruptionId;
            }
            tbpatientdetail.NoOfMedicineTaken = input.TbPatientNoOfMedicineTaken;
            tbpatientdetail.NoOfMonthsMedicineIssued = input.NoOfMonthsMedicineIssued;
            tbpatientdetail.PatientTreatmentCycleNo = objPatientVisitFlow.PatientTreatmentCycleNo;

            if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
                tbpatientdetail.PatientStatus = input.DRTBPatientStatus;

            if (userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0)
                tbpatientdetail.IsReferToDrtb = true;

            await _uowtbpatientdetails.Repository.Insert(tbpatientdetail);
            await _uowtbpatientdetails.Save();
        }

        private async Task<dynamic> UpdateWithVisit(CreateOrEditPatientWithVisitDto input)
        {
            #region PatientAdditionalInfo

            //_uowPatientAdditionalInfo.Repository.Insert();
            if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
            {
                if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
                {

                    if (AppCommonMethod.IsNullOrEmptyGuid(input.CreateOrEditAdditionalPatient.PatientAdditionalInfoId))
                    {
                        var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
                        additioanlPatientInfo.PatientId = input.PatientId;
                        additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
                        //additonalInfo.PatientVisitId=

                        additioanlPatientInfo.CreatedOn = DateTime.Now;
                        additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

                        var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
                        await _uowPatientAdditionalInfo.Save();
                    }
                    else
                    {
                        var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
                        additioanlPatientInfo.PatientId = input.PatientId;

                        //additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
                        //additonalInfo.PatientVisitId=

                        additioanlPatientInfo.UpdatedOn = DateTime.Now;
                        additioanlPatientInfo.UpdatedBy = _tokenService.GetUserId();

                        _uowPatientAdditionalInfo.Repository.Update(additioanlPatientInfo);
                        await _uowPatientAdditionalInfo.Save();
                    }
                }
            }
            #endregion

            // check if user exist and values are edited 
            var dbObj = await _uowPatient.Repository.GetById(input.PatientId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            if (string.IsNullOrEmpty(dbObj.Mrno))
            {
                input.Mrno = await GenerateMRNNo(input);
            }

            // if no Section then set default section 
            if (AppCommonMethod.IsNullorZeroInt(input.SectionLookupId))
            {
                var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
                if (defaultSec != null)
                {
                    input.SectionLookupId = defaultSec.SectionLookupId;
                }
            }

            #region DRTB
            if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
            {
                await CheckPatientDRTBReferedStatus(input);
            }
            #endregion

            // If Patient Is Referred then Fill Patient Entity From DB
            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                CreateOrEditPatientDto dbPatient = new CreateOrEditPatientDto();
                dbPatient = _mapper.Map(dbObj, dbPatient);
                input = _mapper.Map(dbPatient, input);
            }


            var isVisitClose = false;
            var user = TokenService.GetUserLoggedInfo();
            var tokenUserId = _tokenService.GetUserId();
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
            {
                var HfId = TokenService.GetUserHfId();
                input.HealthFacilityId = HfId;
            }

            // ******** Check and Remove
            //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            input.FullName = input.FirstName + " " + input.LastName;


            if (input.IsSelf == true)
                input.NameOfCnicHolder = input.FullName;

            if (String.IsNullOrEmpty(input.GuardianName))
                input.GuardianName = input.LastName;

            if (!string.IsNullOrEmpty(dbObj.Mrno))
            {
                if (input.Cnic != dbObj?.Cnic || input.Mrno != dbObj?.Mrno || input.IsSelf != dbObj?.IsSelf)
                    throw new UserFriendlyException(CommonMessageConstant.CannotEditRecord);
            }

            // ADD DASHES IN CNIC
            if (!string.IsNullOrEmpty(input.Cnic))
            {
                if (!input.Cnic.Contains("-") && input.Cnic.Length == 13)
                {
                    input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
                    input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
                }
            }


            // Check if Visit Exist in IPD against Patient
            //if (input.IsFromIPD == true)
            //{
            //    var visitExist = await CheckIfPatientVisitExistInIPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);
            //}
            //// Check if Visit Exist in Emergency against Patient
            //else if (input.IsFromER == true)
            //{
            //    var visitExist = await CheckIfPatientVisitExistInER(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
            //}
            // check if OPD
            //else
            //{
            //    var visitExist = await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

            //}

            // check if Patient Is Already Exist in Departments
            var isVisitExist = await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.VisitFor);

            if (!AppCommonMethod.IsNullObject(isVisitExist))
            {
                isVisitExist.IsPatientFromVisitList = true;
                return isVisitExist;
            }
                
            //check for next station
            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.StationProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.StationProfile!.ShortName,
                Name = x.StationProfile.Name,
            }).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);
            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();


            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station


                //// SMS on Visit Close
                //SendSMSDto smsObj = new SendSMSDto()
                //{
                //    Receiver = input!.MobileNo!.Replace("-", ""),
                //    Body = $"Dear {input.FirstName}, Thank You for Your Visit."
                //};

                //_smsService.SendSMS(smsObj);
            }

            // check if visit is already generated
            //if (_GenerateVisit_IfAlreadyExists)
            //{
            //    var checkIfVisitAlreadyExists = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId!).Where(x => x.IsDischarge != true && x.IsActive == true).Count();
            //    if (checkIfVisitAlreadyExists > 0)
            //        throw new UserFriendlyException(CommonMessageConstant.VisitIsAlreadyGenerated);
            //}

            Guid? VisitType = null;
            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
                VisitType = null;

            // if Doctor Register then Attended By User
            if (input.IsDoctor)
                input.Doctor = tokenUserId;

            //dbObj.PatientOpenVisits.Clear();
            bool IsPatientUnknown = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict && input.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            //db
            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            bool IsDrugAddict = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict)
            {
                IsDrugAddict = true;
            }
            // Generate New TokenNo
            var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);

            // Except Emergency Confirm visit when Generate or Refer
            //var isAdmitted = (input.VisitFor != CommonStringConstant.ER) ? true : false;
            var isAdmitted = true;

            obj.IsPatientUnknown = IsPatientUnknown;

            var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);
            obj!.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                Age = obj.Age,
                AgeTypeProfileId = ageProfileId,
                PatientId = obj.PatientId,
                TokenNo = NewTokenNo,
                SlipNo = input.SlipNo,
                VisitFor = input.VisitFor,
                VisitSource = input.VisitSource,
                IsAdmitted = isAdmitted,
                VisitNo = UpdateVisitNo(input.PatientId),
                VisitTypeProfileId = VisitType,
                HealthFacilityId = input.HealthFacilityId,
                BedNo = input.BedNo,
                //HealthFacilityName = input
                DepartementLookupId = input.DepartmentLookupId,
                SectionLookupId = input.SectionLookupId,

                CurrentStationProfileId = nextStation!.StationProfileId,
                VisitDate = input.VisitDate == null ? DateTime.Now : input.VisitDate,
                IsFromPmis = input.IsFromPMIS,
                IsFromCallCenter = input.IsFromCallCenter,
                AttendedBy = input.Doctor,
                IsDischarge = isVisitClose,
                IsVitalSkip = input.IsVitalSkip,

                ParentPatientOpenVisitId = input.ReferVisitId,
                IsReferred = input.IsReferred,
                ReferredHealthFacilityId = input.ReferredHealthFacilityId,
                ReferredDepartmentLookupId = input.ReferredByDepartmentLookupId,
                ReferredSectionLookupId = input.ReferredBySectionLookupId,
                ReferredBy = input.ReferredBy,

                IsReferredIpd = input.IsReferredIpd,
                IpdReferredByDepartmentLookupId = (input.IsReferredIpd == true) ? input.ReferredByDepartmentLookupId : null,
                IpdReferredBySectionLookupId = (input.IsReferredIpd == true) ? input.ReferredBySectionLookupId : null,
                IpdReferredBy = (input.IsReferredIpd == true) ? input.ReferredBy : null,

                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create,
                SscNumber = input.SscNumber,
                IsEligibleForSsc = input.IsEligibleForSsc,
                ReasonIfNotEligibleForSsc = input.ReasonIfNotEligibleForSsc,
                IsDrugAddict = IsDrugAddict,
                PatinetPrivateHeathFacilityId = input.PatinetPrivateHeathFacilityId
            });

            if (!AppCommonMethod.IsNullOrEmptyGuid(_tokenService.GetUserEventId()))
            {
                var _uowEventVisit = new UnitOfWork<EventVisit>(_uowPatient.GetDbContext());
                EventVisit eventVisit = new EventVisit();

                eventVisit.EventVisitId = Guid.NewGuid();
                eventVisit.EventId = _tokenService.GetUserEventId() ?? Guid.Empty;
                eventVisit.HealthFacilityId = TokenService.GetUserHfId();
                eventVisit.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
                eventVisit.IsActive = true;
                eventVisit.CreatedOn = DateTime.Now;
                eventVisit.CreatedBy = _tokenService.GetUserId();
                eventVisit.ActionTypeId = (int)ActionTypeEnum.Create;

                await _uowEventVisit.Repository.Insert(eventVisit);
                await _uowEventVisit.Save();
            }

            if (input.IsFromIPD == true)
            {
                foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
                {
                    FillEntityAdmissionDetails(patientAdmissionDetail);
                    patientAdmissionDetail.PatientId = obj.PatientId;
                    patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                    if (input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
                    {
                        patientAdmissionDetail.ShiftedFromHealthFacilityId = input.ReferredHealthFacilityId;
                        patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.ReferredByDepartmentLookupId;
                        patientAdmissionDetail.ShiftedFromSectionLookupId = input.ReferredBySectionLookupId;
                    }

                    patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.PatientId);
                    patientAdmissionDetail.IpdSource = input!.IPDSource;
                    patientAdmissionDetail.HealthFacilityId = input!.HealthFacilityId;
                    patientAdmissionDetail.DepartmentLookupId = input!.DepartmentLookupId;
                    patientAdmissionDetail.SectionLookupId = input!.SectionLookupId;
                }
            }


            _uowPatient.Repository.Update(obj!);

            //await _uowPatient.CommitAsync();

            if ((bool)input?.isEyeBlindness)
            {
                var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
                PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
                patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input);
                FillEntityPatientEyeBlindness(patientEyeBlindness);
                if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
                {
                    patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
                }

                await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
                await _uowPatientEyeBlindness.Save();

            }

            //if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            if (input.VisitSource == (int)AdmissionSourceTypeEnum.Referred && !AppCommonMethod.IsNullOrEmptyGuid(input.ReferVisitId))
            {
                var dbReferVisit = await _uowPatientOpenVisit.Repository.GetById(input.ReferVisitId!);

                if (!AppCommonMethod.IsNullObject(dbReferVisit) && dbReferVisit!.IsAdmittedInIpd == true)
                    throw new UserFriendlyException(CommonMessageConstant.PatientAlreadyRegistered);

                if (!AppCommonMethod.IsNullObject(dbReferVisit))
                {
                    dbReferVisit!.IsAdmittedInIpd = true;
                    dbReferVisit!.PatientId = input.PatientId;
                    dbReferVisit.UpdatedBy = _tokenService.GetUserId();
                    dbReferVisit.UpdatedOn = DateTime.Now;
                    dbReferVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    _uowPatientOpenVisit.Repository.Update(dbReferVisit!);
                    await _uowPatientOpenVisit.CommitAsync();
                }
            }

            await _uowPatient.CommitAsync();


            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPatient.GetDbContext());

            if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            {
                await SaveDrugAddictPatientData(input);
            }
            else if (input.IsFromER == true)
            {
                await SaveDrugAddictPatientData(input);
            }

            //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
            var diagnoseResponse = false;
            if (input.IsFromCallCenter == true)
                diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);

            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = input.PatientId;
            objPatientWorkFlowLog.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = input.HealthFacilityId;
            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(obj!.PatientOpenVisits!.FirstOrDefault()!.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreateOrEdit(objPatientWorkFlowLog);

            // Create Patient Visit Flow
            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.SectionLookupId).FirstOrDefaultAsync();


            objPatientVisitFlow.CurrentDepartmentId = input.DepartmentLookupId;
            objPatientVisitFlow.CurrentSectionId = input.SectionLookupId;

            objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;

            objPatientVisitFlow.IsFilterClinic = (!AppCommonMethod.IsNullObject(sectionLookup)) ? sectionLookup!.IsFilterClinic : null;

            //Comment after disscuss with zulqarnain
            //if (sectionLookup!.Name == CommonStringConstant.OneWindowTb)
            //{
            //    var visitFlow = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId).Include(x => x.PatientVisitFlows.OrderByDescending(x => x.CreatedOn))
            //    .FirstOrDefaultAsync();

            //    if (!AppCommonMethod.IsNullObject(visitFlow.PatientVisitFlows.FirstOrDefault()))
            //        input.TbVisitNo = visitFlow.PatientVisitFlows.FirstOrDefault()!.FollowUpNo;

            //}

            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


            if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.VisitDate))
            {
                await SaveTbPatientDetails(input, objPatientVisitFlow);


            }



            //return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

            var resp = _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

            if (input.IsReferredIpd == true)
            {
                var visitDetail = obj.PatientOpenVisits.Where(x => x.VisitFor == input.VisitFor).FirstOrDefault();

                if (!AppCommonMethod.IsNullObject(visitDetail))
                {

                    foreach (var item in resp.PatientOpenVisits)
                    {
                        if (item.PatientOpenVisitId == visitDetail.PatientOpenVisitId)
                        {
                            item.IsReferred = visitDetail.IsReferredIpd;
                            item.ReferredByDepartmentLookupId = visitDetail.IpdReferredByDepartmentLookupId;
                            item.ReferredBySectionLookupId = visitDetail.IpdReferredBySectionLookupId;
                        }
                    }
                }
            }


            resp.CreatedByName = user!.FullName;
            if (!string.IsNullOrEmpty(user.DesignationName))
                resp.CreatedByName += " (" + user.DesignationName + ")";
            //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return resp;
        }

        private async Task<dynamic> CreateVisit(CreateOrEditPatientWithVisitDto input)
        {
            #region PatientAdditionalInfo

            //_uowPatientAdditionalInfo.Repository.Insert();
            //if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
            //{
            //    if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
            //    {

            //        if (AppCommonMethod.IsNullOrEmptyGuid(input.CreateOrEditAdditionalPatient.PatientAdditionalInfoId))
            //        {
            //            var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
            //            additioanlPatientInfo.PatientId = input.PatientId;
            //            additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
            //            //additonalInfo.PatientVisitId=

            //            additioanlPatientInfo.CreatedOn = DateTime.Now;
            //            additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

            //            var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
            //            await _uowPatientAdditionalInfo.Save();
            //        }
            //        else
            //        {
            //            var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
            //            additioanlPatientInfo.PatientId = input.PatientId;

            //            //additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
            //            //additonalInfo.PatientVisitId=

            //            additioanlPatientInfo.UpdatedOn = DateTime.Now;
            //            additioanlPatientInfo.UpdatedBy = _tokenService.GetUserId();

            //            _uowPatientAdditionalInfo.Repository.Update(additioanlPatientInfo);
            //            await _uowPatientAdditionalInfo.Save();
            //        }
            //    }
            //}
            #endregion

            // check if user exist and values are edited 
            var dbObj = await _uowPatient.Repository.GetById(input.PatientId!);

            //if (string.IsNullOrEmpty(dbObj.Mrno))
            //{
            //    input.Mrno = await GenerateMRNNo(input);
            //}

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            // if no Section then set default section 
            if (AppCommonMethod.IsNullorZeroInt(input.SectionLookupId))
            {
                var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
                if (defaultSec != null)
                {
                    input.SectionLookupId = defaultSec.SectionLookupId;
                }
            }

            #region DRTB
            if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
            {
                await CheckPatientDRTBReferedStatus(input);
            }
            #endregion

            // If Patient Is Referred then Fill Patient Entity From DB
            CreateOrEditPatientDto dbPatient = new CreateOrEditPatientDto();
            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                dbPatient = _mapper.Map(dbObj, dbPatient);
                input = _mapper.Map(dbPatient, input);
            }

            // DbPatient Map on Input so no Record is Edit
            dbPatient = _mapper.Map(dbObj, dbPatient);
            input = _mapper.Map(dbPatient, input);

            var isVisitClose = false;
            var user = TokenService.GetUserLoggedInfo();
            var tokenUserId = _tokenService.GetUserId();
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
            {
                var HfId = TokenService.GetUserHfId();
                input.HealthFacilityId = HfId;
            }

            // ******** Check and Remove
            //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            //input.FullName = input.FirstName + " " + input.LastName;


            //if (input.IsSelf == true)
            //    input.NameOfCnicHolder = input.FullName;

            //if (String.IsNullOrEmpty(input.GuardianName))
            //    input.GuardianName = input.LastName;

            //if (!string.IsNullOrEmpty(dbObj.Mrno))
            //{
            //    if (input.Cnic != dbObj?.Cnic || input.Mrno != dbObj?.Mrno || input.IsSelf != dbObj?.IsSelf)
            //        throw new UserFriendlyException(CommonMessageConstant.CannotEditRecord);
            //}

            //// ADD DASHES IN CNIC
            //if (!string.IsNullOrEmpty(input.Cnic))
            //{
            //    if (!input.Cnic.Contains("-") && input.Cnic.Length == 13)
            //    {
            //        input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
            //        input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
            //    }
            //}


            // Check if Visit Exist in IPD against Patient
            //if (input.IsFromIPD == true)
            //{
            //    var visitExist = await CheckIfPatientVisitExistInIPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);
            //}
            //// Check if Visit Exist in Emergency against Patient
            //else if (input.IsFromER == true) 
            //{
            //    var visitExist = await CheckIfPatientVisitExistInER(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
            //}
            //// check if OPD
            //else
            //{
            //    var visitExist = await CheckIfPatientVisitExistInOPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

            //}

            // check if Patient Is Already Exist in Departments
            //await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);

            //check for next station
            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.StationProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.StationProfile!.ShortName,
                Name = x.StationProfile.Name,
            }).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);
            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();


            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station


                //// SMS on Visit Close
                //SendSMSDto smsObj = new SendSMSDto()
                //{
                //    Receiver = input!.MobileNo!.Replace("-", ""),
                //    Body = $"Dear {input.FirstName}, Thank You for Your Visit."
                //};

                //_smsService.SendSMS(smsObj);
            }

            // check if visit is already generated
            //if (_GenerateVisit_IfAlreadyExists)
            //{
            //    var checkIfVisitAlreadyExists = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId!).Where(x => x.IsDischarge != true && x.IsActive == true).Count();
            //    if (checkIfVisitAlreadyExists > 0)
            //        throw new UserFriendlyException(CommonMessageConstant.VisitIsAlreadyGenerated);
            //}

            Guid? VisitType = null;
            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
                VisitType = null;

            // if Doctor Register then Attended By User
            if (input.IsDoctor)
                input.Doctor = tokenUserId;

            //dbObj.PatientOpenVisits.Clear();
            bool IsPatientUnknown = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict && input.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            //db
            var obj = _mapper.Map(input, dbObj);
            // No Need to Update Patient Record
            //FillEntity(obj!);

            bool IsDrugAddict = false;
            if (input.PatientRole == CommonStringConstant.DrugAddict)
            {
                IsDrugAddict = true;
            }
            // Generate New TokenNo
            var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);

            // Except Emergency Confirm visit when Generate or Refer
            //var isAdmitted = (input.VisitFor != CommonStringConstant.ER) ? true : false;
            var isAdmitted = true;

            obj.IsPatientUnknown = IsPatientUnknown;

            var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);
            obj!.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                Age = obj.Age,
                AgeTypeProfileId = ageProfileId,
                PatientId = obj.PatientId,
                TokenNo = NewTokenNo,
                SlipNo = input.SlipNo,
                VisitFor = input.VisitFor,
                VisitSource = input.VisitSource,
                IsAdmitted = isAdmitted,
                VisitNo = UpdateVisitNo(input.PatientId),
                VisitTypeProfileId = VisitType,
                HealthFacilityId = input.HealthFacilityId,
                BedNo = input.BedNo,
                //HealthFacilityName = input
                DepartementLookupId = input.DepartmentLookupId,
                SectionLookupId = input.SectionLookupId,

                CurrentStationProfileId = nextStation!.StationProfileId,
                VisitDate = input.VisitDate == null ? DateTime.Now : input.VisitDate,
                IsFromPmis = input.IsFromPMIS,
                IsFromCallCenter = input.IsFromCallCenter,
                AttendedBy = input.Doctor,
                IsDischarge = isVisitClose,
                IsVitalSkip = input.IsVitalSkip,

                ParentPatientOpenVisitId = input.ReferVisitId,
                IsReferred = input.IsReferred,
                ReferredHealthFacilityId = input.ReferredHealthFacilityId,
                ReferredDepartmentLookupId = input.ReferredByDepartmentLookupId,
                ReferredSectionLookupId = input.ReferredBySectionLookupId,
                ReferredBy = input.ReferredBy,

                IsReferredIpd = input.IsReferredIpd,
                IpdReferredByDepartmentLookupId = (input.IsReferredIpd == true) ? input.ReferredByDepartmentLookupId : null,
                IpdReferredBySectionLookupId = (input.IsReferredIpd == true) ? input.ReferredBySectionLookupId : null,
                IpdReferredBy = (input.IsReferredIpd == true) ? input.ReferredBy : null,

                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create,
                SscNumber = input.SscNumber,
                IsEligibleForSsc = input.IsEligibleForSsc,
                ReasonIfNotEligibleForSsc = input.ReasonIfNotEligibleForSsc,
                IsDrugAddict = IsDrugAddict,
                PatinetPrivateHeathFacilityId = input.PatinetPrivateHeathFacilityId
            });

            if (!AppCommonMethod.IsNullOrEmptyGuid(_tokenService.GetUserEventId()))
            {
                var _uowEventVisit = new UnitOfWork<EventVisit>(_uowPatient.GetDbContext());
                EventVisit eventVisit = new EventVisit();

                eventVisit.EventVisitId = Guid.NewGuid();
                eventVisit.EventId = _tokenService.GetUserEventId() ?? Guid.Empty;
                eventVisit.HealthFacilityId = TokenService.GetUserHfId();
                eventVisit.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
                eventVisit.IsActive = true;
                eventVisit.CreatedOn = DateTime.Now;
                eventVisit.CreatedBy = _tokenService.GetUserId();
                eventVisit.ActionTypeId = (int)ActionTypeEnum.Create;

                await _uowEventVisit.Repository.Insert(eventVisit);
                await _uowEventVisit.Save();
            }

            if (input.IsFromIPD == true)
            {
                foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
                {
                    FillEntityAdmissionDetails(patientAdmissionDetail);
                    patientAdmissionDetail.PatientId = obj.PatientId;
                    patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                    if (input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
                    {
                        patientAdmissionDetail.ShiftedFromHealthFacilityId = input.ReferredHealthFacilityId;
                        patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.ReferredByDepartmentLookupId;
                        patientAdmissionDetail.ShiftedFromSectionLookupId = input.ReferredBySectionLookupId;
                    }

                    patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.PatientId);
                    patientAdmissionDetail.IpdSource = input!.IPDSource;
                    patientAdmissionDetail.HealthFacilityId = input!.HealthFacilityId;
                    patientAdmissionDetail.DepartmentLookupId = input!.DepartmentLookupId;
                    patientAdmissionDetail.SectionLookupId = input!.SectionLookupId;
                }
            }


            _uowPatient.Repository.Update(obj!);

            //await _uowPatient.CommitAsync();



            if ((bool)input?.isEyeBlindness)
            {
                var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
                PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
                patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input);
                FillEntityPatientEyeBlindness(patientEyeBlindness);
                if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
                {
                    patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
                }

                await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
                await _uowPatientEyeBlindness.Save();

            }

            if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                var dbReferVisit = await _uowPatientOpenVisit.Repository.GetById(input.ReferVisitId!);

                if (!AppCommonMethod.IsNullObject(dbReferVisit) && dbReferVisit!.IsAdmittedInIpd == true)
                    throw new UserFriendlyException(CommonMessageConstant.PatientAlreadyRegistered);

                if (!AppCommonMethod.IsNullObject(dbReferVisit))
                {
                    dbReferVisit!.IsAdmittedInIpd = true;
                    dbReferVisit.UpdatedBy = _tokenService.GetUserId();
                    dbReferVisit.UpdatedOn = DateTime.Now;
                    dbReferVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    _uowPatientOpenVisit.Repository.Update(dbReferVisit!);
                    await _uowPatientOpenVisit.CommitAsync();
                }
            }

            await _uowPatient.CommitAsync();


            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPatient.GetDbContext());

            if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            {
                await SaveDrugAddictPatientData(input);
            }

            //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
            var diagnoseResponse = false;
            if (input.IsFromCallCenter == true)
                diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);

            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = input.PatientId;
            objPatientWorkFlowLog.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = input.HealthFacilityId;
            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(obj!.PatientOpenVisits!.FirstOrDefault()!.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreateOrEdit(objPatientWorkFlowLog);

            // Create Patient Visit Flow
            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.SectionLookupId).FirstOrDefaultAsync();


            objPatientVisitFlow.CurrentDepartmentId = input.DepartmentLookupId;
            objPatientVisitFlow.CurrentSectionId = input.SectionLookupId;

            objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;

            objPatientVisitFlow.IsFilterClinic = (!AppCommonMethod.IsNullObject(sectionLookup)) ? sectionLookup!.IsFilterClinic : null;

            //Comment after disscuss with zulqarnain
            //if (sectionLookup!.Name == CommonStringConstant.OneWindowTb)
            //{
            //    var visitFlow = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId).Include(x => x.PatientVisitFlows.OrderByDescending(x => x.CreatedOn))
            //    .FirstOrDefaultAsync();

            //    if (!AppCommonMethod.IsNullObject(visitFlow.PatientVisitFlows.FirstOrDefault()))
            //        input.TbVisitNo = visitFlow.PatientVisitFlows.FirstOrDefault()!.FollowUpNo;

            //}

            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


            if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.VisitDate))
            {
                await SaveTbPatientDetails(input, objPatientVisitFlow);
            }

            //return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

            var resp = _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);
            resp.CreatedByName = user!.FullName;
            if (!string.IsNullOrEmpty(user.DesignationName))
                resp.CreatedByName += " (" + user.DesignationName + ")";
            //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return resp;
        }

        #region EMR Registration

        public async Task<dynamic> CreateOrEditWithVisitCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        {
            // if patient id is null and Mode is Create
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Patient!.PatientId) && input.RequestMode == (int)RequestModeEnum.CreatePatientWithVisit)
                return await CreateWithVisitCentrally(input);
            else
            {
                // if patient id is available and Mode is Update
                //if (input.RequestMode == (int)RequestModeEnum.UpdatePatientWithVisit)
                //    return await UpdateWithVisitCentrally(input);
                //// if Patient Id is available and Mode is only Create Visit
                //else 
                if (input.RequestMode == (int)RequestModeEnum.CreateVisit)
                    return await CreateVisitCentrally(input);
                else
                    return await UpdatePatient(input);
            }
        }

        private async Task<dynamic> CreateWithVisitCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        {

            #region DRTB
            if (!string.IsNullOrEmpty(input.Patient!.DRTBPatientStatus))
            {
                if (input.Patient!.DRTBPatientStatus == CommonStringConstant.Refered)
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientIsNotReferedToDRTB);
                }
            }
            #endregion

            // ADD DASHES IN CNIC
            if (!string.IsNullOrEmpty(input.Patient!.Cnic))
            {
                if (!input.Patient!.Cnic.Contains("-"))
                {
                    input.Patient!.Cnic = input.Patient!.Cnic.Substring(0, 5) + "-" + input.Patient!.Cnic.Substring(5);
                    input.Patient!.Cnic = input.Patient!.Cnic.Substring(0, 13) + "-" + input.Patient!.Cnic.Substring(13);
                }
            }
            else if (!string.IsNullOrEmpty(input.Patient!.PassportNo))
            {
                input.Patient!.Cnic = input.Patient!.PassportNo;
            }

            var isVisitClose = false;
            var tokenUserId = _tokenService.GetUserId();
            var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            input.Patient!.FullName = input.Patient!.FirstName + " " + input.Patient!.LastName;
            if (AppCommonMethod.IsNullorZeroInt(input.Patient!.HealthFacilityId))
                input.Patient!.HealthFacilityId = user?.HealthFacilityId;
            if (String.IsNullOrEmpty(input.Patient!.GuardianName))
                input.Patient!.GuardianName = input.Patient!.LastName;


            if ((bool)input?.Patient!.isEyeBlindness)
            {
                isVisitClose = true;
            }

            if (input.Patient!.IsSelf == true)
            {
                //input.RelationProfileId = null;
                input.Patient!.NameOfCnicHolder = input.Patient!.FullName;
                if (input.Patient!.PatientRole != CommonStringConstant.DrugAddict && input.Patient!.Category != CommonStringConstant.Unknown)
                {
                    var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Patient!.Cnic && x.IsSelf == true).Count();

                    if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);

                    if (!string.IsNullOrEmpty(input.Patient!.PassportNo))
                    {
                        var CheckPassportExists = _uowPatient.Repository.GetALL(x => x.PassportNo == input.Patient!.PassportNo && x.IsSelf == true).Count();

                        if (!AppCommonMethod.IsNullorZeroInt(CheckPassportExists))
                            throw new UserFriendlyException(CommonMessageConstant.PatientExistWithPassport);
                    }
                }
            }
            else
            {
                var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Patient!.Cnic && x.FirstName == input.Patient!.FirstName && x.RelationProfileId == input.Patient!.RelationProfileId).Count();

                if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithRelationCnic);


                if (!string.IsNullOrEmpty(input.Patient!.PassportNo))
                {
                    var CheckPassportExists = _uowPatient.Repository.GetALL(x => x.PassportNo == input.Patient!.PassportNo && x.IsSelf == true).Count();

                    if (!AppCommonMethod.IsNullorZeroInt(CheckPassportExists))
                        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithPassport);
                }
            }

            // if no Section then set default section 
            if (AppCommonMethod.IsNullorZeroInt(input.PatientOpenVisit!.SectionLookupId))
            {
                var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
                if (defaultSec != null)
                {
                    input.PatientOpenVisit!.SectionLookupId = defaultSec.SectionLookupId;
                }
            }


            //check for next station from Health Facility
            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.Patient!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.StationProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.StationProfile!.ShortName,
                    Name = x.StationProfile.Name,
                }).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);

            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.PatientOpenVisit!.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                // SMS on Visit Close
                //SendSMSDto smsObj = new SendSMSDto()
                //{
                //    Receiver = input!.MobileNo!.Replace("-", ""),
                //    //Body = $"Dear {input.FirstName}, Thank You for Your Visit '\n' Test."

                //    Body = $"معزز {input.FirstName}   ،\r\nڈسٹرکٹ ہیڈ کوارٹر ہسپتال قصور تشریف آوری کا شکریہ\r\nآپ کو اوپی ڈی میں مندرجہ ذیل ادویات مفت فراہم کی گئی ہیں۔\r\n پیناڈول : صبح ، دوپہر، شام"
                //};

                //_smsService.SendSMS(smsObj);
            }

            // Get Last Mrno
            var dbObj = await _uowTehsil.Repository.GetALL(x => x.TehsilId == input.Patient!.TehsilId).Select(y => new
            {
                TehsilCode = y.Code,
                Mrno = y.Patients.OrderByDescending(x => x.Mrno).Select(x => x.Mrno).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            // Mrno Generation

            input.Patient!.Mrno = await GenerateMRNNoCentrally(input);

            //if (_GenerateMrNo_OnEveryReg)
            //{
            //    if (string.IsNullOrEmpty(dbObj?.Mrno))
            //        input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
            //    else
            //        input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            //}
            //else
            //{
            //    if (input.IsSelf == true)
            //    {
            //        if (string.IsNullOrEmpty(dbObj?.Mrno))
            //            input.Mrno = "MRN-" + dbObj?.TehsilCode + "000001";
            //        else
            //            input.Mrno = CommonMethods.IncrementStringEnd(dbObj.Mrno, 15);
            //    }
            //    else
            //        input.Mrno = null;
            //}

            // get Default Visit Type ID

            Guid? VisitType = null;
            if (input.PatientOpenVisit!.IsFromIPD == true && input.PatientOpenVisit!.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
                VisitType = null;

            // if Doctor Register then Attended By User
            if (input.PatientOpenVisit!.IsDoctor)
                input.PatientOpenVisit!.Doctor = tokenUserId;
            //Guid systemSourceId = null;
            //if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            //{
            //    var res = await _authProfileTypeService.GetProfileByProfileType(CommonStringConstant._DrugAddict);

            //    //systemSourceId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant._DrugAddict).Select(x => x.ProfileId).FirstOrDefaultAsync();



            //}

            bool IsPatientUnknown = false;
            if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict && input.Patient!.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            bool IsDrugAddict = false;
            if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict)
            {
                IsDrugAddict = true;
            }
            // Generate New TokenNo
            var NewTokenNo = await GetTokenNo(input.PatientOpenVisit!.HealthFacilityId, input.PatientOpenVisit!.DepartmentLookupId);

            // Except Emergency Confirm visit when Generate or Refer
            //var isAdmitted = (input.PatientOpenVisit!.VisitFor != CommonStringConstant.ER) ? true : false;
            var isAdmitted = true;

            //db
            var obj = _mapper.Map<DbModel.Patient>(input.Patient);
            FillEntity(obj);
            obj.IsPatientUnknown = IsPatientUnknown;


            var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);
            obj.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                Age = obj.Age,
                AgeTypeProfileId = ageProfileId,
                TokenNo = NewTokenNo,
                SlipNo = input.PatientOpenVisit!.SlipNo,
                VisitFor = input.PatientOpenVisit!.VisitFor,
                VisitSource = input.PatientOpenVisit!.VisitSource,
                IsAdmitted = isAdmitted,
                VisitTypeProfileId = VisitType, // Default Visit Type Id Set to Walk In
                HealthFacilityId = input.PatientOpenVisit!.HealthFacilityId,
                VisitNo = 1,
                DepartementLookupId = input.PatientOpenVisit!.DepartmentLookupId,
                SectionLookupId = input.PatientOpenVisit!.SectionLookupId,
                CurrentStationProfileId = nextStation!.StationProfileId,
                BedNo = input.PatientOpenVisit!.BedNo,
                //SourceSystemId = systemSourceId,
                VisitDate = input.PatientOpenVisit!.VisitDate == null ? DateTime.Now : input.PatientOpenVisit!.VisitDate,
                IsFromPmis = input.PatientOpenVisit!.IsFromPMIS,
                IsFromCallCenter = input.PatientOpenVisit!.IsFromCallCenter,
                AttendedBy = input.PatientOpenVisit!.Doctor,
                IsDischarge = isVisitClose,
                IsVitalSkip = input.PatientOpenVisit!.IsVitalSkip,
                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create,
                SscNumber = input.PatientOpenVisit!.SscNumber,
                IsEligibleForSsc = input.PatientOpenVisit!.IsEligibleForSsc,
                ReasonIfNotEligibleForSsc = input.PatientOpenVisit!.ReasonIfNotEligibleForSsc,
                IsDrugAddict = IsDrugAddict,
                PatinetPrivateHeathFacilityId = input.PatientOpenVisit!.PatinetPrivateHeathFacilityId
            });




            if (input.PatientOpenVisit!.IsFromIPD == true)
            {
                foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
                {
                    FillEntityAdmissionDetails(patientAdmissionDetail);
                    patientAdmissionDetail.PatientId = obj.PatientId;
                    patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                    if (input.PatientOpenVisit!.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
                    {
                        patientAdmissionDetail.ShiftedFromHealthFacilityId = input.PatientOpenVisit!.ReferredHealthFacilityId;
                        patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.PatientOpenVisit!.ReferredByDepartmentLookupId;
                        patientAdmissionDetail.ShiftedFromSectionLookupId = input.PatientOpenVisit!.ReferredBySectionLookupId;
                    }

                    patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.Patient!.PatientId);
                    patientAdmissionDetail.IpdSource = input!.PatientOpenVisit!.IPDSource;
                    patientAdmissionDetail.HealthFacilityId = input!.PatientOpenVisit!.HealthFacilityId;
                    patientAdmissionDetail.DepartmentLookupId = input!.PatientOpenVisit!.DepartmentLookupId;
                    patientAdmissionDetail.SectionLookupId = input!.PatientOpenVisit!.SectionLookupId;

                }
            }

            DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
            await _uowPatient.Save();

            input.Patient!.PatientId = responseObj.PatientId;



            if ((bool)input?.Patient!.isEyeBlindness)
            {
                var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
                PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
                patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input);
                FillEntityPatientEyeBlindness(patientEyeBlindness);
                if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
                {
                    patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
                }
                await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
                await _uowPatientEyeBlindness.Save();

            }

            //if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict || input.Patient!.PatientRole == CommonStringConstant.Mortuary)
            //{
            //    await SaveDrugAddictPatientData(input);
            //}

            #region PatientAdditionalInfo
            //_uowPatientAdditionalInfo.Repository.Insert();
            //if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
            //{
            //    if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
            //    {
            //        var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
            //        additioanlPatientInfo.PatientId = input.PatientId;
            //        additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
            //        //additonalInfo.PatientVisitId=

            //        additioanlPatientInfo.CreatedOn = DateTime.Now;
            //        additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

            //        var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
            //        await _uowPatientAdditionalInfo.Save();
            //    }

            //}
            #endregion



            if (input.Patient.PatientRole == CommonStringConstant.DrugAddict && input.Patient!.Category == CommonStringConstant.Unknown)
            {
                var UnknownPatientObj = _mapper.Map<UnknownPatientDto>(responseObj);
                UnknownPatientObj.PatientRole = input.Patient!.PatientRole;

                var response = await VerifyWithNADRA(UnknownPatientObj);
                //if (Int32.Parse(response.code) != 200)
                //{
                //    throw new UserFriendlyException(response.message);
                //}
            }


            //update DataBank
            if (input.Patient!.IsDataBankRecord)
            {
                DataBankService _dataBankService = new DataBankService();

                if (input.Patient!.IsSelf == true)
                    await _dataBankService.UpdateHMISFlagWithCnic(input.Patient!.Cnic);
                else
                    await _dataBankService.UpdateHMISFlagWithId(input.Patient!.DataBankId);
            }

            //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
            //var diagnoseResponse = false;
            //if (input.Patient!.IsFromCallCenter == true)
            //    diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);


            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = responseObj.PatientId;
            objPatientWorkFlowLog.PatientVisitId = responseObj.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = responseObj.HealthFacilityId;
            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(responseObj.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


            // Create Patient Visit Flow
            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.PatientOpenVisit!.SectionLookupId).FirstOrDefaultAsync();
            objPatientVisitFlow.CurrentDepartmentId = input.PatientOpenVisit!.DepartmentLookupId;
            objPatientVisitFlow.CurrentSectionId = input.PatientOpenVisit!.SectionLookupId;
            objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;
            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


            if (!AppCommonMethod.IsNullOrEmptyGuid(input.Patient!.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.PatientOpenVisit!.VisitDate))
            {
                var _uowtbpatientdetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                TbPatientDetail tbpatientdetail = new TbPatientDetail();
                FillEntityTbPatientDetails(tbpatientdetail);
                tbpatientdetail.PatientId = obj.PatientId;
                tbpatientdetail.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
                tbpatientdetail.PatientTypeProfileId = input.Patient!.TbPatientTypeId;
                if (!AppCommonMethod.IsNullOrEmptyGuid(input.Patient!.TbPatientTreatmentLengthId))
                {
                    tbpatientdetail.PatientLengthOfInterruptionProfileId = input.Patient!.TbPatientTreatmentLengthId;
                }
                if (!AppCommonMethod.IsNullOrEmptyGuid(input.Patient!.TbPatientLengthOfInterruptionId))
                {
                    tbpatientdetail.PatientLengthOfInterruptionProfileId = input.Patient!.TbPatientLengthOfInterruptionId;
                }
                tbpatientdetail.NoOfMedicineTaken = input.Patient!.TbPatientNoOfMedicineTaken;
                tbpatientdetail.NoOfMonthsMedicineIssued = input.Patient!.NoOfMonthsMedicineIssued;
                tbpatientdetail.PatientTreatmentCycleNo = objPatientVisitFlow.PatientTreatmentCycleNo;

                //if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
                //    tbpatientdetail.PatientStatus = input.DRTBPatientStatus;

                await _uowtbpatientdetails.Repository.Insert(tbpatientdetail);
                await _uowtbpatientdetails.Save();
            }

            //var mappedObj = _mapper.Map<CreateOrEditPatientWithVisitDto>(responseObj);
            var res = GetResponseModelDto(responseObj, input!.SourceSystemShortName);
            var loginUser = TokenService.GetUserLoggedInfo();
            if (!string.IsNullOrEmpty(loginUser.DesignationName))
                res.CreatedByName += " (" + loginUser.DesignationName + ")";
            //resp.CreatedOn = responseObj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return res;
        }

        //private async Task<dynamic> UpdateWithVisitCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        //{
        //    #region PatientAdditionalInfo

        //    //_uowPatientAdditionalInfo.Repository.Insert();
        //    if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
        //    {
        //        if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
        //        {

        //            if (AppCommonMethod.IsNullOrEmptyGuid(input.CreateOrEditAdditionalPatient.PatientAdditionalInfoId))
        //            {
        //                var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
        //                additioanlPatientInfo.PatientId = input.PatientId;
        //                additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
        //                //additonalInfo.PatientVisitId=

        //                additioanlPatientInfo.CreatedOn = DateTime.Now;
        //                additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

        //                var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
        //                await _uowPatientAdditionalInfo.Save();
        //            }
        //            else
        //            {
        //                var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
        //                additioanlPatientInfo.PatientId = input.PatientId;

        //                //additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
        //                //additonalInfo.PatientVisitId=

        //                additioanlPatientInfo.UpdatedOn = DateTime.Now;
        //                additioanlPatientInfo.UpdatedBy = _tokenService.GetUserId();

        //                _uowPatientAdditionalInfo.Repository.Update(additioanlPatientInfo);
        //                await _uowPatientAdditionalInfo.Save();
        //            }
        //        }
        //    }
        //    #endregion

        //    // check if user exist and values are edited 
        //    var dbObj = await _uowPatient.Repository.GetById(input.PatientId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    if (string.IsNullOrEmpty(dbObj.Mrno))
        //    {
        //        input.Mrno = await GenerateMRNNo(input);
        //    }

        //    // if no Section then set default section 
        //    if (AppCommonMethod.IsNullorZeroInt(input.SectionLookupId))
        //    {
        //        var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
        //        var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
        //        if (defaultSec != null)
        //        {
        //            input.SectionLookupId = defaultSec.SectionLookupId;
        //        }
        //    }

        //    #region DRTB
        //    if (!string.IsNullOrEmpty(input.DRTBPatientStatus))
        //    {
        //        await CheckPatientDRTBReferedStatus(input);
        //    }
        //    #endregion

        //    // If Patient Is Referred then Fill Patient Entity From DB
        //    if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
        //    {
        //        CreateOrEditPatientDto dbPatient = new CreateOrEditPatientDto();
        //        dbPatient = _mapper.Map(dbObj, dbPatient);
        //        input = _mapper.Map(dbPatient, input);
        //    }


        //    var isVisitClose = false;
        //    var user = TokenService.GetUserLoggedInfo();
        //    var tokenUserId = _tokenService.GetUserId();
        //    if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
        //    {
        //        var HfId = TokenService.GetUserHfId();
        //        input.HealthFacilityId = HfId;
        //    }

        //    // ******** Check and Remove
        //    //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

        //    //checks
        //    input.FullName = input.FirstName + " " + input.LastName;


        //    if (input.IsSelf == true)
        //        input.NameOfCnicHolder = input.FullName;

        //    if (String.IsNullOrEmpty(input.GuardianName))
        //        input.GuardianName = input.LastName;

        //    if (!string.IsNullOrEmpty(dbObj.Mrno))
        //    {
        //        if (input.Cnic != dbObj?.Cnic || input.Mrno != dbObj?.Mrno || input.IsSelf != dbObj?.IsSelf)
        //            throw new UserFriendlyException(CommonMessageConstant.CannotEditRecord);
        //    }

        //    // ADD DASHES IN CNIC
        //    if (!string.IsNullOrEmpty(input.Cnic))
        //    {
        //        if (!input.Cnic.Contains("-") && input.Cnic.Length == 13)
        //        {
        //            input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
        //            input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
        //        }
        //    }


        //    // Check if Visit Exist in IPD against Patient
        //    //if (input.IsFromIPD == true)
        //    //{
        //    //    var visitExist = await CheckIfPatientVisitExistInIPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
        //    //    if (visitExist == true)
        //    //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);
        //    //}
        //    //// Check if Visit Exist in Emergency against Patient
        //    //else if (input.IsFromER == true) 
        //    //{
        //    //    var visitExist = await CheckIfPatientVisitExistInER(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
        //    //    if (visitExist == true)
        //    //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
        //    //}
        //    //// check if OPD
        //    //else
        //    //{
        //    //    var visitExist = await CheckIfPatientVisitExistInOPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
        //    //    if (visitExist == true)
        //    //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

        //    //}

        //    // check if Patient Is Already Exist in Departments
        //    //await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);

        //    //check for next station
        //    var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
        //    .Select(x => new ViewStationDto
        //    {
        //        StationProfileId = x.StationProfileId,
        //        SequenceNo = x.SequenceNo,
        //        ShortName = x.StationProfile!.ShortName,
        //        Name = x.StationProfile.Name,
        //    }).ToListAsync();

        //    //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
        //    //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);
        //    if (AppCommonMethod.IsNullOrEmptyList(stationsList))
        //    {
        //        //Station from Profiles
        //        var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

        //        stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
        //            .OrderBy(x => x.SequenceNo)
        //        .Select(x => new ViewStationDto
        //        {
        //            StationProfileId = x.ProfileId,
        //            SequenceNo = x.SequenceNo,
        //            ShortName = x.ShortName,
        //            Name = x.Name
        //        }).ToListAsync();
        //    }

        //    var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

        //    if (AppCommonMethod.IsNullObject(thisStation))
        //        throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

        //    var nextStation = new ViewStationDto();

        //    if (input.IsVitalSkip)
        //        nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
        //    else
        //        nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();


        //    if (AppCommonMethod.IsNullObject(nextStation))
        //    {
        //        isVisitClose = true;
        //        nextStation = thisStation; // if Visit is close then Next Station is set to Current Station


        //        //// SMS on Visit Close
        //        //SendSMSDto smsObj = new SendSMSDto()
        //        //{
        //        //    Receiver = input!.MobileNo!.Replace("-", ""),
        //        //    Body = $"Dear {input.FirstName}, Thank You for Your Visit."
        //        //};

        //        //_smsService.SendSMS(smsObj);
        //    }

        //    // check if visit is already generated
        //    //if (_GenerateVisit_IfAlreadyExists)
        //    //{
        //    //    var checkIfVisitAlreadyExists = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId!).Where(x => x.IsDischarge != true && x.IsActive == true).Count();
        //    //    if (checkIfVisitAlreadyExists > 0)
        //    //        throw new UserFriendlyException(CommonMessageConstant.VisitIsAlreadyGenerated);
        //    //}

        //    Guid? VisitType = null;
        //    if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
        //    {
        //        VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
        //    }
        //    else
        //    {
        //        VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
        //    }

        //    if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
        //        VisitType = null;

        //    // if Doctor Register then Attended By User
        //    if (input.IsDoctor)
        //        input.Doctor = tokenUserId;

        //    //dbObj.PatientOpenVisits.Clear();
        //    bool IsPatientUnknown = false;
        //    if (input.PatientRole == CommonStringConstant.DrugAddict && input.Category == CommonStringConstant.Unknown)
        //    {
        //        IsPatientUnknown = true;
        //    }

        //    //db
        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    bool IsDrugAddict = false;
        //    if (input.PatientRole == CommonStringConstant.DrugAddict)
        //    {
        //        IsDrugAddict = true;
        //    }
        //    // Generate New TokenNo
        //    var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);

        //    // Except Emergency Confirm visit when Generate or Refer
        //    var isAdmitted = (input.VisitFor != CommonStringConstant.ER) ? true : false;

        //    obj.IsPatientUnknown = IsPatientUnknown;

        //    var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);
        //    obj!.PatientOpenVisits.Add(new PatientOpenVisit
        //    {
        //        PatientOpenVisitId = Guid.NewGuid(),
        //        Age = obj.Age,
        //        AgeTypeProfileId = ageProfileId,
        //        PatientId = obj.PatientId,
        //        TokenNo = NewTokenNo,
        //        SlipNo = input.SlipNo,
        //        VisitFor = input.VisitFor,
        //        VisitSource = input.VisitSource,
        //        IsAdmitted = isAdmitted,
        //        VisitNo = UpdateVisitNo(input.PatientId),
        //        VisitTypeProfileId = VisitType,
        //        HealthFacilityId = input.HealthFacilityId,
        //        BedNo = input.BedNo,
        //        //HealthFacilityName = input
        //        DepartementLookupId = input.DepartmentLookupId,
        //        SectionLookupId = input.SectionLookupId,

        //        CurrentStationProfileId = nextStation!.StationProfileId,
        //        VisitDate = input.VisitDate == null ? DateTime.Now : input.VisitDate,
        //        IsFromPmis = input.IsFromPMIS,
        //        IsFromCallCenter = input.IsFromCallCenter,
        //        AttendedBy = input.Doctor,
        //        IsDischarge = isVisitClose,
        //        IsVitalSkip = input.IsVitalSkip,

        //        ParentPatientOpenVisitId = input.ReferVisitId,
        //        IsReferred = input.IsReferred,
        //        ReferredHealthFacilityId = input.ReferredHealthFacilityId,
        //        ReferredDepartmentLookupId = input.ReferredByDepartmentLookupId,
        //        ReferredSectionLookupId = input.ReferredBySectionLookupId,
        //        ReferredBy = input.ReferredBy,

        //        IsReferredIpd = input.IsReferredIpd,
        //        IpdReferredByDepartmentLookupId = (input.IsReferredIpd == true) ? input.ReferredByDepartmentLookupId : null,
        //        IpdReferredBySectionLookupId = (input.IsReferredIpd == true) ? input.ReferredBySectionLookupId : null,
        //        IpdReferredBy = (input.IsReferredIpd == true) ? input.ReferredBy : null,

        //        IsActive = true,
        //        CreatedBy = _tokenService.GetUserId(),
        //        CreatedOn = DateTime.Now,
        //        ActionTypeId = (int)ActionTypeEnum.Create,
        //        SscNumber = input.SscNumber,
        //        IsEligibleForSsc = input.IsEligibleForSsc,
        //        ReasonIfNotEligibleForSsc = input.ReasonIfNotEligibleForSsc,
        //        IsDrugAddict = IsDrugAddict,
        //        PatinetPrivateHeathFacilityId = input.PatinetPrivateHeathFacilityId
        //    });

        //    if (input.IsFromIPD == true)
        //    {
        //        foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
        //        {
        //            FillEntityAdmissionDetails(patientAdmissionDetail);
        //            patientAdmissionDetail.PatientId = obj.PatientId;
        //            patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

        //            if (input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
        //            {
        //                patientAdmissionDetail.ShiftedFromHealthFacilityId = input.ReferredHealthFacilityId;
        //                patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.ReferredByDepartmentLookupId;
        //                patientAdmissionDetail.ShiftedFromSectionLookupId = input.ReferredBySectionLookupId;
        //            }

        //            patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.PatientId);
        //            patientAdmissionDetail.IpdSource = input!.IPDSource;
        //            patientAdmissionDetail.HealthFacilityId = input!.HealthFacilityId;
        //            patientAdmissionDetail.DepartmentLookupId = input!.DepartmentLookupId;
        //            patientAdmissionDetail.SectionLookupId = input!.SectionLookupId;
        //        }
        //    }


        //    _uowPatient.Repository.Update(obj!);

        //    //await _uowPatient.CommitAsync();



        //    if ((bool)input?.isEyeBlindness)
        //    {
        //        var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
        //        PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
        //        patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input);
        //        FillEntityPatientEyeBlindness(patientEyeBlindness);
        //        if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
        //        {
        //            patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
        //        }

        //        await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
        //        await _uowPatientEyeBlindness.Save();

        //    }

        //    if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
        //    {
        //        var dbReferVisit = await _uowPatientOpenVisit.Repository.GetById(input.ReferVisitId!);

        //        if (!AppCommonMethod.IsNullObject(dbReferVisit) && dbReferVisit!.IsAdmittedInIpd == true)
        //            throw new UserFriendlyException(CommonMessageConstant.PatientAlreadyRegistered);

        //        if (!AppCommonMethod.IsNullObject(dbReferVisit))
        //        {
        //            dbReferVisit!.IsAdmittedInIpd = true;
        //            dbReferVisit.UpdatedBy = _tokenService.GetUserId();
        //            dbReferVisit.UpdatedOn = DateTime.Now;
        //            dbReferVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

        //            _uowPatientOpenVisit.Repository.Update(dbReferVisit!);
        //            await _uowPatientOpenVisit.CommitAsync();
        //        }
        //    }

        //    await _uowPatient.CommitAsync();


        //    var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPatient.GetDbContext());

        //    if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
        //    {
        //        await SaveDrugAddictPatientData(input);
        //    }

        //    //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
        //    var diagnoseResponse = false;
        //    if (input.IsFromCallCenter == true)
        //        diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);

        //    // Create Patient Work Log
        //    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

        //    objPatientWorkFlowLog.PatientId = input.PatientId;
        //    objPatientWorkFlowLog.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
        //    objPatientWorkFlowLog.HealthFacilityId = input.HealthFacilityId;
        //    objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
        //    objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
        //    objPatientWorkFlowLog.IsVisitClose = isVisitClose;
        //    objPatientWorkFlowLog.IsActive = true;
        //    objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(obj!.PatientOpenVisits!.FirstOrDefault()!.CreatedOn, DateTime.Now);
        //    await _patientWorkFlowLogService.CreateOrEdit(objPatientWorkFlowLog);

        //    // Create Patient Visit Flow
        //    CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

        //    var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.SectionLookupId).FirstOrDefaultAsync();


        //    objPatientVisitFlow.CurrentDepartmentId = input.DepartmentLookupId;
        //    objPatientVisitFlow.CurrentSectionId = input.SectionLookupId;

        //    objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
        //    objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;

        //    objPatientVisitFlow.IsFilterClinic = (!AppCommonMethod.IsNullObject(sectionLookup)) ? sectionLookup!.IsFilterClinic : null;

        //    //Comment after disscuss with zulqarnain
        //    //if (sectionLookup!.Name == CommonStringConstant.OneWindowTb)
        //    //{
        //    //    var visitFlow = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId).Include(x => x.PatientVisitFlows.OrderByDescending(x => x.CreatedOn))
        //    //    .FirstOrDefaultAsync();

        //    //    if (!AppCommonMethod.IsNullObject(visitFlow.PatientVisitFlows.FirstOrDefault()))
        //    //        input.TbVisitNo = visitFlow.PatientVisitFlows.FirstOrDefault()!.FollowUpNo;

        //    //}

        //    await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


        //    if (!AppCommonMethod.IsNullOrEmptyGuid(input.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.VisitDate))
        //    {
        //        await SaveTbPatientDetails(input, objPatientVisitFlow);
        //    }



        //    //return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

        //    var mappedObj = _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);
        //    var res = GetResponseModelDto(mappedObj, input!.SourceSystemShortName);
        //    res.CreatedByName = user!.FullName;
        //    if (!string.IsNullOrEmpty(user.DesignationName))
        //        res.CreatedByName += " (" + user.DesignationName + ")";
        //    //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
        //    return res;
        //}

        private async Task<dynamic> CreateVisitCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        {
            #region PatientAdditionalInfo

            //_uowPatientAdditionalInfo.Repository.Insert();
            //if (!AppCommonMethod.IsNullObject(input.CreateOrEditAdditionalPatient))
            //{
            //    if (input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.EMCMLE || input.CreateOrEditAdditionalPatient.FormType == CommonStringConstant.PoliceKhidmatForm)
            //    {

            //        if (AppCommonMethod.IsNullOrEmptyGuid(input.CreateOrEditAdditionalPatient.PatientAdditionalInfoId))
            //        {
            //            var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
            //            additioanlPatientInfo.PatientId = input.PatientId;
            //            additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
            //            //additonalInfo.PatientVisitId=

            //            additioanlPatientInfo.CreatedOn = DateTime.Now;
            //            additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

            //            var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
            //            await _uowPatientAdditionalInfo.Save();
            //        }
            //        else
            //        {
            //            var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input.CreateOrEditAdditionalPatient);
            //            additioanlPatientInfo.PatientId = input.PatientId;

            //            //additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
            //            //additonalInfo.PatientVisitId=

            //            additioanlPatientInfo.UpdatedOn = DateTime.Now;
            //            additioanlPatientInfo.UpdatedBy = _tokenService.GetUserId();

            //            _uowPatientAdditionalInfo.Repository.Update(additioanlPatientInfo);
            //            await _uowPatientAdditionalInfo.Save();
            //        }
            //    }
            //}
            #endregion

            // check if user exist and values are edited 
            var dbObj = await _uowPatient.Repository.GetById(input.Patient!.PatientId!);

            //if (string.IsNullOrEmpty(dbObj.Mrno))
            //{
            //    input.Mrno = await GenerateMRNNo(input);
            //}

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            // if no Section then set default section 
            if (AppCommonMethod.IsNullorZeroInt(input.PatientOpenVisit!.SectionLookupId))
            {
                var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                var defaultSec = await _uowSectionLookup.Repository.GetALL(x => x.Name == CommonStringConstant.MedicalOPD).FirstOrDefaultAsync();
                if (defaultSec != null)
                {
                    input.PatientOpenVisit!.SectionLookupId = defaultSec.SectionLookupId;
                }
            }

            #region DRTB
            //if (!string.IsNullOrEmpty(input.Patient!.DRTBPatientStatus))
            //{
            //    await CheckPatientDRTBReferedStatus(input);
            //}
            #endregion

            // If Patient Is Referred then Fill Patient Entity From DB
            CreateOrEditPatientDto dbPatient = new CreateOrEditPatientDto();
            //if (input.IsFromIPD == true && input.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            //{
            //    dbPatient = _mapper.Map(dbObj, dbPatient);
            //    input = _mapper.Map(dbPatient, input);
            //}

            // DbPatient Map on Input so no Record is Edit
            dbPatient = _mapper.Map(dbObj, dbPatient);
            input.Patient = _mapper.Map(dbPatient, input.Patient);

            var isVisitClose = false;
            var user = TokenService.GetUserLoggedInfo();
            var tokenUserId = _tokenService.GetUserId();
            if (AppCommonMethod.IsNullorZeroInt(input.PatientOpenVisit!.HealthFacilityId))
            {
                var HfId = TokenService.GetUserHfId();
                input.PatientOpenVisit!.HealthFacilityId = HfId;
            }

            // ******** Check and Remove
            //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            //input.FullName = input.FirstName + " " + input.LastName;


            //if (input.IsSelf == true)
            //    input.NameOfCnicHolder = input.FullName;

            //if (String.IsNullOrEmpty(input.GuardianName))
            //    input.GuardianName = input.LastName;

            //if (!string.IsNullOrEmpty(dbObj.Mrno))
            //{
            //    if (input.Cnic != dbObj?.Cnic || input.Mrno != dbObj?.Mrno || input.IsSelf != dbObj?.IsSelf)
            //        throw new UserFriendlyException(CommonMessageConstant.CannotEditRecord);
            //}

            //// ADD DASHES IN CNIC
            //if (!string.IsNullOrEmpty(input.Cnic))
            //{
            //    if (!input.Cnic.Contains("-") && input.Cnic.Length == 13)
            //    {
            //        input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
            //        input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
            //    }
            //}


            // Check if Visit Exist in IPD against Patient
            //if (input.IsFromIPD == true)
            //{
            //    var visitExist = await CheckIfPatientVisitExistInIPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);
            //}
            //// Check if Visit Exist in Emergency against Patient
            //else if (input.IsFromER == true) 
            //{
            //    var visitExist = await CheckIfPatientVisitExistInER(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
            //}
            //// check if OPD
            //else
            //{
            //    var visitExist = await CheckIfPatientVisitExistInOPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

            //}

            // check if Patient Is Already Exist in Departments
            //await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);

            //check for next station
            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.PatientOpenVisit!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.StationProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.StationProfile!.ShortName,
                Name = x.StationProfile.Name,
            }).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);
            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.PatientOpenVisit!.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();


            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station


                //// SMS on Visit Close
                //SendSMSDto smsObj = new SendSMSDto()
                //{
                //    Receiver = input!.MobileNo!.Replace("-", ""),
                //    Body = $"Dear {input.FirstName}, Thank You for Your Visit."
                //};

                //_smsService.SendSMS(smsObj);
            }

            // check if visit is already generated
            //if (_GenerateVisit_IfAlreadyExists)
            //{
            //    var checkIfVisitAlreadyExists = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId!).Where(x => x.IsDischarge != true && x.IsActive == true).Count();
            //    if (checkIfVisitAlreadyExists > 0)
            //        throw new UserFriendlyException(CommonMessageConstant.VisitIsAlreadyGenerated);
            //}

            Guid? VisitType = null;
            if (input.PatientOpenVisit!.IsFromIPD == true && input.PatientOpenVisit!.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_Refer).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else
            {
                VisitType = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }

            if (AppCommonMethod.IsNullOrEmptyGuid(VisitType))
                VisitType = null;

            // if Doctor Register then Attended By User
            if (input.PatientOpenVisit!.IsDoctor)
                input.PatientOpenVisit!.Doctor = tokenUserId;

            //dbObj.PatientOpenVisits.Clear();
            bool IsPatientUnknown = false;
            if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict && input.Patient!.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            //db
            var obj = _mapper.Map(input.Patient, dbObj);
            // No Need to Update Patient Record
            //FillEntity(obj!);

            bool IsDrugAddict = false;
            if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict)
            {
                IsDrugAddict = true;
            }
            // Generate New TokenNo
            var NewTokenNo = await GetTokenNo(input.PatientOpenVisit!.HealthFacilityId, input.PatientOpenVisit!.DepartmentLookupId);

            // Except Emergency Confirm visit when Generate or Refer
            //var isAdmitted = (input.PatientOpenVisit!.VisitFor != CommonStringConstant.ER) ? true : false;
            var isAdmitted = true;

            obj.IsPatientUnknown = IsPatientUnknown;

            var ageProfileId = await GetAgeProfileId((DateTime)obj.Dob);

            // check if patient visit exist in Today
            var checkIfVisitExistObj = await CheckIfPatientVisitExist(input.Patient.PatientId, input.PatientOpenVisit.HealthFacilityId);

            EmrPatientRegistrationResponseDto responseDto = new EmrPatientRegistrationResponseDto();
            if (!AppCommonMethod.IsNullObject(checkIfVisitExistObj))
            {

                responseDto = _mapper.Map(obj, responseDto);

                responseDto.visit.Id = checkIfVisitExistObj.PatientVisitId;
                responseDto.visit.IsVisitClosed = false;
                responseDto.visit.DateTimeVisitStart = checkIfVisitExistObj.CreatedOn;
                responseDto.visit.HealthFacility_Id = checkIfVisitExistObj.HealthFacilityId;
                responseDto.visit.Token = checkIfVisitExistObj.TokenNo;
                responseDto.visit.Patient_Id = checkIfVisitExistObj.PatientId;

                responseDto.CreatedByName = user!.FullName;
                if (!string.IsNullOrEmpty(user.DesignationName))
                    responseDto.CreatedByName += " (" + user.DesignationName + ")";
                //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
                return responseDto;

            }



            obj!.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                Age = obj.Age,
                AgeTypeProfileId = ageProfileId,
                PatientId = obj.PatientId,
                TokenNo = NewTokenNo,
                SlipNo = input.PatientOpenVisit!.SlipNo,
                VisitFor = input.PatientOpenVisit!.VisitFor,
                VisitSource = input.PatientOpenVisit!.VisitSource,
                IsAdmitted = isAdmitted,
                VisitNo = UpdateVisitNo(input.Patient!.PatientId),
                VisitTypeProfileId = VisitType,
                HealthFacilityId = input.PatientOpenVisit!.HealthFacilityId,
                BedNo = input.PatientOpenVisit!.BedNo,
                //HealthFacilityName = input
                DepartementLookupId = input.PatientOpenVisit!.DepartmentLookupId,
                SectionLookupId = input.PatientOpenVisit!.SectionLookupId,

                CurrentStationProfileId = nextStation!.StationProfileId,
                VisitDate = input.PatientOpenVisit!.VisitDate == null ? DateTime.Now : input.PatientOpenVisit!.VisitDate,
                IsFromPmis = input.PatientOpenVisit!.IsFromPMIS,
                IsFromCallCenter = input.PatientOpenVisit!.IsFromCallCenter,
                AttendedBy = input.PatientOpenVisit!.Doctor,
                IsDischarge = isVisitClose,
                IsVitalSkip = input.PatientOpenVisit!.IsVitalSkip,

                ParentPatientOpenVisitId = input.PatientOpenVisit!.ReferVisitId,
                IsReferred = input.PatientOpenVisit!.IsReferred,
                ReferredHealthFacilityId = input.PatientOpenVisit!.ReferredHealthFacilityId,
                ReferredDepartmentLookupId = input.PatientOpenVisit!.ReferredByDepartmentLookupId,
                ReferredSectionLookupId = input.PatientOpenVisit!.ReferredBySectionLookupId,
                ReferredBy = input.PatientOpenVisit!.ReferredBy,

                IsReferredIpd = input.PatientOpenVisit!.IsReferredIpd,
                IpdReferredByDepartmentLookupId = (input.PatientOpenVisit!.IsReferredIpd == true) ? input.PatientOpenVisit!.ReferredByDepartmentLookupId : null,
                IpdReferredBySectionLookupId = (input.PatientOpenVisit!.IsReferredIpd == true) ? input.PatientOpenVisit!.ReferredBySectionLookupId : null,
                IpdReferredBy = (input.PatientOpenVisit!.IsReferredIpd == true) ? input.PatientOpenVisit!.ReferredBy : null,

                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create,
                SscNumber = input.PatientOpenVisit!.SscNumber,
                IsEligibleForSsc = input.PatientOpenVisit!.IsEligibleForSsc,
                ReasonIfNotEligibleForSsc = input.PatientOpenVisit!.ReasonIfNotEligibleForSsc,
                IsDrugAddict = IsDrugAddict,
                PatinetPrivateHeathFacilityId = input.PatientOpenVisit!.PatinetPrivateHeathFacilityId
            });

            if (input.PatientOpenVisit!.IsFromIPD == true)
            {
                foreach (var patientAdmissionDetail in obj.PatientAdmissionDetails)
                {
                    FillEntityAdmissionDetails(patientAdmissionDetail);
                    patientAdmissionDetail.PatientId = obj.PatientId;
                    patientAdmissionDetail.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                    if (input.PatientOpenVisit!.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
                    {
                        patientAdmissionDetail.ShiftedFromHealthFacilityId = input.PatientOpenVisit!.ReferredHealthFacilityId;
                        patientAdmissionDetail.ShiftedFromDepartmentLookupId = input.PatientOpenVisit!.ReferredByDepartmentLookupId;
                        patientAdmissionDetail.ShiftedFromSectionLookupId = input.PatientOpenVisit!.ReferredBySectionLookupId;
                    }

                    patientAdmissionDetail.IpdVisitNo = GetIPDVisitNo(input.Patient!.PatientId);
                    patientAdmissionDetail.IpdSource = input!.PatientOpenVisit!.IPDSource;
                    patientAdmissionDetail.HealthFacilityId = input!.PatientOpenVisit!.HealthFacilityId;
                    patientAdmissionDetail.DepartmentLookupId = input!.PatientOpenVisit!.DepartmentLookupId;
                    patientAdmissionDetail.SectionLookupId = input!.PatientOpenVisit!.SectionLookupId;
                }
            }


            _uowPatient.Repository.Update(obj!);

            //await _uowPatient.CommitAsync();



            if ((bool)input?.Patient!.isEyeBlindness)
            {
                var _uowPatientEyeBlindness = new UnitOfWork<PatientEyeBlindness>(_uowPatient.GetDbContext());
                PatientEyeBlindness patientEyeBlindness = new PatientEyeBlindness();
                patientEyeBlindness = _mapper.Map<PatientEyeBlindness>(input!.Patient);
                FillEntityPatientEyeBlindness(patientEyeBlindness);
                if (!AppCommonMethod.IsNullOrEmptyGuid(obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId))
                {
                    patientEyeBlindness.PatientVisitId = obj?.PatientOpenVisits?.FirstOrDefault()?.PatientOpenVisitId;
                }

                await _uowPatientEyeBlindness.Repository.Insert(patientEyeBlindness);
                await _uowPatientEyeBlindness.Save();

            }

            if (input.PatientOpenVisit!.IsFromIPD == true && input.PatientOpenVisit!.IPDSource == (int)AdmissionSourceTypeEnum.Referred)
            {
                var dbReferVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientOpenVisit!.ReferVisitId!);

                if (!AppCommonMethod.IsNullObject(dbReferVisit) && dbReferVisit!.IsAdmittedInIpd == true)
                    throw new UserFriendlyException(CommonMessageConstant.PatientAlreadyRegistered);

                if (!AppCommonMethod.IsNullObject(dbReferVisit))
                {
                    dbReferVisit!.IsAdmittedInIpd = true;
                    dbReferVisit.UpdatedBy = _tokenService.GetUserId();
                    dbReferVisit.UpdatedOn = DateTime.Now;
                    dbReferVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    _uowPatientOpenVisit.Repository.Update(dbReferVisit!);
                    await _uowPatientOpenVisit.CommitAsync();
                }
            }

            await _uowPatient.CommitAsync();


            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPatient.GetDbContext());

            //if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict || input.Patient!.PatientRole == CommonStringConstant.Mortuary)
            //{
            //    await SaveDrugAddictPatientData(input);
            //}

            //When Request From Call Center Create Diagnose Auto & Assign Lab Test SSM
            var diagnoseResponse = false;
            //if (input.PatientOpenVisit!.IsFromCallCenter == true)
            //    diagnoseResponse = await CreatedDiagnoseWithTestForCallCenter(obj);

            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = input.Patient!.PatientId;
            objPatientWorkFlowLog.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = input.PatientOpenVisit!.HealthFacilityId;
            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(obj!.PatientOpenVisits!.FirstOrDefault()!.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreateOrEdit(objPatientWorkFlowLog);

            // Create Patient Visit Flow
            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.PatientOpenVisit!.SectionLookupId).FirstOrDefaultAsync();


            objPatientVisitFlow.CurrentDepartmentId = input.PatientOpenVisit!.DepartmentLookupId;
            objPatientVisitFlow.CurrentSectionId = input.PatientOpenVisit!.SectionLookupId;

            objPatientVisitFlow.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientVisitFlow.HealthFacilityId = obj.HealthFacilityId;

            objPatientVisitFlow.IsFilterClinic = (!AppCommonMethod.IsNullObject(sectionLookup)) ? sectionLookup!.IsFilterClinic : null;

            //Comment after disscuss with zulqarnain
            //if (sectionLookup!.Name == CommonStringConstant.OneWindowTb)
            //{
            //    var visitFlow = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId).Include(x => x.PatientVisitFlows.OrderByDescending(x => x.CreatedOn))
            //    .FirstOrDefaultAsync();

            //    if (!AppCommonMethod.IsNullObject(visitFlow.PatientVisitFlows.FirstOrDefault()))
            //        input.TbVisitNo = visitFlow.PatientVisitFlows.FirstOrDefault()!.FollowUpNo;

            //}

            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);


            //if (!AppCommonMethod.IsNullOrEmptyGuid(input.Patient!.TbPatientTypeId) || !AppCommonMethod.IsNullorEmptyDate(input.PatientOpenVisit!.VisitDate))
            //{
            //    await SaveTbPatientDetails(input, objPatientVisitFlow);
            //}



            //return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);



            //var mappedObj = _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);
            var res = GetResponseModelDto(obj, input!.SourceSystemShortName);
            res.CreatedByName = user!.FullName;
            if (!string.IsNullOrEmpty(user.DesignationName))
                res.CreatedByName += " (" + user.DesignationName + ")";
            //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return res;
        }

        private async Task<dynamic> UpdatePatient(CreateOrEditPatientWithVisitCentrallyDto input)
        {



            // check if user exist and values are edited 
            var dbObj = await _uowPatient.Repository.GetById(input.Patient!.PatientId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            if (string.IsNullOrEmpty(dbObj.Mrno))
            {
                input.Patient!.Mrno = await GenerateMRNNoCentrally(input);
            }




            var user = TokenService.GetUserLoggedInfo();


            // ******** Check and Remove
            //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            input.Patient!.FullName = input.Patient!.FirstName + " " + input.Patient!.LastName;

            if (input.Patient!.IsSelf == true)
                input.Patient!.NameOfCnicHolder = input.Patient!.FullName;

            if (String.IsNullOrEmpty(input.Patient!.GuardianName))
                input.Patient!.GuardianName = input.Patient!.LastName;

            if (!string.IsNullOrEmpty(dbObj.Mrno))
            {
                if (input.Patient!.Cnic != dbObj?.Cnic || input.Patient!.Mrno != dbObj?.Mrno || input.Patient!.IsSelf != dbObj?.IsSelf)
                    throw new UserFriendlyException(CommonMessageConstant.CannotEditRecord);
            }

            // ADD DASHES IN CNIC
            if (!string.IsNullOrEmpty(input.Patient!.Cnic))
            {
                if (!input.Patient!.Cnic.Contains("-") && input.Patient!.Cnic.Length == 13)
                {
                    input.Patient!.Cnic = input.Patient!.Cnic.Substring(0, 5) + "-" + input.Patient!.Cnic.Substring(5);
                    input.Patient!.Cnic = input.Patient!.Cnic.Substring(0, 13) + "-" + input.Patient!.Cnic.Substring(13);
                }
            }


            // Check if Visit Exist in IPD against Patient
            //if (input.IsFromIPD == true)
            //{
            //    var visitExist = await CheckIfPatientVisitExistInIPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);
            //}
            //// Check if Visit Exist in Emergency against Patient
            //else if (input.IsFromER == true) 
            //{
            //    var visitExist = await CheckIfPatientVisitExistInER(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
            //}
            //// check if OPD
            //else
            //{
            //    var visitExist = await CheckIfPatientVisitExistInOPD(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);
            //    if (visitExist == true)
            //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

            //}

            // check if Patient Is Already Exist in Departments
            //await CheckIfPatientVisitExist(input.PatientId, input.HealthFacilityId, input.DepartmentLookupId);


            // check if visit is already generated
            //if (_GenerateVisit_IfAlreadyExists)
            //{
            //    var checkIfVisitAlreadyExists = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input.PatientId!).Where(x => x.IsDischarge != true && x.IsActive == true).Count();
            //    if (checkIfVisitAlreadyExists > 0)
            //        throw new UserFriendlyException(CommonMessageConstant.VisitIsAlreadyGenerated);
            //}




            //dbObj.PatientOpenVisits.Clear();
            bool IsPatientUnknown = false;
            if (input.Patient!.PatientRole == CommonStringConstant.DrugAddict && input.Patient!.Category == CommonStringConstant.Unknown)
            {
                IsPatientUnknown = true;
            }

            //db
            var obj = _mapper.Map(input.Patient, dbObj);
            FillEntity(obj!);

            obj.IsPatientUnknown = IsPatientUnknown;

            _uowPatient.Repository.Update(obj!);
            await _uowPatient.CommitAsync();

            //return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

            //var mappedObj = _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);

            var res = GetResponseModelDto(obj, input!.SourceSystemShortName);
            res.CreatedByName = user!.FullName;
            if (!string.IsNullOrEmpty(user.DesignationName))
                res.CreatedByName += " (" + user.DesignationName + ")";
            //resp.CreatedOn = obj.PatientOpenVisits.FirstOrDefault()!.CreatedOn;
            return res;
        }

        #endregion

        public async Task CreatePatientContactDetails(List<CreatePatientContactDetailsDTO> patientContactDetailsDTOs)
        {
            foreach (var itemPatientContactDetails in patientContactDetailsDTOs)
            {
                var _uowPatientContactDetails = new UnitOfWork<PatientContactDetail>(_uowPatient.GetDbContext());
                var objPatientContactDetails = _mapper.Map<PatientContactDetail>(itemPatientContactDetails);
                FillEntityContactDetail(objPatientContactDetails);
                if (!AppCommonMethod.IsNullObject(objPatientContactDetails))
                {
                    //var patContacts = await _uowPatientContactDetails.Repository.GetALL(x => x.ContactNo == itemPatientContactDetails.ContactNo).FirstOrDefaultAsync();

                    //if (!AppCommonMethod.IsNullObject(patContacts))
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientContactNoAlreadyExists);

                    var patOpenVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == objPatientContactDetails.PatientId).FirstOrDefaultAsync();
                    if (!(AppCommonMethod.IsNullObject(patOpenVisit)))
                    {
                        objPatientContactDetails.CollectedBy = null;
                        objPatientContactDetails.DepartmentLookupId = patOpenVisit.DepartementLookupId;
                        objPatientContactDetails.SectionLookupId = patOpenVisit.SectionLookupId;
                        await _uowPatientContactDetails.Repository.Insert(objPatientContactDetails);
                        await _uowPatientContactDetails.Save();
                    }
                }
            }
        }


        public async Task<CreateOrEditPatientWithVisitDto> CreateUnknownPatient(CreateOrEditPatientWithVisitDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId))
                return await _CreateUnknownPatient(input);
            return null;
        }

        public async Task SaveBaseSixtyFour(DbModel.PatientImage _patientImage, UnitOfWork<PatientImage> _uowPatientImage, PatientImagesDTO images = null, PatientFingerprintsDTO _images = null)
        {
            var _uowImageBaseSixtyFour = new UnitOfWork<ImageBaseSixtyFour>(_uowPatient.GetDbContext());
            ImageBaseSixtyFour imageBaseSixtyFour = new ImageBaseSixtyFour();
            FillEntityPatientBae64(imageBaseSixtyFour);
            imageBaseSixtyFour.PatientImageId = _patientImage.PatientImageId;
            if (!AppCommonMethod.IsNullObject(images))
            {
                imageBaseSixtyFour.Base64 = images.base64;
            }
            else
            {
                imageBaseSixtyFour.Base64 = _images.base64;
            }
            DbModel.ImageBaseSixtyFour _imageBaseSixtyFour = await _uowImageBaseSixtyFour.Repository.Insert(imageBaseSixtyFour);
            await _uowImageBaseSixtyFour.Save();

            _patientImage.ImageBaseSixtyFourId = _imageBaseSixtyFour.ImageBaseSixtyFourId;

            await _uowPatientImage.Save();
        }

        public async Task SaveDrugAddictPatientData(CreateOrEditPatientWithVisitDto input)
        {
            var _uowPatientImage = new UnitOfWork<PatientImage>(_uowPatient.GetDbContext());
            if (!AppCommonMethod.IsNullObject(input.PatientImages))
            {
                foreach (var images in input.PatientImages)
                {
                    if (images.base64 != null)
                    {
                        if (images.base64.Contains("iVBOR"))
                        {
                            images.base64 = images.base64.Replace("iVBOR", "/9j/4");
                        }
                        //if (images.base64.Contains("image/jpg") || images.base64.Contains("image/jpeg"))
                        //{
                        PatientImage patientImage = new PatientImage();
                        FillEntityPatientImages(patientImage);
                        if (input.PatientRole == CommonStringConstant.DrugAddict)
                        {
                            patientImage.PatientId = input.PatientId;
                        }
                        else if (input.PatientRole == CommonStringConstant.Mortuary)
                        {
                            patientImage.PatientId = input.UnknownPatientId;
                        }

                        patientImage.ImageTypeProfileId = images.ProfileId;
                        patientImage.ProfileTypeId = images.ProfileTypeId;
                        var imageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.pateintImages, images.base64, _tokenService.GetAccessToken());
                        patientImage.UnknownPatientId = input.UnknownPatientId;
                        patientImage.ImageUrl = imageUrl;
                        DbModel.PatientImage _patientImage = await _uowPatientImage.Repository.Insert(patientImage);
                        await SaveBaseSixtyFour(_patientImage, _uowPatientImage, images);
                        //}
                        //else
                        //{
                        //    throw new UserFriendlyException(CommonMessageConstant.OnlyJPGImageAllowed);
                        //}
                    }
                }
            }


            if (!AppCommonMethod.IsNullObject(input.PatientFingerprints))
            {
                foreach (var images in input.PatientFingerprints)
                {
                    if (images.base64 != null)
                    {
                        //if (images.base64.Contains(",iVBOR"))
                        //{
                        //    images.base64 = images.base64.Replace(",iVBOR", ",/9j/4");
                        //}

                        if (images.base64.Contains("image/jpg"))
                        {
                            PatientImage patientImage = new PatientImage();
                            FillEntityPatientImages(patientImage);
                            if (input.PatientRole == CommonStringConstant.DrugAddict)
                            {
                                patientImage.PatientId = input.PatientId;
                            }
                            else if (input.PatientRole == CommonStringConstant.Mortuary)
                            {
                                patientImage.PatientId = input.UnknownPatientId;
                            }
                            patientImage.ImageTypeProfileId = images.fingerPrintId;
                            patientImage.ProfileTypeId = images.fingerPrintProfileTypeId;
                            images.base64 = images.base64.Replace("data:image/jpg;base64,", "");
                            images.base64 = AppCommonMethod.ConvertPngToJpegBase64(images.base64);
                            var imageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.pateintFingerprint, images.base64, _tokenService.GetAccessToken());
                            //var base64 = await _fileUploader.GetImageAsBase64Async(imageUrl);
                            patientImage.UnknownPatientId = input.UnknownPatientId;
                            patientImage.ImageUrl = imageUrl;
                            DbModel.PatientImage _patientImage = await _uowPatientImage.Repository.Insert(patientImage);
                            //await _uowPatientImage.Save();
                            await SaveBaseSixtyFour(_patientImage, _uowPatientImage, null, images);
                        }
                        else
                        {
                            throw new UserFriendlyException(CommonMessageConstant.OnlyJPGImageAllowed);
                        }
                    }

                }
            }



            if (!AppCommonMethod.IsNullObject(input.PatientSource))
            {

                var _uowPatientSourceInfo = new UnitOfWork<PatientSourceInfo>(_uowPatient.GetDbContext());
                PatientSourceInfo patientSourceInfo = new PatientSourceInfo();
                FillEntityPatientSourceInfo(patientSourceInfo);
                patientSourceInfo.PatientSourceProfileId = input.PatientSource.source;
                patientSourceInfo.Designation = input.PatientSource.designation;
                patientSourceInfo.VehicleNo = input.PatientSource.vehicleNo;
                patientSourceInfo.Name = input.PatientSource.Name;
                patientSourceInfo.ContactNo = input.PatientSource.ContactNo;
                patientSourceInfo.PatientId = input.PatientId;
                patientSourceInfo.UnknownPatientId = input.UnknownPatientId;
                await _uowPatientSourceInfo.Repository.Insert(patientSourceInfo);
                await _uowPatientSourceInfo.Save();
            }
        }



        public async Task<CreateOrEditPatientWithVisitDto> _CreateUnknownPatient(CreateOrEditPatientWithVisitDto input)
        {
            // ADD DASHES IN CNIC
            if (!input.Cnic.Contains("-"))
            {
                input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
                input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
            }


            var isVisitClose = false;
            var tokenUserId = _tokenService.GetUserId();
            var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();
            input.FullName = input.FirstName + " " + input.LastName;
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                input.HealthFacilityId = user?.HealthFacilityId;
            if (String.IsNullOrEmpty(input.GuardianName))
                input.GuardianName = input.LastName;



            if (input.IsSelf == true)
            {
                input.NameOfCnicHolder = input.FullName;

                //var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.IsSelf == true).Count();

                //if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                //    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);
            }
            else
            {
                var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.FirstName == input.FirstName && x.RelationProfileId == input.RelationProfileId).Count();

                if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
                    throw new UserFriendlyException(CommonMessageConstant.PatientExistWithRelationCnic);
            }



            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
             .Select(x => new ViewStationDto
             {
                 StationProfileId = x.StationProfileId,
                 SequenceNo = x.SequenceNo,
                 ShortName = x.StationProfile!.ShortName,
                 Name = x.StationProfile.Name,
             }).ToListAsync();

            if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            {
                //Station from Profiles
                var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

                stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    .OrderBy(x => x.SequenceNo)
                .Select(x => new ViewStationDto
                {
                    StationProfileId = x.ProfileId,
                    SequenceNo = x.SequenceNo,
                    ShortName = x.ShortName,
                    Name = x.Name
                }).ToListAsync();
            }

            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            if (AppCommonMethod.IsNullObject(thisStation))
                throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            var nextStation = new ViewStationDto();

            if (input.IsVitalSkip)
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo && x.ShortName != CommonStringConstant.VitalStation).FirstOrDefault();
            else
                nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
            if (AppCommonMethod.IsNullObject(nextStation))
            {
                isVisitClose = true;
                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station
            }


            var obj = _mapper.Map<DbModel.UnknownPatient>(input);
            FillEntityUnknownPatient(obj);

            var _uowUnknownPatient = new UnitOfWork<UnknownPatient>(_uowPatient.GetDbContext());

            DbModel.UnknownPatient responseObj = await _uowUnknownPatient.Repository.Insert(obj);
            await _uowUnknownPatient.Save();


            input.UnknownPatientId = responseObj.UnknownPatientId;

            if (input.PatientRole == CommonStringConstant.DrugAddict || input.PatientRole == CommonStringConstant.Mortuary)
            {
                await SaveDrugAddictPatientData(input);
            }

            if (input.PatientRole == CommonStringConstant.Mortuary)
            {
                var UnknownPatientObj = _mapper.Map<UnknownPatientDto>(responseObj);
                UnknownPatientObj.PatientRole = input.PatientRole;

                var response = await VerifyWithNADRA(UnknownPatientObj);
            }


            //update DataBank
            if (input.IsDataBankRecord)
            {
                DataBankService _dataBankService = new DataBankService();

                if (input.IsSelf == true)
                    await _dataBankService.UpdateHMISFlagWithCnic(input.Cnic);
                else
                    await _dataBankService.UpdateHMISFlagWithId(input.DataBankId);
            }

            return _mapper.Map<CreateOrEditPatientWithVisitDto>(responseObj);

        }

        public async Task<CreateOrEditAdditionalPatientDTO> AddNewAdditionalPatientInfo(CreateOrEditAdditionalPatientDTO input)
        {
            try
            {
                var additioanlPatientInfo = _mapper.Map<DbModel.PatientAdditionalInfo>(input);
                additioanlPatientInfo.PatientId = input.PatientId;
                additioanlPatientInfo.PatientAdditionalInfoId = Guid.NewGuid();
                //additonalInfo.PatientVisitId = 

                additioanlPatientInfo.CreatedOn = DateTime.Now;
                additioanlPatientInfo.CreatedBy = _tokenService.GetUserId();

                var additonalInfo = await _uowPatientAdditionalInfo.Repository.Insert(additioanlPatientInfo);
                await _uowPatientAdditionalInfo.Save();
                return input;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateOrEditPatientWithVisitDto> CreateOrEditWithVisitExternally(CreateOrEditPatientWithVisitDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId))
                return await CreateWithVisitExternally(input);
            else
                return await UpdateWithVisitExternally(input);
        }

        private async Task<CreateOrEditPatientWithVisitDto> CreateWithVisitExternally(CreateOrEditPatientWithVisitDto input)
        {

            // ADD DASHES IN CNIC
            if (!input.Cnic.Contains("-"))
            {
                input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
                input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
            }

            var isVisitClose = true;
            var tokenUserId = _tokenService.GetUserId();
            var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

            //checks
            input.FullName = input.FirstName + " " + input.LastName;
            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                input.HealthFacilityId = user?.HealthFacilityId;
            if (String.IsNullOrEmpty(input.GuardianName))
                input.GuardianName = input.LastName;



            if (input.IsRegisteredExternally) // if Patient is registerd Externally
            {
                if (input.IsSelf == true)
                    input.NameOfCnicHolder = input.FullName;

                var patient = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic)

                    .WhereIf(input.IsSelf == true, x => x.IsSelf == true)
                    .WhereIf(input.IsSelf != true, x => x.FirstName == input.FirstName && x.RelationProfileId == input.RelationProfileId)

                    .FirstOrDefault();

                if (!AppCommonMethod.IsNullObject(patient))
                {
                    //throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);
                    //var obj = _mapper.Map<input>(patient);
                    //_mapper.Map(obj)


                    //var updateObj = _mapper.Map<CreateOrEditPatientWithVisitDto>(patient);

                    input.PatientId = patient!.PatientId;

                    var updateResponseObj = await UpdateWithVisitExternally(input);

                    return _mapper.Map<CreateOrEditPatientWithVisitDto>(updateResponseObj);

                }

            }



            // commented
            //if (input.IsSelf == true)
            //{
            //    //input.RelationProfileId = null;
            //    input.NameOfCnicHolder = input.FullName;

            //    var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.IsSelf == true).Count();

            //    if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
            //        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithCnic);
            //}
            //else
            //{
            //    var CheckCnicExists = _uowPatient.Repository.GetALL(x => x.Cnic == input.Cnic && x.FirstName == input.FirstName && x.RelationProfileId == input.RelationProfileId).Count();

            //    if (!AppCommonMethod.IsNullorZeroInt(CheckCnicExists))
            //        throw new UserFriendlyException(CommonMessageConstant.PatientExistWithRelationCnic);
            //}

            ////check for next station from Health Facility
            //var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
            //    .Select(x => new ViewStationDto
            //    {
            //        StationProfileId = x.StationProfileId,
            //        SequenceNo = x.SequenceNo,
            //        ShortName = x.StationProfile!.ShortName,
            //        Name = x.StationProfile.Name,
            //    }).ToListAsync();

            ////if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            ////    throw new UserFriendlyException(CommonMessageConstant.HFStationsNotDefined);

            //if (AppCommonMethod.IsNullOrEmptyList(stationsList))
            //{
            //    //Station from Profiles
            //    var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

            //    stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
            //        .OrderBy(x => x.SequenceNo)
            //    .Select(x => new ViewStationDto
            //    {
            //        StationProfileId = x.ProfileId,
            //        SequenceNo = x.SequenceNo,
            //        ShortName = x.ShortName,
            //        Name = x.Name
            //    }).ToListAsync();
            //}

            //var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.RegistrationStation).FirstOrDefault();

            //if (AppCommonMethod.IsNullObject(thisStation))
            //    throw new UserFriendlyException(CommonMessageConstant.HFMustHaveRegistrationCounter);

            //var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
            //if (AppCommonMethod.IsNullObject(nextStation))
            //{
            //    isVisitClose = true;
            //    nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

            //    // SMS on Visit Close
            //    //SendSMSDto smsObj = new SendSMSDto()
            //    //{
            //    //    Receiver = input!.MobileNo!.Replace("-", ""),
            //    //    //Body = $"Dear {input.FirstName}, Thank You for Your Visit '\n' Test."

            //    //    Body = $"معزز {input.FirstName}   ،\r\nڈسٹرکٹ ہیڈ کوارٹر ہسپتال قصور تشریف آوری کا شکریہ\r\nآپ کو اوپی ڈی میں مندرجہ ذیل ادویات مفت فراہم کی گئی ہیں۔\r\n پیناڈول : صبح ، دوپہر، شام"
            //    //};

            //    //_smsService.SendSMS(smsObj);
            //}

            // Get Last Mrno
            var dbObj = await _uowTehsil.Repository.GetALL(x => x.TehsilId == input.TehsilId).Select(y => new
            {
                TehsilCode = y.Code,
                Mrno = y.Patients.OrderByDescending(x => x.Mrno).Select(x => x.Mrno).FirstOrDefault(),
            }).FirstOrDefaultAsync();

            // Mrno Generation

            input.Mrno = await GenerateMRNNo(input);

            // get Default Visit Type ID
            Guid? WalkIn = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullOrEmptyGuid(WalkIn))
                WalkIn = null;

            // Generate New TokenNo
            //var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);
            var NewTokenNo = "0001";

            //db
            var obj = _mapper.Map<DbModel.Patient>(input);
            FillEntity(obj);
            obj.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                PatientId = obj.PatientId,
                TokenNo = NewTokenNo,
                SlipNo = input.SlipNo,

                IsReferred = input.IsReferred,
                ReferredHealthFacilityId = input.ReferredHealthFacilityId,

                VisitTypeProfileId = WalkIn, // Default Visit Type Id Set to Walk In
                HealthFacilityId = input.HealthFacilityId,
                VisitNo = 1,
                DepartementLookupId = input.DepartmentLookupId,
                SectionLookupId = input.SectionLookupId,
                //CurrentStationProfileId = nextStation!.StationProfileId,
                VisitDate = DateTime.Now,
                IsFromPmis = input.IsFromPMIS,
                AttendedBy = input.Doctor,
                IsDischarge = isVisitClose,
                IsVisitExternally = input.IsRegisteredExternally,
                SourceVisitId = input.SourceVisitId,
                SourceSystemId = input.SourceSystemId,
                SourceHealthFacilityId = input.SourceHealthFacilityId,
                SourceReferredHealthFacilityId = input.SourceReferredHealthFacilityId,
                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create
            });

            DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
            await _uowPatient.Save();

            ////update DataBank
            //if (input.IsDataBankRecord)
            //{
            //    DataBankService _dataBankService = new DataBankService();

            //    if (input.IsSelf == true)
            //        await _dataBankService.UpdateHMISFlagWithCnic(input.Cnic);
            //    else
            //        await _dataBankService.UpdateHMISFlagWithId(input.DataBankId);
            //}

            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = responseObj.PatientId;
            objPatientWorkFlowLog.PatientVisitId = responseObj.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = responseObj.HealthFacilityId;
            //objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            //objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(responseObj.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);




            return _mapper.Map<CreateOrEditPatientWithVisitDto>(responseObj);
        }

        private async Task<CreateOrEditPatientWithVisitDto> UpdateWithVisitExternally(CreateOrEditPatientWithVisitDto input)
        {
            var isVisitClose = true;

            // check if user exist and key values are not edited
            var dbObj = await _uowPatient.Repository.GetById(input.PatientId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            Guid? WalkIn = await _uowProfile.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ShortName == CommonConstants.VisitType_WalkIn).Select(x => x.ProfileId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullOrEmptyGuid(WalkIn))
                WalkIn = null;

            //db

            //var obj = _mapper.Map(input, dbObj);
            var obj = dbObj;
            FillEntity(obj!);

            // Generate New TokenNo
            //var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);
            //string? NewTokenNo = null;
            var NewTokenNo = "0001";

            obj!.PatientOpenVisits.Add(new PatientOpenVisit
            {
                PatientOpenVisitId = Guid.NewGuid(),
                TokenNo = NewTokenNo,
                SlipNo = input.SlipNo,

                IsReferred = input.IsReferred,
                ReferredHealthFacilityId = input.ReferredHealthFacilityId,

                VisitNo = UpdateVisitNo(input.PatientId),
                VisitTypeProfileId = WalkIn,
                HealthFacilityId = input.HealthFacilityId,
                DepartementLookupId = input.DepartmentLookupId,
                SectionLookupId = input.SectionLookupId,
                //CurrentStationProfileId = nextStation!.StationProfileId,
                VisitDate = DateTime.Now,
                IsFromPmis = input.IsFromPMIS,
                AttendedBy = input.Doctor,
                IsDischarge = isVisitClose,

                IsVisitExternally = input.IsRegisteredExternally,
                SourceVisitId = input.SourceVisitId,
                SourceSystemId = input.SourceSystemId,
                SourceHealthFacilityId = input.SourceHealthFacilityId,
                SourceReferredHealthFacilityId = input.SourceReferredHealthFacilityId,

                IsActive = true,
                CreatedBy = _tokenService.GetUserId(),
                CreatedOn = DateTime.Now,
                ActionTypeId = (int)ActionTypeEnum.Create
            });

            _uowPatient.Repository.Update(obj!);
            await _uowPatient.CommitAsync();

            // Create Patient Work Log
            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

            objPatientWorkFlowLog.PatientId = input.PatientId;
            objPatientWorkFlowLog.PatientVisitId = obj!.PatientOpenVisits!.FirstOrDefault()!.PatientOpenVisitId;
            objPatientWorkFlowLog.HealthFacilityId = input.HealthFacilityId;
            //objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
            //objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
            objPatientWorkFlowLog.IsActive = true;
            objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(obj!.PatientOpenVisits!.FirstOrDefault()!.CreatedOn, DateTime.Now);
            await _patientWorkFlowLogService.CreateOrEdit(objPatientWorkFlowLog);

            return _mapper.Map<CreateOrEditPatientWithVisitDto>(obj);
        }

        public async Task<List<ViewPatientDiagnoseRecordDto>> CreatePmis(CreatePmisDto input)
        {
            CreateOrEditPatientWithVisitDto registrationResponse = new CreateOrEditPatientWithVisitDto();
            CreateOrEditPatientVitalDto vitalResponse = new CreateOrEditPatientVitalDto();
            var response = new List<PatientDiagnosisRecord>();

            try
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientRegister!.PatientId))
                    registrationResponse = await CreateWithVisit(input.PatientRegister);

                else
                    registrationResponse = await UpdateWithVisit(input.PatientRegister);


                if (!AppCommonMethod.IsNullObject(registrationResponse!))
                {

                    // Patient Visit Obj Imp Fields
                    var patientObj = new
                    {
                        PatientId = registrationResponse.PatientOpenVisits.FirstOrDefault()?.PatientId,
                        PatientVisitId = registrationResponse.PatientOpenVisits.FirstOrDefault()?.PatientOpenVisitId,
                        TokenNo = registrationResponse.PatientOpenVisits.FirstOrDefault()?.TokenNo,
                        Mrno = registrationResponse.Mrno,

                    };

                    if (!AppCommonMethod.IsNullObject(input.PatientVital))
                    {
                        input.PatientVital!.PatientId = patientObj.PatientId;
                        input.PatientVital!.PatientVisitId = patientObj.PatientVisitId;

                        await _patientVitalService.CreateOrEditPatientVitals(input.PatientVital!);
                    }

                    if (!AppCommonMethod.IsNullObject(input.PatientDiagnose))
                    {
                        input.PatientDiagnose!.PatientId = patientObj.PatientId;
                        input.PatientDiagnose!.PatientVisitId = patientObj.PatientVisitId;

                        var data = await _patientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescription(input.PatientDiagnose!);
                    }

                    if (!AppCommonMethod.IsNullObject(input.PatientPharmacy))
                    {
                        input.PatientPharmacy!.PatientId = patientObj.PatientId ?? Guid.Empty;
                        input.PatientPharmacy!.PatientVisitId = patientObj.PatientVisitId ?? Guid.Empty;

                        await _medicineDispatchService.CreatePatientDispatch(input.PatientPharmacy!);
                    }

                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatient.GetDbContext());

                    response = _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientVisitId == patientObj.PatientVisitId).ToList();


                }

                return _mapper.Map<List<ViewPatientDiagnoseRecordDto>>(response);

            }
            catch (Exception)
            {
                //trans.Rollback();
                throw;
            }

        }




        public async Task<List<ViewPatientDiagnoseRecordDto>> CreatePatientHistory(CreatePatientHistoryDTO input)
        {
            CreateOrEditPatientWithVisitDto registrationResponse = new CreateOrEditPatientWithVisitDto();
            CreateOrEditPatientVitalDto vitalResponse = new CreateOrEditPatientVitalDto();
            CreateOrEditPatientDiagnoseWithPrescriptionDto diagnoseResponse = new CreateOrEditPatientDiagnoseWithPrescriptionDto();
            var response = new List<PatientDiagnosisRecord>();
            int wardId = 0;

            try
            {

                //input.PatientRegister.PatientOpenVisits.Clear();

                //if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientRegister!.PatientId))
                //    registrationResponse = await CreateWithVisit(input.PatientRegister);

                //else
                //    registrationResponse = await UpdateWithVisit(input.PatientRegister);


                if (!AppCommonMethod.IsNullObject(registrationResponse!))
                {

                    // Patient Visit Obj Imp Fields
                    var patientObj = new
                    {
                        PatientId = input.PatientRegister?.PatientId,
                        PatientVisitId = input.PatientRegister?.PatientOpenVisits.FirstOrDefault()?.PatientOpenVisitId,
                        TokenNo = input.PatientRegister?.PatientOpenVisits.FirstOrDefault()?.TokenNo,
                        Mrno = input?.PatientRegister?.Mrno,

                    };

                    if (!AppCommonMethod.IsNullObject(input.PatientVital))
                    {
                        input.PatientVital!.PatientId = patientObj.PatientId;
                        input.PatientVital!.PatientVisitId = patientObj.PatientVisitId;

                        await _patientVitalService.CreateOrEditPatientVitals(input.PatientVital!);
                    }

                    if (!AppCommonMethod.IsNullObject(input.PatientDiagnose))
                    {
                        input.PatientDiagnose!.PatientId = patientObj.PatientId;
                        input.PatientDiagnose!.PatientVisitId = patientObj.PatientVisitId;
                        input.PatientDiagnose.IsPatientHistory = true;
                        diagnoseResponse = await _patientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescription(input.PatientDiagnose!);
                    }

                    if (input?.PatientDiagnose?.PatientPrescriptions.Count > 0)
                    {
                        CreateOrEditPatientMedicineDispatchDto PatientPharmacy = new CreateOrEditPatientMedicineDispatchDto();
                        PatientPharmacy!.PatientId = patientObj.PatientId ?? Guid.Empty;
                        PatientPharmacy!.PatientVisitId = patientObj.PatientVisitId ?? Guid.Empty;


                        if (diagnoseResponse.FormType == CommonStringConstant.TbForm)
                        {
                            var _uowSection = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                            wardId = (int)await _uowSection.Repository.GetALL(x => x.FormType == CommonStringConstant.TbForm).Select(x => x.MimsWardId).FirstOrDefaultAsync();
                        }


                        var userObj = await GetLoggedInfoUserDTO();
                        if (userObj.UserRoleList.Where(x => x.ShortName == RoleConst.PatinetHistory).Count() > 0)
                            PatientPharmacy.IsPatientHistory = true;


                        foreach (var medicine in input?.PatientDiagnose?.PatientPrescriptions)
                        {
                            MedicineDispatchDto medicineDispatchDto = new MedicineDispatchDto();
                            medicineDispatchDto = _mapper.Map<MedicineDispatchDto>(medicine);
                            medicineDispatchDto.QuantityDispatch = medicine.Quantity;
                            medicineDispatchDto.QuantityPrescribed = medicine.Quantity;
                            medicineDispatchDto.IsActive = true;
                            medicineDispatchDto.WardId = wardId;
                            PatientPharmacy.MedicineDispatches.Add(medicineDispatchDto);
                        }



                        await _medicineDispatchService.CreatePatientDispatch(PatientPharmacy);
                    }



                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatient.GetDbContext());

                    response = _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientVisitId == patientObj.PatientVisitId).ToList();


                }

                return _mapper.Map<List<ViewPatientDiagnoseRecordDto>>(response);
            }
            catch (Exception)
            {
                //trans.Rollback();
                throw;
            }

        }


        public async Task<List<ViewPatientDiagnoseRecordDto>> CreatePatientLabTestExternally(List<CreateOrEditPatientExternallyDto> inputDto)
        {
            using (var trans = _uowPatient.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    //var _uowPatient = new UnitOfWork<Patient>(_uowPatientLabTest.GetDbContext());
                    var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());
                    var _uowTehsil = new UnitOfWork<Tehsil>(_uowPatient.GetDbContext());
                    var _uowHealthFacility = new UnitOfWork<HealthFacility>(_uowPatient.GetDbContext());
                    var _uowPatientDiagonose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                    var _uowSourceSystem = new UnitOfWork<SourceSystem>(_uowPatient.GetDbContext());

                    var healthFacilityList = await _uowHealthFacility.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    var tehsilList = await _uowPatient.GetDbContext().ViewHfLocations.ToListAsync();

                    var genderList = await _uowProfile.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ProfileType.ShortName == CommonConstants.Gender).ToListAsync();

                    var relationList = await _uowProfile.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.ProfileType.ShortName == CommonConstants.RelationShip).ToListAsync();

                    var sourceSystemList = await _uowSourceSystem.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var input in inputDto)
                    {
                        var sourceSystem = sourceSystemList.Find(x => x.ShortName!.ToLower().Trim() == input.SystemSourceShortName!.ToLower().Trim());
                        if (AppCommonMethod.IsNullObject(sourceSystem))
                            throw new UserFriendlyException(CommonMessageConstant.SourceSystemNotFound);

                        var relation = relationList.Find(x => x.Name.ToLower().Trim() == input.Relation!.ToLower().Trim());

                        if (AppCommonMethod.IsNullObject(relation))
                            relation = relationList.Find(x => x.Name.ToLower().Trim() == CommonConstants.Self.ToLower().Trim());

                        var gender = genderList.Find(x => x.Name.ToLower().Trim() == input.Gender!.ToLower().Trim());

                        var healthFacility = healthFacilityList.Find(x => x.HrId == input.HealthFacilityHrId);

                        var referredHealthFacility = healthFacilityList.Find(x => x.HrId == input.RefferedHealthFacilityHrId);

                        var tehsil = tehsilList.Find(x => x.TehsilName!.ToLower().Trim() == input!.TehsilName.ToLower().Trim());

                        if (AppCommonMethod.IsNullObject(tehsil))
                            throw new UserFriendlyException(CommonMessageConstant.TehsilNotFound);

                        var patientDto = new CreateOrEditPatientWithVisitDto();

                        patientDto.IsDataBankRecord = false;
                        patientDto.DataBankId = null;


                        patientDto.SourcePatientId = input.SourcePatientId;
                        patientDto.SourceMrno = input.SourceMrno;
                        patientDto.SourceSystemId = sourceSystem!.SourceSystemId;
                        patientDto.SourceVisitId = input.SourceVisitId;
                        patientDto.IsSelf = input.IsSelf;

                        if (!AppCommonMethod.IsNullObject(gender))
                            patientDto.GenderProfileId = gender!.ProfileId;

                        if (!AppCommonMethod.IsNullObject(relation))
                            patientDto.RelationProfileId = relation!.ProfileId;


                        // Format Cnic
                        input.Cnic = input.Cnic.Replace("-", "");

                        if (!input.Cnic.Contains("-"))
                        {
                            input.Cnic = input.Cnic.Substring(0, 5) + "-" + input.Cnic.Substring(5);
                            input.Cnic = input.Cnic.Substring(0, 13) + "-" + input.Cnic.Substring(13);
                        }

                        // Format MobileNo
                        if (!string.IsNullOrEmpty(input.MobileNo))
                        {
                            input.MobileNo = input.MobileNo!.Replace("-", "");

                            if (!input.MobileNo.Contains("-"))
                            {
                                input.MobileNo = input.MobileNo.Substring(0, 4) + "-" + input.MobileNo.Substring(4);

                            }
                        }


                        patientDto.Cnic = input.Cnic;
                        patientDto.FirstName = input.FirstName;
                        patientDto.LastName = input.LastName;
                        patientDto.Dob = input.Dob;
                        patientDto.Age = input.Age;
                        patientDto.MobileNo = input.MobileNo;
                        patientDto.ParmanentAddress = input.ParmanentAddress;

                        patientDto.NameOfCnicHolder = input.NameOfCnicHolder;

                        patientDto.IsFromPMIS = false;
                        patientDto.Doctor = null;
                        patientDto.IsActive = true;




                        if (AppCommonMethod.IsNullObject(healthFacility))
                            throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotFound);
                        else
                            patientDto.HealthFacilityId = healthFacility!.HealthFacilityId;

                        if (AppCommonMethod.IsNullObject(referredHealthFacility))
                            throw new UserFriendlyException(CommonMessageConstant.ReferredHealthFacilityNotFound);
                        else
                            patientDto.ReferredHealthFacilityId = referredHealthFacility!.HealthFacilityId;

                        if (!AppCommonMethod.IsNullorZeroInt(patientDto.ReferredHealthFacilityId))
                            patientDto.IsReferred = true;


                        patientDto.IsRegisteredExternally = true;
                        patientDto.SourceSystemId = sourceSystem!.SourceSystemId;
                        patientDto.SourceReferredHealthFacilityId = input.RefferedHealthFacilityId;
                        patientDto.SourceHealthFacilityId = input.HealthFacilityId;

                        if (!AppCommonMethod.IsNullObject(tehsil))
                        {
                            patientDto.ProvinceId = tehsil!.ProvinceId;
                            patientDto.DivisionId = tehsil.DivisionId;
                            patientDto.DistrictId = tehsil.DistrictId;
                            patientDto.TehsilId = tehsil.TehsilId;
                        }

                        //var patientVisitResponse = await CreateOrEditWithVisit(patientDto);
                        var patientVisitResponse = await CreateOrEditWithVisitExternally(patientDto);

                        if (!AppCommonMethod.IsNullObject(patientVisitResponse))
                        {
                            var patientDiagnose = new CreateOrEditPatientDiagnoseWithPrescriptionDto();

                            patientDiagnose.PatientId = patientVisitResponse.PatientOpenVisits.FirstOrDefault()!.PatientId;
                            patientDiagnose.PatientVisitId = patientVisitResponse.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;

                            patientDiagnose.IsMedicine = false;
                            patientDiagnose.IsLab = true;
                            patientDiagnose.IsRefer = false;
                            patientDiagnose.IsVisitClose = true;
                            patientDiagnose.IsDiagnoseExternally = true;
                            patientDiagnose.SourceSystemId = sourceSystem!.SourceSystemId;
                            patientDiagnose.SourceDoctorName = input.DoctorName;
                            patientDiagnose.IsActive = true;
                            patientDiagnose.ReferredHealthFacilityId = patientDto.ReferredHealthFacilityId;

                            foreach (var patientLabTest in input.PatientLabTests)
                            {
                                var labTest = new CreateOrEditPatientLabTestDto();


                                labTest.PatientId = patientDiagnose.PatientId;
                                labTest.PatientVisitId = patientDiagnose.PatientVisitId;
                                labTest.LabDepartmentProfileId = patientLabTest.LabDepartmentProfileId;
                                labTest.LabTestId = patientLabTest.LabTestId;
                                labTest.SourcePkId = input.PkId;
                                labTest.SourceLabTestId = patientLabTest.SourceLabTestId;
                                labTest.IsAdvisedExternally = true;
                                labTest.SourceSystemId = sourceSystem!.SourceSystemId;
                                labTest.BarcodeNo = patientLabTest.SourceBarcodeNo;
                                labTest.SourceBarcode = patientLabTest.SourceBarcodeBase64;
                                labTest.SourceDoctorName = input.DoctorName;
                                labTest.IsActive = true;

                                patientDiagnose.PatientLabTests.Add(labTest);
                            }

                            var responseDiagnose = await _patientDiagnoseService.CreatePatientDiagnoseWithPrescriptionExternaly(patientDiagnose, _uowPatientDiagonose);
                        }
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return null;

        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatient.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatient.Repository.Update(dbObj!);
            await _uowPatient.CommitAsync();
            return true;
        }



        public async Task UpdatePrintStatusPatientDiagnose(Guid PatientDiagnoseId)
        {
            var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatient.GetDbContext());
            var data = await _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == PatientDiagnoseId).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(data))
            {
                data.IsPrinted = true;
                FillEntityPatientDiagnosisRecord(data);
                _uowPatientDiagnoseRecord.Repository.Update(data);
                await _uowPatientDiagnoseRecord.Save();
            }
            else
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);
        }


        public async Task<FeePayment> SaveFeePayment(FeePayment feePayment)
        {
            var _uowFeePayment = new UnitOfWork<FeePayment>(_uowPatient.GetDbContext());


            var data = await _uowFeePayment.Repository.GetALL(x => x.PatientVisitId == feePayment.PatientVisitId && feePayment.SectionLookupId == x.SectionLookupId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(data) && AppCommonMethod.IsNullBool(data!.IsRefund))
                throw new UserFriendlyException(CommonMessageConstant.FeedPaid);

            FillEntityFeePayment(feePayment);
            await _uowFeePayment.Repository.Insert(feePayment);
            await _uowFeePayment.Save();

            return feePayment;
        }


        public async Task<FeePayment> RefundFee(FeePayment feePayment)
        {
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());


            var data = await _uowPatientOpenVisit.Repository.GetById(feePayment.PatientVisitId);
            if (!AppCommonMethod.IsNullObject(data))
            {
                if (data!.VisitDate != DateTime.UtcNow.Date)
                    throw new UserFriendlyException(CommonMessageConstant.FeeRefundDateHasPassed);

                var _uowDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());

                var _data = await _uowDiagnose.Repository.GetALL(x => x.PatientVisitId == feePayment.PatientVisitId && x.DocSectionLookupId == feePayment.SectionLookupId).FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullObject(_data))
                    throw new UserFriendlyException(CommonMessageConstant.FeeCannotRefund);

                var _uowFeePayment = new UnitOfWork<FeePayment>(_uowPatient.GetDbContext());


                var feePaymentData = await _uowFeePayment.Repository.GetById(feePayment.FeePaymentId);


                feePaymentData.IsRefund = true;
                feePaymentData.RefundReason = feePayment.RefundReason;

                FillEntityFeePayment(feePaymentData);
                _uowFeePayment.Repository.Update(feePaymentData);
                await _uowFeePayment.Save();

                return feePaymentData;
            }

            return null;
        }


        #endregion

        #region Read Operations

        //public async Task<List<ViewPatientDto>> GetAll(Expression<Func<DbModel.Patient, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    string includeProperties = "")
        //{
        //    List<DbModel.Patient> responseObj = await _uowPatient.Repository.GetALL(filter).ToListAsync();
        //    return _mapper.Map<List<ViewPatientDto>>(responseObj);
        //}

        //public async Task<bool> IsPatientAdviseMedicineInLastVisit(string Cnic, int DeptId, int SectionId)
        //{

        //    var patientData = await _uowPatient.Repository.GetALL(x => x.Cnic == Cnic).FirstOrDefaultAsync();
        //    if (!AppCommonMethod.IsNullObject(patientData))
        //    {
        //        var _uowPatientVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatient.GetDbContext());
        //        var lastPatientDiagnose = await _uowPatientVisitFlow.GetDbContext().PatientDiagnoses
        //       .Where(x => x.DocDepartmentLookupId == DeptId && x.DocSectionLookupId == SectionId && x.PatientId == patientData!.PatientId && x.FormType == CommonStringConstant.TbForm)
        //       .OrderByDescending(x => x.CreatedOn)
        //       .FirstOrDefaultAsync();
        //        //&& !AppCommonMethod.IsNullorEmptyDate(lastPatientDiagnose.FollowupDate)
        //        if (!AppCommonMethod.IsNullObject(lastPatientDiagnose))
        //        {
        //            var lastPatientVisitFlow = await _uowPatientVisitFlow.Repository.GetALL().Where(x =>
        //            x.PatientVisitId == lastPatientDiagnose.PatientVisitId
        //            && x.CurrentDepartmentId == DeptId
        //            && x.CurrentSectionId == SectionId && x.IsFollowUp == true)
        //                .FirstOrDefaultAsync();
        //            if (!AppCommonMethod.IsNullObject(lastPatientVisitFlow))
        //            {
        //                if (AppCommonMethod.IsNullObject(lastPatientVisitFlow.FollowUpNo) || lastPatientVisitFlow.FollowUpNo >= 0)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }

        //}

        public async Task<AdditionlInfoDto> GetPatientAdditionalInfoByPatientId(Guid PatientVisitId)
        {
            try
            {
                var result = await _uowPatient.GetDbContext().GetAdditionalInfos
                    .Where(x => x.PatientOpenVisitId == PatientVisitId)
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefaultAsync();

                var AdditionlInfo = new AdditionlInfoDto();





                if (!AppCommonMethod.IsNullObject(result))
                {

                    if (!String.IsNullOrEmpty(result.MlcType) && result.MlcType == "MLE")
                    {
                        AdditionlInfo.EmcMleFormOpenType = "MLEFORM";
                    }

                    if (!String.IsNullOrEmpty(result.MlcType) && result.MlcType == "MLESV")
                    {
                        AdditionlInfo.EmcMleFormOpenType = "MLESV";
                    }

                    if (!String.IsNullOrEmpty(result.MlcType) && result.MlcType == "Post Mortem")
                    {
                        AdditionlInfo.EmcMleFormOpenType = "PostMortem";
                    }

                    AdditionlInfo.PatientAdditionalInfoId = result.PatientAdditionalInfoId;
                    AdditionlInfo.PatientId = result.PatientId;
                    AdditionlInfo.HealthFacilityId = result.HealthFacilityId;
                    AdditionlInfo.GuardianName = result.GuardianName;
                    AdditionlInfo.GuardianCnic = result.GuardianCnic;
                    AdditionlInfo.GuardianAddress = result.GuardianAddress;
                    AdditionlInfo.GuardianMobileNo = result.GuardianMobileNo;
                    AdditionlInfo.Caste = result.Caste;
                    AdditionlInfo.Occupation = result.Occupation;
                    AdditionlInfo.PoliceDistrict = result.PoliceDistrict;
                    AdditionlInfo.CaseAgainst = result.CaseAgainst;
                    AdditionlInfo.PoliceDocketOne = result.PoliceDocketOne;
                    AdditionlInfo.PoliceDocketTwo = result.PoliceDocketTwo;
                    AdditionlInfo.PoliceDocketThree = result.PoliceDocketThree;

                    if (!String.IsNullOrEmpty(result.MleIncidentPlace))
                    {
                        AdditionlInfo.IncidentPlace = result.MleIncidentPlace;
                    }

                    if (!String.IsNullOrEmpty(result.MlcSvIncidentPlace))
                    {
                        AdditionlInfo.IncidentPlace = result.MlcSvIncidentPlace;
                    }

                    if (!String.IsNullOrEmpty(result.PmeIncidentPlace))
                    {
                        AdditionlInfo.IncidentPlace = result.PmeIncidentPlace;
                    }

                    if (!AppCommonMethod.IsNullOrEmptyGuid(result.MlcSvCaseTypeProfileId))
                    {
                        AdditionlInfo.CaseTypeProfileId = result.MlcSvCaseTypeProfileId;
                    }

                    if (!AppCommonMethod.IsNullOrEmptyGuid(result.CaseTypeProfileId))
                    {
                        AdditionlInfo.CaseTypeProfileId = result.CaseTypeProfileId;
                    }


                    AdditionlInfo.DoctorId = result.DoctorId;
                    AdditionlInfo.Mlcno = result.Mlcno;
                    AdditionlInfo.MlctypeProfileId = result.MlctypeProfileId;

                    return AdditionlInfo;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AdditionlInfoDto> GetPatientAdditionalInfoByPatientIdForPostMortem(Guid PatientId)
        {
            try
            {
                var result = await _uowPatient.GetDbContext().GetAdditionalInfos.Where(x => x.PatientId == PatientId).
                    FirstOrDefaultAsync();

                var AdditionlInfo = new AdditionlInfoDto();
                AdditionlInfo.PatientAdditionalInfoId = result.PatientAdditionalInfoId;
                AdditionlInfo.PatientId = result.PatientId;
                AdditionlInfo.HealthFacilityId = result.HealthFacilityId;
                AdditionlInfo.GuardianName = result.GuardianName;
                AdditionlInfo.GuardianCnic = result.GuardianCnic;
                AdditionlInfo.GuardianAddress = result.GuardianAddress;
                AdditionlInfo.GuardianMobileNo = result.GuardianMobileNo;
                AdditionlInfo.Caste = result.Caste;
                AdditionlInfo.Occupation = result.Occupation;
                AdditionlInfo.PoliceDocketOne = result.PoliceDocketOne;
                AdditionlInfo.PoliceDocketTwo = result.PoliceDocketTwo;
                AdditionlInfo.PoliceDocketThree = result.PoliceDocketThree;

                AdditionlInfo.IncidentPlace = result.PmeIncidentPlace;
                AdditionlInfo.CaseTypeProfileId = result.CaseTypeProfileId;
                AdditionlInfo.DoctorId = result.DoctorId;
                AdditionlInfo.Mlcno = result.Mlcno;
                AdditionlInfo.MlctypeProfileId = result.MlctypeProfileId;


                return AdditionlInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<UserLoggedInfoDTO> GetLoggedInfoUserDTO()
        {
            var userId = _tokenService.GetUserId();
            var userData = await _uowUser.Repository.GetALL(x => x.UserId == userId).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync();

            var userObj = new UserLoggedInfoDTO()
            {
                UserRoleList = userData.UserRoles.Select(y => new UserRoleDto
                {
                    Name = y.Role.Name,
                    ShortName = y.Role.ShortName,
                    RoleId = y.Role.RoleId,
                    RoutingUrl = y.Role?.RoutingUrl
                }).ToList()
            };
            return userObj;
        }

        public async Task<ViewPagerDto<ViewPatientDto>> GetAll(FilterPatientDto filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var finalList = _uowPatient.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.HealthFacility)
                    .ThenInclude(x => x!.Tehsil)
                        .ThenInclude(x => x!.District)
                            .ThenInclude(x => x!.Division)
                                .ThenInclude(x => x!.Province)


                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.FullName!.ToLower().StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.MobileNo!.ToLower().StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedBy.ToString()!))

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacility!.Tehsil!.District!.Division!.Province!.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacility!.Tehsil!.District!.Division!.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacility!.Tehsil!.District!.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacility!.Tehsil!.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ViewPatientDto
                {
                    PatientId = x.PatientId,
                    FullName = x.FirstName + " " + x.LastName,
                    MobileNo = x.MobileNo,
                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    GuardianName = x.GuardianName,
                    Age = x.Age,
                    Dob = x.Dob,
                    CreatedBy = x.CreatedByNavigation!.FullName!,
                    CreatedOn = x.CreatedOn,
                    UpdatedBy = x.UpdatedByNavigation!.FullName!,
                    UpdatedOn = x.UpdatedOn
                });



            var pagedList = await PagedListDto<ViewPatientDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        //public async Task<ResponsePaginatedDTO> GetAll(FilterPatientDto filter)
        //{

        //    var list = _uowPatient.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.FullName!.ToLower().StartsWith(filter.FullName!))
        //        .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
        //        .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.MobileNo!.ToLower().StartsWith(filter.MobileNo!))
        //        .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
        //        .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId());

        //    IQueryable<ViewPatientDto> IQueryableList = list.Select(x =>
        //        new ViewPatientDto
        //        {
        //            PatientId = x.PatientId,
        //            FullName = x.FirstName + " " + x.LastName,
        //            MobileNo = x.MobileNo,
        //            Mrno = x.Mrno,
        //            Cnic = x.Cnic,
        //            GuardianName = x.GuardianName,
        //            Age = x.Age,
        //            Dob = x.Dob

        //        });

        //    var pagedList = PagedListDto<ViewPatientDto>.ToPagedList(
        //           IQueryableList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ResponsePaginatedDTO
        //    {
        //        totalRecords = pagedList.TotalCount,
        //        pageCount = pagedList.TotalPages,
        //        data = pagedList
        //    };

        //    return responseObject;
        //}

        public async Task<ViewPatientDto> GetById(Guid input)
        {
            DbModel.Patient? responseObj = await _uowPatient.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDto>(responseObj);
        }



        public async Task<ViewPatientDetailsDto> GetByIdWithDetails(Guid input)
        {
            DbModel.Patient? responseObj = await _uowPatient.Repository.GetALL(x => x.PatientId == input)
                                            .Include(x => x.PatientOpenVisits)
                                            .Include(x => x.PatientVitals)
                                            .Include(x => x.PatientDiagnoses)
                                            .Include(x => x.PatientPrescriptions)
                                            .Include(x => x.PatientLabTests)
                                            .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDetailsDto>(responseObj);
        }

        // Previous Working for Doc
        public async Task<ViewPatientDetailsDto> GetAllDetailsByVisitId(Guid input)
        {
            DbModel.PatientOpenVisit? responseObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                                                    .Include(x => x.Patient)
                                                    .Include(x => x.PatientVitals)
                                                    .Include(x => x.PatientDiagnoses)
                                                    .Include(x => x.PatientPrescriptions)
                                                    .Include(x => x.PatientLabTests)
                                                    .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDetailsDto>(responseObj!.Patient);
        }

        public async Task<List<ViewPatientDto>> GetByKeys(string SearchKey, string SearchValue)
        {
            List<ViewPatientDto> patientList = new List<ViewPatientDto>();

            var SearchValueWithoutDashes = SearchValue.Replace("-", "");

            if (SearchValue == "11111-1111111-1" || SearchValue == "22222-2222222-2" || SearchValue == "33333-3333333-3" || SearchValue == "44444-4444444-4" || SearchKey == "55555-5555555-5" || SearchValue == "66666-6666666-6" || SearchValue == "77777-7777777-7")
                throw new UserFriendlyException(CommonMessageConstant.CNICINVALID);


            if (!_isPatientSearchUsingSP)
            {
                var responseObj = await _uowPatient.Repository.GetALL()
                    .WhereIf(SearchKey == "Pak CNIC", x => x.Cnic == SearchValue || x.Cnic == SearchValueWithoutDashes && x.ActionTypeId != 3)
                    .WhereIf(SearchKey == "Mobile No", x => x.MobileNo == SearchValue && x.ActionTypeId != 3)
                    .WhereIf(SearchKey == "MR No", x => x.Mrno == SearchValue && x.ActionTypeId != 3)
                    .WhereIf(SearchKey == "Afghan CNIC", x => x.Cnic == SearchValue && x.ActionTypeId != 3)
                    .WhereIf(SearchKey == "Passport No", x => x.PassportNo == SearchValue && x.ActionTypeId != 3)
                    .Where(x => x.IsVerifiedFromNadra == null && x.OldPatientId == null)

                    .Include(x => x.RelationProfile)
                    .Include(x => x.HealthFacility)
                    .Include(x => x.Province)
                    .Include(x => x.Division)
                    .Include(x => x.District)
                    .Include(x => x.Tehsil)
                    .ToListAsync();

                var refinePatientList = _mapper.Map<List<ViewPatientDto>>(responseObj);
                patientList.AddRange(refinePatientList);
            }
            else
            {
                var responseObj = await GetPatient(SearchKey, SearchValue);
                var refinePatientList = _mapper.Map<List<ViewPatientDto>>(responseObj);
                patientList.AddRange(refinePatientList);
            }

            // Data Bank
            if (_isPatientSearchViaHubDb)
            {
                var dataBank = await _uowPerson.Repository.GetALL(x => (x.Cnic == SearchValue || x.MobileNumber == SearchValue) && x.IsRegisteredHmis == false).ToListAsync();
                if (SearchKey != CommonStringConstant.MrNo)
                {
                    DataBankService _dataBankService = new DataBankService();
                    var dataBankk = await _dataBankService.GetPatientFromDataBank(SearchKey, SearchValue);
                    List<ViewPatientDto> dataBankPatientList = await GetDataBankPatientProfiles(dataBankk);
                    patientList.AddRange(dataBankPatientList);
                }
            }


            return patientList;
        }

        public async Task<List<GetPatientByFilterDto>> GetPatient(string SearchKey, string SearchValue)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[pt].[GetPatientByFilter]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@SearchKey", SearchKey);
                    sqlComm.Parameters.AddWithValue("@SearchValue", SearchValue);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    await Task.Run(() => da.Fill(ds));
                    List<GetPatientByFilterDto> lst = ds.Tables[0].ToList<GetPatientByFilterDto>();
                    return lst;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }


            //List<ViewPatientDto> patientList = new List<ViewPatientDto>();

            //var responseObj = await _uowPatient.Repository.GetALL(x => x.Mrno == MrNo)
            //    .Include(x => x.RelationProfile)
            //    .Include(x => x.HealthFacility)
            //    .Include(x => x.Province)
            //    .Include(x => x.Division)
            //    .Include(x => x.District)
            //    .Include(x => x.Tehsil)
            //    .ToListAsync();

            //var patientDetail = _mapper.Map<List<ViewPatientDto>>(responseObj);

            //return patientDetail;

        }

        public async Task<List<ViewPatientDto>> GetByMrNo(string MrNo)
        {
            List<ViewPatientDto> patientList = new List<ViewPatientDto>();

            var responseObj = await _uowPatient.Repository.GetALL(x => x.Mrno == MrNo)
                .Include(x => x.RelationProfile)
                .Include(x => x.HealthFacility)
                .Include(x => x.Province)
                .Include(x => x.Division)
                .Include(x => x.District)
                .Include(x => x.Tehsil)
                .ToListAsync();

            var patientDetail = _mapper.Map<List<ViewPatientDto>>(responseObj);

            return patientDetail;

        }

        private async Task<List<ViewPatientDto>> GetDataBankPatientProfiles(List<HMIS.Patient.Domain.Models.HMIS_HUBModels.Person> dataBank)
        {
            if (dataBank.Count() <= 0)
                return new List<ViewPatientDto>();


            var genderList = await _uowProfile.Repository.GetALL().Include(x => x.ProfileType).Where(x => x.ProfileType.ShortName == CommonConstants.Gender).ToListAsync();

            var relationShipList = await _uowProfile.Repository.GetALL().Include(x => x.ProfileType).Where(x => x.ProfileType.ShortName == CommonConstants.RelationShip).ToListAsync();

            var provinceList = await _uowProvince.Repository.GetALL().ToListAsync();

            return dataBank.Select(x => new ViewPatientDto
            {
                DataBankId = x.Id,
                IsDataBankRecord = true,
                FirstName = x.NameTitle ?? string.Empty,
                LastName = x.LastName ?? string.Empty,
                FullName = x.NameTitle ?? string.Empty,
                Cnic = x.Cnic ?? string.Empty,
                MobileNo = x.MobileNumber ?? string.Empty,
                //Dob = x.Dob ?? DateTime.Now.Date,
                Dob = x.Dob ?? null,
                NameOfCnicHolder = x.CnicrelationName == CommonConstants.Self ? x.FirstName : x.LastName,

                IsSelf = x.CnicrelationName == CommonConstants.Self ? true : false,
                DataBankSource = x.Source ?? string.Empty,
                HealthFacility = new Domain.Models.DTO.HealthFacilityDto.ViewHealthFacilityDto { Name = x.HealthFacilityName },
                RelationProfileId = relationShipList.Where(y => y.Name == x.CnicrelationName).FirstOrDefault()?.ProfileId ?? null,
                RelationProfile = relationShipList.Where(y => y.Name == x.CnicrelationName).Select(x => new ViewProfileDto { ProfileId = x.ProfileId, Name = x.Name }).FirstOrDefault(),
                GenderProfileId = genderList.FirstOrDefault(y => y.Name == x.Gender)?.ProfileId ?? null,
                ProvinceId = provinceList.Where(y => y.Code == x?.Cnic[0].ToString()).FirstOrDefault()?.ProvinceId ?? 1, //Default Punjab 
                ParmanentAddress = x.PermanentAddress ?? string.Empty,
                TemporaryAddress = x.CorrespondenceAddress ?? string.Empty,
            }).ToList();
        }

        public async Task<List<ViewPatientDto>> CheckInProfile(string SearchKey)
        {
            //DbModel.Patient? responseObj = new DbModel.Patient();

            var responseObj = await _uowPerson.Repository.GetALL(x => x.Cnic == SearchKey || x.MobileNumber == SearchKey).ToListAsync();

            //if(AppCommonMethod.IsNullObject(responseObj))
            //    var profile = await _uowPerson.Repository.GetALL(x => x.Cnic == cnic).FirstOrDefaultAsync();


            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<ViewPatientDto>>(responseObj);
        }

        public async Task<List<ViewPatientHistoryListDto>> PatientVisitHistory(Guid input)
        {
            var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientOpenVisit.GetDbContext());
            var _uowPatientDiagnoseDisease = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientOpenVisit.GetDbContext());

            var DiseaseList = _uowPatientDiagnoseDisease.Repository.GetALL(x => x.PatientId == input)
            .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(y => new ViewDiseaseListDto
            {
                Id = y.DiseaseProfile.ProfileId,
                Name = y.DiseaseProfile.Name,
                PatientVisitId = y.PatientDiagnose.PatientVisitId ?? Guid.Empty,
                PatientDiagnoseId = y.PatientDiagnoseId,
            }).ToList();

            var patientVisits = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == input)
            .OrderByDescending(x => x.CreatedOn)
            .Include(x => x.PatientVisit)
                .ThenInclude(x => x!.HealthFacility)
            .Select(y => new ViewPatientHistoryListDto
            {
                PatientOpenVisitId = y.PatientVisit!.PatientOpenVisitId,
                PatientDiagnoseId = y.PatientDiagnoseId,
                PatientId = y.PatientId,
                FormType = y.FormType,
                TokenNo = y.PatientVisit.TokenNo,
                VisitNo = y.PatientVisit.VisitNo,
                HealthFacilityId = y.PatientVisit.HealthFacilityId,
                HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                DepartementLookupId = y.PatientVisit.DepartementLookupId,
                DepartementLookupName = y.DocDepartmentLookup!.Name,
                SectionLookupId = y.PatientVisit.SectionLookupId,
                SectionLookupName = y.DocSectionLookup!.Name,
                CurrentStationProfileId = y.PatientVisit.CurrentStationProfileId,
                VisitDate = y.PatientVisit.VisitDate,
                CreatedOn = y.CreatedOn,
                DiagnosedBy = y.DiagnosedByNavigation!.FullName,
                DiagnosedByDesignation = y.DiagnosedByNavigation!.DesignationProfile.Name,
                IsDischarge = y.PatientVisit.IsDischarge,
                IsActive = y.IsActive,
                HasLabTest = y.PatientLabTests.Count() > 0
            })
            .ToListAsync();

            //var patientVisits = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == input)
            //                           .OrderByDescending(x => x.VisitDate).ThenByDescending(x => x.TokenNo)
            //                           .Include(x => x.HealthFacility)
            //                           .Select(y => new ViewPatientHistoryListDto
            //                           {
            //                               PatientOpenVisitId = y.PatientOpenVisitId,
            //                               PatientId = y.PatientId,
            //                               TokenNo = y.TokenNo,
            //                               VisitNo = y.VisitNo,
            //                               HealthFacilityId = y.HealthFacilityId,
            //                               HealthFacilityName = y.HealthFacility!.Name,
            //                               DepartementLookupId = y.DepartementLookupId,
            //                               SectionLookupId = y.SectionLookupId,
            //                               CurrentStationProfileId = y.CurrentStationProfileId,
            //                               VisitDate = y.VisitDate,
            //                               CreatedOn = y.CreatedOn,
            //                               IsDischarge = y.IsDischarge,
            //                               IsActive = y.IsActive
            //                           })
            //                           .ToListAsync();

            foreach (var item in patientVisits)
            {
                item.PatientDiagnoseDiseases = DiseaseList.Where(x => x.PatientDiagnoseId == item.PatientDiagnoseId).ToList();

                foreach (var disease in item.PatientDiagnoseDiseases)
                {
                    if (string.IsNullOrEmpty(item.DiseasesName))
                        item.DiseasesName += disease.Name;
                    else
                        item.DiseasesName += ", " + disease.Name;
                }
            }

            return _mapper.Map<List<ViewPatientHistoryListDto>>(patientVisits);
        }

        public async Task<ViewPatientSlipDetailsDto> PatientVisitDetailById(Guid? visitId, Guid? diagnoseId)
        {
            var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientOpenVisit.GetDbContext());

            var _uowSectionLookUp = new UnitOfWork<SectionLookup>();

            var patientVisit = new ViewPatientSlipDetailsDto();

            if (!AppCommonMethod.IsNullOrEmptyGuid(diagnoseId))
            {

                patientVisit = await _uowPatientDiagnose.Repository.GetALL().Where(x => x.PatientVisitId == visitId)
                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(diagnoseId), x => x.PatientDiagnoseId == diagnoseId)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.SectionLookup)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.DepartementLookup)
                .Include(x => x.Patient).ThenInclude(x => x!.GenderProfile)
                .Include(x => x.Patient).ThenInclude(x => x!.BloodGroupProfile)
                .Include(x => x.PatientVisit).ThenInclude(x => x.HealthFacility)
                .Include(x => x.DiagnosedByNavigation).ThenInclude(x => x!.DesignationProfile)
                .Include(y => y.PatientDiagnoseDiseases)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
                //.Include(x => x.PatientVisit).ThenInclude(x => x!.ReasonIfNotEligibleForSscNavigation)

                //.Include(x => x.PatientPrescriptions).ThenInclude(x => x.Medicine)
                .Include(x => x.PatientPrescriptions)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseProfile)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseTimeProfile)
                .Include(x => x.PatientVisit).ThenInclude(x => x.PatientLabTests).ThenInclude(x => x.LabTest).ThenInclude(x => x.DepartmentProfile)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientVisit!.PatientOpenVisitId,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,
                    Doctor = y.DiagnosedByNavigation!.FullName,
                    DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
                    Section = y.DocSectionLookup!.Name,
                    Department = y.DocDepartmentLookup!.Name,
                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.CreatedOn,
                    IsWillingToBuyMedPrivately = y.PatientVisit.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.Patient.FollowupDate,
                    PresentComplaints = y.PresentComplaints,
                    Examination = y.Examination,
                    PatientMedicalHistory = y.PatientMedicalHistory,
                    AdviseGiven = y.AdviseGiven,
                    PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals.ToList()),
                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,
                    IsVitalSkip = y.PatientVisit!.IsVitalSkip,
                    IsEligibleForSsc = y.PatientVisit.IsEligibleForSsc,
                    SscNumber = y.PatientVisit.SscNumber,
                    IsSscClaimed = y.PatientVisit.IsSscClaimed,
                    ReasonIfSscNotClaimed = y.PatientVisit.ReasonIfSscNotClaimed,
                    SscClaimedDate = y.PatientVisit.SscClaimedDate,

                    //ReasonIfNotEligibleForSsc = y.PatientVisit!.ReasonIfNotEligibleForSscNavigation.Name,
                    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToList(),
                    PatientMedicine = y.PatientVisit.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientPrescriptionDto
                    {
                        MedicineId = x.MedicineId,
                        Days = x.Days,
                        DoseName = x.DoseProfile!.Name,
                        DoseTimeName = x.DoseTimeProfile!.Name,
                        MedicineName = x.MedicineName,
                        PatientPrescriptionId = x.PatientPrescriptionId,
                        Quantity = x.Quantity,
                        AvailableQuantity = x.AvailableQuantity,
                        MedicineDose = x.MedicineDose,
                        MedicineRoute = x.MedicineRoute,
                        MedicineFrequency = x.MedicineFrequency,
                        MedicineInstruction = x.MedicineInstruction,
                        MedicineDuration = x.MedicineDuration
                    }).ToList(),

                    PatientLabTests = y.PatientVisit.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
                    {
                        LabDepartmentName = x.LabDepartmentProfile!.Name,
                        LabTestName = x.LabTest!.Name,
                        PatientLabTestId = x.PatientLabTestId
                    }).ToList(),
                }).FirstOrDefaultAsync();

            }
            else
            {
                patientVisit = await _uowPatientOpenVisit.Repository.GetALL().Where(x => x.PatientOpenVisitId == visitId)
                    .Include(x => x.SectionLookup)
                    .Include(x => x.DepartementLookup)
                    //.Include(x => x.ReasonIfNotEligibleForSscNavigation)
                    .Include(x => x.Patient).ThenInclude(x => x.GenderProfile)
                    .Include(x => x.Patient).ThenInclude(x => x.BloodGroupProfile)
                    .Include(x => x.HealthFacility)
                    .Include(x => x.PatientDiagnoses).ThenInclude(x => x.DiagnosedByNavigation).ThenInclude(x => x!.DesignationProfile)
                    .Include(x => x.PatientDiagnoses).ThenInclude(y => y.PatientDiagnoseDiseases)
                    .Include(x => x.PatientVitals)
                    //.Include(x => x.PatientPrescriptions).ThenInclude(x => x.Medicine)
                    .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseProfile)
                    .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseTimeProfile)
                    .Include(x => x.PatientLabTests).ThenInclude(x => x.LabTest).ThenInclude(x => x.DepartmentProfile)
                    .Select(y => new ViewPatientSlipDetailsDto
                    {
                        PatientOpenVisitId = y.PatientOpenVisitId,
                        PatientId = y.PatientId ?? Guid.Empty,
                        Mrno = y.Patient!.Mrno,
                        Doctor = y.PatientDiagnoses!.FirstOrDefault()!.DiagnosedByNavigation!.FullName,
                        DoctorDesignation = y.PatientDiagnoses!.FirstOrDefault()!.DiagnosedByNavigation!.DesignationProfile!.Name,
                        Section = y.SectionLookup!.Name,
                        Department = y.DepartementLookup!.Name,
                        PatientName = y.Patient.FullName,
                        GurdianName = y.Patient.GuardianName,
                        Age = y.Patient.Age,
                        Dob = y.Patient.Dob,
                        Gender = y.Patient.GenderProfile!.Name,
                        CNIC = y.Patient.Cnic,
                        ContactNo = y.Patient.MobileNo,
                        Address = y.Patient.ParmanentAddress,
                        VisitDate = y.CreatedOn,
                        IsWillingToBuyMedPrivately = y.IsWillingToBuyMedPrivately,
                        NextVisitDate = y.Patient.FollowupDate,
                        PresentComplaints = y.PatientDiagnoses.FirstOrDefault()!.PresentComplaints,
                        Examination = y.PatientDiagnoses.FirstOrDefault()!.Examination,
                        PatientMedicalHistory = y.PatientDiagnoses.FirstOrDefault()!.PatientMedicalHistory,
                        AdviseGiven = y.PatientDiagnoses.FirstOrDefault()!.AdviseGiven,
                        PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVitals.ToList()),
                        HealthFacilityName = y.HealthFacility!.Name,
                        TokenNo = y.TokenNo,
                        IsVitalSkip = y.IsVitalSkip,
                        IsEligibleForSsc = y.IsEligibleForSsc,
                        SscNumber = y.SscNumber,
                        IsSscClaimed = y.IsSscClaimed,
                        ReasonIfSscNotClaimed = y.ReasonIfSscNotClaimed,
                        SscClaimedDate = y.SscClaimedDate,
                        //ReasonIfNotEligibleForSsc = y.ReasonIfNotEligibleForSscNavigation!.Name,
                        PatientDiagnosesDiseases = y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseDiseases.Select(x => new PatientDiagnosesDiseasesDto
                        {
                            PatientDiagnoseDiseaseId = x.PatientDiagnoseId.ToString(),
                            DiseasesName = x.DiseaseProfile.Name
                        }).ToList(),
                        PatientMedicine = y.PatientPrescriptions.Select(x => new PatientPrescriptionDto
                        {
                            MedicineId = x.MedicineId,
                            Days = x.Days,
                            DoseName = x.DoseProfile!.Name,
                            DoseTimeName = x.DoseTimeProfile!.Name,
                            MedicineName = x.MedicineName,
                            PatientPrescriptionId = x.PatientPrescriptionId,
                            Quantity = x.Quantity,
                            AvailableQuantity = x.AvailableQuantity,
                            MedicineDose = x.MedicineDose,
                            MedicineRoute = x.MedicineRoute,
                            MedicineFrequency = x.MedicineFrequency,
                            MedicineInstruction = x.MedicineInstruction,
                            MedicineDuration = x.MedicineDuration
                        }).ToList(),
                        PatientLabTests = y.PatientLabTests.Select(x => new PatientLabTestDto
                        {
                            LabDepartmentName = x.LabDepartmentProfile!.Name,
                            LabTestName = x.LabTest!.Name,
                            PatientLabTestId = x.PatientLabTestId
                        }).ToList(),
                    }).FirstOrDefaultAsync();

            }

            if (patientVisit != null)
            {
                foreach (var item in patientVisit!.PatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DiseasesName))
                        patientVisit.DiseasesName += item.DiseasesName;
                    else
                        patientVisit.DiseasesName += ", " + item.DiseasesName;
                }
            }

            return patientVisit;

        }

        public async Task<List<ViewPatientDiagnoseRecordDto>> GetPatientDiagnoseRecordByVisitId(Guid visitId, Guid? diagnoseId = null, string? visitType = CommonStringConstant.OPD)
        {
            var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatient.GetDbContext());

            var dbUser = TokenService.GetUserLoggedInfo();
            var DepartmentType = CommonStringConstant.OPD;

            if (dbUser!.DepartmentName == CommonStringConstant.InPatientDepartment)
                DepartmentType = CommonStringConstant.IPD;
            else if (dbUser!.DepartmentName == CommonStringConstant.ERDepartment)
                DepartmentType = CommonStringConstant.ER;


            var responseObj = await _uowPatientDiagnoseRecord.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && 
                    x.PatientVisitId == visitId)
                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(diagnoseId), x => x.PatientDiagnoseId == diagnoseId)
                .WhereIf(DepartmentType == CommonStringConstant.OPD && visitType == CommonStringConstant.ER,
                    x => x.PatientDiagnose.IsDischargeDiagnose == true
                )
                .Select(x => new ViewPatientDiagnoseRecordDto
                {
                    PatientDiagnosisRecordId = x.PatientDiagnosisRecordId,
                    PatientDiagnoseId = x.PatientDiagnoseId,
                    PatientVisitId = x.PatientVisitId,
                    PatientId = x.PatientId,
                    IsDischargeDiagnose = x.PatientDiagnose.IsDischargeDiagnose,
                    IsDiagnoseExternally = x.PatientDiagnose.IsDiagnoseExternally,
                    DoctorHealthFacility = x.PatientDiagnose.DiagnosedByNavigation!.HealthFacility!.Name,
                    DoctorDepartment = x.PatientDiagnose.DocDepartmentLookup!.Name,
                    DoctorSection = x.PatientDiagnose.DocSectionLookup!.Name,
                    DiagnosedBy = x.PatientDiagnose.IsDiagnoseExternally == true ? x.PatientDiagnose.SourceDoctorName : x.PatientDiagnose.DiagnosedByNavigation!.FullName,
                    DoctorDesignation = x.PatientDiagnose.DiagnosedByNavigation.DesignationProfile!.Name,
                    DiagnosedOn = x.PatientDiagnose.CreatedOn,
                    FormType = x.FormType,
                    //Json = JsonConvert.DeserializeObject(x.Json),
                    Json = x.Json,
                    IsPrinted = x.IsPrinted
                })
                .OrderBy(x => x.DiagnosedOn)
                .ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList(responseObj))
            {

                responseObj[0].MedicineDispatches = await _uowPatient.GetDbContext().MedicineDispatches.Where(x => x.PatientVisitId == visitId)
                    .Select(x => new ViewMedicineDispatchDto
                    {
                        PatientDiagnoseId = x.PatientDiagnoseId,
                        PatientPrescriptionId = x.PatientPrescriptionId,
                        MedicineId = x.MedicineId,
                        MedicineName = x.MedicineName,
                        QuantityDispatch = x.QuantityDispatch,
                        QuantityPrescribed = x.QuantityPrescribed
                    })
                    .ToListAsync();
            }

            //return _mapper.Map<List<ViewPatientDiagnoseRecordDto>>(responseObj);
            return responseObj;
        }

        public async Task<ViewPatientSlipDetailsDto> PatientVisitDetailsForVitalById(Guid input, bool IsFromGlobalSearch = false)
        {

            var isVisitOccupied = await _uowPatientOpenVisit.Repository.GetALL(x =>
            x.PatientOpenVisitId != input &&
            x.IsOccupied == true &&
            x.VisitDate == DateTime.Today &&
            x.OccupiedBy == _tokenService.GetUserId())
            .Include(x => x.Patient)
            .FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(isVisitOccupied))
            {
                var message = "Patient ";
                if (!AppCommonMethod.IsNullObject(isVisitOccupied!.Patient))
                    message += isVisitOccupied!.Patient!.FirstName;

                if (!string.IsNullOrEmpty(isVisitOccupied.TokenNo))
                    message += " with Token No " + isVisitOccupied.TokenNo;

                message += " is Occupied, kindly attend First";
                throw new UserFriendlyException(message);
                //ViewPatientSlipDetailsDto _result = await GetPatientVisitDetailsForDoctor(input);
                //_result.IsOccupied = true;
                //return _result;
            }

            var dbObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.CurrentStationProfile)
                .Include(x => x.OccupiedByNavigation)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Department)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Section)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

            // check if doctor attended patient then Vital Collector cannot attend Patient
            //if (!AppCommonMethod.IsNullOrEmptyGuid(dbObj.AttendedBy))
            //    throw new UserFriendlyException(CommonMessageConstant.PatientIsAtDoctorCannotAccess);


            //Station from Profiles
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

            var currentStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.VitalStation && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .OrderBy(x => x.SequenceNo)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.ProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.ShortName,
                Name = x.Name
            }).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(currentStation) || currentStation!.StationProfileId != dbObj!.CurrentStationProfileId)
            //{
            //    var message = "Patient is Curently at ";

            //    if (!AppCommonMethod.IsNullObject(dbObj!.CurrentStationProfile))
            //        message += dbObj!.CurrentStationProfile!.Name;
            //    else
            //        message += "Different Station ";

            //    message += ", You cannot Access it!";

            //    throw new UserFriendlyException(message);
            //}

            if (!dbObj!.IsFromPmis)
            {
                if (dbObj.IsOccupied == true && dbObj?.OccupiedBy != _tokenService.GetUserId())
                {
                    var message = "Patient is already Occupied by ";
                    message += "User ( " + dbObj!.OccupiedByNavigation!.FullName + " )";

                    if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Department))
                    {
                        message += " in ( " + dbObj!.OccupiedByNavigation!.Department!.Name;
                        if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Section))
                            message += " / " + dbObj!.OccupiedByNavigation!.Section!.Name;
                        message += " ) ";
                    }
                    throw new UserFriendlyException(message);
                }
                else
                {
                    //if (dbObj?.VitalCollectedBy != null && dbObj?.VitalCollectedBy != _tokenService.GetUserId())
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientCalledAnotherVital);
                    //else
                    dbObj!.VitalCollectedBy = _tokenService.GetUserId();
                }
            }
            else // if PMIS
                dbObj.VitalCollectedBy = dbObj.AttendedBy;


            dbObj.IsDischarge = false;
            dbObj.CurrentStationProfileId = currentStation!.StationProfileId;
            dbObj.IsOccupied = true;
            dbObj.OccupiedBy = _tokenService.GetUserId();
            dbObj.UpdatedBy = _tokenService.GetUserId();
            dbObj.UpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            //ViewPatientSlipDetailsDto result = await GetPatientVisitDetails(input);
            ViewPatientSlipDetailsDto result = await GetPatientVisitDetailsForDoctor(input);



            //var _uowPatientSourceInfo = new UnitOfWork<DbModel.PatientSourceInfo>(_uowPatient.GetDbContext());
            //var _dbObj = await _uowPatientSourceInfo.Repository.GetALL(x => x.PatientId == result.PatientId).FirstOrDefaultAsync();


            //if (!AppCommonMethod.IsNullObject(_dbObj))
            //{
            //    result.PatientCategory = CommonStringConstant.DrugAddict;
            //}
            //throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            return result;
        }


        public async Task<ViewPatientSlipDetailsDto> GetPatientVisitForDoctorById(Guid input, bool? IsFromPMIS, bool? isPatientHistory)
        {
            var userInfo = TokenService.GetUserLoggedInfo();

            // *** Check if doctor already have/occupied another patient ***//
            var isVisitOccupied = await _uowPatientOpenVisit.Repository.GetALL(x =>
            x.PatientOpenVisitId != input &&
            x.IsOccupied == true &&
            x.VisitDate == DateTime.Today &&
            x.OccupiedBy == _tokenService.GetUserId())
            .Include(x => x.Patient)
            .FirstOrDefaultAsync();

            if ((bool)!isPatientHistory)
            {
                if (!AppCommonMethod.IsNullObject(isVisitOccupied))
                {
                    var message = "Patient ";
                    if (!AppCommonMethod.IsNullObject(isVisitOccupied!.Patient))
                        message += isVisitOccupied!.Patient!.FirstName;

                    if (!string.IsNullOrEmpty(isVisitOccupied.TokenNo))
                        message += " with Token No " + isVisitOccupied.TokenNo;

                    message += " is Occupied, kindly attend First";
                    throw new UserFriendlyException(message);
                }
            }


            // Need to Refine 
            var dbObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.OccupiedByNavigation)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Department)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Section)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

            var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
            var sectionObj = await _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == dbObj.SectionLookupId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(sectionObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            if (TokenService.GetUserLoggedInfo().SectionName == CommonStringConstant.DentalOPD)
            {
                var _uowFeePayment = new UnitOfWork<FeePayment>(_uowPatient.GetDbContext());
                var data = await _uowFeePayment.Repository.GetALL(x => x.PatientVisitId == input && x.SectionLookupId == TokenService.GetUserLoggedInfo().SectionId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

                if (AppCommonMethod.IsNullObject(data))
                {
                    dbObj.IsOccupied = false;
                    _uowPatientOpenVisit.Repository.Update(dbObj);
                    _uowPatientOpenVisit.Save();
                    throw new UserFriendlyException(CommonMessageConstant.PatientNotPaidDentalFee);
                }

                if (AppCommonMethod.IsNullBool(data!.IsPaid) || AppCommonMethod.IsNullorZeroInt(data!.PaidAmount))
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientNotPaidDentalFee);
                }

                if (!AppCommonMethod.IsNullBool(data!.IsRefund))
                {
                    throw new UserFriendlyException(CommonMessageConstant.FeeRefundAgainPayFee);
                }
            }


            #region TB
            if (dbObj.DepartementLookupId == CommonStringConstant.TbDepartmentId && dbObj.SectionLookupId == CommonStringConstant.TbSectionId)
            {
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(data))
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientVisitClosedAlready);
                }
            }
            #endregion

            //if()


            if (!dbObj!.IsFromPmis)
            {

                // Check If Patient is Already Occupied by some other user
                if (dbObj.IsOccupied == true && dbObj?.OccupiedBy != _tokenService.GetUserId())
                {
                    var message = "Patient is already Occupied by ";
                    message += "User ( " + dbObj!.OccupiedByNavigation!.FullName + " )";

                    if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Department))
                    {
                        message += " in ( " + dbObj!.OccupiedByNavigation!.Department!.Name;
                        if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Section))
                            message += " / " + dbObj!.OccupiedByNavigation!.Section!.Name;
                        message += " ) ";
                    }

                    throw new UserFriendlyException(message);
                }
                else
                {
                    //if (dbObj?.AttendedBy != null && dbObj?.AttendedBy != _tokenService.GetUserId())
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientCalledAnotherDoctor);
                    //else
                    dbObj!.AttendedBy = _tokenService.GetUserId();
                }
            }
            //else // if PMIS
            //dbObj.AttendedBy = dbObj.AttendedBy; // Attended By already filled When Visit is generated

            // Start - This Block of code must be before save Open Visit because we have to pick Current Department & Section

            ViewPatientSlipDetailsDto result = new ViewPatientSlipDetailsDto();
            if (!AppCommonMethod.IsNullOrEmptyList<PatientDiagnose>(dbObj!.PatientDiagnoses.ToList()))
            {
                var isExistObj = dbObj!.PatientDiagnoses!.Where(x => x.DiagnosedBy == _tokenService.GetUserId() && x.FormType == CommonStringConstant.GeneralForm).SingleOrDefault();

                //var obj = await _patientDiagnoseService.GetByIdWithPrescription(isExistObj!.PatientDiagnoseId);
                if (!AppCommonMethod.IsNullObject(isExistObj))
                    result = await GetPatientDiagnoseById(isExistObj!.PatientDiagnoseId);
                else
                    result = await GetPatientVisitDetailsForDoctor(input);

            }
            else
            {
                if (userInfo.IsConsultant == true || userInfo.SectionName == CommonStringConstant.DentalSurgeonOPD)
                    result = await GetPatientVisitDetails(input);
                else
                    result = await GetPatientVisitDetailsForDoctor(input);
            }




            if (userInfo.FormType == CommonStringConstant.NCDClinicForm || userInfo.FormType == CommonStringConstant.MuawinClinicsForm)
            {
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var PatientDiagnoseId = await _uowPatientDiagnose.Repository
                    .GetALL(x => x.PatientId == dbObj.PatientId && x.PatientVisitId != input && x.DocSectionLookupId == userInfo.SectionId)
                    .OrderByDescending(x => x.CreatedOn)
                    .Select(x => x.PatientDiagnoseId)
                    .FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullOrEmptyGuid(PatientDiagnoseId))
                {
                    var _uowPatientDiagnoseDiseases = new UnitOfWork<PatientDiagnoseDisease>(_uowPatient.GetDbContext());
                    result!.PatientDiagnosesDiseases = await _uowPatientDiagnoseDiseases.Repository
                       .GetALL(x => x.PatientDiagnoseId == PatientDiagnoseId)
                       .Select(x => new PatientDiagnosesDiseasesDto
                       {
                           PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                           DiseasesName = x.DiseaseProfile.Name,
                           DiseaseProfileId = x.DiseaseProfileId
                       })
                       .ToListAsync();
                }

            }
            // End - This Block of code must be before save Open Visit because we have to pick Current Department & Section

            // It should be save in Redis and Get from there 
            var currentStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .OrderBy(x => x.SequenceNo)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.ProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.ShortName,
                Name = x.Name
            }).FirstOrDefaultAsync();

            dbObj.IsDischarge = false;
            dbObj.CurrentStationProfileId = currentStation!.StationProfileId;

            if (userInfo.FormType != CommonStringConstant.NCDClinicForm && userInfo.FormType != CommonStringConstant.MuawinClinicsForm)
            {
                dbObj.DepartementLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.DepartmentId) ? dbObj.DepartementLookupId : userInfo!.DepartmentId;
                dbObj.SectionLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.SectionId) ? dbObj.SectionLookupId : userInfo!.SectionId;
            }
            dbObj.IsOccupied = true;
            dbObj.OccupiedBy = _tokenService.GetUserId();

            dbObj.UpdatedBy = _tokenService.GetUserId();
            dbObj.UpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            //ViewPatientSlipDetailsDto result = await GetPatientVisitDetails(input);


            #region Get All Medicine Against Visit, Check if Already Prescribed by Other Doctor

            result.PatientPrescriptionVisitLists = await _uowPatient.GetDbContext().ViewGetPatientPrescriptionLists
                .Where(x => x.PatientVisitId == input)
                .ToListAsync();

            #endregion

            #region Get All Lab Test Against Visit, Check if Already Prescribed by Other Doctor

            result.PatientLabTestVisitLists = await _uowPatient.GetDbContext().ViewGetPatientLabTestLists
                .Where(x => x.PatientVisitId == input)
                .ToListAsync();

            #endregion


            // *********** Araiz ********* //

            #region This is Only For TB Patients


            if (sectionObj.FormType == CommonStringConstant.TbForm)
            {


                var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                var tbPatData = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsReferToDrtb == true).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
                var userObj = await GetLoggedInfoUserDTO();

                if (!AppCommonMethod.IsNullObject(tbPatData) && !(userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0) && (bool)!isPatientHistory)
                {
                    result.IsReferToDRTB = true;
                    return result;
                }

                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var patientDiagnose = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsConfirmed == true)
                    .Select(x => new ViewPatientSlipDetailsDto { IsConfirmed = x.IsConfirmed, TbStatusConfirmedDate = x.CreatedOn }).ToListAsync();

                //if (!AppCommonMethod.IsNullOrEmptyList(patientDiagnose))
                //{
                //    result.IsConfirmed = true;
                //    result.TbStatusConfirmedDate = patientDiagnose.Min(x => x.TbStatusConfirmedDate);
                //}




                var _uowPatientVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientOpenVisit.GetDbContext());
                var patientVisitFlowObj = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                var _uowTbPatientDetail = new UnitOfWork<TbPatientDetail>(_uowPatientOpenVisit.GetDbContext());

                if (!AppCommonMethod.IsNullObject(patientVisitFlowObj))
                {
                    var patientTreatmentStartDate = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo).OrderBy(x => x.FollowUpNo).Select(x => x.CreatedOn).FirstOrDefaultAsync();

                    result.IsFollowUp = patientVisitFlowObj.IsFollowUp;
                    result.FollowUpNo = patientVisitFlowObj.FollowUpNo;

                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatient.GetDbContext());

                    // last Visit Lab Test
                    var labTests = await _uowPatientLabTest.GetDbContext().ViewPatientLabTestListsForTbs.Where(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId)
                   .ToListAsync();


                    var patVisit = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(patVisit))
                    {
                        if (AppCommonMethod.IsNullObject(result.FollowUpNo))
                        {
                            result.IsConfirmed = false;
                        }
                        else
                        {
                            var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == patVisit.LastVisitId).FirstOrDefaultAsync();
                            if (!AppCommonMethod.IsNullObject(data))
                            {
                                if ((bool)data?.IsConfirmed)
                                {
                                    result.IsConfirmed = true;
                                }
                                else
                                {
                                    result.IsConfirmed = false;
                                }
                            }
                            else
                            {
                                result.IsConfirmed = false;
                            }
                        }


                        if (userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0)
                        {
                            result.IsConfirmed = true;
                        }



                        var tbPatObj = await _uowTbPatientDetail.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo)
                            .OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();

                        result.noOfMonthsPassed = !AppCommonMethod.IsNullObject(tbPatObj) ? tbPatObj?.NoOfMonthsMedicineIssued : 0;




                        var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatient.GetDbContext());
                        var medicinePrescribed = await _uowPatientPrescription.Repository.GetALL(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId).FirstOrDefaultAsync();

                        if (!AppCommonMethod.IsNullObject(medicinePrescribed))
                        {
                            result.IsMedicinePrescribedInLastVisit = true;
                            if (result.FollowUpNo > 0)
                            {
                                result.IsConfirmed = true;
                            }
                        }




                        if (!AppCommonMethod.IsNullOrEmptyList(labTests))
                        {
                            var _uowLabTest = new UnitOfWork<LabTest>(_uowPatient.GetDbContext());

                            foreach (var labTest in labTests)
                            {

                                if (!AppCommonMethod.IsNullObject(labTest))
                                {
                                    if (labTest.LabTestName == CommonStringConstant.CXR && labTest.IsReportGenerated != true)
                                    {
                                        result.IsCXRTestAdvisedInLastVisit = true;
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result.noOfMonthsPassed = 0;
                    }
                }
            }


            #endregion



            if (sectionObj.FormType == CommonStringConstant.NCDClinicForm)
            {
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var patLastVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId != dbObj.ParentPatientOpenVisitId && x.PatientId == dbObj.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(patLastVisit))
                    result.IsFollowUp = false;
                else if (AppCommonMethod.IsNullorEmptyDate(patLastVisit.FollowupDate))
                    result.IsFollowUp = false;
                else
                    result.IsFollowUp = true;
            }


            return result;
            //return null;
        }

        // Get Mlc Patient Visit
        public async Task<ViewPatientSlipDetailsDto> GetPatientVisitForMlcDoctorById(Guid input, bool? IsFromPMIS, bool? isPatientHistory, string? FormTypeMlc)
        {
            var userInfo = TokenService.GetUserLoggedInfo();

            // *** Check if doctor already have/occupied another patient ***//
            var isVisitOccupied = await _uowPatientOpenVisit.Repository.GetALL(x =>
            x.PatientOpenVisitId != input &&
            x.IsOccupied == true &&
            x.VisitDate == DateTime.Today &&
            x.OccupiedBy == _tokenService.GetUserId())
            .Include(x => x.Patient)
            .FirstOrDefaultAsync();

            if ((bool)!isPatientHistory)
            {
                if (!AppCommonMethod.IsNullObject(isVisitOccupied))
                {
                    var message = "Patient ";
                    if (!AppCommonMethod.IsNullObject(isVisitOccupied!.Patient))
                        message += isVisitOccupied!.Patient!.FirstName;

                    if (!string.IsNullOrEmpty(isVisitOccupied.TokenNo))
                        message += " with Token No " + isVisitOccupied.TokenNo;

                    message += " is Occupied, kindly attend First";
                    throw new UserFriendlyException(message);
                }
            }


            // Need to Refine 
            var dbObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.OccupiedByNavigation)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Department)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Section)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);


            #region TB
            if (dbObj.DepartementLookupId == CommonStringConstant.TbDepartmentId && dbObj.SectionLookupId == CommonStringConstant.TbSectionId)
            {
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(data))
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientVisitClosedAlready);
                }
            }
            #endregion

            //if()


            if (!dbObj!.IsFromPmis)
            {

                // Check If Patient is Already Occupied by some other user
                if (dbObj.IsOccupied == true && dbObj?.OccupiedBy != _tokenService.GetUserId())
                {
                    var message = "Patient is already Occupied by ";
                    message += "User ( " + dbObj!.OccupiedByNavigation!.FullName + " )";

                    if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Department))
                    {
                        message += " in ( " + dbObj!.OccupiedByNavigation!.Department!.Name;
                        if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Section))
                            message += " / " + dbObj!.OccupiedByNavigation!.Section!.Name;
                        message += " ) ";
                    }

                    throw new UserFriendlyException(message);
                }
                else
                {
                    //if (dbObj?.AttendedBy != null && dbObj?.AttendedBy != _tokenService.GetUserId())
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientCalledAnotherDoctor);
                    //else
                    dbObj!.AttendedBy = _tokenService.GetUserId();
                }
            }
            //else // if PMIS
            //dbObj.AttendedBy = dbObj.AttendedBy; // Attended By already filled When Visit is generated

            // Start - This Block of code must be before save Open Visit because we have to pick Current Department & Section

            ViewPatientSlipDetailsDto result = new ViewPatientSlipDetailsDto();
            if (!AppCommonMethod.IsNullOrEmptyList<PatientDiagnose>(dbObj!.PatientDiagnoses.ToList()))
            {
                var isExistObj = dbObj!.PatientDiagnoses!.Where(x => x.DiagnosedBy == _tokenService.GetUserId() && x.FormType == CommonStringConstant.GeneralForm).SingleOrDefault();

                //var obj = await _patientDiagnoseService.GetByIdWithPrescription(isExistObj!.PatientDiagnoseId);
                if (!AppCommonMethod.IsNullObject(isExistObj))
                    result = await GetPatientDiagnoseById(isExistObj!.PatientDiagnoseId);
                else
                    result = await GetPatientVisitDetailsForDoctor(input);
            }
            else
            {
                if (userInfo.IsConsultant == true || userInfo.SectionName == CommonStringConstant.DentalSurgeonOPD)
                    result = await GetPatientVisitDetails(input);
                else
                    result = await GetPatientVisitDetailsForDoctor(input);
            }
            // End - This Block of code must be before save Open Visit because we have to pick Current Department & Section

            // It should be save in Redis and Get from there 
            var currentStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .OrderBy(x => x.SequenceNo)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.ProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.ShortName,
                Name = x.Name
            }).FirstOrDefaultAsync();

            dbObj.IsDischarge = false;
            dbObj.CurrentStationProfileId = currentStation!.StationProfileId;

            if (!String.IsNullOrEmpty(FormTypeMlc))
            {
                if (FormTypeMlc != CommonStringConstant.EMCMLEFORM)
                {
                    dbObj.DepartementLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.DepartmentId) ? dbObj.DepartementLookupId : userInfo!.DepartmentId;
                    dbObj.SectionLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.SectionId) ? dbObj.SectionLookupId : userInfo!.SectionId;
                }
            }
            dbObj.IsOccupied = true;
            dbObj.OccupiedBy = _tokenService.GetUserId();

            dbObj.UpdatedBy = _tokenService.GetUserId();
            dbObj.UpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            //ViewPatientSlipDetailsDto result = await GetPatientVisitDetails(input);


            #region Get All Medicine Against Visit, Check if Already Prescribed by Other Doctor

            result.PatientPrescriptionVisitLists = await _uowPatient.GetDbContext().ViewGetPatientPrescriptionLists
                .Where(x => x.PatientVisitId == input)
                .ToListAsync();

            #endregion

            #region Get All Lab Test Against Visit, Check if Already Prescribed by Other Doctor

            result.PatientLabTestVisitLists = await _uowPatient.GetDbContext().ViewGetPatientLabTestLists
                .Where(x => x.PatientVisitId == input)
                .ToListAsync();

            #endregion


            // *********** Araiz ********* //

            #region This is Only For TB Patients
            var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
            var sectionObj = await _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == dbObj.SectionLookupId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(sectionObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            if (sectionObj.FormType == CommonStringConstant.TbForm)
            {


                var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                var tbPatData = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsReferToDrtb == true).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
                var userObj = await GetLoggedInfoUserDTO();

                if (!AppCommonMethod.IsNullObject(tbPatData) && !(userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0) && (bool)!isPatientHistory)
                {
                    result.IsReferToDRTB = true;
                    return result;
                }

                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var patientDiagnose = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsConfirmed == true)
                    .Select(x => new ViewPatientSlipDetailsDto { IsConfirmed = x.IsConfirmed, TbStatusConfirmedDate = x.CreatedOn }).ToListAsync();

                //if (!AppCommonMethod.IsNullOrEmptyList(patientDiagnose))
                //{
                //    result.IsConfirmed = true;
                //    result.TbStatusConfirmedDate = patientDiagnose.Min(x => x.TbStatusConfirmedDate);
                //}




                var _uowPatientVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientOpenVisit.GetDbContext());
                var patientVisitFlowObj = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                var _uowTbPatientDetail = new UnitOfWork<TbPatientDetail>(_uowPatientOpenVisit.GetDbContext());

                if (!AppCommonMethod.IsNullObject(patientVisitFlowObj))
                {
                    var patientTreatmentStartDate = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo).OrderBy(x => x.FollowUpNo).Select(x => x.CreatedOn).FirstOrDefaultAsync();

                    result.IsFollowUp = patientVisitFlowObj.IsFollowUp;
                    result.FollowUpNo = patientVisitFlowObj.FollowUpNo;

                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatient.GetDbContext());

                    // last Visit Lab Test
                    var labTests = await _uowPatientLabTest.GetDbContext().ViewPatientLabTestListsForTbs.Where(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId)
                   .ToListAsync();


                    var patVisit = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(patVisit))
                    {
                        if (AppCommonMethod.IsNullObject(result.FollowUpNo))
                        {
                            result.IsConfirmed = false;
                        }
                        else
                        {
                            var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == patVisit.LastVisitId).FirstOrDefaultAsync();
                            if (!AppCommonMethod.IsNullObject(data))
                            {
                                if ((bool)data?.IsConfirmed)
                                {
                                    result.IsConfirmed = true;
                                }
                                else
                                {
                                    result.IsConfirmed = false;
                                }
                            }
                            else
                            {
                                result.IsConfirmed = false;
                            }
                        }


                        if (userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0)
                        {
                            result.IsConfirmed = true;
                        }



                        var tbPatObj = await _uowTbPatientDetail.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo)
                            .OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();

                        result.noOfMonthsPassed = !AppCommonMethod.IsNullObject(tbPatObj) ? tbPatObj?.NoOfMonthsMedicineIssued : 0;




                        var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatient.GetDbContext());
                        var medicinePrescribed = await _uowPatientPrescription.Repository.GetALL(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId).FirstOrDefaultAsync();

                        if (!AppCommonMethod.IsNullObject(medicinePrescribed))
                        {
                            result.IsMedicinePrescribedInLastVisit = true;
                            if (result.FollowUpNo > 0)
                            {
                                result.IsConfirmed = true;
                            }
                        }




                        if (!AppCommonMethod.IsNullOrEmptyList(labTests))
                        {
                            var _uowLabTest = new UnitOfWork<LabTest>(_uowPatient.GetDbContext());

                            foreach (var labTest in labTests)
                            {

                                if (!AppCommonMethod.IsNullObject(labTest))
                                {
                                    if (labTest.LabTestName == CommonStringConstant.CXR && labTest.IsReportGenerated != true)
                                    {
                                        result.IsCXRTestAdvisedInLastVisit = true;
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result.noOfMonthsPassed = 0;
                    }
                }
            }



            #endregion

            return result;
            //return null;
        }


        public async Task<List<ViewPatientDiagnoseRecordDto>> PatientVisitDetailsForPharmacyById(Guid input, string VisitType)
        {

            var isVisitOccupied = await _uowPatientOpenVisit.Repository.GetALL(x =>
            x.PatientOpenVisitId != input &&
            x.IsOccupied == true &&
            x.VisitDate == DateTime.Today &&
            x.OccupiedBy == _tokenService.GetUserId())
            .Include(x => x.Patient)
            .FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(isVisitOccupied))
            {
                var message = "Patient ";
                if (!AppCommonMethod.IsNullObject(isVisitOccupied!.Patient))
                    message += isVisitOccupied!.Patient!.FirstName;

                if (!string.IsNullOrEmpty(isVisitOccupied.TokenNo))
                    message += " with Token No " + isVisitOccupied.TokenNo;

                message += " is Occupied, kindly attend First";
                throw new UserFriendlyException(message);
            }

            var dbObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.CurrentStationProfile)
                .Include(x => x.OccupiedByNavigation)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Department)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Section)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

            var currentStation = await _uowProfile.Repository.GetALL(x => (x.ShortName == CommonStringConstant.PharmacyStation) && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .OrderBy(x => x.SequenceNo)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.ProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.ShortName,
                Name = x.Name
            }).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(currentStation) || currentStation!.StationProfileId != dbObj!.CurrentStationProfileId)
            //{
            //    var message = "Patient is Currently at ";

            //    if (!AppCommonMethod.IsNullObject(dbObj!.CurrentStationProfile))
            //        message += dbObj!.CurrentStationProfile!.Name;
            //    else
            //        message += "Different Station ";

            //    message += ", You cannot Access it!";

            //    throw new UserFriendlyException(message);
            //}

            if (!dbObj!.IsFromPmis)
            {

                if (dbObj.IsOccupied == true && dbObj?.OccupiedBy != _tokenService.GetUserId())
                {
                    var message = "Patient is already Occupied by ";
                    message += "User ( " + dbObj!.OccupiedByNavigation!.FullName + " )";

                    if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Department))
                    {
                        message += " in ( " + dbObj!.OccupiedByNavigation!.Department!.Name;
                        if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Section))
                            message += " / " + dbObj!.OccupiedByNavigation!.Section!.Name;
                        message += " ) ";
                    }
                    throw new UserFriendlyException(message);
                }
                else
                {
                    //if (dbObj?.PharmacyAttendedBy != null && dbObj?.PharmacyAttendedBy != _tokenService.GetUserId())
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientCalledAnotherVital);
                    //else
                    dbObj!.PharmacyAttendedBy = _tokenService.GetUserId();
                }
            }
            else // if PMIS
                dbObj.PharmacyAttendedBy = dbObj.AttendedBy;

            if(dbObj.VisitFor == CommonStringConstant.OPD)
                dbObj.IsDischarge = false;

            dbObj.CurrentStationProfileId = currentStation!.StationProfileId;
            dbObj.IsOccupied = true;
            dbObj.OccupiedBy = _tokenService.GetUserId();

            dbObj.UpdatedBy = _tokenService.GetUserId();
            dbObj.UpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            //ViewPatientSlipDetailsDto result = await GetPatientVisitDetails(input);

            List<ViewPatientDiagnoseRecordDto> result = await GetPatientDiagnoseRecordByVisitId(input, null, VisitType);

            return result;
        }


        public async Task<ViewPagerDto<ViewFeePayment>> GetFeePaymments(FilterSpecialityFeePaymentDto filter)
        {
            IQueryable<ViewFeePayment> IQueryableList = _uowPatient.GetDbContext().ViewFeePayments.Where(x => x.HealthFacilityId == filter.HealthfacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .OrderByDescending(x => x.CreatedOn);

            var pagedList = await PagedListDto<ViewFeePayment>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewFeePayment>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }


        public async Task<SpecialityFeeDTO> GetAlmonerSpecialityFeeStat(FilterPatientDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPAnmonalSpecialityFeeCounts]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure; ;
                    var userId = _tokenService.GetUserId();


                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                    //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                    sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : userId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", 1);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SpecialityFeeDTO> lst = ds.Tables[0].ToList<SpecialityFeeDTO>();
                    if (lst.Count > 0)
                        return lst[0];
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }


                return null;
            }
        }

        public async Task<ViewPagerDto<SpecilaityListDTOForDashboard>> GetAnmonalSpecilityFeeListWithPagination(FilterDentalListPatientDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var responseObject = new ViewPagerDto<SpecilaityListDTOForDashboard>();
            List<SpecilaityListDTOForDashboard> lst = new List<SpecilaityListDTOForDashboard>();
            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPAnmonalSpecialityFeeDashboardList]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure; ;
                    var userId = _tokenService.GetUserId();


                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                    //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                    sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : userId);
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", 1);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                        sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                        sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    var count = ds.Tables[0].ToList<ListTotalCount>();
                    lst = ds.Tables[1].ToList<SpecilaityListDTOForDashboard>();


                    responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                    responseObject.PageSize = filter.PageSize;
                    responseObject.CurrentPage = filter.PageNumber;
                    responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                    responseObject.HasPrevious = filter.PageNumber > 1;
                    responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                    responseObject.List = lst;

                    return responseObject;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<DentalProcedureStatDTO> GetAlmonerDentalProcedureStat(FilterPatientDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPAlmonerDentalProcedureStat]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure; ;
                    var userId = _tokenService.GetUserId();


                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                    //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                    sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : userId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", 1);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<DentalProcedureStatDTO> lst = ds.Tables[0].ToList<DentalProcedureStatDTO>();
                    if (lst.Count > 0)
                        return lst[0];
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }


                return null;
            }
        }

        public async Task<ViewPagerDto<DentalPatientProcedureListDTOForDashboard>> GetAlmonerDentalProcedureListWithPagination(FilterDentalListPatientDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var responseObject = new ViewPagerDto<DentalPatientProcedureListDTOForDashboard>();
            List<DentalPatientProcedureListDTOForDashboard> lst = new List<DentalPatientProcedureListDTOForDashboard>();
            //var startDate = "";
            //var endDate = "";

            //// Parse the input string using the specified format
            //if (filter.StartDate != null)
            //    startDate = DateTime.ParseExact(filter.StartDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else startDate = DateTime.Now.ToString();
            //if (filter.EndDate != null)
            //    endDate = DateTime.ParseExact(filter.EndDate!.ToString(), "dd/MM/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd HH:mm:ss.fff");
            //else endDate = DateTime.Now.ToString();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[SPAlmonerDentalProcedureList]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure; ;
                    var userId = _tokenService.GetUserId();


                    sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                    sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                    //sqlComm.Parameters.AddWithValue("@StartDate", startDate);
                    //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                    sqlComm.Parameters.AddWithValue("@UserId", filter.IsOverAllDashboard ? null : userId);

                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListTypeForDashboard);
                    if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        sqlComm.Parameters.AddWithValue("@ProvinceId", 1);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                        sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                        sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);


                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    var count = ds.Tables[0].ToList<ListTotalCount>();
                    lst = ds.Tables[1].ToList<DentalPatientProcedureListDTOForDashboard>();


                    responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                    responseObject.PageSize = filter.PageSize;
                    responseObject.CurrentPage = filter.PageNumber;
                    responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                    responseObject.HasPrevious = filter.PageNumber > 1;
                    responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                    responseObject.List = lst;

                    return responseObject;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }


                return null;
            }
        }


        #endregion

        #region Helper Methods

        public dynamic GetResponseModelDto(DbModel.Patient input, string SourceSystemShortName)
        {
            if (SourceSystemShortName == CommonStringConstant.SourceSystemEMR)
            {
                EmrPatientRegistrationResponseDto response = new EmrPatientRegistrationResponseDto();


                response = _mapper.Map(input, response);

                response.visit.Id = input.PatientOpenVisits.Select(x => x.PatientOpenVisitId).FirstOrDefault();
                response.visit.IsVisitClosed = false;
                response.visit.DateTimeVisitStart = input.PatientOpenVisits.Select(x => x.VisitDate).FirstOrDefault();
                response.visit.HealthFacility_Id = input.PatientOpenVisits.Select(x => x.HealthFacilityId).FirstOrDefault();
                response.visit.Token = input.PatientOpenVisits.Select(x => x.TokenNo).FirstOrDefault();
                response.visit.Patient_Id = input.PatientOpenVisits.Select(x => x.PatientId).FirstOrDefault();

                return response;
            }
            else
            {
                return input;
            }

        }


        public async Task<ViewPatientVisitsListWithDetailDto> CheckIfPatientVisitExist(Guid? PatientId, int? HealthFacilityId, string? VisitForDepartment = CommonStringConstant.OPD)
        {
            var user = TokenService.GetUserLoggedInfo();

            if (user.SectionName == CommonStringConstant.OneWindowTb)
                return null;

            //if (user.SectionName == CommonStringConstant.NCDClinicOPD)
            //    return null;

            var userRole = user.UserRoleList.Where(x => x.ShortName == CommonStringConstant.PMIS).FirstOrDefault();
            if (!AppCommonMethod.IsNullObject(userRole))
                return null;

            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());
            //var VisitFor = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == PatientId && x.CreatedOn.D == DateTime.Now.Date).OrderByDescending(x => x.CreatedOn).Select(x => x.DepartementLookup.Name).FirstOrDefaultAsync();

            var response = await _uowPatientOpenVisit.Repository.GetALL()
                .Where(x => x.PatientId == PatientId &&
                    x.HealthFacilityId == HealthFacilityId
                )
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(response))
            {
                var VisitFor = "";

                // if IPD then check if it is not already Registered in IPD or ER
                if (VisitForDepartment == CommonStringConstant.IPD)
                {
                    if(response!.IsDischarge != true)
                    {
                        if (response!.VisitFor == CommonStringConstant.IPD)
                            throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                        if (response!.VisitFor == CommonStringConstant.ER)
                            throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
                    }
                    

                    //var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isIPDVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                    //var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isERVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
                }
                // if ER then check if it is not already Registered in IPD or ER
                if (VisitForDepartment == CommonStringConstant.ER)
                {
                    if (response!.IsDischarge != true)
                    {
                        if (response!.VisitFor == CommonStringConstant.IPD)
                            throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                        if (response!.VisitFor == CommonStringConstant.ER && response.CreatedOn > DateTime.Now.AddHours(-24))
                            throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
                    }
                    //var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isIPDVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                    //var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isERVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

                }
                // if OPD then check if it is not already Registered in IPD or ER or OPD
                if (VisitForDepartment == CommonStringConstant.OPD)
                {
                    if (response!.IsDischarge != true)
                    {
                        if (response!.VisitFor == CommonStringConstant.IPD)
                            throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                        if (response!.VisitFor == CommonStringConstant.ER && response.CreatedOn > DateTime.Now.AddHours(-24))
                            throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
                    }
                    if (response!.VisitFor == CommonStringConstant.OPD && response!.CreatedOn!.Value.Date == DateTime.Now.Date)
                    {
                        var _data = await _uowPatientOpenVisit.GetDbContext().ViewPatientOpenVisiDashbaordLists.Where(x => x.PatientVisitId == response.PatientOpenVisitId).Select(x =>
                        new ViewPatientVisitsListWithDetailDto
                        {
                            PatientVisitId = x.PatientVisitId,
                            PatientId = x.PatientId,
                            FullName = x.PatientName,
                            MobileNo = x.PatientMobileNo,
                            PatientProvinceId = x.PatientProvinceId,
                            Mrno = x.MrNo,
                            Cnic = x.Cnic,
                            VisitDate = x.VisitDate,
                            CreatedBy = x.CreatedBy!,
                            CreatedOn = x.CreatedOn,
                            UpdatedBy = x.UpdatedBy!,
                            UpdatedOn = x.UpdatedOn,
                            DepartmentId = x.DepartementLookupId,
                            Department = x.Department,
                            Section = x.Section,
                            SectionId = x.SectionLookupId,
                            //PatientDiagnoseId = x.PatientDiagnoseId,
                            //FormType = x.FormType,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Gender = x.Gender,
                            Age = x.Age,
                            Dob = x.Dob,
                            IsDischarge = x.IsDischarge,
                            IsVitalSkip = x.IsVitalSkip,
                            IsSscClaimed = x.IsSscClaimed,
                            SscNotConfirmReason = x.SscNotConfirmReason,
                            SscStatus = x.SscStatus,
                            SscStatusName = x.SscStatusName,
                            SscClaimedDate = x.SscClaimedDate,
                            SscStatusReason = x.SscStatusReason,
                            SscStatusUpdatedOn = x.SscStatusUpdatedOn,
                            SscStatusUpdatedBy = x.SscStatusUpdatedBy,
                            SscNumber = x.SscNumber,
                            IsEligibleForSsc = x.IsEligibleForSsc,
                            SscNotEligibleReason = x.SscNotEligibleReason,
                            HealthFacilityName = x.HealthFacilityName,
                            HealthFacilityId = x.HealthFacilityId,
                            TokenNo = x.TokenNo,
                            IsReferred = x.IsReferred,
                            ReferredByDepartmentLookupId = x.ReferredByDepartmentLookupId,
                            ReferredBySectionLookupId = x.ReferredBySectionLookupId

                        }).FirstOrDefaultAsync();
                        return _data;
                    }
                    //var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isIPDVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                    //var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isERVisit))
                    //    throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

                    //var isOPDVisit = response.Where(x => x.VisitDate == DateTime.Now.Date).FirstOrDefault();
                    //if (!AppCommonMethod.IsNullObject(isOPDVisit))
                    //{
                }

                //var data = response.Where(x => x.VisitDate == DateTime.Now.Date && x.DepartementLookupId == CommonConstant.OPD).OrderByDescending(x => x.CreatedOn).FirstOrDefault();

                //if (AppCommonMethod.IsNullObject(data))
                //    return null;

                //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatient.GetDbContext());
                //VisitFor = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == data.DepartementLookupId).Select(x => x.DisplayName).FirstOrDefaultAsync();

                //// if IPD then check if it is not already Registered in IPD or ER
                //if (VisitFor == CommonStringConstant.IPD)
                //{
                //    var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                //    if (!AppCommonMethod.IsNullObject(isIPDVisit))
                //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                //    var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                //    if (!AppCommonMethod.IsNullObject(isERVisit))
                //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);
                //}
                //// if ER then check if it is not already Registered in IPD or ER
                //if (VisitFor == CommonStringConstant.ER)
                //{
                //    var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                //    if (!AppCommonMethod.IsNullObject(isIPDVisit))
                //        throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                //    var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                //    if (!AppCommonMethod.IsNullObject(isERVisit))
                //        throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

                //}
                //if (VisitFor == CommonStringConstant.OPD)
                //{
                //    //var isIPDVisit = response.Where(x => x.VisitFor == CommonStringConstant.IPD).FirstOrDefault();
                //    //if (!AppCommonMethod.IsNullObject(isIPDVisit))
                //    //    throw new UserFriendlyException(CommonMessageConstant.IPDVisitIsAlreadyGenerated);

                //    //var isERVisit = response.Where(x => x.VisitFor == CommonStringConstant.ER).FirstOrDefault();
                //    //if (!AppCommonMethod.IsNullObject(isERVisit))
                //    //    throw new UserFriendlyException(CommonMessageConstant.ERVisitIsAlreadyGenerated);

                //    //var isOPDVisit = response.Where(x => x.VisitDate == DateTime.Now.Date).FirstOrDefault();
                //    //if (!AppCommonMethod.IsNullObject(isOPDVisit))
                //    //{
                    
                //    // }

                //}
                return null;
            }
            return null;
        }

        private async Task<bool> CheckIfPatientVisitExistInIPD(Guid? PatientId, int? HealthFacilityId, int? DepartmentLookupId)
        {


            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());
            var _uowPatientAdmissionDetail = new UnitOfWork<PatientAdmissionDetail>(_uowPatient.GetDbContext());

            PatientAdmissionDetail? response = await _uowPatientAdmissionDetail.Repository.GetALL()
                .Where(x => x.PatientId == PatientId &&
                    x.HealthFacilityId == HealthFacilityId &&
                    x.DepartmentLookupId == DepartmentLookupId &&
                    x.PatientVisit!.IsDischarge != true)
                .FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(response))
                return true;
            else
                return false;
        }

        private async Task<bool> CheckIfPatientVisitExistInER(Guid? PatientId, int? HealthFacilityId, int? DepartmentLookupId)
        {
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());
            //var _uowPatientAdmissionDetail = new UnitOfWork<PatientAdmissionDetail>(_uowPatient.GetDbContext());

            PatientOpenVisit? response = await _uowPatientOpenVisit.Repository.GetALL()
                .Where(x => x.PatientId == PatientId &&
                    x.HealthFacilityId == HealthFacilityId &&
                    x.DepartementLookupId == DepartmentLookupId &&
                    x.IsDischarge != true)
                .FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(response))
                return true;
            else
                return false;
        }


        private bool CheckLabTest(List<ViewTblabTest> labTestResults, string testCategory, string result)
        {
            foreach (var labTestResult in labTestResults)
            {
                if (labTestResult.TestName == testCategory && labTestResult.Result == result)
                {
                    return true;
                }
            }
            return false;
        }
        private void FillEntity(DbModel.Patient obj)
        {
            if (obj.PatientId == Guid.Empty)
            {
                obj.PatientId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityFeePayment(DbModel.FeePayment obj)
        {
            if (obj.FeePaymentId == Guid.Empty)
            {
                obj.FeePaymentId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatientEyeBlindness(DbModel.PatientEyeBlindness obj)
        {
            if (obj.PatientEyeBlindnessId == Guid.Empty)
            {
                obj.PatientEyeBlindnessId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityMortaury(DbModel.UnknownPatient obj)
        {
            if (obj.UnknownPatientId == Guid.Empty)
            {
                obj.UnknownPatientId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        public void FillEntityTbPatientDetails(DbModel.TbPatientDetail obj)
        {
            if (obj.TbPatientDetailsId == Guid.Empty)
            {
                obj.TbPatientDetailsId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityUnknownPatient(DbModel.UnknownPatient obj)
        {
            if (obj.UnknownPatientId == Guid.Empty)
            {
                obj.UnknownPatientId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityContactDetail(PatientContactDetail obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.ContactId))
            {
                obj.ContactId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityPatientOpenVisit(DbModel.PatientOpenVisit obj)
        {
            if (obj.PatientOpenVisitId == Guid.Empty)
            {
                obj.PatientOpenVisitId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatientImages(DbModel.PatientImage obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientImageId))
            {
                obj.PatientImageId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatientBae64(DbModel.ImageBaseSixtyFour obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.ImageBaseSixtyFourId))
            {
                obj.ImageBaseSixtyFourId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }



        private void FillEntityPatientSourceInfo(DbModel.PatientSourceInfo obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientSourceInfoId))
            {
                obj.PatientSourceInfoId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatient(DbModel.Patient obj)
        {
            if (obj.PatientId == Guid.Empty)
            {
                obj.PatientId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(DbModel.Patient obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }
        private void FillEntityPatientDiagnosisRecord(DbModel.PatientDiagnosisRecord obj)
        {
            if (obj.PatientDiagnosisRecordId == Guid.Empty)
            {
                obj.PatientId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityUpdate(DbModel.PatientContactDetail obj)
        {
            if (obj != null)
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        public async Task<string> GetTokenNo(int? HealthFacilityId, int? DepartmentId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[pt].[SPGetToken]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", DepartmentId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetTokenNoDto> lst = ds.Tables[0].ToList<GetTokenNoDto>();
                    //List<string> lst2 = ds.Tables[0].ToList<string>();
                    return lst.FirstOrDefault().TokenNo;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public int UpdateVisitNo(Guid? PatientId)
        {
            var visitNo = _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == PatientId && x.HealthFacilityId == TokenService.GetUserHfId()).Count();

            if (visitNo > 0)
                visitNo += 1;
            else
                visitNo = 1;
            return visitNo;

        }

        public int GetIPDVisitNo(Guid? PatientId)
        {
            var _uowPatientAdmissionDetail = new UnitOfWork<PatientAdmissionDetail>(_uowPatientOpenVisit.GetDbContext());
            var visitNo = _uowPatientAdmissionDetail.Repository.GetALL(x => x.PatientId == PatientId && x.HealthFacilityId == TokenService.GetUserHfId()).Count();

            if (visitNo > 0)
                visitNo += 1;
            else
                visitNo = 1;
            return visitNo;
        }

        public async Task<string> GenerateMRNNo(CreateOrEditPatientWithVisitDto input)
        {
            string prefixLocationCode = input.ProvinceId.ToString() + "-" + input.DivisionId.ToString() + "-" + input.DistrictId.ToString() + "-" + input.TehsilId.ToString() + "-" + input.HealthFacilityId.ToString();

            var patientLocation = _uowPatientLocationPrefix.Repository.GetALL(x => x.LocationCode == prefixLocationCode).FirstOrDefault();

            var patientCount = _uowPatient.Repository.GetCount();
            var gender = await _uowProfile.Repository.GetById(input.GenderProfileId!);
            var relation = await _uowProfile.Repository.GetById(input.RelationProfileId!);

            var prefixPatient = (++patientCount).ToString().PadLeft(9, '0');
            var prefixGender = CommonMethods.GetGenderMRNId(gender.Name);
            var prefixRelation = CommonMethods.GetRelationShipMRNId(relation.Name);
            var prefixLocation = string.Empty;

            if (patientLocation == null)
            {
                PatientLocationPrefix obj = new PatientLocationPrefix()
                {
                    LocationCode = prefixLocationCode,
                };

                FillEntityPatientLocation(obj);
                var response = await _uowPatientLocationPrefix.Repository.Insert(obj);
                await _uowPatientLocationPrefix.CommitAsync();

                prefixLocation = response.Id.ToString().PadLeft(4, '0');
            }
            else
            {
                prefixLocation = patientLocation.Id.ToString().PadLeft(4, '0');
            }

            if (!AppCommonMethod.IsNullBool(input.IsAfghanCnic) && !string.IsNullOrEmpty(input.Cnic))
                return CommonStringConstant.MRN + "-" + input.Cnic[3] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;

            if (!string.IsNullOrEmpty(input.PassportNo))
                return CommonStringConstant.MRN + "-" + input.PassportNo[2] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;

            return CommonStringConstant.MRN + "-" + input.Cnic[0] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;
        }

        public async Task<string> GenerateMRNNoCentrally(CreateOrEditPatientWithVisitCentrallyDto input)
        {
            string prefixLocationCode = input.Patient!.ProvinceId.ToString() + "-" + input.Patient!.DivisionId.ToString() + "-" + input.Patient!.DistrictId.ToString() + "-" + input.Patient!.TehsilId.ToString() + "-" + input.Patient!.HealthFacilityId.ToString();

            var patientLocation = _uowPatientLocationPrefix.Repository.GetALL(x => x.LocationCode == prefixLocationCode).FirstOrDefault();

            var patientCount = _uowPatient.Repository.GetCount();
            var gender = await _uowProfile.Repository.GetById(input.Patient!.GenderProfileId!);
            var relation = await _uowProfile.Repository.GetById(input.Patient!.RelationProfileId!);

            var prefixPatient = (++patientCount).ToString().PadLeft(9, '0');
            var prefixGender = CommonMethods.GetGenderMRNId(gender!.Name);
            var prefixRelation = CommonMethods.GetRelationShipMRNId(relation!.Name);
            var prefixLocation = string.Empty;

            if (patientLocation == null)
            {
                PatientLocationPrefix obj = new PatientLocationPrefix()
                {
                    LocationCode = prefixLocationCode,
                };

                FillEntityPatientLocation(obj);
                var response = await _uowPatientLocationPrefix.Repository.Insert(obj);
                await _uowPatientLocationPrefix.CommitAsync();

                prefixLocation = response.Id.ToString().PadLeft(4, '0');
            }
            else
            {
                prefixLocation = patientLocation.Id.ToString().PadLeft(4, '0');
            }

            if (!AppCommonMethod.IsNullBool(input.Patient!.IsAfghanCnic) && !string.IsNullOrEmpty(input.Patient!.Cnic))
                return CommonStringConstant.MRN + "-" + input.Patient!.Cnic[3] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;

            if (!string.IsNullOrEmpty(input.Patient!.PassportNo))
                return CommonStringConstant.MRN + "-" + input.Patient!.PassportNo[2] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;

            return CommonStringConstant.MRN + "-" + input.Patient!.Cnic[0] + prefixLocation + "-" + prefixGender + prefixRelation + "-" + prefixPatient;
        }

        private void FillEntityPatientLocation(PatientLocationPrefix obj)
        {
            if (obj.Id <= 0)
            {
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityAdmissionDetails(PatientAdmissionDetail obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientAdmissionDetailId))
            {
                obj.PatientAdmissionDetailId = Guid.NewGuid();
                obj.AdmittedInSpeciality = DateTime.Now;
                obj.PatientAdmittedOn = DateTime.Now;
                obj.IsActive = true;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatientNadraRequest(PatientNadraRequest obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientNadraRequestId))
            {
                obj.PatientNadraRequestId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityPatientNadraResponse(PatientNadraResponse obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientNadraResponseId))
            {
                obj.PatientNadraResponseId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityVerifiedPatientDataFromNADRA(PatientNadraInfoResponse obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientNadraInfoResponseId))
            {
                obj.PatientNadraInfoResponseId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private async Task<ViewPatientSlipDetailsDto> GetPatientDiagnoseById(Guid input)
        {
            var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
            var _uowMedicineDispatches = new UnitOfWork<MedicineDispatch>(_uowPatient.GetDbContext());

            var diagnoseObj = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientDiagnoseId == input)
                .Include(x => x.PatientVisit!.DepartementLookup)
                .Include(x => x.PatientVisit!.SectionLookup)
                .Include(x => x.PatientVisit!.HealthFacility)
                .Include(x => x.PatientVisit!.PatientVitals)
                .Include(x => x.Patient).ThenInclude(x => x!.GenderProfile)
                .Include(x => x.Patient).ThenInclude(x => x!.BloodGroupProfile)
                .Include(x => x.DiagnosedByNavigation).ThenInclude(x => x.DesignationProfile)
                .Include(y => y.PatientDiagnoseDiseases)
                //.Include(x => x.PatientDiagnoses).ThenInclude(y => y.PatientDiagnoseProcedures)
                //.Include(x => x.PatientPrescriptions).ThenInclude(x => x.Medicine)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseProfile)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseTimeProfile)
                .Include(x => x.PatientLabTests).ThenInclude(x => x.LabTest).ThenInclude(x => x.DepartmentProfile)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    IsEdit = true,
                    PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,
                    Doctor = y.DiagnosedByNavigation!.FullName,
                    DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
                    Section = y.PatientVisit!.SectionLookup!.Name,
                    Department = y.PatientVisit.DepartementLookup!.Name,
                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.PatientVisit.CreatedOn,
                    IsWillingToBuyMedPrivately = y.PatientVisit.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.Patient.FollowupDate,
                    FollowUpDate = y.FollowupDate,
                    PresentComplaints = y.PresentComplaints,
                    Examination = y.Examination,
                    PatientMedicalHistory = y.PatientMedicalHistory,
                    AdviseGiven = y.AdviseGiven,
                    ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
                    ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
                    ReferredToHealthFacilityId = y.ReferToHealthFacilityId,
                    ReferredToDepartmentLookupId = y.ReferToDepartmentLookupId,
                    ReferredToSectionLookupId = y.ReferToSectionLookupId,
                    IsGynaePatient = y.IsGynaePatient,

                    //IsAdmittedInIPD = y.IsAdmittedInIpd,
                    //IsReferredIpd = y.IsReferredIpd,
                    //IpdDepartmentLookupId = y.IpdDepartmentLookupId,
                    //IpdDepartmentLookupName = y.IpdDepartmentLookup!.Name,
                    //IpdSectionLookupId = y.IpdSectionLookupId,
                    //IpdSectionLookupName = y.IpdSectionLookup!.Name,
                    //IpdReferredBy = y.IpdReferredBy,
                    //IpdReferredByName = y.IpdReferredByNavigation!.FullName,
                    //IpdReferredByDepartmentLookupId = y.IpdReferredByDepartmentLookupId,
                    //IpdReferredByDepartmentLookupName = y.IpdReferredByDepartmentLookup!.Name,
                    //IpdReferredBySectionLookupId = y.IpdReferredBySectionLookupId,
                    //IpdReferredBySectionLookupName = y.IpdReferredBySectionLookup!.Name,

                    //PatientDiagnoseId =y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseId,
                    IsReferred = y.PatientVisit.IsReferred,
                    IsReferInternal = y.IsReferInternal,
                    DepartmentId = y.PatientVisit.DepartementLookupId,
                    SectionId = y.PatientVisit.SectionLookupId,

                    PatientDiagnoseId = y.PatientDiagnoseId,
                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,
                    //PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVitals.ToList()),
                    PatientVitals = y.PatientVisit.PatientVitals
                    .Select(x => new PatientVitalDto
                    {
                        PatientVitalId = x.PatientVitalId,
                        PatientId = x.PatientId,
                        PatientVisitId = x.PatientVisitId,
                        IsChangeBpsystolic = x.IsChangeBpsystolic,
                        Bpsystolic = x.Bpsystolic,
                        IsChangeBpdiaSystolic = x.IsChangeBpdiaSystolic,
                        BpdiaSystolic = x.BpdiaSystolic,
                        IsChangePulse = x.IsChangePulse,
                        Pulse = x.Pulse,
                        IsChangeTemprature = x.IsChangeTemprature,
                        Temprature = x.Temprature,
                        IsChangeWeight = x.IsChangeWeight,
                        Weight = x.Weight,
                        IsChangeHeight = x.IsChangeHeight,
                        Height = x.Height,
                        IsChangeResperatoryRate = x.IsChangeResperatoryRate,
                        ResperatoryRate = x.ResperatoryRate,
                        IsChangeBMI = x.IsChangeBmi,
                        BMI = x.Bmi,
                        VitalsCollectedBy = x.VitalsCollectedBy,
                        VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
                        CreatedOn = x.CreatedOn,
                    })
                    .ToList(),

                    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null) && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseasesName = x.DiseaseProfile.Name,
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiagnoseTypeId = x.DiagnoseTypeId
                    }).ToList(),

                    DefinitivePatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.DiagnoseTypeId == 2 && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseasesName = x.DiseaseProfile.Name,
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiagnoseTypeId = x.DiagnoseTypeId
                    }).ToList(),

                    PatientDiagnoseProcedures = y!.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
                    {
                        PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
                        SectionProcedureId = x.SectionProcedureId,
                        ProcedureTitle = x.SectionProcedure!.ProcedureTitle,
                        ProceduresName = x.SectionProcedure!.ProcedureTitle,
                        PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
                        RecommendBy = x.RecommendBy,
                        AssistedBy = x.AssistedBy,
                        PerformedBy = x.PerformedBy,
                        Feedback = x.Feedback,
                        IsPerformed = x.IsPerformed,
                        ToothNumber = x.ToothNumber,
                        ToothPosition = x.ToothPosition,
                        IsPaidProcedureFee = x.IsPaidProcedureFee
                    }).ToList(),

                    PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientPrescriptionDto
                    {
                        PatientPrescriptionId = x.PatientPrescriptionId,
                        MedicineId = x.MedicineId,
                        Days = x.Days,
                        DoseName = x.DoseProfile!.Name,
                        DoseTimeName = x.DoseTimeProfile!.Name,
                        MedicineName = x.MedicineName,
                        Quantity = x.Quantity,
                        AvailableQuantity = x.AvailableQuantity,
                        MedicineDose = x.MedicineDose,
                        MedicineDuration = x.MedicineDuration,
                        MedicineFrequency = x.MedicineFrequency,
                        MedicineInstruction = x.MedicineInstruction,
                        MedicineRoute = x.MedicineRoute,
                        BatchNo = x.BatchNo,
                        IsSMLMedicine = x.IsSMLMedicine
                    }).ToList(),



                    PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
                    {
                        LabDepartmentName = x.LabDepartmentProfile!.Name,
                        LabDepartmentProfileId = x.LabDepartmentProfileId,
                        DepartmentShortName = x.LabDepartmentProfile.ShortName,
                        LabTestName = x.LabTest!.Name,
                        LabTestId = x.LabTestId,
                        PatientLabTestId = x.PatientLabTestId,
                        IsSampleCollected = (x.IsSampleRequired == true) ? x.IsSampleCollected : true,
                        IsReportGenerated = x.IsReportGenerated,
                        IsSampleRejected = x.IsSampleRejected,
                        TestPrice = x.TestPrice,
                        IsPaid = x.IsPaid,
                        IsRefunded = x.IsRefunded
                    }).ToList(),
                }).FirstOrDefaultAsync();

            var MedicineDispatches = await _uowMedicineDispatches.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseObj!.PatientDiagnoseId)
                .Select(x => new PatientDto.MedicineDispatchDto
                {
                    PatientDiagnoseId = x.PatientDiagnoseId,
                    PatientPrescriptionId = x.PatientPrescriptionId,
                    MedicineId = x.MedicineId,
                    MedicineName = x.MedicineName,
                    QuantityDispatch = x.QuantityDispatch,
                    QuantityPrescribed = x.QuantityPrescribed
                }).ToListAsync();

            // Convert List<T> to ICollection<T>
            diagnoseObj!.MedicineDispatch = new Collection<PatientDto.MedicineDispatchDto>(MedicineDispatches!);

            foreach (var item in diagnoseObj!.PatientDiagnosesDiseases)
            {
                if (string.IsNullOrEmpty(diagnoseObj.DiseasesName))
                    diagnoseObj.DiseasesName += item.DiseasesName;
                else
                    diagnoseObj.DiseasesName += ", " + item.DiseasesName;
            }

            //if (!AppCommonMethod.IsNullOrEmptyList<PatientPrescriptionDto>(diagnoseObj.PatientMedicine.ToList()))
            //{
            //    var _uowMimsMedicineData = new UnitOfWork<MimsMedicineDatum>(_uowPatient.GetDbContext());

            //    var medLookup = await _uowMimsMedicineData.Repository.GetALL().ToListAsync();

            //    foreach (var patientMedicine in diagnoseObj.PatientMedicine)
            //        patientMedicine.IsSMLMedicine = medLookup.Where(x => x.MedicineId == patientMedicine.MedicineId).Select(x => x.IsSMLMedicine).FirstOrDefault();
            //}





            //foreach (var item in diagnoseObj!.PatientDiagnoseProcedures)
            //{
            //    if (string.IsNullOrEmpty(diagnoseObj!.ProceduresName))
            //        diagnoseObj.ProceduresName += item.ProceduresName;
            //    else
            //        diagnoseObj.ProceduresName += ", " + item.ProceduresName;
            //}

            return diagnoseObj;

        }

        private async Task<ViewPatientSlipDetailsDto> GetPatientVisitDetails(Guid input)
        {

            var patientVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.DepartementLookup)
                .Include(x => x.SectionLookup)
                .Include(x => x.HealthFacility)
                .Include(x => x.PatientVitals)
                .Include(x => x.Patient).ThenInclude(x => x!.GenderProfile)
                .Include(x => x.Patient).ThenInclude(x => x!.BloodGroupProfile)
                .Include(x => x.PatientDiagnoses).ThenInclude(x => x.DiagnosedByNavigation).ThenInclude(x => x.DesignationProfile)
                .Include(x => x.PatientDiagnoses).ThenInclude(y => y.PatientDiagnoseDiseases)
                .Include(x => x.PatientDiagnoses).ThenInclude(y => y.PatientDiagnoseProcedures)
                //.Include(x => x.PatientPrescriptions).ThenInclude(x => x.Medicine)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseProfile)
                .Include(x => x.PatientPrescriptions).ThenInclude(x => x.DoseTimeProfile)
                .Include(x => x.PatientLabTests).ThenInclude(x => x.LabTest).ThenInclude(x => x.DepartmentProfile)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientOpenVisitId,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,
                    Doctor = y.PatientDiagnoses!.FirstOrDefault()!.DiagnosedByNavigation!.FullName,
                    DoctorDesignation = y.PatientDiagnoses!.FirstOrDefault()!.DiagnosedByNavigation!.DesignationProfile!.Name,
                    Section = y.SectionLookup!.Name,
                    Department = y.DepartementLookup!.Name,
                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.CreatedOn,
                    IsWillingToBuyMedPrivately = y.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.Patient.FollowupDate,
                    PresentComplaints = y.PatientDiagnoses.FirstOrDefault()!.PresentComplaints,
                    Examination = y.PatientDiagnoses.FirstOrDefault()!.Examination,
                    PatientMedicalHistory = y.PatientDiagnoses.FirstOrDefault()!.PatientMedicalHistory,
                    AdviseGiven = y.PatientDiagnoses.FirstOrDefault()!.AdviseGiven,
                    ReferredDepartmentLookupId = y.ReferredDepartmentLookupId,
                    ReferredSectionLookupId = y.ReferredSectionLookupId,

                    IsAdmittedInIPD = y.IsAdmittedInIpd,
                    IsReferredIpd = y.IsReferredIpd,
                    IpdDepartmentLookupId = y.IpdDepartmentLookupId,
                    IpdDepartmentLookupName = y.IpdDepartmentLookup!.Name,
                    IpdSectionLookupId = y.IpdSectionLookupId,
                    IpdSectionLookupName = y.IpdSectionLookup!.Name,
                    IpdReferredBy = y.IpdReferredBy,
                    IpdReferredByName = y.IpdReferredByNavigation!.FullName,
                    IpdReferredByDepartmentLookupId = y.IpdReferredByDepartmentLookupId,
                    IpdReferredByDepartmentLookupName = y.IpdReferredByDepartmentLookup!.Name,
                    IpdReferredBySectionLookupId = y.IpdReferredBySectionLookupId,
                    IpdReferredBySectionLookupName = y.IpdReferredBySectionLookup!.Name,

                    //PatientDiagnoseId =y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseId,

                    PatientDiagnoseId = y.PatientDiagnoses.FirstOrDefault() != null ? y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseId : Guid.Empty,
                    HealthFacilityName = y.HealthFacility!.Name,
                    TokenNo = y.TokenNo,
                    //PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVitals.ToList()),
                    PatientVitals = y.PatientVitals
                    .Select(x => new PatientVitalDto
                    {
                        PatientVitalId = x.PatientVitalId,
                        PatientId = x.PatientId,
                        PatientVisitId = x.PatientVisitId,
                        IsChangeBpsystolic = x.IsChangeBpsystolic,
                        Bpsystolic = x.Bpsystolic,
                        IsChangeBpdiaSystolic = x.IsChangeBpdiaSystolic,
                        BpdiaSystolic = x.BpdiaSystolic,
                        IsChangePulse = x.IsChangePulse,
                        Pulse = x.Pulse,
                        IsChangeTemprature = x.IsChangeTemprature,
                        Temprature = x.Temprature,
                        IsChangeWeight = x.IsChangeWeight,
                        Weight = x.Weight,
                        IsChangeHeight = x.IsChangeHeight,
                        Height = x.Height,
                        IsChangeResperatoryRate = x.IsChangeResperatoryRate,
                        ResperatoryRate = x.ResperatoryRate,
                        IsChangeBMI = x.IsChangeBmi,
                        BMI = x.Bmi,
                        Hip = x.Hip,
                        Waist = x.Waist,
                        RatioHipToWaist = x.RatioHipToWaist,
                        VitalsCollectedBy = x.VitalsCollectedBy,
                        VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
                        CreatedOn = x.CreatedOn,
                        VitalsCollectedByDesignation = x.VitalsCollectedByNavigation.DesignationProfile.Name
                    })
                    .ToList(),


                    PatientDiagnosesDiseases = y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseasesName = x.DiseaseProfile.Name,
                        DiseaseProfileId = x.DiseaseProfileId
                    }).ToList(),

                    PatientDiagnoseProcedures = y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
                    {
                        PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
                        SectionProcedureId = x.SectionProcedureId,
                        ProcedureTitle = x.SectionProcedure!.ProcedureTitle,
                        ProceduresName = x.SectionProcedure!.ProcedureTitle,
                        PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
                        RecommendBy = x.RecommendBy,
                        PerformedBy = x.PerformedBy,
                        Feedback = x.Feedback,
                        IsPerformed = x.IsPerformed,

                    }).ToList(),

                    PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientPrescriptionDto
                    {
                        MedicineId = x.MedicineId,
                        Days = x.Days,
                        DoseName = x.DoseProfile!.Name,
                        DoseTimeName = x.DoseTimeProfile!.Name,
                        MedicineName = x.MedicineName,
                        PatientPrescriptionId = x.PatientPrescriptionId,
                        Quantity = x.Quantity,
                        AvailableQuantity = x.AvailableQuantity,
                        MedicineDose = x.MedicineDose,
                        MedicineDuration = x.MedicineDuration,
                        MedicineFrequency = x.MedicineFrequency,
                        MedicineInstruction = x.MedicineInstruction,
                        MedicineRoute = x.MedicineRoute,
                        BatchNo = x.BatchNo
                    }).ToList(),

                    PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
                    {
                        LabDepartmentName = x.LabDepartmentProfile!.Name,
                        LabTestName = x.LabTest!.Name,
                        PatientLabTestId = x.PatientLabTestId
                    }).ToList(),
                }).FirstOrDefaultAsync();


            foreach (var item in patientVisit!.PatientDiagnosesDiseases)
            {
                if (string.IsNullOrEmpty(patientVisit.DiseasesName))
                    patientVisit.DiseasesName += item.DiseasesName;
                else
                    patientVisit.DiseasesName += ", " + item.DiseasesName;
            }

            foreach (var item in patientVisit!.PatientDiagnoseProcedures)
            {
                if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
                    patientVisit.ProceduresName += item.ProceduresName;
                else
                    patientVisit.ProceduresName += ", " + item.ProceduresName;
            }

            return patientVisit;

        }

        private async Task<ViewPatientVitalSlipDetailsDto> GetPatientVisitDetailsForVitals(Guid input)
        {

            var patientVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.DepartementLookup)
                .Include(x => x.SectionLookup)
                .Include(x => x.HealthFacility)
                .Include(x => x.Patient).ThenInclude(x => x!.GenderProfile)
                .Select(y => new ViewPatientVitalSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientOpenVisitId,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,
                    Section = y.SectionLookup!.Name,
                    BedNo = y.BedNo,
                    //VitalsFloorNo = y.HealthFacility.HfDepartments.Where(x => x.DepartmentLookupId == y.DepartementLookupId).FirstOrDefault()!.HfDepartmentSections.Where(x => x.HfDepartmentSectionId == y.SectionLookupId).FirstOrDefault()!.VitalsFloorNo,
                    //VitalsRoomNo = y.HealthFacility.HfDepartments.Where(x => x.DepartmentLookupId == y.DepartementLookupId).FirstOrDefault()!.HfDepartmentSections.Where(x => x.HfDepartmentSectionId == y.SectionLookupId).FirstOrDefault()!.VitalsRoomNo,
                    Department = y.DepartementLookup!.Name,
                    DepartmentId = y.DepartementLookup!.DepartmentLookupId,
                    SectionId = y.SectionLookup!.SectionLookupId,
                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.CreatedOn,
                    NextVisitDate = y.Patient.FollowupDate,
                    HealthFacilityName = y.HealthFacility!.Name,
                    HealthFacilityId = y.HealthFacility!.HealthFacilityId,
                    TokenNo = y.TokenNo,
                }).FirstOrDefaultAsync();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetHfDepartmentSectionByHealthFacilityId", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(patientVisit!.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", patientVisit!.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt((patientVisit!.DepartmentId)))
                        sqlComm.Parameters.AddWithValue("@DepartmentLookupId", patientVisit!.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt((patientVisit!.SectionId)))
                        sqlComm.Parameters.AddWithValue("@SectionLookupId", patientVisit!.SectionId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewHfDepartmentSectionFloorRoomDto> lst = ds.Tables[0].ToList<ViewHfDepartmentSectionFloorRoomDto>();
                    ViewHfDepartmentSectionFloorRoomDto objHfDepartmentSection = lst.FirstOrDefault();
                    if (!AppCommonMethod.IsNullObject(objHfDepartmentSection))
                    {
                        patientVisit.VitalsFloorNo = objHfDepartmentSection!.VitalsFloorNo;
                        patientVisit.VitalsRoomNo = objHfDepartmentSection!.VitalsRoomNo;
                        patientVisit.DoctorFloorNo = objHfDepartmentSection!.DoctorFloorNo;
                        patientVisit.DoctorRoomNo = objHfDepartmentSection!.DoctorRoomNo;
                        patientVisit.PharmacyFloorNo = objHfDepartmentSection!.PharmacyFloorNo;
                        patientVisit.PharmacyRoomNo = objHfDepartmentSection!.PharmacyRoomNo;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return patientVisit;
        }
        private async Task<ViewPatientSlipDetailsDto> GetPatientVisitDetailsForDoctor(Guid input)
        {

            var patientVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.DepartementLookup)
                .Include(x => x.SectionLookup)
                .Include(x => x.PatientConditionProfile)
                .Include(x => x.HealthFacility)
                .Include(x => x.PatientVitals)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.Patient).ThenInclude(x => x!.GenderProfile)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientOpenVisitId,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,
                    Section = y.SectionLookup!.Name,
                    BedNo = y.BedNo,
                    PatientCondition = y.PatientConditionProfile!.Name,
                    FormType = y.SectionLookup.FormType,
                    //VitalsFloorNo = y.HealthFacility.HfDepartments.Where(x => x.DepartmentLookupId == y.DepartementLookupId).FirstOrDefault()!.HfDepartmentSections.Where(x => x.HfDepartmentSectionId == y.SectionLookupId).FirstOrDefault()!.VitalsFloorNo,
                    //VitalsRoomNo = y.HealthFacility.HfDepartments.Where(x => x.DepartmentLookupId == y.DepartementLookupId).FirstOrDefault()!.HfDepartmentSections.Where(x => x.HfDepartmentSectionId == y.SectionLookupId).FirstOrDefault()!.VitalsRoomNo,
                    Department = y.DepartementLookup!.Name,
                    DepartmentId = y.DepartementLookup!.DepartmentLookupId,
                    SectionId = y.SectionLookup!.SectionLookupId,
                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.CreatedOn,
                    IsWillingToBuyMedPrivately = y.IsWillingToBuyMedPrivately,
                    //NextVisitDate = y.Patient.FollowupDate,
                    HealthFacilityName = y.HealthFacility!.Name,
                    HealthFacilityId = y.HealthFacility!.HealthFacilityId,
                    TokenNo = y.TokenNo,
                    PatientDiagnoseId = y.PatientDiagnoses.Count() > 0 ? y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseId : Guid.Empty,
                    PresentComplaints = y.PatientDiagnoses.Count() > 0 ? y.PatientDiagnoses.FirstOrDefault()!.PresentComplaints : null,
                    Examination = y.PatientDiagnoses.Count() > 0 ? y.PatientDiagnoses.FirstOrDefault()!.Examination : null,
                    PatientMedicalHistory = y.PatientDiagnoses.Count() > 0 ? y.PatientDiagnoses.FirstOrDefault()!.PatientMedicalHistory : null,
                    AdviseGiven = y.PatientDiagnoses.Count() > 0 ? y.PatientDiagnoses.FirstOrDefault()!.AdviseGiven : null,
                    PatientDiagnosesDiseases = y.PatientDiagnoses.FirstOrDefault()!.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseasesName = x.DiseaseProfile.Name,
                        DiseaseProfileId = x.DiseaseProfileId
                    }).ToList(),
                    PatientVitals = y.PatientVitals
                    .Select(x => new PatientVitalDto
                    {
                        PatientVitalId = x.PatientVitalId,
                        PatientId = x.PatientId,
                        PatientVisitId = x.PatientVisitId,
                        IsChangeBpsystolic = x.IsChangeBpsystolic,
                        Bpsystolic = x.Bpsystolic,
                        IsChangeBpdiaSystolic = x.IsChangeBpdiaSystolic,
                        BpdiaSystolic = x.BpdiaSystolic,
                        IsChangePulse = x.IsChangePulse,
                        Pulse = x.Pulse,
                        IsChangeTemprature = x.IsChangeTemprature,
                        Temprature = x.Temprature,
                        IsChangeWeight = x.IsChangeWeight,
                        Weight = x.Weight,
                        IsChangeHeight = x.IsChangeHeight,
                        Height = x.Height,
                        IsChangeResperatoryRate = x.IsChangeResperatoryRate,
                        ResperatoryRate = x.ResperatoryRate,
                        IsChangeBMI = x.IsChangeBmi,
                        BloodSugar = x.BloodSugar,
                        BMI = x.Bmi,
                        Hip = x.Hip,
                        Waist = x.Waist,
                        RatioHipToWaist = x.RatioHipToWaist,
                        VitalsCollectedBy = x.VitalsCollectedBy,
                        VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
                        CreatedOn = x.CreatedOn,
                        VitalsCollectedByDesignation = x.VitalsCollectedByNavigation.DesignationProfile.Name
                    })
                    .ToList(),
                }).FirstOrDefaultAsync();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetHfDepartmentSectionByHealthFacilityId", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(patientVisit!.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", patientVisit!.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt((patientVisit!.DepartmentId)))
                        sqlComm.Parameters.AddWithValue("@DepartmentLookupId", patientVisit!.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt((patientVisit!.SectionId)))
                        sqlComm.Parameters.AddWithValue("@SectionLookupId", patientVisit!.SectionId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewHfDepartmentSectionFloorRoomDto> lst = ds.Tables[0].ToList<ViewHfDepartmentSectionFloorRoomDto>();
                    ViewHfDepartmentSectionFloorRoomDto objHfDepartmentSection = lst.FirstOrDefault();
                    if (!AppCommonMethod.IsNullObject(objHfDepartmentSection))
                    {
                        patientVisit.VitalsFloorNo = objHfDepartmentSection!.VitalsFloorNo;
                        patientVisit.VitalsRoomNo = objHfDepartmentSection!.VitalsRoomNo;
                        patientVisit.DoctorFloorNo = objHfDepartmentSection!.DoctorFloorNo;
                        patientVisit.DoctorRoomNo = objHfDepartmentSection!.DoctorRoomNo;
                        patientVisit.PharmacyFloorNo = objHfDepartmentSection!.PharmacyFloorNo;
                        patientVisit.PharmacyRoomNo = objHfDepartmentSection!.PharmacyRoomNo;
                    }

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }


            if (patientVisit.FormType == CommonStringConstant.NCDClinicForm || patientVisit.FormType == CommonStringConstant.MuawinClinicsForm)
            {
                var userObj = TokenService.GetUserLoggedInfo();
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var followUpDate = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId != patientVisit.PatientOpenVisitId && x.PatientId == patientVisit.PatientId && x.DocSectionLookupId == patientVisit.SectionId)
                    .OrderByDescending(x => x.CreatedOn).Select(x => x.FollowupDate).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullorEmptyDate(followUpDate))
                {
                    patientVisit.IsFollowUp = true;
                    if (patientVisit.FormType == CommonStringConstant.NCDClinicForm)
                    {

                        var data = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, patientVisit.FormType);
                        if (!AppCommonMethod.IsNullObject(data))
                        {
                            if (!AppCommonMethod.IsNullObject(data.data))
                            {
                                string lastVisitId = data.data.ToString();
                                if (!AppCommonMethod.IsNullObject(data.data))
                                {
                                    patientVisit.PatientLastVisitId = Guid.Parse(lastVisitId);
                                    //if(!AppCommonMethod.IsNullOrEmptyGuid(patientVisit.PatientLastVisitId))
                                    //var lastCreatedOn = _uowPatientOpenVisit.Repository.GetById(patientVisit.PatientLastVisitId)!.Result!.CreatedOn;

                                    //patientVisit.lastCreatedOn = lastCreatedOn;
                                }
                            }
                        }


                        var muawindata = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, CommonStringConstant.MuawinClinicsForm);
                        
                        if (!AppCommonMethod.IsNullObject(muawindata))
                        {
                            if (!AppCommonMethod.IsNullObject(muawindata.data))
                            {
                                var lastVisitData = Newtonsoft.Json.JsonConvert.DeserializeObject<PatientLastAssessmentDTO>(muawindata!.data!.ToString()!);
                                if (!AppCommonMethod.IsNullObject(lastVisitData))
                                {
                                    patientVisit.TreatmentOutCome = lastVisitData.TreatmentOutCome;
                                }
                            }
                        }
                    }
                    else if (patientVisit.FormType == CommonStringConstant.MuawinClinicsForm)
                    {

                        var data = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, patientVisit.FormType);
                        //string lastVisitId =  data.data.ToString();

                        if (!AppCommonMethod.IsNullObject(data))
                        {
                            if (!AppCommonMethod.IsNullObject(data.data))
                            {
                                var lastVisitData = Newtonsoft.Json.JsonConvert.DeserializeObject<PatientLastAssessmentDTO>(data!.data!.ToString()!);
                                if (!AppCommonMethod.IsNullObject(lastVisitData))
                                {
                                    patientVisit.PatientLastVisitId = lastVisitData.PatientVisitId;
                                    patientVisit.TreatmentOutCome = lastVisitData.TreatmentOutCome;

                                    //var lastCreatedOn = _uowPatientOpenVisit.Repository.GetById(patientVisit.PatientLastVisitId)!.Result!.CreatedOn;

                                    //patientVisit.lastCreatedOn = lastCreatedOn;
                                }
                            }
                        }
                    }
                }
                else
                {
                    var data = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, CommonStringConstant.NCDClinicForm);
                    if (!AppCommonMethod.IsNullObject(data))
                    {
                        if (!AppCommonMethod.IsNullObject(data.data))
                        {
                            string lastVisitId = data.data.ToString();
                            if (!string.IsNullOrEmpty(lastVisitId))
                            {
                                if (!AppCommonMethod.IsNullOrEmptyGuid(Guid.Parse(lastVisitId)))
                                {
                                    patientVisit.PatientLastVisitId = Guid.Parse(lastVisitId);
                                    //if(!AppCommonMethod.IsNullOrEmptyGuid(patientVisit.PatientLastVisitId))
                                    var lastCreatedOn = _uowPatientOpenVisit.Repository.GetById(patientVisit.PatientLastVisitId)!.Result!.CreatedOn;

                                    patientVisit.lastCreatedOn = lastCreatedOn;
                                    patientVisit.IsAssessmentNegative = true;
                                }
                            }
                        }
                    }
                }

            }
            else if (patientVisit.FormType == CommonStringConstant.NcdAndMuawinClinicForm)
            {
                var userObj = TokenService.GetUserLoggedInfo();
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var followUpDate = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId != patientVisit.PatientOpenVisitId && x.PatientId == patientVisit.PatientId && x.DocSectionLookupId == userObj.SectionId)
                    .OrderByDescending(x => x.CreatedOn).Select(x => x.FollowupDate).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullorEmptyDate(followUpDate))
                {
                    patientVisit.IsFollowUp = true;
                    if (userObj.FormType == CommonStringConstant.NCDClinicForm)
                    {

                        var data = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, userObj.FormType);
                        if (!AppCommonMethod.IsNullObject(data))
                        {
                            if (!AppCommonMethod.IsNullObject(data.data))
                            {
                                string lastVisitId = data.data.ToString();
                                if (!AppCommonMethod.IsNullObject(data.data))
                                {
                                    patientVisit.PatientLastVisitId = Guid.Parse(lastVisitId);
                                    //if(!AppCommonMethod.IsNullOrEmptyGuid(patientVisit.PatientLastVisitId))
                                    //var lastCreatedOn = _uowPatientOpenVisit.Repository.GetById(patientVisit.PatientLastVisitId)!.Result!.CreatedOn;

                                    //patientVisit.lastCreatedOn = lastCreatedOn;
                                }
                            }
                        }
                    }
                    else if (userObj.FormType == CommonStringConstant.MuawinClinicsForm)
                    {

                        var data = await _nCDService.GetPatientLastVisitIdForNcdAssesmentQuestions(patientVisit.PatientId, userObj.FormType);
                        //string lastVisitId =  data.data.ToString();

                        if (!AppCommonMethod.IsNullObject(data))
                        {
                            if (!AppCommonMethod.IsNullObject(data.data))
                            {
                                var lastVisitData = Newtonsoft.Json.JsonConvert.DeserializeObject<PatientLastAssessmentDTO>(data!.data!.ToString()!);
                                if (!AppCommonMethod.IsNullObject(lastVisitData))
                                {
                                    patientVisit.PatientLastVisitId = lastVisitData.PatientVisitId;
                                    patientVisit.TreatmentOutCome = lastVisitData.TreatmentOutCome;

                                    var lastCreatedOn = _uowPatientOpenVisit.Repository.GetById(patientVisit.PatientLastVisitId)!.Result!.CreatedOn;

                                    patientVisit.lastCreatedOn = lastCreatedOn;
                                }
                            }
                        }
                    }
                }

            }
            return patientVisit;
        }


        private async Task<ViewPatientIpdReceipt> GetPatientVisitDetailsForIpdRecipt(Guid input)
        {

            var patientVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)

                .Select(y => new ViewPatientIpdReceipt
                {
                    PatientOpenVisitId = y.PatientOpenVisitId,
                    PatientId = y.PatientId ?? Guid.Empty,
                    Mrno = y.Patient!.Mrno,

                    DepartmentName = y.DepartementLookup!.Name,
                    SectionName = y.SectionLookup!.Name,

                    PatientName = y.Patient.FullName,
                    GurdianName = y.Patient.GuardianName,
                    Age = y.Patient.Age,
                    Dob = y.Patient.Dob,
                    Gender = y.Patient.GenderProfile!.Name,
                    CNIC = y.Patient.Cnic,
                    ContactNo = y.Patient.MobileNo,
                    Address = y.Patient.ParmanentAddress,
                    VisitDate = y.CreatedOn,
                    NextVisitDate = y.Patient.FollowupDate,
                    HealthFacilityName = y.HealthFacility!.Name,
                    TokenNo = y.TokenNo,

                    CreatedOn = y.CreatedOn,
                    CreatedbyName = y.CreatedByNavigation!.FullName,

                    UpdatedOn = y.UpdatedOn,
                    UpdatedByName = y.UpdatedByNavigation!.FullName,

                }).FirstOrDefaultAsync();


            return patientVisit;
        }



        private async Task<Guid> GetAgeProfileId(DateTime Dob)
        {

            // Calculate age
            DateTime currentDate = DateTime.Now;

            // Calculate age
            TimeSpan ageDifference = currentDate - Dob;
            DateTime ageDateTime = DateTime.MinValue + ageDifference;

            // Extract years, months, and days
            int ageInYears = ageDateTime.Year - 1;
            int ageInMonths = ageDateTime.Month - 1;
            int ageInDays = ageDateTime.Day - 1;
            var profileId = new Guid();

            if (ageInYears > 0)
            {
                profileId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.Years).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else if (ageInMonths > 0 && ageInYears == 0)
            {
                profileId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.Months).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }
            else if (ageInDays >= 0 && ageInMonths == 0 && ageInYears == 0)
            {
                profileId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.Days).Select(x => x.ProfileId).FirstOrDefaultAsync();
            }


            return profileId;
        }


        #endregion

        #region EmergencyDoctor
        public async Task<ViewPatientSlipDetailsDto> GetPatientVisitForEmergencyDoctorById(Guid input, bool? IsFromPMIS, bool? isPatientHistory)
        {
            var userInfo = TokenService.GetUserLoggedInfo();

            // *** Check if doctor already have/occupied another patient ***//
            var isVisitOccupied = await _uowPatientOpenVisit.Repository.GetALL(x =>
            x.PatientOpenVisitId != input &&
            x.IsOccupied == true &&
            x.VisitDate == DateTime.Today &&
            x.OccupiedBy == _tokenService.GetUserId())
            .Include(x => x.Patient)
            .FirstOrDefaultAsync();

            if ((bool)!isPatientHistory)
            {
                if (!AppCommonMethod.IsNullObject(isVisitOccupied))
                {
                    var message = "Patient ";
                    if (!AppCommonMethod.IsNullObject(isVisitOccupied!.Patient))
                        message += isVisitOccupied!.Patient!.FirstName;

                    if (!string.IsNullOrEmpty(isVisitOccupied.TokenNo))
                        message += " with Token No " + isVisitOccupied.TokenNo;

                    message += " is Occupied, kindly attend First";
                    throw new UserFriendlyException(message);
                }
            }


            // Need to Refine 
            var dbObj = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input)
                .Include(x => x.OccupiedByNavigation)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Department)
                .Include(x => x.OccupiedByNavigation).ThenInclude(x => x!.Section)
                .Include(x => x.PatientDiagnoses)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

            if (dbObj!.VisitFor == CommonStringConstant.ER || dbObj!.VisitFor == CommonStringConstant.IPD)
                if (dbObj.IsDischarge == true)
                    throw new UserFriendlyException(CommonMessageConstant.PatientVisitClosedAlready);


            #region TB
            if (dbObj.DepartementLookupId == CommonStringConstant.TbDepartmentId && dbObj.SectionLookupId == CommonStringConstant.TbSectionId)
            {
                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(data))
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientVisitClosedAlready);
                }
            }
            #endregion

            //if()


            if (!dbObj!.IsFromPmis)
            {

                // Check If Patient is Already Occupied by some other user
                if (dbObj.IsOccupied == true && dbObj?.OccupiedBy != _tokenService.GetUserId())
                {
                    var message = "Patient is already Occupied by ";
                    message += "User ( " + dbObj!.OccupiedByNavigation!.FullName + " )";

                    if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Department))
                    {
                        message += " in ( " + dbObj!.OccupiedByNavigation!.Department!.Name;
                        if (!AppCommonMethod.IsNullObject(dbObj.OccupiedByNavigation!.Section))
                            message += " / " + dbObj!.OccupiedByNavigation!.Section!.Name;
                        message += " ) ";
                    }

                    throw new UserFriendlyException(message);
                }
                else
                {
                    //if (dbObj?.AttendedBy != null && dbObj?.AttendedBy != _tokenService.GetUserId())
                    //    throw new UserFriendlyException(CommonMessageConstant.PatientCalledAnotherDoctor);
                    //else
                    dbObj!.AttendedBy = _tokenService.GetUserId();
                }
            }
            //else // if PMIS
            //dbObj.AttendedBy = dbObj.AttendedBy; // Attended By already filled When Visit is generated

            // It should be save in Redis and Get from there 
            var currentStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                .OrderBy(x => x.SequenceNo)
            .Select(x => new ViewStationDto
            {
                StationProfileId = x.ProfileId,
                SequenceNo = x.SequenceNo,
                ShortName = x.ShortName,
                Name = x.Name
            }).FirstOrDefaultAsync();

            dbObj.IsDischarge = false;

            if (dbObj!.VisitFor == CommonStringConstant.OPD)
            {
                dbObj.DepartementLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.DepartmentId) ? dbObj.DepartementLookupId : userInfo!.DepartmentId;
                dbObj.SectionLookupId = AppCommonMethod.IsNullorZeroInt(userInfo!.SectionId) ? dbObj.SectionLookupId : userInfo!.SectionId;
            }

            dbObj.CurrentStationProfileId = currentStation!.StationProfileId;

            dbObj.IsOccupied = true;
            dbObj.OccupiedBy = _tokenService.GetUserId();

            dbObj.UpdatedBy = _tokenService.GetUserId();
            dbObj.UpdatedOn = DateTime.Now;
            dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientOpenVisit.Repository.Update(dbObj!);
            await _uowPatientOpenVisit.CommitAsync();

            //ViewPatientSlipDetailsDto result = await GetPatientVisitDetails(input);

            ViewPatientSlipDetailsDto result = new ViewPatientSlipDetailsDto();

            if (userInfo.IsConsultant == true || userInfo.SectionName == CommonStringConstant.DentalSurgeonOPD)
                result = await GetPatientVisitDetails(input);
            else
                result = await GetPatientVisitDetailsForDoctor(input);


            // *********** Araiz ********* //

            #region This is Only For TB Patients
            var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
            var sectionObj = await _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == dbObj.SectionLookupId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(sectionObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            if (sectionObj.FormType == CommonStringConstant.TbForm)
            {


                var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                var tbPatData = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsReferToDrtb == true).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
                var userObj = await GetLoggedInfoUserDTO();

                if (!AppCommonMethod.IsNullObject(tbPatData) && !(userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0) && (bool)!isPatientHistory)
                {
                    result.IsReferToDRTB = true;
                    return result;
                }

                var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatient.GetDbContext());
                var patientDiagnose = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == result.PatientId && x.IsConfirmed == true)
                    .Select(x => new ViewPatientSlipDetailsDto { IsConfirmed = x.IsConfirmed, TbStatusConfirmedDate = x.CreatedOn }).ToListAsync();

                //if (!AppCommonMethod.IsNullOrEmptyList(patientDiagnose))
                //{
                //    result.IsConfirmed = true;
                //    result.TbStatusConfirmedDate = patientDiagnose.Min(x => x.TbStatusConfirmedDate);
                //}




                var _uowPatientVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientOpenVisit.GetDbContext());
                var patientVisitFlowObj = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                var _uowTbPatientDetail = new UnitOfWork<TbPatientDetail>(_uowPatientOpenVisit.GetDbContext());

                if (!AppCommonMethod.IsNullObject(patientVisitFlowObj))
                {
                    var patientTreatmentStartDate = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo).OrderBy(x => x.FollowUpNo).Select(x => x.CreatedOn).FirstOrDefaultAsync();

                    result.IsFollowUp = patientVisitFlowObj.IsFollowUp;
                    result.FollowUpNo = patientVisitFlowObj.FollowUpNo;

                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatient.GetDbContext());

                    // last Visit Lab Test
                    var labTests = await _uowPatientLabTest.GetDbContext().ViewPatientLabTestListsForTbs.Where(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId)
                   .ToListAsync();


                    var patVisit = await _uowPatientVisitFlow.Repository.GetALL(x => x.PatientVisitId == dbObj.PatientOpenVisitId).FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(patVisit))
                    {
                        if (AppCommonMethod.IsNullObject(result.FollowUpNo))
                        {
                            result.IsConfirmed = false;
                        }
                        else
                        {
                            var data = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == patVisit.LastVisitId).FirstOrDefaultAsync();
                            if (!AppCommonMethod.IsNullObject(data))
                            {
                                if ((bool)data?.IsConfirmed)
                                {
                                    result.IsConfirmed = true;
                                }
                                else
                                {
                                    result.IsConfirmed = false;
                                }
                            }
                            else
                            {
                                result.IsConfirmed = false;
                            }
                        }


                        if (userObj.UserRoleList.Where(x => x.Name == RoleConst.DRTbDoctor).Count() > 0)
                        {
                            result.IsConfirmed = true;
                        }



                        var tbPatObj = await _uowTbPatientDetail.Repository.GetALL(x => x.PatientTreatmentCycleNo == patientVisitFlowObj.PatientTreatmentCycleNo)
                            .OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();

                        result.noOfMonthsPassed = !AppCommonMethod.IsNullObject(tbPatObj) ? tbPatObj?.NoOfMonthsMedicineIssued : 0;




                        var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatient.GetDbContext());
                        var medicinePrescribed = await _uowPatientPrescription.Repository.GetALL(x => x.PatientVisitId == patientVisitFlowObj.LastVisitId).FirstOrDefaultAsync();

                        if (!AppCommonMethod.IsNullObject(medicinePrescribed))
                        {
                            result.IsMedicinePrescribedInLastVisit = true;
                            if (result.FollowUpNo > 0)
                            {
                                result.IsConfirmed = true;
                            }
                        }




                        if (!AppCommonMethod.IsNullOrEmptyList(labTests))
                        {
                            var _uowLabTest = new UnitOfWork<LabTest>(_uowPatient.GetDbContext());

                            foreach (var labTest in labTests)
                            {

                                if (!AppCommonMethod.IsNullObject(labTest))
                                {
                                    if (labTest.LabTestName == CommonStringConstant.CXR && labTest.IsReportGenerated != true)
                                    {
                                        result.IsCXRTestAdvisedInLastVisit = true;
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        result.noOfMonthsPassed = 0;
                    }
                }
            }

            #endregion

            return result;
            //return null;
        }

        #endregion

        #region IPD

        //currently not is use
        public async Task<ViewPatientIpdReceipt> PatientDischargeReceiptForIpdById(Guid input)
        {
            ViewPatientIpdReceipt result = await GetPatientVisitDetailsForIpdRecipt(input);
            return result;
        }

        // Need to be Update when work on IPD see Emergency Refer que
        public async Task<List<ViewPatientIpdQueDto>> GetAllQueForIpd(int? HealthFacilityId)
        {
            var _uowUser = new UnitOfWork<User>(_uowPatient.GetDbContext());
            var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();

            //Doctor Station 
            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());
            //Guid? doctorStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = await _uowPatientOpenVisit.Repository.GetALL(x =>
                        x.HealthFacilityId == HealthFacilityId &&
                        x.IsReferredIpd == true &&
                        x.IsAdmittedInIpd == false
                        )
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.IpdDepartmentLookupId == dbUser.DepartmentId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.IpdSectionLookupId == dbUser.SectionId)
                .Include(x => x.Patient)
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientIpdQueDto
                    {
                        PatientVisitId = y.PatientOpenVisitId,
                        PatientId = y.PatientId,
                        TokenNo = y.TokenNo,
                        Mrno = y.Patient!.Mrno,
                        CNIC = y.Patient.Cnic,
                        MobileNo = y.Patient.MobileNo,
                        FirstName = y.Patient.FirstName,
                        LastName = y.Patient.LastName,
                        FullName = y.Patient.FullName,
                        Age = y.Patient.Age,
                        Dob = y.Patient.Dob,
                        ProvinceId = y.Patient.ProvinceId,
                        ProvinceName = y.Patient.Province.Name,
                        DivisionId = y.Patient.DivisionId,
                        DivisionName = y.Patient.Division.Name,
                        DistrictId = y.Patient.DistrictId,
                        DistrictName = y.Patient.District.Name,
                        TehsilId = y.Patient.TehsilId,
                        TehsilName = y.Patient.Tehsil.Name,
                        permanentAddress = y.Patient.ParmanentAddress,
                        Gender = y.Patient.GenderProfile!.Name,
                        GenderProfileId = y.Patient.GenderProfile!.ProfileId,
                        RelationProfileId = y.Patient.RelationProfile!.ProfileId,
                        DepartmentName = y.DepartementLookup!.Name,
                        SectionName = y.SectionLookup!.Name,

                        IsAdmittedInIPD = y.IsAdmittedInIpd,
                        IsReferredIpd = y.IsReferredIpd,
                        IpdDepartmentLookupId = y.IpdDepartmentLookupId,
                        IpdDepartmentLookupName = y.IpdDepartmentLookup!.Name,
                        IpdSectionLookupId = y.IpdSectionLookupId,
                        IpdSectionLookupName = y.IpdSectionLookup!.Name,
                        IpdReferredBy = y.IpdReferredBy,
                        IpdReferredByName = y.IpdReferredByNavigation!.FullName,
                        IpdReferredByDepartmentLookupId = y.IpdReferredByDepartmentLookupId,
                        IpdReferredByDepartmentLookupName = y.IpdReferredByDepartmentLookup!.Name,
                        IpdReferredBySectionLookupId = y.IpdReferredBySectionLookupId,
                        IpdReferredBySectionLookupName = y.IpdReferredBySectionLookup!.Name
                    }).ToListAsync();

            return _mapper.Map<List<ViewPatientIpdQueDto>>(responseObj);
        }

        public async Task<List<ViewPatientIpdQueDto>> GetAllReferredQue(int? HealthFacilityId)
        {
            var _uowUser = new UnitOfWork<User>(_uowPatient.GetDbContext());
            var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();

            //Doctor Station 
            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());
            //Guid? doctorStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = await _uowPatientOpenVisit.Repository.GetALL(x =>
                        x.HealthFacilityId == HealthFacilityId &&
                        x.IsReferredIpd == true &&
                        x.IsAdmittedInIpd == false
                        )
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.IpdDepartmentLookupId == dbUser.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.IpdSectionLookupId == dbUser.SectionId)
                .Include(x => x.Patient)
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientIpdQueDto
                    {
                        PatientVisitId = y.PatientOpenVisitId,
                        PatientId = y.PatientId,
                        TokenNo = y.TokenNo,
                        Mrno = y.Patient!.Mrno,
                        CNIC = y.Patient.Cnic,
                        MobileNo = y.Patient.MobileNo,
                        FirstName = y.Patient.FirstName,
                        LastName = y.Patient.LastName,
                        FullName = y.Patient.FullName,
                        Age = y.Patient.Age,
                        Dob = y.Patient.Dob,
                        ProvinceId = y.Patient.ProvinceId,
                        ProvinceName = y.Patient.Province.Name,
                        DivisionId = y.Patient.DivisionId,
                        DivisionName = y.Patient.Division.Name,
                        DistrictId = y.Patient.DistrictId,
                        DistrictName = y.Patient.District.Name,
                        TehsilId = y.Patient.TehsilId,
                        TehsilName = y.Patient.Tehsil.Name,
                        permanentAddress = y.Patient.ParmanentAddress,
                        Gender = y.Patient.GenderProfile!.Name,
                        GenderProfileId = y.Patient.GenderProfile!.ProfileId,
                        RelationProfileId = y.Patient.RelationProfile!.ProfileId,
                        DepartmentName = y.DepartementLookup!.Name,
                        SectionName = y.SectionLookup!.Name,

                        IsAdmittedInIPD = y.IsAdmittedInIpd,
                        IsReferredIpd = y.IsReferredIpd,
                        IpdDepartmentLookupId = y.IpdDepartmentLookupId,
                        IpdDepartmentLookupName = y.IpdDepartmentLookup!.Name,
                        IpdSectionLookupId = y.IpdSectionLookupId,
                        IpdSectionLookupName = y.IpdSectionLookup!.Name,
                        IpdReferredBy = y.IpdReferredBy,
                        IpdReferredByName = y.IpdReferredByNavigation!.FullName,
                        IpdReferredByDepartmentLookupId = y.IpdReferredByDepartmentLookupId,
                        IpdReferredByDepartmentLookupName = y.IpdReferredByDepartmentLookup!.Name,
                        IpdReferredBySectionLookupId = y.IpdReferredBySectionLookupId,
                        IpdReferredBySectionLookupName = y.IpdReferredBySectionLookup!.Name,
                        Patient = _mapper.Map<ViewPatientDto>(y.Patient)
                    }).ToListAsync();

            return _mapper.Map<List<ViewPatientIpdQueDto>>(responseObj);
        }

        #endregion

        #region Drug Addict

        public async Task<VerifiedPatientDataFromNADRADTO> GetVerifiedPatientDataFromNADRAForDeadBody(Guid PatientId)
        {
            var _uowPatientNadraResponse = new UnitOfWork<PatientNadraResponse>(_uowPatient.GetDbContext());
            var patientNadraResponse = await _uowPatientNadraResponse.Repository.GetALL(x => x.PatientId == PatientId && x.TransactionId != null).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(patientNadraResponse))
                throw new UserFriendlyException(CommonMessageConstant.VerificationRequestNotGeneratedOrRecordNotFound);

            var _uowPatientNadraInfoResponse = new UnitOfWork<PatientNadraInfoResponse>(_uowPatient.GetDbContext());
            var patientNadraInfoResponse = await _uowPatientNadraInfoResponse.Repository.GetALL(x => x.TransactionId == patientNadraResponse.TransactionId).OrderByDescending(x => x.RequestCount).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(patientNadraInfoResponse))
            {
                if (!AppCommonMethod.IsNullObject(patientNadraInfoResponse.IsNadraResponseBack))
                {
                    if ((bool)patientNadraInfoResponse.IsNadraResponseBack)
                    {
                        return _mapper.Map<VerifiedPatientDataFromNADRADTO>(patientNadraInfoResponse);
                    }
                }
            }


            var response = await _verifiedPatientDataFromNADRAService.GetVerifiedPatientDataFromNADRA(patientNadraResponse.TransactionId);

            PatientNadraInfoResponse PatientNadraInfoResponse = _mapper.Map<DbModel.PatientNadraInfoResponse>(response);
            PatientNadraInfoResponse.PatientId = patientNadraResponse.PatientId;
            PatientNadraInfoResponse.PatientNadraResponseId = patientNadraResponse.PatientNadraResponseId;
            FillEntityVerifiedPatientDataFromNADRA(PatientNadraInfoResponse);



            //var _uowPatientNadraInfoResponse = new UnitOfWork<PatientNadraInfoResponse>(_uowPatient.GetDbContext());
            //VerifiedPatientDataFromNADRADTO response = new VerifiedPatientDataFromNADRADTO();
            //response.name = "آریز جاوید";
            //response.citizenNumber = "3520285886773";
            //response.currentAddress = "محلہ 3بیگم روڈ، نزد مزنگ اڈا،لاہور";
            //response.permanentAddress = "محلہ 3بیگم روڈ، نزد مزنگ اڈا،لاہور";
            //response.transactionId = "700101001IDVER250723122455650";
            //response.message = "Verified";
            //response.code = "1";
            //response.requestId = "26";

            if (response.code != null)
            {

                if (response.code != CommonMessageConstant.IdentificationIsInProgress)
                {
                    PatientNadraInfoResponse.IsNadraResponseBack = true;
                }


                if (response.citizenNumber != null)
                {
                    if (!response.citizenNumber.Contains("-"))
                    {
                        response.citizenNumber = response.citizenNumber.Substring(0, 5) + "-" + response.citizenNumber.Substring(5);
                        response.citizenNumber = response.citizenNumber.Substring(0, 13) + "-" + response.citizenNumber.Substring(13);
                    }

                }

                var _uowMortaury = new UnitOfWork<UnknownPatient>(_uowPatient.GetDbContext());
                var deadBodyData = await _uowMortaury.Repository.GetALL(x => x.Cnic == response.citizenNumber).FirstOrDefaultAsync();

                //if (!AppCommonMethod.IsNullObject(deadBodyData))
                //    throw new UserFriendlyException(CommonMessageConstant.CNICAlreadyExists);

                if (!AppCommonMethod.IsNullObject(deadBodyData))
                {
                    PatientNadraInfoResponse.Message = "This Person is Already Verified Form NADRA With Cnic " + deadBodyData.Cnic;
                    await _uowPatientNadraInfoResponse.Repository.Insert(PatientNadraInfoResponse);
                    await _uowPatientNadraInfoResponse.Save();
                    return response;
                }
                else
                {
                    await _uowPatientNadraInfoResponse.Repository.Insert(PatientNadraInfoResponse);
                    await _uowPatientNadraInfoResponse.Save();
                }

                if (response.code == CommonMessageConstant.Verified)
                {


                    //var deadBodyData = _uowMortaury.Repository.GetALL(x => x.UnknownPatientId == PatientId).FirstOrDefaultAsync();

                    //if (AppCommonMethod.IsNullObject(deadBodyData))
                    //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);





                    deadBodyData = await _uowMortaury.Repository.GetALL(x => x.UnknownPatientId == patientNadraResponse.PatientId).FirstOrDefaultAsync();


                    deadBodyData.Cnic = response.citizenNumber;
                    deadBodyData.FirstName = response.name;
                    deadBodyData.LastName = response.name;
                    deadBodyData.FullName = response.name;
                    deadBodyData.TemporaryAddress = response.currentAddress;
                    deadBodyData.ParmanentAddress = response.permanentAddress;
                    FillEntityMortaury(deadBodyData);
                    _uowMortaury.Repository.Update(deadBodyData);
                    await _uowMortaury.CommitAsync();




                    //DbModel.Patient patientObj = await _uowPatient.Repository.GetALL(x => x.Cnic == response.citizenNumber).FirstOrDefaultAsync();

                    //if (!AppCommonMethod.IsNullObject(patientObj))
                    //    throw new UserFriendlyException(CommonMessageConstant.CNICAlreadyExists);

                    //patientObj = _mapper.Map<DbModel.Patient>(deadBodyData);


                    ////patientObj.IsPatientUnknown = false;
                    //CreateOrEditPatientWithVisitDto input = _mapper.Map<CreateOrEditPatientWithVisitDto>(patientObj);

                    //patientObj.Mrno = await GenerateMRNNo(input);

                    //FillEntity(patientObj);

                    //await _uowPatient.Repository.Insert(patientObj);
                    //await _uowPatient.Save();
                }
            }
            else
            {
                await _uowPatientNadraInfoResponse.Repository.Insert(PatientNadraInfoResponse);
                await _uowPatientNadraInfoResponse.Save();
            }

            return response;
        }


        public async Task<VerifiedPatientDataFromNADRADTO> GetVerifiedPatientDataFromNADRA(Guid PatientId)
        {

            var _uowPatientNadraResponse = new UnitOfWork<PatientNadraResponse>(_uowPatient.GetDbContext());
            var patientNadraResponse = await _uowPatientNadraResponse.Repository.GetALL(x => x.PatientId == PatientId && x.TransactionId != null).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(patientNadraResponse))
                throw new UserFriendlyException(CommonMessageConstant.VerificationRequestNotGeneratedOrRecordNotFound);

            var _uowPatientNadraInfoResponse = new UnitOfWork<PatientNadraInfoResponse>(_uowPatient.GetDbContext());
            var patientNadraInfoResponse = await _uowPatientNadraInfoResponse.Repository.GetALL(x => x.TransactionId == patientNadraResponse.TransactionId).OrderByDescending(x => x.RequestCount).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(patientNadraInfoResponse))
            {
                if (!AppCommonMethod.IsNullObject(patientNadraInfoResponse.IsNadraResponseBack))
                {
                    if ((bool)patientNadraInfoResponse.IsNadraResponseBack)
                    {
                        return _mapper.Map<VerifiedPatientDataFromNADRADTO>(patientNadraInfoResponse);
                    }
                }
            }


            var response = await _verifiedPatientDataFromNADRAService.GetVerifiedPatientDataFromNADRA(patientNadraResponse.TransactionId);


            //var _uowPatientNadraInfoResponse = new UnitOfWork<PatientNadraInfoResponse>(_uowPatient.GetDbContext());
            //VerifiedPatientDataFromNADRADTO response = new VerifiedPatientDataFromNADRADTO();
            //response.name = "انیب بابر";
            //response.citizenNumber = "3310073324507";
            //response.currentAddress = "مکان نمبر p-23-22،گلی نمبر 6،محلہ حسیب شہید کالونی،فیصل آباد،تحصیل فیصل آباد سٹی،ضلع فیصل آباد";
            //response.permanentAddress = "مکان نمبر p-23-22،گلی نمبر 6،محلہ حسیب شہید کالونی،فیصل آباد،تحصیل فیصل آباد سٹی،ضلع فیصل آباد";
            //response.transactionId = "700101001IDVER200723113942341";
            //response.message = "Verified";
            //response.code = "3";
            //response.requestId = "26";


            if (response.code != null)
            {
                if (response.citizenNumber != null)
                {
                    if (!response.citizenNumber.Contains("-"))
                    {
                        response.citizenNumber = response.citizenNumber.Substring(0, 5) + "-" + response.citizenNumber.Substring(5);
                        response.citizenNumber = response.citizenNumber.Substring(0, 13) + "-" + response.citizenNumber.Substring(13);
                    }

                }


                PatientNadraInfoResponse PatientNadraInfoResponse = _mapper.Map<DbModel.PatientNadraInfoResponse>(response);
                PatientNadraInfoResponse.PatientId = patientNadraResponse.PatientId;
                PatientNadraInfoResponse.PatientNadraResponseId = patientNadraResponse.PatientNadraResponseId;
                FillEntityVerifiedPatientDataFromNADRA(PatientNadraInfoResponse);

                if (response.code != CommonMessageConstant.IdentificationIsInProgress)
                {
                    PatientNadraInfoResponse.IsNadraResponseBack = true;

                }


                var patientObj = await _uowPatient.Repository.GetALL(x => x.Cnic == response.citizenNumber).FirstOrDefaultAsync();

                if (!AppCommonMethod.IsNullObject(patientObj))
                {
                    PatientNadraInfoResponse.Message = response.message;
                    await _uowPatientNadraInfoResponse.Repository.Insert(PatientNadraInfoResponse);
                    await _uowPatientNadraInfoResponse.Save();

                    var patientData = await _uowPatient.Repository.GetALL(x => x.PatientId == patientNadraResponse.PatientId).FirstOrDefaultAsync();

                    patientData.Cnic = response.citizenNumber;
                    patientData.FirstName = response.name;
                    patientData.LastName = response.name;
                    patientData.FullName = response.name;
                    patientData.NameOfCnicHolder = response.name;
                    patientData.TemporaryAddress = response.currentAddress;
                    patientData.ParmanentAddress = response.permanentAddress;
                    patientData.IsVerifiedFromNadra = true;
                    patientData.OldPatientId = patientObj.PatientId;
                    FillEntity(patientData);
                    _uowPatient.Repository.Update(patientData);
                    await _uowPatient.CommitAsync();

                    response.code = CommonMessageConstant.Verified;

                    return response;
                }
                else
                {
                    await _uowPatientNadraInfoResponse.Repository.Insert(PatientNadraInfoResponse);
                    await _uowPatientNadraInfoResponse.Save();
                }
                //throw new UserFriendlyException(CommonMessageConstant.CNICAlreadyExists);




                if (response.code == CommonMessageConstant.Verified)
                {
                    var patientData = await _uowPatient.Repository.GetALL(x => x.PatientId == patientNadraResponse.PatientId).FirstOrDefaultAsync();

                    if (AppCommonMethod.IsNullObject(patientData))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


                    patientData.Cnic = response.citizenNumber;
                    patientData.FirstName = response.name;
                    patientData.LastName = response.name;
                    patientData.FullName = response.name;
                    patientData.NameOfCnicHolder = response.name;
                    patientData.TemporaryAddress = response.currentAddress;
                    patientData.ParmanentAddress = response.permanentAddress;
                    patientData.IsVerifiedFromNadra = true;
                    FillEntity(patientData);
                    _uowPatient.Repository.Update(patientData);
                    await _uowPatient.CommitAsync();



                    var _uowPatientNadraRequest = new UnitOfWork<PatientNadraRequest>(_uowPatient.GetDbContext());

                    var patientVerification = await _uowPatientNadraRequest.Repository.GetALL(x => x.PatientNadraRequestId == patientNadraResponse.PatientNadraRequestId).FirstOrDefaultAsync();

                    patientVerification.IsPendingVerification = false;
                    //_uowPatientNadraRequest.Repository.Update(patientVerification!);
                    await _uowPatientNadraRequest.Save();

                }
            }
            return response;
        }


        public async Task<NadraResponseDTO> VerifyWithNADRA(UnknownPatientDto UnknownPatientDto)
        {
            NadraVerificationDTO nadraVerificationDTO = new NadraVerificationDTO();
            if (UnknownPatientDto.PatientRole == CommonStringConstant.DrugAddict)
            {
                nadraVerificationDTO.Id = UnknownPatientDto.PatientId.ToString();
            }
            else if (UnknownPatientDto.PatientRole == CommonStringConstant.Mortuary)
            {
                nadraVerificationDTO.Id = UnknownPatientDto.UnknownPatientId.ToString();
            }
            var gender = await _uowProfile.Repository.GetById(UnknownPatientDto?.GenderProfileId);
            if (!AppCommonMethod.IsNullObject(gender))
            {
                if (gender.ShortName == CommonConstants.MALE)
                    nadraVerificationDTO.Gender = "1";
                else if (gender.ShortName == CommonConstants.FMALE)
                    nadraVerificationDTO.Gender = "2";
            }
            if (UnknownPatientDto.PatientRole == CommonStringConstant.DrugAddict)
                nadraVerificationDTO.RequestCode = "1";
            else if (UnknownPatientDto.PatientRole == CommonStringConstant.Mortuary)
                nadraVerificationDTO.RequestCode = "2";


            var _uowHealthFacility = new UnitOfWork<HealthFacility>(_uowPatient.GetDbContext());

            var healthFacility = await _uowHealthFacility.Repository.GetALL(x => x.HealthFacilityId == UnknownPatientDto.HealthFacilityId).Select(x => x.Code).FirstOrDefaultAsync();

            if (string.IsNullOrEmpty(healthFacility))
                nadraVerificationDTO.LocationId = "0340060010020110002";
            else
                nadraVerificationDTO.LocationId = healthFacility;


            var _uowPatientNadraRequest = new UnitOfWork<PatientNadraRequest>(_uowPatient.GetDbContext());
            var patientReqObj = new PatientNadraRequest();

            if (UnknownPatientDto.PatientRole == CommonStringConstant.DrugAddict)
            {
                patientReqObj = await _uowPatientNadraRequest.Repository.GetALL(x => x.PatientId == UnknownPatientDto.PatientId).FirstOrDefaultAsync();
            }
            else if (UnknownPatientDto.PatientRole == CommonStringConstant.Mortuary)
            {
                patientReqObj = await _uowPatientNadraRequest.Repository.GetALL(x => x.PatientId == UnknownPatientDto.UnknownPatientId).FirstOrDefaultAsync();
            }


            if (!AppCommonMethod.IsNullObject(patientReqObj))
            {
                nadraVerificationDTO.RequestId = patientReqObj.RequestId.ToString();
            }
            else
            {
                var requesId = await _uowPatientNadraRequest.Repository.GetALL().OrderByDescending(x => x.RequestId).Select(x => x.RequestId).FirstOrDefaultAsync();

                if (requesId == null)
                    nadraVerificationDTO.RequestId = "21";
                else
                    nadraVerificationDTO.RequestId = (requesId + 1).ToString();
            }

            PatientNadraRequest PatientNadraRequest = _mapper.Map<DbModel.PatientNadraRequest>(nadraVerificationDTO);
            PatientNadraRequest.FingerPrintFormate = nadraVerificationDTO.fingerprintFormat;
            if (UnknownPatientDto.PatientRole == CommonStringConstant.DrugAddict)
            {
                PatientNadraRequest.PatientId = UnknownPatientDto.PatientId;
            }
            else if (UnknownPatientDto.PatientRole == CommonStringConstant.Mortuary)
            {
                PatientNadraRequest.PatientId = UnknownPatientDto.UnknownPatientId;
            }


            FillEntityPatientNadraRequest(PatientNadraRequest);

            DbModel.PatientNadraRequest responseObj = await _uowPatientNadraRequest.Repository.Insert(PatientNadraRequest);

            var response = await _requestForNadraVerfication.SentRequestForVerfication(nadraVerificationDTO);

            if (response.code == null)
                throw new UserFriendlyException(CommonMessageConstant.APIResponseError);



            var _uowPatientNadraResponse = new UnitOfWork<PatientNadraResponse>(_uowPatient.GetDbContext());
            PatientNadraResponse PatientNadraResponse = _mapper.Map<DbModel.PatientNadraResponse>(response);
            if (UnknownPatientDto.PatientRole == CommonStringConstant.DrugAddict)
            {
                PatientNadraResponse.PatientId = UnknownPatientDto.PatientId;
            }
            else if (UnknownPatientDto.PatientRole == CommonStringConstant.Mortuary)
            {
                PatientNadraResponse.PatientId = UnknownPatientDto.UnknownPatientId;
            }


            if (Int32.Parse(response.code) == 200)
            {
                PatientNadraResponse.PatientNadraRequestId = responseObj.PatientNadraRequestId;
                PatientNadraRequest.IsPendingVerification = true;
                PatientNadraRequest.IsTransactionSaveSuccessfully = true;
                FillEntityPatientNadraResponse(PatientNadraResponse);
                DbModel.PatientNadraResponse responseObj2 = await _uowPatientNadraResponse.Repository.Insert(PatientNadraResponse);
                await _uowPatientNadraRequest.Save();
            }
            else
            {
                PatientNadraResponse.PatientNadraRequestId = responseObj.PatientNadraRequestId;
                PatientNadraResponse.ErrorResponse = JsonSerializer.Serialize(response);
                PatientNadraRequest.IsPendingVerification = false;
                PatientNadraRequest.IsTransactionSaveSuccessfully = false;
                FillEntityPatientNadraResponse(PatientNadraResponse);
                DbModel.PatientNadraResponse responseObj2 = await _uowPatientNadraResponse.Repository.Insert(PatientNadraResponse);
                await _uowPatientNadraRequest.Save();
            }
            await _uowPatientNadraRequest.Save();
            return response;

        }



        public async Task<ViewPagerDto<UnknownPatientsListView>> GetUnknownPatientsList(FilterPatientVisitDto filter)
        {
            var tokenUserId = _tokenService.GetUserId();

            var UnknowPnatientList = _uowPatient.GetDbContext().UnknownPatientsListViews.Where(x => x.IsPatientUnknown == true && x.CreatedBy == tokenUserId)
                .Select(x => new UnknownPatientsListView
                {
                    PatientId = x.PatientId,
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    ProvinceId = x.ProvinceId,
                    IsPatientUnknown = x.IsPatientUnknown,
                    GenderProfileId = x.GenderProfileId,
                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    CreatedBy = x.CreatedBy!,
                    CreatedOn = x.CreatedOn,
                    HealthFacilityId = x.HealthFacilityId,
                    IsPendingVerification = x.IsPendingVerification,
                    IsTransactionSaveSuccessfully = x.IsTransactionSaveSuccessfully,
                    TransctionSaveTime = x.TransctionSaveTime

                }).OrderByDescending(x => x.CreatedOn);



            var pagedList = await PagedListDto<UnknownPatientsListView>.ToPagedListAsync(
                 UnknowPnatientList,
                 filter.PageNumber,
                 filter.PageSize
                 );

            var responseObject = new ViewPagerDto<UnknownPatientsListView>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;

        }


        public async Task<ViewPagerDto<DeadBodiesListView>> GetMortauryPatientsList(FilterPatientVisitDto filter)
        {
            var _uowMortaury = new UnitOfWork<UnknownPatient>(_uowPatient.GetDbContext());
            var tokenUserId = _tokenService.GetUserId();
            var UnknowPnatientList = _uowMortaury.GetDbContext().DeadBodiesListViews.Where(x => x.CreatedBy == tokenUserId)
                .Select(x => new DeadBodiesListView
                {
                    UnknownPatientId = x.UnknownPatientId,
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    ProvinceId = x.ProvinceId,
                    GenderProfileId = x.GenderProfileId,
                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    CreatedBy = x.CreatedBy!,
                    CreatedOn = x.CreatedOn,
                    HealthFacilityId = x.HealthFacilityId,
                    IsTransactionSaveSuccessfully = x.IsTransactionSaveSuccessfully,
                    TransctionSaveTime = x.TransctionSaveTime
                }).OrderByDescending(x => x.TransctionSaveTime);



            var pagedList = await PagedListDto<DeadBodiesListView>.ToPagedListAsync(
                 UnknowPnatientList,
                 filter.PageNumber,
                 filter.PageSize
                 );

            var responseObject = new ViewPagerDto<DeadBodiesListView>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;

            return null;

        }
        #endregion

        #region Tb



        public async Task CheckPatientDRTBReferedStatus(CreateOrEditPatientWithVisitDto input)
        {

            var _uowTbPatietDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
            var tbPatData = await _uowTbPatietDetails.Repository.GetALL(x => x.PatientId == input.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(tbPatData))
            {
                if (!AppCommonMethod.IsNullObject(tbPatData?.IsReferToDrtb))
                {
                    if ((bool)tbPatData?.IsReferToDrtb)
                    {
                        if (input.DRTBPatientStatus == CommonStringConstant.WalkIn)
                        {
                            throw new UserFriendlyException(CommonMessageConstant.PatientIsReferedToDRTB);
                        }
                    }
                }
            }
            else
            {
                if (input.DRTBPatientStatus == CommonStringConstant.Refered)
                {
                    throw new UserFriendlyException(CommonMessageConstant.PatientIsNotReferedToDRTB);
                }
            }
        }


        public async Task<ViewPagerDto<ViewTbPatientsListDTO>> GetTbPatientsList(FilterPatientVisitDto filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _uowPatients = new UnitOfWork<PatientDiagnose>(_uowPatientOpenVisit.GetDbContext());

            var finalList = _uowPatients.GetDbContext().ViewTbPatientsLists
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.FullName!.ToLower().StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.MobileNo!.ToLower().StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedById.ToString()!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.MinAge), x => x.Age >= filter.MinAge)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.MaxAge), x => x.Age <= filter.MaxAge)
                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(filter.GenderId), x => x.GenderId == filter.GenderId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x =>
                new ViewTbPatientsListDTO
                {
                    PatientId = x.PatientId,
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    PatientProvinceId = x.ProvinceId,

                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    CreatedBy = x.CreatedBy!,
                    CreatedOn = x.CreatedOn
                });

            var pagedList = await PagedListDto<ViewTbPatientsListDTO>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewTbPatientsListDTO>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }





        public async Task<ViewPagerDto<ViewEyeBlindnesspatient>> GetEyeBlindnessPatientList(FilterPatientVisitDto filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _uowPatients = new UnitOfWork<ViewEyeBlindnesspatient>(_uowPatientOpenVisit.GetDbContext());

            var finalList = _uowPatients.GetDbContext().ViewEyeBlindnesspatients
                .WhereIf(!(AppCommonMethod.IsNullorZeroInt(filter.ProvinceId)), x => x.Createdby == _tokenService.GetUserId())
                .OrderByDescending(x => x.CreatedOn)
                .Select(x =>
                new ViewEyeBlindnesspatient
                {
                    FullName = x.FullName,
                    Age = x.Age,
                    ComorbidityBy = x.ComorbidityBy,
                    DateOfInocvlation = x.DateOfInocvlation,
                    ConsultantName = x.ConsultantName,
                    StatusOfVision = x.StatusOfVision,
                    Recovery = x.Recovery,
                    HealthFacility = x.HealthFacility,
                    OtherHealthFacility = x.OtherHealthFacility,
                    InjectedHospital = x.InjectedHospital,
                    CreatedOn = x.CreatedOn
                });

            var pagedList = await PagedListDto<ViewEyeBlindnesspatient>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewEyeBlindnesspatient>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }





        public async Task<ViewPagerDto<ViewEyeBlindnesspatient>> GetExcelExportEyeBlindnessPatientList(FilterPatientVisitDto filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _uowPatients = new UnitOfWork<ViewEyeBlindnesspatient>(_uowPatientOpenVisit.GetDbContext());

            var finalList = _uowPatients.GetDbContext().ViewEyeBlindnesspatients
                .WhereIf(!(AppCommonMethod.IsNullorZeroInt(filter.ProvinceId)), x => x.Createdby == _tokenService.GetUserId())
                .OrderByDescending(x => x.CreatedOn)
                .Select(x =>
                new ViewEyeBlindnesspatient
                {
                    FullName = x.FullName,
                    Age = x.Age,
                    ComorbidityBy = x.ComorbidityBy,
                    DateOfInocvlation = x.DateOfInocvlation,
                    ConsultantName = x.ConsultantName,
                    StatusOfVision = x.StatusOfVision,
                    Recovery = x.Recovery,
                    //HealthFacility = x.HealthFacility,
                    OtherHealthFacility = x.OtherHealthFacility,
                    InjectedHospital = x.InjectedHospital,
                    CreatedOn = x.CreatedOn
                });

            var pagedList = await PagedListDto<ViewEyeBlindnesspatient>.ToPagedListAsync(
                   finalList,
                  1,
                   9999
                   );

            var responseObject = new ViewPagerDto<ViewEyeBlindnesspatient>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }


        public async Task<ViewPagerDto<FilterTbPatientContactDto>> GetTbPatientContactListByPatientId(FilterTbPatientContactDto filter)
        {
            var _uowPatientConatctDetails = new UnitOfWork<PatientContactDetail>(_uowPatient.GetDbContext());
            var tbPatientContacts = _uowPatientConatctDetails.Repository.GetALL(x => x.PatientId == filter.PatientId)
               .WhereIf(!string.IsNullOrEmpty(filter.ContactName), x => x.ContactName!.ToLower().StartsWith(filter.ContactName!))
               .WhereIf(!string.IsNullOrEmpty(filter.ContactName), x => x.ContactName!.ToLower().StartsWith(filter.ContactName!))
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.Age), x => x.Age >= filter.Age)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartmentLookupId == filter.DepartmentId)
               .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
               .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
               .OrderByDescending(x => x.CreatedOn)
               .Select(x =>
               new FilterTbPatientContactDto
               {
                   ContactId = x.ContactId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,
                   ContactName = x.ContactName,
                   ContactNo = x.ContactNo,
                   ProvinceId = x.ProvinceId,
                   DivisionId = x.DivisionId,
                   DistrictId = x.DistrictId,
                   TehsilId = x.TehsilId,
                   Age = x.Age,
                   Relation = x.Relation,
                   DepartmentId = x.DepartmentLookupId,
                   SectionId = x.SectionLookupId,
                   isSputumCollected = x.IsSputumCollected,
                   CollectedBy = x.CollectedBy
               });

            var pagedList = await PagedListDto<FilterTbPatientContactDto>.ToPagedListAsync(
                   tbPatientContacts,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<FilterTbPatientContactDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };
            return responseObject;
        }


        public async Task<bool> UpdateSputumById(Guid ContactId)
        {
            var _uowPatientConatctDetails = new UnitOfWork<PatientContactDetail>(_uowPatient.GetDbContext());
            var dbObj = await _uowPatientConatctDetails.Repository.GetById(ContactId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityUpdate(dbObj);
            dbObj.IsSputumCollected = true;
            _uowPatientConatctDetails.Repository.Update(dbObj!);
            await _uowPatientConatctDetails.CommitAsync();
            return true;
        }


        private async Task<bool> CreatedDiagnoseWithTestForCallCenter(DbModel.Patient obj)
        {
            var _uowLabTest = new UnitOfWork<LabTest>(_uowPatient.GetDbContext());

            //var ssmTest = await _uowLabTest.Repository.GetALL(x => x.Name == CommonStringConstant.SSM).FirstOrDefaultAsync();

            var testList = await _uowLabTest.Repository.GetALL(x => x.Name == CommonStringConstant.SSM || x.Name == CommonStringConstant.XPert).ToListAsync();

            if (AppCommonMethod.IsNullOrEmptyList<LabTest>(testList))
                return false;

            CreateOrEditPatientDiagnoseWithPrescriptionDto objPatientDiagnoseWithPrescriptionDto = new CreateOrEditPatientDiagnoseWithPrescriptionDto();

            objPatientDiagnoseWithPrescriptionDto.PatientDiagnoseId = null;
            objPatientDiagnoseWithPrescriptionDto.PatientId = obj.PatientId;
            objPatientDiagnoseWithPrescriptionDto.PatientVisitId = obj.PatientOpenVisits.FirstOrDefault()!.PatientOpenVisitId;
            objPatientDiagnoseWithPrescriptionDto.IsVisitClose = true;
            objPatientDiagnoseWithPrescriptionDto.IsFromCallCenter = obj.IsFromCallCenter;

            foreach (var test in testList)
            {
                objPatientDiagnoseWithPrescriptionDto.PatientLabTests.Add(new CreateOrEditPatientLabTestDto
                {
                    LabDepartmentProfileId = test.DepartmentProfileId,
                    LabTestId = test.LabTestId,
                    PatientId = test.DepartmentProfileId,
                    PatientVisitId = test.DepartmentProfileId,
                    IsFromCallCenter = obj.IsFromCallCenter
                });
            }

            var response = await _patientDiagnoseService.CreateOrEditPatientDiagnoseWithPrescription(objPatientDiagnoseWithPrescriptionDto);

            return true;

        }

        #endregion

        #region HCP
        public async Task<ViewPagerDto<LostOfFollowupDto>> GetLostOfFollowupPatients(FilterPatientVisitDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetLostOfFollowupPatient", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(loginUser.HealthFacilityId))
                    {
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", loginUser.HealthFacilityId);
                    }
                    else
                    {
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", 0);
                    }
                    //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                    //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
                    ////sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    //    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    //    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    //    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                    //if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    //    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                    //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);


                    //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    //    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    //    sqlComm.Parameters.AddWithValue("@FilterString", filter.SearchString.Trim());

                    //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                    //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    //if (!string.IsNullOrEmpty(filter.listType))
                    //    sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                    //if (!string.IsNullOrEmpty(filter.User))
                    //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));




                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<LostOfFollowupDto> lst = ds.Tables[0].ToList<LostOfFollowupDto>();

                    var pagedList = await Task.Run(() => PagedListDto<LostOfFollowupDto>.ToPagedList(
                        lst.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<LostOfFollowupDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<LostOfFollowupDto>>(pagedList)
                    };


                    //var pagedList = await PagedListDto<LostOfFollowupDto>.ToPagedListAsync(
                    //           //lst.AsQueryable(),
                    //           //lst.ToList().AsQueryable(),
                    //           ds.Tables[0].ToList<LostOfFollowupDto>().AsQueryable(),
                    //           //lst.AsQueryable(),
                    //           filter.PageNumber,
                    //           filter.PageSize
                    //           );

                    //var responseObject = new ViewPagerDto<LostOfFollowupDto>
                    //{
                    //    TotalCount = pagedList.TotalCount,
                    //    PageSize = pagedList.PageSize,
                    //    CurrentPage = pagedList.CurrentPage,
                    //    TotalPages = pagedList.TotalPages,
                    //    HasNext = pagedList.HasNext,
                    //    HasPrevious = pagedList.HasPrevious,
                    //    //TotalCount = 1,
                    //    //PageSize = 1,
                    //    //CurrentPage = 1,
                    //    //TotalPages = 1,
                    //    //HasNext = false,
                    //    //HasPrevious = false,
                    //    List = lst
                    //};

                    return responseObject;

                    //return lst[0];

                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }



            //var pagedList = await PagedListDto<ViewSampleCollectedConsignmentLilst>.ToPagedListAsync(
            //       IQueryableList,
            //       filter.PageNumber,
            //       filter.PageSize
            //       );

            //foreach (var item in pagedList)
            //{
            //    if (!string.IsNullOrEmpty(item.SampleRejectedReason))
            //    {
            //        var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

            //        item.SampleRejectedReasonName = string.Join(',', ReasonsList
            //            .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
            //    }
            //}
            //var responseObject = new ViewPagerDto<ViewSampleCollectedConsignmentLilst>
            //{
            //    TotalCount = pagedList.TotalCount,
            //    PageSize = pagedList.PageSize,
            //    CurrentPage = pagedList.CurrentPage,
            //    TotalPages = pagedList.TotalPages,
            //    HasNext = pagedList.HasNext,
            //    HasPrevious = pagedList.HasPrevious,
            //    List = pagedList
            //};

            //return responseObject;
            //return null;
        }
        public async Task<ViewPagerDto<SvrPcrPendingPatientsDto>> GetSvrPcrPendingPatients(FilterPatientVisitDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();

            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetSvrPcrPendingPatients", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(loginUser.HealthFacilityId))
                    {
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", loginUser.HealthFacilityId);
                    }
                    else
                    {
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", 0);
                    }



                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<SvrPcrPendingPatientsDto> lst = ds.Tables[0].ToList<SvrPcrPendingPatientsDto>();

                    var pagedList = await Task.Run(() => PagedListDto<SvrPcrPendingPatientsDto>.ToPagedList(
                        lst.AsQueryable(),
                        filter.PageNumber,
                        filter.PageSize));

                    var responseObject = new ViewPagerDto<SvrPcrPendingPatientsDto>
                    {
                        TotalCount = pagedList.TotalCount,
                        PageSize = pagedList.PageSize,
                        CurrentPage = pagedList.CurrentPage,
                        TotalPages = pagedList.TotalPages,
                        HasNext = pagedList.HasNext,
                        HasPrevious = pagedList.HasPrevious,
                        List = _mapper.Map<List<SvrPcrPendingPatientsDto>>(pagedList)
                    };


                    return responseObject;


                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }



            //var pagedList = await PagedListDto<ViewSampleCollectedConsignmentLilst>.ToPagedListAsync(
            //       IQueryableList,
            //       filter.PageNumber,
            //       filter.PageSize
            //       );

            //foreach (var item in pagedList)
            //{
            //    if (!string.IsNullOrEmpty(item.SampleRejectedReason))
            //    {
            //        var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

            //        item.SampleRejectedReasonName = string.Join(',', ReasonsList
            //            .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
            //    }
            //}
            //var responseObject = new ViewPagerDto<ViewSampleCollectedConsignmentLilst>
            //{
            //    TotalCount = pagedList.TotalCount,
            //    PageSize = pagedList.PageSize,
            //    CurrentPage = pagedList.CurrentPage,
            //    TotalPages = pagedList.TotalPages,
            //    HasNext = pagedList.HasNext,
            //    HasPrevious = pagedList.HasPrevious,
            //    List = pagedList
            //};

            //return responseObject;
            //return null;
        }

        #endregion
    }
}
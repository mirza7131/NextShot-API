using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.Common;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Repositories.UOW;
using HMIS.Patient.Service.Common;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using SMSSender.DTO;
using SMSSender;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientLabTestDto;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDiseaseDto;
using System;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseRecordDto;
using HMIS.Patient.Domain.Models.DTO.PatientContactDetailsDTO;
using HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Newtonsoft.Json.Linq;
using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Extensions.Logging;
using System.Data;
using Microsoft.Data.SqlClient;
using CommonDTOs.TBScreeningDTO;
using HMIS.Patient.Domain.Models.DTO.NursingEventDto;
using HMIS.Aggregator.API;
using AppCommonMethods.AppConstants;
using HMIS.Aggregator.API.Models.Dto.EMR;
using HMIS.Patient.Domain.Models.DTO.EMRRequest;
//using DPUruNet;

namespace HMIS.Patient.Service
{
    public class PatientDiagnoseService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly PatientWorkFlowLogService<PatientWorkFlowLog> _patientWorkFlowLogService;
        private readonly PatientVisitFlowService<PatientVisitFlow> _patientVisitFlowService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;
        private readonly UnitOfWork<PatientPrescription> _uowPatientPrescription;
        private readonly UnitOfWork<PatientLabTest> _uowPatientLabTest;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly UnitOfWork<DbModel.GetAllMlcQueDatum> _uowGetAllMlcQueData;
        private readonly UnitOfWork<PatientOpenVisit> _uowPatientOpenVisit;
        private readonly PatientOpenVisitService<PatientOpenVisit> _patientOpenVisitService;
        private readonly UnitOfWork<User> _uowUser;
        private readonly UnitOfWork<HealthFacilityStation> _uowHealthFacilityStation;
        private readonly UnitOfWork<DbModel.Profile> _uowProfile;
        private readonly SMS _smsService;
        private readonly MedicineDispatchService<MedicineDispatch> _MedicineDispatchService;
        private readonly UnitOfWork<MedicineDispatch> _uowMedicineDispatch;
        private readonly NursingEventsService<NursingEvent> _nursingEventsService;
        private readonly MIMSService _mimsService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly bool _isExternallyAdvisedLabTestAreFree;
        private readonly bool _isGynaeOPD;
        private readonly bool _isOnline;
        private readonly EMRService _emrService;
        private string[] measlesDiseaseList;

        #endregion

        #region Constructor

        public PatientDiagnoseService(TokenService tokenService, PatientWorkFlowLogService<PatientWorkFlowLog> patientWorkFlowLogService, UnitOfWork<PatientDiagnose> uowPatientDiagnose, UnitOfWork<PatientPrescription> uowPatientPrescription, UnitOfWork<PatientLabTest> uowPatientLabTest, UnitOfWork<PatientOpenVisit> uowPatientOpenVisit, UnitOfWork<DbModel.Patient> uowPatient, IMapper mapper, UnitOfWork<User> uowUser, UnitOfWork<HealthFacilityStation> uowHealthFacilityStation, UnitOfWork<DbModel.Profile> uowProfile, SMS smsService,
            PatientVisitFlowService<PatientVisitFlow> patientVisitFlowService,
            MedicineDispatchService<MedicineDispatch> medicineDispatchService,
            UnitOfWork<MedicineDispatch> uowMedicineDispatch,
            MIMSService mimsService,
            PatientOpenVisitService<PatientOpenVisit> patientOpenVisitService,
            IConfiguration config,
            UnitOfWork<DbModel.GetAllMlcQueDatum> uowGetAllMlcQueData,
            NursingEventsService<NursingEvent> nursingEventService,
            EMRService emrService
        )
        {
            _tokenService = tokenService;
            _patientWorkFlowLogService = patientWorkFlowLogService;
            _uowPatientDiagnose = uowPatientDiagnose;
            _uowPatient = uowPatient;
            _uowPatientOpenVisit = uowPatientOpenVisit;
            _uowGetAllMlcQueData = uowGetAllMlcQueData;
            _mapper = mapper;
            _uowPatientPrescription = uowPatientPrescription;
            _uowPatientLabTest = uowPatientLabTest;
            _uowUser = uowUser;
            _uowHealthFacilityStation = uowHealthFacilityStation;
            _uowProfile = uowProfile;
            _smsService = smsService;
            _patientVisitFlowService = patientVisitFlowService;
            _MedicineDispatchService = medicineDispatchService;
            _uowMedicineDispatch = uowMedicineDispatch;
            _mimsService = mimsService;
            _emrService = emrService;
            _patientOpenVisitService = patientOpenVisitService;
            _nursingEventsService = nursingEventService;
            _isExternallyAdvisedLabTestAreFree = config.GetValue<bool>("IsExternallyAdvisedLabTestAreFree") ? config.GetValue<bool>("IsExternallyAdvisedLabTestAreFree") : false;
            _isGynaeOPD = config.GetValue<bool>("IsGynaeOPD") ? config.GetValue<bool>("IsGynaeOPD") : false;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;

            _isOnline = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ? true : false;
            measlesDiseaseList = config.GetSection("HealthFacility").GetSection("MeaslesDiseasList").Get<string[]>() ?? new string[0];

        }

        #endregion

        #region CUD Operations

        public async Task<DoctorNotesDTO> CreateDoctorNotes(DoctorNotesDTO input)
        {
            var _uowDoctorNotes = new UnitOfWork<DoctorNote>(_uowPatient.GetDbContext());
            DoctorNote doctorNote = new DoctorNote();
            doctorNote = _mapper.Map<DoctorNote>(input);
            FillEntityDoctorNote(doctorNote);
            await _uowDoctorNotes.Repository.Insert(doctorNote);
            await _uowDoctorNotes.Save();


            var dbUser = TokenService.GetUserLoggedInfo();
            
            if(dbUser.IsDoctor == true)
            {
                CreateOrEditNursingEventDto objNursingEvents = new CreateOrEditNursingEventDto();

                objNursingEvents.PatientId = input.PatientId;
                objNursingEvents.PatientDiagnoseId = input.PatientDiagnoseId;
                objNursingEvents.PatientVisitId = input.PatientVisitId;
                objNursingEvents.Events = CommonPrases.DoctorNotes;

                await _nursingEventsService.CreateEvent(objNursingEvents);
            }

            return _mapper.Map<DoctorNotesDTO>(doctorNote);
        }

        public async Task<CreateOrEditPatientDiagnoseDto> CreateOrEdit(CreateOrEditPatientDiagnoseDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDiagnoseDto> Create(CreateOrEditPatientDiagnoseDto input)
        {
            var obj = _mapper.Map<PatientDiagnose>(input);
            FillEntity(obj);
            PatientDiagnose responseObj = await _uowPatientDiagnose.Repository.Insert(obj);
            await _uowPatientDiagnose.Save();
            return _mapper.Map<CreateOrEditPatientDiagnoseDto>(responseObj);

        }

        private async Task<CreateOrEditPatientDiagnoseDto> Update(CreateOrEditPatientDiagnoseDto input)
        {
            var dbObj = await _uowPatientDiagnose.Repository.GetById(input.PatientDiagnoseId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientDiagnose.Repository.Update(obj!);
            await _uowPatientDiagnose.CommitAsync();
            return _mapper.Map<CreateOrEditPatientDiagnoseDto>(obj);
        }


        public async Task SavePatientStatusBySpeciality(CreateOrEditPatientDiagnoseWithPrescriptionDto input, PatientDiagnose patientDiagnose)
        {
            var _uowPatientStatusBySpeciality = new UnitOfWork<PatientStatusBySpeciality>(_uowPatient.GetDbContext());
            var PatientStatusBySpeciality = new PatientStatusBySpeciality();
            PatientStatusBySpeciality.PatientId = input.PatientId;
            PatientStatusBySpeciality.PatientVisitId = input.PatientVisitId;
            PatientStatusBySpeciality.PatientDiagnoseId = input.PatientDiagnoseId;
            PatientStatusBySpeciality.HealthFacilityId = TokenService.GetUserHfId();
            PatientStatusBySpeciality.FormType = CommonStringConstant.TbForm;
            PatientStatusBySpeciality.PatientStatusProfileId = input.PatientStatusProfileId;
            PatientStatusBySpeciality.DeseaseSubType = input.DeseaseSubType;
            PatientStatusBySpeciality.TbPatientConfirmationType = input.TbPatientConfirmationType;
            PatientStatusBySpeciality.DepartmentLookupId = patientDiagnose.DocDepartmentLookupId;
            PatientStatusBySpeciality.SectionLookupId = patientDiagnose.DocSectionLookupId;

            FillEntityPatientStatusBySpeciality(PatientStatusBySpeciality);

            await _uowPatientStatusBySpeciality.Repository.Insert(PatientStatusBySpeciality);
            await _uowPatientStatusBySpeciality.Save();
        }


        public async Task SavePatientMedicinessuedMonth(PatientDiagnose patientDiagnose)
        {

            var TbPatientDetail = new TbPatientDetail();
            TbPatientDetail = _mapper.Map<TbPatientDetail>(patientDiagnose);

            var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());

            var _uowVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientDiagnose.GetDbContext());

            var dbVisitFlow = await _uowVisitFlow.Repository.GetALL(x => x.PatientVisitId == patientDiagnose.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();


            if (!AppCommonMethod.IsNullObject(dbVisitFlow))
            {
                var tbPatObj = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientTreatmentCycleNo == dbVisitFlow.PatientTreatmentCycleNo).OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();
                TbPatientDetail.PatientTreatmentCycleNo = dbVisitFlow?.PatientTreatmentCycleNo;

                if (!AppCommonMethod.IsNullObject(tbPatObj))
                {
                    var tbPatDetailObj = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientVisitId == patientDiagnose.PatientVisitId).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(tbPatDetailObj))
                    {
                        tbPatDetailObj.NoOfMonthsMedicineIssued = tbPatObj?.NoOfMonthsMedicineIssued + 1;

                        _uowTbPatientDetails.Repository.Update(tbPatDetailObj);
                        await _uowTbPatientDetails.CommitAsync();
                    }
                }
                else
                {
                    FillEntityTbPatientDetails(TbPatientDetail);
                    TbPatientDetail.NoOfMonthsMedicineIssued = 1;
                    await _uowTbPatientDetails.Repository.Insert(TbPatientDetail);
                    await _uowTbPatientDetails.Save();
                }

            }
            else
            {
                FillEntityTbPatientDetails(TbPatientDetail);
                TbPatientDetail.NoOfMonthsMedicineIssued = 1;
                TbPatientDetail.PatientTreatmentCycleNo = dbVisitFlow?.PatientTreatmentCycleNo;
                await _uowTbPatientDetails.Repository.Insert(TbPatientDetail);
                await _uowTbPatientDetails.Save();
            }


            //if (!AppCommonMethod.IsNullObject(tbPatObj))
            //{
            //    if (!AppCommonMethod.IsNullorZeroInt(tbPatObj?.NoOfMonthsMedicineIssued))
            //    {
            //        TbPatientDetail.NoOfMonthsMedicineIssued = tbPatObj?.NoOfMonthsMedicineIssued + 1;
            //    }
            //    else
            //    {
            //        var patLastVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId != patientDiagnose.PatientVisitId && x.PatientId == patientDiagnose.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            //        var dbObj = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientVisitId == patLastVisit.PatientOpenVisitId).OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();

            //        TbPatientDetail.NoOfMonthsMedicineIssued = AppCommonMethod.IsNullorZeroInt(dbObj.NoOfMonthsMedicineIssued) ? 1 : dbObj.NoOfMonthsMedicineIssued + 1;
            //    }

            //}
            //else
            //{
            //    //var ptDiagnose = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == patientDiagnose.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            //    if (!AppCommonMethod.IsNullObject(patientDiagnose))
            //    {
            //        if (!AppCommonMethod.IsNullorEmptyDate(patientDiagnose.FollowupDate))
            //        {

            //            var patLastVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId != patientDiagnose.PatientVisitId && x.PatientId == patientDiagnose.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            //            var dbObj = await _uowTbPatientDetails.Repository.GetALL(x => x.PatientVisitId == patLastVisit.PatientOpenVisitId).OrderByDescending(x => x.NoOfMonthsMedicineIssued).FirstOrDefaultAsync();

            //            TbPatientDetail.NoOfMonthsMedicineIssued = AppCommonMethod.IsNullorZeroInt(dbObj?.NoOfMonthsMedicineIssued) ? 1 : dbObj.NoOfMonthsMedicineIssued + 1;
            //        }
            //        else
            //        {
            //            TbPatientDetail.NoOfMonthsMedicineIssued = 1;
            //        }
            //    }
            //    else
            //    {
            //        TbPatientDetail.NoOfMonthsMedicineIssued = 1;
            //    }
            //}

        }


        public async Task<CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto> SaveSpeechTherapyForm(string input, Guid PatientDiagnoseId)
        {
            CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto SpeechtherapyFormDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>(input);
            if (!AppCommonMethod.IsNullObject(SpeechtherapyFormDto))
            {
                SpeechtherapyFormDto.PatientDiagnoseId = PatientDiagnoseId;

                var _uowDevelopmentMilestone = new UnitOfWork<DevelopmentMilestone>(_uowPatientDiagnose.GetDbContext());
                var _uowSpeechMilestone = new UnitOfWork<SpeechMilestone>(_uowPatientDiagnose.GetDbContext());
                var _uowSpeechAndLanguageHistory = new UnitOfWork<SpeechAndLanguageHistory>(_uowPatientDiagnose.GetDbContext());
                var _uowEducationalHistory = new UnitOfWork<EducationalHistory>(_uowPatientDiagnose.GetDbContext());
                var _uowFamiliyHistory = new UnitOfWork<FamiliyHistory>(_uowPatientDiagnose.GetDbContext());
                var _uowHearingProblemHistory = new UnitOfWork<HearingProblemHistory>(_uowPatientDiagnose.GetDbContext());
                var _uowSpeechTherapyPatientAssessment = new UnitOfWork<SpeechTherapyPatientAssessment>(_uowPatientDiagnose.GetDbContext());

                var _uowSpeechDisorder = new UnitOfWork<SpeechDisorder>(_uowPatientDiagnose.GetDbContext());
                var _uowAssociatedDisorder = new UnitOfWork<AssociatedDisorder>(_uowPatientDiagnose.GetDbContext());
                var _uowSpeechModality = new UnitOfWork<SpeechModality>(_uowPatientDiagnose.GetDbContext());

                DevelopmentMilestone developmentMilestone = new DevelopmentMilestone();

                developmentMilestone = _mapper.Map<DevelopmentMilestone>(SpeechtherapyFormDto);
                if (!string.IsNullOrEmpty(SpeechtherapyFormDto.NoDevelopmentMilestone))
                {
                    developmentMilestone.IsNoDevelopmentMilestone = true;
                }
                else
                {
                    developmentMilestone.IsNoDevelopmentMilestone = false;
                }
                developmentMilestone.DevelopmentMilestoneStatus = SpeechtherapyFormDto.DevelopmentMilstoneNormalOrDelayed;
                FillEntityDevelopmentMilestone(developmentMilestone);
                await _uowDevelopmentMilestone.Repository.Insert(developmentMilestone);
                await _uowDevelopmentMilestone.Save();



                SpeechAndLanguageHistory speechAndLanguageHistory = new SpeechAndLanguageHistory();

                speechAndLanguageHistory = _mapper.Map<SpeechAndLanguageHistory>(SpeechtherapyFormDto);
                FillEntitySpeechAndLanguageHistory(speechAndLanguageHistory);
                await _uowSpeechAndLanguageHistory.Repository.Insert(speechAndLanguageHistory);
                await _uowSpeechAndLanguageHistory.Save();


                SpeechMilestone speechMilestone = new SpeechMilestone();

                speechMilestone = _mapper.Map<SpeechMilestone>(SpeechtherapyFormDto);
                if (!string.IsNullOrEmpty(SpeechtherapyFormDto.NoSpeechMilestone))
                {
                    speechMilestone.IsNoSpeechMilestone = true;
                }
                else
                {
                    speechMilestone.IsNoSpeechMilestone = false;
                }
                speechMilestone.SpeechMilestoneStatus = SpeechtherapyFormDto.SpeechMilstoneNormalOrDelayed;
                FillEntitySpeechMilestone(speechMilestone);
                await _uowSpeechMilestone.Repository.Insert(speechMilestone);
                await _uowSpeechMilestone.Save();




                EducationalHistory educationalHistory = new EducationalHistory();
                educationalHistory = _mapper.Map<EducationalHistory>(SpeechtherapyFormDto);
                FillEntityEducationalHistory(educationalHistory);
                await _uowEducationalHistory.Repository.Insert(educationalHistory);
                await _uowEducationalHistory.Save();

                FamiliyHistory familiyHistory = new FamiliyHistory();
                familiyHistory = _mapper.Map<FamiliyHistory>(SpeechtherapyFormDto);
                if (SpeechtherapyFormDto.DisabilityInfamily == "Yes")
                    familiyHistory.IsFamilyHistory = true;
                else
                    familiyHistory.IsFamilyHistory = false;
                FillEntityFamiliyHistory(familiyHistory);
                await _uowFamiliyHistory.Repository.Insert(familiyHistory);
                await _uowFamiliyHistory.Save();

                HearingProblemHistory hearingProblemHistory = new HearingProblemHistory();
                hearingProblemHistory = _mapper.Map<HearingProblemHistory>(SpeechtherapyFormDto);

                if (SpeechtherapyFormDto.HearingLoss == "Yes")
                    hearingProblemHistory.IsHearingLoss = true;
                else
                    hearingProblemHistory.IsHearingLoss = false;

                FillEntityHearingProblemHistory(hearingProblemHistory);
                await _uowHearingProblemHistory.Repository.Insert(hearingProblemHistory);
                await _uowHearingProblemHistory.Save();

                SpeechTherapyPatientAssessment speechTherapyPatientAssessment = new SpeechTherapyPatientAssessment();
                speechTherapyPatientAssessment = _mapper.Map<SpeechTherapyPatientAssessment>(SpeechtherapyFormDto);
                FillEntitySpeechTherapyPatientAssessment(speechTherapyPatientAssessment);
                await _uowSpeechTherapyPatientAssessment.Repository.Insert(speechTherapyPatientAssessment);
                await _uowSpeechTherapyPatientAssessment.Save();



                foreach (var SpeechDisorder in SpeechtherapyFormDto?.SpeechDisorder)
                {
                    SpeechDisorder speechDisorder = new SpeechDisorder();
                    speechDisorder = _mapper.Map<SpeechDisorder>(SpeechtherapyFormDto);
                    speechDisorder.SpeechDisorderProfileId = SpeechDisorder.SpeechDisorderProfileId;
                    FillEntitySpeechDisorder(speechDisorder);
                    await _uowSpeechDisorder.Repository.Insert(speechDisorder);
                    await _uowSpeechDisorder.Save();
                }


                foreach (var AssociatedDisorder in SpeechtherapyFormDto?.AssociatedDisorder)
                {
                    AssociatedDisorder associatedDisorder = new AssociatedDisorder();
                    associatedDisorder = _mapper.Map<AssociatedDisorder>(SpeechtherapyFormDto);
                    associatedDisorder.AssociatedDisorderProfileId = AssociatedDisorder.AssociatedDisorderProfileId;
                    FillEntityAssociatedDisorder(associatedDisorder);
                    await _uowAssociatedDisorder.Repository.Insert(associatedDisorder);
                    await _uowAssociatedDisorder.Save();
                }



                foreach (var Modalities in SpeechtherapyFormDto?.Modalities)
                {
                    SpeechModality speechModality = new SpeechModality();
                    speechModality = _mapper.Map<SpeechModality>(SpeechtherapyFormDto);
                    speechModality.SpeechModalitiesProfileId = Modalities.SpeechModalitiesProfileId;
                    FillEntitySpeechModality(speechModality);
                    await _uowSpeechModality.Repository.Insert(speechModality);
                    await _uowSpeechModality.Save();
                }
            }


            return SpeechtherapyFormDto;
        }





        public async Task<CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto> SaveNutritionalAssesmentForm(string input, Guid PatientDiagnoseId)
        {
            CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto NutritionalAssesmentFormDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>(input);
            if (!AppCommonMethod.IsNullObject(NutritionalAssesmentFormDto))
            {
                NutritionalAssesmentFormDto.PatientDiagnoseId = PatientDiagnoseId;

                var _uowPatientBmi = new UnitOfWork<PatientBmi>(_uowPatientDiagnose.GetDbContext());
                var _uowPatientNutritionalRisk = new UnitOfWork<PatientNutritionalRisk>(_uowPatientDiagnose.GetDbContext());
                var _uowNutritionalAsessmentFinding = new UnitOfWork<NutritionalAsessmentFinding>(_uowPatientDiagnose.GetDbContext());
                var _uowPatientComorbidity = new UnitOfWork<PatientComorbidity>(_uowPatientDiagnose.GetDbContext());
                var _uowPatientMalnutrition = new UnitOfWork<PatientMalnutrition>(_uowPatientDiagnose.GetDbContext());
                var _uowPatientDietPlan = new UnitOfWork<PatientDietPlan>(_uowPatientDiagnose.GetDbContext());


                PatientBmi patientBmi = new PatientBmi();

                patientBmi = _mapper.Map<PatientBmi>(NutritionalAssesmentFormDto);
                FillEntityPatientBmi(patientBmi);
                await _uowPatientBmi.Repository.Insert(patientBmi);
                await _uowPatientBmi.Save();


                PatientNutritionalRisk patientNutritionalRisk = new PatientNutritionalRisk();

                patientNutritionalRisk = _mapper.Map<PatientNutritionalRisk>(NutritionalAssesmentFormDto);
                FillEntityPatientNutritionalRisk(patientNutritionalRisk);
                await _uowPatientNutritionalRisk.Repository.Insert(patientNutritionalRisk);
                await _uowPatientNutritionalRisk.Save();


                if (AppCommonMethod.IsNullOrEmptyList(NutritionalAssesmentFormDto.SignificantExaminationFindings))
                {
                    NutritionalAsessmentFinding nutritionalAsessmentFinding = new NutritionalAsessmentFinding();
                    nutritionalAsessmentFinding = _mapper.Map<NutritionalAsessmentFinding>(NutritionalAssesmentFormDto);
                    nutritionalAsessmentFinding.IsSignificantExaminationFindings = false;
                    nutritionalAsessmentFinding.SignificantExaminationFindings = null;
                    FillEntityNutritionalAsessmentFinding(nutritionalAsessmentFinding);
                    await _uowNutritionalAsessmentFinding.Repository.Insert(nutritionalAsessmentFinding);
                    await _uowNutritionalAsessmentFinding.Save();
                }
                else
                {
                    if (!AppCommonMethod.IsNullOrEmptyList(NutritionalAssesmentFormDto.SignificantExaminationFindings))
                    {
                        foreach (var significantFindings in NutritionalAssesmentFormDto.SignificantExaminationFindings)
                        {
                            NutritionalAsessmentFinding nutritionalAsessmentFinding = new NutritionalAsessmentFinding();
                            nutritionalAsessmentFinding = _mapper.Map<NutritionalAsessmentFinding>(NutritionalAssesmentFormDto);
                            nutritionalAsessmentFinding.IsSignificantExaminationFindings = true;
                            nutritionalAsessmentFinding.SignificantExaminationFindings = significantFindings;
                            FillEntityNutritionalAsessmentFinding(nutritionalAsessmentFinding);
                            await _uowNutritionalAsessmentFinding.Repository.Insert(nutritionalAsessmentFinding);
                            await _uowNutritionalAsessmentFinding.Save();
                        }
                    }
                }


                if (!AppCommonMethod.IsNullOrEmptyList(NutritionalAssesmentFormDto.Comorbidity))
                {
                    foreach (var comorbidity in NutritionalAssesmentFormDto.Comorbidity)
                    {
                        PatientComorbidity patientComorbidity = new PatientComorbidity();

                        patientComorbidity = _mapper.Map<PatientComorbidity>(NutritionalAssesmentFormDto);
                        patientComorbidity.IsAnyComorbidity = true;
                        patientComorbidity.Comorbidity = comorbidity;
                        FillEntityPatientComorbidity(patientComorbidity);
                        await _uowPatientComorbidity.Repository.Insert(patientComorbidity);
                        await _uowPatientComorbidity.Save();
                    }

                }
                else
                {
                    PatientComorbidity patientComorbidity = new PatientComorbidity();

                    patientComorbidity = _mapper.Map<PatientComorbidity>(NutritionalAssesmentFormDto);
                    patientComorbidity.IsAnyComorbidity = false;
                    FillEntityPatientComorbidity(patientComorbidity);
                    await _uowPatientComorbidity.Repository.Insert(patientComorbidity);
                    await _uowPatientComorbidity.Save();
                }



                PatientMalnutrition patientMalnutrition = new PatientMalnutrition();

                patientMalnutrition = _mapper.Map<PatientMalnutrition>(NutritionalAssesmentFormDto);
                FillEntityPatientMalnutrition(patientMalnutrition);
                await _uowPatientMalnutrition.Repository.Insert(patientMalnutrition);
                await _uowPatientMalnutrition.Save();


                PatientDietPlan patientDietPlan = new PatientDietPlan();

                patientDietPlan = _mapper.Map<PatientDietPlan>(NutritionalAssesmentFormDto);
                if (NutritionalAssesmentFormDto.DietPlan == "Yes")
                {
                    patientDietPlan.IsAnyPatientDietPlan = true;
                }
                else
                {
                    patientDietPlan.IsAnyPatientDietPlan = false;
                }
                FillEntityPatientDietPlan(patientDietPlan);
                await _uowPatientDietPlan.Repository.Insert(patientDietPlan);
                await _uowPatientDietPlan.Save();


            }
            return NutritionalAssesmentFormDto;
        }


        public async Task<CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto> SavePsychologicalForm(string input, Guid PatientDiagnoseId)
        {
            CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto PsychologicalAssesmentFormDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>(input);

            if (!AppCommonMethod.IsNullObject(PsychologicalAssesmentFormDto))
            {
                PsychologicalAssesmentFormDto.PatientDiagnoseId = PatientDiagnoseId;
                var _uowPastPsychiatricHistory = new UnitOfWork<PastPsychiatricHistory>(_uowPatientDiagnose.GetDbContext());
                var _uowPsychologicalAssessment = new UnitOfWork<PsychologicalAssessment>(_uowPatientDiagnose.GetDbContext());
                var _uowPsychologicalTestApplied = new UnitOfWork<PsychologicalTestApplied>(_uowPatientDiagnose.GetDbContext());
                var _uowPsychologyPatientModality = new UnitOfWork<PsychologyPatientModality>(_uowPatientDiagnose.GetDbContext());
                var _uowPsychologyDisorder = new UnitOfWork<PsychologyDisorder>(_uowPatientDiagnose.GetDbContext());
                var _uowPsychologyAssociatedDisorder = new UnitOfWork<PsychologyAssociatedDisorder>(_uowPatientDiagnose.GetDbContext());

                PastPsychiatricHistory pastPsychiatricHistory = new PastPsychiatricHistory();

                pastPsychiatricHistory = _mapper.Map<PastPsychiatricHistory>(PsychologicalAssesmentFormDto);
                if (PsychologicalAssesmentFormDto.IsPastPsychiatric == "Yes")
                {
                    pastPsychiatricHistory.IsAnyPastHistory = true;
                }
                else
                {
                    pastPsychiatricHistory.IsAnyPastHistory = false;
                }
                FillEntityPastPsychiatricHistory(pastPsychiatricHistory);
                await _uowPastPsychiatricHistory.Repository.Insert(pastPsychiatricHistory);
                await _uowPastPsychiatricHistory.Save();


                PsychologicalAssessment psychologicalAssessment = new PsychologicalAssessment();
                psychologicalAssessment = _mapper.Map<PsychologicalAssessment>(PsychologicalAssesmentFormDto);
                FillEntityPsychologicalAssessment(psychologicalAssessment);
                await _uowPsychologicalAssessment.Repository.Insert(psychologicalAssessment);
                await _uowPsychologicalAssessment.Save();


                PsychologicalTestApplied psychologicalTestApplied = new PsychologicalTestApplied();
                psychologicalTestApplied = _mapper.Map<PsychologicalTestApplied>(PsychologicalAssesmentFormDto);
                if (!string.IsNullOrEmpty(PsychologicalAssesmentFormDto.PsychologicalTestApplied))
                {
                    if (PsychologicalAssesmentFormDto.PsychologicalTestApplied == "Yes")
                    {
                        psychologicalTestApplied.IsPsychologicalTestApplied = true;
                    }
                    else
                    {
                        psychologicalTestApplied.IsPsychologicalTestApplied = false;
                    }
                }
                FillEntityPsychologicalTestApplied(psychologicalTestApplied);
                await _uowPsychologicalTestApplied.Repository.Insert(psychologicalTestApplied);
                await _uowPsychologicalTestApplied.Save();






                foreach (var PsychologyDisorder in PsychologicalAssesmentFormDto?.PsychologyDisorder)
                {
                    PsychologyDisorder psychologyDisorder = new PsychologyDisorder();
                    psychologyDisorder = _mapper.Map<PsychologyDisorder>(PsychologicalAssesmentFormDto);
                    psychologyDisorder.PsychologyDisorderProfileId = PsychologyDisorder.PsychologyDisorderProfileId;
                    FillEntityPsychologyDisorder(psychologyDisorder);
                    await _uowPsychologyDisorder.Repository.Insert(psychologyDisorder);
                    await _uowPsychologyDisorder.Save();
                }


                foreach (var AssociatedDisorder in PsychologicalAssesmentFormDto?.PsychologyAssociatedDisorder)
                {
                    PsychologyAssociatedDisorder associatedDisorder = new PsychologyAssociatedDisorder();
                    associatedDisorder = _mapper.Map<PsychologyAssociatedDisorder>(PsychologicalAssesmentFormDto);
                    associatedDisorder.PsychologyAssociatedDisorderProfileId = AssociatedDisorder.PsychologyAssociatedDisorderProfileId;
                    FillEntityPsychologyAssociatedDisorder(associatedDisorder);
                    await _uowPsychologyAssociatedDisorder.Repository.Insert(associatedDisorder);
                    await _uowPsychologyAssociatedDisorder.Save();
                }





                PsychologyPatientModality psychologyPatientModality = new PsychologyPatientModality();
                psychologyPatientModality = _mapper.Map<PsychologyPatientModality>(PsychologicalAssesmentFormDto);
                if (!string.IsNullOrEmpty(PsychologicalAssesmentFormDto.Modalities))
                {
                    if (PsychologicalAssesmentFormDto.Modalities == "Yes")
                    {
                        psychologyPatientModality.IsAnyModality = true;
                        foreach (var PsychologyModalites in PsychologicalAssesmentFormDto?.PsychologyModalites)
                        {
                            psychologyPatientModality = new PsychologyPatientModality();
                            psychologyPatientModality = _mapper.Map<PsychologyPatientModality>(PsychologicalAssesmentFormDto);
                            psychologyPatientModality.PsychologyModalitiesProfileId = PsychologyModalites.PsychologyModalitiesProfileId;
                            FillEntityPsychologyPatientModality(psychologyPatientModality);
                            await _uowPsychologyPatientModality.Repository.Insert(psychologyPatientModality);
                            await _uowPsychologyPatientModality.Save();
                        }
                    }
                    else
                    {
                        psychologyPatientModality.IsAnyModality = false;

                        FillEntityPsychologyPatientModality(psychologyPatientModality);
                        await _uowPsychologyPatientModality.Repository.Insert(psychologyPatientModality);
                        await _uowPsychologyPatientModality.Save();
                    }
                }

                return PsychologicalAssesmentFormDto;
            }

            return null;
        }


        public async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> CreateOrEditPatientDiagnoseWithPrescription(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                return await CreatePatientDiagnoseWithPrescription(input);
            else
                return await UpdatePatientDiagnoseWithPrescription(input);
        }


        private async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> CreatePatientDiagnoseWithPrescription(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {

            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var dbUser = TokenService.GetUserLoggedInfo();
                    CreateOrEditPatientDiagnoseWithPrescriptionDto duplicatedDiagnose = null;

                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);
                    if (string.IsNullOrEmpty(input.FormType))
                    {
                        input.FormType = CommonStringConstant.GeneralForm;
                    }

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientScreening = new UnitOfWork<PatientScreening>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientAssessment = new UnitOfWork<PatientAssessment>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientVaccination = new UnitOfWork<PatientVaccination>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientOpenVisits = new UnitOfWork<PatientOpenVisit>(_uowPatientDiagnose.GetDbContext());
                    var _uowHfLabTestConfig = new UnitOfWork<HfLabTestConfig>(_uowPatientDiagnose.GetDbContext());

                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisits.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //if (input.PatientLabTests.Count == 0 && input.FormType == CommonStringConstant.TbForm)

                    //Zulqarnain Working
                    //if (input.PatientLabTests.Count == 0 && input.FormType == CommonStringConstant.TbForm && AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                    //{
                    //    var PatientLabTestObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientId == input.PatientId).FirstOrDefaultAsync();

                    //    if (AppCommonMethod.IsNullObject(PatientLabTestObj))
                    //        throw new UserFriendlyException(CommonMessageConstant.NoLabTestFoundAgainstThisPatient);
                    //}

                    if (AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                        dbPatient!.FollowupDate = input.FollowupDate;
                    else
                        dbPatient!.FollowupDate = null;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();

                    var objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);
                    objPatientDiagnose.IsGynaePatient = input.IsGyanePatient;
                    FillEntityWithDetails(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                        objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    if ((bool)input.IsPatientHistory)
                    {
                        objPatientDiagnose.DocDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                        objPatientDiagnose.DocSectionLookupId = dbObjPatientVisit.SectionLookupId;
                    }
                    else
                    {
                        objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                        objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;
                    }

                    objPatientDiagnose.PatientDiagnoseDiseases.Clear();
                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientLabTests.Clear();

                    input.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    objPatientDiagnose.FormType = input.FormType;

                    if (input.IsConfirmed)
                    {
                        //var dbObj = _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == objPatientDiagnose.PatientId).ToList();
                        //if (!AppCommonMethod.IsNullOrEmptyList(dbObj))
                        //{
                        //    foreach (var item in dbObj)
                        //    {
                        //        if (!(bool)item.IsConfirmed)
                        //        {
                        //            item.IsConfirmed = true;
                        //        }
                        //    }
                        //}   
                        objPatientDiagnose.IsConfirmed = true;
                    }

                    if (!string.IsNullOrEmpty(input.ReactionNote))
                    {
                        objPatientDiagnose.AnyMedicineReaction = true;
                        objPatientDiagnose.ReactionNote = input.ReactionNote;
                    }

                    //if (!AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                    //{
                    //    objPatientDiagnose.OutcomeStatusProfileId = input.OutcomeStatusProfileId;
                    //    objPatientDiagnose.FollowupDate = null;
                    //}
                    if (input.IsRefer)
                    {
                        objPatientDiagnose.IsRefer = input.IsRefer;
                        objPatientDiagnose.IsReferInternal = input.IsReferInternal;

                        if (input.IsReferInternal == true)
                        {
                            objPatientDiagnose.ReferToDepartmentLookupId = input.ReferDepartment;
                            objPatientDiagnose.ReferToSectionLookupId = input.ReferSection;
                        } 
                        else
                        {
                            objPatientDiagnose.ReferToHealthFacilityId = input.ReferHealthFacility;
                        }
                        
                    }


                    await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    await _uowPatientDiagnose.Save();



                    if (input.IsReferToDRTB && input.FormType == CommonStringConstant.TbForm)
                    {

                        var _uowVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientDiagnose.GetDbContext());

                        var dbVisitFlow = await _uowVisitFlow.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();


                        var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                        TbPatientDetail tbPatientDetail = new TbPatientDetail();
                        FillEntityTbPatientDetails(tbPatientDetail);
                        tbPatientDetail.PatientId = input.PatientId;
                        tbPatientDetail.PatientVisitId = input.PatientVisitId;
                        tbPatientDetail.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                        tbPatientDetail.Drtbcenter = input.DRTBCenter;
                        tbPatientDetail.PatientTreatmentCycleNo = dbVisitFlow?.PatientTreatmentCycleNo;
                        tbPatientDetail.IsReferToDrtb = true;
                        await _uowTbPatientDetails.Repository.Insert(tbPatientDetail);
                        await _uowTbPatientDetails.Save();

                    }

                    if (input.FormType == CommonStringConstant.TbForm && !AppCommonMethod.IsNullOrEmptyGuid(input.PatientStatusProfileId))
                    {
                        await SavePatientStatusBySpeciality(input, objPatientDiagnose);
                    }

                    if (input.FormType == CommonStringConstant.TbForm && input.PatientPrescriptions.Count() > 0)
                    {
                        await SavePatientMedicinessuedMonth(objPatientDiagnose);
                    }



                    // Prescription Check
                    if (input.FormType == CommonStringConstant.HCPForm)
                    {
                        // Variable for HCP Medicine Dispatch
                        List<PatientPrescription> tempPatientPrescription = new List<PatientPrescription>();

                        foreach (var itemPatientPrescription in input.PatientPrescriptions)
                        {
                            var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                            FillEntityPrescription(objPatientPrescription);

                            objPatientPrescription.PatientId = input.PatientId;
                            objPatientPrescription.PatientVisitId = input.PatientVisitId;
                            objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            if (dbObjPatientVisit!.IsFromPmis)
                                objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                            else
                                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                            await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                            await _uowPatientPrescription.Save();
                            // Assigning Prescription Object for HCP to Dispatch Kits
                            tempPatientPrescription.Add(objPatientPrescription);
                        }

                        #region HCP
                        //Adding code for HCV 
                        // Assigining Patient Diagnose Id to Patient Screening
                        //if (input.PatientScreening.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                        if ((input.PatientScreening.PatientId != null && input.FormType == CommonStringConstant.HCPForm) || input.PatientVaccination.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                        {

                            input.IsActive = true;

                            if (input.PatientScreening.PatientId != null)
                            {
                                input.PatientScreening.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                                var objPatientScreening = _mapper.Map<PatientScreening>(input.PatientScreening);
                                FillEntityHCPScreening(objPatientScreening);
                                await _uowPatientScreening.Repository.Insert(objPatientScreening);
                                await _uowPatientScreening.Save();
                            }

                            //// Now Dispatching Testing Kits for New-Diagnosed Patients
                            //if (input.PatientScreening.PatientType == "New Patient")
                            //{
                            if (input.PatientScreeningTestKitsDispense.MedicineDispatches.Count != 0)
                            {
                                // Assigning Patient Prescription ID to Dispatch Medicine Forign key
                                foreach (var prescriptionItem in tempPatientPrescription)
                                {
                                    input.PatientScreeningTestKitsDispense.MedicineDispatches.FirstOrDefault(x => x.MedicineId == prescriptionItem.MedicineId).PatientPrescriptionId = prescriptionItem.PatientPrescriptionId;
                                }

                                foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                                {
                                    item.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                                }
                                // here--
                                //var ScreeningTestKitsDispense = _MedicineDispatchService.CreatePatientDispatch(input.PatientScreeningTestKitsDispense);
                                foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                                {
                                    List<MedicineDispenseDto> mimsMedicineList = new List<MedicineDispenseDto>();
                                    mimsMedicineList.Add(new MedicineDispenseDto
                                    {
                                        hfmisCode = TokenService.GetHfHrId(),//TokenService.GetUserHfCode(),  //
                                        Quantity = item.QuantityDispatch ?? 0,
                                        MedId = item.MedicineId,
                                        BatchNo = item.BatchNo!,
                                        WardId = Convert.ToInt32(TokenService.GetMimsDepartmentId())
                                    });
                                    var mimsDispatchResponse = await _mimsService.MedicineDespenseByHealthFacility(_mimsBaseUrl, mimsMedicineList);

                                    //if (mimsDispatchResponse != null && mimsDispatchResponse.Status != "Medicine Not Found") // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                                    if (mimsDispatchResponse != null && mimsDispatchResponse.Status)
                                    {
                                        // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                                        item.MIMSDispatched = true;
                                    }
                                    if (mimsDispatchResponse.Data == null)
                                    {
                                        item.Reason = mimsDispatchResponse.Message;
                                        throw new UserFriendlyException(CommonMessageConstant.TestingKitsNotFound);
                                    }
                                    if (input.PatientScreening.PatientId != null)
                                    {
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == true && input.PatientScreening.IsDiagnosedHbvrepidKit == null)
                                        {
                                            item.Reason = "Kit not used";
                                            //throw new UserFriendlyException(CommonMessageConstant.HBVTestingKitsNotFound);
                                        }
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHbvrepidKit != null)
                                        {
                                            item.Reason = mimsDispatchResponse.Data[0].Reason;
                                            throw new UserFriendlyException(CommonMessageConstant.HBVKitStock);
                                        }
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHbvrepidKit == null)
                                        {
                                            item.Reason = mimsDispatchResponse.Data[0].Reason;
                                            throw new UserFriendlyException(CommonMessageConstant.HBVKitStock);
                                        }
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == true && input.PatientScreening.IsDiagnosedHcvrepidKit == null)
                                        {
                                            item.Reason = "Kit not used";
                                            //throw new UserFriendlyException(CommonMessageConstant.HCVTestingKitsNotFound);
                                        }
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHcvrepidKit != null)
                                        {
                                            item.Reason = mimsDispatchResponse.Data[0].Reason;
                                            throw new UserFriendlyException(CommonMessageConstant.HCVKitStock);
                                        }
                                        if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHcvrepidKit == null)
                                        {
                                            item.Reason = mimsDispatchResponse.Data[0].Reason;
                                            throw new UserFriendlyException(CommonMessageConstant.HCVKitStock);
                                        }
                                    }
                                    else if (input.PatientVaccination.PatientId != null && mimsDispatchResponse.Data.Count > 0)
                                    {
                                        item.Reason = mimsDispatchResponse.Data[0].Reason;
                                        throw new UserFriendlyException(CommonMessageConstant.ScreeningStageStock);
                                    }

                                }


                                //using (var tran = _uowMedicineDispatch.GetDbContext().Database.BeginTransaction())
                                //{
                                var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowMedicineDispatch.GetDbContext());
                                var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineDispatch.GetDbContext());

                                var patientPrescription = await _uowPatientPrescription.Repository.GetALL().Where(x => x.PatientVisitId == input.PatientScreeningTestKitsDispense.PatientVisitId)
                                        .Include(x => x.DoseProfile).Include(x => x.DoseTimeProfile).ToListAsync();

                                if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                                    throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

                                if (dbObjPatientVisit!.IsDischarge == true)
                                    throw new UserFriendlyException(CommonMessageConstant.VisitClosed);

                                foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                                {
                                    var obj = _mapper.Map<MedicineDispatch>(item);
                                    FillEntityMedicineDispatch(obj);
                                    obj.PatientId = input.PatientScreeningTestKitsDispense.PatientId;
                                    obj.PatientVisitId = input.PatientScreeningTestKitsDispense.PatientVisitId;
                                    obj.Pharmacist = _tokenService.GetUserId();
                                    obj.PatientDiagnoseId = item.PatientDiagnoseId;
                                    obj.PatientPrescriptionId = item.PatientPrescriptionId;
                                    obj.WardId = Convert.ToInt32(TokenService.GetMimsDepartmentId());

                                    await _uowMedicineDispatch.Repository.Insert(obj);
                                    await _uowMedicineDispatch.Save();


                                    //await UpdatePatientDiagnoseRecordJsonObj(item.PatientDiagnoseId);

                                }

                                //dbObjPatientVisit.IsOccupied = false;
                                //dbObjPatientVisit.OccupiedBy = null;
                                //dbObjPatientVisit.IsDischarge = true;
                                //dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                                //dbObjPatientVisit.UpdatedOn = DateTime.Now;
                                //dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                                //_uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                                //await _uowPatientOpenVisit.Save();

                                // Create Patient Work Log
                                //CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                                //objPatientWorkFlowLog.PatientId = input.PatientId;
                                //objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                                //objPatientWorkFlowLog.HealthFacilityId = dbObjPatientVisit!.HealthFacilityId;
                                //objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                                //objPatientWorkFlowLog.NextStationProfileId = null;
                                //objPatientWorkFlowLog.IsVisitClose = true;

                                foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                                {
                                    var prescription = patientPrescription.Where(x => x.MedicineId == item.MedicineId).FirstOrDefault();

                                    if (item.AvailableQuantity < item.QuantityPrescribed)
                                    {
                                        var remainingQty = item.QuantityPrescribed - item.QuantityDispatch;

                                    }


                                    //int i = 0;
                                    //foreach (var medItem in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                                    //{

                                    //    var MedPrescription = patientPrescription.Where(x => x.MedicineId == medItem.MedicineId).FirstOrDefault();

                                    //    if (medItem.AvailableQuantity < medItem.QuantityPrescribed)
                                    //    {
                                    //        var remainingQty = medItem.QuantityPrescribed - medItem.QuantityDispatch;
                                    //    }
                                    //}




                                    //objPatientWorkFlowLog.IsActive = true;
                                    //await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);
                                    //}
                                    //return input;
                                }
                            }
                        }
                        // Assigining Patient Diagnose Id to Patient Assessment
                        if (input.PatientAssessment.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                        {
                            if (input.PatientAssessment.SurgeryDate != null)
                            {
                                input.PatientAssessment.SurgeryDate = input.PatientAssessment.SurgeryDate.Value.AddDays(5);
                            }
                            if (input.PatientAssessment.BloodTransfusionYear != null)
                            {
                                input.PatientAssessment.SurgeryDate = input.PatientAssessment.BloodTransfusionYear.Value.AddDays(5);
                            }
                            if (input.PatientAssessment.BloodTransfusionYear != null)
                            {
                                input.PatientAssessment.SurgeryDate = input.PatientAssessment.BloodTransfusionYear.Value.AddDays(5);
                            }
                            input.PatientAssessment.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            var objPatientAssessment = _mapper.Map<PatientAssessment>(input.PatientAssessment);
                            FillEntityHCPAssessment(objPatientAssessment);

                            await _uowPatientAssessment.Repository.Insert(objPatientAssessment);
                            await _uowPatientAssessment.Save();
                        }
                        // Assigining Patient Diagnose Id to Patient Vaccination
                        if (input.PatientVaccination.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                        {
                            if (input.PatientVaccination.VaccinationDose1Date != null)
                            {
                                input.PatientVaccination.VaccinationDose1Date = input.PatientVaccination.VaccinationDose1Date.Value.AddHours(5);
                            }
                            if (input.PatientVaccination.VaccinationDose2Date != null)
                            {
                                input.PatientVaccination.VaccinationDose2Date = input.PatientVaccination.VaccinationDose2Date.Value.AddHours(5);
                            }
                            if (input.PatientVaccination.VaccinationDose3Date != null)
                            {
                                input.PatientVaccination.VaccinationDose3Date = input.PatientVaccination.VaccinationDose3Date.Value.AddHours(5);
                            }
                            input.PatientVaccination.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            var objPatientVaccination = _mapper.Map<PatientVaccination>(input.PatientVaccination);
                            FillEntityHCPVaccination(objPatientVaccination);

                            await _uowPatientVaccination.Repository.Insert(objPatientVaccination);
                            await _uowPatientVaccination.Save();
                        }
                        #endregion

                    }
                    else
                    {
                        foreach (var itemPatientPrescription in input.PatientPrescriptions)
                        {
                            var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                            FillEntityPrescription(objPatientPrescription);

                            objPatientPrescription.PatientId = input.PatientId;
                            objPatientPrescription.PatientVisitId = input.PatientVisitId;
                            objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            if (dbObjPatientVisit!.IsFromPmis)
                                objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                            else
                                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                            await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                            await _uowPatientPrescription.Save();
                        }
                    }

                    // Lab Check
                    //var depName = "";
                    //var _uowSection = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                    //if (_isGynaeOPD)
                    //{
                    //    depName = await _uowSection.Repository.GetALL(x => x.SectionLookupId == user.SectionId).Select(x => x.Name).FirstOrDefaultAsync();
                    //}
                    foreach (var itemPatientLabTests in input.PatientLabTests)
                    {

                        var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                            .Include(x => x.LabTestDetails.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted))
                             .FirstOrDefaultAsync();

                        var configIsPerformedPrivately = await _uowHfLabTestConfig.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId
                                                && x.HealthFacilityId == dbUser!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.IsPerformedPrivately).FirstOrDefaultAsync();

                        //var dbLabTestDetail = await _uowLabTestDetail.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId).ToListAsync();

                        var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);
                        var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                        // If Test Sample is not Required
                        if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                        {
                            objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                            FillEntitySampleCollection(objPatientLabTest);
                        }
                        objPatientLabTest.IsPerformedPrivately = (configIsPerformedPrivately == true) ? true : false;

                        ////if test price 0 then redirect to pending collection
                        //if (AppCommonMethod.IsNullorZeroDecimal(dbLabTest.TestPrice))
                        //{
                        //    objPatientLabTest.IsPaid = true;
                        //}

                        //if (_isGynaeOPD)
                        //{
                        //    if (depName == CommonStringConstant.GynaeOPD)
                        //    {
                        //        objPatientLabTest.IsPaid = true;
                        //        objPatientLabTest.TestPrice = 0;
                        //    }
                        //    else
                        //    {
                        //        objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                        //    }
                        //}
                        //else
                        //{  objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                        //}


                        // If gynae Patient then all lab test will be free
                        if (input!.IsGyanePatient)
                        {
                            //objPatientLabTest.IsPaid = true;
                            objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                            objPatientLabTest.DiscountedPrice = (decimal)dbLabTest.TestPrice; // set Price to Zero
                            objPatientLabTest.TestPrice = (decimal)0;// set Price to Zero
                            objPatientLabTest.DiscountedByProfileId = await _uowProfile.Repository.GetALL(x => x.ShortName == "GYNDTR").Select(x => x.ProfileId).FirstOrDefaultAsync();
                        }

                        // If Flag IsFreeLabTest = true set in Section then set Lab Test as 100% Discount (as Free)
                        if (dbUser!.IsFreeLabTest == true)
                        {

                            // set Discound 100 % to set Test as Free
                            objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                            objPatientLabTest.DiscountedPrice = (decimal)0; // set Price to Zero

                        }

                        // If Flag IsSkipAlmoner = true set in Section then set Lab Test as 100% Discount and IsPaid=true (as Free & refer to Lab Direct)
                        if (dbUser!.IsSkipAlmoner == true)
                        {
                            // set Discound 100 % and IsPaid = true to redirect directly to Pending Collection
                            objPatientLabTest.IsPaid = true; // redirect to Pending Collection
                            objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                            objPatientLabTest.DiscountedPrice = (decimal)0; // set Price to Zero
                        }

                        if (!input!.IsGyanePatient)
                            objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                        FillEntityLab(objPatientLabTest);
                        objPatientLabTest.PatientId = input.PatientId;
                        objPatientLabTest.PatientVisitId = input.PatientVisitId;
                        objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        if (dbObjPatientVisit!.IsFromPmis)
                            objPatientLabTest.TestAdvisedBy = dbObjPatientVisit.AttendedBy;
                        else
                            objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                        //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                        objPatientLabTest.IsArchived = false;
                        objPatientLabTest.IsActive = true;

                        // Generate New BarcodeNo
                        objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest.LabTestId, _isOnline);
                        //var NewTokenNo = await GetTokenNo(input.HealthFacilityId, input.DepartmentLookupId);

                        foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                        {
                            itemdbLabTestDetail.IsActive = true;
                            FillEntityLabDetail(itemdbLabTestDetail);
                            objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                        }

                        await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                        await _uowPatientLabTest.Save();

                    }


                    //// Diagnose Diseases Check
                    var _uowPatientDiagnoseDisease = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());
                    if (input.FormType != CommonStringConstant.NutritionForm && input.FormType != CommonStringConstant.PsychologyForm && input.FormType != CommonStringConstant.RespiratoryForm)
                    {
                        foreach (var item in input.PatientDiagnoseDiseases)
                        {
                            //item.PatientDiagnoseDiseaseId = null;
                            if (AppCommonMethod.IsNullorZeroInt(item.DiagnoseTypeId))
                                item.DiagnoseTypeId = 1; // Preliminary Diagnosis    

                            var objPatientDiagnoseDisease = _mapper.Map<PatientDiagnoseDisease>(item);

                            objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            objPatientDiagnoseDisease.PatientDiagnoseDiseaseId = Guid.NewGuid();
                            objPatientDiagnoseDisease.IsActive = true;
                            objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                            objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                            objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;

                            await _uowPatientDiagnoseDisease.Repository.Insert(objPatientDiagnoseDisease);
                            await _uowPatientDiagnoseDisease.Save();

                        }
                    }

                    if (input.IsRefer) // if refered
                    {

                        // Create Patient Visit Flow
                        CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                        //objPatientDiagnoseReferLog.CurrentDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        //objPatientDiagnoseReferLog.CurrentSectionId = dbObjPatientVisit.SectionLookupId;

                        //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                        //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                        objPatientVisitFlow.CurrentDepartmentId = input.ReferDepartment;
                        objPatientVisitFlow.CurrentSectionId = input.ReferSection;

                        objPatientVisitFlow.PreviousDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        objPatientVisitFlow.PreviousSectionId = dbObjPatientVisit.SectionLookupId;

                        objPatientVisitFlow.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                        objPatientVisitFlow.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                        objPatientVisitFlow.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        objPatientVisitFlow.ReferedBy = _tokenService.GetUserId();

                        var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.ReferSection).FirstOrDefaultAsync();
                        if (sectionLookup != null)
                        {
                            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
                            objPatientVisitFlow.IsConsultant = sectionLookup!.IsConsultant;
                        }


                        await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;

                        if (input.ReferDepartment == dbObjPatientVisit.DepartementLookupId)
                        {
                            dbObjPatientVisit.IsReferred = true;
                            dbObjPatientVisit.ReferredHealthFacilityId = dbObjPatientVisit.HealthFacilityId;
                            dbObjPatientVisit.ReferredDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                            dbObjPatientVisit.ReferredSectionLookupId = dbObjPatientVisit.SectionLookupId;
                            dbObjPatientVisit.ReferredBy = _tokenService.GetUserId();

                            dbObjPatientVisit.DepartementLookupId = input.ReferDepartment;
                            dbObjPatientVisit.SectionLookupId = input.ReferSection;
                        }
                        
                        dbObjPatientVisit.AttendedBy = null;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;

                        // if refer to IPD
                        //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                        //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                        //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                        // If Refer to Other Department
                        if(dbObjPatientVisit.ReferredDepartmentLookupId != dbObjPatientVisit.DepartementLookupId)
                        {
                            //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment == CommonStringConstant.IPD)
                            //{
                                var userInfo = TokenService.GetUserLoggedInfo();

                                dbObjPatientVisit.IsAdmittedInIpd = false;
                                dbObjPatientVisit.IsReferredIpd = true;
                                dbObjPatientVisit.IpdDepartmentLookupId = input.ReferDepartment;
                                dbObjPatientVisit.IpdSectionLookupId = input.ReferSection;
                                dbObjPatientVisit.IpdReferredBy = _tokenService.GetUserId();
                                dbObjPatientVisit.IpdReferredByDepartmentLookupId = userInfo!.DepartmentId;
                                dbObjPatientVisit.IpdReferredBySectionLookupId = userInfo!.SectionId;
                            //}
                        }
                        

                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        //objPatientWorkFlowLog.IsVisitClose = true;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                    }
                    else
                    { // if not refered


                        if (input.IsVisitClose)
                        {
                            //check for next station
                            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                                //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                            if (AppCommonMethod.IsNullObject(thisStation))
                                throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                            var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                            if (AppCommonMethod.IsNullObject(nextStation))
                            {
                                //isVisitClose = true;
                                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                                // SMS on Visit Close
                                //SendSMSDto smsObj = new SendSMSDto()
                                //{
                                //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                                //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                                //};


                                //_smsService.SendSMS(smsObj);
                            }



                            //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                            //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            if (dbObjPatientVisit.IsReferred == true)
                            {
                                dbObjPatientVisit.IsReferred = null;
                                dbObjPatientVisit.ReferredHealthFacilityId = null;
                                dbObjPatientVisit.ReferredDepartmentLookupId = null;
                                dbObjPatientVisit.ReferredSectionLookupId = null;
                                dbObjPatientVisit.ReferredBy = null;
                            }

                            dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;


                            if (input.FormType == CommonStringConstant.TbForm)
                                dbObjPatientVisit!.IsDischarge = true;
                            else
                                dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;

                            dbObjPatientVisit.IsOccupied = false;
                            dbObjPatientVisit.OccupiedBy = null;
                            dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                            dbObjPatientVisit.UpdatedOn = DateTime.Now;
                            dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                            _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                            await _uowPatientOpenVisit.Save();

                            // Create Patient Work Log
                            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                            objPatientWorkFlowLog.PatientId = input.PatientId;
                            objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                            objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                            objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                            objPatientWorkFlowLog.NextStationProfileId = null;
                            objPatientWorkFlowLog.IsVisitClose = false;
                            objPatientWorkFlowLog.IsActive = true;
                            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                            // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};

                            //_smsService.SendSMS(smsObj);

                        }
                        else
                        {
                            //check for next station
                            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                                //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                            if (AppCommonMethod.IsNullObject(thisStation))
                                throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                            var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                            if (AppCommonMethod.IsNullObject(nextStation))
                            {
                                //isVisitClose = true;
                                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                                // SMS on Visit Close
                                //SendSMSDto smsObj = new SendSMSDto()
                                //{
                                //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                                //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                                //};


                                //_smsService.SendSMS(smsObj);
                            }



                            //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                            //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            if (dbObjPatientVisit.IsReferred == true)
                            {
                                dbObjPatientVisit.IsReferred = null;
                                dbObjPatientVisit.ReferredHealthFacilityId = null;
                                dbObjPatientVisit.ReferredDepartmentLookupId = null;
                                dbObjPatientVisit.ReferredSectionLookupId = null;
                                dbObjPatientVisit.ReferredBy = null;
                            }

                            dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                            dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                            dbObjPatientVisit.IsDischarge = isVisitClose;

                            dbObjPatientVisit.IsOccupied = false;
                            dbObjPatientVisit.OccupiedBy = null;
                            dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                            dbObjPatientVisit.UpdatedOn = DateTime.Now;
                            dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                            _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                            await _uowPatientOpenVisit.Save();

                            // Create Patient Work Log
                            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                            objPatientWorkFlowLog.PatientId = input.PatientId;
                            objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                            objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                            objPatientWorkFlowLog.IsActive = true;
                            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                        }
                    }
                    // Store Json Object


                    var jsonObj = "";
                    var TempJsonObj = "";
                    if (input.FormType == CommonStringConstant.SpeechTherapyForm)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto SpeechtherapyFormDto = await SaveSpeechTherapyForm(input.FormData, objPatientDiagnose.PatientDiagnoseId);
                            TempJsonObj = JsonConvert.SerializeObject(SpeechtherapyFormDto);
                        }
                    }
                    else if (input.FormType == CommonStringConstant.NutritionForm)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto NutritiontherapyFormDto = await SaveNutritionalAssesmentForm(input.FormData, objPatientDiagnose.PatientDiagnoseId);
                            TempJsonObj = JsonConvert.SerializeObject(NutritiontherapyFormDto);
                        }
                    }
                    else if (input.FormType == CommonStringConstant.NutritionFormIPD)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithNutritionisttherapyFormIPDDto NutritiontherapyFormDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithNutritionisttherapyFormIPDDto>(input.FormData);
                            NutritiontherapyFormDto.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            TempJsonObj = JsonConvert.SerializeObject(NutritiontherapyFormDto);
                        }
                    }
                    else if (input.FormType == CommonStringConstant.PsychologyForm)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto PsychologytherapyFormDto = await SavePsychologicalForm(input.FormData, objPatientDiagnose.PatientDiagnoseId);
                            TempJsonObj = JsonConvert.SerializeObject(PsychologytherapyFormDto);
                        }
                    }
                    else if (input.FormType == CommonStringConstant.RespiratoryFormIPD)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormIPDDto RespiratoryFormIPDDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormIPDDto>(input.FormData);
                            RespiratoryFormIPDDto.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            TempJsonObj = JsonConvert.SerializeObject(RespiratoryFormIPDDto);
                        }
                    }
                    else if (input.FormType == CommonStringConstant.RespiratoryForm)
                    {
                        if (!string.IsNullOrEmpty(input.FormData))
                        {
                            CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormDto RespiratoryFormDto = JsonConvert.DeserializeObject<CreateOrEditPatientDiagnoseWithRespiratoryTherapyFormDto>(input.FormData);
                            RespiratoryFormDto.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            TempJsonObj = JsonConvert.SerializeObject(RespiratoryFormDto);
                        }
                    }

                    jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    JObject obj1 = JObject.Parse(jsonObj);
                    if (!string.IsNullOrEmpty(TempJsonObj))
                    {
                        obj1[input.FormType] = JObject.Parse(TempJsonObj);
                    }
                    string combinedJson = obj1.ToString();

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input.FormType;
                    dbPatientDiagnoseRecord.Json = combinedJson;
                    dbPatientDiagnoseRecord.IsActive = true;

                    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    await _uowPatientDiagnoseRecord.Save();

                    trans.Commit();

                    //if (input.FormType == CommonStringConstant.TbForm)
                    //{
                    //    if (input.IsExpertTest && !AppCommonMethod.IsNullObject(duplicatedDiagnose))
                    //    {
                    //        duplicatedDiagnose.IsExpertTest = false;
                    //        var response = await CreateOrEditPatientDiagnoseWithPrescription(duplicatedDiagnose!);
                    //    }

                    //}

                    if ((input.PatientScreening.PatientId != null && input.FormType == CommonStringConstant.HCPForm && input.PatientScreeningTestKitsDispense.MedicineDispatches.Count != 0) || (input.PatientVaccination.PatientId != null && input.FormType == CommonStringConstant.HCPForm && input.PatientScreeningTestKitsDispense.MedicineDispatches.Count != 0))
                    {
                        await UpdatePatientDiagnoseRecordJsonObj(input.PatientDiagnoseId);
                    }

                    var diseaseList = input.PatientDiagnoseDiseases.Select(x => x.DiseaseProfileId).ToArray();

                    var isMesslesDisease = _uowProfile!.Repository.GetALL(x => diseaseList.Contains(x.ProfileId) && measlesDiseaseList.Contains(x.ShortName)).Count();
                    
                    if (isMesslesDisease > 0)
                    {
                        await EmrSuspectPatientRequest(dbPatient,input);
                    }

                }
                //}
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
        }

        private async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> UpdatePatientDiagnoseWithPrescription(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    var dbUser = TokenService.GetUserLoggedInfo();

                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);
                    if (string.IsNullOrEmpty(input.FormType))
                    {
                        input.FormType = CommonStringConstant.GeneralForm;
                    }

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());
                    var _uowHfLabTestConfig = new UnitOfWork<HfLabTestConfig>(_uowPatientDiagnose.GetDbContext());


                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();



                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    DbModel.PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    dbPatient!.FollowupDate = input.FollowupDate;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();


                    var objPatientDiagnose = new PatientDiagnose();
                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        objPatientDiagnose = await _uowPatientDiagnose.Repository.GetById(input.PatientDiagnoseId!);
                        objPatientDiagnose!.FollowupDate = input.FollowupDate;
                        objPatientDiagnose.AdviseGiven = input.AdviseGiven;
                        objPatientDiagnose.Examination = input.Examination;
                        objPatientDiagnose.PatientMedicalHistory = input.PatientMedicalHistory;
                        objPatientDiagnose.PresentComplaints = input.PresentComplaints;
                        objPatientDiagnose.IsVerifiedByConsultant = input.IsVerifiedByConsultant;
                        objPatientDiagnose.IsMlc = input.IsMlc;
                        objPatientDiagnose.IsSendToCdc = input.IsSendToCdc;
                    }
                    else
                        objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);


                    FillEntityWithDetails(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                        objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                    objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                    objPatientDiagnose.PatientDiagnoseDiseases.Clear();
                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientDiagnoseProcedures.Clear();

                    if (input.IsRefer)
                    {
                        objPatientDiagnose.IsRefer = input.IsRefer;
                        objPatientDiagnose.IsReferInternal = input.IsReferInternal;
                        if (input.IsReferInternal == true)
                        {
                            objPatientDiagnose.ReferToHealthFacilityId = null;
                            objPatientDiagnose.ReferToDepartmentLookupId = input.ReferDepartment;
                            objPatientDiagnose.ReferToSectionLookupId = input.ReferSection;
                        }
                        else
                        {
                            objPatientDiagnose.ReferToHealthFacilityId = input.ReferHealthFacility;
                            objPatientDiagnose.ReferToDepartmentLookupId = null;
                            objPatientDiagnose.ReferToSectionLookupId = null;
                        }
                    }
                    else
                    {
                        objPatientDiagnose.IsRefer = null;
                        objPatientDiagnose.IsReferInternal = null;
                        objPatientDiagnose.ReferToHealthFacilityId = null;
                        objPatientDiagnose.ReferToDepartmentLookupId = null;
                        objPatientDiagnose.ReferToSectionLookupId = null;
                    }

                    if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    }
                    else
                    {
                        _uowPatientDiagnose.Repository.Update(objPatientDiagnose);
                    }
                    await _uowPatientDiagnose.Save();


                    // Delete diagnose diseases 
                    var _uowPatientDiagnoseDisease = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());

                    List<PatientDiagnoseDisease> dbListPatientDiagnoseDisease = await _uowPatientDiagnoseDisease.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var item in dbListPatientDiagnoseDisease)
                    {

                        item.DeletedBy = _tokenService.GetUserId();
                        item.DeletedOn = DateTime.Now;
                        item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                        _uowPatientDiagnoseDisease.Repository.Update(item);
                        await _uowPatientDiagnoseDisease.Save();

                    }
                    // END Delete diagnose diseases


                    //// Diagnose Diseases Check

                    //var _uowPatientDiagnoseDisease = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());
                    foreach (var item in input.PatientDiagnoseDiseases)
                    {
                        item.PatientDiagnoseDiseaseId = null;
                        var objPatientDiagnoseDisease = _mapper.Map<PatientDiagnoseDisease>(item);


                        objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                        objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                        objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                        objPatientDiagnoseDisease.PatientDiagnoseDiseaseId = Guid.NewGuid();
                        objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;

                        await _uowPatientDiagnoseDisease.Repository.Insert(objPatientDiagnoseDisease);
                        await _uowPatientDiagnoseDisease.Save();

                    }

                    //var dbPatientDiagnoseDiseases = await _uowPatientDiagnoseDisease.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId! && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //soft delete Record
                    //foreach (var ptientDiagnoseDisease in dbPatientDiagnoseDiseases.ToList())
                    //{
                    //    if (!input.PatientDiagnoseDiseases.Any(c => c.DiseaseProfileId == ptientDiagnoseDisease.DiseaseProfileId && c.PatientDiagnoseId == ptientDiagnoseDisease.PatientDiagnoseId))
                    //    {
                    //        ptientDiagnoseDisease.DeletedBy = _tokenService.GetUserId();
                    //        ptientDiagnoseDisease.DeletedOn = DateTime.Now;
                    //        ptientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    //        _uowPatientDiagnoseDisease.Repository.Update(ptientDiagnoseDisease);
                    //        //await _uowPatientLabTest.Save();
                    //    }
                    //}

                    //// Update and Insert Lab Test
                    //foreach (var ptientDiagnoseDisease in input.PatientDiagnoseDiseases.ToList())
                    //{
                    //    var dbPatientDiagnoseDisease = dbPatientDiagnoseDiseases
                    //        .Where(c => c.DiseaseProfileId == ptientDiagnoseDisease.DiseaseProfileId && c.PatientDiagnoseId == ptientDiagnoseDisease.PatientDiagnoseId && c.DiseaseProfileId == ptientDiagnoseDisease.DiseaseProfileId && c.PatientDiagnoseDiseaseId != default(Guid))
                    //        .SingleOrDefault();

                    //    if (dbPatientDiagnoseDisease != null)
                    //    {
                    //        // Update child

                    //        //FillEntityPrescription(dbPatientPrescription);
                    //        //_uowPatientPrescription.Repository.Update(dbPatientPrescription);
                    //    }
                    //    else
                    //    {
                    //        // Insert child

                    //        var objPatientDiagnoseDisease = _mapper.Map<PatientDiagnoseDisease>(ptientDiagnoseDisease);

                    //        objPatientDiagnoseDisease.PatientId = input.PatientId;
                    //        //objPatientDiagnoseDisease.PatientVisitId = input.PatientVisitId;
                    //        objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //        objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                    //        objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                    //        objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;

                    //        await _uowPatientDiagnoseDisease.Repository.Insert(objPatientDiagnoseDisease);

                    //    }
                    //}
                    //await _uowPatientDiagnoseDisease.Save();

                    //// End Diagnose Diseases Check


                    // Delete Patient Prescription
                    //var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatientDiagnose.GetDbContext());

                    //List<PatientPrescription> dbListPatientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //foreach (var item in dbListPatientPrescription)
                    //{
                    //    item.DeletedBy = _tokenService.GetUserId();
                    //    item.DeletedOn = DateTime.Now;
                    //    item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    //    _uowPatientPrescription.Repository.Update(item);
                    //    await _uowPatientPrescription.Save();

                    //}
                    // END Delete diagnose diseases

                    // old working
                    // Delete Patient Lab Test Recommended
                    //var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientDiagnose.GetDbContext());

                    //List<PatientLabTest> dbListPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //foreach (var item in dbListPatientLabTest)
                    //{
                    //    item.DeletedBy = _tokenService.GetUserId();
                    //    item.DeletedOn = DateTime.Now;
                    //    item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    //    _uowPatientLabTest.Repository.Update(item);
                    //    await _uowPatientLabTest.Save();

                    //}
                    // END Delete dPatient Lab Test
                    // old working

                    // Delete diagnose Diagnose procedures
                    var _uowPatientDiagnoseProcedure = new UnitOfWork<PatientDiagnoseProcedure>(_uowPatientDiagnose.GetDbContext());

                    List<PatientDiagnoseProcedure> dbListPatientDiagnoseProcedure = await _uowPatientDiagnoseProcedure.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var item in dbListPatientDiagnoseProcedure)
                    {
                        if (input.PatientDiagnoseProcedures.Count() > 0 && input.PatientDiagnoseProcedures.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                        {
                            var tempDiagnoseProcedure = input.PatientDiagnoseProcedures.Where(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId).FirstOrDefault();

                            item.Feedback = tempDiagnoseProcedure.Feedback;
                            item.IsPerformed = tempDiagnoseProcedure.IsPerformed;
                            item.PerformedBy = tempDiagnoseProcedure.PerformedBy;
                            item.RecommendBy = tempDiagnoseProcedure.RecommendBy;
                            item.AssistedBy = tempDiagnoseProcedure.AssistedBy;
                            item.ToothNumber = tempDiagnoseProcedure.ToothNumber;
                            item.ToothPosition = tempDiagnoseProcedure.ToothPosition;
                            item.UpdatedBy = _tokenService.GetUserId();
                            item.UpdatedOn = DateTime.Now;
                            item.ActionTypeId = (int)ActionTypeEnum.Edit;
                            _uowPatientDiagnoseProcedure.Repository.Update(item);
                        }
                        else
                        {
                            item.DeletedBy = _tokenService.GetUserId();
                            item.DeletedOn = DateTime.Now;
                            item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                            _uowPatientDiagnoseProcedure.Repository.Update(item);
                        }

                        await _uowPatientDiagnoseProcedure.Save();

                    }

                    foreach (var item in input.PatientDiagnoseProcedures)
                    {
                        if (dbListPatientDiagnoseProcedure.Count() > 0 && !dbListPatientDiagnoseProcedure.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                        {
                            var dbObj = _mapper.Map<PatientDiagnoseProcedure>(item);
                            dbObj.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            dbObj.PatientVisitId = objPatientDiagnose.PatientVisitId;
                            dbObj.PatientDiagnoseProcedureId = Guid.NewGuid();
                            dbObj.CreatedBy = _tokenService.GetUserId();
                            dbObj.CreatedOn = DateTime.Now;
                            dbObj.ActionTypeId = (int)ActionTypeEnum.Create;
                            dbObj.IsActive = true;
                            dbObj.AssistedBy = item.AssistedBy;

                            await _uowPatientDiagnoseProcedure.Repository.Insert(dbObj);
                            await _uowPatientDiagnoseProcedure.Save();
                        }

                    }
                    // END Delete diagnose Diagnose procedures



                    // Prescription Check
                    if (TokenService.GetUserLoggedInfo()!.SectionName == CommonStringConstant.DentalSurgeonOPD)
                    {
                        foreach (var itemPatientPrescription in input.PatientPrescriptions)
                        {
                            if (!AppCommonMethod.IsNullOrEmptyGuid(itemPatientPrescription.PatientPrescriptionId))
                                continue;

                            var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                            FillEntityPrescription(objPatientPrescription);

                            objPatientPrescription.PatientId = input.PatientId;
                            objPatientPrescription.PatientVisitId = input.PatientVisitId;
                            objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            if (dbObjPatientVisit!.IsFromPmis)
                                objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                            else
                                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                            await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                            await _uowPatientPrescription.Save();
                        }
                    }
                    else
                    {
                        // old working
                        //var dbListPatientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientDiagnoseId == objPatientDiagnose.PatientDiagnoseId && x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                        // Visit Edit Case
                        //foreach (var itemPatientPrescription in input.PatientPrescriptions)
                        //{
                        //    var dbObj = await _uowPatientPrescription.Repository.GetById(itemPatientPrescription.PatientPrescriptionId);

                        //    if (dbObj != null)
                        //    {
                        //        FillEntityPrescription(dbObj);

                        //        if (dbObjPatientVisit!.IsFromPmis)
                        //            dbObj.PrescribedBy = dbObjPatientVisit.AttendedBy;
                        //        else
                        //            dbObj.PrescribedBy = _tokenService.GetUserId();

                        //        _uowPatientPrescription.Repository.Update(dbObj);
                        //    }
                        //    else
                        //    {

                        //        var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);

                        //        objPatientPrescription.PatientId = input.PatientId;
                        //        objPatientPrescription.PatientVisitId = input.PatientVisitId;
                        //        objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        //        if (dbObjPatientVisit!.IsFromPmis)
                        //            objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                        //        else
                        //            objPatientPrescription.PrescribedBy = _tokenService.GetUserId();


                        //        FillEntityPrescription(objPatientPrescription);
                        //        await _uowPatientPrescription.Repository.Insert(objPatientPrescription);

                        //    }

                        //    await _uowPatientPrescription.Save();
                        //}
                        // old working

                        var dbPatientPrescriptions = await _uowPatientPrescription.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId! && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                        //soft delete Record
                        foreach (var patientPrescription in dbPatientPrescriptions.ToList())
                        {
                            if (!input.PatientPrescriptions.Any(c => c.PatientPrescriptionId == patientPrescription.PatientPrescriptionId))
                            {
                                patientPrescription.DeletedBy = _tokenService.GetUserId();
                                patientPrescription.DeletedOn = DateTime.Now;
                                patientPrescription.ActionTypeId = (int)ActionTypeEnum.Deleted;
                                _uowPatientPrescription.Repository.Update(patientPrescription);
                                //await _uowPatientLabTest.Save();
                            }
                        }

                        // Update and Insert PatientPrescription
                        foreach (var patientPrescription in input.PatientPrescriptions.ToList())
                        {
                            var dbPatientPrescription = dbPatientPrescriptions
                                .Where(c => c.PatientPrescriptionId == patientPrescription.PatientPrescriptionId && c.PatientPrescriptionId != default(Guid))
                                .SingleOrDefault();

                            if (dbPatientPrescription != null)
                            {
                                // Update child

                                //FillEntityPrescription(dbPatientPrescription);
                                //_uowPatientPrescription.Repository.Update(dbPatientPrescription);
                            }
                            else
                            {
                                // Insert child

                                var objPatientPrescription = _mapper.Map<PatientPrescription>(patientPrescription);

                                objPatientPrescription.PatientId = input.PatientId;
                                objPatientPrescription.PatientVisitId = input.PatientVisitId;
                                objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                                if (dbObjPatientVisit!.IsFromPmis)
                                    objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                                else
                                    objPatientPrescription.PrescribedBy = _tokenService.GetUserId();


                                FillEntityPrescription(objPatientPrescription);
                                await _uowPatientPrescription.Repository.Insert(objPatientPrescription);

                            }
                        }
                        await _uowPatientPrescription.Save();
                    }
                    //Patient Priscription

                    // old working
                    // Lab Check

                    //foreach (var itemPatientLabTests in input.PatientLabTests)
                    //{
                    //    itemPatientLabTests.PatientLabTestId = null;

                    //    var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId)
                    //        .Include(x => x.LabTestDetails)
                    //        .FirstOrDefaultAsync();

                    //    var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);

                    //    var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                    //    // If Test Sample is not Required
                    //    if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                    //    {
                    //        objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                    //        FillEntitySampleCollection(objPatientLabTest);
                    //    }

                    //    FillEntityLab(objPatientLabTest);

                    //    objPatientLabTest.PatientId = input.PatientId;
                    //    objPatientLabTest.PatientVisitId = input.PatientVisitId;

                    //    if (dbObjPatientVisit!.IsFromPmis)
                    //        objPatientLabTest.TestAdvisedBy = dbObjPatientVisit.AttendedBy;
                    //    else
                    //        objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                    //    //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                    //    objPatientLabTest.IsArchived = false;
                    //    objPatientLabTest.IsActive = true;

                    //    objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(objPatientLabTest);
                    //    objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //    foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                    //    {
                    //        itemdbLabTestDetail.IsActive = true;
                    //        FillEntityLabDetail(itemdbLabTestDetail);
                    //        objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                    //    }

                    //    await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                    //    await _uowPatientLabTest.Save();

                    //}
                    // old working

                    // Lab Test
                    var dbPatientLabTests = await _uowPatientLabTest.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId! && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();
                    //var _uowSection = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                    //var depName = "";
                    //if (_isGynaeOPD)
                    //{
                    //    depName = await _uowSection.Repository.GetALL(x => x.SectionLookupId == user.SectionId).Select(x => x.Name).FirstOrDefaultAsync();
                    //}
                    //soft delete Record
                    foreach (var patientLabTest in dbPatientLabTests.ToList())
                    {
                        if (!input.PatientLabTests.Any(c => c.PatientLabTestId == patientLabTest.PatientLabTestId))
                        {
                            patientLabTest.DeletedBy = _tokenService.GetUserId();
                            patientLabTest.DeletedOn = DateTime.Now;
                            patientLabTest.ActionTypeId = (int)ActionTypeEnum.Deleted;
                            _uowPatientLabTest.Repository.Update(patientLabTest);
                            //await _uowPatientLabTest.Save();
                        }
                    }

                    // Update and Insert Lab Test
                    //if(TokenService.GetUserLoggedInfo().SectionName == CommonConstant.d)
                    objPatientDiagnose.IsGynaePatient = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).Select(x => x.IsGynaePatient).FirstOrDefaultAsync();
                    int count = 0;
                    foreach (var patientLabTest in input.PatientLabTests.ToList())
                    {
                        var dbPatientLabTest = dbPatientLabTests
                            .Where(c => c.PatientLabTestId == patientLabTest.PatientLabTestId && c.PatientLabTestId != default(Guid))
                            .SingleOrDefault();



                        if (dbPatientLabTest != null)
                        {
                            // Update child

                            //_uowPatientLabTest.Repository.Update(patientLabTest);
                            //await _uowPatientLabTest.Save();
                            if (input!.IsGyanePatient != objPatientDiagnose.IsGynaePatient)
                            {
                                count++;
                                var labTestObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != 3 && x.TestPrice > 0 && (x.IsPaid == true || x.IsSampleCollected == true || x.IsReportGenerated == true)).FirstOrDefaultAsync();
                                if (!AppCommonMethod.IsNullObject(labTestObj))
                                    throw new UserFriendlyException(CommonMessageConstant.FeePaidForSomeLabTests);

                                if (dbPatientLabTest.IsPaid == true)
                                    throw new UserFriendlyException(CommonMessageConstant.FeePaidForSomeLabTests);
                                else if (dbPatientLabTest.IsSampleCollected == true && dbPatientLabTest.IsReportGenerated == false)
                                    throw new UserFriendlyException(CommonMessageConstant.FeePaidForSomeLabTests);
                                else if (dbPatientLabTest.IsSampleCollected == true && dbPatientLabTest.IsReportGenerated == true)
                                    throw new UserFriendlyException(CommonMessageConstant.FeePaidForSomeLabTests);

                                if (input!.IsGyanePatient)
                                {
                                    //    var labTestList = await _uowPatientLabTest.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != 3 && x.TestPrice > 0).ToListAsync();
                                    //    foreach (var lab in labTestList)
                                    //    {
                                    dbPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                                    dbPatientLabTest.DiscountedPrice = (decimal)dbPatientLabTest.TestPrice;
                                    dbPatientLabTest.TestPrice = (decimal)0;// set Price to Zero
                                    dbPatientLabTest.DiscountedByProfileId = await _uowProfile.Repository.GetALL(x => x.ShortName == "GYNDTR").Select(x => x.ProfileId).FirstOrDefaultAsync();


                                    _uowPatientLabTest.Repository.Update(dbPatientLabTest);
                                    await _uowPatientLabTest.Save();
                                    //}
                                }
                                else
                                {
                                    //var labTestList = await _uowPatientLabTest.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != 3 && x.DiscountInPercentage == 100).ToListAsync();
                                    //foreach (var lab in labTestList)
                                    //{
                                    decimal testPrice = (decimal)await _uowLabTest.Repository.GetALL(x => x.LabTestId == dbPatientLabTest.LabTestId).Select(x => x.TestPrice).FirstOrDefaultAsync();
                                    dbPatientLabTest.DiscountInPercentage = (decimal)0;
                                    dbPatientLabTest.DiscountedPrice = (decimal)0;
                                    dbPatientLabTest.TestPrice = (decimal)testPrice;// set Price to Zero
                                    dbPatientLabTest.DiscountedByProfileId = null;
                                    _uowPatientLabTest.Repository.Update(dbPatientLabTest);
                                    await _uowPatientLabTest.Save();
                                    //}
                                }
                            }
                        }
                        else
                        {
                            // Insert child

                            var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == patientLabTest.LabTestId)
                            .Include(x => x.LabTestDetails)
                            .FirstOrDefaultAsync();

                            var configIsPerformedPrivately = await _uowHfLabTestConfig.Repository.GetALL(x => x.LabTestId == patientLabTest.LabTestId
                                                    && x.HealthFacilityId == dbUser!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.IsPerformedPrivately).FirstOrDefaultAsync();

                            var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);

                            var objPatientLabTest = _mapper.Map<PatientLabTest>(patientLabTest);

                            // If Test Sample is not Required
                            if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                            {
                                objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                                FillEntitySampleCollection(objPatientLabTest);
                            }
                            objPatientLabTest.IsPerformedPrivately = (configIsPerformedPrivately == true) ? true : false;

                            //if (AppCommonMethod.IsNullorZeroDecimal(dbLabTest.TestPrice))
                            //{
                            //    objPatientLabTest.IsPaid = true;
                            //}

                            //if (_isGynaeOPD)
                            //{
                            //    if (depName == CommonStringConstant.GynaeOPD)
                            //    {
                            //        objPatientLabTest.IsPaid = true;
                            //        objPatientLabTest.TestPrice = 0;
                            //    }
                            //    else
                            //    {
                            //        objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                            //    }
                            //}
                            //else
                            //{
                            //    objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                            //}

                            // If gynae Patient then all lab test will be free
                            if (input!.IsGyanePatient)
                            {
                                objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                                objPatientLabTest.DiscountedPrice = (decimal)dbLabTest.TestPrice; // set Price to Zero
                                objPatientLabTest.TestPrice = (decimal)0;// set Price to Zero
                                objPatientLabTest.DiscountedByProfileId = await _uowProfile.Repository.GetALL(x => x.ShortName == "GYNDTR").Select(x => x.ProfileId).FirstOrDefaultAsync();

                            }

                            // If Flag IsFreeLabTest = true set in Section then set Lab Test as 100% Discount (as Free)
                            if (dbUser!.IsFreeLabTest == true)
                            {

                                // set Discound 100 % to set Test as Free
                                objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                                objPatientLabTest.DiscountedPrice = (decimal)0; // set Price to Zero

                            }

                            // If Flag IsSkipAlmoner = true set in Section then set Lab Test as 100% Discount and IsPaid=true (as Free & refer to Lab Direct)
                            if (dbUser!.IsSkipAlmoner == true)
                            {
                                // set Discound 100 % and IsPaid = true to redirect directly to Pending Collection
                                objPatientLabTest.IsPaid = true; // redirect to Pending Collection
                                objPatientLabTest.DiscountInPercentage = (decimal)100; // set Discount to 100%
                                objPatientLabTest.DiscountedPrice = (decimal)0; // set Price to Zero
                            }

                            if (!input!.IsGyanePatient)
                                objPatientLabTest.TestPrice = dbLabTest.TestPrice;
                            FillEntityLab(objPatientLabTest);
                            objPatientLabTest.PatientId = input.PatientId;
                            objPatientLabTest.PatientVisitId = input.PatientVisitId;

                            if (dbObjPatientVisit!.IsFromPmis)
                                objPatientLabTest.TestAdvisedBy = dbObjPatientVisit.AttendedBy;
                            else
                                objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                            //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                            objPatientLabTest.IsArchived = false;
                            objPatientLabTest.IsActive = true;

                            objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest!.LabTestId, _isOnline);
                            objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                            {
                                itemdbLabTestDetail.IsActive = true;
                                FillEntityLabDetail(itemdbLabTestDetail);
                                objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                            }



                            await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                        }

                        if (count == input.PatientLabTests.Count())
                        {
                            objPatientDiagnose.IsGynaePatient = input.IsGyanePatient;
                        }
                    }
                    await _uowPatientLabTest.Save();
                    // Lab Test
                    _uowPatientDiagnose.Repository.Update(objPatientDiagnose);
                    await _uowPatientDiagnose.Save();



                    // Patinet Diagnose Procedures

                    //foreach (var itemPatientDiagnoseProcedures in input.PatientDiagnoseProcedures)
                    //{
                    //    itemPatientDiagnoseProcedures.PatientDiagnoseProcedureId = itemPatientDiagnoseProcedures.PatientDiagnoseProcedureId;
                    //    itemPatientDiagnoseProcedures.PatientDiagnoseId = itemPatientDiagnoseProcedures.PatientDiagnoseId;
                    //    itemPatientDiagnoseProcedures.PerformedBy = itemPatientDiagnoseProcedures.PerformedBy;
                    //    itemPatientDiagnoseProcedures.RecommendBy = itemPatientDiagnoseProcedures.RecommendBy;
                    //    itemPatientDiagnoseProcedures.Feedback = itemPatientDiagnoseProcedures.Feedback;
                    //    itemPatientDiagnoseProcedures.IsPerformed = itemPatientDiagnoseProcedures.IsPerformed;


                    //    var objPatientDiagnoseProcedure = _mapper.Map<PatientDiagnoseProcedure>(itemPatientDiagnoseProcedures);
                    //    FillEntityDiagnoseProcedure(objPatientDiagnoseProcedure);

                    //    _uowPatientDiagnoseProcedure.Repository.Update(objPatientDiagnoseProcedure);
                    //    await _uowPatientDiagnoseProcedure.Save();

                    //}


                    if (input.IsRefer) // if refered
                    {

                        // Create Patient Visit Flow
                        CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                        //objPatientDiagnoseReferLog.CurrentDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        //objPatientDiagnoseReferLog.CurrentSectionId = dbObjPatientVisit.SectionLookupId;

                        //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                        //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                        objPatientVisitFlow.CurrentDepartmentId = input.ReferDepartment;
                        objPatientVisitFlow.CurrentSectionId = input.ReferSection;

                        objPatientVisitFlow.PreviousDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        objPatientVisitFlow.PreviousSectionId = dbObjPatientVisit.SectionLookupId;

                        objPatientVisitFlow.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                        objPatientVisitFlow.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                        objPatientVisitFlow.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        objPatientVisitFlow.ReferedBy = _tokenService.GetUserId();

                        var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.ReferSection).FirstOrDefaultAsync();
                        if (sectionLookup != null)
                        {
                            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
                            objPatientVisitFlow.IsConsultant = sectionLookup!.IsConsultant;
                        }

                        await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;

                        if (input.ReferDepartment == dbObjPatientVisit.DepartementLookupId)
                        {
                            dbObjPatientVisit.IsReferred = true;
                            dbObjPatientVisit.ReferredHealthFacilityId = dbObjPatientVisit.HealthFacilityId;
                            dbObjPatientVisit.ReferredDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                            dbObjPatientVisit.ReferredSectionLookupId = dbObjPatientVisit.SectionLookupId;
                            dbObjPatientVisit.ReferredBy = _tokenService.GetUserId();

                            dbObjPatientVisit.DepartementLookupId = input.ReferDepartment;
                            dbObjPatientVisit.SectionLookupId = input.ReferSection;
                        }

                        dbObjPatientVisit.AttendedBy = null;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;

                        // Update Station In Case of Refer
                        var stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                    .OrderBy(x => x.SequenceNo)
                                .Select(x => new ViewStationDto
                                {
                                    StationProfileId = x.ProfileId,
                                    SequenceNo = x.SequenceNo,
                                    ShortName = x.ShortName,
                                    Name = x.Name
                                }).ToListAsync();

                        var currentStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        dbObjPatientVisit.CurrentStationProfileId = currentStation!.StationProfileId;

                        // Update Station In Case of Refer

                        // If Refer to Other Department
                        //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());
                        //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                        //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                        if (dbObjPatientVisit.ReferredDepartmentLookupId != dbObjPatientVisit.DepartementLookupId)
                        {
                            //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment == CommonStringConstant.IPD)
                            //{
                            var userInfo = TokenService.GetUserLoggedInfo();

                            dbObjPatientVisit.IsAdmittedInIpd = false;
                            dbObjPatientVisit.IsReferredIpd = true;
                            dbObjPatientVisit.IpdDepartmentLookupId = input.ReferDepartment;
                            dbObjPatientVisit.IpdSectionLookupId = input.ReferSection;
                            dbObjPatientVisit.IpdReferredBy = _tokenService.GetUserId();
                            dbObjPatientVisit.IpdReferredByDepartmentLookupId = userInfo!.DepartmentId;
                            dbObjPatientVisit.IpdReferredBySectionLookupId = userInfo!.SectionId;
                            //}
                        }

                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        //objPatientWorkFlowLog.IsVisitClose = true;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);
                    }
                    else if (input.IsVisitClose)
                    {
                        // if visit close then also send to pharmacy for print & not close visit
                        var stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                    .OrderBy(x => x.SequenceNo)
                                .Select(x => new ViewStationDto
                                {
                                    StationProfileId = x.ProfileId,
                                    SequenceNo = x.SequenceNo,
                                    ShortName = x.ShortName,
                                    Name = x.Name
                                }).ToListAsync();

                        var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        if (AppCommonMethod.IsNullObject(thisStation))
                            throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                        var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                        if (AppCommonMethod.IsNullObject(nextStation))
                        {
                            //isVisitClose = true;
                            nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                            // SMS on Visit Close
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};


                            //_smsService.SendSMS(smsObj);
                        }


                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        //dbObjPatientVisit!.IsDischarge = true;

                        if (input.FormType == CommonStringConstant.TbForm)
                            dbObjPatientVisit!.IsDischarge = true;
                        else
                            dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;

                        if (dbObjPatientVisit.IsReferred == true)
                        {
                            dbObjPatientVisit.IsReferred = null;
                            dbObjPatientVisit.ReferredHealthFacilityId = null;
                            dbObjPatientVisit.ReferredDepartmentLookupId = null;
                            dbObjPatientVisit.ReferredSectionLookupId = null;
                            dbObjPatientVisit.ReferredBy = null;
                        }


                        //remove refer if not Admitted
                        if (dbObjPatientVisit.IsReferredIpd == true)
                        {
                            //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                            //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                            //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                            //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment != CommonStringConstant.IPD)
                            //{
                            if (dbObjPatientVisit.IsAdmittedInIpd == true)
                                throw new UserFriendlyException(CommonMessageConstant.PatientAdmittedAgainstThisRefer);
                            else
                            {
                                dbObjPatientVisit.IsAdmittedInIpd = null;
                                dbObjPatientVisit.IsReferredIpd = null;
                                dbObjPatientVisit.IpdDepartmentLookupId = null;
                                dbObjPatientVisit.IpdSectionLookupId = null;
                                dbObjPatientVisit.IpdReferredBy = null;
                                dbObjPatientVisit.IpdReferredByDepartmentLookupId = null;
                                dbObjPatientVisit.IpdReferredBySectionLookupId = null;
                            }
                            //}
                        }


                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                        // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                        //SendSMSDto smsObj = new SendSMSDto()
                        //{
                        //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                        //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                        //};

                        //_smsService.SendSMS(smsObj);

                    }
                    else
                    {
                        //check for next station
                        var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                        var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        if (AppCommonMethod.IsNullObject(thisStation))
                            throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                        var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                        if (AppCommonMethod.IsNullObject(nextStation))
                        {
                            //isVisitClose = true;
                            nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                            // SMS on Visit Close
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};


                            //_smsService.SendSMS(smsObj);
                        }



                        //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                        //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        if (dbObjPatientVisit.IsReferred == true)
                        {
                            dbObjPatientVisit.IsReferred = null;
                            dbObjPatientVisit.ReferredHealthFacilityId = null;
                            dbObjPatientVisit.ReferredDepartmentLookupId = null;
                            dbObjPatientVisit.ReferredSectionLookupId = null;
                            dbObjPatientVisit.ReferredBy = null;
                        }


                        //remove refer if not Admitted
                        if (dbObjPatientVisit.IsReferredIpd == true)
                        {
                            //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                            //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                            //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                            //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment != CommonStringConstant.IPD)
                            //{
                            if (dbObjPatientVisit.IsAdmittedInIpd == true)
                                throw new UserFriendlyException(CommonMessageConstant.PatientAdmittedAgainstThisRefer);
                            else
                            {
                                dbObjPatientVisit.IsAdmittedInIpd = null;
                                dbObjPatientVisit.IsReferredIpd = null;
                                dbObjPatientVisit.IpdDepartmentLookupId = null;
                                dbObjPatientVisit.IpdSectionLookupId = null;
                                dbObjPatientVisit.IpdReferredBy = null;
                                dbObjPatientVisit.IpdReferredByDepartmentLookupId = null;
                                dbObjPatientVisit.IpdReferredBySectionLookupId = null;
                            }
                            //}
                        }

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                        dbObjPatientVisit.IsDischarge = isVisitClose;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                    }

                    // Store Json Object
                    var jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        dbPatientDiagnoseRecord = await _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).FirstOrDefaultAsync();

                    }

                    // CASE IF
                    dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input.FormType;
                    dbPatientDiagnoseRecord.Json = jsonObj;
                    dbPatientDiagnoseRecord.IsActive = true;
                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        _uowPatientDiagnoseRecord.Repository.Update(dbPatientDiagnoseRecord!);
                    }
                    else
                    {
                        await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    }

                    await _uowPatientDiagnoseRecord.Save();


                    // Create Patient Visit Flow

                    //CreateOrEditPatientVisitFlowDto objPatientDiagnoseReferLog = new CreateOrEditPatientVisitFlowDto();

                    //objPatientDiagnoseReferLog.CurrentDepartmentId = input.ReferDepartment == null ? dbObjPatientVisit.DepartementLookupId : input.ReferDepartment;
                    //objPatientDiagnoseReferLog.CurrentSectionId = input.ReferSection == null ? dbObjPatientVisit.SectionLookupId : input.ReferSection;

                    //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                    //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                    //objPatientDiagnoseReferLog.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                    //objPatientDiagnoseReferLog.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                    //objPatientDiagnoseReferLog.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //await _patientVisitFlowService.CreateOrEdit(objPatientDiagnoseReferLog);


                    trans.Commit();
                    var diseaseList = input.PatientDiagnoseDiseases.Select(x => x.DiseaseProfileId).ToArray();

                    var isMesslesDisease = _uowProfile!.Repository.GetALL(x => diseaseList.Contains(x.ProfileId) && measlesDiseaseList.Contains(x.ShortName)).Count();

                    //if (TokenService.GetUserLoggedInfo().UserRoleList[0].ShortName.ToLower() == RoleConst.Measles.ToLower())
                    //{
                    if (isMesslesDisease > 0)
                    {
                        await EmrSuspectPatientRequest(dbPatient, input);
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
        }

        public async Task EmrSuspectPatientRequest(DbModel.Patient patient , CreateOrEditPatientDiagnoseWithPrescriptionDto input )
        {
            var location = await _uowProfile.GetDbContext().ViewHfLocations.Where(x => x.TehsilId == patient.TehsilId).FirstOrDefaultAsync();

            EMRRequestForSuspectedPatientDto obj = new EMRRequestForSuspectedPatientDto();

            obj.HealthFacility_Id = Convert.ToInt64(TokenService.GetHfHrId().ToString());
            obj.FirstName = patient.FullName;
            obj.PhoneNumber = patient.MobileNo;
            obj.RelativeName = patient.NameOfCnicHolder;
            obj.RelativeRelation = (await _uowProfile.Repository.GetById(patient.RelationProfileId)).Name;
            obj.AddressLine1 = patient.ParmanentAddress;
            obj.AddressProvince = location.ProvinceName;
            obj.AddressDivision = location.DivisionName;
            obj.AddressDistrict = location.DistrictName;
            obj.AddressTehsil = location.TehsilName;
            obj.UC = patient.UnionCouncilId.ToString();
            obj.DateOfBirth = patient.Dob;
            obj.CnicNumber = patient.Cnic;
            obj.Gender = _uowProfile.Repository.GetById(patient.GenderProfileId).Result.Name;
            obj.PatientRegId = patient.PatientId;

            foreach (var item in input.PatientDiagnoseDiseases)
            {
                DbModel.Profile profile = await _uowProfile.Repository.GetById(item.DiseaseProfileId);
                PatientVisitDiseaseViewModelDto disease = new PatientVisitDiseaseViewModelDto();
                disease.DiseaseName = profile.Name;
                obj.Diseases.Add(disease);
            }
            _emrService.EMRRequestForSuspectedPatient(obj);
        }
    public async Task CreatePatientLabTest(List<CreateOrEditPatientLabTestDto> PatientLabTests)
        {
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    foreach (var itemPatientLabTests in PatientLabTests)
                    {
                        var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                        var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientDiagnose.GetDbContext());

                        var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId && x.ActionTypeId != 3)
                             .Include(x => x.LabTestDetails.Where(x => x.ActionTypeId != 3))
                             .FirstOrDefaultAsync();

                        PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(itemPatientLabTests!.PatientVisitId!);

                        var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);
                        var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                        // If Test Sample is not Required
                        if (!string.IsNullOrEmpty(dbObjPatientVisit!.VisitFor) && dbObjPatientVisit.VisitFor == CommonStringConstant.ER)
                        {
                            objPatientLabTest.IsPaid = true;
                            objPatientLabTest.DiscountInPercentage = 100;
                            objPatientLabTest.DiscountedPrice = 0;
                        }

                        // If Test Sample is not Required
                        if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                        {
                            objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                            FillEntitySampleCollection(objPatientLabTest);
                        }
                        FillEntityLab(objPatientLabTest);

                        objPatientLabTest.PatientId = itemPatientLabTests.PatientId;
                        objPatientLabTest.PatientVisitId = itemPatientLabTests.PatientVisitId;
                        objPatientLabTest.PatientDiagnoseId = itemPatientLabTests.PatientDiagnoseId;

                        objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                        objPatientLabTest.IsArchived = false;
                        objPatientLabTest.IsActive = true;

                        objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit!.HealthFacilityId, objPatientLabTest!.LabTestId, _isOnline);
                        //objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(objPatientLabTest);

                        foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                        {
                            itemdbLabTestDetail.IsActive = true;
                            FillEntityLabDetail(itemdbLabTestDetail);
                            objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                        }

                        await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                        await _uowPatientLabTest.Save();

                    }

                    var dbUser = TokenService.GetUserLoggedInfo();

                    if (dbUser.IsDoctor == true)
                    {
                        CreateOrEditNursingEventDto objNursingEvents = new CreateOrEditNursingEventDto();

                        objNursingEvents.PatientId = PatientLabTests.FirstOrDefault()!.PatientId;
                        objNursingEvents.PatientDiagnoseId = PatientLabTests.FirstOrDefault()!.PatientDiagnoseId;
                        objNursingEvents.PatientVisitId = PatientLabTests.FirstOrDefault()!.PatientVisitId;
                        objNursingEvents.Events = CommonPrases.LabTestAdvised;
                          
                        await _nursingEventsService.CreateEvent(objNursingEvents);
                    }

                    var _uowPatientDiagnosisRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());
                    PatientDiagnosisRecord objPatientDiagnoseRecord = new PatientDiagnosisRecord();
                    
                    PatientDiagnosisRecord? dbPatientDiagnoseRecord = await _uowPatientDiagnosisRecord.Repository.GetALL(x => x.PatientDiagnoseId == PatientLabTests.FirstOrDefault()!.PatientDiagnoseId).FirstOrDefaultAsync();
                    var jsonObj = await GetJsonObj(PatientLabTests.FirstOrDefault()!.PatientVisitId!, CommonStringConstant.GeneralForm, PatientLabTests.FirstOrDefault()!.PatientDiagnoseId);

                    if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                        objPatientDiagnoseRecord = dbPatientDiagnoseRecord!;

                    FillEntityDiagnoseRecord(objPatientDiagnoseRecord!);

                    if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
                    {
                        objPatientDiagnoseRecord.Json = jsonObj;
                        objPatientDiagnoseRecord.IsActive = true;
                        objPatientDiagnoseRecord.UpdatedBy = _tokenService.GetUserId();
                        objPatientDiagnoseRecord.UpdatedOn = DateTime.Now;
                        _uowPatientDiagnosisRecord.Repository.Update(objPatientDiagnoseRecord!);
                    }
                    else
                    {
                        objPatientDiagnoseRecord.PatientId = PatientLabTests!.FirstOrDefault()!.PatientId! ?? Guid.Empty;
                        objPatientDiagnoseRecord.PatientVisitId = PatientLabTests.FirstOrDefault()!.PatientVisitId! ?? Guid.Empty;
                        objPatientDiagnoseRecord.PatientDiagnoseId = PatientLabTests.FirstOrDefault()!.PatientDiagnoseId! ?? Guid.Empty;
                        objPatientDiagnoseRecord.FormType = CommonStringConstant.GeneralForm;
                        objPatientDiagnoseRecord.Json = jsonObj;
                        objPatientDiagnoseRecord.IsActive = true;
                        objPatientDiagnoseRecord.CreatedOn = DateTime.Now;
                        objPatientDiagnoseRecord.CreatedBy = _tokenService.GetUserId();
                        await _uowPatientDiagnosisRecord.Repository.Insert(objPatientDiagnoseRecord!);
                        await _uowPatientDiagnosisRecord.CommitAsync();
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public async Task UpdatePatientDiagnoseRecordJsonObj(Guid? diagnoseId)
        {
            var dbContextPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineDispatch.GetDbContext());

            var currentRecord = await dbContextPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefaultAsync();

            var jsonObj = JsonConvert.DeserializeObject<ViewPatientSlipDetailsDto>(currentRecord.Json);

            var medicineDispatches = await _uowMedicineDispatch.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).ToListAsync();

            List<Domain.Models.DTO.PatientDto.MedicineDispatchDto> medicineDispatch = new List<Domain.Models.DTO.PatientDto.MedicineDispatchDto>();

            foreach (var item in jsonObj.PatientMedicine)
            {
                medicineDispatch.Add(new Domain.Models.DTO.PatientDto.MedicineDispatchDto
                {
                    //MedicineDispatchId 
                    MedicineId = item.MedicineId,
                    MedicineName = item.MedicineName,
                    Days = item.Days,
                    DoseName = item.DoseName,
                    DoseTimeName = item.DoseTimeName,
                    Quantity = item.Quantity,
                    AvailableQuantity = item.AvailableQuantity,
                    MedicineDose = item.MedicineDose,
                    MedicineRoute = item.MedicineRoute,
                    MedicineFrequency = item.MedicineFrequency,
                    MedicineInstruction = item.MedicineInstruction,
                    MedicineDuration = item.MedicineDuration,
                    BatchNo = item.BatchNo,
                    QuantityPrescribed = medicineDispatches.Where(x => x.MedicineId == item.MedicineId && x.PatientPrescriptionId == item.PatientPrescriptionId).Select(x => x.QuantityPrescribed).FirstOrDefault(),
                    QuantityDispatch = medicineDispatches.Where(x => x.MedicineId == item.MedicineId && x.PatientPrescriptionId == item.PatientPrescriptionId).Select(x => x.QuantityDispatch).FirstOrDefault(),
                });
            }

            jsonObj.MedicineDispatch = medicineDispatch;

            currentRecord.Json = JsonConvert.SerializeObject(jsonObj);

            var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineDispatch.GetDbContext());


            currentRecord.UpdatedBy = _tokenService.GetUserId();
            currentRecord.UpdatedOn = DateTime.Now;
            currentRecord.ActionTypeId = (int)ActionTypeEnum.Edit;

            _uowPatientDiagnoseRecord.Repository.Update(currentRecord!);
            await _uowPatientDiagnoseRecord.CommitAsync();

        }


        public async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> CreatePatientDiagnoseWithPrescriptionExternaly(CreateOrEditPatientDiagnoseWithPrescriptionDto input, UnitOfWork<PatientDiagnose> _uowPatientDiagnoseInput = null)
        {
            try
            {

                if (input.FollowupDate != null)
                    input.FollowupDate = input.FollowupDate.Value.AddHours(5);

                //var isVisitClose = false;
                //var tokenUserId = _tokenService.GetUserId();

                var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnoseInput.GetDbContext());

                var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnoseInput.GetDbContext());

                var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnoseInput.GetDbContext());
                var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnoseInput.GetDbContext());

                //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

                //DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                //if (AppCommonMethod.IsNullObject(dbPatient))
                //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                //dbPatient!.FollowupDate = input.FollowupDate;

                //_uowPatient.Repository.Update(dbPatient);
                //await _uowPatient.Save();

                var objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);
                FillEntityWithDetails(objPatientDiagnose);

                //if (dbObjPatientVisit!.IsFromPmis)
                //    objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                //else
                //    objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                //var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                //objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                //objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                objPatientDiagnose.PatientPrescriptions.Clear();
                objPatientDiagnose.PatientLabTests.Clear();

                input.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                objPatientDiagnose.FormType = CommonStringConstant.GeneralForm;

                await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                await _uowPatientDiagnose.Save();

                // Lab Check
                foreach (var itemPatientLabTests in input.PatientLabTests)
                {

                    var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId)
                            .Include(x => x.LabTestDetails)
                            .FirstOrDefaultAsync();

                    var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);

                    var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                    // If Test Sample is not Required
                    if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                    {
                        objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                        FillEntitySampleCollection(objPatientLabTest);
                    }



                    //var dbLabTestDetail = await _uowLabTestDetail.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId).ToListAsync();
                    //var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTestDetail);

                    //var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);
                    FillEntityLab(objPatientLabTest);

                    objPatientLabTest.PatientId = input.PatientId;
                    objPatientLabTest.PatientVisitId = input.PatientVisitId;
                    objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;


                    //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                    objPatientLabTest.IsArchived = false;
                    objPatientLabTest.IsActive = true;

                    //if (string.IsNullOrEmpty(itemPatientLabTests.BarcodeNo))
                    //    objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(objPatientLabTest);
                    //else
                    //    objPatientLabTest.BarcodeNo = itemPatientLabTests.BarcodeNo;

                    // For External Case Barcode is not generated use Pre Generated Barcode
                    objPatientLabTest.PreGeneratedBarcodeNo = itemPatientLabTests.BarcodeNo;
                    objPatientLabTest.SourceBarcode = itemPatientLabTests.SourceBarcode;

                    objPatientLabTest.TestPrice = dbLabTest.TestPrice;

                    // for now External Lab Test Are Consider as Free -- 31-01-24
                    if (_isExternallyAdvisedLabTestAreFree)
                    {
                        objPatientLabTest.IsPaid = true;
                        objPatientLabTest.DiscountInPercentage = (decimal)100;
                        objPatientLabTest.DiscountedPrice = (decimal)0;
                    }
                    else
                    {
                        objPatientLabTest.IsPaid = false;
                        //objPatientLabTest.DiscountInPercentage = (decimal)100;
                        //objPatientLabTest.DiscountedPrice = (decimal)0;
                    }

                    foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                    {
                        itemdbLabTestDetail.IsActive = true;
                        FillEntityLabDetail(itemdbLabTestDetail);
                        objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                    }

                    await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                    await _uowPatientLabTest.Save();
                }


                // Store Json Object
                var jsonObj = await GetJsonObj(input.PatientVisitId!, CommonStringConstant.GeneralForm, objPatientDiagnose.PatientDiagnoseId);

                PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                dbPatientDiagnoseRecord.FormType = CommonStringConstant.GeneralForm;
                dbPatientDiagnoseRecord.Json = jsonObj;
                dbPatientDiagnoseRecord.IsActive = true;

                await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                await _uowPatientDiagnoseRecord.Save();

            }
            catch (Exception)
            {
                throw;
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
        }


        public async Task<CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto> CreateOrEditPhysiotherapyForm(CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                return await CreatePhysiotherapyForm(input);
            else
                return input;
            //return await Update(input);
        }

        private async Task<CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto> CreatePhysiotherapyForm(CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto input)
        {
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    if (string.IsNullOrEmpty(input.FormType))
                        input.FormType = CommonStringConstant.GeneralForm;

                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();
                    //var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());
                    var _uowPhysiotherapyForm = new UnitOfWork<PhysiotherapyForm>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    dbPatient!.FollowupDate = input.FollowupDate;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();

                    // Patient Diagnose
                    PatientDiagnose objPatientDiagnose = new PatientDiagnose();

                    //var objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);
                    objPatientDiagnose.PatientId = input.PatientId;
                    objPatientDiagnose.PatientVisitId = input.PatientVisitId;
                    objPatientDiagnose.FollowupDate = input.FollowupDate;
                    objPatientDiagnose.FormType = input.FormType;



                    FillEntityWithDetails(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                        objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                    objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                    if (input.FormType == CommonStringConstant.PhysiotherapyFormIPD)
                    {
                        var countVisit = _uowPatientDiagnose.Repository.GetCount(x => x.PatientVisitId == input.PatientVisitId && x.DiagnosedBy == objPatientDiagnose.DiagnosedBy);
                        if (countVisit > 0)
                            objPatientDiagnose.DoctorVisitNo = ++countVisit;
                        else
                            objPatientDiagnose.DoctorVisitNo = 1; //Count Doctor Visits IPD and others in Case of OPD Default Value is 1
                    }
                    else
                        objPatientDiagnose.DoctorVisitNo = 1; //Count Doctor Visits IPD and others in Case of OPD Default Value is 1

                    objPatientDiagnose.IsActive = true;

                    objPatientDiagnose.PatientPrescriptions.Clear();

                    input.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    await _uowPatientDiagnose.Save();

                    // Physiotherapy Form

                    var objPhysiotherapyForm = _mapper.Map<PhysiotherapyForm>(input);
                    objPhysiotherapyForm.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    objPhysiotherapyForm.IsActive = true;

                    FillEntityWithForPhysiotherapyForm(objPhysiotherapyForm);

                    await _uowPhysiotherapyForm.Repository.Insert(objPhysiotherapyForm);
                    await _uowPhysiotherapyForm.Save();





                    foreach (var HomeExercises in input.HomeExercisePlanDropdown)
                    {

                        var _uowPhysiotherapyHomeExercisePlan = new UnitOfWork<PhysiotherapyHomeExercisePlan>(_uowPatient.GetDbContext());
                        PhysiotherapyHomeExercisePlan physiotherapyHomeExercisePlan = new PhysiotherapyHomeExercisePlan();





                        physiotherapyHomeExercisePlan = _mapper.Map<PhysiotherapyHomeExercisePlan>(HomeExercises);
                        physiotherapyHomeExercisePlan.PhysiotherapyFormId = objPhysiotherapyForm.PhysiotherapyFormId;

                        physiotherapyHomeExercisePlan.IsActive = true;



                        FillEntityPhysiotherapyHomeExercisesPlan(physiotherapyHomeExercisePlan);



                        await _uowPhysiotherapyHomeExercisePlan.Repository.Insert(physiotherapyHomeExercisePlan);

                        await _uowPhysiotherapyHomeExercisePlan.Save();
                    }

                    // Obsolete
                    // Prescription Check

                    //foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    //{
                    //    var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                    //    FillEntityPrescription(objPatientPrescription);

                    //    objPatientPrescription.PatientId = input.PatientId;
                    //    objPatientPrescription.PatientVisitId = input.PatientVisitId;
                    //    objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //    objPatientPrescription.IsActive = true;

                    //    if (dbObjPatientVisit!.IsFromPmis)
                    //        objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                    //    else
                    //        objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                    //    await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                    //    await _uowPatientPrescription.Save();
                    //}


                    if (input.IsVisitClose)
                    {

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        //dbObjPatientVisit!.IsDischarge = true;
                        dbObjPatientVisit!.VisitTypeProfileId = input.VisitTypeProfileId;
                        dbObjPatientVisit!.ReferredDepartmentLookupId = input.ReferredDepartmentLookupId;
                        dbObjPatientVisit!.ReferredSectionLookupId = input.ReferredSectionLookupId;
                        dbObjPatientVisit!.ReferredBy = input.ReferredBy;


                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                        // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                        //SendSMSDto smsObj = new SendSMSDto()
                        //{
                        //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                        //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                        //};

                        //_smsService.SendSMS(smsObj);

                    }
                    else
                    {
                        //check for next station
                        var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                        var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        if (AppCommonMethod.IsNullObject(thisStation))
                            throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                        var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                        if (AppCommonMethod.IsNullObject(nextStation))
                        {
                            //isVisitClose = true;
                            nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                            // SMS on Visit Close
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};

                            //_smsService.SendSMS(smsObj);
                        }

                        //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                        //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        dbObjPatientVisit!.VisitTypeProfileId = input.VisitTypeProfileId;
                        dbObjPatientVisit!.ReferredDepartmentLookupId = input.ReferredDepartmentLookupId;
                        dbObjPatientVisit!.ReferredSectionLookupId = input.ReferredSectionLookupId;
                        dbObjPatientVisit!.ReferredBy = input.ReferredBy;

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.IsDischarge = isVisitClose;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                    }


                    // Store Json Object
                    var jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    dbPatientDiagnoseRecord.PatientId = input!.PatientId!;
                    dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId!;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input.FormType;
                    dbPatientDiagnoseRecord.Json = jsonObj;
                    dbPatientDiagnoseRecord.IsActive = true;

                    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    await _uowPatientDiagnoseRecord.Save();

                    trans.Commit();

                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto>(input);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientDiagnose.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientDiagnose.Repository.Update(dbObj!);
            await _uowPatientDiagnose.CommitAsync();
            return true;
        }

        public async Task UpdateProcedureData(List<DentalProcedureListDTO> dentalProcedures)
        {
            var _uowPatientDiagnoseProcedure = new UnitOfWork<PatientDiagnoseProcedure>(_uowPatientDiagnose.GetDbContext());
            foreach (var dentalProcedure in dentalProcedures)
            {
                var data = await _uowPatientDiagnoseProcedure.Repository.GetById(dentalProcedure.PatientDiagnoseProcedureId);

                if (AppCommonMethod.IsNullObject(data))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                data.PerformedBy = _tokenService.GetUserId();
                data.IsPerformed = dentalProcedure.IsPerformed;
                data.AssistedBy = dentalProcedure.AssistedBy;
                data.Feedback = dentalProcedure.Feedback;
                FillEntityPatientDiagnoseProcedure(data);
                _uowPatientDiagnoseProcedure.Repository.Update(data);
                await _uowPatientDiagnoseProcedure.Save();
            }
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientDiagnoseDto>> GetAll(Expression<Func<PatientDiagnose, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<PatientDiagnose> responseObj = await _uowPatientDiagnose.Repository.GetALL(filter!).ToListAsync();
            return _mapper.Map<List<ViewPatientDiagnoseDto>>(responseObj);
        }

        public async Task<ViewPatientDiagnoseDto> GetById(Guid input)
        {
            PatientDiagnose? responseObj = await _uowPatientDiagnose.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDiagnoseDto>(responseObj);
        }
        public async Task<ViewPatientDiagnoseRecordDto> GetByIdWithPrescription(Guid input)
        {

            //PatientDiagnose? responseObj = await _uowPatientDiagnose.Repository.GetALL(x=>x.PatientDiagnoseId == input)
            //    .Include(x => x.Patient).ThenInclude(x=>x.GenderProfile)
            //    .Include(x => x.PatientDiagnoseDiseases)
            //    .Include(x=>x.PatientVisit).ThenInclude(x=>x.PatientVitals)
            //    .Include(x => x.PatientVisit).ThenInclude(x => x.PatientPrescriptions)
            //    .FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //return _mapper.Map<ViewPatientDiagnoseDto>(responseObj);


            var _uowPatientDiagnosisRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());
            var responseObj = await _uowPatientDiagnosisRecord.Repository.GetALL(x => x.PatientDiagnoseId == input).FirstOrDefaultAsync();

            var responseObj2 = _mapper.Map<ViewPatientDiagnoseRecordDto>(responseObj);

            responseObj2.MedicineDispatches = await _uowPatientDiagnose.GetDbContext().MedicineDispatches.Where(x => x.PatientVisitId == responseObj!.PatientVisitId)
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
            return responseObj2;
        }


        public async Task<List<ViewDoctorNote>> GetDoctorNotesByVisit(Guid PatientVisitId)
        {
            var doctorNotes = await _uowPatient.GetDbContext().ViewDoctorNotes.Where(x => x.PatientvisitId == PatientVisitId)
                                               .OrderByDescending(x => x.AdvisedOn).ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList(doctorNotes))
                return doctorNotes;

            return null;
        }

        public async Task<List<ViewPatientDoctorQueDto>> GetAllQue(int? HealthFacilityId)
        {
            var _uowUser = new UnitOfWork<User>(_uowPatientDiagnose.GetDbContext());
            //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            var dbUser = TokenService.GetUserLoggedInfo();

            //Doctor Station 
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientDiagnose.GetDbContext());
            Guid? doctorStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = new Object();
            if (dbUser.DesignationName == "SocialWelfareForm")
            {
                responseObj = await _uowPatientOpenVisit.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.SectionLookupId == dbUser.SectionId)

                // If IPD don't show until Patient is Admitted in IPD
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.InPatientDepartment), x => x.VisitFor == CommonStringConstant.IPD && x.IsAdmitted == true)
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.ERDepartment), x => x.VisitFor == CommonStringConstant.ER && x.IsAdmitted == true)

                // if  OPD or NULL then only show today patient and OPD patient
                .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitDate == DateTime.Today
                    && (x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null)
                    && x.CurrentStationProfileId == doctorStation
                    && (x.AttendedBy != null ? x.AttendedBy == _tokenService.GetUserId() : true))
                //.WhereIf((dbUser.DepartmentName == CommonStringConstant.ERDepartment),x=>x.IsDrugAddict == true)
                .Include(x => x.Patient)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.PatientConditionProfile)
                .Where(x =>
                        x.IsDischarge != true &&
                        x.HealthFacilityId == HealthFacilityId &&
                        (x.IsDrugAddict == true || dbUser.DesignationName == "SocialWelfareForm")
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientDoctorQueDto
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
                        DepartmentName = y.DepartementLookup!.Name,
                        SectionName = y.SectionLookup!.Name,
                        PatientCondition = y.PatientConditionProfile!.Name,
                        BedNo = y.BedNo,
                        VisitFor = y.VisitFor,
                        IsAlreadyAttended = (y.PatientDiagnoses.IsNullOrEmpty()) ? false : true,
                        IsSelfAttended = (y.PatientDiagnoses.Where(x => x.DiagnosedBy == _tokenService.GetUserId()).IsNullOrEmpty()) ? false : true,
                        //AttendedPatientDiagnoseId = (y.PatientDiagnoses.IsNullOrEmpty()) ? null: y.PatientDiagnoses.SingleOrDefault()!.PatientDiagnoseId,
                        AttendedDiagnoseLists = y.PatientDiagnoses.Select(x => new ViewAttendedDiagnoseListDto
                        {
                            PatientDiagnoseId = x.PatientDiagnoseId,
                            PatientVisitId = x.PatientVisitId,
                            DiagnosedBy = x.DiagnosedBy,
                            DiagnosedByName = x.DiagnosedByNavigation!.FullName,
                            DiagnosedByDesignation = x.DiagnosedByNavigation!.DesignationProfile!.Name,
                            Speciality = x.DocSectionLookup!.Name,
                            FormType = x.DocSectionLookup!.FormType,
                            CreatedOn = x.CreatedOn,
                        }).ToList()
                    }).ToListAsync();
            }
            else if (dbUser.FormType == CommonStringConstant.NCDClinicForm || dbUser.FormType == CommonStringConstant.MuawinClinicsForm)
            {
                var _uowSection = new UnitOfWork<SectionLookup>(_uowPatientDiagnose.GetDbContext());
                int ncdAndMuawinSectionId = await _uowSection.Repository.GetALL(x => x.FormType == CommonStringConstant.NcdAndMuawinClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                responseObj = await _uowPatientOpenVisit.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
                .WhereIf((!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId) && dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.SectionLookupId == dbUser.SectionId || x.SectionLookupId == ncdAndMuawinSectionId)

                // If IPD don't show until Patient is Admitted in IPD
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.InPatientDepartment), x => x.VisitFor == CommonStringConstant.IPD && x.IsAdmitted == true)
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.ERDepartment), x => x.VisitFor == CommonStringConstant.ER && x.IsAdmitted == true)

                // if  OPD or NULL then only show today patient and OPD patient
                .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitDate == DateTime.Today
                    && (x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null)
                    && x.CurrentStationProfileId == doctorStation
                    && (x.AttendedBy != null ? x.AttendedBy == _tokenService.GetUserId() : true))

                .Include(x => x.Patient)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.PatientConditionProfile)
                .Where(x =>
                        x.IsDischarge != true &&
                        x.HealthFacilityId == HealthFacilityId
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientDoctorQueDto
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
                        RelationProfileId = y.Patient.RelationProfileId,
                        DepartmentName = y.DepartementLookup!.Name,
                        SectionName = y.SectionLookup!.Name,
                        PatientCondition = y.PatientConditionProfile!.Name,
                        BedNo = y.BedNo,
                        VisitFor = y.VisitFor,
                        IsAlreadyAttended = (y.PatientDiagnoses.IsNullOrEmpty()) ? false : true,
                        IsSelfAttended = (y.PatientDiagnoses.Where(x => x.DiagnosedBy == _tokenService.GetUserId()).IsNullOrEmpty()) ? false : true,
                        //AttendedPatientDiagnoseId = (y.PatientDiagnoses.IsNullOrEmpty()) ? null: y.PatientDiagnoses.SingleOrDefault()!.PatientDiagnoseId,
                        AttendedDiagnoseLists = y.PatientDiagnoses.Select(x => new ViewAttendedDiagnoseListDto
                        {
                            PatientDiagnoseId = x.PatientDiagnoseId,
                            PatientVisitId = x.PatientVisitId,
                            DiagnosedBy = x.DiagnosedBy,
                            DiagnosedByName = x.DiagnosedByNavigation!.FullName,
                            DiagnosedByDesignation = x.DiagnosedByNavigation!.DesignationProfile!.Name,
                            Speciality = x.DocSectionLookup!.Name,
                            FormType = x.DocSectionLookup!.FormType,
                            CreatedOn = x.CreatedOn,
                        }).ToList()
                    }).ToListAsync();
            }
            else
            {
                responseObj = await _uowPatientOpenVisit.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.SectionLookupId == dbUser.SectionId)
                .WhereIf((!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId) && dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.SectionLookupId == dbUser.SectionId)

                // If IPD don't show until Patient is Admitted in IPD
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.InPatientDepartment), x => x.VisitFor == CommonStringConstant.IPD && x.IsAdmitted == true)
                .WhereIf((dbUser.DepartmentName == CommonStringConstant.ERDepartment), x => x.VisitFor == CommonStringConstant.ER && x.IsAdmitted == true)

                // if  OPD or NULL then only show today patient and OPD patient
                .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitDate == DateTime.Today
                    && (x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null)
                    && x.CurrentStationProfileId == doctorStation
                    && (x.AttendedBy != null ? x.AttendedBy == _tokenService.GetUserId() : true))

                .Include(x => x.Patient)
                .Include(x => x.PatientDiagnoses)
                .Include(x => x.PatientConditionProfile)
                .Where(x =>
                        x.IsDischarge != true &&
                        x.HealthFacilityId == HealthFacilityId
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientDoctorQueDto
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
                        RelationProfileId = y.Patient.RelationProfileId,
                        DepartmentName = y.DepartementLookup!.Name,
                        SectionName = y.SectionLookup!.Name,
                        PatientCondition = y.PatientConditionProfile!.Name,
                        BedNo = y.BedNo,
                        VisitFor = y.VisitFor,
                        IsAlreadyAttended = (y.PatientDiagnoses.IsNullOrEmpty()) ? false : true,
                        IsSelfAttended = (y.PatientDiagnoses.Where(x => x.DiagnosedBy == _tokenService.GetUserId()).IsNullOrEmpty()) ? false : true,
                        //AttendedPatientDiagnoseId = (y.PatientDiagnoses.IsNullOrEmpty()) ? null: y.PatientDiagnoses.SingleOrDefault()!.PatientDiagnoseId,
                        AttendedDiagnoseLists = y.PatientDiagnoses.Select(x => new ViewAttendedDiagnoseListDto
                        {
                            PatientDiagnoseId = x.PatientDiagnoseId,
                            PatientVisitId = x.PatientVisitId,
                            DiagnosedBy = x.DiagnosedBy,
                            DiagnosedByName = x.DiagnosedByNavigation!.FullName,
                            DiagnosedByDesignation = x.DiagnosedByNavigation!.DesignationProfile!.Name,
                            Speciality = x.DocSectionLookup!.Name,
                            FormType = x.DocSectionLookup!.FormType,
                            CreatedOn = x.CreatedOn,
                        }).ToList()
                    }).ToListAsync();
            }


            return _mapper.Map<List<ViewPatientDoctorQueDto>>(responseObj);
        }

        public async Task<List<GetAllMlcQueDatum>> GetAllQueMLC(int? HealthFacilityId)
        {
            var dbUser = TokenService.GetUserLoggedInfo();

            var lst = await _uowGetAllMlcQueData.Repository.GetALL()
                // if  OPD or NULL then only show today patient and OPD patient
                .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitDate == DateTime.Today
                    && (x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null))
                .Where(x => x.DoctorId == dbUser.UserId && x.HealthFacilityId == HealthFacilityId)
                .OrderBy(x => x.TokenNo)
                .ToListAsync();

            //var lst1 = lst.Where(x => x.DoctorId == dbUser.UserId).ToList();

            return _mapper.Map<List<GetAllMlcQueDatum>>(lst);
        }

        public async Task<ViewLabTestSlipDto> GetLabTestbyVisitId(Guid VisitId)
        {

            var responseObj = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == VisitId)
                .Include(x => x.DiagnosedByNavigation)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.PatientLabTests)
                        .ThenInclude(x => x.LabTest)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.HealthFacility)
                .Include(x => x.Patient)
                .Select(x => new ViewLabTestSlipDto
                {
                    HealthFacilityName = x.PatientVisit!.HealthFacility!.Name!,
                    PatientName = x.Patient!.FullName!,
                    TokenNo = x.PatientVisit!.TokenNo!,
                    VisitNo = x.PatientVisit.VisitNo,
                    VisitDate = x.PatientVisit.VisitDate,
                    CreatedOn = x.PatientVisit.CreatedOn,
                    AdvisedBy = x.DiagnosedByNavigation!.FullName!,
                    AdvisedByDesignation = x.DiagnosedByNavigation.DesignationProfile!.Name,
                    MrNo = x.Patient.Mrno!,

                    LabTests = x.PatientVisit.PatientLabTests
                    .Select(y => new LabTestDto
                    {
                        Name = y.LabTest!.Name!,
                        Price = y.LabTest!.TestPrice!,
                        Department = y.LabDepartmentProfile!.Name!,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            return responseObj;
        }

        public async Task<ViewLabTestSlipDto> GetLabTestbyDiagnoseId(Guid DiagnoseId)
        {

            var responseObj = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientDiagnoseId == DiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.DiagnosedByNavigation)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.HealthFacility)
                .Include(x => x!.PatientLabTests)
                        .ThenInclude(x => x.LabTest)
                .Include(x => x.Patient)
                .Select(x => new ViewLabTestSlipDto
                {
                    HealthFacilityName = x.PatientVisit!.HealthFacility!.Name!,
                    PatientName = x.Patient!.FullName!,
                    TokenNo = x.PatientVisit!.TokenNo!,
                    VisitNo = x.PatientVisit.VisitNo,
                    VisitDate = x.PatientVisit.VisitDate,
                    CreatedOn = x.PatientVisit.CreatedOn,
                    AdvisedBy = x.DiagnosedByNavigation!.FullName!,
                    AdvisedByDesignation = x.DiagnosedByNavigation.DesignationProfile!.Name,
                    MrNo = x.Patient.Mrno!,

                    LabTests = x.PatientLabTests
                    .Where(y => y.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .OrderBy(y => y.LabTest!.Name)
                    .Select(y => new LabTestDto
                    {
                        Name = y.LabTest!.Name!,
                        Price = y.LabTest!.TestPrice!,
                        Department = y.LabDepartmentProfile!.Name!,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            return responseObj;
        }


        public async Task<ViewPagerDto<ViewPatientDiagnoseDto>> GetAllWithPagination(FilterPatientVisitDto filter)
        {
            var diagnosedBy = _tokenService.GetUserId();
            var tokenUser = TokenService.GetUserLoggedInfo();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = _uowPatientDiagnose.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!TokenService.IsSuperAdmin() && tokenUser!.DepartmentName == CommonStringConstant.OutPatientDepartment, x => x.DiagnosedBy == diagnosedBy)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(tokenUser!.HealthFacilityId), x => x.PatientVisit!.HealthFacilityId == tokenUser!.HealthFacilityId)
                .WhereIf(filter.IsConsultant, x => x.DocSectionLookup!.IsConsultant == true)
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Patient!.Cnic.Replace("-", String.Empty) == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.Patient!.FullName!.StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.Patient!.MobileNo!.StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Patient!.Mrno!.StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedBy.ToString()!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.PatientVisit!.HealthFacility!.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.PatientVisit!.HealthFacility!.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.PatientVisit!.HealthFacility!.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.PatientVisit!.HealthFacility!.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.PatientVisit!.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DocDepartmentLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.DocSectionLookupId == filter.SectionId)


                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewPatientDiagnoseDto> IQueryableList = list.Select(y =>
                new ViewPatientDiagnoseDto
                {
                    PatientId = y.PatientId,
                    PatientVisitId = y.PatientVisitId,
                    PatientDiagnoseId = y.PatientDiagnoseId,
                    Mrno = y.Patient!.Mrno,
                    Cnic = y.Patient.Cnic,
                    FirstName = y.Patient.FirstName,
                    LastName = y.Patient.LastName,
                    FullName = y.Patient.FullName,
                    TokenNo = y.PatientVisit!.TokenNo,
                    VisitNo = y.PatientVisit!.VisitNo,
                    VisitDate = y.PatientVisit.VisitDate,
                    IsDischarge = y.PatientVisit.IsDischarge,
                    MobileNo = y.Patient.MobileNo,
                    HasLabTest = y.PatientLabTests.Count() > 0,
                });



            var pagedList = await PagedListDto<ViewPatientDiagnoseDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientDiagnoseDto>
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

        public async Task<ViewPagerDto<ViewPatientDiagnoseDto>> GetAllWithPaginationWithDetail(FilterPatientVisitDto filter)
        {
            var diagnosedBy = _tokenService.GetUserId();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var list = _uowPatientDiagnose.GetDbContext().PatientVisitFlows
                .WhereIf(!filter.IsConsultant, x => x.IsConsultant == false || x.IsConsultant == null)
                .WhereIf(filter.IsConsultant, x => x.IsConsultant == true)
                .WhereIf(!TokenService.IsSuperAdmin(), x => x.PatientDiagnose!.DiagnosedBy == diagnosedBy)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()), x => x.PatientVisit!.HealthFacilityId == TokenService.GetUserHfId())
                .WhereIf(filter.IsConsultant, x => x.IsConsultant == true)
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.PatientVisit!.Patient!.Cnic.Replace("-", String.Empty) == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.PatientVisit!.Patient!.FullName!.StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.PatientVisit!.Patient!.MobileNo!.StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.PatientVisit!.Patient!.Mrno!.StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedBy.ToString()!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.PatientVisit!.HealthFacility!.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.PatientVisit!.HealthFacility!.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.PatientVisit!.HealthFacility!.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.PatientVisit!.HealthFacility!.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.PatientVisit!.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.PatientVisit!.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.PatientVisit!.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewPatientDiagnoseDto> IQueryableList = list.Select(y =>
                new ViewPatientDiagnoseDto
                {
                    PatientId = y.PatientVisit!.PatientId,
                    PatientVisitId = y.PatientVisitId,
                    PatientDiagnoseId = y.PatientDiagnose != null ? y.PatientDiagnose!.PatientDiagnoseId : Guid.Empty,
                    Mrno = y.PatientVisit!.Patient!.Mrno,
                    Cnic = y.PatientVisit!.Patient.Cnic,
                    FirstName = y.PatientVisit!.Patient.FirstName,
                    LastName = y.PatientVisit!.Patient.LastName,
                    FullName = y.PatientVisit!.Patient.FullName,
                    TokenNo = y.PatientVisit!.TokenNo,
                    VisitNo = y.PatientVisit!.VisitNo,
                    VisitDate = y.PatientVisit!.VisitDate,
                    IsDischarge = y.PatientVisit!.IsDischarge,
                    MobileNo = y.PatientVisit!.Patient.MobileNo,
                    HasLabTest = y.PatientVisit!.PatientLabTests.Count() > 0,
                });



            var pagedList = await PagedListDto<ViewPatientDiagnoseDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientDiagnoseDto>
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

        public async Task<dynamic> UpdateJsonObj(Guid patientVisitId, string formType, Guid? diagnoseId, Guid patientId)
        {
            PatientDiagnosisRecord objPatientDiagnoseRecord = new PatientDiagnosisRecord();

            var _uowPatientDiagnosisRecord = new UnitOfWork<PatientDiagnosisRecord>();

            PatientDiagnosisRecord? dbPatientDiagnoseRecord = await _uowPatientDiagnosisRecord.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefaultAsync();
            var jsonObj = await GetJsonObj(patientVisitId, formType, diagnoseId);

            if (!AppCommonMethod.IsNullObject(dbPatientDiagnoseRecord))
            {
                objPatientDiagnoseRecord = dbPatientDiagnoseRecord!;
                FillEntityDiagnoseRecord(objPatientDiagnoseRecord!);
                _uowPatientDiagnosisRecord.Repository.Update(objPatientDiagnoseRecord!);
            }
            else
            {
                objPatientDiagnoseRecord.PatientId = patientId!;
                objPatientDiagnoseRecord.PatientVisitId = patientVisitId!;
                objPatientDiagnoseRecord.PatientDiagnoseId = diagnoseId ?? Guid.Empty;
                objPatientDiagnoseRecord.FormType = formType;
                objPatientDiagnoseRecord.Json = jsonObj;
                //objPatientDiagnoseRecord.IsActive = true;
                FillEntityDiagnoseRecord(objPatientDiagnoseRecord!);
                await _uowPatientDiagnosisRecord.Repository.Insert(objPatientDiagnoseRecord!);
            }
                
            await _uowPatientDiagnosisRecord.CommitAsync();
            return true;
        }

            public async Task<dynamic> GetJsonObj(Guid? input, string formType, Guid? diagnoseId)
        {
            if (formType == CommonStringConstant.PhysiotherapyFormOPD)
            {
                var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                .Select(y => new
                {
                    PatientOpenVisitId = y.PatientVisitId,
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
                    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.FollowupDate,
                    VisitTypeProfileId = y.PatientVisit.VisitTypeProfileId,
                    VisitTypeName = y.PatientVisit.VisitTypeProfile!.Name,

                    IsReferred = y.PatientVisit.IsReferred,
                    ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
                    ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
                    ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
                    ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

                    ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
                    ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
                    ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
                    ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

                    ReferredBy = y.PatientVisit.ReferredBy,
                    ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,

                    CreatedOn = y.CreatedOn,
                    CreatedBy = y.CreatedByNavigation!.FullName,

                    PhysiotherapistForm = y.PhysiotherapyForms.Select(x => new
                    {
                        x.PhysiotherapyFormId,
                        x.PatientDiagnoseId,
                        x.PresentingComplaint,
                        x.ProblemSince,
                        x.AnyComorbidity,
                        x.DrugHistory,
                        x.SignificantExaminationFindings,
                        x.TotalDurationOfTreatmentSession,
                        x.DischargeFromPhysicalTherapyTreatment,
                        x.HomeExercisePlan,
                        x.TreatmentAtDepartment,
                        x.Prognosis,
                        x.ClinicalDiagnosis,
                        x.PhysiotherapyDiagnosis,
                        x.PlanOfCare,
                        x.IsActive

                    }).FirstOrDefault(),

                    PhysiotherapyModalities = y.PhysiotherapyForms.FirstOrDefault()!.PhysiotherapyModalities.Select(x => new
                    {
                        PhysiotherapyModalitiesId = x.PhysiotherapyModalitiesId,
                        PhysiotherapyFormId = x.PhysiotherapyFormId,
                        DepartmentLookupId = x.DepartmentLookupId,
                        ModalitiesProfileId = x.ModalitiesProfileId,
                        ModalitiesProfileName = x.Name,
                        Value = x.Value,
                        IsActive = x.IsActive,

                    }).ToList(),

                    PatientVitals = y.PatientVisit.PatientVitals.OrderByDescending(x => x.CreatedOn).Select(x => new
                    {
                        PatientVitalId = x.PatientVitalId,
                        Bpsystolic = x.Bpsystolic,
                        BpdiaSystolic = x.BpdiaSystolic,
                        Pulse = x.Pulse,
                        Temprature = x.Temprature,
                        Weight = x.Weight,
                        Height = x.Height,
                        ResperatoryRate = x.ResperatoryRate,
                        VitalsCollectedBy = x.VitalsCollectedBy,
                        VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
                        IsActive = x.IsActive,
                        CreatedOn = x.CreatedOn

                    }).FirstOrDefault(),

                    //PatientMedicine = y.PatientDiagnoses.Where(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefault()!.PatientPrescriptions.Select(x => new PatientPrescriptionDto
                    //{
                    //    MedicineId = x.MedicineId,
                    //    Days = x.Days,
                    //    DoseName = x.DoseProfile!.Name,
                    //    DoseTimeName = x.DoseTimeProfile!.Name,
                    //    MedicineName = x.MedicineName,
                    //    PatientPrescriptionId = x.PatientPrescriptionId,
                    //    Quantity = x.Quantity,
                    //    AvailableQuantity = x.AvailableQuantity

                    //}).ToList(),

                }).FirstOrDefaultAsync();

                return JsonConvert.SerializeObject(patientVisit);

            }
            else if (formType == CommonStringConstant.PhysiotherapyFormIPD)
            {
                var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                .Select(y => new
                {

                    PatientOpenVisitId = y.PatientVisitId,
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
                    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.Patient.FollowupDate,
                    VisitTypeProfileId = y.PatientVisit.VisitTypeProfileId,
                    VisitTypeName = y.PatientVisit.VisitTypeProfile!.Name,

                    IsReferred = y.PatientVisit.IsReferred,
                    ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
                    ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
                    ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
                    ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

                    ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
                    ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
                    ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
                    ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

                    ReferredBy = y.PatientVisit.ReferredBy,
                    ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,

                    CreatedOn = y.CreatedOn,
                    CreatedBy = y.CreatedByNavigation!.FullName,

                    PhysiotherapistForm = y.PhysiotherapyForms.Select(x => new
                    {
                        x.PhysiotherapyFormId,
                        x.PatientDiagnoseId,
                        x.PresentingComplaint,
                        x.ProblemSince,
                        x.AnyComorbidity,
                        x.DrugHistory,
                        x.SignificantExaminationFindings,
                        x.TotalDurationOfTreatmentSession,
                        x.Prognosis,
                        x.ClinicalDiagnosis,
                        x.PhysiotherapyDiagnosis,
                        x.KeyTreatment,
                        x.PlanOfCare,

                        x.FrequencyOfExercise,
                        x.IntensityOfExercise,
                        x.TypeOfExercise,

                        x.DischargePlanOfCareProfileId,
                        DischargePlanOfCareProfileName = x.DischargePlanOfCareProfile!.Name,
                        x.IsActive

                    }).FirstOrDefault(),

                    PhysiotherapyModalities = y.PhysiotherapyForms.FirstOrDefault()!.PhysiotherapyModalities.Select(x => new
                    {
                        PhysiotherapyModalitiesId = x.PhysiotherapyModalitiesId,
                        PhysiotherapyFormId = x.PhysiotherapyFormId,
                        DepartmentLookupId = x.DepartmentLookupId,
                        ModalitiesProfileId = x.ModalitiesProfileId,
                        ModalitiesProfileName = x.Name,
                        Value = x.Value,
                        IsActive = x.IsActive,

                    }).ToList(),

                    PatientVitals = y.PatientVisit.PatientVitals.OrderByDescending(x => x.CreatedOn).Select(x => new
                    {
                        PatientVitalId = x.PatientVitalId,
                        Bpsystolic = x.Bpsystolic,
                        BpdiaSystolic = x.BpdiaSystolic,
                        Pulse = x.Pulse,
                        Temprature = x.Temprature,
                        Weight = x.Weight,
                        Height = x.Height,
                        ResperatoryRate = x.ResperatoryRate,
                        VitalsCollectedBy = x.VitalsCollectedBy,
                        VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
                        IsActive = x.IsActive,
                        CreatedOn = x.CreatedOn

                    }).FirstOrDefault(),

                    //PatientMedicine = y.PatientDiagnoses.Where(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefault()!.PatientPrescriptions.Select(x => new PatientPrescriptionDto
                    //{
                    //    MedicineId = x.MedicineId,
                    //    Days = x.Days,
                    //    DoseName = x.DoseProfile!.Name,
                    //    DoseTimeName = x.DoseTimeProfile!.Name,
                    //    MedicineName = x.MedicineName,
                    //    PatientPrescriptionId = x.PatientPrescriptionId,
                    //    Quantity = x.Quantity,
                    //    AvailableQuantity = x.AvailableQuantity

                    //}).ToList(),

                }).FirstOrDefaultAsync();

                return JsonConvert.SerializeObject(patientVisit);

            }
            else if (formType == CommonStringConstant.NutritionForm)
            {
                var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
                    PatientDiagnoseId = y.PatientDiagnoseId,
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
                    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
                    //NextVisitDate = y.FollowupDate,
                    PresentComplaints = y.PresentComplaints,
                    Examination = y.Examination,
                    PatientMedicalHistory = y.PatientMedicalHistory,
                    AdviseGiven = y.AdviseGiven,

                    IsReferred = y.PatientVisit.IsReferred,
                    ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
                    ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
                    ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
                    ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

                    ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
                    ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
                    ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
                    ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

                    ReferredBy = y.PatientVisit.ReferredBy,
                    ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

                    PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,
                    CreatedOn = y.CreatedOn,
                    CreatedBy = y.CreatedBy,
                    IsMlc = y.IsMlc,
                    IsSendToCdc = y.IsSendToCdc,
                    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToList(),

                    PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
                    {
                        LabTestId = x.LabTestId,
                        LabDepartmentProfileId = x.LabDepartmentProfileId,
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

                foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
                        patientVisit.DefinitiveDiseasesName += item.DiseasesName;
                    else
                        patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
                }


                foreach (var item in patientVisit!.PatientDiagnoseProcedures)
                {
                    if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
                        patientVisit.ProceduresName += item.ProcedureTitle;
                    else
                        patientVisit.ProceduresName += ", " + item.ProcedureTitle;
                }

                return JsonConvert.SerializeObject(patientVisit);

            }
            else if (
                formType == CommonStringConstant.SurgeryForm ||
                formType == CommonStringConstant.PsychiatryForm ||
                formType == CommonStringConstant.PsychologyForm ||
                formType == CommonStringConstant.SpeechTherapyForm ||
                formType == CommonStringConstant.OccupationalTherapyForm ||
                formType == CommonStringConstant.TechnologyForm
            )
            {
                var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
                    PatientDiagnoseId = y.PatientDiagnoseId,
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
                    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
                    //NextVisitDate = y.FollowupDate,
                    PresentComplaints = y.PresentComplaints,
                    Examination = y.Examination,
                    PatientMedicalHistory = y.PatientMedicalHistory,
                    AdviseGiven = y.AdviseGiven,

                    IsReferred = y.PatientVisit.IsReferred,
                    ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
                    ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
                    ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
                    ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

                    ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
                    ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
                    ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
                    ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

                    ReferredBy = y.PatientVisit.ReferredBy,
                    ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

                    PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,
                    CreatedOn = y.CreatedOn,
                    CreatedBy = y.CreatedBy,
                    IsMlc = y.IsMlc,
                    IsSendToCdc = y.IsSendToCdc,
                    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToList(),


                    //PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
                    //{
                    //    MedicineId = x.MedicineId,
                    //    Days = x.Days,
                    //    DoseName = x.DoseProfile!.Name,
                    //    DoseTimeName = x.DoseTimeProfile!.Name,
                    //    MedicineName = x.MedicineName,
                    //    PatientPrescriptionId = x.PatientPrescriptionId,
                    //    Quantity = x.Quantity,
                    //    AvailableQuantity = x.AvailableQuantity,
                    //    MedicineDose = x.MedicineDose,
                    //    MedicineRoute = x.MedicineRoute,
                    //    MedicineFrequency = x.MedicineFrequency,
                    //    MedicineInstruction = x.MedicineInstruction,
                    //    MedicineDuration = x.MedicineDuration,
                    //    BatchNo = x.BatchNo,
                    //    MedicineResourceProfileId = x.MedicineResourceProfileId,
                    //    MedicineTypeProfileId = x.MedicineTypeProfileId,
                    //    UnitPrice = x.UnitPrice
                    //}).ToList(),


                }).FirstOrDefaultAsync();


                foreach (var item in patientVisit!.PatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DiseasesName))
                        patientVisit.DiseasesName += item.DiseasesName;
                    else
                        patientVisit.DiseasesName += ", " + item.DiseasesName;
                }

                foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
                        patientVisit.DefinitiveDiseasesName += item.DiseasesName;
                    else
                        patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
                }


                foreach (var item in patientVisit!.PatientDiagnoseProcedures)
                {
                    if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
                        patientVisit.ProceduresName += item.ProcedureTitle;
                    else
                        patientVisit.ProceduresName += ", " + item.ProcedureTitle;
                }

                return JsonConvert.SerializeObject(patientVisit);
                //return patientVisit;
            }
            //else if (formType == CommonStringConstant.MuawinClinicsForm)
            //{
            //    var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
            //    .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
            //    .Select(y => new ViewPatientSlipDetailsDto
            //    {
            //        PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
            //        PatientDiagnoseId = y.PatientDiagnoseId,
            //        PatientId = y.PatientId ?? Guid.Empty,
            //        Mrno = y.Patient!.Mrno,
            //        Doctor = y.DiagnosedByNavigation!.FullName,
            //        DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
            //        Section = y.DocSectionLookup!.Name,
            //        Department = y.DocDepartmentLookup!.Name,
            //        PatientName = y.Patient.FullName,
            //        GurdianName = y.Patient.GuardianName,
            //        Age = y.Patient.Age,
            //        Dob = y.Patient.Dob,
            //        Gender = y.Patient.GenderProfile!.Name,
            //        CNIC = y.Patient.Cnic,
            //        ContactNo = y.Patient.MobileNo,
            //        Address = y.Patient.ParmanentAddress,
            //        VisitDate = y.CreatedOn,
            //        IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
            //        NextVisitDate = y.FollowupDate,
            //        FollowUpDate = y.FollowupDate,
            //        PresentComplaints = y.PresentComplaints,
            //        Examination = y.Examination,
            //        PatientMedicalHistory = y.PatientMedicalHistory,
            //        AdviseGiven = y.AdviseGiven,
            //        PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
            //        PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
            //        {
            //            MedicineId = x.MedicineId,
            //            Days = x.Days,
            //            DoseName = x.DoseProfile!.Name,
            //            DoseTimeName = x.DoseTimeProfile!.Name,
            //            MedicineName = x.MedicineName,
            //            PatientPrescriptionId = x.PatientPrescriptionId,
            //            Quantity = x.Quantity,
            //            AvailableQuantity = x.AvailableQuantity,
            //            MedicineDose = x.MedicineDose,
            //            MedicineRoute = x.MedicineRoute,
            //            MedicineFrequency = x.MedicineFrequency,
            //            MedicineInstruction = x.MedicineInstruction,
            //            MedicineDuration = x.MedicineDuration,
            //            BatchNo = x.BatchNo,
            //            MedicineResourceProfileId = x.MedicineResourceProfileId,
            //            MedicineTypeProfileId = x.MedicineTypeProfileId,
            //            UnitPrice = x.UnitPrice,
            //            IsSMLMedicine = x.IsSMLMedicine
            //        }).ToList(),
            //        PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
            //        {
            //            LabTestId = x.LabTestId,
            //            LabDepartmentProfileId = x.LabDepartmentProfileId,
            //            LabDepartmentName = x.LabDepartmentProfile!.Name,
            //            LabTestName = x.LabTest!.Name,
            //            PatientLabTestId = x.PatientLabTestId
            //        }).ToList(),
            //        PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
            //        {
            //            PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
            //            DiseaseProfileId = x.DiseaseProfileId,
            //            DiseasesName = x.DiseaseProfile.Name
            //        }).ToList()

            //    }).FirstOrDefaultAsync();
            //    return JsonConvert.SerializeObject(patientVisit);
            //}

            else
            { // General OPD

                var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
                .Select(y => new ViewPatientSlipDetailsDto
                {
                    PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
                    PatientDiagnoseId = y.PatientDiagnoseId,
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
                    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
                    NextVisitDate = y.FollowupDate,
                    FollowUpDate = y.FollowupDate,
                    PresentComplaints = y.PresentComplaints,
                    Examination = y.Examination,
                    PatientMedicalHistory = y.PatientMedicalHistory,
                    AdviseGiven = y.AdviseGiven,

                    IsReferred = y.IsRefer,
                    ReferredDepartmentLookupId = y.DocDepartmentLookupId,
                    ReferredDepartmentName = y.DocDepartmentLookup!.Name,
                    ReferredSectionLookupId = y.DocSectionLookupId,
                    ReferredSectionName = y.DocSectionLookup!.Name,


                    IsReferInternal = y.IsReferInternal,
                    ReferredToHealthFacilityId = (y.IsRefer == true) ? y.ReferToHealthFacilityId : null,
                    ReferredToHealthFacility = (y.IsRefer == true) ? y.ReferToHealthFacility!.Name : null,

                    ReferredToDepartmentLookupId = (y.IsRefer == true) ? y.ReferToDepartmentLookupId : null,
                    ReferredToDepartmentName = (y.IsRefer == true) ? y.ReferToDepartmentLookup!.Name : null,
                    ReferredToSectionLookupId = (y.IsRefer == true) ? y.ReferToSectionLookupId : null,
                    ReferredToSectionName = (y.IsRefer == true) ? y.ReferToSectionLookup!.Name : null,

                    ReferredBy = (y.IsRefer == true) ? y.CreatedBy : null,
                    ReferredByName = (y.IsRefer == true) ? y.CreatedByNavigation!.FullName : null,

                    PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
                    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
                    TokenNo = y.PatientVisit.TokenNo,
                    CreatedOn = y.CreatedOn,
                    CreatedBy = y.CreatedBy,
                    IsMlc = y.IsMlc,
                    IsSendToCdc = y.IsSendToCdc,
                    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToList(),
                    DefinitivePatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.DiagnoseTypeId == 2).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToList(),

                    PatientDiagnoseProcedures = y.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
                    {
                        PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
                        PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
                        SectionProcedureId = x.SectionProcedureId,
                        ProcedureTitle = x.SectionProcedure!.ProcedureTitle,
                        ProcedureFee = x.ProcedureFee,
                        IsPerformed = x.IsPerformed,
                        Feedback = x.Feedback,
                        RecommendBy = x.RecommendBy,
                        PerformedBy = x.PerformedBy,
                        ToothNumber = x.ToothNumber,
                        ToothPosition = x.ToothPosition,
                        AssistedBy = x.AssistedBy,


                    }).ToList(),
                    PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
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
                        MedicineDuration = x.MedicineDuration,
                        BatchNo = x.BatchNo,
                        MedicineResourceProfileId = x.MedicineResourceProfileId,
                        MedicineTypeProfileId = x.MedicineTypeProfileId,
                        UnitPrice = x.UnitPrice,
                        IsSMLMedicine = x.IsSMLMedicine
                    }).ToList(),
                    PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
                    {
                        LabTestId = x.LabTestId,
                        LabDepartmentProfileId = x.LabDepartmentProfileId,
                        LabDepartmentName = x.LabDepartmentProfile!.Name,
                        LabTestName = x.LabTest!.Name,
                        PatientLabTestId = x.PatientLabTestId
                    }).ToList(),
                    
                }).FirstOrDefaultAsync();

                var _uowDoctorNotes = new UnitOfWork<DoctorNote>(_uowPatientDiagnose.GetDbContext());
                var Notes = await _uowDoctorNotes.Repository.GetALL(x => x.PatientDiagnoseId == patientVisit!.PatientDiagnoseId).OrderByDescending(x => x.CreatedOn).ToListAsync();
                patientVisit!.DoctorNotes = _mapper.Map(Notes, patientVisit.DoctorNotes);

                foreach (var item in patientVisit!.PatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DiseasesName))
                        patientVisit.DiseasesName += item.DiseasesName;
                    else
                        patientVisit.DiseasesName += ", " + item.DiseasesName;
                }

                foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
                {
                    if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
                        patientVisit.DefinitiveDiseasesName += item.DiseasesName;
                    else
                        patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
                }


                foreach (var item in patientVisit!.PatientDiagnoseProcedures)
                {
                    if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
                        patientVisit.ProceduresName += item.ProcedureTitle;
                    else
                        patientVisit.ProceduresName += ", " + item.ProcedureTitle;
                }

                //if (!AppCommonMethod.IsNullOrEmptyList<PatientPrescriptionDto>(patientVisit.PatientMedicine.ToList()))
                //{
                //    var _uowMimsMedicineData = new UnitOfWork<MimsMedicineDatum>(_uowPatientDiagnose.GetDbContext());

                //    var medLookup = await _uowMimsMedicineData.Repository.GetALL().ToListAsync();

                //    foreach (var patientMedicine in patientVisit.PatientMedicine)
                //        patientMedicine.IsSMLMedicine = medLookup.Where(x => x.MedicineId == patientMedicine.MedicineId).Select(x => x.IsSMLMedicine).FirstOrDefault();
                //}

                return JsonConvert.SerializeObject(patientVisit);
                //return patientVisit;
            }
        }

        public async Task<Tuple<ViewPagerDto<DentalProcedurePatientListDTO>, List<DentalPatientProcedureListDTO>>> GetFilteredDentalProcedureList(FilterPatientDto filter)
        {
            List<DentalPatientProcedureListDTO> procedureList = new List<DentalPatientProcedureListDTO>();
            var responseObject = new ViewPagerDto<DentalProcedurePatientListDTO>();

            var conn = _uowPatientDiagnose.GetDbContext().Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPDentalProcedureList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.ToString());// DateTime.Now.Date.ToString());
                sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.ToString());

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                if (!string.IsNullOrEmpty(filter.Cnic))
                    sqlComm.Parameters.AddWithValue("@Cnic", filter.Cnic);


                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);


                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<ListTotalCount>();
                responseObject.List = ds.Tables[1].ToList<DentalProcedurePatientListDTO>();
                procedureList = ds.Tables[2].ToList<DentalPatientProcedureListDTO>();

                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                responseObject.PageSize = filter.PageSize;
                responseObject.CurrentPage = filter.PageNumber;
                responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                responseObject.HasPrevious = filter.PageNumber > 1;
                responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;

                return Tuple.Create(responseObject, procedureList);

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


		public async Task<List<EMRRequestForSuspectedPatientDto>> GetMeaslesPatientList(FilterMeaslesPatientDto filter)
		{
			List<MisealesPatientRecordDTO> patientList = new List<MisealesPatientRecordDTO>();
		

			var conn = _uowPatientDiagnose.GetDbContext().Database.GetDbConnection();
			try
			{
				DataSet ds = new DataSet();
				SqlCommand sqlComm = new SqlCommand("[dbo].[GETEmergencyMisclesPatients]", (SqlConnection)conn);
				sqlComm.CommandType = CommandType.StoredProcedure;

                sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.StartDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));// DateTime.Now.Date.ToString());
                sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss") : filter.EndDate.Value.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss"));

                SqlDataAdapter da = new SqlDataAdapter();
				da.SelectCommand = sqlComm;
				await Task.Run(() => da.Fill(ds));
				patientList = ds.Tables[0].ToList<MisealesPatientRecordDTO>();

				List<EMRRequestForSuspectedPatientDto> objList = new List<EMRRequestForSuspectedPatientDto>();
                foreach (var patient in patientList)
                {
					EMRRequestForSuspectedPatientDto obj = new EMRRequestForSuspectedPatientDto();

					obj.HealthFacility_Id = patient.HealthFacilityId;
					obj.FirstName = patient.FullName;
					obj.PhoneNumber = patient.MobileNo;
					obj.RelativeName = patient.NameOfCnicHolder;
					obj.RelativeRelation = patient.Relation;
					obj.AddressLine1 = patient.ParmanentAddress;
					obj.AddressProvince = patient.Province;
					obj.AddressDivision = patient.Division;
					obj.AddressDistrict = patient.District;
					obj.AddressTehsil = patient.Tehsil;
					obj.UC = string.Empty;
					obj.DateOfBirth = patient.DOB;
					obj.CnicNumber = patient.CNIC;
					obj.Gender = patient.Gender;
					obj.PatientRegId = patient.PatientID;
                    obj.DateTimeCreatedAt = patient.ReportedDate;
					
					PatientVisitDiseaseViewModelDto disease = new PatientVisitDiseaseViewModelDto();
					disease.DiseaseName = patient.DiseaseName;
					obj.Diseases.Add(disease);

                    objList.Add(obj);

					//var res = await _emrService.EMRRequestForSuspectedPatient(obj);

				}

				return objList;
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


		#endregion

		#region Helper Methods


		private void FillEntityDetialForPatientVisitFlow(PatientVisitFlow obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientVisitId))
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

        private void FillEntity(PatientDiagnose obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseId))
            {
                obj.PatientDiagnoseId = Guid.NewGuid();
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


        private void FillEntityDoctorNote(DoctorNote obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.DoctorNotesId))
            {
                obj.DoctorNotesId = Guid.NewGuid();
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

        private void FillEntityPatientStatusBySpeciality(PatientStatusBySpeciality obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientStatusBySpecialityId))
            {
                obj.PatientStatusBySpecialityId = Guid.NewGuid();
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

        private void FillEntityMedicineDispatch(MedicineDispatch obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.MedicineDispatchId))
            {
                obj.MedicineDispatchId = Guid.NewGuid();
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


        public void FillEntityPhysiotherapyHomeExercisesPlan(DbModel.PhysiotherapyHomeExercisePlan obj)
        {
            if (obj.PhysiotherapyHomeExercisePlanId == Guid.Empty)
            {
                obj.PhysiotherapyHomeExercisePlanId = Guid.NewGuid();
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


        private void FillEntityWithDetails(PatientDiagnose obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseId))
            {
                obj.IsActive = true;
                obj.PatientDiagnoseId = Guid.NewGuid();
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

            foreach (var objDiseases in obj.PatientDiagnoseDiseases)
            {
                if (objDiseases.PatientDiagnoseDiseaseId == Guid.Empty)
                {
                    objDiseases.PatientId = obj.PatientId;
                    objDiseases.PatientDiagnoseDiseaseId = Guid.NewGuid();
                    objDiseases.CreatedBy = _tokenService.GetUserId();
                    objDiseases.CreatedOn = DateTime.Now;
                    objDiseases.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    objDiseases.PatientId = obj.PatientId;
                    objDiseases.UpdatedBy = _tokenService.GetUserId();
                    objDiseases.UpdatedOn = DateTime.Now;
                    objDiseases.ActionTypeId = (int)ActionTypeEnum.Edit;
                }

            }

            foreach (var objDiseases in obj.PatientDiagnoseProcedures)
            {
                if (objDiseases.PatientDiagnoseProcedureId == Guid.Empty)
                {
                    objDiseases.PatientDiagnoseProcedureId = Guid.NewGuid();
                    objDiseases.PatientVisitId = obj.PatientVisitId;
                    objDiseases.CreatedBy = _tokenService.GetUserId();
                    objDiseases.CreatedOn = DateTime.Now;
                    objDiseases.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    objDiseases.UpdatedBy = _tokenService.GetUserId();
                    objDiseases.UpdatedOn = DateTime.Now;
                    objDiseases.ActionTypeId = (int)ActionTypeEnum.Edit;
                }

            }
        }

        private void FillEntityWithForPhysiotherapyForm(PhysiotherapyForm obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PhysiotherapyFormId))
            {
                obj.PhysiotherapyFormId = Guid.NewGuid();
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

            //foreach (var objHomeExercisePlan)
            //{

            //}

            foreach (var objModalities in obj.PhysiotherapyModalities)
            {
                if (objModalities.PhysiotherapyModalitiesId == Guid.Empty)
                {
                    objModalities.PhysiotherapyFormId = obj.PhysiotherapyFormId;

                    objModalities.PhysiotherapyModalitiesId = Guid.NewGuid();
                    objModalities.CreatedBy = _tokenService.GetUserId();
                    objModalities.CreatedOn = DateTime.Now;
                    objModalities.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    //objModalities.PhysiotherapyFormId = obj.PhysiotherapyFormId;
                    objModalities.UpdatedBy = _tokenService.GetUserId();
                    objModalities.UpdatedOn = DateTime.Now;
                    objModalities.ActionTypeId = (int)ActionTypeEnum.Edit;
                }

            }
        }
        private void FillEntityPrescription(PatientPrescription obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientPrescriptionId))
            {
                obj.PatientPrescriptionId = Guid.NewGuid();
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

        private void FillEntityDiagnoseProcedure(PatientDiagnoseProcedure obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseProcedureId))
            {
                obj.PatientDiagnoseProcedureId = Guid.NewGuid();
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

        private void FillEntityDiagnoseRecord(PatientDiagnosisRecord obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnosisRecordId))
            {
                obj.PatientDiagnosisRecordId = Guid.NewGuid();
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
        private void FillEntityLab(PatientLabTest obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientLabTestId))
            {
                obj.PatientLabTestId = Guid.NewGuid();
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

        private void FillEntitySampleCollection(PatientLabTest obj)
        {
            obj.IsSampleCollected = true;
            obj.SampleCollectedBy = _tokenService.GetUserId();
            obj.SampleCollectedOn = DateTime.Now;
        }

        private void FillEntityLabDetail(PatientLabTestDetail obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientLabTestDetailId))
            {
                obj.PatientLabTestDetailId = Guid.NewGuid();
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

        private void FillEntityDelete(PatientDiagnose obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        private void FillEntityPatientDiagnoseForPhysiotherapy(PatientDiagnose obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        private void FillEntityPatientDiagnoseReferLog(PatientDiagnoseReferLog obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseReferLogId))
            {
                obj.PatientDiagnoseReferLogId = Guid.NewGuid();
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

        private void FillEntityHCPScreening(PatientScreening obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientScreeningId))
            {
                obj.IsActive = true;
                obj.PatientScreeningId = Guid.NewGuid();
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
        private void FillEntityHCPAssessment(PatientAssessment obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientAssessmentId))
            {
                obj.PatientAssessmentId = Guid.NewGuid();
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
        private void FillEntityHCPVaccination(PatientVaccination obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientVaccinationId))
            {
                obj.PatientVaccinationId = Guid.NewGuid();
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


        private void FillEntityDevelopmentMilestone(DbModel.DevelopmentMilestone obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.DevelopmentMilestoneId))
            {
                obj.DevelopmentMilestoneId = Guid.NewGuid();
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



        private void FillEntitySpeechMilestone(DbModel.SpeechMilestone obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.SpeechMilestoneId))
            {
                obj.SpeechMilestoneId = Guid.NewGuid();
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



        private void FillEntitySpeechAndLanguageHistory(DbModel.SpeechAndLanguageHistory obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.SpeechAndLanguageHistoryId))
            {
                obj.SpeechAndLanguageHistoryId = Guid.NewGuid();
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


        private void FillEntityEducationalHistory(DbModel.EducationalHistory obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.EducationalHistoryId))
            {
                obj.EducationalHistoryId = Guid.NewGuid();
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

        private void FillEntityFamiliyHistory(DbModel.FamiliyHistory obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.FamiliyHistoryId))
            {
                obj.FamiliyHistoryId = Guid.NewGuid();
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


        private void FillEntityHearingProblemHistory(DbModel.HearingProblemHistory obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.HearingProblemHistoryId))
            {
                obj.HearingProblemHistoryId = Guid.NewGuid();
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


        private void FillEntitySpeechTherapyPatientAssessment(DbModel.SpeechTherapyPatientAssessment obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.SpeechTherapyPatientAssessmentId))
            {
                obj.SpeechTherapyPatientAssessmentId = Guid.NewGuid();
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

        private void FillEntitySpeechDisorder(DbModel.SpeechDisorder obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.SpeechDisorderId))
            {
                obj.SpeechDisorderId = Guid.NewGuid();
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


        private void FillEntityAssociatedDisorder(DbModel.AssociatedDisorder obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.AssociatedDisorderId))
            {
                obj.AssociatedDisorderId = Guid.NewGuid();
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


        private void FillEntitySpeechModality(DbModel.SpeechModality obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.SpeechModalitiesId))
            {
                obj.SpeechModalitiesId = Guid.NewGuid();
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



        private void FillEntityPastPsychiatricHistory(DbModel.PastPsychiatricHistory obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PastPsychiatricHistoryId))
            {
                obj.PastPsychiatricHistoryId = Guid.NewGuid();
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

        private void FillEntityPsychologicalAssessment(DbModel.PsychologicalAssessment obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PsychologicalAssessmentId))
            {
                obj.PsychologicalAssessmentId = Guid.NewGuid();
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

        private void FillEntityPsychologicalTestApplied(DbModel.PsychologicalTestApplied obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PsychologicalTestAppliedId))
            {
                obj.PsychologicalTestAppliedId = Guid.NewGuid();
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



        private void FillEntityPsychologyDisorder(DbModel.PsychologyDisorder obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PsychologyDisorderId))
            {
                obj.PsychologyDisorderId = Guid.NewGuid();
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



        private void FillEntityPsychologyAssociatedDisorder(DbModel.PsychologyAssociatedDisorder obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PsychologyAssociatedDisorderId))
            {
                obj.PsychologyAssociatedDisorderId = Guid.NewGuid();
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

        private void FillEntityPsychologyPatientModality(DbModel.PsychologyPatientModality obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PsychologyPatientModalityId))
            {
                obj.PsychologyPatientModalityId = Guid.NewGuid();
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


        private void FillEntityPatientBmi(DbModel.PatientBmi obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientBmiid))
            {
                obj.PatientBmiid = Guid.NewGuid();
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


        private void FillEntityPatientNutritionalRisk(DbModel.PatientNutritionalRisk obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientNutritionalRiskId))
            {
                obj.PatientNutritionalRiskId = Guid.NewGuid();
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

        private void FillEntityNutritionalAsessmentFinding(DbModel.NutritionalAsessmentFinding obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.NutritionalAsessmentFindingId))
            {
                obj.NutritionalAsessmentFindingId = Guid.NewGuid();
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


        private void FillEntityPatientComorbidity(DbModel.PatientComorbidity obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientComorbidityId))
            {
                obj.PatientComorbidityId = Guid.NewGuid();
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

        private void FillEntityPatientMalnutrition(DbModel.PatientMalnutrition obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientMalnutritionId))
            {
                obj.PatientMalnutritionId = Guid.NewGuid();
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

        private void FillEntityPatientDietPlan(DbModel.PatientDietPlan obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDietPlanId))
            {
                obj.PatientDietPlanId = Guid.NewGuid();
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



        private void FillEntityPatientDiagnoseProcedure(DbModel.PatientDiagnoseProcedure obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnoseProcedureId))
            {
                obj.PatientDiagnoseProcedureId = Guid.NewGuid();
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

        public async Task<string> GenerateBarcodeNo(int? HealthFacilityId, int? LabTestId, bool IsOnline)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[pt].[SPGetPatientLabTestBarcodeNo]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(LabTestId))
                        sqlComm.Parameters.AddWithValue("@LabTestId", LabTestId);

                    if (!AppCommonMethod.IsNullBool(IsOnline))
                        sqlComm.Parameters.AddWithValue("@IsOnline", IsOnline);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetSerialNoDto> lst = ds.Tables[0].ToList<GetSerialNoDto>();
                    //List<string> lst2 = ds.Tables[0].ToList<string>();
                    return lst.FirstOrDefault().BarcodeNo;

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

            //var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientDiagnose.GetDbContext());

            //PatientOpenVisit? patientOpenVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

            //var prefixHealthFacility = (patientOpenVisit!.HealthFacilityId!).ToString()!.PadLeft(4, '0');
            //var prefixLabTestId = (input.LabTestId!).ToString()!.PadLeft(4, '0');


            //var countHfwiseTest = _uowPatientLabTest.Repository.GetCount(x => x.BarcodeNo!.StartsWith(prefixHealthFacility + "-" + prefixLabTestId + "-"));

            //var barcodePostfixValue = (++countHfwiseTest).ToString().PadLeft(10, '0');


            //return prefixHealthFacility + "-" + prefixLabTestId + "-" + barcodePostfixValue;
        }

        public TEntity GetClone()
        {
            return (TEntity)this.MemberwiseClone();
        }

        #endregion

        #region Ipd

        public async Task<List<ViewPatientQueDto>> GetAllIpdQue(int? HealthFacilityId)
        {

            //var _uowUser = new UnitOfWork<User>(_uowPatientDiagnose.GetDbContext());
            //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            var user = TokenService.GetUserLoggedInfo();

            //Doctor Station 
            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientDiagnose.GetDbContext());
            //Guid? doctorStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.DoctorStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = await _uowPatientOpenVisit.GetDbContext().ViewGetAllIpdQueues
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(user!.DepartmentId), x => x.DepartementLookupId == user.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(user!.SectionId), x => x.SectionLookupId == user.SectionId)

                .Where(x =>
                        x.IsDischarge != true &&
                        //x.VisitDate == DateTime.Today &&
                        x.HealthFacilityId == HealthFacilityId &&
                        //x.CurrentStationProfileId == doctorStation &&
                        (x.OccupiedBy != null ? x.OccupiedBy == _tokenService.GetUserId() : true)
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y =>
                    new ViewPatientQueDto
                    {
                        PatientVisitId = y.PatientOpenVisitId,
                        PatientId = y.PatientId,
                        TokenNo = y.TokenNo,
                        Mrno = y.Mrno,
                        CNIC = y.Cnic,
                        MobileNo = y.MobileNo,
                        FirstName = y.FirstName,
                        LastName = y.LastName,
                        FullName = y.FullName,
                        DepartmentName = y.DepartmentName,
                        SectionName = y.SectionName,
                        BedNo = y.BedNo,
                    }).ToListAsync();

            return _mapper.Map<List<ViewPatientQueDto>>(responseObj);
        }


        public async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> CreateOrEditPatientDiagnoseWithPrescriptionForIpd(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                return await CreatePatientDiagnoseWithPrescriptionForIpd(input);
            else
                return await UpdatePatientDiagnoseWithPrescriptionForIpd(input);
        }


        private async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> CreatePatientDiagnoseWithPrescriptionForIpd(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {

            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    CreateOrEditPatientDiagnoseWithPrescriptionDto duplicatedDiagnose = null;

                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);
                    if (string.IsNullOrEmpty(input.FormType))
                    {
                        input.FormType = CommonStringConstant.GeneralForm;
                    }

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientScreening = new UnitOfWork<PatientScreening>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientAssessment = new UnitOfWork<PatientAssessment>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientVaccination = new UnitOfWork<PatientVaccination>(_uowPatientDiagnose.GetDbContext());

                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullorZeroInt(input.BedNo))
                        input.BedNo = dbObjPatientVisit!.BedNo;


                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //if (input.PatientLabTests.Count == 0 && input.FormType == CommonStringConstant.TbForm)

                    //Zulqarnain Working
                    if (input.PatientLabTests.Count == 0 && input.FormType == CommonStringConstant.TbForm && AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                    {
                        var PatientLabTestObj = await _uowPatientLabTest.Repository.GetALL(x => x.PatientId == input.PatientId).FirstOrDefaultAsync();

                        if (AppCommonMethod.IsNullObject(PatientLabTestObj))
                            throw new UserFriendlyException(CommonMessageConstant.NoLabTestFoundAgainstThisPatient);
                    }



                    //if(input.IsReferToDRTB && input.FormType == CommonStringConstant.TbForm)
                    //{
                    //    var _uowPatientVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatient.GetDbContext());
                    //    var currentVisit = await _uowPatientVisitFlow.GetDbContext().PatientOpenVisits.Where(x => x.PatientOpenVisitId == input.PatientVisitId).FirstOrDefaultAsync();
                    //    if (AppCommonMethod.IsNullObject(currentVisit))
                    //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //    var lastPatientDiagnose = await _uowPatientVisitFlow.GetDbContext().PatientDiagnoses
                    //        .Where(x => x.DocDepartmentLookupId == currentVisit.DepartementLookupId && x.DocSectionLookupId == currentVisit.SectionLookupId&& x.FollowupDate != null && x.PatientId == currentVisit!.PatientId)
                    //        .OrderByDescending(x => x.CreatedOn)
                    //        .FirstOrDefaultAsync();


                    //    if (!AppCommonMethod.IsNullObject( lastPatientDiagnose ))
                    //    {
                    //        var lastPatientVisitFlow = await _uowPatientVisitFlow.Repository.GetALL().Where(x =>
                    //        x.PatientVisitId == lastPatientDiagnose.PatientVisitId
                    //        && x.CurrentDepartmentId == currentVisit.DepartementLookupId
                    //        && x.CurrentSectionId == currentVisit.SectionLookupId && x.IsFollowUp == true)
                    //            .FirstOrDefaultAsync();


                    //        if (!AppCommonMethod.IsNullObject(lastPatientDiagnose))
                    //        {
                    //            FillEntity(lastPatientDiagnose);
                    //            lastPatientDiagnose!.FollowupDate = null;
                    //            _uowPatientDiagnose.Repository.Update(lastPatientDiagnose);
                    //            await _uowPatientDiagnose.Save();
                    //        }
                    //        if (!AppCommonMethod.IsNullObject(lastPatientVisitFlow))
                    //        {
                    //            FillEntityDetialForPatientVisitFlow(lastPatientVisitFlow);
                    //            lastPatientVisitFlow!.IsFollowUp = false;
                    //            lastPatientVisitFlow.FollowUpNo = -1;
                    //            lastPatientVisitFlow.LastVisitId = null;
                    //            _uowPatientVisitFlow.Repository.Update(lastPatientVisitFlow);
                    //            await _uowPatientVisitFlow.Save();
                    //        }
                    //    }

                    //    //var patientVisitFlowObj = await _uowPatientVisitFlow.Repository
                    //    //                            .GetALL(x => x.PatientVisitId == input.PatientVisitId)
                    //    //                            .FirstOrDefaultAsync();





                    //}

                    if (AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                        dbPatient!.FollowupDate = input.FollowupDate;
                    else
                        dbPatient!.FollowupDate = null;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();

                    var objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);
                    FillEntityWithDetails(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                    {
                        if (!AppCommonMethod.IsNullOrEmptyGuid(input.DiagnosedBy))
                            objPatientDiagnose.DiagnosedBy = input.DiagnosedBy;
                        else
                            objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();
                    }

                    // Need to refine remove db request
                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                    objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientLabTests.Clear();

                    input.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    objPatientDiagnose.FormType = input.FormType;

                    if (input.IsConfirmed)
                    {
                        //var dbObj = _uowPatientDiagnose.Repository.GetALL(x => x.PatientId == objPatientDiagnose.PatientId).ToList();
                        //if (!AppCommonMethod.IsNullOrEmptyList(dbObj))
                        //{
                        //    foreach (var item in dbObj)
                        //    {
                        //        if (!(bool)item.IsConfirmed)
                        //        {
                        //            item.IsConfirmed = true;
                        //        }
                        //    }
                        //}   
                        objPatientDiagnose.IsConfirmed = true;
                    }

                    //if (!string.IsNullOrEmpty(input.ReactionNote))
                    //{
                    //    objPatientDiagnose.AnyMedicineReaction = true;
                    //    objPatientDiagnose.ReactionNote = input.ReactionNote;
                    //}

                    //if (!AppCommonMethod.IsNullOrEmptyGuid(input.OutcomeStatusProfileId))
                    //{
                    //    objPatientDiagnose.OutcomeStatusProfileId = input.OutcomeStatusProfileId;
                    //    objPatientDiagnose.FollowupDate = null;
                    //}
                    if (input.IsRefer)
                    {
                        objPatientDiagnose.IsRefer = input.IsRefer;
                        objPatientDiagnose.ReferToDepartmentLookupId = input.ReferDepartment;
                        objPatientDiagnose.ReferToSectionLookupId = input.ReferSection;
                    }

                    await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    await _uowPatientDiagnose.Save();



                    //if (input.IsReferToDRTB && input.FormType == CommonStringConstant.TbForm)
                    //{

                    //    var _uowVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientDiagnose.GetDbContext());

                    //    var dbVisitFlow = await _uowVisitFlow.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();


                    //    var _uowTbPatientDetails = new UnitOfWork<TbPatientDetail>(_uowPatient.GetDbContext());
                    //    TbPatientDetail tbPatientDetail = new TbPatientDetail();
                    //    FillEntityTbPatientDetails(tbPatientDetail);
                    //    tbPatientDetail.PatientId = input.PatientId;
                    //    tbPatientDetail.PatientVisitId = input.PatientVisitId;
                    //    tbPatientDetail.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //    tbPatientDetail.Drtbcenter = input.DRTBCenter;
                    //    tbPatientDetail.PatientTreatmentCycleNo = dbVisitFlow?.PatientTreatmentCycleNo;
                    //    tbPatientDetail.IsReferToDrtb = true;
                    //    await _uowTbPatientDetails.Repository.Insert(tbPatientDetail);
                    //    await _uowTbPatientDetails.Save();

                    //}

                    //if (input.FormType == CommonStringConstant.TbForm && !AppCommonMethod.IsNullOrEmptyGuid(input.PatientStatusProfileId))
                    //{
                    //    await SavePatientStatusBySpeciality(input, objPatientDiagnose);
                    //}

                    //if (input.FormType == CommonStringConstant.TbForm && input.PatientPrescriptions.Count() > 0)
                    //{
                    //    await SavePatientMedicinessuedMonth(objPatientDiagnose);
                    //}



                    // Prescription Check
                    //if (input.FormType == CommonStringConstant.HCPForm)
                    //{
                    //    // Variable for HCP Medicine Dispatch
                    //    List<PatientPrescription> tempPatientPrescription = new List<PatientPrescription>();

                    //    foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    //    {
                    //        var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                    //        FillEntityPrescription(objPatientPrescription);

                    //        objPatientPrescription.PatientId = input.PatientId;
                    //        objPatientPrescription.PatientVisitId = input.PatientVisitId;
                    //        objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //        if (dbObjPatientVisit!.IsFromPmis)
                    //            objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                    //        else
                    //            objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                    //        await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                    //        await _uowPatientPrescription.Save();
                    //        // Assigning Prescription Object for HCP to Dispatch Kits
                    //        tempPatientPrescription.Add(objPatientPrescription);
                    //    }

                    //    #region HCP
                    //    //Adding code for HCV 
                    //    // Assigining Patient Diagnose Id to Patient Screening
                    //    if (input.PatientScreening.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                    //    {

                    //        input.IsActive = true;
                    //        input.PatientScreening.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //        var objPatientScreening = _mapper.Map<PatientScreening>(input.PatientScreening);
                    //        FillEntityHCPScreening(objPatientScreening);
                    //        await _uowPatientScreening.Repository.Insert(objPatientScreening);
                    //        await _uowPatientScreening.Save();

                    //        //// Now Dispatching Testing Kits for New-Diagnosed Patients
                    //        //if (input.PatientScreening.PatientType == "New Patient")
                    //        //{
                    //        if (input.PatientScreeningTestKitsDispense.MedicineDispatches.Count != 0)
                    //        {
                    //            // Assigning Patient Prescription ID to Dispatch Medicine Forign key
                    //            foreach (var prescriptionItem in tempPatientPrescription)
                    //            {
                    //                input.PatientScreeningTestKitsDispense.MedicineDispatches.FirstOrDefault(x => x.MedicineId == prescriptionItem.MedicineId).PatientPrescriptionId = prescriptionItem.PatientPrescriptionId;
                    //            }

                    //            foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                    //            {
                    //                item.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //            }
                    //            // here--
                    //            //var ScreeningTestKitsDispense = _MedicineDispatchService.CreatePatientDispatch(input.PatientScreeningTestKitsDispense);
                    //            foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                    //            {
                    //                List<MedicineDispenseDto> mimsMedicineList = new List<MedicineDispenseDto>();
                    //                mimsMedicineList.Add(new MedicineDispenseDto
                    //                {
                    //                    hfmisCode = TokenService.GetHfHrId(),//TokenService.GetUserHfCode(),  //
                    //                    Quantity = item.QuantityDispatch ?? 0,
                    //                    MedId = item.MedicineId,
                    //                    BatchNo = item.BatchNo!,
                    //                    WardId = Convert.ToInt32(TokenService.GetMimsDepartmentId())
                    //                });
                    //                var mimsDispatchResponse = await _mimsService.MedicineDespenseByHealthFacility(_mimsBaseUrl, mimsMedicineList);

                    //                //if (mimsDispatchResponse != null && mimsDispatchResponse.Status != "Medicine Not Found") // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                    //                if (mimsDispatchResponse != null && mimsDispatchResponse.Status)
                    //                {
                    //                    // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                    //                    item.MIMSDispatched = true;
                    //                }
                    //                if (mimsDispatchResponse.Data == null)
                    //                {
                    //                    item.Reason = mimsDispatchResponse.Message;
                    //                    throw new UserFriendlyException(CommonMessageConstant.TestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == true && input.PatientScreening.IsDiagnosedHbvrepidKit == null)
                    //                {
                    //                    item.Reason = "Kit not used";
                    //                    //throw new UserFriendlyException(CommonMessageConstant.HBVTestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHbvrepidKit != null)
                    //                {
                    //                    item.Reason = mimsDispatchResponse.Data[0].Reason;
                    //                    throw new UserFriendlyException(CommonMessageConstant.HBVTestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1681 && input.PatientScreening.HasHbvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHbvrepidKit == null)
                    //                {
                    //                    item.Reason = mimsDispatchResponse.Data[0].Reason;
                    //                    throw new UserFriendlyException(CommonMessageConstant.HBVTestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == true && input.PatientScreening.IsDiagnosedHcvrepidKit == null)
                    //                {
                    //                    item.Reason = "Kit not used";
                    //                    //throw new UserFriendlyException(CommonMessageConstant.HCVTestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHcvrepidKit != null)
                    //                {
                    //                    item.Reason = mimsDispatchResponse.Data[0].Reason;
                    //                    throw new UserFriendlyException(CommonMessageConstant.HCVTestingKitsNotFound);
                    //                }
                    //                if (mimsDispatchResponse.Data.Count > 0 && mimsDispatchResponse.Data[0].MedId == 1682 && input.PatientScreening.HasHcvpcrconfirmation == false && input.PatientScreening.IsDiagnosedHcvrepidKit == null)
                    //                {
                    //                    item.Reason = mimsDispatchResponse.Data[0].Reason;
                    //                    throw new UserFriendlyException(CommonMessageConstant.HCVTestingKitsNotFound);
                    //                }

                    //            }


                    //            //using (var tran = _uowMedicineDispatch.GetDbContext().Database.BeginTransaction())
                    //            //{
                    //            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowMedicineDispatch.GetDbContext());
                    //            var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineDispatch.GetDbContext());

                    //            var patientPrescription = await _uowPatientPrescription.Repository.GetALL().Where(x => x.PatientVisitId == input.PatientScreeningTestKitsDispense.PatientVisitId)
                    //                    .Include(x => x.DoseProfile).Include(x => x.DoseTimeProfile).ToListAsync();

                    //            if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                    //                throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

                    //            if (dbObjPatientVisit!.IsDischarge == true)
                    //                throw new UserFriendlyException(CommonMessageConstant.VisitClosed);

                    //            foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                    //            {
                    //                var obj = _mapper.Map<MedicineDispatch>(item);
                    //                FillEntityMedicineDispatch(obj);
                    //                obj.PatientId = input.PatientScreeningTestKitsDispense.PatientId;
                    //                obj.PatientVisitId = input.PatientScreeningTestKitsDispense.PatientVisitId;
                    //                obj.Pharmacist = _tokenService.GetUserId();
                    //                obj.PatientDiagnoseId = item.PatientDiagnoseId;
                    //                obj.PatientPrescriptionId = item.PatientPrescriptionId;
                    //                obj.WardId = Convert.ToInt32(TokenService.GetMimsDepartmentId());

                    //                await _uowMedicineDispatch.Repository.Insert(obj);
                    //                await _uowMedicineDispatch.Save();


                    //                //await UpdatePatientDiagnoseRecordJsonObj(item.PatientDiagnoseId);

                    //            }

                    //            //dbObjPatientVisit.IsOccupied = false;
                    //            //dbObjPatientVisit.OccupiedBy = null;
                    //            //dbObjPatientVisit.IsDischarge = true;
                    //            //dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                    //            //dbObjPatientVisit.UpdatedOn = DateTime.Now;
                    //            //dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    //            //_uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    //            //await _uowPatientOpenVisit.Save();

                    //            // Create Patient Work Log
                    //            //CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                    //            //objPatientWorkFlowLog.PatientId = input.PatientId;
                    //            //objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    //            //objPatientWorkFlowLog.HealthFacilityId = dbObjPatientVisit!.HealthFacilityId;
                    //            //objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                    //            //objPatientWorkFlowLog.NextStationProfileId = null;
                    //            //objPatientWorkFlowLog.IsVisitClose = true;

                    //            foreach (var item in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                    //            {
                    //                var prescription = patientPrescription.Where(x => x.MedicineId == item.MedicineId).FirstOrDefault();

                    //                if (item.AvailableQuantity < item.QuantityPrescribed)
                    //                {
                    //                    var remainingQty = item.QuantityPrescribed - item.QuantityDispatch;

                    //                }


                    //                //int i = 0;
                    //                //foreach (var medItem in input.PatientScreeningTestKitsDispense.MedicineDispatches)
                    //                //{

                    //                //    var MedPrescription = patientPrescription.Where(x => x.MedicineId == medItem.MedicineId).FirstOrDefault();

                    //                //    if (medItem.AvailableQuantity < medItem.QuantityPrescribed)
                    //                //    {
                    //                //        var remainingQty = medItem.QuantityPrescribed - medItem.QuantityDispatch;
                    //                //    }
                    //                //}




                    //                //objPatientWorkFlowLog.IsActive = true;
                    //                //await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);
                    //                //}
                    //                //return input;
                    //            }
                    //        }
                    //    }
                    //    // Assigining Patient Diagnose Id to Patient Assessment
                    //    if (input.PatientAssessment.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                    //    {
                    //        if (input.PatientAssessment.SurgeryDate != null)
                    //        {
                    //            input.PatientAssessment.SurgeryDate = input.PatientAssessment.SurgeryDate.Value.AddDays(5);
                    //        }
                    //        if (input.PatientAssessment.BloodTransfusionYear != null)
                    //        {
                    //            input.PatientAssessment.SurgeryDate = input.PatientAssessment.BloodTransfusionYear.Value.AddDays(5);
                    //        }
                    //        if (input.PatientAssessment.BloodTransfusionYear != null)
                    //        {
                    //            input.PatientAssessment.SurgeryDate = input.PatientAssessment.BloodTransfusionYear.Value.AddDays(5);
                    //        }
                    //        input.PatientAssessment.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //        var objPatientAssessment = _mapper.Map<PatientAssessment>(input.PatientAssessment);
                    //        FillEntityHCPAssessment(objPatientAssessment);

                    //        await _uowPatientAssessment.Repository.Insert(objPatientAssessment);
                    //        await _uowPatientAssessment.Save();
                    //    }
                    //    // Assigining Patient Diagnose Id to Patient Vaccination
                    //    if (input.PatientVaccination.PatientId != null && input.FormType == CommonStringConstant.HCPForm)
                    //    {
                    //        if (input.PatientVaccination.VaccinationDose1Date != null)
                    //        {
                    //            input.PatientVaccination.VaccinationDose1Date = input.PatientVaccination.VaccinationDose1Date.Value.AddHours(5);
                    //        }
                    //        if (input.PatientVaccination.VaccinationDose2Date != null)
                    //        {
                    //            input.PatientVaccination.VaccinationDose2Date = input.PatientVaccination.VaccinationDose2Date.Value.AddHours(5);
                    //        }
                    //        if (input.PatientVaccination.VaccinationDose3Date != null)
                    //        {
                    //            input.PatientVaccination.VaccinationDose3Date = input.PatientVaccination.VaccinationDose3Date.Value.AddHours(5);
                    //        }
                    //        input.PatientVaccination.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //        var objPatientVaccination = _mapper.Map<PatientVaccination>(input.PatientVaccination);
                    //        FillEntityHCPVaccination(objPatientVaccination);

                    //        await _uowPatientVaccination.Repository.Insert(objPatientVaccination);
                    //        await _uowPatientVaccination.Save();
                    //    }
                    //    #endregion

                    //}
                    //else
                    //{
                    foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    {
                        var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                        FillEntityPrescription(objPatientPrescription);

                        objPatientPrescription.PatientId = input.PatientId;
                        objPatientPrescription.PatientVisitId = input.PatientVisitId;
                        objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        if (dbObjPatientVisit!.IsFromPmis)
                            objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                        else
                        {
                            if (!AppCommonMethod.IsNullOrEmptyGuid(input.DiagnosedBy))
                                objPatientPrescription.PrescribedBy = input.DiagnosedBy;
                            else
                                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();
                        }

                        await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                        await _uowPatientPrescription.Save();
                    }
                    //}


                    // Patient Contact Check
                    //foreach (var itemPatientContactDetails in input.PatientContactDetails)
                    //{
                    //    var _uowPatientContactDetails = new UnitOfWork<PatientContactDetail>(_uowPatient.GetDbContext());
                    //    var objPatientContactDetails = _mapper.Map<PatientContactDetail>(itemPatientContactDetails);
                    //    FillEntityContactDetail(objPatientContactDetails);
                    //    if (!AppCommonMethod.IsNullObject(objPatientContactDetails))
                    //    {
                    //        //var patContacts = await _uowPatientContactDetails.Repository.GetALL(x => x.ContactNo == itemPatientContactDetails.ContactNo).FirstOrDefaultAsync();

                    //        //if (!AppCommonMethod.IsNullObject(patContacts))
                    //        //    throw new UserFriendlyException(CommonMessageConstant.PatientContactNoAlreadyExists);

                    //        var patOpenVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientId == objPatientContactDetails.PatientId).FirstOrDefaultAsync();
                    //        if (!(AppCommonMethod.IsNullObject(patOpenVisit)))
                    //        {
                    //            objPatientContactDetails.CollectedBy = null;
                    //            objPatientContactDetails.DepartmentLookupId = patOpenVisit.DepartementLookupId;
                    //            objPatientContactDetails.SectionLookupId = patOpenVisit.SectionLookupId;
                    //            await _uowPatientContactDetails.Repository.Insert(objPatientContactDetails);
                    //            await _uowPatientContactDetails.Save();
                    //        }
                    //    }
                    //}


                    //if (input.FormType == CommonStringConstant.TbForm)
                    //{
                    //    if (input.IsExpertTest)
                    //    {
                    //        var _uowVisitFlow = new UnitOfWork<PatientVisitFlow>(_uowPatientDiagnose.GetDbContext());

                    //        var dbVisitFlow = await _uowVisitFlow.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

                    //        if (AppCommonMethod.IsNullObject(dbVisitFlow) || dbVisitFlow!.IsFollowUp != true)
                    //        {
                    //            // Create Visit
                    //            var dbVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input.PatientVisitId).FirstOrDefaultAsync();

                    //            CreateOrEditOpenVisitDto newVisit = new CreateOrEditOpenVisitDto();
                    //            newVisit = _mapper.Map<CreateOrEditOpenVisitDto>(dbVisit);

                    //            newVisit.PatientOpenVisitId = null;
                    //            var response = await _patientOpenVisitService.CreateOrEditAutoVisit(newVisit);

                    //            // Create Patient Visit Flow
                    //            CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                    //            var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == response.SectionLookupId).FirstOrDefaultAsync();

                    //            objPatientVisitFlow.CurrentDepartmentId = response.DepartementLookupId;
                    //            objPatientVisitFlow.CurrentSectionId = response.SectionLookupId;

                    //            objPatientVisitFlow.PatientVisitId = response.PatientOpenVisitId;
                    //            objPatientVisitFlow.HealthFacilityId = response.HealthFacilityId;
                    //            objPatientVisitFlow.IsAutoGenerated = true;

                    //            objPatientVisitFlow.IsFilterClinic = (!AppCommonMethod.IsNullObject(sectionLookup)) ? sectionLookup!.IsFilterClinic : null;

                    //            await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                    //            List<CreateOrEditPatientLabTestDto> patientLabTest = new List<CreateOrEditPatientLabTestDto>();
                    //            patientLabTest.AddRange(input.PatientLabTests);

                    //            duplicatedDiagnose = _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
                    //            duplicatedDiagnose.PatientDiagnoseId = null;

                    //            duplicatedDiagnose.PatientVisitId = response.PatientOpenVisitId;
                    //            duplicatedDiagnose.IsAutoGenerated = true;

                    //            var xpertLabTest = await _uowLabTest.Repository.GetALL(x => x.Name == CommonStringConstant.XPert).FirstOrDefaultAsync();

                    //            // remove xpert test from First Visit
                    //            input.PatientLabTests.Remove(input.PatientLabTests.Single(x => x.LabTestId == xpertLabTest!.LabTestId));

                    //            // assign only Xpert Test for auto generated Visit
                    //            duplicatedDiagnose.PatientLabTests = duplicatedDiagnose.PatientLabTests.Where(x => x.LabTestId == xpertLabTest!.LabTestId).ToList();
                    //        }

                    //    }

                    //}

                    // Lab Check
                    foreach (var itemPatientLabTests in input.PatientLabTests)
                    {

                        var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId && x.ActionTypeId != 3)
                             .Include(x => x.LabTestDetails.Where(x => x.ActionTypeId != 3))
                             .FirstOrDefaultAsync();

                        //var dbLabTestDetail = await _uowLabTestDetail.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId).ToListAsync();

                        var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);
                        var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                        // If Test Sample is not Required
                        if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                        {
                            objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                            FillEntitySampleCollection(objPatientLabTest);
                        }
                        FillEntityLab(objPatientLabTest);

                        objPatientLabTest.PatientId = input.PatientId;
                        objPatientLabTest.PatientVisitId = input.PatientVisitId;
                        objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        if (dbObjPatientVisit!.IsFromPmis)
                            objPatientLabTest.TestAdvisedBy = dbObjPatientVisit.AttendedBy;
                        else
                        {
                            if (!AppCommonMethod.IsNullOrEmptyGuid(input.DiagnosedBy))
                                objPatientLabTest.TestAdvisedBy = input.DiagnosedBy;
                            else
                                objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                        }

                        //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                        objPatientLabTest.IsArchived = false;
                        objPatientLabTest.IsActive = true;

                        objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest.LabTestId, _isOnline);
                        //objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(objPatientLabTest);

                        foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                        {
                            itemdbLabTestDetail.IsActive = true;
                            FillEntityLabDetail(itemdbLabTestDetail);
                            objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                        }

                        await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                        await _uowPatientLabTest.Save();

                    }


                    if (input.IsRefer) // if refered
                    {

                        // Create Patient Visit Flow
                        CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                        //objPatientDiagnoseReferLog.CurrentDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        //objPatientDiagnoseReferLog.CurrentSectionId = dbObjPatientVisit.SectionLookupId;

                        //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                        //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                        objPatientVisitFlow.CurrentDepartmentId = input.ReferDepartment;
                        objPatientVisitFlow.CurrentSectionId = input.ReferSection;

                        objPatientVisitFlow.PreviousDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        objPatientVisitFlow.PreviousSectionId = dbObjPatientVisit.SectionLookupId;

                        objPatientVisitFlow.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                        objPatientVisitFlow.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                        objPatientVisitFlow.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        objPatientVisitFlow.ReferedBy = _tokenService.GetUserId();

                        var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.ReferSection).FirstOrDefaultAsync();
                        if (sectionLookup != null)
                        {
                            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
                            objPatientVisitFlow.IsConsultant = sectionLookup!.IsConsultant;
                        }


                        await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        if (input.ReferDepartment == dbObjPatientVisit.DepartementLookupId)
                        {
                            dbObjPatientVisit.IsReferred = true;
                            dbObjPatientVisit.ReferredHealthFacilityId = dbObjPatientVisit.HealthFacilityId;
                            dbObjPatientVisit.ReferredDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                            dbObjPatientVisit.ReferredSectionLookupId = dbObjPatientVisit.SectionLookupId;
                            dbObjPatientVisit.ReferredBy = _tokenService.GetUserId();

                            dbObjPatientVisit.DepartementLookupId = input.ReferDepartment;
                            dbObjPatientVisit.SectionLookupId = input.ReferSection;
                        }

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.AttendedBy = null;


                        // if refer to Other Department
                        //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                        //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                        //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                        //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment == CommonStringConstant.IPD)
                        if (dbObjPatientVisit.ReferredDepartmentLookupId != dbObjPatientVisit.DepartementLookupId)
                        {
                            var userInfo = TokenService.GetUserLoggedInfo();

                            dbObjPatientVisit.IsAdmittedInIpd = false;
                            dbObjPatientVisit.IsReferredIpd = true;
                            dbObjPatientVisit.IpdDepartmentLookupId = input.ReferDepartment;
                            dbObjPatientVisit.IpdSectionLookupId = input.ReferSection;
                            dbObjPatientVisit.IpdReferredBy = _tokenService.GetUserId();
                            dbObjPatientVisit.IpdReferredByDepartmentLookupId = userInfo!.DepartmentId;
                            dbObjPatientVisit.IpdReferredBySectionLookupId = userInfo!.SectionId;
                        }

                        dbObjPatientVisit.BedNo = input.BedNo;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        //objPatientWorkFlowLog.IsVisitClose = true;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                    }
                    else
                    { // if not refered


                        if (input.IsVisitClose)
                        {
                            //check for next station
                            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                                //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                            if (AppCommonMethod.IsNullObject(thisStation))
                                throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                            var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                            if (AppCommonMethod.IsNullObject(nextStation))
                            {
                                //isVisitClose = true;
                                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                                // SMS on Visit Close
                                //SendSMSDto smsObj = new SendSMSDto()
                                //{
                                //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                                //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                                //};


                                //_smsService.SendSMS(smsObj);
                            }



                            //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                            //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;


                            if (input.FormType == CommonStringConstant.TbForm)
                                dbObjPatientVisit!.IsDischarge = true;
                            else
                                dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;

                            dbObjPatientVisit.BedNo = input.BedNo;
                            dbObjPatientVisit.IsOccupied = false;
                            dbObjPatientVisit.OccupiedBy = null;
                            dbObjPatientVisit.AttendedBy = null;
                            dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                            dbObjPatientVisit.UpdatedOn = DateTime.Now;
                            dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                            _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                            await _uowPatientOpenVisit.Save();

                            // Create Patient Work Log
                            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                            objPatientWorkFlowLog.PatientId = input.PatientId;
                            objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                            objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                            objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                            objPatientWorkFlowLog.NextStationProfileId = null;
                            objPatientWorkFlowLog.IsVisitClose = false;
                            objPatientWorkFlowLog.IsActive = true;
                            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                            // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};

                            //_smsService.SendSMS(smsObj);

                        }
                        else
                        {
                            //check for next station
                            var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                                //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                            var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                            if (AppCommonMethod.IsNullObject(thisStation))
                                throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                            var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                            if (AppCommonMethod.IsNullObject(nextStation))
                            {
                                //isVisitClose = true;
                                nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                                // SMS on Visit Close
                                //SendSMSDto smsObj = new SendSMSDto()
                                //{
                                //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                                //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                                //};


                                //_smsService.SendSMS(smsObj);
                            }



                            //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                            //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                            dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                            dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                            dbObjPatientVisit.IsDischarge = isVisitClose;

                            dbObjPatientVisit.BedNo = input.BedNo;
                            dbObjPatientVisit.IsOccupied = false;
                            dbObjPatientVisit.OccupiedBy = null;
                            dbObjPatientVisit.AttendedBy = null;
                            dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                            dbObjPatientVisit.UpdatedOn = DateTime.Now;
                            dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                            _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                            await _uowPatientOpenVisit.Save();

                            // Create Patient Work Log
                            CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                            objPatientWorkFlowLog.PatientId = input.PatientId;
                            objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                            objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                            objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                            objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                            objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                            objPatientWorkFlowLog.IsActive = true;
                            //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                            await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                        }
                    }
                    // Store Json Object
                    var jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input.FormType;
                    dbPatientDiagnoseRecord.Json = jsonObj;
                    dbPatientDiagnoseRecord.IsActive = true;

                    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    await _uowPatientDiagnoseRecord.Save();

                    trans.Commit();

                    //if (input.FormType == CommonStringConstant.TbForm)
                    //{
                    //    if (input.IsExpertTest && !AppCommonMethod.IsNullObject(duplicatedDiagnose))
                    //    {
                    //        duplicatedDiagnose.IsExpertTest = false;
                    //        var response = await CreateOrEditPatientDiagnoseWithPrescription(duplicatedDiagnose!);
                    //    }

                    //}

                    //if (input.PatientScreening.PatientId != null && input.FormType == CommonStringConstant.HCPForm && input.PatientScreeningTestKitsDispense.MedicineDispatches.Count != 0)
                    //{
                    //    await UpdatePatientDiagnoseRecordJsonObj(input.PatientDiagnoseId);
                    //}
                }
                //}
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
        }

        private async Task<CreateOrEditPatientDiagnoseWithPrescriptionDto> UpdatePatientDiagnoseWithPrescriptionForIpd(CreateOrEditPatientDiagnoseWithPrescriptionDto input)
        {
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);
                    if (string.IsNullOrEmpty(input.FormType))
                    {
                        input.FormType = CommonStringConstant.GeneralForm;
                    }

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    //var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();
                    var user = TokenService.GetUserLoggedInfo();


                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    DbModel.PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    dbPatient!.FollowupDate = input.FollowupDate;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();


                    var objPatientDiagnose = new PatientDiagnose();
                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        objPatientDiagnose = await _uowPatientDiagnose.Repository.GetById(input.PatientDiagnoseId!);
                        objPatientDiagnose!.FollowupDate = input.FollowupDate;
                        objPatientDiagnose.AdviseGiven = input.AdviseGiven;
                        objPatientDiagnose.Examination = input.Examination;
                        objPatientDiagnose.PatientMedicalHistory = input.PatientMedicalHistory;
                        objPatientDiagnose.PresentComplaints = input.PresentComplaints;
                        objPatientDiagnose.IsVerifiedByConsultant = input.IsVerifiedByConsultant;
                        objPatientDiagnose.IsMlc = input.IsMlc;
                        objPatientDiagnose.IsSendToCdc = input.IsSendToCdc;
                    }
                    else
                        objPatientDiagnose = _mapper.Map<PatientDiagnose>(input);

                    FillEntityWithDetails(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                    {
                        if (!AppCommonMethod.IsNullOrEmptyGuid(input.DiagnosedBy))
                            objPatientDiagnose.DiagnosedBy = input.DiagnosedBy;
                        else
                            objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();
                    }


                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                    objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientDiagnoseProcedures.Clear();

                    if (input.IsRefer)
                    {
                        objPatientDiagnose.IsRefer = input.IsRefer;
                        objPatientDiagnose.ReferToDepartmentLookupId = input.ReferDepartment;
                        objPatientDiagnose.ReferToSectionLookupId = input.ReferSection;
                    }
                    else
                    {
                        objPatientDiagnose.IsRefer = null;
                        objPatientDiagnose.ReferToDepartmentLookupId = null;
                        objPatientDiagnose.ReferToSectionLookupId = null;
                    }


                    if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    }
                    else
                    {
                        _uowPatientDiagnose.Repository.Update(objPatientDiagnose);
                    }
                    await _uowPatientDiagnose.Save();


                    // Delete diagnose diseases 
                    var _uowPatientDiagnoseDisease = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());

                    List<PatientDiagnoseDisease> dbListPatientDiagnoseDisease = await _uowPatientDiagnoseDisease.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var item in dbListPatientDiagnoseDisease)
                    {

                        item.DeletedBy = _tokenService.GetUserId();
                        item.DeletedOn = DateTime.Now;
                        item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                        _uowPatientDiagnoseDisease.Repository.Update(item);
                        await _uowPatientDiagnoseDisease.Save();

                    }
                    // END Delete diagnose diseases

                    // Delete Patient Prescription
                    var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatientDiagnose.GetDbContext());

                    //List<PatientPrescription> dbListPatientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //foreach (var item in dbListPatientPrescription)
                    //{
                    //    item.DeletedBy = _tokenService.GetUserId();
                    //    item.DeletedOn = DateTime.Now;
                    //    item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    //    _uowPatientPrescription.Repository.Update(item);
                    //    await _uowPatientPrescription.Save();

                    //}
                    // END Delete diagnose diseases


                    // Delete Patient Lab Test Recommended
                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientDiagnose.GetDbContext());

                    List<PatientLabTest> dbListPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var item in dbListPatientLabTest)
                    {
                        item.DeletedBy = _tokenService.GetUserId();
                        item.DeletedOn = DateTime.Now;
                        item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                        _uowPatientLabTest.Repository.Update(item);
                        await _uowPatientLabTest.Save();

                    }
                    // END Delete dPatient Lab Test

                    // Delete diagnose Diagnose procedures
                    var _uowPatientDiagnoseProcedure = new UnitOfWork<PatientDiagnoseProcedure>(_uowPatientDiagnose.GetDbContext());

                    List<PatientDiagnoseProcedure> dbListPatientDiagnoseProcedure = await _uowPatientDiagnoseProcedure.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    foreach (var item in dbListPatientDiagnoseProcedure)
                    {
                        if (input.PatientDiagnoseProcedures.Count() > 0 && input.PatientDiagnoseProcedures.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                        {
                            var tempDiagnoseProcedure = input.PatientDiagnoseProcedures.Where(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId).FirstOrDefault();

                            item.Feedback = tempDiagnoseProcedure.Feedback;
                            item.IsPerformed = tempDiagnoseProcedure.IsPerformed;
                            item.PerformedBy = tempDiagnoseProcedure.PerformedBy;
                            item.RecommendBy = tempDiagnoseProcedure.RecommendBy;
                            item.AssistedBy = tempDiagnoseProcedure.AssistedBy;
                            item.ToothNumber = tempDiagnoseProcedure.ToothNumber;
                            item.ToothPosition = tempDiagnoseProcedure.ToothPosition;
                            item.UpdatedBy = _tokenService.GetUserId();
                            item.UpdatedOn = DateTime.Now;
                            item.ActionTypeId = (int)ActionTypeEnum.Edit;
                            _uowPatientDiagnoseProcedure.Repository.Update(item);
                        }
                        else
                        {
                            item.DeletedBy = _tokenService.GetUserId();
                            item.DeletedOn = DateTime.Now;
                            item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                            _uowPatientDiagnoseProcedure.Repository.Update(item);
                        }

                        await _uowPatientDiagnoseProcedure.Save();

                    }

                    foreach (var item in input.PatientDiagnoseProcedures)
                    {
                        if (dbListPatientDiagnoseProcedure.Count() > 0 && !dbListPatientDiagnoseProcedure.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                        {
                            var dbObj = _mapper.Map<PatientDiagnoseProcedure>(item);
                            dbObj.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                            dbObj.PatientVisitId = objPatientDiagnose.PatientVisitId;
                            dbObj.PatientDiagnoseProcedureId = Guid.NewGuid();
                            dbObj.CreatedBy = _tokenService.GetUserId();
                            dbObj.CreatedOn = DateTime.Now;
                            dbObj.ActionTypeId = (int)ActionTypeEnum.Create;
                            dbObj.IsActive = true;
                            dbObj.AssistedBy = item.AssistedBy;

                            await _uowPatientDiagnoseProcedure.Repository.Insert(dbObj);
                            await _uowPatientDiagnoseProcedure.Save();
                        }

                    }
                    // END Delete diagnose Diagnose procedures



                    // Prescription Check
                    //if (TokenService.GetUserLoggedInfo()!.SectionName == CommonStringConstant.DentalSurgeonOPD)
                    //{
                    //    foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    //    {
                    //        if (!AppCommonMethod.IsNullOrEmptyGuid(itemPatientPrescription.PatientPrescriptionId))
                    //            continue;

                    //        var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);
                    //        FillEntityPrescription(objPatientPrescription);

                    //        objPatientPrescription.PatientId = input.PatientId;
                    //        objPatientPrescription.PatientVisitId = input.PatientVisitId;
                    //        objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //        if (dbObjPatientVisit!.IsFromPmis)
                    //            objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                    //        else
                    //            objPatientPrescription.PrescribedBy = _tokenService.GetUserId();

                    //        await _uowPatientPrescription.Repository.Insert(objPatientPrescription);
                    //        await _uowPatientPrescription.Save();
                    //    }
                    //}
                    //else
                    //{
                    //var dbListPatientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientDiagnoseId == objPatientDiagnose.PatientDiagnoseId && x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    // Visit Edit Case
                    foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    {
                        var dbObj = await _uowPatientPrescription.Repository.GetById(itemPatientPrescription.PatientPrescriptionId);

                        if (dbObj != null)
                        {
                            FillEntityPrescription(dbObj);

                            if (dbObjPatientVisit!.IsFromPmis)
                                dbObj.PrescribedBy = dbObjPatientVisit.AttendedBy;
                            else
                                dbObj.PrescribedBy = _tokenService.GetUserId();

                            _uowPatientPrescription.Repository.Update(dbObj);
                        }
                        else
                        {

                            var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);

                            objPatientPrescription.PatientId = input.PatientId;
                            objPatientPrescription.PatientVisitId = input.PatientVisitId;
                            objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                            if (dbObjPatientVisit!.IsFromPmis)
                                objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                            else
                                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();


                            FillEntityPrescription(objPatientPrescription);
                            await _uowPatientPrescription.Repository.Insert(objPatientPrescription);

                        }

                        await _uowPatientPrescription.Save();
                    }
                    //}


                    // Lab Check

                    foreach (var itemPatientLabTests in input.PatientLabTests)
                    {
                        itemPatientLabTests.PatientLabTestId = null;

                        var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId)
                            .Include(x => x.LabTestDetails)
                            .FirstOrDefaultAsync();

                        var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);

                        var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                        // If Test Sample is not Required
                        if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                        {
                            objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                            FillEntitySampleCollection(objPatientLabTest);
                        }

                        FillEntityLab(objPatientLabTest);

                        objPatientLabTest.PatientId = input.PatientId;
                        objPatientLabTest.PatientVisitId = input.PatientVisitId;

                        if (dbObjPatientVisit!.IsFromPmis)
                            objPatientLabTest.TestAdvisedBy = dbObjPatientVisit.AttendedBy;
                        else
                            objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                        //objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();
                        objPatientLabTest.IsArchived = false;
                        objPatientLabTest.IsActive = true;

                        objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest.LabTestId, _isOnline);
                        //objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(objPatientLabTest);

                        objPatientLabTest.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                        {
                            itemdbLabTestDetail.IsActive = true;
                            FillEntityLabDetail(itemdbLabTestDetail);
                            objPatientLabTest.PatientLabTestDetails.Add(itemdbLabTestDetail);
                        }

                        await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                        await _uowPatientLabTest.Save();

                    }

                    //// Diagnose Diseases Check

                    foreach (var item in input.PatientDiagnoseDiseases)
                    {
                        item.PatientDiagnoseDiseaseId = null;
                        var objPatientDiagnoseDisease = _mapper.Map<PatientDiagnoseDisease>(item);


                        objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                        objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                        objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                        objPatientDiagnoseDisease.PatientDiagnoseDiseaseId = Guid.NewGuid();
                        objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;

                        await _uowPatientDiagnoseDisease.Repository.Insert(objPatientDiagnoseDisease);
                        await _uowPatientDiagnoseDisease.Save();

                    }

                    // Patinet Diagnose Procedures

                    //foreach (var itemPatientDiagnoseProcedures in input.PatientDiagnoseProcedures)
                    //{
                    //    itemPatientDiagnoseProcedures.PatientDiagnoseProcedureId = itemPatientDiagnoseProcedures.PatientDiagnoseProcedureId;
                    //    itemPatientDiagnoseProcedures.PatientDiagnoseId = itemPatientDiagnoseProcedures.PatientDiagnoseId;
                    //    itemPatientDiagnoseProcedures.PerformedBy = itemPatientDiagnoseProcedures.PerformedBy;
                    //    itemPatientDiagnoseProcedures.RecommendBy = itemPatientDiagnoseProcedures.RecommendBy;
                    //    itemPatientDiagnoseProcedures.Feedback = itemPatientDiagnoseProcedures.Feedback;
                    //    itemPatientDiagnoseProcedures.IsPerformed = itemPatientDiagnoseProcedures.IsPerformed;


                    //    var objPatientDiagnoseProcedure = _mapper.Map<PatientDiagnoseProcedure>(itemPatientDiagnoseProcedures);
                    //    FillEntityDiagnoseProcedure(objPatientDiagnoseProcedure);

                    //    _uowPatientDiagnoseProcedure.Repository.Update(objPatientDiagnoseProcedure);
                    //    await _uowPatientDiagnoseProcedure.Save();

                    //}


                    if (input.IsRefer) // if refered
                    {

                        // Create Patient Visit Flow
                        CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                        //objPatientDiagnoseReferLog.CurrentDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        //objPatientDiagnoseReferLog.CurrentSectionId = dbObjPatientVisit.SectionLookupId;

                        //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                        //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                        objPatientVisitFlow.CurrentDepartmentId = input.ReferDepartment;
                        objPatientVisitFlow.CurrentSectionId = input.ReferSection;

                        objPatientVisitFlow.PreviousDepartmentId = dbObjPatientVisit.DepartementLookupId;
                        objPatientVisitFlow.PreviousSectionId = dbObjPatientVisit.SectionLookupId;

                        objPatientVisitFlow.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                        objPatientVisitFlow.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                        objPatientVisitFlow.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                        objPatientVisitFlow.ReferedBy = _tokenService.GetUserId();

                        var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.ReferSection).FirstOrDefaultAsync();
                        if (sectionLookup != null)
                        {
                            objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
                            objPatientVisitFlow.IsConsultant = sectionLookup!.IsConsultant;
                        }


                        await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;

                        if (input.ReferDepartment == dbObjPatientVisit.DepartementLookupId)
                        {
                            dbObjPatientVisit.IsReferred = true;
                            dbObjPatientVisit.ReferredHealthFacilityId = dbObjPatientVisit.HealthFacilityId;
                            dbObjPatientVisit.ReferredDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                            dbObjPatientVisit.ReferredSectionLookupId = dbObjPatientVisit.SectionLookupId;
                            dbObjPatientVisit.ReferredBy = _tokenService.GetUserId();

                            dbObjPatientVisit.DepartementLookupId = input.ReferDepartment;
                            dbObjPatientVisit.SectionLookupId = input.ReferSection;
                        }

                        dbObjPatientVisit.AttendedBy = null;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;

                        // Update Station In Case of Refer
                        var stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                    .OrderBy(x => x.SequenceNo)
                                .Select(x => new ViewStationDto
                                {
                                    StationProfileId = x.ProfileId,
                                    SequenceNo = x.SequenceNo,
                                    ShortName = x.ShortName,
                                    Name = x.Name
                                }).ToListAsync();

                        var currentStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        dbObjPatientVisit.CurrentStationProfileId = currentStation!.StationProfileId;

                        // Update Station In Case of Refer

                        // if refer to IPD
                        //var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                        //var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                        //.Select(x => x.DisplayName).FirstOrDefaultAsync();

                        if (dbObjPatientVisit.ReferredDepartmentLookupId != dbObjPatientVisit.DepartementLookupId)
                        {
                            //if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment == CommonStringConstant.IPD)
                            //{
                            var userInfo = TokenService.GetUserLoggedInfo();

                            dbObjPatientVisit.IsAdmittedInIpd = false;
                            dbObjPatientVisit.IsReferredIpd = true;
                            dbObjPatientVisit.IpdDepartmentLookupId = input.ReferDepartment;
                            dbObjPatientVisit.IpdSectionLookupId = input.ReferSection;
                            dbObjPatientVisit.IpdReferredBy = _tokenService.GetUserId();
                            dbObjPatientVisit.IpdReferredByDepartmentLookupId = userInfo!.DepartmentId;
                            dbObjPatientVisit.IpdReferredBySectionLookupId = userInfo!.SectionId;

                            //}
                        }

                        dbObjPatientVisit.BedNo = input.BedNo;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        //objPatientWorkFlowLog.IsVisitClose = true;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);
                    }
                    else if (input.IsVisitClose)
                    {

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        //dbObjPatientVisit!.IsDischarge = true;

                        dbObjPatientVisit.BedNo = input.BedNo;
                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                        // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                        //SendSMSDto smsObj = new SendSMSDto()
                        //{
                        //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                        //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                        //};

                        //_smsService.SendSMS(smsObj);

                    }
                    else
                    {
                        //check for next station
                        var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                        var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                        if (AppCommonMethod.IsNullObject(thisStation))
                            throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                        var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                        if (AppCommonMethod.IsNullObject(nextStation))
                        {
                            //isVisitClose = true;
                            nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                            // SMS on Visit Close
                            //SendSMSDto smsObj = new SendSMSDto()
                            //{
                            //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                            //};


                            //_smsService.SendSMS(smsObj);
                        }



                        //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                        //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                        dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                        dbObjPatientVisit.IsDischarge = isVisitClose;

                        dbObjPatientVisit.BedNo = input.BedNo;

                        dbObjPatientVisit.IsOccupied = false;
                        dbObjPatientVisit.OccupiedBy = null;
                        dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                        dbObjPatientVisit.UpdatedOn = DateTime.Now;
                        dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                        _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                        await _uowPatientOpenVisit.Save();

                        // Create Patient Work Log
                        CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                        objPatientWorkFlowLog.PatientId = input.PatientId;
                        objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                        objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                        objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                        objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                        objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                        objPatientWorkFlowLog.IsActive = true;
                        //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                        await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                    }

                    // Store Json Object
                    var jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        dbPatientDiagnoseRecord = await _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).FirstOrDefaultAsync();

                    }

                    // CASE IF
                    dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input.FormType;
                    dbPatientDiagnoseRecord.Json = jsonObj;
                    dbPatientDiagnoseRecord.IsActive = true;
                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        _uowPatientDiagnoseRecord.Repository.Update(dbPatientDiagnoseRecord!);
                    }
                    else
                    {
                        await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    }

                    await _uowPatientDiagnoseRecord.Save();


                    // Create Patient Visit Flow

                    //CreateOrEditPatientVisitFlowDto objPatientDiagnoseReferLog = new CreateOrEditPatientVisitFlowDto();

                    //objPatientDiagnoseReferLog.CurrentDepartmentId = input.ReferDepartment == null ? dbObjPatientVisit.DepartementLookupId : input.ReferDepartment;
                    //objPatientDiagnoseReferLog.CurrentSectionId = input.ReferSection == null ? dbObjPatientVisit.SectionLookupId : input.ReferSection;

                    //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                    //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                    //objPatientDiagnoseReferLog.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                    //objPatientDiagnoseReferLog.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                    //objPatientDiagnoseReferLog.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //await _patientVisitFlowService.CreateOrEdit(objPatientDiagnoseReferLog);


                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseWithPrescriptionDto>(input);
        }

        #endregion
    }
}
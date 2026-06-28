using AutoMapper;
using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Service.Interfaces;
using HMIS.NCD.Domain.Repositories._UOW;
using JWTAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.NCD.Domain.Models.DTO;
using AppCommonMethods;
using CommonDTOs.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using CommonMessages;
using AggregatorService = HMIS.Aggregator.API;
using CommonExceptionHandler;

namespace HMIS.NCD.Service
{


    public class WellWomanClinicService<TEntity> : IWellWomanClinic where TEntity : class
    {

        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<BreastCancerPatientDetail> _uowBreastCancerPatientDetail;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly bool _isOnline;
        #endregion


        #region Constructor
        public WellWomanClinicService(TokenService tokenService, IConfiguration config, IMapper mapper, UnitOfWork<BreastCancerPatientDetail> uowBreastCancerPatientDetail, PatientDiagnoseService patientDiagnoseService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowBreastCancerPatientDetail = uowBreastCancerPatientDetail;
            _patientDiagnoseService = patientDiagnoseService;
        }

        #endregion


        #region CUD


        public async Task SaveBreastCancerPatient(BreastCancerRiskIdentificationDTO model)
        {
            using (var trans = _uowBreastCancerPatientDetail.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowCervicalCancerPatientDetails = new UnitOfWork<CervicalCancerPatientDetail>();

                    PatientDiagnoseDto digDto = new PatientDiagnoseDto();
                    digDto.FormType = model!.FormType;
                    digDto.DocDepartmentLookupId = model!.DocDepartmentLookupId;
                    digDto.DocSectionLookupId = model!.DocSectionLookupId;
                    digDto.PatientId = model!.PatientId;
                    digDto.PatientVisitId = model!.PatientVisitId;
                    var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);


                    var BC_PatientInfo = new BreastCancerPatientDetail();
                    BC_PatientInfo.PatientId = model!.PatientId;
                    BC_PatientInfo.PatientVisitId = model!.PatientVisitId;
                    BC_PatientInfo.PatientDiagnoseId = digService.PatientDiagnoseId;
                    BC_PatientInfo.MaritalStatus = model.MaritalStatus;
                    BC_PatientInfo.CurrentAge = model.CurrentAge;
                    BC_PatientInfo.ScoreCurrentAge = (model.CurrentAge == "40-50 years") ? 1 : 0;
                    BC_PatientInfo.MenarcheAge = model.MenarcheAge;
                    BC_PatientInfo.ScoreMenarche = (model.MenarcheAge == "< 12 years") ? 1 : 0;
                    BC_PatientInfo.FirstLiveBirthAge = model.FirstLiveBirthAge;
                    BC_PatientInfo.ScoreFirstLiveBirth = (model.FirstLiveBirthAge == "> 30 years") ? 1 : 0;
                    BC_PatientInfo.BreastFedAge = model.BreastFedAge;
                    BC_PatientInfo.ScoreBreastFed = (model.BreastFedAge == "< 1 year") ? 1 : 0;
                    BC_PatientInfo.NoBreastfed = model.NoBreastfed;
                    BC_PatientInfo.ScoreNoBreastfed = (model.NoBreastfed == "No breastfed") ? 1 : 0;
                    BC_PatientInfo.Nulliparity = model.Nulliparity;
                    BC_PatientInfo.ScoreNulliparity = (model.Nulliparity == "Yes") ? 1 : 0;
                    BC_PatientInfo.KnownFamilyBreastCancer = model.KnownFamilyBreastCancer;
                    BC_PatientInfo.ScoreFamilyBreastCancer = (model.KnownFamilyBreastCancer == "Yes") ? 1 : 0;
                    BC_PatientInfo.AtypicalHyperplasia = model.AtypicalHyperplasia;
                    BC_PatientInfo.ScoreAtypicalHyperplasia = (model.AtypicalHyperplasia == "Yes") ? 1 : 0;
                    BC_PatientInfo.OralHarmoneTherapy = model.OralHarmoneTherapy;
                    BC_PatientInfo.ScoreHarmoneTherapy = (model.OralHarmoneTherapy == "Yes") ? 1 : 0;
                    BC_PatientInfo.OtherCancers = model.OtherCancers;
                    BC_PatientInfo.ScoreOtherCancers = (model.OtherCancers == "Yes") ? 1 : 0;
                    BC_PatientInfo.ScoreTotal = (int)BC_PatientInfo.ScoreCurrentAge + (int)BC_PatientInfo.ScoreMenarche + (int)BC_PatientInfo.ScoreFirstLiveBirth + (int)BC_PatientInfo.ScoreBreastFed + (int)BC_PatientInfo.ScoreNoBreastfed + (int)BC_PatientInfo.ScoreNulliparity + (int)BC_PatientInfo.ScoreFamilyBreastCancer
                        + (int)BC_PatientInfo.ScoreAtypicalHyperplasia + (int)BC_PatientInfo.ScoreHarmoneTherapy + (int)BC_PatientInfo.ScoreOtherCancers;

                    BC_PatientInfo.RiskStatus = (BC_PatientInfo.ScoreTotal <= 2) ? "Low Risk" : (BC_PatientInfo.ScoreTotal <= 4) ? "Moderate" : (BC_PatientInfo.ScoreTotal >= 5) ? "High" : null;
                    BC_PatientInfo.Status = true;
                    BC_PatientInfo.AssessmentDate = DateTime.Now;
                    BC_PatientInfo.AssessmentHealthFacility = TokenService.GetUserHfId().ToString();
                    FillEntityBreastCancerPatientDetail(BC_PatientInfo);

                    await _uowBreastCancerPatientDetail.Repository.Insert(BC_PatientInfo);
                    await _uowBreastCancerPatientDetail.Save();


                    if (!string.IsNullOrEmpty(model.IsPregnant))
                    {
                        //pr.IsPregnant = model.IsPregnant;

                        if (model.IsPregnant == "No")
                        {
                            var CVC_PatientInfo = new CervicalCancerPatientDetail();
                            CVC_PatientInfo.PatientId = model!.PatientId;
                            CVC_PatientInfo.PatientVisitId = model!.PatientVisitId;
                            CVC_PatientInfo.PatientDiagnoseId = digService.PatientDiagnoseId;
                            CVC_PatientInfo.MarriedAge = model.CVC_Assessement.MarriedAge;
                            CVC_PatientInfo.ScoreMarriedAge = (model.CVC_Assessement.MarriedAge == "Before 18 years") ? 2 : 0;
                            CVC_PatientInfo.Noofchildren = model.CVC_Assessement.Noofchildren;
                            CVC_PatientInfo.ScoreNoofchildren = (model.CVC_Assessement.Noofchildren == "More than 3") ? 1 : 0;
                            CVC_PatientInfo.Oralcontraceptive = model.CVC_Assessement.Oralcontraceptive;
                            CVC_PatientInfo.ScoreOralcontraceptive = (model.CVC_Assessement.Oralcontraceptive == "More than 5 year") ? 1 : 0;
                            CVC_PatientInfo.Smoking = model.CVC_Assessement.Smoking;
                            CVC_PatientInfo.ScoreSmoking = (model.CVC_Assessement.Smoking == "Yes") ? 1 : 0;
                            CVC_PatientInfo.Morethanonemarriage = model.CVC_Assessement.Morethanonemarriage;
                            CVC_PatientInfo.ScoreMorethanonemarriage = (model.CVC_Assessement.Morethanonemarriage == "Yes") ? 2 : 0;
                            CVC_PatientInfo.Postcoitalbleeding = model.CVC_Assessement.Postcoitalbleeding;
                            CVC_PatientInfo.ScorePostcoitalbleeding = (model.CVC_Assessement.Postcoitalbleeding == "Yes") ? 2 : 0;
                            CVC_PatientInfo.ScoreTotal = model.CVC_Assessement.ScoreTotal;
                            CVC_PatientInfo.RiskStatus = model.CVC_Assessement.ScoreTotal != null && model.CVC_Assessement.ScoreTotal > 2 ? "High Risk" : model.CVC_Assessement.ScoreTotal <= 2 ? "Low Risk" : null;


                            CVC_PatientInfo.AssessmentHealthFacility = TokenService.GetUserHfId().ToString();
                            CVC_PatientInfo.AssessmentDate = DateTime.Now;
                            CVC_PatientInfo.Status = true;
                            FillEntityCervicalCancerPatientDetail(CVC_PatientInfo);
                            await _uowCervicalCancerPatientDetails.Repository.Insert(CVC_PatientInfo);
                            await _uowCervicalCancerPatientDetails.Save();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex.InnerException;
                }
            }
        }


        public async Task SaveBreastClinicalExamination(BreastCBCDTO model)
        {
            var breastPatient = await _uowBreastCancerPatientDetail.Repository.GetALL(x => x.PatientId == model.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(breastPatient))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //breastPatient = _mapper.Map<BreastCancerPatientDetail>(model);
            breastPatient.Cbcstatus = model.CBCStatus;
            breastPatient.Lump = model.Lump;
            breastPatient.Pain = model.Pain;
            breastPatient.NippleDischarge = model.NippleDischarge;
            breastPatient.SkinChanges = model.SkinChanges;
            breastPatient.AxillaryLump = model.AxillaryLump;
            breastPatient.ReferForUltraSound = model.ReferForUltraSound;
            breastPatient.ReferSurgeryDepartment = model.ReferSurgeryDepartment;
            //breastPatient.ReferHealthFacilityID = model.ReferHealthFacilityID;
            breastPatient.Cbedate = DateTime.Now;
            breastPatient.CbehealthFacility = TokenService.GetUserHfId().ToString();
            FillEntityBreastCancerPatientDetail(breastPatient);
            _uowBreastCancerPatientDetail.Repository.Update(breastPatient);
            await _uowBreastCancerPatientDetail.Save();

        }

        public async Task SaveCVCExamination(CVCExamDTO model)
        {

            using (var trans = _uowBreastCancerPatientDetail.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowCervicalCancerPatientDetail = new UnitOfWork<CervicalCancerPatientDetail>(_uowBreastCancerPatientDetail.GetDbContext());

                    var CVCPatient = await _uowCervicalCancerPatientDetail.Repository.GetALL(x => x.PatientId == model.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
                    if (AppCommonMethod.IsNullObject(CVCPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    CVCPatient.SpeculumExamination = model.SpeculumExamination;
                    //CVCPatient.Referfortertiaryhospital = model.ReferHealthFacilityID;

                    CVCPatient.ReferToTurtiaryHospital = model.ReferToTurtiaryHospital;

                    PatientRefer patientRefer = new PatientRefer();
                    if (model.ReferHealthFacilityID != null)
                    {
                        var _uowPatientRefer = new UnitOfWork<PatientRefer>(_uowBreastCancerPatientDetail.GetDbContext());
                        patientRefer = _mapper.Map<PatientRefer>(CVCPatient);
                        patientRefer.ReferHealthFacilityId = model.ReferHealthFacilityID;
                        patientRefer.ReferTypeId = model.ReferTypeId;
                        FillEntityPatientRefer(patientRefer);
                        await _uowPatientRefer.Repository.Insert(patientRefer);
                        await _uowPatientRefer.Save();
                    }

                    CVCPatient.VisualInspectionbyAceticAcid = model.VisualInspectionbyAceticAcid;
                    CVCPatient.ConsunForFpscreen = model.ConsunForFPScreen;
                    CVCPatient.LocationOfAcctowhite = model.LocationOfAcctowhite;
                    CVCPatient.CryotherapyApplied = model.CryotherapyApplied;
                    CVCPatient.ExamDate = DateTime.Now;
                    CVCPatient.IsPapSmearPerformed = model.IsPapSmearPerformed;
                    CVCPatient.PatientReferId = patientRefer.PatientReferId;
                    if (model.IsPapSmearPerformed == true)
                    {
                        CVCPatient.PapSmearPerformedDate = DateTime.Now;
                        CVCPatient.SampleBarcode = model.SampleBarcode;
                    }
                    else
                    {
                        CVCPatient.PapSmearPerformedDate = null;
                    }
                    if (model.CryotherapyApplied == "Yes")
                    {
                        CVCPatient.CryotherapyAppliedDate = DateTime.Now;
                    }
                    CVCPatient.PapSmearTestResult = model.PapSmearTestResult;
                    CVCPatient.ReferToTurtiaryHospital = model.ReferToTurtiaryHospital;
                    CVCPatient.ExamHealthFacility = TokenService.GetUserHfId().ToString();
                    FillEntityCervicalCancerPatientDetail(CVCPatient);

                    _uowCervicalCancerPatientDetail.Repository.Update(CVCPatient);
                    await _uowCervicalCancerPatientDetail.Save();


                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }

        public async Task SaveGDMPatientwithTrimester(GDMPatientDTO model)
        {
            using (var trans = _uowBreastCancerPatientDetail.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowGDMPatientDetails = new UnitOfWork<GdmpatientDetail>(_uowBreastCancerPatientDetail.GetDbContext());
                    var _uowGDMPatientTrimesterDetails = new UnitOfWork<GdmpatientTrimesterDetail>(_uowBreastCancerPatientDetail.GetDbContext());
                    if (!CheckValidation(model) && model.GDMTrimesterViewModel.LMPDate != null)
                    {
                        throw new UserFriendlyException(CommonMessageConstant.MissingScreeningValueOrPreviousPregency);
                    }

                    if (!AppCommonMethod.IsNullObject(model.GDMDemograhicViewModel) && model!.GDMDemograhicViewModel!.PregencyType.ToUpper().ToString() == CommonStringConstant.NewPregnancy)
                    {
                        var GDMPatientDetails = new GdmpatientDetail();
                        GDMPatientDetails.PregencyType = model.GDMDemograhicViewModel.PregencyType;
                        GDMPatientDetails.IsPregnancy = model.GDMDemograhicViewModel.IsPregnancy;
                        GDMPatientDetails.RiskFactors = model.GDMDemograhicViewModel.RiskFactors;
                        GDMPatientDetails.IsOnRisk = model.GDMDemograhicViewModel.IsOnRisk;
                        GDMPatientDetails.PlanningPregnancy = model.GDMDemograhicViewModel.PlanningPregnancy;

                        GDMPatientDetails.HealthFacilityCode = TokenService.GetUserHfId().ToString();
                        GDMPatientDetails.PatientId = model.PatientId;
                        GDMPatientDetails.PatientVisitId = model.PatientVisitId;
                        GDMPatientDetails.Lmpdate = model.GDMTrimesterViewModel.LMPDate;
                        GDMPatientDetails.Previouspregnancy = model.GDMTrimesterViewModel.previouspregnancy;


                        GDMPatientDetails.Status = true;
                        GDMPatientDetails.IsDeleted = false;
                        FillEntityGdmpatientDetail(GDMPatientDetails);
                        await _uowGDMPatientDetails.Repository.Insert(GDMPatientDetails);
                        await _uowGDMPatientDetails.Save();
                        model.GDMPatientID = GDMPatientDetails.GdmpatientDetailId;

                    }


                    if (!AppCommonMethod.IsNullObject(model.GDMTrimesterViewModel))
                    {
                        try
                        {
                            var today = DateTime.UtcNow.AddHours(5).Date;
                            var todayFollowUp = _uowGDMPatientTrimesterDetails.Repository.GetALL(a => a.GdmpatientDetailId == model.GDMPatientID && a.CreatedOn >= today && a.Trimester == model.GDMTrimesterViewModel.Trimester);

                            if (todayFollowUp != null)
                            {
                                _uowGDMPatientTrimesterDetails.Repository.Delete(todayFollowUp);
                                await _uowGDMPatientTrimesterDetails.Save();
                            }
                        }
                        catch (Exception) { }
                    }

                    var GDMTrimester = new GdmpatientTrimesterDetail();
                    GDMTrimester.Bsr = model.GDMTrimesterViewModel.BSR;
                    GDMTrimester.Bsf = model.GDMTrimesterViewModel.BSF;
                    GDMTrimester.HbA1c = model.GDMTrimesterViewModel.HbA1C;
                    GDMTrimester.OgttoneHour = model.GDMTrimesterViewModel.OGTTOneHour;
                    GDMTrimester.OgtttwoHour = model.GDMTrimesterViewModel.OGTTTwoHour;
                    GDMTrimester.Lmpdate = model.GDMTrimesterViewModel.LMPDate;

                    GDMTrimester.ConfirmedonTwodifferentdates = model.GDMTrimesterViewModel.Trimester == "1st" ? model.GDMTrimesterViewModel.ConfirmedonTwodifferentdates : null;
                    GDMTrimester.GestationalOgttat16Weeks = model.GDMTrimesterViewModel.GestationalOGTTat16Weeks;
                    GDMTrimester.GestationalOgttat24Weeks = model.GDMTrimesterViewModel.GestationalOGTTat24Weeks;
                    GDMTrimester.Gestationalagegreaterthan34weeks = model.GDMTrimesterViewModel.Gestationalagegreaterthan34weeks;
                    GDMTrimester.Onehourpostmeal = model.GDMTrimesterViewModel.Onehourpostmeal;

                    GDMTrimester.PreGestationalDiabetes = model.GDMTrimesterViewModel.PreGestationalDiabetes; // Negative,Imperative,BSR
                    GDMTrimester.Previouspregnancy = model.GDMTrimesterViewModel.previouspregnancy;
                    GDMTrimester.Trimester = model.GDMTrimesterViewModel.Trimester; // // 1 = ( ≤ 12 weeks ) ,2=  ( >12 or <24 weeks ),3 = ( ≥ 24 weeks )
                    GDMTrimester.TrimesterCounter = _uowGDMPatientTrimesterDetails.Repository.GetALL(a => a.GdmpatientDetailId == model.GDMPatientID).Count() + 1;


                    try
                    {
                        int weeksDifference = Convert.ToInt32(DateTime.Now.Date.Subtract((DateTime)model.GDMTrimesterViewModel.LMPDate).TotalDays / 7);
                        GDMTrimester.WeeksDifference = weeksDifference;
                        if (model.GDMTrimesterViewModel.Trimester == "1st" && !String.IsNullOrEmpty(model.GDMTrimesterViewModel.previouspregnancy))
                        {
                            GDMTrimester.ReferForOgtt = model.GDMTrimesterViewModel.previouspregnancy.ToUpper() == "YES" ? "ReferFor16weeks" : "ReferFor24weeks";
                            GDMTrimester.OgttvalueType = ((model.GDMTrimesterViewModel.OGTTOneHour != null || model.GDMTrimesterViewModel.OGTTOneHour > 0) || (model.GDMTrimesterViewModel.OGTTTwoHour != null || model.GDMTrimesterViewModel.OGTTTwoHour > 0)) ? "OGTT16Weeks" : "";

                        }
                        else if (model.GDMTrimesterViewModel.Trimester == "2nd" && !String.IsNullOrEmpty(model.GDMTrimesterViewModel.previouspregnancy))
                        {
                            GDMTrimester.ReferForOgtt = model.GDMTrimesterViewModel.previouspregnancy.ToUpper() == "NO" ? "ReferFor24weeks" : (model.GDMTrimesterViewModel.previouspregnancy.ToUpper() == "YES" && model.GDMTrimesterViewModel.OGTTOneHour != null ? "ReferFor24weeks" : "ReferFor16weeks");
                            GDMTrimester.OgttvalueType = ((model.GDMTrimesterViewModel.OGTTOneHour != null || model.GDMTrimesterViewModel.OGTTOneHour > 0) || (model.GDMTrimesterViewModel.OGTTTwoHour != null || model.GDMTrimesterViewModel.OGTTTwoHour > 0)) ? "OGTT16Weeks" : "";
                        }
                        /* 
                         * NotAllowForRefer : If patient is negative and 24weeksOGTT is not done and his LMP and Trimester Date Difference > 34
                         * ReferFor24weeks : If patient is negative and 24weeksOGTT is not done and his LMP and Trimester Date Difference < 34
                         */
                        else if (model.GDMTrimesterViewModel.Trimester == "3rd" && (model.GDMTrimesterViewModel.BSF < 92 || model.GDMTrimesterViewModel.OGTTOneHour < 180 || model.GDMTrimesterViewModel.OGTTTwoHour < 153 || model.GDMTrimesterViewModel.BSR < 180 || model.GDMTrimesterViewModel.HbA1C < 6.5))
                        {
                            GDMTrimester.ReferForOgtt = ((model.GDMTrimesterViewModel.OGTTOneHour == null || model.GDMTrimesterViewModel.OGTTOneHour <= 0) || (model.GDMTrimesterViewModel.OGTTTwoHour == null || model.GDMTrimesterViewModel.OGTTTwoHour <= 0)) ? (weeksDifference < 34 ? "ReferFor24weeks" : "NotAllowForRefer") : "";
                            GDMTrimester.OgttvalueType = ((model.GDMTrimesterViewModel.OGTTOneHour != null || model.GDMTrimesterViewModel.OGTTOneHour > 0) || (model.GDMTrimesterViewModel.OGTTTwoHour != null || model.GDMTrimesterViewModel.OGTTTwoHour > 0)) ? "OGTT24Weeks" : "";
                        }
                    }
                    catch { }



                    GDMTrimester.HealthFacilityCode = TokenService.GetUserHfId().ToString();
                    GDMTrimester.GdmpatientDetailId = model.GDMPatientID;
                    GDMTrimester.PregencyType = model.GDMDemograhicViewModel.PregencyType;
                    GDMTrimester.Status = true;
                    FillEntityGDMPatientTrimesterDetails(GDMTrimester);
                    await _uowGDMPatientTrimesterDetails.Repository.Insert(GDMTrimester);
                    await _uowGDMPatientTrimesterDetails.Save();
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }

        public async Task SaveUltraSoundAndDiagnosisResults(UltraSoundAndDiagnosisDTO model)
        {
            using (var trans = _uowBreastCancerPatientDetail.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var BreastPatient = await _uowBreastCancerPatientDetail.Repository.GetALL(x => x.PatientId == model.PatientId).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(BreastPatient))
                    {


                        var dbUser = TokenService.GetUserLoggedInfo();
                        var _uowLabTest = new UnitOfWork<LabTest>(_uowBreastCancerPatientDetail.GetDbContext());
                        var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowBreastCancerPatientDetail.GetDbContext());
                        var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowBreastCancerPatientDetail.GetDbContext());
                        var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowBreastCancerPatientDetail.GetDbContext());
                        var _uowHfLabTestConfig = new UnitOfWork<HfLabTestConfig>(_uowBreastCancerPatientDetail.GetDbContext());

                        foreach (var itemPatientLabTests in model.PatientUltrasound)
                        {

                            var dbLabTest = await _uowLabTest.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                                .Include(x => x.LabTestDetails.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted))
                                 .FirstOrDefaultAsync();

                            var configIsPerformedPrivately = await _uowHfLabTestConfig.Repository.GetALL(x => x.LabTestId == itemPatientLabTests.LabTestId
                                                    && x.HealthFacilityId == dbUser!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.IsPerformedPrivately).FirstOrDefaultAsync();


                            var inputPatientLabTestDetail = _mapper.Map<List<PatientLabTestDetail>>(dbLabTest!.LabTestDetails);
                            var objPatientLabTest = _mapper.Map<PatientLabTest>(itemPatientLabTests);

                            // If Test Sample is not Required
                            if (!AppCommonMethod.IsNullObject(dbLabTest.IsSampleRequired) && dbLabTest.IsSampleRequired != true)
                            {
                                objPatientLabTest.IsSampleRequired = dbLabTest.IsSampleRequired;
                                FillEntitySampleCollection(objPatientLabTest);
                            }
                            objPatientLabTest.IsPerformedPrivately = (configIsPerformedPrivately == true) ? true : false;


                            // If gynae Patient then all lab test will be free


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

                            FillEntityLab(objPatientLabTest);
                            objPatientLabTest.PatientId = BreastPatient.PatientId;
                            objPatientLabTest.PatientVisitId = BreastPatient.PatientVisitId;
                            objPatientLabTest.PatientDiagnoseId = BreastPatient.PatientDiagnoseId;

                            objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                            objPatientLabTest.IsArchived = false;
                            objPatientLabTest.IsActive = true;

                            // Generate New BarcodeNo
                            //objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest.LabTestId, _isOnline);

                            foreach (var itemdbLabTestDetail in inputPatientLabTestDetail)
                            {
                                itemdbLabTestDetail.IsActive = true;
                                itemdbLabTestDetail.PatientLabTestId = objPatientLabTest.PatientLabTestId;
                                FillEntityLabDetail(itemdbLabTestDetail);
                                await _uowPatientLabTestDetail.Repository.Insert(itemdbLabTestDetail);
                                await _uowPatientLabTestDetail.Save();
                            }

                            await _uowPatientLabTest.Repository.Insert(objPatientLabTest);
                            await _uowPatientLabTest.Save();
                        }





                        await UpdateUltraSoundAndDiagnosisResults(_uowBreastCancerPatientDetail, BreastPatient, model);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }





        private async Task UpdateUltraSoundAndDiagnosisResults(UnitOfWork<BreastCancerPatientDetail> _uowBreastCancerPatientDetail, BreastCancerPatientDetail SaveBreastPatient, UltraSoundAndDiagnosisDTO model)
        {
            SaveBreastPatient.UltraSoundComments = model.UltraSoundComments;
            SaveBreastPatient.ProvisionalDiagnosis = model.ProvisionalDiagnosis;
            SaveBreastPatient.ReferSurgeryDepartment = model.ReferSurgeryDepartment;
            SaveBreastPatient.IsReferToTeritaryCareHospital = model.IsReferToTeritaryCareHospital;
            SaveBreastPatient.UpdatedOn = DateTime.Now;
            SaveBreastPatient.UpdatedBy = _tokenService.GetUserId();
            //if (SaveBreastPatient.PatientUltraSoundResults.Count > 0)
            //{
            //    db.PatientUltraSoundResults.RemoveRange(SaveBreastPatient.PatientUltraSoundResults);
            //}
            //if (SaveBreastPatient.BreastUltraSoundResults.Count > 0)
            //{
            //    db.BreastPatientUltraSoundResults.RemoveRange(SaveBreastPatient.BreastUltraSoundResults);
            //}
            if (!AppCommonMethod.IsNullObject(model.BreastUltraSounds))
            {

                var _uowBreastPatientUltraSoundResult = new UnitOfWork<BreastPatientUltraSoundResult>(_uowBreastCancerPatientDetail.GetDbContext());
                var BreastPatientUltraSoundResults = new BreastPatientUltraSoundResult
                {
                    PatientId = model.PatientId,
                    PatientVisitId = model.PatientVisitId,
                    LesionSize = model!.BreastUltraSounds!.LesionSize,
                    NumberofLesions = model.BreastUltraSounds.NumberofLesions,
                    LeftLesion = model.BreastUltraSounds.LeftLesion,
                    RightLesion = model.BreastUltraSounds.RightLesion,
                    Texture = model.BreastUltraSounds.Texture,
                    Margins = model.BreastUltraSounds.Margins,
                    Orientation = model.BreastUltraSounds.Orientation,
                    Shape = model.BreastUltraSounds.Shape,
                    ThinEcogenicCapsule = model.BreastUltraSounds.ThinEcogenicCapsule,
                    PosteriorAcoustic = model.BreastUltraSounds.PosteriorAcoustic,
                    GentleLobulation = model.BreastUltraSounds.GentleLobulation,
                    MicroCalcification = model.BreastUltraSounds.MicroCalcification,
                    ArchitecturalDistortion = model.BreastUltraSounds.ArchitecturalDistortion,
                    DilatedDucts = model.BreastUltraSounds.DilatedDucts,
                    Skinthickening = model.BreastUltraSounds.Skinthickening,
                    LymphNodesEnlarged = model.BreastUltraSounds.LymphNodesEnlarged,
                    LymphLocation = model.BreastUltraSounds.LymphLocation,
                    LymphNodesSize = model.BreastUltraSounds.LymphNodesSize,
                    FattyHilum = model.BreastUltraSounds.FattyHilum,
                    CorticalThickness = model.BreastUltraSounds.CorticalThickness,
                    RadiologistImpression = model.BreastUltraSounds.RadiologistImpression,
                    Fnac = model.BreastUltraSounds.FNAC,
                    BreastCancerPatientDetailId = SaveBreastPatient.BreastCancerPatientDetailId,
                    Status = true
                };

                FillEntityBreastPatientUltraSoundResult(BreastPatientUltraSoundResults);

                await _uowBreastPatientUltraSoundResult.Repository.Insert(BreastPatientUltraSoundResults);
                await _uowBreastPatientUltraSoundResult.Save();
            }
            SaveBreastPatient.UltraSoundDate = DateTime.Now;
            SaveBreastPatient.UltraSoundHealthFacility = TokenService.GetUserHfId().ToString();
            SaveBreastPatient.UltraSoundFinding = model.UltraSoundFinding;
            FillEntityBreastCancerPatientDetail(SaveBreastPatient);
            _uowBreastCancerPatientDetail.Repository.Update(SaveBreastPatient);
            await _uowBreastCancerPatientDetail.Save();
        }

        #endregion


        #region GET

        public async Task<BreastCancerPatientDetail> GetRiskAssessmentForBreastCancerStatus(Guid PatientId)
        {
            var dbObj = await _uowBreastCancerPatientDetail.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(dbObj))
            {
                return dbObj;
            }
            return null;
        }

        public async Task<GDMPatientDTO> CheckGDMData(Guid PatientId)
        {
            var _uowGDMPatient = new UnitOfWork<GdmpatientDetail>(_uowBreastCancerPatientDetail.GetDbContext());
            var _uowGDMTrimester = new UnitOfWork<GdmpatientTrimesterDetail>(_uowBreastCancerPatientDetail.GetDbContext());
            var GDMPatient = await _uowGDMPatient.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(a => a.CreatedOn).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(GDMPatient))
                return null;

            var GDMTrimester = await _uowGDMTrimester.Repository.GetALL(x => x.GdmpatientDetailId == GDMPatient.GdmpatientDetailId).OrderBy(a => a.CreatedOn).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(GDMPatient))
                return null;

            GDMPatientDTO gdmPatient = new GDMPatientDTO();
            gdmPatient.PatientId = GDMPatient.PatientId;
            gdmPatient.GDMPatientID = GDMPatient.GdmpatientDetailId;
            gdmPatient.GDMDemograhicViewModel.IsPregnancy = GDMPatient.IsPregnancy;
            gdmPatient.GDMDemograhicViewModel.IsOnRisk = GDMPatient.IsOnRisk;
            gdmPatient.GDMDemograhicViewModel.PlanningPregnancy = GDMPatient.PlanningPregnancy;
            gdmPatient.GDMDemograhicViewModel.RiskFactors = GDMPatient.RiskFactors;
            gdmPatient.GDMDemograhicViewModel.TotalFollowups = GDMTrimester.TrimesterCounter == null ? 0 : GDMTrimester.TrimesterCounter;
            gdmPatient.GDMTrimesterViewModel.BSF = GDMTrimester.Bsf;
            gdmPatient.GDMTrimesterViewModel.BSR = GDMTrimester.Bsr;
            gdmPatient.GDMTrimesterViewModel.HbA1C = GDMTrimester.HbA1c;
            gdmPatient.GDMTrimesterViewModel.LMPDate = GDMTrimester.Lmpdate;
            gdmPatient.GDMTrimesterViewModel.OGTTOneHour = GDMTrimester.OgttoneHour;
            gdmPatient.GDMTrimesterViewModel.OGTTTwoHour = GDMTrimester.OgtttwoHour;
            gdmPatient.GDMTrimesterViewModel.PreGestationalDiabetes = GDMTrimester.PreGestationalDiabetes;
            gdmPatient.GDMTrimesterViewModel.previouspregnancy = GDMTrimester.Previouspregnancy;
            gdmPatient.GDMTrimesterViewModel.Trimester = GDMTrimester.Trimester;
            gdmPatient.GDMTrimesterViewModel.LMPDate = GDMTrimester.Lmpdate;

            return gdmPatient;
        }


        public async Task<CervicalCancerPatientDetail> GetCervicalCancerPatientDetailByVisitId(Guid PatientVisitId)
        {
            var _uowCervicalCancerPatientDetail = new UnitOfWork<CervicalCancerPatientDetail>(_uowBreastCancerPatientDetail.GetDbContext());
            var patientDetailForCervix = await _uowCervicalCancerPatientDetail.Repository.GetALL(x => x.PatientVisitId == PatientVisitId).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(patientDetailForCervix))
            {
                return patientDetailForCervix;
            }

            return null;
        }
        #endregion


        #region Helper Method
        public void FillEntityBreastCancerPatientDetail(BreastCancerPatientDetail obj)
        {
            if (obj.BreastCancerPatientDetailId == Guid.Empty)
            {
                obj.BreastCancerPatientDetailId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                //obj.IsActive = true;
                //obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                //obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        public void FillEntityCervicalCancerPatientDetail(CervicalCancerPatientDetail obj)
        {
            if (obj.CervicalCancerPatientDetailId == Guid.Empty)
            {
                obj.CervicalCancerPatientDetailId = Guid.NewGuid();
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

        public void FillEntityPatientRefer(PatientRefer obj)
        {
            if (obj.PatientReferId == Guid.Empty)
            {
                obj.PatientReferId = Guid.NewGuid();
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
        public void FillEntityBreastPatientUltraSoundResult(BreastPatientUltraSoundResult obj)
        {
            if (obj.BreastPatientUltrasoundResultId == Guid.Empty)
            {
                obj.BreastPatientUltrasoundResultId = Guid.NewGuid();
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


        private void FillEntitySampleCollection(PatientLabTest obj)
        {
            obj.IsSampleCollected = true;
            obj.SampleCollectedBy = _tokenService.GetUserId();
            obj.SampleCollectedOn = DateTime.Now;
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


        private void FillEntityGdmpatientDetail(GdmpatientDetail obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.GdmpatientDetailId))
            {
                obj.GdmpatientDetailId = Guid.NewGuid();
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
        private void FillEntityGDMPatientTrimesterDetails(GdmpatientTrimesterDetail obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.GdmpatientTrimesterDetailId))
            {
                obj.GdmpatientTrimesterDetailId = Guid.NewGuid();
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


        public bool CheckValidation(GDMPatientDTO model)
        {
            bool Response = false;
            try
            {
                if (model.GDMTrimesterViewModel.BSR > 0 || model.GDMTrimesterViewModel.BSF > 0 || model.GDMTrimesterViewModel.OGTTOneHour > 0 || model.GDMTrimesterViewModel.OGTTTwoHour > 0 || model.GDMTrimesterViewModel.Onehourpostmeal > 0 || model.GDMTrimesterViewModel.HbA1C > 0)
                {
                    Response = true;
                }
                else
                {
                    Response = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Response;
        }

        #endregion


    }
}

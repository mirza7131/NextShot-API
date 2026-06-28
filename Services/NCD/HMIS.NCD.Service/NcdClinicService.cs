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
using Newtonsoft.Json;

namespace HMIS.NCD.Service
{
    public class NcdClinicService<TEntity> : INcdClinic where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly PatientDiagnoseService _patientDiagnoseService;
        private readonly AggregatorService.PatientDiagnoseJsonService __patientDiagnoseService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientFollowUp> _uowPatientFollowUp;
        private readonly bool _isOnline;
        #endregion

        #region Constructor
        public NcdClinicService(TokenService tokenService, IConfiguration config, IMapper mapper, UnitOfWork<PatientFollowUp> uowPatientFollowUp, PatientDiagnoseService patientDiagnoseService, AggregatorService.PatientDiagnoseJsonService patientDiagnoseService2)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientFollowUp = uowPatientFollowUp;
            _patientDiagnoseService = patientDiagnoseService;
            __patientDiagnoseService = patientDiagnoseService2;

            _isOnline = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ? true : false;
        }

        #endregion

        #region CU
        public async Task<PatientFollowUp> CreateOrEdit(CreateOrEditPatientFollowUpNcdClinicDto Input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(Input.PatientFollowUpsId))
            {
                return await Create(Input);
            }
            else
            {
                return await Edit(Input);
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

        public async Task SaveFollowup(PatientPrescriptionDTO input)
        {
            using (var trans = _uowPatientFollowUp.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowPatientFollowUp.GetDbContext());
                    var MentalHealthPatient = await _uowMentalHealthPatientDetail.Repository.GetALL(x => x.PatientId == input.patientDiagnose.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

                    if (!AppCommonMethod.IsNullObject(MentalHealthPatient))
                    {


                        if ((MentalHealthPatient.TotalNoofFollowups == null || MentalHealthPatient.TotalNoofFollowups == 0) && input.patientDiagnose.DiagnosticDisease == "No Depression/Anxiety")
                        {
                            MentalHealthPatient.DiagnosticDisease = input.patientDiagnose.DiagnosticDisease;
                            MentalHealthPatient.TreatmentOutCome = !string.IsNullOrEmpty(MentalHealthPatient.TreatmentOutCome) && MentalHealthPatient.TreatmentOutCome.Contains(MentalHealthPatient.TreatmentOutCome) ? MentalHealthPatient.TreatmentOutCome : string.IsNullOrEmpty(input.patientDiagnose.TreatmentOutCome) ? input.patientDiagnose.TreatmentOutCome : !string.IsNullOrEmpty(MentalHealthPatient.TreatmentOutCome) ? MentalHealthPatient.TreatmentOutCome + "&" + input.patientDiagnose.TreatmentOutCome : input.patientDiagnose.TreatmentOutCome;
                            if (!string.IsNullOrEmpty(input.patientDiagnose.DiagnosticDisease) && input.patientDiagnose.DiagnosticDisease != "No Depression/Anxiety")
                            {
                                MentalHealthPatient.TotalNoofFollowups = (MentalHealthPatient.TotalNoofFollowups ?? 0) + 1;
                                //MentalHealthPatient.LastFollowID = Model.MentalHealthFollowID;
                                if (!string.IsNullOrEmpty(input.patientDiagnose.ReferredTo))
                                {
                                    MentalHealthPatient.ReferredTo = input.patientDiagnose.ReferredTo;
                                    MentalHealthPatient.ReferredDate = DateTime.Now;
                                    MentalHealthPatient.ReferredBy = _tokenService.GetUserId();
                                }
                            }
                        }
                        else
                        {
                            var today = DateTime.UtcNow.AddHours(5).Date;
                            var _uowMentalHealthPatientFollowups = new UnitOfWork<MentalHealthPatientFollowup>(_uowPatientFollowUp.GetDbContext());
                            var todayFollowUp = await _uowMentalHealthPatientFollowups.Repository.GetALL(x => x.MentalHealthPatientId == MentalHealthPatient.MentalHealthPatientDetailId && x.CreatedOn >= today).FirstOrDefaultAsync();
                            if (todayFollowUp != null)
                            {
                                _uowMentalHealthPatientFollowups.Repository.Delete(todayFollowUp);
                                await _uowMentalHealthPatientFollowups.Save();
                                MentalHealthPatient.TotalNoofFollowups = MentalHealthPatient.TotalNoofFollowups - 1;
                            }


                            var _uowMentalHealthPatientFollowup = new UnitOfWork<MentalHealthPatientFollowup>(_uowPatientFollowUp.GetDbContext());
                            var Followup = new MentalHealthPatientFollowup();
                            Followup.MentalHealthPatientId = MentalHealthPatient.MentalHealthPatientDetailId;
                            Followup.ReferredTo = input!.patientDiagnose!.ReferredTo;
                            Followup.TreatmentOutCome = string.IsNullOrEmpty(input!.patientDiagnose!.TreatmentOutCome) ? "Continue Treatment" : input!.patientDiagnose!.TreatmentOutCome;
                            if (!AppCommonMethod.IsNullOrEmptyList(input.patientPrescription))
                                Followup.NextFollowupDate = input.patientDiagnose.NextFollowUpDate ?? DateTime.Now.AddDays(15);
                            Followup.Status = true;
                            FillEntityMentalHealthPatientFollowup(Followup);
                            await _uowMentalHealthPatientFollowup.Repository.Insert(Followup);
                            await _uowMentalHealthPatientFollowup.Save();


                            //await SavePatientPrescription(input);

                            MentalHealthPatient.DiagnosticDisease = input.patientDiagnose.DiagnosticDisease;
                            MentalHealthPatient.TreatmentOutCome = input.patientDiagnose.TreatmentOutCome;
                            if (!string.IsNullOrEmpty(input.patientDiagnose.DiagnosticDisease) && input.patientDiagnose.DiagnosticDisease != "No Depression/Anxiety")
                            {
                                MentalHealthPatient.TotalNoofFollowups = (MentalHealthPatient.TotalNoofFollowups ?? 0) + 1;
                                MentalHealthPatient.LastFollowId = Followup.MentalHealthPatientFollowupsId;
                                if (!string.IsNullOrEmpty(input.patientDiagnose.ReferredTo))
                                {
                                    MentalHealthPatient.ReferredTo = input.patientDiagnose.ReferredTo;
                                    MentalHealthPatient.ReferredDate = DateTime.Now;
                                    MentalHealthPatient.ReferredBy = _tokenService.GetUserId();
                                }
                            }


                        }



                        MentalHealthPatient.IsPatientCounciled = input.patientDiagnose.IsPatientCounciled;
                        MentalHealthPatient.UpdatedBy = _tokenService.GetUserId();
                        MentalHealthPatient.UpdatedOn = DateTime.Now;
                        _uowMentalHealthPatientDetail.Repository.Update(MentalHealthPatient);
                        await _uowMentalHealthPatientDetail.Save();


                        if (AppCommonMethod.IsNullOrEmptyList(input.patientPrescription))
                        {
                            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientFollowUp.GetDbContext());
                            PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.patientDiagnose.PatientVisitId!);

                            dbObjPatientVisit.IsDischarge = true;

                            FillEntityPatientOpenVisit(dbObjPatientVisit);
                            _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                            await _uowPatientOpenVisit.Save();
                        }
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
        }
        public async Task SavePatientPrescription(PatientPrescriptionDTO input)
        {

            using (var trans = _uowPatientFollowUp.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    var _uowProfile = new UnitOfWork<HMIS.NCD.Domain.Models.DbModels.Profile>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientFollowUp.GetDbContext());

                    var _uowPresMap = new UnitOfWork<PatientPrescription>(_uowPatientFollowUp.GetDbContext());
                    PatientDiagnoseDto digDto = new PatientDiagnoseDto();
                    digDto.FormType = input!.patientDiagnose!.FormType;
                    digDto.DocDepartmentLookupId = input!.patientDiagnose!.DocDepartmentLookupId;
                    digDto.DocSectionLookupId = input!.patientDiagnose!.DocSectionLookupId;
                    digDto.PatientId = input!.patientDiagnose!.PatientId;
                    digDto.PatientVisitId = input!.patientDiagnose!.PatientVisitId;
                    digDto.NextFollowUpDate = input!.patientDiagnose!.NextFollowUpDate;
                    var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);
                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.patientDiagnose.PatientVisitId!);


                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);


                    if (input.patientDiagnose.IsFollowUp == false)
                    {
                        var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowPatientFollowUp.GetDbContext());

                        var MentalHealthPatient = await _uowMentalHealthPatientDetail.Repository.GetALL(x => x.PatientVisitId == input.patientDiagnose.PatientVisitId).FirstOrDefaultAsync();

                        MentalHealthPatient.DiagnosticDisease = input.patientDiagnose.DiagnosticDisease;
                        MentalHealthPatient.TreatmentOutCome = input.patientDiagnose.TreatmentOutCome;
                        if (!string.IsNullOrEmpty(input.patientDiagnose.ReferredTo))
                        {
                            MentalHealthPatient.ReferredTo = input.patientDiagnose.ReferredTo;
                            MentalHealthPatient.ReferredDate = DateTime.Now;
                            MentalHealthPatient.ReferredBy = _tokenService.GetUserId();
                        }

                        MentalHealthPatient.IsPatientCounciled = input.patientDiagnose.IsPatientCounciled;
                        MentalHealthPatient.UpdatedBy = _tokenService.GetUserId();
                        MentalHealthPatient.UpdatedOn = DateTime.Now;
                        _uowMentalHealthPatientDetail.Repository.Update(MentalHealthPatient);
                        await _uowMentalHealthPatientDetail.Save();
                    }

                    bool areAllDiagnosisDone = false;
                    var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatientDiagnose.GetDbContext());
                    var patientSection = await _uowSectionLookup.Repository.GetById(dbObjPatientVisit!.SectionLookupId);
                    if (!AppCommonMethod.IsNullObject(patientSection))
                    {
                        if (patientSection.FormType == CommonStringConstant.NcdAndMuawinClinicForm)
                        {
                            var patientDiagnosis = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input.patientDiagnose.PatientVisitId).ToListAsync();
                            if (!AppCommonMethod.IsNullOrEmptyList(patientDiagnosis))
                            {
                                areAllDiagnosisDone = patientDiagnosis.Any(x => x.FormType == CommonStringConstant.NCDClinicForm) && patientDiagnosis.Any(x => x.FormType == CommonStringConstant.MuawinClinicsForm);
                            }
                        }
                        else
                        {
                            areAllDiagnosisDone = true;
                        }
                    }
                    if (!AppCommonMethod.IsNullOrEmptyList(input.patientPrescription!))
                    {
                        var pharmacyStationProfileId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.PharmacyStation)
                                                                          .Select(x => x.ProfileId)
                                                                          .FirstOrDefaultAsync();
                        if (areAllDiagnosisDone)
                        {
                            dbObjPatientVisit.CurrentStationProfileId = pharmacyStationProfileId;
                        }


                        foreach (var item in input.patientPrescription!)
                        {
                            item!.PatientPrescriptionId = Guid.NewGuid();
                            var presMap = _mapper.Map<PatientPrescription>(item);
                            presMap!.PatientId = input!.patientDiagnose!.PatientId;
                            presMap!.PatientDiagnoseId = digService.PatientDiagnoseId;
                            presMap!.PatientVisitId = input!.patientDiagnose!.PatientVisitId;
                            presMap.CreatedOn = DateTime.Now;
                            presMap.CreatedBy = _tokenService.GetUserId();
                            presMap.PrescribedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Create;
                            await _uowPresMap.Repository.Insert(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                    }
                    else
                    {
                        dbObjPatientVisit.IsDischarge = areAllDiagnosisDone == true ? true : false;
                    }


                    //var response = await __patientDiagnoseService.GetJsonObj(digDto.PatientVisitId, input.patientDiagnose.FormType!, digService.PatientDiagnoseId);

                    var response = await GetJsonObj(digDto.PatientVisitId, digService.PatientDiagnoseId);
                    //if (response.statusCode != System.Net.HttpStatusCode.OK)
                    //    throw new UserFriendlyException(CommonMessageConstant.WaitOperationTimeOut);

                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    dbPatientDiagnoseRecord.PatientId = digDto.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = digDto.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = digService.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input!.patientDiagnose!.FormType;
                    //dbPatientDiagnoseRecord.Json = response!.data!.ToString();
                    dbPatientDiagnoseRecord.Json = response;
                    dbPatientDiagnoseRecord.IsActive = true;

                    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    await _uowPatientDiagnoseRecord.Save();

                    dbObjPatientVisit.AttendedBy = null;
                    FillEntityPatientOpenVisit(dbObjPatientVisit);
                    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    await _uowPatientOpenVisit.Save();

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

        }

        public async Task<PatientFollowUp> Create(CreateOrEditPatientFollowUpNcdClinicDto input)
        {
            using (var trans = _uowPatientFollowUp.GetDbContext().Database.BeginTransaction())
            {


                try
                {
                    // Generate PatientDiagnose Id
                    var _uowProfile = new UnitOfWork<HMIS.NCD.Domain.Models.DbModels.Profile>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientFollowUp.GetDbContext());
                    PatientDiagnoseDto digDto = new PatientDiagnoseDto();
                    digDto.FormType = input.FormType;
                    digDto.DocDepartmentLookupId = input.DocDepartmentLookupId;
                    digDto.DocSectionLookupId = input.DocSectionLookupId;
                    digDto.PatientId = input.PatientId;
                    digDto.PatientVisitId = input.PatientVisitId;
                    digDto.NextFollowUpDate = input.NextFollowUpDate;
                    var digService = await _patientDiagnoseService.InsertPatientDiagnose(digDto);



                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);



                    // Add Patient PresCription
                    var areAllDiagnosisDone = false;
                    var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatientDiagnose.GetDbContext());
                    var patientSection = await _uowSectionLookup.Repository.GetById(dbObjPatientVisit!.SectionLookupId);
                    if (!AppCommonMethod.IsNullObject(patientSection))
                    {
                        if (patientSection.FormType == CommonStringConstant.NcdAndMuawinClinicForm)
                        {
                            var patientDiagnosis = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).ToListAsync();
                            if (!AppCommonMethod.IsNullOrEmptyList(patientDiagnosis))
                            {
                                areAllDiagnosisDone = patientDiagnosis.Any(x => x.FormType == CommonStringConstant.NCDClinicForm) && patientDiagnosis.Any(x => x.FormType == CommonStringConstant.MuawinClinicsForm);
                            }
                        }
                        else
                        {
                            areAllDiagnosisDone = true;
                        }
                    }
                    if (!AppCommonMethod.IsNullOrEmptyList(input.PatientPrescription!))
                    {


                        var pharmacyStationProfileId = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.PharmacyStation)
                                                                          .Select(x => x.ProfileId)
                                                                          .FirstOrDefaultAsync();

                        if (areAllDiagnosisDone)
                        {
                            dbObjPatientVisit.CurrentStationProfileId = pharmacyStationProfileId;
                        }

                        foreach (var item in input.PatientPrescription!)
                        {
                            item!.PatientPrescriptionId = Guid.NewGuid();
                            var presMap = _mapper.Map<PatientPrescription>(item);
                            presMap!.PatientId = input.PatientId;
                            presMap!.PatientDiagnoseId = digService.PatientDiagnoseId;
                            presMap!.PatientVisitId = input.PatientVisitId;
                            presMap.CreatedOn = DateTime.Now;
                            presMap.CreatedBy = _tokenService.GetUserId();
                            presMap.PrescribedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Create;
                            var _uowPresMap = new UnitOfWork<PatientPrescription>(_uowPatientFollowUp.GetDbContext());
                            await _uowPresMap.Repository.Insert(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                    }
                    else
                    {
                        dbObjPatientVisit.IsDischarge = areAllDiagnosisDone == true ? true : false;
                        //dbObjPatientVisit.IsDischarge = true;
                    }


                    var dbUser = TokenService.GetUserLoggedInfo();

                    var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientFollowUp.GetDbContext());
                    var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientFollowUp.GetDbContext());
                    var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientFollowUp.GetDbContext());
                    var _uowHfLabTestConfig = new UnitOfWork<HfLabTestConfig>(_uowPatientFollowUp.GetDbContext());

                    foreach (var itemPatientLabTests in input.PatientLabTests)
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
                        objPatientLabTest.PatientId = input.PatientId;
                        objPatientLabTest.PatientVisitId = input.PatientVisitId;
                        objPatientLabTest.PatientDiagnoseId = digService.PatientDiagnoseId;

                        objPatientLabTest.TestAdvisedBy = _tokenService.GetUserId();

                        objPatientLabTest.IsArchived = false;
                        objPatientLabTest.IsActive = true;

                        // Generate New BarcodeNo
                        objPatientLabTest.BarcodeNo = await GenerateBarcodeNo(dbObjPatientVisit.HealthFacilityId, objPatientLabTest.LabTestId, _isOnline);

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


                    // Add PatientDiagnoseDisease
                    if (!AppCommonMethod.IsNullObject(input.PatientDiagnoseDiaeasae))
                    {
                        foreach (var item in input.PatientDiagnoseDiaeasae)
                        {
                            item!.PatientDiagnoseDiseaseId = Guid.NewGuid();
                            var presMap = _mapper.Map<PatientDiagnoseDisease>(item);
                            presMap!.PatientId = input.PatientId;
                            presMap!.PatientDiagnoseId = digService.PatientDiagnoseId;
                            presMap.CreatedOn = DateTime.Now;
                            presMap.CreatedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Create;
                            var _uowPresMap = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientFollowUp.GetDbContext());

                            // Disease Status
                            var dseSts = _mapper.Map<DiseaseStatus>(item);
                            dseSts.CreatedOn = DateTime.Now;
                            dseSts.CreatedBy = _tokenService.GetUserId();
                            dseSts.IsActive = true;
                            dseSts.ActionTypeId = (int)ActionTypeEnum.Create;
                            dseSts.DiseaseStatusId = Guid.NewGuid();
                            dseSts.PatientDiagnoseDiseaseId = presMap.PatientDiagnoseDiseaseId;
                            dseSts.DiseaseStatusTypeProfileId = item.DiseaseStatusTypeProfileId;
                            var _uowdseSts = new UnitOfWork<DiseaseStatus>(_uowPatientFollowUp.GetDbContext());

                            await _uowdseSts.Repository.Insert(dseSts);
                            await _uowdseSts.CommitAsync();
                            // End Disease Status

                            await _uowPresMap.Repository.Insert(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                    }

                    input.PatientDiagnoseId = digService.PatientDiagnoseId;
                    // Add Patient FollowUp
                    var patFollowUp = _mapper.Map<PatientFollowUp>(input);
                    var patientFollowup = await _uowPatientFollowUp.Repository.GetALL(x => x.PatientId == input.PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

                    if (AppCommonMethod.IsNullObject(patientFollowup))
                        patFollowUp.TotalFollowUpNo = 0;
                    else
                    {
                        //if (patientFollowup!.TotalFollowUpNo == null)
                        //    patFollowUp.TotalFollowUpNo = 0;
                        //else
                        patFollowUp.TotalFollowUpNo = (patientFollowup.TotalFollowUpNo ?? 0) + 1;
                    }


                    var today = DateTime.UtcNow.AddHours(5).Date;

                    var todayFollowUp = await _uowPatientFollowUp.Repository.GetALL(x => x.CreatedOn >= today).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(todayFollowUp))
                    {
                        _uowPatientFollowUp.Repository.Delete(todayFollowUp);
                        await _uowPatientFollowUp.Save();
                        patFollowUp.TotalFollowUpNo = patFollowUp.TotalFollowUpNo - 1;
                        if(patFollowUp.TotalFollowUpNo < 0)
                        {
                            patFollowUp.TotalFollowUpNo = 0;
                        }
                    }


                    if (patFollowUp.IsNcd == true)
                    {
                        var isNcdCount = _uowPatientFollowUp.Repository.GetCount(x => x.PatientId == input.PatientId && x.IsNcd == true);
                        patFollowUp.IsNcdFollowUps = isNcdCount > 0 ? isNcdCount - 1 : isNcdCount;
                    }

                    FillEntity(patFollowUp);
                    await _uowPatientFollowUp.Repository.Insert(patFollowUp);
                    await _uowPatientFollowUp.CommitAsync();

                    //var response = await __patientDiagnoseService.GetJsonObj(digDto.PatientVisitId, input.FormType!, digService.PatientDiagnoseId);

                    var response = await GetJsonObj(digDto.PatientVisitId, digService.PatientDiagnoseId);
                    //if (response.statusCode != System.Net.HttpStatusCode.OK)
                    //{
                    //    trans.Rollback();
                    //    throw new UserFriendlyException(CommonMessageConstant.WaitOperationTimeOut);
                    //}


                    PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    dbPatientDiagnoseRecord.PatientId = digDto.PatientId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientVisitId = digDto.PatientVisitId ?? Guid.Empty;
                    dbPatientDiagnoseRecord.PatientDiagnoseId = digService.PatientDiagnoseId;
                    dbPatientDiagnoseRecord.FormType = input!.FormType;
                    //dbPatientDiagnoseRecord.Json = response.data.ToString();
                    dbPatientDiagnoseRecord.Json = response;
                    dbPatientDiagnoseRecord.IsActive = true;

                    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    await _uowPatientDiagnoseRecord.Save();

                    dbObjPatientVisit.AttendedBy = null;
                    FillEntityPatientOpenVisit(dbObjPatientVisit);
                    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    await _uowPatientOpenVisit.Save();
                    trans.Commit();
                    return patFollowUp;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }


            }
        }

        public async Task<PatientFollowUp> Edit(CreateOrEditPatientFollowUpNcdClinicDto input)
        {
            try
            {
                // Add Patient PresCription
                if (!AppCommonMethod.IsNullOrEmptyList(input.PatientPrescription!))
                {
                    foreach (var item in input.PatientPrescription!)
                    {
                        if (AppCommonMethod.IsNullOrEmptyGuid(item.PatientPrescriptionId))
                        {
                            item!.PatientPrescriptionId = Guid.NewGuid();
                            var presMap = _mapper.Map<PatientPrescription>(item);

                            presMap!.PatientId = input.PatientId;
                            presMap!.PatientDiagnoseId = input.PatientDiagnoseId;
                            presMap!.PatientVisitId = input.PatientVisitId;
                            presMap.CreatedOn = DateTime.Now;
                            presMap.CreatedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Create;
                            var _uowPresMap = new UnitOfWork<PatientPrescription>(_uowPatientFollowUp.GetDbContext());
                            await _uowPresMap.Repository.Insert(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                        else
                        {
                            var _uowPresMap = new UnitOfWork<PatientPrescription>(_uowPatientFollowUp.GetDbContext());
                            var dbOb = await _uowPresMap.Repository.GetById(item.PatientPrescriptionId!);
                            var presMap = _mapper.Map(item, dbOb);
                            presMap.UpdatedOn = DateTime.Now;
                            presMap.UpdatedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Edit;
                            _uowPresMap.Repository.Update(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                    }
                }

                // Add PatientDiagnoseDisease
                if (!AppCommonMethod.IsNullObject(input.PatientDiagnoseDiaeasae))
                {
                    foreach (var item in input.PatientDiagnoseDiaeasae)
                    {
                        if (!AppCommonMethod.IsNullOrEmptyGuid(item.PatientDiagnoseDiseaseId))
                        {
                            var _uowPresMap = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientFollowUp.GetDbContext());
                            var dbOb = await _uowPresMap.Repository.GetById(item!.PatientDiagnoseDiseaseId!);
                            var presMap = _mapper.Map(item, dbOb);
                            presMap.UpdatedOn = DateTime.Now;
                            presMap.UpdatedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Edit;

                            // Disease Status
                            var _uowdseSts = new UnitOfWork<DiseaseStatus>(_uowPatientFollowUp.GetDbContext());
                            var dseObj = await _uowdseSts.Repository.GetById(item!.DiseaseStatusId!);
                            var dseSts = _mapper.Map(item, dseObj);
                            dseSts.UpdatedOn = DateTime.Now;
                            dseSts.UpdatedBy = _tokenService.GetUserId();
                            dseSts.ActionTypeId = (int)ActionTypeEnum.Edit;

                            _uowdseSts.Repository.Update(dseSts);
                            await _uowdseSts.CommitAsync();
                            // End Disease Status

                            _uowPresMap.Repository.Update(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                        else
                        {
                            var presMap = _mapper.Map<PatientDiagnoseDisease>(item);
                            presMap.PatientDiagnoseDiseaseId = Guid.NewGuid();
                            presMap.PatientId = input.PatientId;
                            presMap.PatientDiagnoseId = input.PatientDiagnoseId;
                            presMap.CreatedOn = DateTime.Now;
                            presMap.CreatedBy = _tokenService.GetUserId();
                            presMap.IsActive = true;
                            presMap.ActionTypeId = (int)ActionTypeEnum.Create;
                            var _uowPresMap = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientFollowUp.GetDbContext());

                            // Disease Status
                            var dseSts = _mapper.Map<DiseaseStatus>(item);
                            dseSts.CreatedOn = DateTime.Now;
                            dseSts.CreatedBy = _tokenService.GetUserId();
                            dseSts.IsActive = true;
                            dseSts.ActionTypeId = (int)ActionTypeEnum.Create;
                            dseSts.DiseaseStatusId = Guid.NewGuid();
                            dseSts.PatientDiagnoseDiseaseId = presMap.PatientDiagnoseDiseaseId;
                            dseSts.DiseaseStatusTypeProfileId = item.DiseaseStatusTypeProfileId;
                            var _uowdseSts = new UnitOfWork<DiseaseStatus>(_uowPatientFollowUp.GetDbContext());

                            await _uowdseSts.Repository.Insert(dseSts);
                            await _uowdseSts.CommitAsync();
                            // End Disease Status

                            await _uowPresMap.Repository.Insert(presMap);
                            await _uowPresMap.CommitAsync();
                        }
                    }
                }

                // Add Patient FollowUp
                var dbObj = await _uowPatientFollowUp.Repository.GetById(input.PatientFollowUpsId!);
                var patFollowUp = _mapper.Map(input, dbObj);
                FillEntity(patFollowUp);
                _uowPatientFollowUp.Repository.Update(patFollowUp);
                await _uowPatientFollowUp.CommitAsync();

                return patFollowUp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException!.Message);
            }
        }


        #endregion


        #region Get
        public async Task<DateTime?> GetPatientLastIssueBookLetDate(Guid PatientId)
        {
            var LastIssueBookLetDateTime = await _uowPatientFollowUp.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.LastIssueBookLetDateTime).Select(x => x.LastIssueBookLetDateTime).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullorEmptyDate(LastIssueBookLetDateTime))
                return (DateTime)LastIssueBookLetDateTime;
            return null;
        }



        public async Task<dynamic> GetJsonObj(Guid? input, Guid? diagnoseId)
        {
            var _uowPatient = new UnitOfWork<Patient>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientDiagnose = new UnitOfWork<PatientDiagnose>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientVital = new UnitOfWork<PatientVital>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientDiagnoseDiseases = new UnitOfWork<PatientDiagnoseDisease>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowPatientFollowUp.GetDbContext());
            var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientFollowUp.GetDbContext());
            var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientFollowUp.GetDbContext());
            var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowPatientFollowUp.GetDbContext());
            var _uowUser = new UnitOfWork<User>(_uowPatientFollowUp.GetDbContext());

            ViewPatientSlipDetailsDto patientVisit = new ViewPatientSlipDetailsDto();
            var patientdiagnose = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId).FirstOrDefaultAsync();
            var createdByDesignationId = _uowUser.Repository.GetALL(x => x.UserId == patientdiagnose.CreatedBy).Select(x => x.DesignationProfileId).FirstOrDefault();
            if (!AppCommonMethod.IsNullObject(patientdiagnose))
            {
                patientVisit.PatientDiagnoseId = patientdiagnose.PatientDiagnoseId;
                var PatientVisitData = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input).FirstOrDefaultAsync();
                var patientData = await _uowPatient.Repository.GetById(patientdiagnose.PatientId);
                if (!AppCommonMethod.IsNullObject(patientData))
                {
                    var _uowProfile = new UnitOfWork<HMIS.NCD.Domain.Models.DbModels.Profile>(_uowPatientFollowUp.GetDbContext());
                    patientVisit.PatientId = patientData.PatientId;
                    patientVisit.PatientOpenVisitId = PatientVisitData.PatientOpenVisitId;
                    patientVisit.PatientName = patientData.FullName;
                    patientVisit.GurdianName = patientData.GuardianName;
                    patientVisit.Age = patientData.Age;
                    patientVisit.Mrno = patientData.Mrno;
                    patientVisit.Dob = patientData.Dob;
                    patientVisit.Gender = await _uowProfile.Repository.GetALL(x => x.ProfileId == patientData.GenderProfileId).Select(x => x.Name).FirstOrDefaultAsync();
                    patientVisit.CNIC = patientData.Cnic;
                    patientVisit.ContactNo = patientData.MobileNo;
                    patientVisit.Address = patientData.ParmanentAddress;
                    //patientVisit.IsWillingToBuyMedPrivately = patinetDiagnose!.IsWillingToBuyMedPrivately,
                    patientVisit.NextVisitDate = patientdiagnose.FollowupDate;
                    patientVisit.Doctor = _uowUser.Repository.GetALL(x => x.UserId == patientdiagnose.CreatedBy).Select(x => x.FullName).FirstOrDefault();
                    patientVisit.DoctorDesignation = _uowProfile.Repository.GetALL(x => x.ProfileId == createdByDesignationId).Select(x => x.Name).FirstOrDefault();
                    patientVisit.FollowUpDate = patientdiagnose.FollowupDate;
                    patientVisit.PresentComplaints = patientdiagnose.PresentComplaints;
                    patientVisit.Examination = patientdiagnose.Examination;
                    patientVisit.PatientMedicalHistory = patientdiagnose.PatientMedicalHistory;
                    patientVisit.AdviseGiven = patientdiagnose.AdviseGiven;
                    patientVisit.FormType = patientdiagnose.FormType;

                    patientVisit.IsReferred = PatientVisitData.IsReferred;
                    patientVisit.ReferredDepartmentLookupId = PatientVisitData.ReferredDepartmentLookupId;
                    patientVisit.Department = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == PatientVisitData.ReferredDepartmentLookupId).Select(x => x.Name).FirstOrDefaultAsync();
                    patientVisit.ReferredDepartmentName = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == PatientVisitData.ReferredDepartmentLookupId).Select(x => x.Name).FirstOrDefaultAsync();
                    patientVisit.ReferredSectionLookupId = PatientVisitData.ReferredSectionLookupId;
                    patientVisit.ReferredSectionName = await _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == PatientVisitData.ReferredSectionLookupId).Select(x => x.Name).FirstOrDefaultAsync();
                    patientVisit.ReferredToDepartmentLookupId = (PatientVisitData.IsReferred == true) ? PatientVisitData.DepartementLookupId : null;
                    patientVisit.ReferredToDepartmentName = (PatientVisitData.IsReferred == true) ? patientVisit.ReferredDepartmentName : null;
                    patientVisit.ReferredToSectionLookupId = (PatientVisitData.IsReferred == true) ? PatientVisitData.SectionLookupId : null;
                    patientVisit.ReferredToSectionName = (PatientVisitData.IsReferred == true) ? patientVisit.ReferredSectionName : null;
                    patientVisit.ReferredBy = (PatientVisitData.IsReferred == true) ? PatientVisitData.ReferredBy : null;
                    patientVisit.CreatedOn = PatientVisitData.CreatedOn;
                    patientVisit.PatientVitals = await _uowPatientVital.Repository.GetALL(x => x.PatientVisitId == input).ToListAsync();
                    patientVisit.PatientDiagnosesDiseases = await _uowPatientDiagnoseDiseases.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
                    {
                        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
                        DiseaseProfileId = x.DiseaseProfileId,
                        DiseasesName = x.DiseaseProfile.Name
                    }).ToListAsync();

                    var patientPrescriptions = _uowPatientPrescription.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
                    .OrderByDescending(x => x.CreatedOn)
                    .Select(x => new PatientPrescriptionDto
                    {
                        MedicineId = x.MedicineId,
                        Days = x.Days,
                        //DoseName = x.DoseProfile!.Name,
                        DoseProfileId = x.DoseProfileId, // Add this line to get the DoseProfileId
                        //DoseTimeName = x.DoseTimeProfile!.Name,
                        DoseTimeProfileId = x.DoseTimeProfileId, // Add this line to get the DoseTimeProfileId
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
                    })
                    .ToList();

                    //foreach (var prescription in patientPrescriptions)
                    //{
                    //    prescription.DoseName = _uowProfile.Repository.GetALL(y => y.ProfileId == prescription.DoseProfileId)
                    //        .Select(x => x.Name)
                    //        .FirstOrDefault();

                    //    prescription.DoseTimeName = _uowProfile.Repository.GetALL(y => y.ProfileId == prescription.DoseTimeProfileId)
                    //        .Select(x => x.Name)
                    //        .FirstOrDefault();
                    //}

                    patientVisit.PatientMedicine = patientPrescriptions;

                    patientVisit.PatientLabTests = _uowPatientLabTest.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId).Select(x => new PatientLabTestDto
                    {
                        LabTestId = x.LabTestId,
                        LabDepartmentProfileId = x.LabDepartmentProfileId,
                        LabDepartmentName = x.LabDepartmentProfile!.Name,
                        LabTestName = x.LabTest!.Name,
                        PatientLabTestId = x.PatientLabTestId
                    }).ToList();



                    foreach (var item in patientVisit!.PatientDiagnosesDiseases)
                    {
                        if (string.IsNullOrEmpty(patientVisit.DiseasesName))
                            patientVisit.DiseasesName += item.DiseasesName;
                        else
                            patientVisit.DiseasesName += ", " + item.DiseasesName;
                    }

                }
            }


            return JsonConvert.SerializeObject(patientVisit);
        }
        #endregion

        #region Helper
        private void FillEntity(PatientFollowUp obj)
        {
            if (obj.PatientFollowUpsId == Guid.Empty)
            {
                obj.PatientFollowUpsId = Guid.NewGuid();
                obj.IsActive = true;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }

        private void FillEntityMentalHealthPatientFollowup(MentalHealthPatientFollowup obj)
        {
            if (obj.MentalHealthPatientFollowupsId == Guid.Empty)
            {
                obj.MentalHealthPatientFollowupsId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
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

        private void FillEntityDiagnoseRecord(PatientDiagnosisRecord obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDiagnosisRecordId))
            {
                obj.PatientDiagnosisRecordId = Guid.NewGuid();
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


        private void FillEntityPatientOpenVisit(PatientOpenVisit obj)
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
        #endregion
    }
}

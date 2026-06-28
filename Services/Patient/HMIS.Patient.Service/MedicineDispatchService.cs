using System.Linq.Expressions;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Repositories.UOW;
using HMIS.Patient.Service.Common;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using SMSSender.DTO;
using SMSSender;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;
using System.Linq;
using AuthBAL;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineData;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;

namespace HMIS.Patient.Service
{
    public class MedicineDispatchService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly PatientWorkFlowLogService<PatientWorkFlowLog> _patientWorkFlowLogService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<MedicineDispatch> _uowMedicineDispatch;
        private readonly UnitOfWork<PatientPrescription> _uowPatientPrescription;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly SMS _smsService;
        private readonly MIMSService _mimsService;
        private readonly MimsMedicineDataService _mimsMedicineDataService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly bool _isActiveMedicineDispenseFromMims;
        private readonly bool _isActiveOffline;
        #endregion

        #region Constructor

        public MedicineDispatchService(TokenService tokenService, PatientWorkFlowLogService<PatientWorkFlowLog> patientWorkFlowLogService,
            UnitOfWork<MedicineDispatch> uowMedicineDispatch,
            UnitOfWork<DbModel.Patient> uowPatient,
            UnitOfWork<PatientPrescription> uowPatientPrescription,
            IMapper mapper,
            SMS smsService,
            MIMSService mimsService,
            IConfiguration config,
            MimsMedicineDataService mimsMedicineDataService
        )
        {
            _tokenService = tokenService;
            _patientWorkFlowLogService = patientWorkFlowLogService;
            _uowMedicineDispatch = uowMedicineDispatch;
            _mapper = mapper;
            _uowPatient = uowPatient;
            _smsService = smsService;
            _uowPatientPrescription = uowPatientPrescription;
            _mimsService = mimsService;
            _mimsMedicineDataService = mimsMedicineDataService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
            _isActiveMedicineDispenseFromMims = config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActive") ?
                             config.GetSection("SystemMode").GetSection("Online").GetValue<bool>("IsActiveMedicineDispenseFromMims") :
                             (
                             config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                              config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActiveMedicineDispenseFromMims") :
                              false
                             );
            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                           config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;

        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditMedicineDispatchDto> CreateOrEdit(CreateOrEditMedicineDispatchDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.MedicineDispatchId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditMedicineDispatchDto> Create(CreateOrEditMedicineDispatchDto input)
        {
            var obj = _mapper.Map<MedicineDispatch>(input);
            FillEntity(obj);
            MedicineDispatch responseObj = await _uowMedicineDispatch.Repository.Insert(obj);
            await _uowMedicineDispatch.Save();
            return _mapper.Map<CreateOrEditMedicineDispatchDto>(responseObj);

        }

        private async Task<CreateOrEditMedicineDispatchDto> Update(CreateOrEditMedicineDispatchDto input)
        {
            var dbObj = await _uowMedicineDispatch.Repository.GetById(input.MedicineDispatchId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowMedicineDispatch.Repository.Update(obj!);
            await _uowMedicineDispatch.CommitAsync();
            return _mapper.Map<CreateOrEditMedicineDispatchDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowMedicineDispatch.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowMedicineDispatch.Repository.Update(dbObj!);
            await _uowMedicineDispatch.CommitAsync();
            return true;
        }


        public async Task UpdateMedicineRequisitionStatus(Guid PatientPrescriptionId)
        {
            using (var trans = _uowPatient.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var patientPrescriptions = await _uowPatientPrescription.Repository.GetById(PatientPrescriptionId);
                    if (!AppCommonMethod.IsNullObject(patientPrescriptions))
                    {
                        patientPrescriptions.Status = (byte)CommonConstant.dispensed;
                        _uowPatientPrescription.Repository.Update(patientPrescriptions);
                        await _uowPatientPrescription.Save();

                        var _uowMedicineAdviseRequisition = new UnitOfWork<MedicineAdvisedRequisition>(_uowPatient.GetDbContext());
                        var medicineAdviseRequisition = await _uowMedicineAdviseRequisition.Repository.GetALL(x => x.MedicineAdvisedRequisitionId == patientPrescriptions.MedicineAdvisedRequisitionId && x.Status != CommonConstant.dispensed).FirstOrDefaultAsync();

                        if (!AppCommonMethod.IsNullObject(medicineAdviseRequisition))
                        {
                            medicineAdviseRequisition.Status = (byte)CommonConstant.dispensed;
                            FillEntityMedicineAdvisedRequisition(medicineAdviseRequisition);
                            _uowMedicineAdviseRequisition.Repository.Update(medicineAdviseRequisition);
                            await _uowMedicineAdviseRequisition.Save();
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
        }

        public async Task<CreateOrEditPatientMedicineDispatchDto> CreatePatientDispatch(CreateOrEditPatientMedicineDispatchDto input)
        {


            var res = false;
            if (_isActiveOffline)
                res = await UpdateMimsMedicineAvailableQuantity(input);

            if (_isActiveMedicineDispenseFromMims)
            {
                foreach (var item in input.MedicineDispatches)
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
                    if (mimsDispatchResponse != null && mimsDispatchResponse.Status) // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                    {
                        item.MIMSDispatched = true;
                        if (!AppCommonMethod.IsNullOrEmptyGuid(input?.PatientVisitId))
                        {
                            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatient.GetDbContext());
                            var departmentId = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input.PatientVisitId).Select(x => x.DepartementLookupId).FirstOrDefaultAsync();
                            if (!AppCommonMethod.IsNullorZeroInt(departmentId))
                            {
                                if (departmentId == CommonConstant.IPD || departmentId == CommonConstant.Emergency)
                                {
                                    await UpdateMedicineRequisitionStatus((Guid)item.PatientPrescriptionId);
                                }
                            }
                        }
                    }
                    else if (mimsDispatchResponse.Data == null)
                        item.Reason = mimsDispatchResponse.Message;
                    else if (mimsDispatchResponse.Data.Count > 0)
                        item.Reason = mimsDispatchResponse.Data[0].Reason;

                }
            }
            else
            {
                foreach (var item in input.MedicineDispatches)
                {
                    item.Reason = CommonMessageConstant.MedicineDispenseViaMimsNotActive;
                }
            }

            using (var trans = _uowMedicineDispatch.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowMedicineDispatch.GetDbContext());
                    var _uowPatientPrescription = new UnitOfWork<PatientPrescription>(_uowMedicineDispatch.GetDbContext());
                    var _uowMedicineAdvisedRequisition = new UnitOfWork<MedicineAdvisedRequisition>(_uowMedicineDispatch.GetDbContext());
                    var _uowMedicineAdvised = new UnitOfWork<MedicineAdvised>(_uowMedicineDispatch.GetDbContext());

                    var patientPrescription = await _uowPatientPrescription.Repository.GetALL().Where(x => x.PatientVisitId == input.PatientVisitId)
                        .Include(x => x.DoseProfile).Include(x => x.DoseTimeProfile).ToListAsync();

                    PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == input.PatientVisitId!)
                        .Include(x => x.DepartementLookup)
                        .FirstOrDefaultAsync();
                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetALL(x => x.PatientId == input.PatientId!).Include(x => x.HealthFacility).FirstOrDefaultAsync();

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

                    if (dbObjPatientVisit!.IsDischarge == true)
                        throw new UserFriendlyException(CommonMessageConstant.VisitClosed);

                    List<Guid?> reqIds = new List<Guid?>();
                    //List<Object?> medAdv = new List<Object?>();

                    foreach (var item in input.MedicineDispatches)
                    {
                        var obj = _mapper.Map<MedicineDispatch>(item);
                        FillEntity(obj);
                        obj.PatientId = input.PatientId;
                        obj.PatientVisitId = input.PatientVisitId;
                        obj.Pharmacist = _tokenService.GetUserId();
                        obj.PatientDiagnoseId = item.PatientDiagnoseId;
                        obj.PatientPrescriptionId = item.PatientPrescriptionId;
                        obj.WardId = (item.WardId != 0 && (bool)input.IsPatientHistory) ? item.WardId : Convert.ToInt32(TokenService.GetMimsDepartmentId());

                        // Save External Medicine Dispatch Quantity
                        if (obj.MedicineId > 0 && obj.QuantityDispatch >= 0 && obj.QuantityDispatch <= obj.QuantityPrescribed)
                            obj.ExternalQuantity = obj.QuantityPrescribed - obj.QuantityDispatch;

                        // Save External Medicine Dispatch Quantity
                        if (obj.MedicineId > 0 && obj.QuantityDispatch >= 0)
                            obj.TotalAmount = (obj.UnitPrice ?? 0) * obj.QuantityDispatch;

                        await _uowMedicineDispatch.Repository.Insert(obj);
                        await _uowMedicineDispatch.Save();

                        // Update Prescription Status

                        var dbPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientPrescriptionId == obj.PatientPrescriptionId).FirstOrDefaultAsync();
                        
                        if (!AppCommonMethod.IsNullObject(dbPrescription))
                        {
                            var isExist = reqIds.Where(x => x == dbPrescription!.MedicineAdvisedRequisitionId).FirstOrDefault();
                            if(AppCommonMethod.IsNullOrEmptyGuid(isExist))
                                reqIds.Add(dbPrescription!.MedicineAdvisedRequisitionId);

                            dbPrescription!.Status = (byte)CommonConstant.dispensed;
                            dbPrescription!.UpdatedBy = _tokenService.GetUserId();
                            dbPrescription!.UpdatedOn = DateTime.Now;

                            _uowPatientPrescription.Repository.Update(dbPrescription!);
                            await _uowPatientPrescription.Save();

                            var medAdv = await _uowMedicineAdvised.Repository.GetALL(x => x.MedicineAdvisedId == dbPrescription.MedicineAdvisedId).FirstOrDefaultAsync();
                            if (!AppCommonMethod.IsNullObject(medAdv))
                            {
                                if(AppCommonMethod.IsNullorZeroInt(medAdv!.QuantityDispatch))
                                    medAdv.QuantityDispatch = dbPrescription!.Quantity;
                                else
                                    medAdv.QuantityDispatch += dbPrescription!.Quantity;

                                _uowMedicineAdvised.Repository.Update(medAdv!);
                                await _uowMedicineAdvised.Save();
                            }

                        }

                        // End Update Prescription Status

                        await UpdatePatientDiagnoseRecordJsonObj(item.PatientDiagnoseId);
                    }


                    // Update Requisition

                    var dbReq = await _uowMedicineAdvisedRequisition.Repository.GetALL(x => reqIds.Contains(x.MedicineAdvisedRequisitionId)).ToListAsync();

                    if (dbReq.Count > 0)
                    {
                        foreach (var item in dbReq)
                        {
                            item.Status = (byte)CommonConstant.dispensed;
                            item.UpdatedBy = _tokenService.GetUserId();
                            item.UpdatedOn = DateTime.Now;

                            _uowMedicineAdvisedRequisition.Repository.Update(item!);
                        }
                        await _uowMedicineAdvisedRequisition.Save();
                    }

                    // End Update Requisition

                    if (input.RiskFactors.Count > 0)
                    {
                        var _uowRiskFators = new UnitOfWork<RiskFactor>(_uowMedicineDispatch.GetDbContext());
                        foreach (var item in input.RiskFactors)
                        {
                            var obj = _mapper.Map<RiskFactor>(item);
                            FillEntityRiskFactors(obj);
                            obj.PatientId = input.PatientId;
                            obj.PatientVisitId = input.PatientVisitId;
                            obj.Answer = item.Answer;
                            await _uowRiskFators.Repository.Insert(obj);
                            await _uowRiskFators.Save();
                        }
                    }


                    dbObjPatientVisit.IsOccupied = false;
                    dbObjPatientVisit.OccupiedBy = null;

                    if (dbObjPatientVisit.VisitFor == CommonStringConstant.OPD)
                        dbObjPatientVisit.IsDischarge = true;
                    else
                        dbObjPatientVisit.IsDischarge = false;

                    //if (dbObjPatientVisit.DepartementLookup!.Name == CommonStringConstant.InPatientDepartment)
                    //    dbObjPatientVisit.IsDischarge = false;
                    //else
                    //    dbObjPatientVisit.IsDischarge = true;

                    dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                    dbObjPatientVisit.UpdatedOn = DateTime.Now;
                    dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    await _uowPatientOpenVisit.Save();

                    // Create Patient Work Log
                    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                    objPatientWorkFlowLog.PatientId = input.PatientId;
                    objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    objPatientWorkFlowLog.HealthFacilityId = dbObjPatientVisit!.HealthFacilityId;
                    objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                    objPatientWorkFlowLog.NextStationProfileId = null;

                    if (dbObjPatientVisit.VisitFor == CommonStringConstant.OPD)
                        objPatientWorkFlowLog.IsVisitClose = true;
                    else
                        objPatientWorkFlowLog.IsVisitClose = false;

                    //if (dbObjPatientVisit.DepartementLookup!.Name == CommonStringConstant.InPatientDepartment)
                    //    objPatientWorkFlowLog.IsVisitClose = false;
                    //else
                    //    objPatientWorkFlowLog.IsVisitClose = true;

                    //// SMS on Visit Close
                    //SendSMSDto smsObj = new SendSMSDto()
                    //{
                    //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                    //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                    //};

                    //_smsService.SendSMS(smsObj);

                    //SMS on Visit Close Send SMS
                    if (dbObjPatientVisit.VisitFor == CommonStringConstant.OPD) { 
                        if (input.MedicineDispatches.Count() > 0)  // if input has medicine then send medicine other wise not
                        {
                            SendSMSDto smsObj = new SendSMSDto()
                            {
                                Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                                Body = $"معزز {dbPatient.FirstName}،\r\n{dbPatient?.HealthFacility?.Name ?? ""} تشریف آوری کا شکریہ\r\nآپ کو او پی ڈی میں مندرجہ ذیل ادویات مفت فراہم کی گئی ہیں۔",
                            };


                            foreach (var item in input.MedicineDispatches)
                            {
                                var prescription = patientPrescription.Where(x => x.MedicineId == item.MedicineId).FirstOrDefault();

                                smsObj.Body += "\r\n" + item.MedicineName + " QTY : " + item.QuantityDispatch + "\r\n" + prescription?.DoseProfile?.Name.Replace("<br />", "\r\n").Replace("<br/>", "\r\n");

                                if (item.AvailableQuantity < item.QuantityPrescribed)
                                {
                                    var remainingQty = item.QuantityPrescribed - item.QuantityDispatch;

                                    smsObj.Body += "\r\n" + item.MedicineName + " QTY : " + item.QuantityDispatch + "\r\n" + prescription?.DoseProfile?.Name.Replace("<br />", "\r\n").Replace("<br/>", "\r\n");
                                }

                            }

                            _smsService.SendSMS(smsObj);

                        }
                        var otherSmsObj = new SendSMSDto()
                        {
                            Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                            Body = $"معزز {dbPatient.FirstName}،\r\n\"آپ کو اوپی ڈی میں مندرجہ ذیل ادویات پرا ئیویٹ فارمیسی سے تجویز کی گئی ہیں کیونکہ یہ ادویات ہسپتال کی فارمیسی میں موجود نہ ہیں۔"
                        };

                        int i = 0;
                        foreach (var item in input.MedicineDispatches)
                        {

                            var prescription = patientPrescription.Where(x => x.MedicineId == item.MedicineId).FirstOrDefault();

                            if (item.AvailableQuantity < item.QuantityPrescribed)
                            {
                                var remainingQty = item.QuantityPrescribed - item.QuantityDispatch;

                                //if (i <= 0)
                                //{
                                //    smsObj.Body = "\r\n \r\n" + "آپ کو اوپی ڈی میں مندرجہ ذیل ادویات پرا ئیویٹ فارمیسی سے تجویز کی گئی ہیں کیونکہ یہ ادویات ہسپتال کی فارمیسی میں موجود نہ ہیں۔";
                                //}
                                otherSmsObj.Body += "\r\n" + item.MedicineName + " QTY : " + remainingQty + "\r\n" + prescription?.DoseProfile?.Name.Replace("<br />", "\r\n").Replace("<br/>", "\r\n");
                                i = i + 1;
                            }
                        }

                        if (i > 0)
                        {
                            _smsService.SendSMS(otherSmsObj);
                        }

                    }


                    objPatientWorkFlowLog.IsActive = true;
                    await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                    trans.Commit();


                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return input;
        }

        public async Task UpdatePatientDiagnoseRecordJsonObj(Guid? diagnoseId)
        {
            var dbContextPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineDispatch.GetDbContext());

            var currentRecord = await dbContextPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefaultAsync();
            if (currentRecord != null)
            {
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
                        UnitPrice = item.UnitPrice,
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

            //foreach (var prop in jsonObj.Properties())
            //{
            //    //var targetProperty = jsonObj.Property(prop.Name);
            //    if (prop.Name == "PatientMedicine")
            //    {
            //        var res = JsonConvert.DeserializeObject<ViewPatientSlipDetailsDto>(jsonObj.Property(prop.Name));
            //    }
            //}

            //PatientMedicine

            //var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
            //.Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
            //.Select(y => new ViewPatientSlipDetailsDto
            //{
            //    PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
            //    PatientId = y.PatientId ?? Guid.Empty,
            //    Mrno = y.Patient!.Mrno,
            //    Doctor = y.DiagnosedByNavigation!.FullName,
            //    DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
            //    Section = y.DocSectionLookup!.Name,
            //    Department = y.DocDepartmentLookup!.Name,
            //    PatientName = y.Patient.FullName,
            //    GurdianName = y.Patient.GuardianName,
            //    Age = y.Patient.Age,
            //    Dob = y.Patient.Dob,
            //    Gender = y.Patient.GenderProfile!.Name,
            //    CNIC = y.Patient.Cnic,
            //    ContactNo = y.Patient.MobileNo,
            //    Address = y.Patient.ParmanentAddress,
            //    VisitDate = y.CreatedOn,
            //    IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
            //    NextVisitDate = y.FollowupDate,
            //    PresentComplaints = y.PresentComplaints,
            //    Examination = y.Examination,
            //    PatientMedicalHistory = y.PatientMedicalHistory,
            //    AdviseGiven = y.AdviseGiven,
            //    PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
            //    HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
            //    TokenNo = y.PatientVisit.TokenNo,
            //    CreatedOn = y.CreatedOn,
            //    CreatedBy = y.CreatedBy,
            //    PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnosesDiseasesDto
            //    {
            //        PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
            //        DiseaseProfileId = x.DiseaseProfileId,
            //        DiseasesName = x.DiseaseProfile.Name
            //    }).ToList(),

            //    PatientDiagnoseProcedures = y.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
            //    {
            //        PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
            //        PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
            //        SectionProcedureId = x.SectionProcedureId,
            //        ProcedureTitle = x.SectionProcedure!.ProcedureTitle
            //    }).ToList(),
            //    PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
            //    {
            //        MedicineId = x.MedicineId,
            //        Days = x.Days,
            //        DoseName = x.DoseProfile!.Name,
            //        DoseTimeName = x.DoseTimeProfile!.Name,
            //        MedicineName = x.MedicineName,
            //        PatientPrescriptionId = x.PatientPrescriptionId,
            //        Quantity = x.Quantity,
            //        AvailableQuantity = x.AvailableQuantity,
            //        MedicineDose = x.MedicineDose,
            //        MedicineRoute = x.MedicineRoute,
            //        MedicineFrequency = x.MedicineFrequency,
            //        MedicineInstruction = x.MedicineInstruction,
            //        MedicineDuration = x.MedicineDuration,
            //        BatchNo = x.BatchNo
            //    }).ToList(),
            //    PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
            //    {
            //        LabTestId = x.LabTestId,
            //        LabDepartmentProfileId = x.LabDepartmentProfileId,
            //        LabDepartmentName = x.LabDepartmentProfile!.Name,
            //        LabTestName = x.LabTest!.Name,
            //        PatientLabTestId = x.PatientLabTestId
            //    }).ToList(),
            //}).FirstOrDefaultAsync();


            //foreach (var item in patientVisit!.PatientDiagnosesDiseases)
            //{
            //    if (string.IsNullOrEmpty(patientVisit.DiseasesName))
            //        patientVisit.DiseasesName += item.DiseasesName;
            //    else
            //        patientVisit.DiseasesName += ", " + item.DiseasesName;
            //}


            //foreach (var item in patientVisit!.PatientDiagnoseProcedures)
            //{
            //    if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
            //        patientVisit.ProceduresName += item.ProcedureTitle;
            //    else
            //        patientVisit.ProceduresName += ", " + item.ProcedureTitle;
            //}

            //return JsonConvert.SerializeObject(patientVisit);
            //return patientVisit;
            //}
        }
        #endregion

        #region Read Operations

        public async Task<List<ViewMedicineDispatchDto>> GetAll(Expression<Func<MedicineDispatch, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<MedicineDispatch> responseObj = await _uowMedicineDispatch.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewMedicineDispatchDto>>(responseObj);
        }


        public async Task<ViewMedicineDispatchDto> GetById(int input)
        {
            MedicineDispatch? responseObj = await _uowMedicineDispatch.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewMedicineDispatchDto>(responseObj);
        }

        public async Task<ViewPagerDto<ViewMedicineDispatchListDto>> GetAllWithPagination(FilterPatientVitalDto filter)
        {
            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var finalList = _uowMedicineDispatch.GetDbContext().ViewMedicineDispatchLists
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.PatientName!.ToLower().StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.PatientMobileNo!.ToLower().StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedBy.ToString()!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn)
                .GroupBy(x => x.PatientVisitId)
                .Select(x =>
                new ViewMedicineDispatchListDto
                {
                    //MedicineDispatchId = x.MedicineDispatchId,
                    PatientVisitId = x.Select(x => x.PatientVisitId).FirstOrDefault(),
                    PatientId = x.Select(x => x.PatientId).FirstOrDefault(),
                    FullName = x.Select(x => x.PatientName).FirstOrDefault(),
                    MobileNo = x.Select(x => x.PatientMobileNo).FirstOrDefault(),
                    Mrno = x.Select(x => x.Mrno).FirstOrDefault(),
                    Cnic = x.Select(x => x.Cnic).FirstOrDefault(),
                    VisitDate = x.Select(x => x.VisitDate).FirstOrDefault(),
                    CreatedBy = x.Select(x => x.CreatedByName).FirstOrDefault(),
                    CreatedOn = x.Select(x => x.CreatedOn).FirstOrDefault(),
                    UpdatedBy = x.Select(x => x.UpdatedByName).FirstOrDefault(),
                    UpdatedOn = x.Select(x => x.UpdatedOn).FirstOrDefault()
                }).Distinct();

            var pagedList = await PagedListDto<ViewMedicineDispatchListDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewMedicineDispatchListDto>
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

        public async Task<List<ViewPatientPharmacyQueDto>> GetAllQue(int HealthFacilityId)
        {
            ////Pharmacy Station 
            ////var _uowUser = new UnitOfWork<User>(_uowMedicineDispatch.GetDbContext());
            //var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowMedicineDispatch.GetDbContext());
            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowMedicineDispatch.GetDbContext());

            ////var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            //var dbUser = TokenService.GetUserLoggedInfo();

            //Guid? pharmacyStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.PharmacyStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            //var responseObj = await _uowPatientOpenVisit.Repository.GetALL()
            //    .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
            //    .WhereIf((!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId) && dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.SectionLookupId == dbUser.SectionId)

            //    // If IPD don't show until Patient is Admitted in IPD
            //    .WhereIf((dbUser.DepartmentName == CommonStringConstant.InPatientDepartment), x => x.VisitFor == CommonStringConstant.IPD && x.IsAdmitted == true)
            //    .WhereIf((dbUser.DepartmentName == CommonStringConstant.ERDepartment), x => x.VisitFor == CommonStringConstant.ER && x.IsAdmitted == true)

            //    // if  OPD or NULL then only show today patient and OPD patient
            //    .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitDate == DateTime.Today
            //        && (x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null)
            //        && x.CurrentStationProfileId == pharmacyStation
            //        && (x.PharmacyAttendedBy != null ? x.PharmacyAttendedBy == _tokenService.GetUserId() : true))

            //    .Include(x => x.Patient)
            //    .Where(x =>
            //        x.IsDischarge != true &&
            //        //x.VisitDate == DateTime.Today && 
            //        x.HealthFacilityId == HealthFacilityId
            //        //x.CurrentStationProfileId == pharmacyStation &&
            //        //(x.PharmacyAttendedBy != null ? x.PharmacyAttendedBy == _tokenService.GetUserId() : true))
            //        //(x.IsOccupied == true && x.OccupiedBy != null ? x.OccupiedBy == _tokenService.GetUserId() : true)
            //        )
            //    .OrderBy(x => x.TokenNo)
            //    .Select(y => new ViewPatientQueDto
            //    {
            //        PatientVisitId = y.PatientOpenVisitId,
            //        PatientId = y.PatientId,
            //        TokenNo = y.TokenNo,
            //        Mrno = y.Patient!.Mrno,
            //        CNIC = y.Patient.Cnic,
            //        MobileNo = y.Patient.MobileNo,
            //        FirstName = y.Patient.FirstName,
            //        LastName = y.Patient.LastName,
            //        FullName = y.Patient.FullName
            //    }).ToListAsync();
            //return _mapper.Map<List<ViewPatientQueDto>>(responseObj);

            var dbUser = TokenService.GetUserLoggedInfo();
            var DepartmentType = CommonStringConstant.OPD;

            if (dbUser!.DepartmentName == CommonStringConstant.InPatientDepartment)
                DepartmentType = CommonStringConstant.IPD;
            else if(dbUser!.DepartmentName == CommonStringConstant.ERDepartment)
                DepartmentType = CommonStringConstant.ER;

            using (var db = new HmisAuthContext())
            {
                //var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetAllQueForPharmacy]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                    if (!string.IsNullOrEmpty(DepartmentType))
                        sqlComm.Parameters.AddWithValue("@DepartmentType", DepartmentType);

                    if (AppCommonMethod.IsNullOrEmptyGuid(dbUser.UserId))
                        sqlComm.Parameters.AddWithValue("@UserId", dbUser.UserId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", dbUser.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", dbUser.SectionId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewPatientPharmacyQueDto> lst = ds.Tables[0].ToList<ViewPatientPharmacyQueDto>();
                    //List<string> lst2 = ds.Tables[0].ToList<string>();
                    return lst;
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

        public async Task<ViewPharmacySlipDto> GetPharmacySlipByVisitId(Guid VisitId)
        {
            var responseObj = await _uowMedicineDispatch.Repository.GetALL(x => x.PatientVisitId == VisitId)
                .Include(x => x.PharmacistNavigation)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.HealthFacility)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.MedicineDispatches)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.PatientDiagnoses)
                        .ThenInclude(x => x!.DiagnosedByNavigation)
                .Include(x => x.Patient)
                .Select(x => new ViewPharmacySlipDto
                {
                    HealthFacilityName = x.PatientVisit!.HealthFacility!.Name!,
                    PatientName = x.Patient!.FullName!,
                    MrNo = x.Patient.Mrno,
                    TokenNo = x.PatientVisit!.TokenNo!,
                    VisitNo = x.PatientVisit.VisitNo,
                    VisitDate = x.PatientVisit.VisitDate,
                    CreatedOn = x.PatientVisit.CreatedOn,
                    PrescribedBy = x.PatientVisit.PatientDiagnoses.FirstOrDefault()!.DiagnosedByNavigation!.FullName!,
                    Pharmacist = x.PharmacistNavigation!.FullName!,

                    MedicineDispatchLists = x.PatientVisit.MedicineDispatches
                    .Select(y => new MedicineDispatchListDto
                    {
                        MedicineId = y.MedicineId!,
                        Name = y.MedicineName!,
                        QuantityPrescribed = y.QuantityPrescribed!,
                        QuantityDispatch = y.QuantityDispatch!,
                    }).ToList(),
                })
                .FirstOrDefaultAsync();

            return responseObj;
        }

        #endregion

        #region Helper Methods

        private void FillEntity(MedicineDispatch obj)
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




        private void FillEntityMedicineAdvisedRequisition(MedicineAdvisedRequisition obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.MedicineAdvisedRequisitionId))
            {
                obj.MedicineAdvisedRequisitionId = Guid.NewGuid();
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



        private void FillEntityPatientPrescription(PatientPrescription obj)
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

        private void FillEntityRiskFactors(RiskFactor obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.RiskFactorId))
            {
                obj.RiskFactorId = Guid.NewGuid();
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

        private void FillEntityDelete(MedicineDispatch obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        public async Task<bool> UpdateMimsMedicineAvailableQuantity(CreateOrEditPatientMedicineDispatchDto input)
        {
            foreach (var item in input.MedicineDispatches)
            {
                if (item.MedicineId > 0)
                {
                    // Old Working
                    //var dbObj = await _mimsMedicineDataService.GetByMedicineIdWardId(item.MedicineId, Convert.ToInt32(TokenService.GetMimsDepartmentId()));

                    //if (AppCommonMethod.IsNullObject(dbObj))
                    //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //if (dbObj.AvailableQuantity == null || item.QuantityDispatch > dbObj.AvailableQuantity)
                    //    throw new UserFriendlyException("Sorry," + item.MedicineName + " cannot be dispense, available stock is " + dbObj.AvailableQuantity);

                    //dbObj.AvailableQuantity = dbObj.AvailableQuantity - item.QuantityDispatch;
                    //dbObj.TotalDispatchQuantity = (dbObj.TotalDispatchQuantity ?? 0) + item.QuantityDispatch;

                    //CreateOrEditMimsMedicineDataDto mimsMedicineDataDto = new CreateOrEditMimsMedicineDataDto();
                    //mimsMedicineDataDto.MimsMedicineDataId = dbObj.MimsMedicineDataId;
                    //mimsMedicineDataDto.MedicineId = dbObj.MedicineId;
                    //mimsMedicineDataDto.MedicineName = dbObj.MedicineName;
                    //mimsMedicineDataDto.MedicineTypeId = dbObj.MedicineTypeId;
                    //mimsMedicineDataDto.MedicineTypeName = dbObj.MedicineTypeName;
                    //mimsMedicineDataDto.WardId = dbObj.WardId;
                    //mimsMedicineDataDto.WardName = dbObj.WardName;
                    //mimsMedicineDataDto.TotalDispatchQuantity = dbObj.TotalDispatchQuantity;
                    //mimsMedicineDataDto.AvailableQuantity = dbObj.AvailableQuantity;
                    //mimsMedicineDataDto.TotalQuantity = dbObj.TotalQuantity;

                    //await _mimsMedicineDataService.CreateOrEdit(mimsMedicineDataDto);
                    // End Old Working


                    var dbUser = TokenService.GetUserLoggedInfo();

                    if (AppCommonMethod.IsNullOrEmptyGuid(dbUser!.MimsBranchId))
                        throw new Exception(CommonStringConstant.MIMSBranchIdNotAssignedtoUser);

                    using (var db = new HmisAuthContext())
                    {
                        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                        try
                        {
                            DataSet ds = new DataSet();
                            SqlCommand sqlComm = new SqlCommand("[mims].[SPMedicineDispenceFromStock]", (SqlConnection)conn);
                            sqlComm.CommandType = CommandType.StoredProcedure;

                            if (!AppCommonMethod.IsNullorZeroInt(dbUser!.HealthFacilityId))
                                sqlComm.Parameters.AddWithValue("@HealthFacilityId", dbUser.HealthFacilityId);

                            if (!AppCommonMethod.IsNullOrEmptyGuid(dbUser!.MimsBranchId))
                                sqlComm.Parameters.AddWithValue("@MimsBranchId", dbUser.MimsBranchId);

                            if (!AppCommonMethod.IsNullorZeroInt(item.MedicineId))
                                sqlComm.Parameters.AddWithValue("@MedicineId", item.MedicineId);

                            if (!AppCommonMethod.IsNullorZeroInt(item.QuantityDispatch))
                                sqlComm.Parameters.AddWithValue("@QuantityDispatch", item.QuantityDispatch);

                            SqlDataAdapter da = new SqlDataAdapter();
                            da.SelectCommand = sqlComm;
                            await Task.Run(() => da.Fill(ds));
                            List<ResponseSPDispenceMedicine> res = ds.Tables[0].ToList<ResponseSPDispenceMedicine>();

                            var result = new ResponseSPDispenceMedicine();
                            if (res.Count > 0)
                            {
                                result = res.FirstOrDefault();
                                if (result.Response == false && result.AvailableQuantity < item.QuantityDispatch)
                                    throw new UserFriendlyException("Sorry," + item.MedicineName + " cannot be dispense, available stock is " + result.AvailableQuantity);
                            }

                            return result.Response;
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        finally
                        {
                            //conn.Close();
                        }
                    }
                }
                return false;
            }
            return false;
        }
        #endregion


        #region IPD

        public async Task<List<ViewPatientQueDto>> GetAllIpdQue(int HealthFacilityId)
        {

            //Pharmacy Station 
            //var _uowUser = new UnitOfWork<User>(_uowMedicineDispatch.GetDbContext());
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowMedicineDispatch.GetDbContext());
            //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowMedicineDispatch.GetDbContext());

            //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            var user = TokenService.GetUserLoggedInfo();

            //Guid? pharmacyStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.PharmacyStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = await _uowPatientOpenVisit.GetDbContext().ViewGetAllIpdQueues
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(user!.DepartmentId), x => x.DepartementLookupId == user.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(user!.SectionId), x => x.SectionLookupId == user.SectionId)
                .Where(x =>
                    x.IsDischarge != true &&
                    x.HealthFacilityId == HealthFacilityId &&
                    //(x.PharmacyAttendedBy != null ? x.PharmacyAttendedBy == _tokenService.GetUserId() : true))
                    (x.IsOccupied == true && x.OccupiedBy != null ? x.OccupiedBy == _tokenService.GetUserId() : true)
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y => new ViewPatientQueDto
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
                    BedNo = y.BedNo
                }).ToListAsync();
            return _mapper.Map<List<ViewPatientQueDto>>(responseObj);
        }

        #endregion
    }
}

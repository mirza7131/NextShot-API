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
using AuthDAL.Models.DbModels;

namespace HMIS.Patient.Service
{
    public class EmergencyDoctorService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<DbModel.PatientDiagnose> _uowPatientDiagnose;
        private readonly UnitOfWork<DbModel.User> _uowUser;
        private readonly UnitOfWork<DbModel.Patient> _uowPatient;
        private readonly UnitOfWork<DbModel.PatientOpenVisit> _uowPatientOpenVisit;
        #endregion

        #region Constructor

        public EmergencyDoctorService(
            TokenService tokenService, 
            IMapper mapper,
            UnitOfWork<DbModel.PatientDiagnose> uowPatientDiagnose,
            UnitOfWork<DbModel.User> uowUser,
            UnitOfWork<DbModel.Patient> uowPatient,
            UnitOfWork<DbModel.PatientOpenVisit> uowPatientOpenVisit
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientDiagnose = uowPatientDiagnose;
            _uowUser = uowUser;
            _uowPatient = uowPatient;
            _uowPatientOpenVisit = uowPatientOpenVisit;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDiagnoseDto> CreateOrEditPatientDiagnose(CreateOrEditPatientDiagnoseDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                return await CreatePatientDiagnose(input);
            else
                return await UpdatePatientDiagnose(input);
        }

        private async Task<CreateOrEditPatientDiagnoseDto> CreatePatientDiagnose(CreateOrEditPatientDiagnoseDto input)
        {   
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    if (input.FollowupDate != null)
                        input.FollowupDate = input.FollowupDate.Value.AddHours(5);

                    //if (string.IsNullOrEmpty(input.FormType))
                    //{
                    //    input.FormType = CommonStringConstant.GeneralForm;
                    //}

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    //var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    //var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    //var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    //var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    //var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientDiagnose.GetDbContext());
                    //var _uowPatientScreening = new UnitOfWork<PatientScreening>(_uowPatientDiagnose.GetDbContext());
                    //var _uowPatientAssessment = new UnitOfWork<PatientAssessment>(_uowPatientDiagnose.GetDbContext());
                    //var _uowPatientVaccination = new UnitOfWork<PatientVaccination>(_uowPatientDiagnose.GetDbContext());
                    var _uowPatientOpenVisits = new UnitOfWork<DbModel.PatientOpenVisit>(_uowPatientDiagnose.GetDbContext());

                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();

                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    DbModel.PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisits.Repository.GetById(input.PatientVisitId!);

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

                    //_uowPatient.Repository.Update(dbPatient);
                    //await _uowPatient.Save();

                    var objPatientDiagnose = _mapper.Map<DbModel.PatientDiagnose>(input);
                    FillEntityPatientDiagnose(objPatientDiagnose);


                    objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    //if ((bool)input.IsPatientHistory)
                    //{
                    //    objPatientDiagnose.DocDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                    //    objPatientDiagnose.DocSectionLookupId = dbObjPatientVisit.SectionLookupId;
                    //}
                    //else
                    //{
                        objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                        objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;
                    //}

                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientLabTests.Clear();

                    input.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //objPatientDiagnose.FormType = input.FormType;

                    foreach (var item in objPatientDiagnose.PatientDiagnoseDiseases)
                    {
                        //item.PatientDiagnoseDiseaseId = null;
                        var objPatientDiagnoseDisease = _mapper.Map<DbModel.PatientDiagnoseDisease>(item);


                        objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                        objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                        objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                        objPatientDiagnoseDisease.PatientDiagnoseDiseaseId = Guid.NewGuid();
                        objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;
                    }


                    await _uowPatientDiagnose.Repository.Insert(objPatientDiagnose);
                    await _uowPatientDiagnose.Save();




                    //// Diagnose Diseases Check
                    //var _uowPatientDiagnoseDisease = new UnitOfWork<DbModel.PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());
                   
                    //foreach (var item in input.PatientDiagnoseDiseases)
                    //{
                    //    item.PatientDiagnoseDiseaseId = null;
                    //    var objPatientDiagnoseDisease = _mapper.Map<DbModel.PatientDiagnoseDisease>(item);


                    //    objPatientDiagnoseDisease.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //    objPatientDiagnoseDisease.CreatedBy = _tokenService.GetUserId();
                    //    objPatientDiagnoseDisease.CreatedOn = DateTime.Now;
                    //    objPatientDiagnoseDisease.PatientDiagnoseDiseaseId = Guid.NewGuid();
                    //    objPatientDiagnoseDisease.ActionTypeId = (int)ActionTypeEnum.Create;

                    //    await _uowPatientDiagnoseDisease.Repository.Insert(objPatientDiagnoseDisease);
                    //    await _uowPatientDiagnoseDisease.Save();

                    //}
                    
                    trans.Commit();

                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientDiagnoseDto>(input);
    }

        private async Task<CreateOrEditPatientDiagnoseDto> UpdatePatientDiagnose(CreateOrEditPatientDiagnoseDto input)
        {
            using (var trans = _uowPatientDiagnose.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    //if (input.FollowupDate != null)
                    //    input.FollowupDate = input.FollowupDate.Value.AddHours(5);
                    //if (string.IsNullOrEmpty(input.FormType))
                    //{
                    //    input.FormType = CommonStringConstant.GeneralForm;
                    //}

                    var isVisitClose = false;
                    var tokenUserId = _tokenService.GetUserId();

                    //var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowPatientDiagnose.GetDbContext());

                    //var _uowPatientLabTestDetail = new UnitOfWork<PatientLabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    //var _uowLabTest = new UnitOfWork<LabTest>(_uowPatientDiagnose.GetDbContext());
                    //var _uowLabTestDetail = new UnitOfWork<LabTestDetail>(_uowPatientDiagnose.GetDbContext());

                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();



                    DbModel.Patient? dbPatient = await _uowPatient.Repository.GetById(input.PatientId!);

                    if (AppCommonMethod.IsNullObject(dbPatient))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    DbModel.PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //dbPatient!.FollowupDate = input.FollowupDate;

                    _uowPatient.Repository.Update(dbPatient);
                    await _uowPatient.Save();


                    var objPatientDiagnose = new DbModel.PatientDiagnose();
                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    {
                        objPatientDiagnose = await _uowPatientDiagnose.Repository.GetById(input.PatientDiagnoseId!);
                        objPatientDiagnose!.FollowupDate = input.FollowupDate;
                        objPatientDiagnose.AdviseGiven = input.AdviseGiven;
                        objPatientDiagnose.Examination = input.Examination;
                        objPatientDiagnose.PatientMedicalHistory = input.PatientMedicalHistory;
                        objPatientDiagnose.PresentComplaints = input.PresentComplaints;
                        //objPatientDiagnose.IsVerifiedByConsultant = input.IsVerifiedByConsultant;
                        //objPatientDiagnose.IsMlc = input.IsMlc;
                        //objPatientDiagnose.IsSendToCdc = input.IsSendToCdc;
                    }
                    else
                        objPatientDiagnose = _mapper.Map<DbModel.PatientDiagnose>(input);

                    FillEntityPatientDiagnose(objPatientDiagnose);

                    if (dbObjPatientVisit!.IsFromPmis)
                        objPatientDiagnose.DiagnosedBy = dbObjPatientVisit.AttendedBy;
                    else
                        objPatientDiagnose.DiagnosedBy = _tokenService.GetUserId();

                    var diagnosedByUser = await _uowUser.Repository.GetById(objPatientDiagnose.DiagnosedBy!);

                    objPatientDiagnose.DocDepartmentLookupId = diagnosedByUser!.DepartmentId;
                    objPatientDiagnose.DocSectionLookupId = diagnosedByUser!.SectionId;

                    objPatientDiagnose.PatientPrescriptions.Clear();
                    objPatientDiagnose.PatientDiagnoseProcedures.Clear();



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
                    var _uowPatientDiagnoseDisease = new UnitOfWork<DbModel.PatientDiagnoseDisease>(_uowPatientDiagnose.GetDbContext());

                    List<DbModel.PatientDiagnoseDisease> dbListPatientDiagnoseDisease = await _uowPatientDiagnoseDisease.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

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

                    // Delete diagnose Diagnose procedures
                    //var _uowPatientDiagnoseProcedure = new UnitOfWork<PatientDiagnoseProcedure>(_uowPatientDiagnose.GetDbContext());

                    //List<PatientDiagnoseProcedure> dbListPatientDiagnoseProcedure = await _uowPatientDiagnoseProcedure.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //foreach (var item in dbListPatientDiagnoseProcedure)
                    //{
                    //    if (input.PatientDiagnoseProcedures.Count() > 0 && input.PatientDiagnoseProcedures.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                    //    {
                    //        var tempDiagnoseProcedure = input.PatientDiagnoseProcedures.Where(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId).FirstOrDefault();

                    //        item.Feedback = tempDiagnoseProcedure.Feedback;
                    //        item.IsPerformed = tempDiagnoseProcedure.IsPerformed;
                    //        item.PerformedBy = tempDiagnoseProcedure.PerformedBy;
                    //        item.RecommendBy = tempDiagnoseProcedure.RecommendBy;
                    //        item.AssistedBy = tempDiagnoseProcedure.AssistedBy;
                    //        item.ToothNumber = tempDiagnoseProcedure.ToothNumber;
                    //        item.ToothPosition = tempDiagnoseProcedure.ToothPosition;
                    //        item.UpdatedBy = _tokenService.GetUserId();
                    //        item.UpdatedOn = DateTime.Now;
                    //        item.ActionTypeId = (int)ActionTypeEnum.Edit;
                    //        _uowPatientDiagnoseProcedure.Repository.Update(item);
                    //    }
                    //    else
                    //    {
                    //        item.DeletedBy = _tokenService.GetUserId();
                    //        item.DeletedOn = DateTime.Now;
                    //        item.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    //        _uowPatientDiagnoseProcedure.Repository.Update(item);
                    //    }

                    //    await _uowPatientDiagnoseProcedure.Save();

                    //}

                    //foreach (var item in input.PatientDiagnoseProcedures)
                    //{
                    //    if (dbListPatientDiagnoseProcedure.Count() > 0 && !dbListPatientDiagnoseProcedure.Any(x => x.PatientDiagnoseProcedureId == item.PatientDiagnoseProcedureId))
                    //    {
                    //        var dbObj = _mapper.Map<PatientDiagnoseProcedure>(item);
                    //        dbObj.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //        dbObj.PatientDiagnoseProcedureId = Guid.NewGuid();
                    //        dbObj.CreatedBy = _tokenService.GetUserId();
                    //        dbObj.CreatedOn = DateTime.Now;
                    //        dbObj.ActionTypeId = (int)ActionTypeEnum.Create;
                    //        dbObj.IsActive = true;
                    //        dbObj.AssistedBy = item.AssistedBy;

                    //        await _uowPatientDiagnoseProcedure.Repository.Insert(dbObj);
                    //        await _uowPatientDiagnoseProcedure.Save();
                    //    }

                    //}
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
                    //    //var dbListPatientPrescription = await _uowPatientPrescription.Repository.GetALL(x => x.PatientDiagnoseId == objPatientDiagnose.PatientDiagnoseId && x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

                    //    // Visit Edit Case
                    //    foreach (var itemPatientPrescription in input.PatientPrescriptions)
                    //    {
                    //        var dbObj = await _uowPatientPrescription.Repository.GetById(itemPatientPrescription.PatientPrescriptionId);

                    //        if (dbObj != null)
                    //        {
                    //            FillEntityPrescription(dbObj);

                    //            if (dbObjPatientVisit!.IsFromPmis)
                    //                dbObj.PrescribedBy = dbObjPatientVisit.AttendedBy;
                    //            else
                    //                dbObj.PrescribedBy = _tokenService.GetUserId();

                    //            _uowPatientPrescription.Repository.Update(dbObj);
                    //        }
                    //        else
                    //        {

                    //            var objPatientPrescription = _mapper.Map<PatientPrescription>(itemPatientPrescription);

                    //            objPatientPrescription.PatientId = input.PatientId;
                    //            objPatientPrescription.PatientVisitId = input.PatientVisitId;
                    //            objPatientPrescription.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //            if (dbObjPatientVisit!.IsFromPmis)
                    //                objPatientPrescription.PrescribedBy = dbObjPatientVisit.AttendedBy;
                    //            else
                    //                objPatientPrescription.PrescribedBy = _tokenService.GetUserId();


                    //            FillEntityPrescription(objPatientPrescription);
                    //            await _uowPatientPrescription.Repository.Insert(objPatientPrescription);

                    //        }

                    //        await _uowPatientPrescription.Save();
                    //    }
                    //}


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

                    //// Diagnose Diseases Check

                    foreach (var item in input.PatientDiagnoseDiseases)
                    {
                        item.PatientDiagnoseDiseaseId = null;
                        var objPatientDiagnoseDisease = _mapper.Map<DbModel.PatientDiagnoseDisease>(item);


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


                    //if (input.IsRefer) // if refered
                    //{

                    //    // Create Patient Visit Flow
                    //    CreateOrEditPatientVisitFlowDto objPatientVisitFlow = new CreateOrEditPatientVisitFlowDto();

                    //    //objPatientDiagnoseReferLog.CurrentDepartmentId = dbObjPatientVisit.DepartementLookupId;
                    //    //objPatientDiagnoseReferLog.CurrentSectionId = dbObjPatientVisit.SectionLookupId;

                    //    //objPatientDiagnoseReferLog.PreviousDepartmentId = dbObjPatientVisit.ReferredDepartmentLookupId;
                    //    //objPatientDiagnoseReferLog.PreviousSectionId = dbObjPatientVisit.ReferredSectionLookupId;

                    //    objPatientVisitFlow.CurrentDepartmentId = input.ReferDepartment;
                    //    objPatientVisitFlow.CurrentSectionId = input.ReferSection;

                    //    objPatientVisitFlow.PreviousDepartmentId = dbObjPatientVisit.DepartementLookupId;
                    //    objPatientVisitFlow.PreviousSectionId = dbObjPatientVisit.SectionLookupId;

                    //    objPatientVisitFlow.PatientVisitId = dbObjPatientVisit.PatientOpenVisitId;
                    //    objPatientVisitFlow.HealthFacilityId = dbObjPatientVisit.HealthFacilityId;

                    //    objPatientVisitFlow.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;

                    //    objPatientVisitFlow.ReferedBy = _tokenService.GetUserId();

                    //    var sectionLookup = await _uowPatient.GetDbContext().SectionLookups.Where(x => x.SectionLookupId == input.ReferSection).FirstOrDefaultAsync();

                    //    objPatientVisitFlow.IsFilterClinic = sectionLookup!.IsFilterClinic;
                    //    objPatientVisitFlow.IsConsultant = sectionLookup!.IsConsultant;

                    //    await _patientVisitFlowService.CreateOrEdit(objPatientVisitFlow);

                    //    dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;

                    //    dbObjPatientVisit.IsReferred = true;
                    //    dbObjPatientVisit.ReferredHealthFacilityId = dbObjPatientVisit.HealthFacilityId;
                    //    dbObjPatientVisit.ReferredDepartmentLookupId = dbObjPatientVisit.DepartementLookupId;
                    //    dbObjPatientVisit.ReferredSectionLookupId = dbObjPatientVisit.SectionLookupId;
                    //    dbObjPatientVisit.ReferredBy = _tokenService.GetUserId();

                    //    dbObjPatientVisit.AttendedBy = null;

                    //    dbObjPatientVisit.DepartementLookupId = input.ReferDepartment;
                    //    dbObjPatientVisit.SectionLookupId = input.ReferSection;

                    //    dbObjPatientVisit.IsOccupied = false;
                    //    dbObjPatientVisit.OccupiedBy = null;

                    //    // Update Station In Case of Refer
                    //    var stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                    //                .OrderBy(x => x.SequenceNo)
                    //            .Select(x => new ViewStationDto
                    //            {
                    //                StationProfileId = x.ProfileId,
                    //                SequenceNo = x.SequenceNo,
                    //                ShortName = x.ShortName,
                    //                Name = x.Name
                    //            }).ToListAsync();

                    //    var currentStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                    //    dbObjPatientVisit.CurrentStationProfileId = currentStation!.StationProfileId;

                    //    // Update Station In Case of Refer

                    //    // if refer to IPD
                    //    var _uowDepartmentLookup = new UnitOfWork<DepartmentLookup>(_uowPatientDiagnose.GetDbContext());

                    //    var currentDepartment = await _uowDepartmentLookup.Repository.GetALL(x => x.DepartmentLookupId == input.ReferDepartment)
                    //    .Select(x => x.DisplayName).FirstOrDefaultAsync();

                    //    if (!string.IsNullOrEmpty(currentDepartment) && currentDepartment == CommonStringConstant.IPD)
                    //    {
                    //        var userInfo = TokenService.GetUserLoggedInfo();

                    //        dbObjPatientVisit.IsAdmittedInIpd = false;
                    //        dbObjPatientVisit.IsReferredIpd = true;
                    //        dbObjPatientVisit.IpdDepartmentLookupId = input.ReferDepartment;
                    //        dbObjPatientVisit.IpdSectionLookupId = input.ReferSection;
                    //        dbObjPatientVisit.IpdReferredBy = _tokenService.GetUserId();
                    //        dbObjPatientVisit.IpdReferredByDepartmentLookupId = userInfo!.DepartmentId;
                    //        dbObjPatientVisit.IpdReferredBySectionLookupId = userInfo!.SectionId;
                    //    }

                    //    dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                    //    dbObjPatientVisit.UpdatedOn = DateTime.Now;
                    //    dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    //    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    //    await _uowPatientOpenVisit.Save();

                    //    // Create Patient Work Log
                    //    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                    //    objPatientWorkFlowLog.PatientId = input.PatientId;
                    //    objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    //    objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                    //    objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                    //    objPatientWorkFlowLog.NextStationProfileId = null;
                    //    //objPatientWorkFlowLog.IsVisitClose = true;
                    //    objPatientWorkFlowLog.IsActive = true;
                    //    //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                    //    await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);
                    //}
                    //else if (input.IsVisitClose)
                    //{

                    //    dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                    //    //dbObjPatientVisit!.IsDischarge = true;

                    //    dbObjPatientVisit.IsOccupied = false;
                    //    dbObjPatientVisit.OccupiedBy = null;
                    //    dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                    //    dbObjPatientVisit.UpdatedOn = DateTime.Now;
                    //    dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    //    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    //    await _uowPatientOpenVisit.Save();

                    //    // Create Patient Work Log
                    //    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                    //    objPatientWorkFlowLog.PatientId = input.PatientId;
                    //    objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    //    objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                    //    objPatientWorkFlowLog.CurrentStationProfileId = dbObjPatientVisit!.CurrentStationProfileId;
                    //    objPatientWorkFlowLog.NextStationProfileId = null;
                    //    objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                    //    objPatientWorkFlowLog.IsActive = true;
                    //    //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                    //    await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                    //    // SMS on Visit Close If Not Refer to Pharmacy in case no medidine Advice 
                    //    //SendSMSDto smsObj = new SendSMSDto()
                    //    //{
                    //    //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                    //    //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                    //    //};

                    //    //_smsService.SendSMS(smsObj);

                    //}
                    //else
                    //{
                    //    //check for next station
                    //    var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
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
                    //        //var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatient.GetDbContext());

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

                    //    var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.DoctorStation).FirstOrDefault();

                    //    if (AppCommonMethod.IsNullObject(thisStation))
                    //        throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                    //    var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                    //    if (AppCommonMethod.IsNullObject(nextStation))
                    //    {
                    //        //isVisitClose = true;
                    //        nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                    //        // SMS on Visit Close
                    //        //SendSMSDto smsObj = new SendSMSDto()
                    //        //{
                    //        //    Receiver = dbPatient!.MobileNo!.Replace("-", ""),
                    //        //    Body = $"Dear {dbPatient!.FirstName}, Thank You for Your Visit."
                    //        //};


                    //        //_smsService.SendSMS(smsObj);
                    //    }



                    //    //PatientOpenVisit? dbObjPatientVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    //    //if (AppCommonMethod.IsNullObject(dbObjPatientVisit))
                    //    //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    //    dbObjPatientVisit!.IsWillingToBuyMedPrivately = input.IsWillingToBuyMedPrivately;
                    //    dbObjPatientVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                    //    dbObjPatientVisit.IsDischarge = isVisitClose;

                    //    dbObjPatientVisit.IsOccupied = false;
                    //    dbObjPatientVisit.OccupiedBy = null;
                    //    dbObjPatientVisit.UpdatedBy = _tokenService.GetUserId();
                    //    dbObjPatientVisit.UpdatedOn = DateTime.Now;
                    //    dbObjPatientVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    //    _uowPatientOpenVisit.Repository.Update(dbObjPatientVisit);
                    //    await _uowPatientOpenVisit.Save();

                    //    // Create Patient Work Log
                    //    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();
                    //    objPatientWorkFlowLog.PatientId = input.PatientId;
                    //    objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    //    objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                    //    objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                    //    objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation.StationProfileId : null;
                    //    objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                    //    objPatientWorkFlowLog.IsActive = true;
                    //    //objPatientWorkFlowLog.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPatient.PatientVitals.FirstOrDefault()!.CreatedOn, DateTime.Now);
                    //    await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);


                    //}

                    //// Store Json Object
                    //var jsonObj = await GetJsonObj(input.PatientVisitId!, input.FormType, objPatientDiagnose.PatientDiagnoseId);

                    //PatientDiagnosisRecord dbPatientDiagnoseRecord = new PatientDiagnosisRecord();

                    //if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    //{
                    //    dbPatientDiagnoseRecord = await _uowPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == input.PatientDiagnoseId).FirstOrDefaultAsync();

                    //}

                    //// CASE IF
                    //dbPatientDiagnoseRecord.PatientId = input.PatientId ?? Guid.Empty;
                    //dbPatientDiagnoseRecord.PatientVisitId = input!.PatientVisitId ?? Guid.Empty;
                    //dbPatientDiagnoseRecord.PatientDiagnoseId = objPatientDiagnose.PatientDiagnoseId;
                    //dbPatientDiagnoseRecord.FormType = input.FormType;
                    //dbPatientDiagnoseRecord.Json = jsonObj;
                    //dbPatientDiagnoseRecord.IsActive = true;
                    //FillEntityDiagnoseRecord(dbPatientDiagnoseRecord);

                    //if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientDiagnoseId))
                    //{
                    //    _uowPatientDiagnoseRecord.Repository.Update(dbPatientDiagnoseRecord!);
                    //}
                    //else
                    //{
                    //    await _uowPatientDiagnoseRecord.Repository.Insert(dbPatientDiagnoseRecord!);
                    //}

                    //await _uowPatientDiagnoseRecord.Save();


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

            return _mapper.Map<CreateOrEditPatientDiagnoseDto>(input);
        }

        //    public async Task UpdatePatientDiagnoseRecordJsonObj(Guid? diagnoseId)
        //{
        //    var dbContextPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineDispatch.GetDbContext());

        //    var currentRecord = await dbContextPatientDiagnoseRecord.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefaultAsync();

        //    var jsonObj = JsonConvert.DeserializeObject<ViewPatientSlipDetailsDto>(currentRecord.Json);

        //    var medicineDispatches = await _uowMedicineDispatch.Repository.GetALL(x => x.PatientDiagnoseId == diagnoseId).ToListAsync();

        //    List<Domain.Models.DTO.PatientDto.MedicineDispatchDto> medicineDispatch = new List<Domain.Models.DTO.PatientDto.MedicineDispatchDto>();

        //    foreach (var item in jsonObj.PatientMedicine)
        //    {
        //        medicineDispatch.Add(new Domain.Models.DTO.PatientDto.MedicineDispatchDto
        //        {
        //            //MedicineDispatchId 
        //            MedicineId = item.MedicineId,
        //            MedicineName = item.MedicineName,
        //            Days = item.Days,
        //            DoseName = item.DoseName,
        //            DoseTimeName = item.DoseTimeName,
        //            Quantity = item.Quantity,
        //            AvailableQuantity = item.AvailableQuantity,
        //            MedicineDose = item.MedicineDose,
        //            MedicineRoute = item.MedicineRoute,
        //            MedicineFrequency = item.MedicineFrequency,
        //            MedicineInstruction = item.MedicineInstruction,
        //            MedicineDuration = item.MedicineDuration,
        //            BatchNo = item.BatchNo,
        //            QuantityPrescribed = medicineDispatches.Where(x => x.MedicineId == item.MedicineId && x.PatientPrescriptionId == item.PatientPrescriptionId).Select(x => x.QuantityPrescribed).FirstOrDefault(),
        //            QuantityDispatch = medicineDispatches.Where(x => x.MedicineId == item.MedicineId && x.PatientPrescriptionId == item.PatientPrescriptionId).Select(x => x.QuantityDispatch).FirstOrDefault(),
        //        });
        //    }

        //    jsonObj.MedicineDispatch = medicineDispatch;

        //    currentRecord.Json = JsonConvert.SerializeObject(jsonObj);

        //    var _uowPatientDiagnoseRecord = new UnitOfWork<PatientDiagnosisRecord>(_uowMedicineDispatch.GetDbContext());


        //    currentRecord.UpdatedBy = _tokenService.GetUserId();
        //    currentRecord.UpdatedOn = DateTime.Now;
        //    currentRecord.ActionTypeId = (int)ActionTypeEnum.Edit;

        //    _uowPatientDiagnoseRecord.Repository.Update(currentRecord!);
        //    await _uowPatientDiagnoseRecord.CommitAsync();

        //}

        #endregion

        #region Read Operations

        //public async Task<dynamic> GetJsonObj(Guid? input, string formType, Guid? diagnoseId)
        //{
        //    if (formType == CommonStringConstant.PhysiotherapyFormOPD)
        //    {
        //        var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Select(y => new
        //        {
        //            PatientOpenVisitId = y.PatientVisitId,
        //            PatientId = y.PatientId ?? Guid.Empty,
        //            Mrno = y.Patient!.Mrno,
        //            Doctor = y.DiagnosedByNavigation!.FullName,
        //            DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
        //            Section = y.DocSectionLookup!.Name,
        //            Department = y.DocDepartmentLookup!.Name,
        //            PatientName = y.Patient.FullName,
        //            GurdianName = y.Patient.GuardianName,
        //            Age = y.Patient.Age,
        //            Dob = y.Patient.Dob,
        //            Gender = y.Patient.GenderProfile!.Name,
        //            CNIC = y.Patient.Cnic,
        //            ContactNo = y.Patient.MobileNo,
        //            Address = y.Patient.ParmanentAddress,
        //            VisitDate = y.CreatedOn,
        //            IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
        //            NextVisitDate = y.FollowupDate,
        //            VisitTypeProfileId = y.PatientVisit.VisitTypeProfileId,
        //            VisitTypeName = y.PatientVisit.VisitTypeProfile!.Name,

        //            IsReferred = y.PatientVisit.IsReferred,
        //            ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //            ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //            ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //            ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //            ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
        //            ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
        //            ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
        //            ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

        //            ReferredBy = y.PatientVisit.ReferredBy,
        //            ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

        //            HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //            TokenNo = y.PatientVisit.TokenNo,

        //            CreatedOn = y.CreatedOn,
        //            CreatedBy = y.CreatedByNavigation!.FullName,

        //            PhysiotherapistForm = y.PhysiotherapyForms.Select(x => new
        //            {
        //                x.PhysiotherapyFormId,
        //                x.PatientDiagnoseId,
        //                x.PresentingComplaint,
        //                x.ProblemSince,
        //                x.AnyComorbidity,
        //                x.DrugHistory,
        //                x.SignificantExaminationFindings,
        //                x.TotalDurationOfTreatmentSession,
        //                x.DischargeFromPhysicalTherapyTreatment,
        //                x.HomeExercisePlan,
        //                x.TreatmentAtDepartment,
        //                x.Prognosis,
        //                x.ClinicalDiagnosis,
        //                x.PhysiotherapyDiagnosis,
        //                x.PlanOfCare,
        //                x.IsActive

        //            }).FirstOrDefault(),

        //            PhysiotherapyModalities = y.PhysiotherapyForms.FirstOrDefault()!.PhysiotherapyModalities.Select(x => new
        //            {
        //                PhysiotherapyModalitiesId = x.PhysiotherapyModalitiesId,
        //                PhysiotherapyFormId = x.PhysiotherapyFormId,
        //                DepartmentLookupId = x.DepartmentLookupId,
        //                ModalitiesProfileId = x.ModalitiesProfileId,
        //                ModalitiesProfileName = x.Name,
        //                Value = x.Value,
        //                IsActive = x.IsActive,

        //            }).ToList(),

        //            PatientVitals = y.PatientVisit.PatientVitals.OrderByDescending(x => x.CreatedOn).Select(x => new
        //            {
        //                PatientVitalId = x.PatientVitalId,
        //                Bpsystolic = x.Bpsystolic,
        //                BpdiaSystolic = x.BpdiaSystolic,
        //                Pulse = x.Pulse,
        //                Temprature = x.Temprature,
        //                Weight = x.Weight,
        //                Height = x.Height,
        //                ResperatoryRate = x.ResperatoryRate,
        //                VitalsCollectedBy = x.VitalsCollectedBy,
        //                VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
        //                IsActive = x.IsActive,
        //                CreatedOn = x.CreatedOn

        //            }).FirstOrDefault(),

        //            //PatientMedicine = y.PatientDiagnoses.Where(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefault()!.PatientPrescriptions.Select(x => new PatientPrescriptionDto
        //            //{
        //            //    MedicineId = x.MedicineId,
        //            //    Days = x.Days,
        //            //    DoseName = x.DoseProfile!.Name,
        //            //    DoseTimeName = x.DoseTimeProfile!.Name,
        //            //    MedicineName = x.MedicineName,
        //            //    PatientPrescriptionId = x.PatientPrescriptionId,
        //            //    Quantity = x.Quantity,
        //            //    AvailableQuantity = x.AvailableQuantity

        //            //}).ToList(),

        //        }).FirstOrDefaultAsync();

        //        return JsonConvert.SerializeObject(patientVisit);

        //    }
        //    else if (formType == CommonStringConstant.PhysiotherapyFormIPD)
        //    {
        //        var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Select(y => new
        //        {

        //            PatientOpenVisitId = y.PatientVisitId,
        //            PatientId = y.PatientId ?? Guid.Empty,
        //            Mrno = y.Patient!.Mrno,
        //            Doctor = y.DiagnosedByNavigation!.FullName,
        //            DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
        //            Section = y.DocSectionLookup!.Name,
        //            Department = y.DocDepartmentLookup!.Name,
        //            PatientName = y.Patient.FullName,
        //            GurdianName = y.Patient.GuardianName,
        //            Age = y.Patient.Age,
        //            Dob = y.Patient.Dob,
        //            Gender = y.Patient.GenderProfile!.Name,
        //            CNIC = y.Patient.Cnic,
        //            ContactNo = y.Patient.MobileNo,
        //            Address = y.Patient.ParmanentAddress,
        //            VisitDate = y.CreatedOn,
        //            IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
        //            NextVisitDate = y.Patient.FollowupDate,
        //            VisitTypeProfileId = y.PatientVisit.VisitTypeProfileId,
        //            VisitTypeName = y.PatientVisit.VisitTypeProfile!.Name,

        //            IsReferred = y.PatientVisit.IsReferred,
        //            ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //            ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //            ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //            ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //            ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
        //            ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
        //            ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
        //            ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

        //            ReferredBy = y.PatientVisit.ReferredBy,
        //            ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

        //            HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //            TokenNo = y.PatientVisit.TokenNo,

        //            CreatedOn = y.CreatedOn,
        //            CreatedBy = y.CreatedByNavigation!.FullName,

        //            PhysiotherapistForm = y.PhysiotherapyForms.Select(x => new
        //            {
        //                x.PhysiotherapyFormId,
        //                x.PatientDiagnoseId,
        //                x.PresentingComplaint,
        //                x.ProblemSince,
        //                x.AnyComorbidity,
        //                x.DrugHistory,
        //                x.SignificantExaminationFindings,
        //                x.TotalDurationOfTreatmentSession,
        //                x.Prognosis,
        //                x.ClinicalDiagnosis,
        //                x.PhysiotherapyDiagnosis,
        //                x.KeyTreatment,
        //                x.PlanOfCare,

        //                x.FrequencyOfExercise,
        //                x.IntensityOfExercise,
        //                x.TypeOfExercise,

        //                x.DischargePlanOfCareProfileId,
        //                DischargePlanOfCareProfileName = x.DischargePlanOfCareProfile!.Name,
        //                x.IsActive

        //            }).FirstOrDefault(),

        //            PhysiotherapyModalities = y.PhysiotherapyForms.FirstOrDefault()!.PhysiotherapyModalities.Select(x => new
        //            {
        //                PhysiotherapyModalitiesId = x.PhysiotherapyModalitiesId,
        //                PhysiotherapyFormId = x.PhysiotherapyFormId,
        //                DepartmentLookupId = x.DepartmentLookupId,
        //                ModalitiesProfileId = x.ModalitiesProfileId,
        //                ModalitiesProfileName = x.Name,
        //                Value = x.Value,
        //                IsActive = x.IsActive,

        //            }).ToList(),

        //            PatientVitals = y.PatientVisit.PatientVitals.OrderByDescending(x => x.CreatedOn).Select(x => new
        //            {
        //                PatientVitalId = x.PatientVitalId,
        //                Bpsystolic = x.Bpsystolic,
        //                BpdiaSystolic = x.BpdiaSystolic,
        //                Pulse = x.Pulse,
        //                Temprature = x.Temprature,
        //                Weight = x.Weight,
        //                Height = x.Height,
        //                ResperatoryRate = x.ResperatoryRate,
        //                VitalsCollectedBy = x.VitalsCollectedBy,
        //                VitalsCollectedByName = x.VitalsCollectedByNavigation!.FullName,
        //                IsActive = x.IsActive,
        //                CreatedOn = x.CreatedOn

        //            }).FirstOrDefault(),

        //            //PatientMedicine = y.PatientDiagnoses.Where(x => x.PatientDiagnoseId == diagnoseId).FirstOrDefault()!.PatientPrescriptions.Select(x => new PatientPrescriptionDto
        //            //{
        //            //    MedicineId = x.MedicineId,
        //            //    Days = x.Days,
        //            //    DoseName = x.DoseProfile!.Name,
        //            //    DoseTimeName = x.DoseTimeProfile!.Name,
        //            //    MedicineName = x.MedicineName,
        //            //    PatientPrescriptionId = x.PatientPrescriptionId,
        //            //    Quantity = x.Quantity,
        //            //    AvailableQuantity = x.AvailableQuantity

        //            //}).ToList(),

        //        }).FirstOrDefaultAsync();

        //        return JsonConvert.SerializeObject(patientVisit);

        //    }
        //    else if (formType == CommonStringConstant.NutritionForm)
        //    {
        //        var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
        //        .Select(y => new ViewPatientSlipDetailsDto
        //        {
        //            PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
        //            PatientDiagnoseId = y.PatientDiagnoseId,
        //            PatientId = y.PatientId ?? Guid.Empty,
        //            Mrno = y.Patient!.Mrno,
        //            Doctor = y.DiagnosedByNavigation!.FullName,
        //            DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
        //            Section = y.DocSectionLookup!.Name,
        //            Department = y.DocDepartmentLookup!.Name,
        //            PatientName = y.Patient.FullName,
        //            GurdianName = y.Patient.GuardianName,
        //            Age = y.Patient.Age,
        //            Dob = y.Patient.Dob,
        //            Gender = y.Patient.GenderProfile!.Name,
        //            CNIC = y.Patient.Cnic,
        //            ContactNo = y.Patient.MobileNo,
        //            Address = y.Patient.ParmanentAddress,
        //            VisitDate = y.CreatedOn,
        //            IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
        //            //NextVisitDate = y.FollowupDate,
        //            PresentComplaints = y.PresentComplaints,
        //            Examination = y.Examination,
        //            PatientMedicalHistory = y.PatientMedicalHistory,
        //            AdviseGiven = y.AdviseGiven,

        //            IsReferred = y.PatientVisit.IsReferred,
        //            ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //            ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //            ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //            ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //            ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
        //            ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
        //            ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
        //            ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

        //            ReferredBy = y.PatientVisit.ReferredBy,
        //            ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

        //            PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
        //            HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //            TokenNo = y.PatientVisit.TokenNo,
        //            CreatedOn = y.CreatedOn,
        //            CreatedBy = y.CreatedBy,
        //            IsMlc = y.IsMlc,
        //            IsSendToCdc = y.IsSendToCdc,
        //            PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
        //            {
        //                PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //                DiseaseProfileId = x.DiseaseProfileId,
        //                DiseasesName = x.DiseaseProfile.Name
        //            }).ToList(),

        //            PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
        //            {
        //                LabTestId = x.LabTestId,
        //                LabDepartmentProfileId = x.LabDepartmentProfileId,
        //                LabDepartmentName = x.LabDepartmentProfile!.Name,
        //                LabTestName = x.LabTest!.Name,
        //                PatientLabTestId = x.PatientLabTestId
        //            }).ToList(),

        //        }).FirstOrDefaultAsync();


        //        foreach (var item in patientVisit!.PatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DiseasesName))
        //                patientVisit.DiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DiseasesName += ", " + item.DiseasesName;
        //        }

        //        foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
        //                patientVisit.DefinitiveDiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
        //        }


        //        foreach (var item in patientVisit!.PatientDiagnoseProcedures)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
        //                patientVisit.ProceduresName += item.ProcedureTitle;
        //            else
        //                patientVisit.ProceduresName += ", " + item.ProcedureTitle;
        //        }

        //        return JsonConvert.SerializeObject(patientVisit);

        //    }
        //    else if (
        //        formType == CommonStringConstant.SurgeryForm ||
        //        formType == CommonStringConstant.PsychiatryForm ||
        //        formType == CommonStringConstant.PsychologyForm ||
        //        formType == CommonStringConstant.SpeechTherapyForm ||
        //        formType == CommonStringConstant.OccupationalTherapyForm ||
        //        formType == CommonStringConstant.TechnologyForm
        //    )
        //    {
        //        var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
        //        .Select(y => new ViewPatientSlipDetailsDto
        //        {
        //            PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
        //            PatientDiagnoseId = y.PatientDiagnoseId,
        //            PatientId = y.PatientId ?? Guid.Empty,
        //            Mrno = y.Patient!.Mrno,
        //            Doctor = y.DiagnosedByNavigation!.FullName,
        //            DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
        //            Section = y.DocSectionLookup!.Name,
        //            Department = y.DocDepartmentLookup!.Name,
        //            PatientName = y.Patient.FullName,
        //            GurdianName = y.Patient.GuardianName,
        //            Age = y.Patient.Age,
        //            Dob = y.Patient.Dob,
        //            Gender = y.Patient.GenderProfile!.Name,
        //            CNIC = y.Patient.Cnic,
        //            ContactNo = y.Patient.MobileNo,
        //            Address = y.Patient.ParmanentAddress,
        //            VisitDate = y.CreatedOn,
        //            IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
        //            //NextVisitDate = y.FollowupDate,
        //            PresentComplaints = y.PresentComplaints,
        //            Examination = y.Examination,
        //            PatientMedicalHistory = y.PatientMedicalHistory,
        //            AdviseGiven = y.AdviseGiven,

        //            IsReferred = y.PatientVisit.IsReferred,
        //            ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //            ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //            ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //            ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //            ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
        //            ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
        //            ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
        //            ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

        //            ReferredBy = y.PatientVisit.ReferredBy,
        //            ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,

        //            PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
        //            HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //            TokenNo = y.PatientVisit.TokenNo,
        //            CreatedOn = y.CreatedOn,
        //            CreatedBy = y.CreatedBy,
        //            IsMlc = y.IsMlc,
        //            IsSendToCdc = y.IsSendToCdc,
        //            PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
        //            {
        //                PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //                DiseaseProfileId = x.DiseaseProfileId,
        //                DiseasesName = x.DiseaseProfile.Name
        //            }).ToList(),


        //            //PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
        //            //{
        //            //    MedicineId = x.MedicineId,
        //            //    Days = x.Days,
        //            //    DoseName = x.DoseProfile!.Name,
        //            //    DoseTimeName = x.DoseTimeProfile!.Name,
        //            //    MedicineName = x.MedicineName,
        //            //    PatientPrescriptionId = x.PatientPrescriptionId,
        //            //    Quantity = x.Quantity,
        //            //    AvailableQuantity = x.AvailableQuantity,
        //            //    MedicineDose = x.MedicineDose,
        //            //    MedicineRoute = x.MedicineRoute,
        //            //    MedicineFrequency = x.MedicineFrequency,
        //            //    MedicineInstruction = x.MedicineInstruction,
        //            //    MedicineDuration = x.MedicineDuration,
        //            //    BatchNo = x.BatchNo,
        //            //    MedicineResourceProfileId = x.MedicineResourceProfileId,
        //            //    MedicineTypeProfileId = x.MedicineTypeProfileId,
        //            //    UnitPrice = x.UnitPrice
        //            //}).ToList(),


        //        }).FirstOrDefaultAsync();


        //        foreach (var item in patientVisit!.PatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DiseasesName))
        //                patientVisit.DiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DiseasesName += ", " + item.DiseasesName;
        //        }

        //        foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
        //                patientVisit.DefinitiveDiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
        //        }


        //        foreach (var item in patientVisit!.PatientDiagnoseProcedures)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
        //                patientVisit.ProceduresName += item.ProcedureTitle;
        //            else
        //                patientVisit.ProceduresName += ", " + item.ProcedureTitle;
        //        }

        //        return JsonConvert.SerializeObject(patientVisit);
        //        //return patientVisit;
        //    }
        //    else
        //    { // General OPD

        //        var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
        //        .Select(y => new ViewPatientSlipDetailsDto
        //        {
        //            PatientOpenVisitId = y.PatientVisitId ?? Guid.Empty,
        //            PatientDiagnoseId = y.PatientDiagnoseId,
        //            PatientId = y.PatientId ?? Guid.Empty,
        //            Mrno = y.Patient!.Mrno,
        //            Doctor = y.DiagnosedByNavigation!.FullName,
        //            DoctorDesignation = y.DiagnosedByNavigation!.DesignationProfile!.Name,
        //            Section = y.DocSectionLookup!.Name,
        //            Department = y.DocDepartmentLookup!.Name,
        //            PatientName = y.Patient.FullName,
        //            GurdianName = y.Patient.GuardianName,
        //            Age = y.Patient.Age,
        //            Dob = y.Patient.Dob,
        //            Gender = y.Patient.GenderProfile!.Name,
        //            CNIC = y.Patient.Cnic,
        //            ContactNo = y.Patient.MobileNo,
        //            Address = y.Patient.ParmanentAddress,
        //            VisitDate = y.CreatedOn,
        //            IsWillingToBuyMedPrivately = y.PatientVisit!.IsWillingToBuyMedPrivately,
        //            NextVisitDate = y.FollowupDate,
        //            PresentComplaints = y.PresentComplaints,
        //            Examination = y.Examination,
        //            PatientMedicalHistory = y.PatientMedicalHistory,
        //            AdviseGiven = y.AdviseGiven,

        //            IsReferred = y.PatientVisit.IsReferred,
        //            ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //            ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //            ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //            ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //            ReferredToDepartmentLookupId = y.PatientVisit.DepartementLookupId,
        //            ReferredToDepartmentName = y.PatientVisit.DepartementLookup!.Name,
        //            ReferredToSectionLookupId = y.PatientVisit.SectionLookupId,
        //            ReferredToSectionName = y.PatientVisit.SectionLookup!.Name,

        //            ReferredBy = y.PatientVisit.ReferredBy,
        //            ReferredByName = y.PatientVisit.ReferredByNavigation!.FullName,


        //            PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
        //            HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //            TokenNo = y.PatientVisit.TokenNo,
        //            CreatedOn = y.CreatedOn,
        //            CreatedBy = y.CreatedBy,
        //            IsMlc = y.IsMlc,
        //            IsSendToCdc = y.IsSendToCdc,
        //            PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
        //            {
        //                PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //                DiseaseProfileId = x.DiseaseProfileId,
        //                DiseasesName = x.DiseaseProfile.Name
        //            }).ToList(),
        //            DefinitivePatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.DiagnoseTypeId == 2).Select(x => new PatientDiagnosesDiseasesDto
        //            {
        //                PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //                DiseaseProfileId = x.DiseaseProfileId,
        //                DiseasesName = x.DiseaseProfile.Name
        //            }).ToList(),

        //            PatientDiagnoseProcedures = y.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
        //            {
        //                PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
        //                PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
        //                SectionProcedureId = x.SectionProcedureId,
        //                ProcedureTitle = x.SectionProcedure!.ProcedureTitle,
        //                IsPerformed = x.IsPerformed,
        //                Feedback = x.Feedback,
        //                RecommendBy = x.RecommendBy,
        //                PerformedBy = x.PerformedBy,
        //                ToothNumber = x.ToothNumber,
        //                ToothPosition = x.ToothPosition,
        //                AssistedBy = x.AssistedBy,


        //            }).ToList(),
        //            PatientMedicine = y.PatientPrescriptions.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderByDescending(x => x.CreatedOn).Select(x => new PatientPrescriptionDto
        //            {
        //                MedicineId = x.MedicineId,
        //                Days = x.Days,
        //                DoseName = x.DoseProfile!.Name,
        //                DoseTimeName = x.DoseTimeProfile!.Name,
        //                MedicineName = x.MedicineName,
        //                PatientPrescriptionId = x.PatientPrescriptionId,
        //                Quantity = x.Quantity,
        //                AvailableQuantity = x.AvailableQuantity,
        //                MedicineDose = x.MedicineDose,
        //                MedicineRoute = x.MedicineRoute,
        //                MedicineFrequency = x.MedicineFrequency,
        //                MedicineInstruction = x.MedicineInstruction,
        //                MedicineDuration = x.MedicineDuration,
        //                BatchNo = x.BatchNo,
        //                MedicineResourceProfileId = x.MedicineResourceProfileId,
        //                MedicineTypeProfileId = x.MedicineTypeProfileId,
        //                UnitPrice = x.UnitPrice
        //            }).ToList(),
        //            PatientLabTests = y.PatientLabTests.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientLabTestDto
        //            {
        //                LabTestId = x.LabTestId,
        //                LabDepartmentProfileId = x.LabDepartmentProfileId,
        //                LabDepartmentName = x.LabDepartmentProfile!.Name,
        //                LabTestName = x.LabTest!.Name,
        //                PatientLabTestId = x.PatientLabTestId
        //            }).ToList(),
        //        }).FirstOrDefaultAsync();


        //        foreach (var item in patientVisit!.PatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DiseasesName))
        //                patientVisit.DiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DiseasesName += ", " + item.DiseasesName;
        //        }

        //        foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
        //                patientVisit.DefinitiveDiseasesName += item.DiseasesName;
        //            else
        //                patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
        //        }


        //        foreach (var item in patientVisit!.PatientDiagnoseProcedures)
        //        {
        //            if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
        //                patientVisit.ProceduresName += item.ProcedureTitle;
        //            else
        //                patientVisit.ProceduresName += ", " + item.ProcedureTitle;
        //        }

        //        return JsonConvert.SerializeObject(patientVisit);
        //        //return patientVisit;
        //    }
        //}

        #endregion

        #region Helper Methods

        private void FillEntityPatientDiagnose(DbModel.PatientDiagnose obj)
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

        #endregion


    



}
}
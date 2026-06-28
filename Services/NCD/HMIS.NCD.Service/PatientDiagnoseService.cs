using HMIS.NCD.Domain.Models.DbModels;
using JWTAuthentication;
using HMIS.NCD.Domain.Repositories._UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.NCD.Domain.Models.DTO;
using AutoMapper;
using CommonDTOs.Enums;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace HMIS.NCD.Service
{
    public class PatientDiagnoseService
    {
        #region Class Fields & Propeties
        private readonly TokenService _tokenService;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PatientDiagnoseService(TokenService tokenService, UnitOfWork<PatientDiagnose> uowPatientDiagnose, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientDiagnose = uowPatientDiagnose;
            _mapper = mapper;
        }
        #endregion

        #region CUD
        public async Task<PatientDiagnoseDto> InsertPatientDiagnose(PatientDiagnoseDto Input)
        {
            Input.PatientDiagnoseId = Guid.NewGuid();
            var diagnose = _mapper.Map<PatientDiagnose>(Input);
            diagnose.DoctorVisitNo = 1;
            diagnose.DiagnosedBy = _tokenService.GetUserId();
            diagnose.IsDiagnoseExternally = false;
            diagnose.FollowupDate = Input.NextFollowUpDate;
            diagnose.IsActive = true;
            diagnose.ActionTypeId = 1;
            diagnose.CreatedOn = DateTime.Now;
            diagnose.CreatedBy = _tokenService.GetUserId();

            var dg = await _uowPatientDiagnose.Repository.Insert(diagnose);
            await _uowPatientDiagnose.CommitAsync();

            return Input;
        }



        //public async Task<dynamic> GetJsonObj(Guid? input, string formType, Guid? diagnoseId)
        //{
        //    var patientVisit = await _uowPatientDiagnose.Repository.GetALL(x => x.PatientVisitId == input && x.PatientDiagnoseId == diagnoseId)
        //        .Include(x => x.PatientVisit).ThenInclude(x => x!.PatientVitals)
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

        //        IsReferred = y.PatientVisit.IsReferred,
        //        ReferredDepartmentLookupId = y.PatientVisit.ReferredDepartmentLookupId,
        //        ReferredDepartmentName = y.PatientVisit.ReferredDepartmentLookup!.Name,
        //        ReferredSectionLookupId = y.PatientVisit.ReferredSectionLookupId,
        //        ReferredSectionName = y.PatientVisit.ReferredSectionLookup!.Name,

        //        ReferredToDepartmentLookupId = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.DepartementLookupId : null,
        //        ReferredToDepartmentName = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.DepartementLookup!.Name : null,
        //        ReferredToSectionLookupId = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.SectionLookupId : null,
        //        ReferredToSectionName = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.SectionLookup!.Name : null,

        //        ReferredBy = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.ReferredBy : null,
        //        ReferredByName = (y.PatientVisit.IsReferred == true) ? y.PatientVisit.ReferredByNavigation!.FullName : null,


        //        PatientVitals = _mapper.Map<List<PatientVitalDto>>(y.PatientVisit.PatientVitals!.ToList()),
        //        HealthFacilityName = y.PatientVisit.HealthFacility!.Name,
        //        TokenNo = y.PatientVisit.TokenNo,
        //        CreatedOn = y.CreatedOn,
        //        CreatedBy = y.CreatedBy,
        //        IsMlc = y.IsMlc,
        //        IsSendToCdc = y.IsSendToCdc,
        //        PatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && (x.DiagnoseTypeId == 1 || x.DiagnoseTypeId == null)).Select(x => new PatientDiagnosesDiseasesDto
        //        {
        //            PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //            DiseaseProfileId = x.DiseaseProfileId,
        //            DiseasesName = x.DiseaseProfile.Name
        //        }).ToList(),
        //        DefinitivePatientDiagnosesDiseases = y.PatientDiagnoseDiseases.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.DiagnoseTypeId == 2).Select(x => new PatientDiagnosesDiseasesDto
        //        {
        //            PatientDiagnoseDiseaseId = x.PatientDiagnoseDiseaseId.ToString(),
        //            DiseaseProfileId = x.DiseaseProfileId,
        //            DiseasesName = x.DiseaseProfile.Name
        //        }).ToList(),

        //        PatientDiagnoseProcedures = y.PatientDiagnoseProcedures.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => new PatientDiagnoseProcedureDto
        //        {
        //            PatientDiagnoseProcedureId = x.PatientDiagnoseProcedureId.ToString(),
        //            PatientDiagnoseId = x.PatientDiagnoseId.ToString(),
        //            SectionProcedureId = x.SectionProcedureId,
        //            ProcedureTitle = x.SectionProcedure!.ProcedureTitle,
        //            ProcedureFee = x.ProcedureFee,
        //            IsPerformed = x.IsPerformed,
        //            Feedback = x.Feedback,
        //            RecommendBy = x.RecommendBy,
        //            PerformedBy = x.PerformedBy,
        //            ToothNumber = x.ToothNumber,
        //            ToothPosition = x.ToothPosition,
        //            AssistedBy = x.AssistedBy,


        //        }).ToList(),
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
        //    }).FirstOrDefaultAsync();


        //    foreach (var item in patientVisit!.PatientDiagnosesDiseases)
        //    {
        //        if (string.IsNullOrEmpty(patientVisit.DiseasesName))
        //            patientVisit.DiseasesName += item.DiseasesName;
        //        else
        //            patientVisit.DiseasesName += ", " + item.DiseasesName;
        //    }

        //    foreach (var item in patientVisit!.DefinitivePatientDiagnosesDiseases)
        //    {
        //        if (string.IsNullOrEmpty(patientVisit.DefinitiveDiseasesName))
        //            patientVisit.DefinitiveDiseasesName += item.DiseasesName;
        //        else
        //            patientVisit.DefinitiveDiseasesName += ", " + item.DiseasesName;
        //    }


        //    foreach (var item in patientVisit!.PatientDiagnoseProcedures)
        //    {
        //        if (string.IsNullOrEmpty(patientVisit!.ProceduresName))
        //            patientVisit.ProceduresName += item.ProcedureTitle;
        //        else
        //            patientVisit.ProceduresName += ", " + item.ProcedureTitle;
        //    }

        //    //if (!AppCommonMethod.IsNullOrEmptyList<PatientPrescriptionDto>(patientVisit.PatientMedicine.ToList()))
        //    //{
        //    //    var _uowMimsMedicineData = new UnitOfWork<MimsMedicineDatum>(_uowPatientDiagnose.GetDbContext());

        //    //    var medLookup = await _uowMimsMedicineData.Repository.GetALL().ToListAsync();

        //    //    foreach (var patientMedicine in patientVisit.PatientMedicine)
        //    //        patientMedicine.IsSMLMedicine = medLookup.Where(x => x.MedicineId == patientMedicine.MedicineId).Select(x => x.IsSMLMedicine).FirstOrDefault();
        //    //}

        //    return JsonConvert.SerializeObject(patientVisit);
        //    //return patientVisit;
        //}

        #endregion
    }
}

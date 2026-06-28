using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Service.Interfaces
{
    public interface IWellWomanClinic
    {
        public Task SaveBreastCancerPatient(BreastCancerRiskIdentificationDTO model);
        public Task SaveCVCExamination(CVCExamDTO model);
        public Task SaveBreastClinicalExamination(BreastCBCDTO model);
        public Task SaveGDMPatientwithTrimester(GDMPatientDTO model);
        public Task SaveUltraSoundAndDiagnosisResults(UltraSoundAndDiagnosisDTO model);
        public Task<BreastCancerPatientDetail> GetRiskAssessmentForBreastCancerStatus(Guid PatientId);
        public Task<GDMPatientDTO> CheckGDMData(Guid PatientId);
        public Task<CervicalCancerPatientDetail> GetCervicalCancerPatientDetailByVisitId(Guid PatientVisitId);
    }
}

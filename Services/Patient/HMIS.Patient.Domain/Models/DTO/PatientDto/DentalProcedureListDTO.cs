using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{

    public class DentalProcedurePatientListDTO
    {
        public Guid? PatientVisitId { get; set; }
        public string? FullName { get; set; }
        public string? CNIC { get; set; }
        public string? MRNo { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class DentalProcedureListDTO
    {
        public Guid? PatientDiagnoseProcedureId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? SectionProcedureId { get; set; }
        public string? FullName { get; set; } = "";
        public string? CNIC { get; set; } = "";
        public string? MRNo { get; set; } = "";
        public string? MobileNo { get; set; } = "";
        public int? ProcedureFee { get; set; }
        public string? ProcedureTitle { get; set; }
        public bool? IsPaidProcedureFee { get; set; }
        public bool? IsPerformed { get; set; }
        public Guid? AssistedBy { get; set; }
        public string? Feedback { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? PerformedBy { get; set; }
        public Guid? RecommendBy { get; set; }
        public string? ToothNumber { get; set; }
        public string? ToothPosition { get; set; }
    }




    public class DentalPatientProcedureListDTO
    {
        public Guid? PatientDiagnoseProcedureId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? SectionProcedureId { get; set; }
        public int? ProcedureFee { get; set; }
        public string? ProcedureTitle { get; set; }
        public bool? IsPaidProcedureFee { get; set; }
        public bool? IsPerformed { get; set; }
        public Guid? AssistedBy { get; set; }
        public string? Feedback { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? PerformedBy { get; set; }
        public Guid? RecommendBy { get; set; }
        public string? ToothNumber { get; set; }
        public string? ToothPosition { get; set; }
    }




    public class DentalPatientProcedureListDTOForDashboard
    {
        public int? ProcedureFee { get; set; }
        public string? PatientName { get; set; }
        public string? Cnic { get; set; }
        public string? ProcedureTitle { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
    public class SpecilaityListDTOForDashboard
    {
        public string? PatientName { get; set; }
        public string? Cnic { get; set; }
        public int? PaidAmount { get; set; }
        public string? SectionName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }

    public class ListTotalCount
    {
        public int TotalRecord { get; set; }
    }
}

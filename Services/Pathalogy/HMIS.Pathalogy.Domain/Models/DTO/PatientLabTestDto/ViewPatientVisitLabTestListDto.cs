using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class ViewPatientVisitLabTestListDto
    {
        public Guid PatientLabTestId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public string? BarcodeNo { get; set; }

        public int? LabTestId { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public string? DepartmentShortName { get; set; }

        public string LabDepartmentName { get; set; } = null!;

        
        public string? LabTestName { get; set; }
        public string? LabTestTypeName { get; set; }
        public string? LabTestTypeShortName { get; set; }
        public string? LabTestImage { get; set; }

        public string? AdvisedBy { get; set; }

        public DateTime? AdvisedOn { get; set; }

        public decimal? TestPrice { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsSampleCollected { get; set; }

        public string? SampleCollectedBy { get; set; }

        public DateTime? SampleCollectedOn { get; set; }

        public bool? IsReportGenerated { get; set; }

        public string? ReportGeneratedBy { get; set; }

        public DateTime? ReportGeneratedOn { get; set; }

        public bool? IsSampleRejected { get; set; }

        public string? SampleRejectedBy { get; set; }

        public DateTime? SampleRejectedOn { get; set; }

        public string? SampleRejectedReason { get; set; }
       
    }
}

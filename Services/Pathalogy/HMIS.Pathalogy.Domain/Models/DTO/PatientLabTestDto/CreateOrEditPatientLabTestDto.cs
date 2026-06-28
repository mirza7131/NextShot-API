using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class CreateOrEditPatientLabTestDto
    {
        public Guid? PatientLabTestId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? LabDepartmentProfileId { get; set; }

        public int? LabTestId { get; set; }

        public bool? IsPendingCollection { get; set; }

        public bool? IsSampleCollected { get; set; }

        public Guid? SampleCollectedBy { get; set; }

        public DateTime? SampleCollectedOn { get; set; }

        public bool? IsReportGenerated { get; set; }

        public Guid? ReportGeneratedBy { get; set; }

        public DateTime? ReportGeneratedOn { get; set; }

        public bool? IsArchived { get; set; }

        public Guid? ArchivedBy { get; set; }

        public DateTime? ArchivedOn { get; set; }

        public Guid? TestAdvisedBy { get; set; }

        public bool? IsActive { get; set; }
        public string? BatchNumber { get; set; }
        public DateTime? BatchCreatedOn { get; set; }
        public Guid? BatchCreatedBy { get; set; }
        public DateTime? BatchResultUploadedOn { get; set; }
        public string? LHWName { get; set; }
        public string? LHWCnic { get; set; }
    }
}

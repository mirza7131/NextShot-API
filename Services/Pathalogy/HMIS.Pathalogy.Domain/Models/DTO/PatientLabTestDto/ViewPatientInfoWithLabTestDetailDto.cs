using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class ViewPatientInfoWithLabTestDetailDto
    {
        public ViewPatientInfoWithLabTestDetailDto()
        {
            PatientLabTestDetails = new List<ViewPatientLabTestDetailDto>();
            SampleRejectedReasonsList = new List<RejectedSampleDto>();
        }
        public Guid PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public DateTime? PatientVisitDate { get; set; }
        public string? HealthFacilityName { get; set; }
        public string PatientName { get; set; } = null!;
        public string? MrNo { get; set; } = null!;
        public string? Cnic { get; set; } = null!;
        public string? MobileNo { get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public DateTime? Dob { get; set; }
        public string? Address { get; set; } = null!;
        public int? Age { get; set; } = null!;
        public string? ResultImageLink { get; set; }
        public string? SampleCollectedBy { get; set; }
        public DateTime? SampleCollectedOn { get; set; }
        public string? ReportGeneratedBy { get; set; }
        public DateTime? ReportGeneratedOn { get; set; }
        public bool? IsSampleRejected { get; set; }
        public string? SampleRejectedBy { get; set; }
        public DateTime? SampleRejectedOn { get; set; }
        public string? SampleRejectedReason { get; set; }
        public List<RejectedSampleDto> SampleRejectedReasonsList { get; set; }
        public string? TestAdvisedBy { get; set; }
        public DateTime? TestAdvisedOn { get; set; }
        public int? LabTestId { get; set; }
        public string? TestName { get; set; }
        public string? BarcodeNo { get; set; }
        public string? PreGenratedBarcodeNo { get; set; }

        public string? Description { get; set; }
        public string? TestCategory { get; set; }
        public string? TestType { get; set; }
        public string? TestTypeShortName { get; set; }
        public bool? IsSampleRequired { get; set; }
        public bool? IsActive { get; set; }
        public List<ViewPatientLabTestDetailDto> PatientLabTestDetails { get; set; }

    }

    public class RejectedSampleDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }

    }
}

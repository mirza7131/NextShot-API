using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class SPPatientLabTestListDto
    {
        public Guid PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }

        public string? StageName { get; set; }
        public byte StatusCode { get; set; }
        public string? StatusName { get; set; }

        public int? LabTestId { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsEdit { get; set; }
        public string? BarcodeNo { get; set; }
        public string? PreGenratedBarcodeNo { get; set; }
        public string LabDepartmentName { get; set; } = null!;
        public string Cnic { get; set; } = null!;
        public string? MrNo { get; set; }
        public string? PatientMobileNo { get; set; }
        public string? PatientName { get; set; }
        public string? LabTestName { get; set; }
        public string? AdvisedBy { get; set; }
        public DateTime? AdvisedOn { get; set; }

        public decimal TestPrice { get; set; }
        public string? SampleType { get; set; }

        public string? DoctorDepartmentName { get; set; }
        public string? DoctorSectionName { get; set; }

        public bool? IsOnBedSample { get; set; }
        public bool? IsFromCallCenter { get; set; }
        public bool? IsSampleRequired { get; set; }

        public bool? IsSampleCollected { get; set; }
        public string? SampleCollectedBy { get; set; }
        public DateTime? SampleCollectedOn { get; set; }

        public bool? IsReportGenerated { get; set; }
        public string? ReportGeneratedBy { get; set; }
        public DateTime? ReportGeneratedOn { get; set; }

        public bool? IsSampleRejected { get; set; }
        public string? SampleRejectedBy { get; set; }
        public DateTime? SampleRejectedOn { get; set; }

        public bool? IsAdvisedExternally { get; set; }

        public string? SourceDoctorName { get; set; }
        public string? HealthFacilityName { get; set; }

        public String? SampleRejectedReason { get; set; }
        //public String? SampleRejectedReasonName { get; set; }

        public string? LabType { get; set; }

        public string? LabTypeShortName { get; set; }
        public string? ReportLink { get; set; }
        public string? ResultImageLink { get; set; }
        
        public Guid? SampleConsignmentDetailId { get; set; }
        public Guid? ConsignmentLabTestStatusUpdatedBy { get; set; }
    }
    public partial class SPPatientLabTestListTotalCount
    {
        public int TotalRecord { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.SampleCollectedConsignmentListDto
{
    public class SampleCollectedConsignmentListDTO
    {
        public int? TotalCount { get; set; }
        public List<SampleCollectedConsignmentList> List { get; set; }
    }
    public class SampleCollectedConsignmentList
    {
        public Guid PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? LabTestId { get; set; }
        public string? BarcodeNo { get; set; }
        public string? SampleCollectedBy { get; set; }
        public string Cnic { get; set; } = null!;
        public string? MrNo { get; set; }
        public string? PatientName { get; set; }
        public string? LabTestName { get; set; }
        public bool IsActive { get; set; }
        //public string? SampleType { get; set; }
        public DateTime? AdvisedOn { get; set; }
        public string? PatientMobileNo { get; set; }
        public string? HealthFacilityName { get; set; }
        public int? ConsignmentToHealthFacilityId { get; set; }
        public string? BatchNumber { get; set; }
        public Guid? SampleConsignmentId { get; set; }
    }
}

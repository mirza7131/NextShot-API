using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.ReceiptDto
{
    public class ViewPathalogyRegistrationSlipDto
    {
        public string HealthFacilityName { get; set; } = null!;
        public string TokenNo { get; set; } = null!;

        public Guid? PatientVisitid { get; set; }
        public int? VisitNo { get; set; }
        public DateTime? VisitDate { get; set; }

        public string PatientName { get; set; } = null!;
        public string? MrNo { get; set; } = null!;
        public string? Cnic { get; set; } = null!;
        public string? MobileNo { get; set; } = null!;
        public string? Gender { get; set; } = null!;
        public DateTime? Dob { get; set; }

        public int? Age { get; set; }
        public int? TestId { get; set; }
        public string? BarcodeNo { get; set; }
        public string TestName { get; set; } = null!;
        public string AdvisedBy { get; set; } = null!;
        public DateTime? AdvisedOn { get; set; }

        public string SampleTransportMode { get; set; }
        public string SampleCollectedby { get; set; } = null!;
        public DateTime? SampleCollectedOn { get; set; }
        public string? LHWName { get; set; }
        public string? LHWCnic { get; set; }

    }
}

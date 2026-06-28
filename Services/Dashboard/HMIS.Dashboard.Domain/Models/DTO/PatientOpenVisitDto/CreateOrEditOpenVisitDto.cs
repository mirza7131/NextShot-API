using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientOpenVisitDto
{
    public class CreateOrEditOpenVisitDto
    {
        public Guid? PatientOpenVisitId { get; set; }

        public string? TokenNo { get; set; }
        public string? BedNo { get; set; }

        public int? VisitNo { get; set; }

        public Guid? VisitTypeProfileId { get; set; }

        public Guid? PatientId { get; set; }

        public int? HealthFacilityId { get; set; }

        public int? DepartementLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public Guid? CurrentStationProfileId { get; set; }

        public Guid? CurrentStationUserId { get; set; }

        public DateTime? VisitDate { get; set; }

        public bool? IsDischarge { get; set; }

        public bool? IsVisitExternally { get; set; }

        public string? SourceVisitId { get; set; }
        public Guid? SourceSystemId { get; set; }
        public bool IsActive { get; set; }
        public bool? IsEligibleForSsc { get; set; }
        public string? SscNumber { get; set; }
        public bool? IsSscClaimed { get; set; }

    }

    public class PatientVisitSscClaimedDto
    {
        public Guid? PatientOpenVisitId { get; set; }
        public bool? IsSscClaimed { get; set; }
        public DateTime? SScClaimedDate { get; set; }
        public Guid? ReasonIfSscNotClaimed { get; set; }
        

    }
}

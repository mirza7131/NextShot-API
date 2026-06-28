using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.PatientContactDetailsDTO
{
    public class CreatePatientContactDetailsDTO
    {
        public Guid? ContactId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public int? SectionLookupId { get; set; }
        public string? ContactName { get; set; }
        public string? ContactNo { get; set; }
        public string? Relation { get; set; }
        public int? Age { get; set; }
        public int? ProvinceId { get; set; }
        public int? DivisionId { get; set; }
        public int? DistrictId { get; set; }
        public int? TehsilId { get; set; }
        public bool? IsSputumCollected { get; set; }
        public Guid? CollectedBy { get; set; }

    }
}

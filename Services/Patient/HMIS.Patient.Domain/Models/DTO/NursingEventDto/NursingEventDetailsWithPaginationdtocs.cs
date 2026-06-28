using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.NursingEventDto
{
    //public class NursingEventDetailsWithPaginationdtocs
    //{
    //    public int? TotalRecord { get; set; }
    //    public List<NursingEventDetailList> NursingEventList { get; set; } = new List<NursingEventDetailList>();
    //}

    public class NursingEventDetailList
    {
        public Guid NursingEventsId { get; set; }

        public Guid? PatientId { get; set; }

        public Guid? PatientVisitId { get; set; }

        public Guid? PatientDiagnoseId { get; set; }

        public long? HealthFacilityId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public int? SectionLookupId { get; set; }

        public string? Events { get; set; }

        public DateTime? AcknowledgedOn { get; set; }

        public Guid? AcknowledgedBy { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? DeletedOn { get; set; }

        public Guid? DeletedBy { get; set; }

        public byte ActionTypeId { get; set; }
    }
}
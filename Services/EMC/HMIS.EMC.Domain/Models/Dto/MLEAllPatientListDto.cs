using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class MLEAllPatientListDto
    {
        public string? Mrno { get; set; }
        public string? FullName { get; set; }
        public string Cnic { get; set; } = null!;
        public Guid PatientId { get; set; }
        public Guid PatientVisitId { get; set; }
        public Guid? PatientDiagnoseId { get; set; }
        public int? HealthFacilityId { get; set; }
        public int? Age { get; set; }
        public string? Mlcno { get; set; }

        //public string? PatientImage { get; set; }
        public string? Relation { get; set; }
        public string? DoctorName { get; set; }
        public string? MobileNo { get; set; }
        public string? Gender { get; set; }
        public long? ReportCounts { get; set; }

        public bool? IsFinalReport { get; set; }

        public DateTime? CreatedOn { get; set; }

      
    }
}
